using CefSharp;
using CefSharp.WinForms;
using Newtonsoft.Json.Linq;
using PuppeteerExtraSharp;
using PuppeteerExtraSharp.Plugins.ExtraStealth;
using PuppeteerSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Numerics;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebSocketSharp;
using Yove.Proxy;

namespace RBX_Alt_Manager.Classes
{
    internal class AccountBrowser
    {
        public static BrowserFetcher Fetcher = new BrowserFetcher(Product.Chrome);

        private static Dictionary<int, Vector2> ScreenGrid;

        private readonly Dictionary<string, string> Images = new Dictionary<string, string>();
        private readonly HashSet<string> Solved = new HashSet<string>();
        private ProxyClient Proxy = null;
        private string Password;

        public Browser browser;
        public Page page;
        public Vector2 Size = new Vector2(880, 740), Position;
        public int Index = -1;

        public AccountBrowser() { }

        public AccountBrowser(Account account, string Url = null, string Script = null, Func<Page, Task> PostNavigation = null)
        {
            _ = LaunchBrowser(Url ?? string.Empty, Script: Script, PostNavigation: PostNavigation, PostPageCreation: () => page.SetCookieAsync(new CookieParam
            {
                Name = ".ROBLOSECURITY",
                Domain = ".roblox.com",
                Expires = (DateTime.Now.AddYears(1) - DateTime.MinValue).TotalSeconds,
                HttpOnly = true,
                Secure = true,
                Url = "https://roblox.com",
                Value = account.SecurityToken
            }));
        }

        public async Task LaunchBrowser(string Url = "https://roblox.com/", Func<Task> PostPageCreation = null, Func<Page, Task> PostNavigation = null, string Script = null, string[] Arguments = null)
        {
            if (string.IsNullOrEmpty(Url)) Url = "https://roblox.com/";

            if (Position != null && Index >= 0 && ScreenGrid != null && ScreenGrid.Count > 0)
                Position = ScreenGrid[Index % (ScreenGrid.Count - 1)];
            else
                Position = new Vector2(Screen.PrimaryScreen.WorkingArea.Width / 2 - (Size.X / 2), Screen.PrimaryScreen.WorkingArea.Height / 2 - (Size.Y / 2));

            List<string> Args = new List<string>(Arguments ?? new string[] { "--disable-web-security" });

            string ExtensionPath = Path.Combine(Environment.CurrentDirectory, "extension");
            string ConfigPath = Path.Combine(Environment.CurrentDirectory, "BrowserConfig.json");
            BrowserConfig Config = null;

            if (Directory.Exists(ExtensionPath))
                Args.AddRange(new string[] { $@"--disable-extensions-except=""{ExtensionPath}""", $@"--load-extension=""{ExtensionPath}""" });
            if (File.Exists(ConfigPath))
                File.ReadAllText(ConfigPath).TryParseJson(out Config);

            if (Config?.CustomArguments != null) Args.AddRange(Config.CustomArguments);

            if (Arguments == null)
                Args.AddRange(new string[] { $"--window-size=\"{(int)Size.X},{(int)Size.Y}\"", $"--window-position=\"{(int)Position.X},{(int)Position.Y}\"" });

            string ProxiesPath = Path.Combine(Environment.CurrentDirectory, "proxies.txt");
            string ProxyString = string.Empty, Username = string.Empty, Password = string.Empty;

            if (AccountManager.General.Get<bool>("UseProxies") && File.Exists(ProxiesPath))
            { // Format: [optional protocol://]ip:port@user:pass / socks5://1.2.3.4:999@user:pass
                Random Rng = new Random();
                int Timeout = AccountManager.General.Exists("ProxyTimeout") ? AccountManager.General.Get<int>("ProxyTimeout") : 3000;
                int Limit = AccountManager.General.Exists("ProxyTestLimit") ? AccountManager.General.Get<int>("ProxyTestLimit") : 10;
                List<string> Proxies = File.ReadAllLines(ProxiesPath).ToList();

                Proxies.OrderBy(x => Rng.Next());

                for (int i = 0; i < Limit; i++)
                {
                    if (i > Proxies.Count - 1) break;

                    ProxyString = Proxies[i];

                    string _Proxy = ProxyString;

                    if (_Proxy.Contains("://"))
                        _Proxy = _Proxy.Substring(_Proxy.IndexOf("://") + 3);

                    Uri ProxyUrl = new Uri($"http://{(_Proxy.Contains("@") ? _Proxy.Substring(0, _Proxy.IndexOf('@')) : _Proxy)}");

                    if (ProxyString.Contains("@") && ProxyString.Substring(ProxyString.IndexOf('@')).Contains(':'))
                    {
                        string Combo = ProxyString.Substring(ProxyString.IndexOf('@') + 1);

                        ProxyString = ProxyString.Substring(0, ProxyString.IndexOf('@'));
                        Username = Combo.Substring(0, Combo.IndexOf(':'));
                        Password = Combo.Substring(Combo.IndexOf(':') + 1);
                    }

                    ProxyType Protocol = ProxyType.Http;

                    if (ProxyString.StartsWith("socks5://"))
                        Protocol = ProxyType.Socks5;
                    else if (ProxyString.StartsWith("socks4://"))
                        Protocol = ProxyType.Socks4;

                    Proxy = new ProxyClient(ProxyUrl.Host, ProxyUrl.Port, Username, Password, Protocol);

                    using (var Handler = new HttpClientHandler() { Proxy = Proxy })
                    using (var Client = new HttpClient(Handler) { Timeout = TimeSpan.FromMilliseconds(Timeout) })
                        try { (await Client.GetAsync("https://auth.roblox.com/")).EnsureSuccessStatusCode(); } catch { ProxyString = string.Empty; }

                    if (!string.IsNullOrEmpty(ProxyString)) break;
                }

                if (!string.IsNullOrEmpty(ProxyString))
                {
                    ProxyString = Proxy?.GetProxy(null).Authority ?? ProxyString;

                    if (ProxyString.StartsWith("http://") || ProxyString.StartsWith("https://"))
                        ProxyString = ProxyString.Substring(ProxyString.IndexOf("://") + 3);

                    Args.Add($"--proxy-server={ProxyString}");
                }
            }

            var Options = new LaunchOptions { Headless = false, DefaultViewport = null, Args = Args.ToArray(), IgnoreHTTPSErrors = true };

            await Fetcher.DownloadAsync(BrowserFetcher.DefaultChromiumRevision);

            browser = (Browser)await new PuppeteerExtra().Use(new StealthPlugin()).LaunchAsync(Options);
            page = (Page)(await browser.PagesAsync())[0];

            if (Proxy != null) browser.Disconnected += (s, e) => Proxy.Dispose();

            await page.SetUserAgentAsync("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/111.0.0.0 Safari/537.36");

            if (Config?.PreNavigateActions != null)
                foreach (var action in Config.PreNavigateActions)
                {
                    if (!Uri.IsWellFormedUriString(action.Key, UriKind.Absolute)) continue;

                    await page.GoToAsync(action.Key);
                    await page.EvaluateExpressionAsync(action.Value);
                }

            if (Config?.PreNavigateScripts != null)
                foreach (var script in Config.PreNavigateScripts)
                    await page.EvaluateExpressionAsync(script);

            if (PostPageCreation != null) try { await PostPageCreation(); } catch { }

            try { await page.GoToAsync(Url, new NavigationOptions { Referer = "https://google.com/", Timeout = 300000 }); } catch { }

            if (!string.IsNullOrEmpty(Script)) await page.EvaluateExpressionAsync(Script);

            page.FrameAttached += Page_FrameAttached;

            if (PostNavigation != null) try { await PostNavigation(page); } catch { }

            if (Config?.PostNavigateScripts != null)
                foreach (var script in Config.PostNavigateScripts)
                    await page.EvaluateExpressionAsync(script);
        }

        public async Task Login(string Username = "", string Password = "", string[] Arguments = null) => await LaunchBrowser("https://roblox.com/login", Arguments: Arguments, PostNavigation: async (page) => await LoginTask(page, Username, Password));

        public async Task LoginTask(Page page, string Username = "", string Password = "")
        {
            page.RequestFinished += Page_RequestFinished;

            await page.EvaluateExpressionAsync(@"document.body.classList.remove(""light-theme"");document.body.classList.add(""dark-theme"");");

            try
            {
                if (!string.IsNullOrEmpty(Username) && await page.WaitForSelectorAsync("#login-username", new WaitForSelectorOptions() { Timeout = 5000 }) != null)
                    await page.TypeAsync("#login-username", Username);
                if (!string.IsNullOrEmpty(Password) && await page.WaitForSelectorAsync("#login-password", new WaitForSelectorOptions() { Timeout = 5000 }) != null)
                    await page.TypeAsync("#login-password", Password);

                if (!string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password)) await page.ClickAsync("#login-button");
            }
            catch (Exception ex) { Program.Logger.Error($"An exception was caught while trying to automatically log in: {ex}"); }
        }

        private async void Page_FrameAttached(object sender, FrameEventArgs e)
        {
            if (!AccountManager.General.Exists("NopechaKey")) return;

            string APIKey = AccountManager.General.Get<string>("NopechaKey");

            var Start = DateTime.Now;

            while (string.IsNullOrEmpty(e.Frame.Name) && (DateTime.Now - Start).TotalSeconds < 10)
                await Task.Delay(200);

            if ((DateTime.Now - Start).TotalSeconds > 3) return;

            if (e.Frame.Name == "CaptchaFrame" || e.Frame.Name == "CaptchaFrame2" || e.Frame.Name == "game-core-frame")
            {
                using HttpClient client = new HttpClient();
                try // :|
                {
                    while (browser.IsConnected && !page.IsClosed && !e.Frame.Detached)
                    {
                        await Task.Delay(500);

                        if (e.Frame.QuerySelectorAsync("#app") == null && e.Frame.QuerySelectorAsync("#root") == null) continue;

                        try // :|
                        {
                            var VerifyButton = await e.Frame.QuerySelectorAsync("#home_children_button");
                            var RetryButton = await e.Frame.QuerySelectorAsync("#wrong_children_button");

                            if (VerifyButton == null && await e.Frame.XPathAsync("//*[@id=\"root\"]/div/div[1]/button") is IElementHandle[] VB && VB.Length > 0) VerifyButton = VB[0];

                            if (VerifyButton != null) await VerifyButton.ClickAsync();
                            else if (RetryButton != null) await RetryButton.ClickAsync();

                            var CaptchaImage = await e.Frame.QuerySelectorAsync("#game_challengeItem_image");
                            var TaskElement = await e.Frame.XPathAsync("//*[@id=\"game_children_text\"]/h2");

                            if (CaptchaImage == null && await e.Frame.XPathAsync("//*[@id=\"root\"]/div/div[1]/div/div[3]/div/button[1]") is IElementHandle[] CI && CI.Length > 0) CaptchaImage = CI[0];
                            if (TaskElement.Length < 1) TaskElement = await e.Frame.XPathAsync("//*[@id=\"root\"]/div/div[1]/div/div[1]/h2");

                            if (TaskElement.Length < 1) continue;

                            string TaskString = await e.Frame.EvaluateFunctionAsync<string>("e => e.textContent", TaskElement[0]);

                            if (!string.IsNullOrEmpty(TaskString) && CaptchaImage != null)
                            {
                                string Source = await e.Frame.EvaluateFunctionAsync<string>("e => e.src", CaptchaImage);

                                if (string.IsNullOrEmpty(Source) && await e.Frame.EvaluateFunctionAsync<bool>("e => e instanceof HTMLButtonElement", CaptchaImage))
                                    Source = await e.Frame.EvaluateFunctionAsync<string>("x => new Promise(a=>fetch(x.style.backgroundImage.match(/(?!^)\".*?\"/g)[0].replaceAll('\"', '')).then(e=>e.blob()).then(e=>{const t=new FileReader;t.onload=()=>a(t.result),t.readAsDataURL(e)}))", CaptchaImage);

                                if (string.IsNullOrEmpty(Source)) continue;

                                if (Images.ContainsKey(Source) && !Solved.Contains(Source))
                                {
                                    var Response = await client.GetAsync($"https://api.nopecha.com/?key={APIKey}&id={Images[Source]}");

                                    JObject Data = JObject.Parse(await Response.Content.ReadAsStringAsync());

                                    if (Data.ContainsKey("data") && !Data.ContainsKey("error"))
                                    {
                                        int CorrectIndex = Array.IndexOf(Data["data"].ToObject<bool[]>(), true);
                                        var Correct = await e.Frame.XPathAsync($"//*[@id=\"image{CorrectIndex + 1}\"]/a");

                                        if (Correct.Length < 1)
                                            Correct = await e.Frame.XPathAsync($"//*[@id=\"root\"]/div/div[1]/div/div[3]/div/button[{CorrectIndex + 1}]");

                                        if (Correct != null && Correct.Length > 0)
                                        {
                                            try { await e.Frame.EvaluateExpressionAsync($"var x=document.evaluate('//*[@id=\"image{CorrectIndex + 1}\"]/a', document, null, XPathResult.FIRST_ORDERED_NODE_TYPE, null).singleNodeValue;x.style.backgroundColor='#00ff00';x.style.opacity='24%'"); } catch { }
                                            try { await e.Frame.EvaluateExpressionAsync($"var x=document.evaluate('//*[@id=\"root\"]/div/div[1]/div/div[3]/div/button[{CorrectIndex + 1}]', document, null, XPathResult.FIRST_ORDERED_NODE_TYPE, null).singleNodeValue;x.style.opacity='60%';setTimeout(()=>{{temp1.style.opacity = '100%';}},3000);"); } catch { }

                                            if (AccountManager.General.Get<bool>("NopechaAutoSolve")) await Correct[0].ClickAsync();

                                            Solved.Add(Source);
                                        }

                                        await Task.Delay(1800);
                                    }
                                }
                                else
                                {
                                    var Response = await client.PostAsync("https://api.nopecha.com/", JsonContent.Create(new { key = APIKey, type = "funcaptcha", task = TaskString, image_data = new string[] { Source.Substring(23) } }));
                                    Response.EnsureSuccessStatusCode();

                                    JObject data = JObject.Parse(await Response.Content.ReadAsStringAsync());

                                    if (!data.ContainsKey("error") && data.ContainsKey("data"))
                                    {
                                        Images.Add(Source, data?["data"]?.Value<string>() ?? "");

                                        await Task.Delay(500);
                                    }
                                }
                            }
                        }
                        catch { }
                    }
                }
                catch { } // Ignore exceptions
            }
        }

        private async void Page_RequestFinished(object sender, RequestEventArgs e)
        {
            Uri Url = new Uri(e.Request.Url);

            if (e.Request.Response.Status == HttpStatusCode.OK && e.Request.Method == HttpMethod.Post && Url.Host == "auth.roblox.com")
            {
                if ((Url.AbsolutePath == "/v2/login" || Url.AbsolutePath == "/v2/signup") && e.Request.PostData != null && Utilities.TryParseJson((string)e.Request.PostData, out JObject LoginData))
                {
                    if (LoginData?["password"]?.Value<string>() is string password && !string.IsNullOrEmpty(password) && LoginData?["ctype"].Value<string>() is string loginType && loginType.ToLowerInvariant() == "username")
                        Password = password;

                    if ((await page.GetCookiesAsync("https://roblox.com/")).FirstOrDefault(Cookie => Cookie.Name == ".ROBLOSECURITY") is CookieParam Cookie)
                        await AddAccount(Cookie);
                }
                else if (Regex.IsMatch(Url.AbsolutePath, "/users/[0-9]+/two-step-verification/login") && (await page.GetCookiesAsync("https://roblox.com/")).FirstOrDefault(Cookie => Cookie.Name == ".ROBLOSECURITY") is CookieParam Cookie)
                    await AddAccount(Cookie);
            }
        }

        private async Task AddAccount(CookieParam SecurityToken)
        {
            if (Proxy == null)
                AccountManager.AddAccount(SecurityToken.Value, Password);
            else
            {
                await page.WaitForNavigationAsync();
                AccountManager.AddAccount(SecurityToken.Value, Password, await page.EvaluateFunctionAsync<string>("() => { return fetch('/my/account/json').then(x=>x.text()); }"));
            }

            Password = null;

            await browser.DisposeAsync();
        }

        public static void CreateGrid(Vector2 Size)
        {
            ScreenGrid = new Dictionary<int, Vector2>();

            for (int x = 0; x < SystemInformation.VirtualScreen.Width; x += (int)Size.X)
                for (int y = 0; y < SystemInformation.VirtualScreen.Height - (Size.Y / 2); y += (int)Size.Y)
                    ScreenGrid.Add(ScreenGrid.Count, new Vector2(x, y));
        }
    }

    internal class CefBrowser : Form
    {
        public static CefBrowser Instance
        {
            get
            {
                BrowserForm ??= new CefBrowser();

                return BrowserForm;
            }
        }
        private static CefBrowser BrowserForm;
        public ChromiumWebBrowser browser { get; private set; }
        public bool BrowserMode = false;
        private string Password;

        private CefBrowser(string Url = "https://roblox.com/")
        {
            if (!Directory.Exists(Path.Combine(Environment.CurrentDirectory, "x86"))) throw new DirectoryNotFoundException("Unable to locate CefSharp dependency folder");
            if (!File.Exists(Path.Combine(Environment.CurrentDirectory, "x86", "CefSharp.dll"))) throw new FileNotFoundException("Unable to locate CefSharp.dll");

            Size = new System.Drawing.Size(880, 740);

            FormClosing += (s, e) =>
            {
                e.Cancel = true;
                Hide();
                Cef.GetGlobalCookieManager().DeleteCookies();
            };

            AccountManager.SetDarkBar(Handle);

            browser = new ChromiumWebBrowser(Url);
            browser.AddressChanged += OnNavigated;
            browser.FrameLoadEnd += OnPageLoaded;

            Controls.Add(browser);
        }

        public void Login()
        {
            Cef.GetGlobalCookieManager().DeleteCookies();
            
            BrowserMode = false;

            browser.Load("https://roblox.com/login");

            Show();
        }

        public void EnterBrowserMode(Account account, string URL = null)
        {
            Cef.GetGlobalCookieManager().SetCookie("https://roblox.com", new CefSharp.Cookie()
            {
                Name = ".ROBLOSECURITY",
                Domain = ".roblox.com",
                Expires = DateTime.Now.AddYears(1),
                HttpOnly = true,
                Secure = true,
                Value = account.SecurityToken
            });

            BrowserMode = true;

            browser.Load(URL ?? "https://roblox.com/home");

            Show();
        }

        private async void OnNavigated(object sender, AddressChangedEventArgs args)
        {
            Program.Logger.Info($"Browser Navigated to {args.Address}"); // someone has an issue where they land on a home page and nothing happens

            string url = args.Address;

            if (url.Contains("/home") && !BrowserMode)
            {
                var cookieManager = Cef.GetGlobalCookieManager();

                await cookieManager.VisitAllCookiesAsync().ContinueWith(t =>
                {
                    if (t.Status == TaskStatus.RanToCompletion)
                    {
                        List<CefSharp.Cookie> cookies = t.Result;

                        CefSharp.Cookie RSec = cookies.Find(x => x.Name == ".ROBLOSECURITY");

                        if (RSec != null)
                        {
                            AccountManager.AddAccount(RSec.Value, Password);

                            Cef.GetGlobalCookieManager().DeleteCookies();

                            this.InvokeIfRequired(() => Hide());
                        }

                        Password = string.Empty;
                    }
                });
            }
        }

        private void OnPageLoaded(object sender, FrameLoadEndEventArgs args)
        {
            Task.Factory.StartNew(async () =>
            {
                await Task.Delay(200);

                while (Visible && args.Url == browser.Address)
                {
                    JavascriptResponse response = await browser.EvaluateScriptAsync("document.getElementById('login-password').value");

                    if (!response.Success)
                        response = await browser.EvaluateScriptAsync("document.getElementById('signup-password').value");

                    if (response.Success)
                        Password = (string)response.Result;

                    await Task.Delay(50);
                }
            });

            browser.ExecuteScriptAsyncWhenPageLoaded(@"document.body.classList.remove(""light-theme"");document.body.classList.add(""dark-theme"");");
        }
    }

    internal class BrowserConfig
    {
        public Dictionary<string, string> PreNavigateActions;

        public List<string> PreNavigateScripts;
        public List<string> PostNavigateScripts;

        public List<string> CustomArguments;
    }
}
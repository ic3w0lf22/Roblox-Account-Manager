using BrightIdeasSoftware;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RBX_Alt_Manager.Classes;
using RBX_Alt_Manager.Forms;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

#pragma warning disable CS0618 // stupid parameter warnings

namespace RBX_Alt_Manager
{
    public partial class AccountManager : Form
    {
        public static AccountManager Instance;
        public static List<Account> AccountsList;
        public static List<Account> SelectedAccounts;
        public static Account SelectedAccount;
        public static RestClient MainClient;
        public static RestClient FriendsClient;
        public static RestClient UsersClient;
        public static RestClient APIClient;
        public static RestClient AuthClient;
        public static RestClient EconClient;
        public static RestClient AccountClient;
        public static RestClient GameJoinClient;
        public static RestClient Web13Client;
        public static string CurrentPlaceId;
        public static string CurrentJobId;
        private AccountAdder aaform;
        private ArgumentsForm afform;
        private ServerList ServerListForm;
        private AccountUtils UtilsForm;
        private ImportForm ImportAccountsForm;
        private AccountFields FieldsForm;
        private ThemeEditor ThemeForm;
        private AccountControl ControlForm;
        private SettingsForm SettingsForm;
        private readonly static DateTime startTime = DateTime.Now;
        public static bool IsTeleport = false;
        public static bool UseOldJoin = false;
        public static string CurrentVersion;
        public OLVListItem SelectedAccountItem;
        private WebServer AltManagerWS;
        private string WSPassword = "";
        private static DateTime LastAccountSave = DateTime.Now;
        private static System.Timers.Timer SaveAccountsTimer;

        public static IniFile IniSettings;
        public static IniSection General;
        public static IniSection Developer;
        public static IniSection WebServer;
        public static IniSection AccountControl;

        private static Mutex rbxMultiMutex;
        private readonly static object saveLock = new object();

        private bool LaunchNext;
        private CancellationTokenSource LauncherToken;

        private delegate void SafeCallDelegateRefresh();
        private delegate void SafeCallDelegateGroup(string Group, OLVListItem Item = null);
        private delegate void SafeCallDelegateRemoveAt(int Index);
        private delegate void SafeCallDelegateUpdateAccountView(Account account);
        private delegate int SafeCallDelegateInvite(object Item);

        private static readonly byte[] Entropy = new byte[] { 0x52, 0x4f, 0x42, 0x4c, 0x4f, 0x58, 0x20, 0x41, 0x43, 0x43, 0x4f, 0x55, 0x4e, 0x54, 0x20, 0x4d, 0x41, 0x4e, 0x41, 0x47, 0x45, 0x52, 0x20, 0x7c, 0x20, 0x3a, 0x29, 0x20, 0x7c, 0x20, 0x42, 0x52, 0x4f, 0x55, 0x47, 0x48, 0x54, 0x20, 0x54, 0x4f, 0x20, 0x59, 0x4f, 0x55, 0x20, 0x42, 0x55, 0x59, 0x20, 0x69, 0x63, 0x33, 0x77, 0x30, 0x6c, 0x66 };

        [DllImport("DwmApi")]
        private static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, int[] attrValue, int attrSize);

        public static void SetDarkBar(IntPtr Handle)
        {
            if (ThemeEditor.UseDarkTopBar && DwmSetWindowAttribute(Handle, 19, new[] { 1 }, 4) != 0)
                DwmSetWindowAttribute(Handle, 20, new[] { 1 }, 4);
        }

        public AccountManager()
        {
            Instance = this;

            ThemeEditor.LoadTheme();

            SetDarkBar(Handle);

            InitializeComponent();

            AccountsList = new List<Account>();
            SelectedAccounts = new List<Account>();

            if (ThemeEditor.UseDarkTopBar) Icon = Properties.Resources.team_KX4_icon_white; // this has to go after or icon wont actually change

            AccountsView.UnfocusedHighlightBackgroundColor = Color.FromArgb(0, 150, 215);
            AccountsView.UnfocusedHighlightForegroundColor = Color.FromArgb(240, 240, 240);

            SimpleDropSink sink = AccountsView.DropSink as SimpleDropSink;
            sink.CanDropBetween = true;
            sink.CanDropOnItem = true;
            sink.CanDropOnBackground = false;
            sink.CanDropOnSubItem = false;
            sink.CanDrop += Sink_CanDrop;
            sink.Dropped += Sink_Dropped;
            sink.FeedbackColor = Color.FromArgb(33, 33, 33);

            AccountsView.AlwaysGroupByColumn = Group;

            Group.GroupKeyGetter = delegate (object account)
            {
                return ((Account)account).Group;
            };

            Group.GroupKeyToTitleConverter = delegate (object Key)
            {
                string GroupName = Key as string;
                Match match = Regex.Match(GroupName, @"\d{1,3}\s?");

                if (match.Success)
                    return GroupName.Substring(match.Length);
                else
                    return GroupName;
            };
        }

        private void Sink_CanDrop(object sender, OlvDropEventArgs e)
        {
            if (e.DataObject.GetType() != typeof(OLVDataObject) && e.DragEventArgs.Data.GetDataPresent(DataFormats.Text))
                e.Effect = DragDropEffects.Copy;
        }

        private void Sink_Dropped(object sender, OlvDropEventArgs e)
        {
            if (e.Effect == DragDropEffects.Copy)
            {
                string Text = (string)e.DragEventArgs.Data.GetData(DataFormats.Text);

                var RSecRegex = new Regex(@"(_\|WARNING:-DO-NOT-SHARE-THIS\.--Sharing-this-will-allow-someone-to-log-in-as-you-and-to-steal-your-ROBUX-and-items\.\|\w+)");
                MatchCollection RSecMatches = RSecRegex.Matches(Text);

                foreach (Match match in RSecMatches)
                    AddAccount(match.Value);
            }
        }

        private readonly static string SaveFilePath = Path.Combine(Environment.CurrentDirectory, "AccountData.json");

        private void RefreshView()
        {
            if (AccountsView.InvokeRequired)
            {
                var refreshView = new SafeCallDelegateRefresh(RefreshView);
                AccountsView.Invoke(refreshView);
            }
            else
            {
                AccountsView.BuildList(true);
                AccountsView.BuildGroups();
            }
        }

        private void LoadAccounts()
        {
            if (File.Exists(SaveFilePath))
            {
                try
                {
                    string Decoded = Encoding.UTF8.GetString(ProtectedData.Unprotect(File.ReadAllBytes(SaveFilePath), Entropy, DataProtectionScope.CurrentUser));

                    AccountsList = JsonConvert.DeserializeObject<List<Account>>(Decoded);
                }
                catch
                {
                    try
                    {
                        AccountsList = JsonConvert.DeserializeObject<List<Account>>(File.ReadAllText(SaveFilePath));
                    }
                    catch (Exception x)
                    {
                        MessageBox.Show("Failed to load accounts!\nA backup file was created.\n\n" + x);

                        File.WriteAllText(SaveFilePath + ".bak", File.ReadAllText(SaveFilePath));
                    }
                }
            }

            if (AccountsList == null) AccountsList = new List<Account>();

            if (AccountsList.Count == 0 && File.Exists(SaveFilePath + ".backup"))
            {
                DialogResult Result = MessageBox.Show("No accounts were loaded but there is a backup file, would you like to load the backup file?", "Roblox Account Manager", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);

                if (Result == DialogResult.Yes)
                {
                    try
                    {
                        string Decoded = Encoding.UTF8.GetString(ProtectedData.Unprotect(File.ReadAllBytes(SaveFilePath + ".backup"), Entropy, DataProtectionScope.CurrentUser));

                        AccountsList = JsonConvert.DeserializeObject<List<Account>>(Decoded);
                    }
                    catch
                    {
                        try
                        {
                            AccountsList = JsonConvert.DeserializeObject<List<Account>>(File.ReadAllText(SaveFilePath + ".backup"));
                        }
                        catch
                        {
                            MessageBox.Show("Failed to load backup file!", "Roblox Account Manager", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                        }
                    }
                }
            }

            AccountsView.SetObjects(AccountsList);
            RefreshView();
        }

        public static void SaveAccounts()
        {
            if ((DateTime.Now - startTime).Seconds < 5 || AccountsList.Count == 0) return;
            if ((DateTime.Now - LastAccountSave).Seconds < 1) return;

            lock (saveLock)
            {
                LastAccountSave = DateTime.Now;

                string OldInfo = File.Exists(SaveFilePath) ? File.ReadAllText(SaveFilePath) : "";
                string SaveData = JsonConvert.SerializeObject(AccountsList);
                int OldSize = Encoding.Unicode.GetByteCount(OldInfo);
                int NewSize = Encoding.Unicode.GetByteCount(SaveData);

                FileInfo OldFile = new FileInfo(SaveFilePath);

                if (OldFile.Exists && NewSize < OldSize || (OldFile.Exists && (DateTime.Now - OldFile.LastWriteTime).TotalHours > 36))
                    File.WriteAllText(SaveFilePath + ".backup", OldInfo);

                File.WriteAllBytes(SaveFilePath, ProtectedData.Protect(Encoding.UTF8.GetBytes(SaveData), Entropy, DataProtectionScope.LocalMachine));
            }
        }

        public static void DelayedSaveAccounts() // Prevent file being locked
        {
            if ((DateTime.Now - startTime).TotalMilliseconds < 2000) return;

            if (SaveAccountsTimer.Enabled)
                SaveAccountsTimer.Stop();

            SaveAccountsTimer.Start();
        }

        public static bool GetUserID(string Username, out long UserId)
        {
            RestRequest request = new RestRequest("users/get-by-username?username=" + Username, Method.GET);
            request.AddHeader("Accept", "application/json");
            IRestResponse response = APIClient.Execute(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                UsernameReponse userData = JsonConvert.DeserializeObject<UsernameReponse>(response.Content);

                UserId = userData.Id;

                return true;
            }

            UserId = -1;

            return false;
        }

        public void UpdateAccountView(Account account)
        {
            if (AccountsView.InvokeRequired)
            {
                var getItem = new SafeCallDelegateUpdateAccountView(UpdateAccountView);
                AccountsView.Invoke(getItem, new object[] { account });
            }
            else
                AccountsView.UpdateObject(account);
        }

        public static Account AddAccount(string SecurityToken, string Password = "")
        {
            Account account = new Account(SecurityToken);

            if (account.Valid)
            {
                account.Password = Password;

                Account exists = AccountsList.FirstOrDefault(acc => acc.UserID == account.UserID);

                if (exists != null)
                {
                    exists.SecurityToken = account.SecurityToken;
                    exists.Password = Password;
                    exists.LastUse = DateTime.Now;

                    Instance.RefreshView();
                }
                else
                {
                    AccountsList.Add(account);

                    Instance.RefreshView();
                }

                SaveAccounts();

                Utilities.InvokeIfRequired(Instance.AccountsView, () => Instance.AccountsView.EnsureModelVisible(account));

                return account;
            }

            return null;
        }

        public static string ShowDialog(string text, string caption) // tbh pasted from stackoverflow
        {
            Form prompt = new Form()
            {
                Width = 270,
                Height = 125,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                Text = caption,
                StartPosition = FormStartPosition.CenterScreen
            };

            Label textLabel = new Label() { Left = 15, Top = 10, Text = text };
            TextBox textBox = new TextBox() { Left = 15, Top = 25, Width = 220 };
            Button confirmation = new Button() { Text = "OK", Left = 15, Width = 100, Top = 50, DialogResult = DialogResult.OK };

            confirmation.Click += (sender, e) => { prompt.Close(); };
            prompt.Controls.Add(textBox);
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(textLabel);
            prompt.AcceptButton = confirmation;

            return prompt.ShowDialog() == DialogResult.OK ? textBox.Text : "";
        }

        private void AccountManager_Load(object sender, EventArgs e)
        {
            int Stupid = 1337;

            try
            {
                bool RanAsAdmin = new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator);

                if (RanAsAdmin)
                    MessageBox.Show("Some features may not work properly if you ran the account manager as admin!", "Roblox Account Manager", MessageBoxButtons.OK, MessageBoxIcon.Error);

                if (Directory.GetParent(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)).FullName.Contains(Path.GetTempPath().Remove(Path.GetTempPath().Length - 1)))
                {
                    MessageBox.Show("bro extract the files, don't run it in winrar");
                    Environment.Exit(Stupid);
                }
            }
            catch { }

            if (File.Exists("AU.exe"))
            {
                if (File.Exists("Auto Update.exe"))
                    File.Delete("Auto Update.exe");

                File.Copy("AU.exe", "Auto Update.exe");
                File.Delete("AU.exe");
            }

            if (Directory.Exists(Path.Combine(Environment.CurrentDirectory, "Update")))
                Directory.Delete(Path.Combine(Environment.CurrentDirectory, "Update"));

            SaveAccountsTimer = new System.Timers.Timer(2500);
            SaveAccountsTimer.Elapsed += SaveTimer_Tick;

            aaform = new AccountAdder();
            afform = new ArgumentsForm();
            ServerListForm = new ServerList();
            UtilsForm = new AccountUtils();
            ImportAccountsForm = new ImportForm();
            FieldsForm = new AccountFields();
            ThemeForm = new ThemeEditor();

            MainClient = new RestClient("https://www.roblox.com/");
            MainClient.CachePolicy = new System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.BypassCache);

            APIClient = new RestClient("https://api.roblox.com/");
            APIClient.CachePolicy = new System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.BypassCache);

            AuthClient = new RestClient("https://auth.roblox.com/");
            AuthClient.CachePolicy = new System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.BypassCache);

            EconClient = new RestClient("https://economy.roblox.com/");
            EconClient.CachePolicy = new System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.BypassCache);

            AccountClient = new RestClient("https://accountsettings.roblox.com/");
            AccountClient.CachePolicy = new System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.BypassCache);

            GameJoinClient = new RestClient("https://gamejoin.roblox.com/");
            GameJoinClient.CachePolicy = new System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.BypassCache);

            Web13Client = new RestClient("https://web.roblox.com/");
            Web13Client.CachePolicy = new System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.BypassCache);

            FriendsClient = new RestClient("https://friends.roblox.com");
            FriendsClient.CachePolicy = new System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.BypassCache);

            UsersClient = new RestClient("https://users.roblox.com");
            UsersClient.CachePolicy = new System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.BypassCache);

            ApplyTheme();

            PlaceID_TextChanged(PlaceID, new EventArgs());

            IniSettings = File.Exists("RAMSettings.ini") ? new IniFile("RAMSettings.ini") : new IniFile();

            General = IniSettings.Section("General");
            Developer = IniSettings.Section("Developer");
            WebServer = IniSettings.Section("WebServer");
            AccountControl = IniSettings.Section("AccountControl");

            if (!General.Exists("CheckForUpdates")) General.Set("CheckForUpdates", "true");
            if (!General.Exists("AccountJoinDelay")) General.Set("AccountJoinDelay", "8");
            if (!General.Exists("AsyncJoin")) General.Set("AsyncJoin", "false");
            if (!General.Exists("DisableAgingAlert")) General.Set("DisableAgingAlert", "false");
            if (!General.Exists("SavePasswords")) General.Set("SavePasswords", "true");
            if (!General.Exists("ServerRegionFormat")) General.Set("ServerRegionFormat", "<city>, <countryCode>", "Visit http://ip-api.com/json/1.1.1.1 to see available format options");

            if (!Developer.Exists("DevMode")) Developer.Set("DevMode", "false");
            if (!Developer.Exists("EnableWebServer")) Developer.Set("EnableWebServer", "false");

            if (!WebServer.Exists("WebServerPort")) WebServer.Set("WebServerPort", "7963");
            if (!WebServer.Exists("AllowGetCookie")) WebServer.Set("AllowGetCookie", "false");
            if (!WebServer.Exists("AllowGetAccounts")) WebServer.Set("AllowGetAccounts", "false");
            if (!WebServer.Exists("AllowLaunchAccount")) WebServer.Set("AllowLaunchAccount", "false");
            if (!WebServer.Exists("AllowAccountEditing")) WebServer.Set("AllowAccountEditing", "false");
            if (!WebServer.Exists("Password")) WebServer.Set("Password", ""); else WSPassword = WebServer.Get("Password");
            if (!WebServer.Exists("EveryRequestRequiresPassword")) WebServer.Set("EveryRequestRequiresPassword", "false");

            if (!AccountControl.Exists("AllowExternalConnections")) AccountControl.Set("AllowExternalConnections", "false");
            if (!AccountControl.Exists("RelaunchDelay")) AccountControl.Set("RelaunchDelay", "60");
            if (!AccountControl.Exists("NexusPort")) AccountControl.Set("NexusPort", "5242");

            PlaceID.Text = General.Exists("SavedPlaceId") ? General.Get("SavedPlaceId") : "5315046213";

            if (!Developer.Get<bool>("DevMode"))
            {
                AccountsStrip.Items.Remove(viewFieldsToolStripMenuItem);
                AccountsStrip.Items.Remove(getAuthenticationTicketToolStripMenuItem);
                AccountsStrip.Items.Remove(copyRbxplayerLinkToolStripMenuItem);
                AccountsStrip.Items.Remove(copySecurityTokenToolStripMenuItem);
                AccountsStrip.Items.Remove(copyAppLinkToolStripMenuItem);
            }
            else
            {
                ImportByCookie.Visible = true;
                OpenApp.Location = new Point(398, 266);
                OpenApp.Size = new Size(70, 23);
                ArgumentsB.Visible = true;
            }

            if (General.Get<bool>("CheckForUpdates"))
            {
                Task.Run(() =>
                {
                    try
                    {
                        ServicePointManager.Expect100Continue = true;
                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;

                        WebClient WC = new WebClient();
                        Assembly assembly = Assembly.GetExecutingAssembly();
                        FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
                        WC.Headers["User-Agent"] = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/95.0.4638.54 Safari/537.36";
                        string Releases = WC.DownloadString("https://api.github.com/repos/ic3w0lf22/Roblox-Account-Manager/releases/latest");
                        Match match = Regex.Match(Releases, @"""tag_name"":\s*""?([^""]+)");

                        if (match.Success)
                        {
                            string version = fvi.FileVersion.Substring(0, match.Groups[1].Value.Length);

                            if (match.Groups[1].Value != version)
                            {
                                DialogResult result = MessageBox.Show("An update is available, would you like to update now?", "Roblox Account Manager", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                                if (result == DialogResult.Yes)
                                {
                                    string AFN = Path.Combine(Directory.GetCurrentDirectory(), "Auto Update.exe");

                                    if (File.Exists(AFN))
                                    {
                                        Process.Start(AFN);
                                        Environment.Exit(1);
                                    }
                                    else
                                    {
                                        MessageBox.Show("You do not have the auto updater downloaded, go to the github page and download the latest release.");
                                        Process.Start("https://github.com/ic3w0lf22/Roblox-Account-Manager/releases");
                                    }
                                }
                            }
                        }
                    }
                    catch { }
                });
            }

            if (!General.Get<bool>("DisableAgingAlert"))
                Username.Renderer = new AccountRenderer();

            try
            {
                if (Developer.Get<bool>("EnableWebServer"))
                {
                    string Port = WebServer.Exists("WebServerPort") ? WebServer.Get("WebServerPort") : "7963";

                    AltManagerWS = new WebServer(SendResponse, $"http://localhost:{Port}/");
                    AltManagerWS.Run();
                }
            }
            catch (Exception x) { MessageBox.Show("Failed to start webserver!\n\n" + x, "Roblox Account Manager", MessageBoxButtons.OK, MessageBoxIcon.Error); }

            try
            {
                WebClient WC = new WebClient();
                string VersionJSON = WC.DownloadString("https://clientsettings.roblox.com/v1/client-version/WindowsPlayer");
                JObject j = JObject.Parse(VersionJSON);

                if (j.TryGetValue("clientVersionUpload", out JToken token))
                    CurrentVersion = token.Value<string>();
            }
            catch { }

            IniSettings.Save("RAMSettings.ini");
        }

        public void ApplyTheme()
        {
            this.BackColor = ThemeEditor.FormsBackground;
            this.ForeColor = ThemeEditor.FormsForeground;

            if (AccountsView.BackColor != ThemeEditor.AccountBackground || AccountsView.ForeColor != ThemeEditor.AccountForeground)
            {
                AccountsView.BackColor = ThemeEditor.AccountBackground;
                AccountsView.ForeColor = ThemeEditor.AccountForeground;

                RefreshView();
            }

            AccountsView.HeaderStyle = ThemeEditor.ShowHeaders ? ColumnHeaderStyle.Nonclickable : ColumnHeaderStyle.None;

            foreach (Control control in this.Controls)
            {
                if (control is Button || control is CheckBox)
                {
                    if (control is Button)
                    {
                        Button b = control as Button;
                        b.FlatStyle = ThemeEditor.ButtonStyle;
                        b.FlatAppearance.BorderColor = ThemeEditor.ButtonsBorder;
                    }

                    if (!(control is CheckBox)) control.BackColor = ThemeEditor.ButtonsBackground;
                    control.ForeColor = ThemeEditor.ButtonsForeground;
                }
                else if (control is TextBox || control is RichTextBox || control is Label)
                {
                    if (control is Classes.BorderedTextBox)
                    {
                        Classes.BorderedTextBox b = control as Classes.BorderedTextBox;
                        b.BorderColor = ThemeEditor.TextBoxesBorder;
                    }

                    if (control is Classes.BorderedRichTextBox)
                    {
                        Classes.BorderedRichTextBox b = control as Classes.BorderedRichTextBox;
                        b.BorderColor = ThemeEditor.TextBoxesBorder;
                    }

                    control.BackColor = ThemeEditor.TextBoxesBackground;
                    control.ForeColor = ThemeEditor.TextBoxesForeground;
                }
            }

            aaform.ApplyTheme();
            afform.ApplyTheme();
            ServerListForm.ApplyTheme();
            UtilsForm.ApplyTheme();
            ImportAccountsForm.ApplyTheme();
            FieldsForm.ApplyTheme();
            ThemeForm.ApplyTheme();

            if (ControlForm != null) ControlForm.ApplyTheme();
            if (SettingsForm != null) SettingsForm.ApplyTheme();
        }

        private List<ServerData> AttemptedJoins = new List<ServerData>();

        private string SendResponse(HttpListenerContext Context)
        {
            HttpListenerRequest request = Context.Request;

            if (!request.IsLocal || request.Url.AbsolutePath == "/favicon.ico") return "";

            if (request.Url.AbsolutePath == "/Running") return "true";

            string Method = request.Url.AbsolutePath.Substring(1);
            string Account = request.QueryString["Account"];
            string Password = request.QueryString["Password"];

            Context.Response.StatusCode = 401;

            if (WebServer.Get<bool>("EveryRequestRequiresPassword") && (WSPassword.Length < 6 || Password != WSPassword)) return "Invalid Password";

            if ((Method == "GetCookie" || Method == "GetAccounts" || Method == "LaunchAccount") && (WSPassword.Length < 6 || Password != WSPassword)) return "Invalid Password";

            Context.Response.StatusCode = 200;

            if (Method == "GetAccounts")
            {
                if (!WebServer.Get<bool>("AllowGetAccounts")) return "Method not allowed";

                string Names = "";
                string GroupFilter = request.QueryString["Group"];

                foreach (Account acc in AccountsList)
                {
                    if (!string.IsNullOrEmpty(GroupFilter) && acc.Group != GroupFilter) continue;

                    Names += acc.Username + ",";
                }

                return Names.Remove(Names.Length - 1);
            }

            if (Method == "GetAccountsJson")
            {
                if (!WebServer.Get<bool>("AllowGetAccounts")) return "Method not allowed";

                string GroupFilter = request.QueryString["Group"];
                bool ShowCookies = request.QueryString["IncludeCookies"] == "true" && WebServer.Get<bool>("AllowGetCookie");

                List<object> Objects = new List<object>();

                foreach (Account acc in AccountsList)
                {
                    if (!string.IsNullOrEmpty(GroupFilter) && acc.Group != GroupFilter) continue;

                    object AccountObject = new
                    {
                        Username = acc.Username,
                        UserId = acc.UserID,
                        Alias = acc.Alias,
                        Description = acc.Description,
                        Group = acc.Group,
                        CurrentCSRFToken = acc.CSRFToken,
                        LastUsed = acc.LastUse.ToRobloxTick(),
                        Cookie = ShowCookies ? acc.SecurityToken : null,
                        Fields = acc.Fields,
                    };


                    Objects.Add(AccountObject);
                }

                return JsonConvert.SerializeObject(Objects);
            }

            Context.Response.StatusCode = 400;

            if (Method == "ImportCookie")
            {
                Account New = AddAccount(request.QueryString["Cookie"]);
                
                if (New != null)
                    Context.Response.StatusCode = 200;

                return New != null ? "true" : "false";
            }

            if (string.IsNullOrEmpty(Account))                return "Empty Account";

            Account account = AccountsList.FirstOrDefault(x => x.Username == Account || x.UserID.ToString() == Account);

            if (account == null || string.IsNullOrEmpty(account.GetCSRFToken()))                 return "Invalid Account";

            Context.Response.StatusCode = 401;

            if (Method == "GetCookie")
            {
                if (!WebServer.Get<bool>("AllowGetCookie")) return "Method not allowed";

                Context.Response.StatusCode = 200;

                return account.SecurityToken;
            }

            if (Method == "LaunchAccount")
            {
                if (!WebServer.Get<bool>("AllowLaunchAccount"))                    return "Method not allowed";

                bool ValidPlaceId = long.TryParse(request.QueryString["PlaceId"], out long PlaceId); if (!ValidPlaceId) return "Invalid PlaceId";

                string JobID = !string.IsNullOrEmpty(request.QueryString["JobId"]) ? request.QueryString["JobId"] : "";
                string FollowUser = request.QueryString["FollowUser"];
                string JoinVIP = request.QueryString["JoinVIP"];

                account.JoinServer(PlaceId, JobID, FollowUser == "true", JoinVIP == "true");

                Context.Response.StatusCode = 200;

                return $"Launched {Account} to {PlaceId}";
            }

            if (Method == "FollowUser") // https://github.com/ic3w0lf22/Roblox-Account-Manager/pull/52
            {
                if (!WebServer.Get<bool>("AllowLaunchAccount")) return "Method not allowed";

                string User = request.QueryString["Username"]; if (string.IsNullOrEmpty(User)) { Context.Response.StatusCode = 400; return "Invalid Username Parameter"; }

                if (!GetUserID(User, out long UserId))
                    return "Failed to get UserId";

                account.JoinServer(UserId, "", true);

                Context.Response.StatusCode = 200;

                return $"Joining {User}'s game on {Account}";
            }

            Context.Response.StatusCode = 200;

            if (Method == "GetCSRFToken") return account.GetCSRFToken();
            if (Method == "GetAlias") return account.Alias;
            if (Method == "GetDescription") return account.Description;

            if (Method == "BlockUser" && !string.IsNullOrEmpty(request.QueryString["UserId"])) return account.BlockUserId(request.QueryString["UserId"], Context: Context);
            if (Method == "UnblockUser" && !string.IsNullOrEmpty(request.QueryString["UserId"])) return account.UnblockUserId(request.QueryString["UserId"], Context: Context);
            if (Method == "UnblockEveryone") return account.UnblockEveryone(Context);
            if (Method == "GetBlockedList") return account.GetBlockedList(Context);

            if (Method == "SetServer" && !string.IsNullOrEmpty(request.QueryString["PlaceId"]) && !string.IsNullOrEmpty(request.QueryString["JobId"]))
            {
                string RSP = account.SetServer(Convert.ToInt64(request.QueryString["PlaceId"]), request.QueryString["JobId"], out bool Success);

                if (!Success)
                    Context.Response.StatusCode = 400;

                return RSP;
            }

            if (Method == "SetRecommendedServer")
            {
                int attempts = 0;
                string res = "-1";

                for (int i = RBX_Alt_Manager.ServerList.servers.Count - 1; i > 0; i--)
                {
                    if (attempts > 10)
                    {
                        Context.Response.StatusCode = 400;

                        return "Too many failed attempts";
                    }

                    ServerData server = RBX_Alt_Manager.ServerList.servers[i];

                    if (AttemptedJoins.FirstOrDefault(x => x.id == server.id) != null) continue;

                    AttemptedJoins.Add(server);

                    attempts++;

                    res = account.SetServer(RBX_Alt_Manager.ServerList.CurrentPlaceID, server.id, out bool Success);

                    if (Success)
                        return res;
                }

                if (string.IsNullOrEmpty(res)) Context.Response.StatusCode = 400;

                return string.IsNullOrEmpty(res) ? "Failed" : res;
            }

            if (Method == "GetField" && !string.IsNullOrEmpty(request.QueryString["Field"])) return account.GetField(request.QueryString["Field"]);
            
            Context.Response.StatusCode = 401;

            if (Method == "SetField" && !string.IsNullOrEmpty(request.QueryString["Field"]) && !string.IsNullOrEmpty(request.QueryString["Value"]))
            {
                if (!WebServer.Get<bool>("AllowAccountEditing")) return "Method not allowed";

                account.SetField(request.QueryString["Field"], request.QueryString["Value"]);

                Context.Response.StatusCode = 200;

                return $"Set Field {request.QueryString["Field"]} to {request.QueryString["Value"]} for {account.Username}";
            }
            if (Method == "RemoveField" && !string.IsNullOrEmpty(request.QueryString["Field"]))
            {
                if (!WebServer.Get<bool>("AllowAccountEditing")) return "Method not allowed";

                account.RemoveField(request.QueryString["Field"]);

                Context.Response.StatusCode = 200;

                return $"Removed Field {request.QueryString["Field"]} from {account.Username}";
            }

            string Body = new StreamReader(request.InputStream).ReadToEnd();

            if (Method == "SetAlias" && !string.IsNullOrEmpty(Body))
            {
                if (!WebServer.Get<bool>("AllowAccountEditing")) return "Method not allowed";

                account.Alias = Body;
                UpdateAccountView(account);

                Context.Response.StatusCode = 200;

                return $"Set Alias of {account.Username} to {Body}";
            }
            if (Method == "SetDescription" && !string.IsNullOrEmpty(Body))
            {
                if (!WebServer.Get<bool>("AllowAccountEditing")) return "Method not allowed";

                account.Description = Body;
                UpdateAccountView(account);

                Context.Response.StatusCode = 200;

                return $"Set Description of {account.Username} to {Body}";
            }
            if (Method == "AppendDescription" && !string.IsNullOrEmpty(Body))
            {
                if (!WebServer.Get<bool>("AllowAccountEditing")) return "Method not allowed";

                account.Description += Body;
                UpdateAccountView(account);

                Context.Response.StatusCode = 200;

                return $"Appended Description of {account.Username} with {Body}";
            }

            Context.Response.StatusCode = 404;

            return "";
        }

        private void AccountManager_Shown(object sender, EventArgs e)
        {
            LoadAccounts();

            if (!General.Get<bool>("DisableMultiRbx"))
            {
                try
                {
                    rbxMultiMutex = new Mutex(true, "ROBLOX_singletonMutex");

                    if (!rbxMultiMutex.WaitOne(TimeSpan.Zero, true) && !General.Get<bool>("HideRbxAlert"))
                        MessageBox.Show("WARNING: Roblox is currently running, multi roblox will not work until you restart the account manager with roblox closed.", "Roblox Account Manager", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                finally { }
            }
        }

        private void Remove_Click(object sender, EventArgs e)
        {
            if (AccountsView.SelectedObjects.Count > 1)
            {
                DialogResult result = MessageBox.Show($"Are you sure you want to remove {AccountsView.SelectedObjects.Count} accounts?", "Remove Accounts", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    foreach (Account acc in AccountsView.SelectedObjects)
                        AccountsList.Remove(acc);

                    RefreshView();

                    SaveAccounts();
                }
            }
            else if (SelectedAccount != null)
            {
                DialogResult result = MessageBox.Show($"Are you sure you want to remove {SelectedAccount.Username}?", "Remove Account", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    AccountsList.RemoveAll(x => x == SelectedAccount);

                    RefreshView();

                    SaveAccounts();
                }
            }
        }

        private void Add_Click(object sender, EventArgs e)
        {
            if (aaform != null && aaform.Visible)
                aaform.HideForm();

            aaform.BrowserMode = false;
            aaform.ShowForm();
        }

        private void AccountsView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (AccountsView.SelectedItems.Count != 1)
            {
                SelectedAccount = null;
                SelectedAccountItem = null;

                if (AccountsView.SelectedObjects.Count > 1)
                    SelectedAccounts = AccountsView.SelectedObjects.Cast<Account>().ToList();

                return;
            }

            SelectedAccount = AccountsView.SelectedObject as Account;
            SelectedAccountItem = AccountsView.SelectedItem;

            if (SelectedAccount == null) return;

            AccountsView.HideSelection = false;

            Alias.Text = SelectedAccount.Alias;
            DescriptionBox.Text = SelectedAccount.Description;

            if (!string.IsNullOrEmpty(SelectedAccount.GetField("SavedPlaceId"))) PlaceID.Text = SelectedAccount.GetField("SavedPlaceId");
            if (!string.IsNullOrEmpty(SelectedAccount.GetField("SavedJobId"))) JobID.Text = SelectedAccount.GetField("SavedJobId");
        }

        private void SetAlias_Click(object sender, EventArgs e)
        {
            foreach (Account account in AccountsView.SelectedObjects)
                account.Alias = Alias.Text;

            RefreshView();
        }

        private void SetDescription_Click(object sender, EventArgs e)
        {
            foreach (Account account in AccountsView.SelectedObjects)
                account.Description = DescriptionBox.Text;

            RefreshView();
        }

        private void JoinServer_Click(object sender, EventArgs e)
        {
            Match IDMatch = Regex.Match(PlaceID.Text, @"\/games\/(\d+)[\/|\?]?"); // idiotproofing

            if (PlaceID.Text.Contains("privateServerLinkCode") && IDMatch.Success)
                JobID.Text = PlaceID.Text;

            PlaceID.Text = IDMatch.Success ? IDMatch.Groups[1].Value : Regex.Replace(PlaceID.Text, "[^0-9]", "");

            bool VIPServer = JobID.TextLength > 4 ? JobID.Text.Substring(0, 4) == "VIP:" : false;

            if (!long.TryParse(PlaceID.Text, out long PlaceId)) return;

            CancelLaunching();

            if (AccountsView.SelectedObjects.Count > 1)
            {
                LauncherToken = new CancellationTokenSource();

                Task.Run(() => LaunchAccounts(SelectedAccounts, PlaceId, VIPServer ? JobID.Text.Substring(4) : JobID.Text, false, VIPServer), LauncherToken.Token);
            }
            else if (SelectedAccount != null)
            {
                string res = SelectedAccount.JoinServer(PlaceId, VIPServer ? JobID.Text.Substring(4) : JobID.Text, false, VIPServer);

                if (!res.Contains("Success"))
                    MessageBox.Show(res);
            }
        }

        private void Follow_Click(object sender, EventArgs e)
        {
            if (SelectedAccount == null) return;

            if (!GetUserID(UserID.Text, out long UserId))
            {
                MessageBox.Show("Failed to get UserId");
                return;
            }

            CancelLaunching();

            if (AccountsView.SelectedObjects.Count > 1)
            {
                LauncherToken = new CancellationTokenSource();

                Task.Run(() => LaunchAccounts(SelectedAccounts, UserId, "", true), LauncherToken.Token);
            }
            else if (SelectedAccount != null)
            {
                string res = SelectedAccount.JoinServer(UserId, "", true);

                if (!res.Contains("Success"))
                    MessageBox.Show(res);
            }
        }

        private void ServerList_Click(object sender, EventArgs e)
        {
            if (ServerListForm.Visible)
            {
                ServerListForm.WindowState = FormWindowState.Normal;
                ServerListForm.BringToFront();
            }
            else
                ServerListForm.Show();

            ServerListForm.Busy = false; // incase it somehow bugs out

            ServerListForm.StartPosition = FormStartPosition.Manual;
            ServerListForm.Top = Top;
            ServerListForm.Left = Right;
        }

        private void HideUsernamesCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            AccountsView.BeginUpdate();

            Username.Width = HideUsernamesCheckbox.Checked ? 0 : 120;

            AccountsView.EndUpdate();
        }

        private void addAccountsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (aaform != null && aaform.Visible)
                aaform.HideForm();

            aaform.ShowForm();
        }

        private void removeAccountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (AccountsView.SelectedObjects.Count > 1)
            {
                DialogResult result = MessageBox.Show($"Are you sure you want to remove {AccountsView.SelectedObjects.Count} accounts?", "Remove Accounts", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    foreach (Account acc in AccountsView.SelectedObjects)
                        AccountsList.Remove(acc);

                    RefreshView();
                    SaveAccounts();
                }
            }
            else if (SelectedAccount != null)
            {
                DialogResult result = MessageBox.Show($"Are you sure you want to remove {SelectedAccount.Username}?", "Remove Account", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    AccountsList.Remove(SelectedAccount);

                    RefreshView();
                    SaveAccounts();
                }
            }
        }

        private void AccountManager_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (AltManagerWS != null)
                AltManagerWS.Stop();

            if (PlaceID == null || string.IsNullOrEmpty(PlaceID.Text)) return;

            General.Set("SavedPlaceId", PlaceID.Text);
            IniSettings.Save("RAMSettings.ini");
        }

        private void BrowserButton_Click(object sender, EventArgs e)
        {
            if (SelectedAccount == null)
            {
                MessageBox.Show("No Account Selected!");
                return;
            }

            UtilsForm.Show();
            UtilsForm.WindowState = FormWindowState.Normal;
            UtilsForm.BringToFront();
        }

        private void reAuthToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedAccount == null) return;

            if (aaform != null && aaform.Visible)
                aaform.HideForm();

            aaform.BrowserMode = false;
            aaform.ShowForm();

            aaform.SetUsername = SelectedAccount.Username;
        }

        private void getAuthenticationTicketToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedAccount == null) return;

            if (SelectedAccount.GetAuthTicket(out string Ticket))
                Clipboard.SetText(Ticket);
        }

        private void copyRbxplayerLinkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedAccount == null) return;

            if (SelectedAccount.GetAuthTicket(out string Ticket))
            {
                bool HasJobId = string.IsNullOrEmpty(JobID.Text);
                double LaunchTime = Math.Floor((DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds * 1000);

                Random r = new Random();
                Clipboard.SetText(string.Format("<roblox-player://1/1+launchmode:play+gameinfo:{0}+launchtime:{4}+browsertrackerid:{5}+placelauncherurl:https://assetgame.roblox.com/game/PlaceLauncher.ashx?request=RequestGame{3}&placeId={1}{2}+robloxLocale:en_us+gameLocale:en_us>", Ticket, PlaceID.Text, HasJobId ? "" : ("&gameId=" + JobID.Text), HasJobId ? "" : "Job", LaunchTime, r.Next(100000, 130000).ToString() + r.Next(100000, 900000).ToString()));
            }
        }

        private void ArgumentsB_Click(object sender, EventArgs e)
        {
            if (afform != null && afform.Visible)
                afform.HideForm();

            afform.ShowForm();
        }

        private void copySecurityTokenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<string> Tokens = new List<string>();

            foreach (Account account in AccountsView.SelectedObjects)
                Tokens.Add(account.SecurityToken);

            Clipboard.SetText(string.Join("\n", Tokens));
        }

        private void copyUsernameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<string> Usernames = new List<string>();

            foreach (Account account in AccountsView.SelectedObjects)
                Usernames.Add(account.Username);

            Clipboard.SetText(string.Join("\n", Usernames));
        }

        private void copyPasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<string> Passwords = new List<string>();

            foreach (Account account in AccountsView.SelectedObjects)
                Passwords.Add($"{account.Username}:{account.Password}");

            Clipboard.SetText(string.Join("\n", Passwords));
        }

        private void PlaceID_TextChanged(object sender, EventArgs e)
        {
            CurrentPlaceId = PlaceID.Text;

            if (PlaceTimer.Enabled) PlaceTimer.Stop();

            PlaceTimer.Start();
        }

        private void PlaceTimer_Tick(object sender, EventArgs e)
        {
            if (APIClient == null) return;

            PlaceTimer.Stop();

            RestRequest request = new RestRequest("Marketplace/ProductInfo?assetId=" + PlaceID.Text, Method.GET);
            request.AddHeader("Accept", "application/json");
            IRestResponse response = APIClient.Execute(request);

            if (response.IsSuccessful && response.StatusCode == HttpStatusCode.OK && (response.Content.StartsWith("{") && response.Content.EndsWith("}")))
            {
                ProductInfo placeInfo = JsonConvert.DeserializeObject<ProductInfo>(response.Content);

                CurrentPlace.Text = placeInfo.Name;
            }
        }

        private void moveToToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (AccountsView.SelectedObjects.Count == 0) return;

            string GroupName = ShowDialog("Group Name", "Move Account to Group");

            if (string.IsNullOrEmpty(GroupName)) GroupName = "Default";

            foreach (Account acc in AccountsView.SelectedObjects)
                acc.Group = GroupName;

            RefreshView();
            DelayedSaveAccounts();
        }

        private void copyAppLinkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedAccount == null) return;

            if (SelectedAccount.GetAuthTicket(out string Ticket))
            {
                bool HasJobId = string.IsNullOrEmpty(JobID.Text);
                double LaunchTime = Math.Floor((DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds * 1000);

                Random r = new Random();
                Clipboard.SetText(string.Format("<roblox-player://1/1+launchmode:app+gameinfo:{0}+launchtime:{1}+browsertrackerid:{2}+robloxLocale:en_us+gameLocale:en_us>", Ticket, LaunchTime, r.Next(500000, 600000).ToString() + r.Next(10000, 90000).ToString()));
            }
        }

        private void JoinDiscord_Click(object sender, EventArgs e)
        {
            Process.Start("https://discord.gg/MsEH7smXY8");
        }

        private void OpenApp_Click(object sender, EventArgs e)
        {
            if (SelectedAccount != null) SelectedAccount.LaunchApp();
        }

        private void ImportByCookie_Click(object sender, EventArgs e)
        {
            ImportAccountsForm.Show();
            ImportAccountsForm.WindowState = FormWindowState.Normal;
            ImportAccountsForm.BringToFront();
        }

        private void copyProfileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<string> Profiles = new List<string>();

            foreach (Account account in AccountsView.SelectedObjects)
                Profiles.Add($"https://www.roblox.com/users/{account.UserID}/profile");

            Clipboard.SetText(string.Join("\n", Profiles));
        }

        private void viewFieldsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedAccount == null) return;

            FieldsForm.View(SelectedAccount);
        }

        private void JobID_TextChanged(object sender, EventArgs e)
        {
            CurrentJobId = JobID.Text;
        }

        private void SaveToAccount_Click(object sender, EventArgs e)
        {
            foreach (Account account in AccountsView.SelectedObjects)
            {
                if (string.IsNullOrEmpty(PlaceID.Text) && string.IsNullOrEmpty(JobID.Text))
                {
                    account.RemoveField("SavedPlaceId");
                    account.RemoveField("SavedJobId");

                    return;
                }

                string PlaceId = CurrentPlaceId;

                if (JobID.Text.Contains("privateServerLinkCode") && Regex.IsMatch(JobID.Text, @"\/games\/(\d+)\/"))
                    PlaceId = Regex.Match(CurrentJobId, @"\/games\/(\d+)\/").Groups[1].Value;

                account.SetField("SavedPlaceId", PlaceId);
                account.SetField("SavedJobId", JobID.Text);
            }
        }

        private void SaveTimer_Tick(object sender, EventArgs e)
        {
            SaveAccounts();
            SaveAccountsTimer.Stop();
        }

        private void AccountsView_ModelCanDrop(object sender, ModelDropEventArgs e)
        {
            if (e.SourceModels[0] != null && e.SourceModels[0] is Account) e.Effect = DragDropEffects.Move;
        }

        private void AccountsView_ModelDropped(object sender, ModelDropEventArgs e)
        {
            if (e.TargetModel == null || e.SourceModels.Count == 0) return;

            Account droppedOn = e.TargetModel as Account;

            int Index = e.DropTargetIndex;

            for (int i = e.SourceModels.Count; i > 0; i--)
            {
                Account dragged = e.SourceModels[i - 1] as Account; if (dragged == null) continue;

                dragged.Group = droppedOn.Group;

                AccountsList.Remove(dragged);
                AccountsList.Insert(Index, dragged);
            }

            RefreshView();
            AccountsView.EnsureModelVisible(e.SourceModels[e.SourceModels.Count - 1]);
            DelayedSaveAccounts();
        }

        private void sortAlphabeticallyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show($"Are you sure you want to sort every account alphabetically?", "Roblox Account Manager", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                AccountsList = AccountsList.OrderByDescending(x => x.Username.All(char.IsDigit)).ThenByDescending(x => x.Username.Any(char.IsLetter)).ThenBy(x => x.Username).ToList();

                AccountsView.SetObjects(AccountsList);
            }
        }

        private void toggleToolStripMenuItem_Click(object sender, EventArgs e) =>
            AccountsView.ShowGroups = !AccountsView.ShowGroups;

        private void EditTheme_Click(object sender, EventArgs e)
        {
            if (ThemeForm != null && ThemeForm.Visible)
            {
                ThemeForm.Hide();
                return;
            }

            ThemeForm.Show();
        }

        private void LaunchNexus_Click(object sender, EventArgs e)
        {
            if (ControlForm != null)
            {
                ControlForm.Top = Bottom;
                ControlForm.Left = Left;
                ControlForm.Show();
                ControlForm.BringToFront();
            }
            else
            {
                ControlForm = new AccountControl();
                ControlForm.StartPosition = FormStartPosition.Manual;
                ControlForm.Top = Bottom;
                ControlForm.Left = Left;
                ControlForm.Show();
                ControlForm.ApplyTheme();
            }
        }

        private async void LaunchAccounts(List<Account> Accounts, long PlaceId, string JobID, bool FollowUser = false, bool VIPServer = false)
        {
            int Delay = General.Exists("AccountJoinDelay") ? General.Get<int>("AccountJoinDelay") : 8;

            bool AsyncJoin = General.Get<bool>("AsyncJoin");
            CancellationTokenSource Token = LauncherToken;

            foreach (Account account in Accounts)
            {
                if (Token.IsCancellationRequested) break;

                account.JoinServer(PlaceId, JobID, FollowUser, VIPServer);

                if (AsyncJoin)
                {
                    while (!LaunchNext)
                        await Task.Delay(50);
                }
                else
                    await Task.Delay(Delay * 1000);

                LaunchNext = false;
            }

            LaunchNext = false;

            Token.Cancel();
            Token.Dispose();
        }

        public void NextAccount() => LaunchNext = true;
        public void CancelLaunching()
        {
            if (LauncherToken != null && !LauncherToken.IsCancellationRequested)
                LauncherToken.Cancel();
        }

        private void AccountManager_HelpButtonClicked(object sender, System.ComponentModel.CancelEventArgs e) =>
        MessageBox.Show("Some elements may have tooltips, hover over them for about 2 seconds to see instructions.", "Roblox Account Manager", MessageBoxButtons.OK, MessageBoxIcon.Information);

        private void infoToolStripMenuItem1_Click(object sender, EventArgs e) =>
            MessageBox.Show("Roblox Account Manager created by ic3w0lf under the GNU GPLv3 license.", "Roblox Account Manager", MessageBoxButtons.OK, MessageBoxIcon.Information);

        private void groupsToolStripMenuItem_Click(object sender, EventArgs e) =>
            MessageBox.Show("Groups can be sorted by naming them a number then whatever you want.\nFor example: You can put Group Apple on top by naming it '001 Apple' or '1Apple'.\nThe numbers will be hidden from the name but will be correctly sorted depending on the number.\nAccounts can also be dragged into groups.", "Roblox Account Manager", MessageBoxButtons.OK, MessageBoxIcon.Information);

        private void DonateButton_Click(object sender, EventArgs e) =>
            Process.Start("https://ic3w0lf22.github.io/donate.html");

        private void ConfigButton_Click(object sender, EventArgs e)
        {
            if (SettingsForm == null)
                SettingsForm = new SettingsForm();

            if (SettingsForm.Visible)
            {
                SettingsForm.WindowState = FormWindowState.Normal;
                SettingsForm.BringToFront();
            }
            else
                SettingsForm.Show();

            SettingsForm.StartPosition = FormStartPosition.Manual;
            SettingsForm.Top = Top;
            SettingsForm.Left = Right;
        }
    }
}
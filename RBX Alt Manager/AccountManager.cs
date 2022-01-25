using BrightIdeasSoftware;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Principal;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using Newtonsoft.Json.Linq;
using RBX_Alt_Manager.Forms;
using System.Runtime.InteropServices;

#pragma warning disable CS0618 // stupid parameter warnings

namespace RBX_Alt_Manager
{
    public partial class AccountManager : Form
    {
        public static List<Account> AccountsList = new List<Account>();
        public static List<Account> SelectedAccounts = new List<Account>();
        public static Account SelectedAccount;
        public static RestClient MainClient;
        public static RestClient FriendsClient;
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
        private readonly static DateTime startTime = DateTime.Now;
        public static bool IsTeleport = false;
        public static bool UseOldJoin = false;
        public static string CurrentVersion;
        public OLVListItem SelectedAccountItem;
        private WebServer AltManagerWS;
        public static IniFile IniSettings;
        private string WSPassword = "";
        private static DateTime LastAccountSave = DateTime.Now;
        private static System.Timers.Timer SaveAccountsTimer;
        private StreamWriter ConsoleWriter;

        private static Mutex rbxMultiMutex;
        private readonly static object saveLock = new object();

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
            ThemeEditor.LoadTheme();

            SetDarkBar(Handle);

            InitializeComponent();

            if (ThemeEditor.UseDarkTopBar) Icon = Properties.Resources.team_KX4_icon_white; // this has to go after or icon wont actually change

            AccountsView.UnfocusedHighlightBackgroundColor = Color.FromArgb(0, 150, 215);
            AccountsView.UnfocusedHighlightForegroundColor = Color.FromArgb(240, 240, 240);

            SimpleDropSink sink = AccountsView.DropSink as SimpleDropSink;
            sink.CanDropBetween = true;
            sink.CanDropOnBackground = false;
            sink.CanDropOnItem = false;
            sink.CanDropOnSubItem = false;
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
            Console.WriteLine("LoadAccounts");
            Console.WriteLine(SaveFilePath);
            Console.WriteLine(AccountsList);
            Console.WriteLine(AccountsView);

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
            if ((DateTime.Now - startTime).TotalMilliseconds < 5000) return;

            if (SaveAccountsTimer.Enabled)
                SaveAccountsTimer.Stop();

            SaveAccountsTimer.Start();
        }

        public static long GetUserID(string Username)
        {
            RestRequest request = new RestRequest("users/get-by-username?username=" + Username, Method.GET);
            request.AddHeader("Accept", "application/json");
            IRestResponse response = APIClient.Execute(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                UsernameReponse userData = JsonConvert.DeserializeObject<UsernameReponse>(response.Content);

                return userData.Id;
            }

            return -1;
        }

        public void AddAccountToList(Account account) => RefreshView();

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

        public static void AddAccount(string SecurityToken, string UserData)
        {
            Account account = new Account();

            string res = account.Validate(SecurityToken, UserData);

            if (res == "Success")
            {
                Account exists = AccountsList.FirstOrDefault(acc => acc.UserID == account.UserID);

                if (exists != null)
                    exists.SecurityToken = account.SecurityToken;
                else
                {
                    AccountsList.Add(account);

                    Program.MainForm.AddAccountToList(account);
                }

                SaveAccounts();
            }
            else MessageBox.Show(res);
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

            ApplyTheme();

            PlaceID_TextChanged(PlaceID, new EventArgs());

            IniSettings = new IniFile("RAMSettings.ini");

            if (!IniSettings.KeyExists("DisableAutoUpdate", "General")) IniSettings.Write("DisableAutoUpdate", "false", "General");
            if (!IniSettings.KeyExists("AccountJoinDelay", "General")) IniSettings.Write("AccountJoinDelay", "8", "General");

            if (!IniSettings.KeyExists("DevMode", "Developer")) IniSettings.Write("DevMode", "false", "Developer");
            if (!IniSettings.KeyExists("EnableWebServer", "Developer")) IniSettings.Write("EnableWebServer", "false", "Developer");
            if (!IniSettings.KeyExists("WebServerPort", "WebServer")) IniSettings.Write("WebServerPort", "7963", "WebServer");
            if (!IniSettings.KeyExists("AllowGetCookie", "WebServer")) IniSettings.Write("AllowGetCookie", "false", "WebServer");
            if (!IniSettings.KeyExists("AllowGetAccounts", "WebServer")) IniSettings.Write("AllowGetAccounts", "false", "WebServer");
            if (!IniSettings.KeyExists("AllowLaunchAccount", "WebServer")) IniSettings.Write("AllowLaunchAccount", "false", "WebServer");
            if (!IniSettings.KeyExists("AllowAccountEditing", "WebServer")) IniSettings.Write("AllowAccountEditing", "false", "WebServer");
            if (!IniSettings.KeyExists("Password", "WebServer")) IniSettings.Write("Password", "", "WebServer"); else WSPassword = IniSettings.Read("Password", "WebServer");
            if (!IniSettings.KeyExists("EveryRequestRequiresPassword", "WebServer")) IniSettings.Write("EveryRequestRequiresPassword", "false", "WebServer");

            PlaceID.Text = IniSettings.KeyExists("SavedPlaceId", "General") ? IniSettings.Read("SavedPlaceId", "General") : "5315046213";

            if (IniSettings.Read("DevMode", "Developer") != "true" && !File.Exists("dev.mode"))
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

            FileStream fs = new FileStream("console.txt", FileMode.Create);

            ConsoleWriter = new StreamWriter(fs);

            Console.SetOut(ConsoleWriter);

            if (File.Exists("AU.exe"))
            {
                if (File.Exists("Auto Update.exe"))
                    File.Delete("Auto Update.exe");

                File.Copy("AU.exe", "Auto Update.exe");
                File.Delete("AU.exe");
            }

            if (IniSettings.Read("DisableAutoUpdate", "General") != "true")
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
                                DialogResult result = MessageBox.Show("An update is available, click yes to run the auto updater or no to be redirected to the download page.", "Roblox Account Manager", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information);

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
                                else if (result == DialogResult.No)
                                    Process.Start("https://github.com/ic3w0lf22/Roblox-Account-Manager/releases");
                            }
                        }
                    }
                    catch { }
                });
            }

            try
            {
                if (IniSettings.Read("EnableWebServer", "Developer") == "true")
                {
                    string Port = IniSettings.KeyExists("WebServerPort", "WebServer") ? IniSettings.Read("WebServerPort", "WebServer") : "7963";

                    AltManagerWS = new WebServer(SendResponse, $"http://localhost:{Port}/");
                    AltManagerWS.Run();
                }
            }
            catch (Exception x) { MessageBox.Show("Failed to start webserver! " + x, "Roblox Account Manager", MessageBoxButtons.OK, MessageBoxIcon.Error); }

            try
            {
                WebClient WC = new WebClient();
                string VersionJSON = WC.DownloadString("https://clientsettings.roblox.com/v1/client-version/WindowsPlayer");
                JObject j = JObject.Parse(VersionJSON);

                if (j.TryGetValue("clientVersionUpload", out JToken token))
                    CurrentVersion = token.Value<string>();
            }
            catch { }
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
        }

        private List<ServerData> AttemptedJoins = new List<ServerData>();

        private string SendResponse(HttpListenerRequest request)
        {
            if (!request.IsLocal || request.Url.AbsolutePath == "/favicon.ico") return "";

            if (request.Url.AbsolutePath == "/Running") return "true";

            string Method = request.Url.AbsolutePath.Substring(1);
            string Account = request.QueryString["Account"];
            string Password = request.QueryString["Password"];

            if (IniSettings.Read("EveryRequestRequiresPassword", "WebServer") == "true" && (WSPassword.Length < 6 || Password != WSPassword)) return "Invalid Password";

            if ((Method == "GetCookie" || Method == "GetAccounts" || Method == "LaunchAccount") && (WSPassword.Length < 6 || Password != WSPassword)) return "Invalid Password";

            if (Method == "GetAccounts")
            {
                if (IniSettings.Read("AllowGetAccounts", "WebServer") != "true") return "Method not allowed";

                string Names = "";

                foreach (Account acc in AccountsList)
                    Names += acc.Username + ",";

                return Names.Remove(Names.Length - 1);
            }

            if (string.IsNullOrEmpty(Account)) return "Empty Account";

            Account account = AccountsList.FirstOrDefault(x => x.Username == Account || x.UserID.ToString() == Account);

            if (account == null || string.IsNullOrEmpty(account.GetCSRFToken())) return "Invalid Account";

            if (Method == "GetCookie")
            {
                if (IniSettings.Read("AllowGetCookie", "WebServer") != "true") return "Method not allowed";

                return account.SecurityToken;
            }
            if (Method == "LaunchAccount")
            {
                if (IniSettings.Read("AllowLaunchAccount", "WebServer") != "true") return "Method not allowed";

                bool ValidPlaceId = long.TryParse(request.QueryString["PlaceId"], out long PlaceId); if (!ValidPlaceId) return "Invalid PlaceId";

                string JobID = !string.IsNullOrEmpty(request.QueryString["JobId"]) ? request.QueryString["JobId"] : "";
                string FollowUser = request.QueryString["FollowUser"];
                string JoinVIP = request.QueryString["JoinVIP"];

                account.JoinServer(PlaceId, JobID, FollowUser == "true", JoinVIP == "true");

                return $"Launched {Account} to {PlaceId}";
            }

            if (Method == "GetCSRFToken") return account.GetCSRFToken();
            if (Method == "GetAlias") return account.Alias;
            if (Method == "GetDescription") return account.Description;

            if (Method == "BlockUser" && !string.IsNullOrEmpty(request.QueryString["UserId"])) return account.BlockUserId(request.QueryString["UserId"]);
            if (Method == "UnblockUser" && !string.IsNullOrEmpty(request.QueryString["UserId"])) return account.UnblockUserId(request.QueryString["UserId"]);
            if (Method == "UnblockEveryone") return account.UnblockEveryone();
            if (Method == "GetBlockedList") return account.GetBlockedList();

            if (Method == "SetServer" && !string.IsNullOrEmpty(request.QueryString["PlaceId"]) && !string.IsNullOrEmpty(request.QueryString["JobId"])) return account.SetServer(Convert.ToInt64(request.QueryString["PlaceId"]), request.QueryString["JobId"]);
            if (Method == "SetRecommendedServer")
            {
                int attempts = 0;
                string res = "-1";

                for (int i = RBX_Alt_Manager.ServerList.servers.Count - 1; i > 0; i--)
                {
                    if (attempts > 10) return "Too many failed attempts";

                    ServerData server = RBX_Alt_Manager.ServerList.servers[i];

                    if (AttemptedJoins.FirstOrDefault(x => x.id == server.id) != null) continue;

                    AttemptedJoins.Add(server);

                    attempts++;

                    res = account.SetServer(RBX_Alt_Manager.ServerList.CurrentPlaceID, server.id);

                    if (res == "Success")
                        return res;
                }

                return string.IsNullOrEmpty(res) ? "Failed" : res;
            }

            if (Method == "GetField" && !string.IsNullOrEmpty(request.QueryString["Field"])) return account.GetField(request.QueryString["Field"]);
            if (Method == "SetField" && !string.IsNullOrEmpty(request.QueryString["Field"]) && !string.IsNullOrEmpty(request.QueryString["Value"]))
            {
                if (IniSettings.Read("AllowAccountEditing", "WebServer") != "true") return "Method not allowed";

                account.SetField(request.QueryString["Field"], request.QueryString["Value"]);

                return $"Set Field {request.QueryString["Field"]} to {request.QueryString["Value"]}";
            }
            if (Method == "RemoveField" && !string.IsNullOrEmpty(request.QueryString["Field"]))
            {
                if (IniSettings.Read("AllowAccountEditing", "WebServer") != "true") return "Method not allowed";

                account.RemoveField(request.QueryString["Field"]);

                return $"Removed Field {request.QueryString["Field"]}";
            }

            string Body = new StreamReader(request.InputStream).ReadToEnd();

            if (Method == "SetAlias" && !string.IsNullOrEmpty(Body))
            {
                if (IniSettings.Read("AllowAccountEditing", "WebServer") != "true") return "Method not allowed";

                account.Alias = Body;
                UpdateAccountView(account);

                return $"Set Alias of {account.Username} to {Body}";
            }
            if (Method == "SetDescription" && !string.IsNullOrEmpty(Body))
            {
                if (IniSettings.Read("AllowAccountEditing", "WebServer") != "true") return "Method not allowed";

                account.Description = Body;
                UpdateAccountView(account);

                return $"Set Description of {account.Username} to {Body}";
            }
            if (Method == "AppendDescription" && !string.IsNullOrEmpty(Body))
            {
                if (IniSettings.Read("AllowAccountEditing", "WebServer") != "true") return "Method not allowed";

                account.Description += Body;
                UpdateAccountView(account);

                return $"Appended Description of {account.Username} with {Body}";
            }

            return $"";
        }

        private void AccountManager_Shown(object sender, EventArgs e)
        {
            LoadAccounts();

            if (IniSettings.Read("DisableMultiRbx", "General") != "true")
            {
                try
                {
                    rbxMultiMutex = new Mutex(true, "ROBLOX_singletonMutex");

                    if (!rbxMultiMutex.WaitOne(TimeSpan.Zero, true) && IniSettings.Read("HideRbxAlert", "General") != "true")
                    {
                        DialogResult MsgResult = MessageBox.Show("WARNING: Roblox is currently running, multi roblox will not work until you restart the account manager with roblox closed.\nTo hide this warning permanently, press Cancel.", "Roblox Account Manager", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);

                        if (MsgResult == DialogResult.Cancel)
                            IniSettings.Write("HideRbxAlert", "true", "General");
                    }
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
            Console.WriteLine("Add_Click");
            Console.WriteLine(aaform);

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
            bool VIPServer = JobID.TextLength > 4 ? JobID.Text.Substring(0, 4) == "VIP:" : false;

            if (AccountsView.SelectedObjects.Count > 1)
            {
                Task.Run(async () =>
                {
                    int Delay = 8;
                    int.TryParse(IniSettings.Read("AccountJoinDelay", "General"), out Delay);

                    foreach (Account account in SelectedAccounts)
                    {
                        account.JoinServer(Convert.ToInt64(PlaceID.Text), VIPServer ? JobID.Text.Substring(4) : JobID.Text, false, VIPServer);

                        await Task.Delay(Delay * 1000);
                    }
                });
            }
            else if (SelectedAccount != null)
            {
                string res = SelectedAccount.JoinServer(Convert.ToInt64(PlaceID.Text), VIPServer ? JobID.Text.Substring(4) : JobID.Text, false, VIPServer);

                if (!res.Contains("Success"))
                    MessageBox.Show(res);
            }
        }

        private void Follow_Click(object sender, EventArgs e)
        {
            if (SelectedAccount == null) return;

            long UserId = GetUserID(UserID.Text);

            if (UserId < 0)
            {
                MessageBox.Show("Failed to get UserId");
                return;
            }

            if (AccountsView.SelectedObjects.Count > 1)
            {
                Task.Run(async () =>
                {
                    int Delay = 8;
                    int.TryParse(IniSettings.Read("AccountJoinDelay", "General"), out Delay);

                    foreach (Account account in SelectedAccounts)
                    {
                        account.JoinServer(UserId, "", true);

                        await Task.Delay(Delay * 1000);
                    }
                });
            }
            else if (SelectedAccount != null)
            {
                string res = SelectedAccount.JoinServer(UserId, "", true);

                if (!res.Contains("Success"))
                    MessageBox.Show(res);
            }
        }

        private void PlaceID_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
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
            ConsoleWriter.Close();

            if (AltManagerWS != null)
                AltManagerWS.Stop();

            if (PlaceID == null || string.IsNullOrEmpty(PlaceID.Text)) return;

            IniSettings.Write("SavedPlaceId", PlaceID.Text, "General");
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

            RestRequest request = new RestRequest("v1/authentication-ticket/", Method.POST);

            request.AddCookie(".ROBLOSECURITY", SelectedAccount.SecurityToken);
            request.AddHeader("Referer", "https://www.roblox.com/games/606849621/Jailbreak");

            IRestResponse response = AuthClient.Execute(request);
            Parameter result = response.Headers.FirstOrDefault(x => x.Name == "x-csrf-token");

            string Token = "";

            if (result != null)
                Token = (string)result.Value;
            else
                return;

            if (string.IsNullOrEmpty(Token) || result == null)
                return;

            request = new RestRequest("/v1/authentication-ticket/", Method.POST);
            request.AddCookie(".ROBLOSECURITY", SelectedAccount.SecurityToken);
            request.AddHeader("X-CSRF-TOKEN", Token);
            request.AddHeader("Referer", "https://www.roblox.com/games/606849621/Jailbreak");
            response = AuthClient.Execute(request);

            Parameter Ticket = response.Headers.FirstOrDefault(x => x.Name == "rbx-authentication-ticket");

            if (Ticket != null)
                Clipboard.SetText((string)Ticket.Value);
        }

        private void copyRbxplayerLinkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedAccount == null) return;

            RestRequest request = new RestRequest("v1/authentication-ticket/", Method.POST);

            request.AddCookie(".ROBLOSECURITY", SelectedAccount.SecurityToken);
            request.AddHeader("Referer", "https://www.roblox.com/games/606849621/Jailbreak");

            IRestResponse response = AuthClient.Execute(request);
            Parameter result = response.Headers.FirstOrDefault(x => x.Name == "x-csrf-token");

            string Token = "";

            if (result != null)
                Token = (string)result.Value;
            else
                return;

            if (string.IsNullOrEmpty(Token) || result == null)
                return;

            request = new RestRequest("/v1/authentication-ticket/", Method.POST);
            request.AddCookie(".ROBLOSECURITY", SelectedAccount.SecurityToken);
            request.AddHeader("X-CSRF-TOKEN", Token);
            request.AddHeader("Referer", "https://www.roblox.com/games/606849621/Jailbreak");
            response = AuthClient.Execute(request);

            Parameter Ticket = response.Headers.FirstOrDefault(x => x.Name == "rbx-authentication-ticket");

            if (Ticket != null)
            {
                Token = (string)Ticket.Value;
                bool HasJobId = string.IsNullOrEmpty(JobID.Text);
                double LaunchTime = Math.Floor((DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds * 1000);

                Random r = new Random();
                Clipboard.SetText(string.Format("<roblox-player://1/1+launchmode:play+gameinfo:{0}+launchtime:{4}+browsertrackerid:{5}+placelauncherurl:https://assetgame.roblox.com/game/PlaceLauncher.ashx?request=RequestGame{3}&placeId={1}{2}+robloxLocale:en_us+gameLocale:en_us>", Token, PlaceID.Text, HasJobId ? "" : ("&gameId=" + JobID.Text), HasJobId ? "" : "Job", LaunchTime, r.Next(100000, 130000).ToString() + r.Next(100000, 900000).ToString()));
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

            RestRequest request = new RestRequest("v1/authentication-ticket/", Method.POST);

            request.AddCookie(".ROBLOSECURITY", SelectedAccount.SecurityToken);
            request.AddHeader("Referer", "https://www.roblox.com/games/606849621/Jailbreak");

            IRestResponse response = AuthClient.Execute(request);
            Parameter result = response.Headers.FirstOrDefault(x => x.Name == "x-csrf-token");

            string Token = "";

            if (result != null)
                Token = (string)result.Value;
            else
                return;

            if (string.IsNullOrEmpty(Token) || result == null)
                return;

            request = new RestRequest("/v1/authentication-ticket/", Method.POST);
            request.AddCookie(".ROBLOSECURITY", SelectedAccount.SecurityToken);
            request.AddHeader("X-CSRF-TOKEN", Token);
            request.AddHeader("Referer", "https://www.roblox.com/games/606849621/Jailbreak");
            response = AuthClient.Execute(request);

            Parameter Ticket = response.Headers.FirstOrDefault(x => x.Name == "rbx-authentication-ticket");

            if (Ticket != null)
            {
                Token = (string)Ticket.Value;
                bool HasJobId = string.IsNullOrEmpty(JobID.Text);
                double LaunchTime = Math.Floor((DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds * 1000);

                Random r = new Random();
                Clipboard.SetText(string.Format("<roblox-player://1/1+launchmode:app+gameinfo:{0}+launchtime:{1}+browsertrackerid:{2}+robloxLocale:en_us+gameLocale:en_us>", Token, LaunchTime, r.Next(500000, 600000).ToString() + r.Next(10000, 90000).ToString()));
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

        private void AccountManager_HelpButtonClicked(object sender, System.ComponentModel.CancelEventArgs e) =>
        MessageBox.Show("Some elements may have tooltips, hover over them for about 2 seconds to see instructions.", "Roblox Account Manager", MessageBoxButtons.OK, MessageBoxIcon.Information);


        private void infoToolStripMenuItem1_Click(object sender, EventArgs e) =>
            MessageBox.Show("Roblox Account Manager created by ic3w0lf under the GNU GPLv3 license.", "Roblox Account Manager", MessageBoxButtons.OK, MessageBoxIcon.Information);

        private void groupsToolStripMenuItem_Click(object sender, EventArgs e) =>
            MessageBox.Show("Groups can be sorted by naming them a number then whatever you want.\nFor example: You can put Group Apple on top by naming it '001 Apple' or '1Apple'.\nThe numbers will be hidden from the name but will be correctly sorted depending on the number.\nAccounts can also be dragged into groups.", "Roblox Account Manager", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }
}
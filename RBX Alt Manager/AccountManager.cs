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

#pragma warning disable CS0618 // parameter warnings

namespace RBX_Alt_Manager
{
    public partial class AccountManager : Form
    {
        public static AccountManager Instance;
        public static List<Account> AccountsList;
        public static List<Account> SelectedAccounts;
        public static List<Game> RecentGames;
        public static Account SelectedAccount;
        public static Account LastValidAccount; // this is used for the Batch class since getting place details requires authorization, auto updates whenever an account is used
        public static RestClient MainClient;
        public static RestClient AvatarClient;
        public static RestClient FriendsClient;
        public static RestClient UsersClient;
        public static RestClient AuthClient;
        public static RestClient EconClient;
        public static RestClient AccountClient;
        public static RestClient GameJoinClient;
        public static RestClient Web13Client;
        public static string CurrentPlaceId { get => Instance.PlaceID.Text; }
        public static string CurrentJobId { get => Instance.JobID.Text; }
        public static bool Elevated;
        private AccountAdder aaform;
        private ArgumentsForm afform;
        private ServerList ServerListForm;
        private AccountUtils UtilsForm;
        private ImportForm ImportAccountsForm;
        private AccountFields FieldsForm;
        private ThemeEditor ThemeForm;
        private AccountControl ControlForm;
        private SettingsForm SettingsForm;
        private RecentGamesForm RGForm;
        private readonly static DateTime startTime = DateTime.Now;
        public static bool IsTeleport = false;
        public static bool UseOldJoin = false;
        public static bool ShuffleJobID = false;
        public static string CurrentVersion;
        public OLVListItem SelectedAccountItem;
        private WebServer AltManagerWS;
        private string WSPassword = "";

        public static IniFile IniSettings;
        public static IniSection General;
        public static IniSection Developer;
        public static IniSection WebServer;
        public static IniSection AccountControl;
        public static IniSection Watcher;
        public static IniSection Prompts;

        private static Mutex rbxMultiMutex;
        private readonly static object saveLock = new object();
        private readonly static object rgSaveLock = new object();
        public event EventHandler<GameArgs> RecentGameAdded;

        private bool LaunchNext;
        private CancellationTokenSource LauncherToken;

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

            IniSettings = File.Exists(Path.Combine(Environment.CurrentDirectory, "RAMSettings.ini")) ? new IniFile("RAMSettings.ini") : new IniFile();

            General = IniSettings.Section("General");
            Developer = IniSettings.Section("Developer");
            WebServer = IniSettings.Section("WebServer");
            AccountControl = IniSettings.Section("AccountControl");
            Watcher = IniSettings.Section("Watcher");
            Prompts = IniSettings.Section("Prompts");

            if (!General.Exists("CheckForUpdates")) General.Set("CheckForUpdates", "true");
            if (!General.Exists("AccountJoinDelay")) General.Set("AccountJoinDelay", "8");
            if (!General.Exists("AsyncJoin")) General.Set("AsyncJoin", "false");
            if (!General.Exists("DisableAgingAlert")) General.Set("DisableAgingAlert", "false");
            if (!General.Exists("SavePasswords")) General.Set("SavePasswords", "true");
            if (!General.Exists("ServerRegionFormat")) General.Set("ServerRegionFormat", "<city>, <countryCode>", "Visit http://ip-api.com/json/1.1.1.1 to see available format options");
            if (!General.Exists("MaxRecentGames")) General.Set("MaxRecentGames", "8");
            if (!General.Exists("ShuffleChoosesLowestServer")) General.Set("ShuffleChoosesLowestServer", "false");
            if (!General.Exists("ShufflePageCount")) General.Set("ShufflePageCount", "5");
            if (!General.Exists("IPApiLink")) General.Set("IPApiLink", "http://ip-api.com/json/<ip>");
            if (!General.Exists("WindowScale"))
            {
                General.Set("WindowScale", Screen.PrimaryScreen.Bounds.Height <= Screen.PrimaryScreen.Bounds.Width /*scuffed*/ ? Math.Max(Math.Min(Screen.PrimaryScreen.Bounds.Height / 1080f, 2f), 1f).ToString(".0#") : "1.0");

                if (Program.Scale > 1)
                    if (!Utilities.YesNoPrompt("Roblox Account Manager", "RAM has detected you have a monitor larger than average", $"Would you like to keep the WindowScale setting of {Program.Scale:F2}?", false))
                        General.Set("WindowScale", "1.0");
                    else
                        MessageBox.Show("In case the font scaling is incorrect, open RAMSettings.ini and change \"ScaleFonts=true\" to \"ScaleFonts=false\" without the quotes.", "Roblox Account Manager", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            if (!General.Exists("ScaleFonts")) General.Set("ScaleFonts", "true");

            if (!Developer.Exists("DevMode")) Developer.Set("DevMode", "false");
            if (!Developer.Exists("EnableWebServer")) Developer.Set("EnableWebServer", "false");

            if (!WebServer.Exists("WebServerPort")) WebServer.Set("WebServerPort", "7963");
            if (!WebServer.Exists("AllowGetCookie")) WebServer.Set("AllowGetCookie", "false");
            if (!WebServer.Exists("AllowGetAccounts")) WebServer.Set("AllowGetAccounts", "false");
            if (!WebServer.Exists("AllowLaunchAccount")) WebServer.Set("AllowLaunchAccount", "false");
            if (!WebServer.Exists("AllowAccountEditing")) WebServer.Set("AllowAccountEditing", "false");
            if (!WebServer.Exists("Password")) WebServer.Set("Password", ""); else WSPassword = WebServer.Get("Password");
            if (!WebServer.Exists("EveryRequestRequiresPassword")) WebServer.Set("EveryRequestRequiresPassword", "false");
            if (!WebServer.Exists("AllowExternalConnections")) WebServer.Set("AllowExternalConnections", "false");

            if (!AccountControl.Exists("AllowExternalConnections")) AccountControl.Set("AllowExternalConnections", "false");
            if (!AccountControl.Exists("RelaunchDelay")) AccountControl.Set("RelaunchDelay", "60");
            if (!AccountControl.Exists("LauncherDelayNumber")) AccountControl.Set("LauncherDelayNumber", "9");
            if (!AccountControl.Exists("NexusPort")) AccountControl.Set("NexusPort", "5242");

            InitializeComponent();
            this.Rescale();

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
                Regex RSecRegex = new Regex(@"(_\|WARNING:-DO-NOT-SHARE-THIS\.--Sharing-this-will-allow-someone-to-log-in-as-you-and-to-steal-your-ROBUX-and-items\.\|\w+)");
                MatchCollection RSecMatches = RSecRegex.Matches(Text);

                foreach (Match match in RSecMatches)
                    AddAccount(match.Value);
            }
        }

        private readonly static string SaveFilePath = Path.Combine(Environment.CurrentDirectory, "AccountData.json");
        private readonly static string RecentGamesFilePath = Path.Combine(Environment.CurrentDirectory, "RecentGames.json"); // i shouldve combined everything that isnt accountdata into one file but oh well im too lazy : |

        private void RefreshView(object obj = null)
        {
            AccountsView.InvokeIfRequired(() =>
            {
                AccountsView.BuildList();
                if (AccountsView.ShowGroups) AccountsView.BuildGroups();

                if (obj != null)
                {
                    AccountsView.RefreshObject(obj);
                    AccountsView.EnsureModelVisible(obj);
                }
            });
        }

        private void LoadAccounts()
        {
            if (File.Exists(SaveFilePath))
            {
                try { AccountsList = JsonConvert.DeserializeObject<List<Account>>(Encoding.UTF8.GetString(ProtectedData.Unprotect(File.ReadAllBytes(SaveFilePath), Entropy, DataProtectionScope.CurrentUser))); }
                catch (CryptographicException e)
                {
                    try { AccountsList = JsonConvert.DeserializeObject<List<Account>>(File.ReadAllText(SaveFilePath)); }
                    catch
                    {
                        File.WriteAllText(SaveFilePath + ".bak", File.ReadAllText(SaveFilePath));

                        MessageBox.Show($"Failed to load accounts!\nA backup file was created.\n\n{e.Message}");
                    }
                }
            }

            AccountsList ??= new List<Account>();

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
                        try { AccountsList = JsonConvert.DeserializeObject<List<Account>>(File.ReadAllText(SaveFilePath + ".backup")); }
                        catch { MessageBox.Show("Failed to load backup file!", "Roblox Account Manager", MessageBoxButtons.OKCancel, MessageBoxIcon.Error); }
                    }
                }
            }

            AccountsView.SetObjects(AccountsList);
            RefreshView();

            if (AccountsList.Count > 0)
            {
                LastValidAccount = AccountsList[0];

                foreach (Account account in AccountsList)
                    if (account.LastUse > LastValidAccount.LastUse)
                        LastValidAccount = account;
            }
        }

        public static void SaveAccounts()
        {
            if ((DateTime.Now - startTime).Seconds < 5 || AccountsList.Count == 0) return;

            lock (saveLock)
            {
                string OldInfo = File.Exists(SaveFilePath) ? File.ReadAllText(SaveFilePath) : "";
                string SaveData = JsonConvert.SerializeObject(AccountsList);
                int OldSize = Encoding.Unicode.GetByteCount(OldInfo);
                int NewSize = Encoding.Unicode.GetByteCount(SaveData);

                FileInfo OldFile = new FileInfo(SaveFilePath);

                if (OldFile.Exists && NewSize < OldSize || (OldFile.Exists && (DateTime.Now - OldFile.LastWriteTime).TotalHours > 36))
                    File.WriteAllText(SaveFilePath + ".backup", OldInfo);

                if (File.Exists(Path.Combine(Environment.CurrentDirectory, "NoEncryption.IUnderstandTheRisks.iautamor")))
                    File.WriteAllBytes(SaveFilePath, Encoding.UTF8.GetBytes(SaveData));
                else
                    File.WriteAllBytes(SaveFilePath, ProtectedData.Protect(Encoding.UTF8.GetBytes(SaveData), Entropy, DataProtectionScope.LocalMachine));
            }
        }

        public static bool GetUserID(string Username, out long UserId)
        {
            RestRequest request = new RestRequest("v1/usernames/users", Method.POST);
            request.AddJsonBody(new { usernames = new string[] { Username } });

            IRestResponse response = UsersClient.Execute(request);

            if (response.StatusCode == HttpStatusCode.OK && response.Content.TryParseJson(out JObject UserData) && UserData.ContainsKey("data") && UserData["data"].Count() >= 1)
            {
                UserId = UserData["data"]?[0]?["id"].Value<long>() ?? -1;

                return true;
            }

            UserId = -1;

            return false;
        }

        public void UpdateAccountView(Account account) =>
            AccountsView.InvokeIfRequired(() => AccountsView.UpdateObject(account));

        public static Account AddAccount(string SecurityToken, string Password = "")
        {
            Account account = new Account(SecurityToken);

            if (account.Valid)
            {
                account.Password = Password;

                Account exists = AccountsList.FirstOrDefault(acc => acc.UserID == account.UserID);

                if (exists != null)
                {
                    account = exists;

                    exists.SecurityToken = SecurityToken;
                    exists.Password = Password;
                    exists.LastUse = DateTime.Now;

                    Instance.RefreshView(exists);
                }
                else
                {
                    AccountsList.Add(account);

                    Instance.RefreshView(account);
                }

                SaveAccounts();

                return account;
            }

            return null;
        }

        public static string ShowDialog(string text, string caption, string defaultText = "") // tbh pasted from stackoverflow
        {
            Form prompt = new Form()
            {
                Width = 340,
                Height = 125,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                Text = caption,
                StartPosition = FormStartPosition.CenterScreen
            };

            Label textLabel = new Label() { Left = 15, Top = 10, Text = text };
            TextBox textBox = new TextBox() { Left = 15, Top = 25, Width = 220, Text = defaultText };
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
                if (Directory.GetParent(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)).FullName.Contains(Path.GetTempPath().Remove(Path.GetTempPath().Length - 1)))
                {
                    MessageBox.Show("Roblox Account Manager must be extracted in order to function correctly!", "Roblox Account Manager", MessageBoxButtons.OK);
                    Environment.Exit(Stupid);
                }
            }
            catch { }

            string AFN = Path.Combine(Directory.GetCurrentDirectory(), "Auto Update.exe");
            string AU2FN = Path.Combine(Directory.GetCurrentDirectory(), "AU.exe");

            if (File.Exists(AU2FN))
            {
                if (File.Exists(AFN))
                    File.Delete(AFN);

                File.Copy(AU2FN, AFN);
                File.Delete(AU2FN);
            }

            DirectoryInfo UpdateDir = new DirectoryInfo(Path.Combine(Environment.CurrentDirectory, "Update"));

            if (UpdateDir.Exists)
                UpdateDir.RecursiveDelete();

            aaform = new AccountAdder();
            afform = new ArgumentsForm();
            ServerListForm = new ServerList();
            UtilsForm = new AccountUtils();
            ImportAccountsForm = new ImportForm();
            FieldsForm = new AccountFields();
            ThemeForm = new ThemeEditor();
            RGForm = new RecentGamesForm();

            MainClient = new RestClient("https://www.roblox.com/");
            AvatarClient = new RestClient("https://avatar.roblox.com/");
            AuthClient = new RestClient("https://auth.roblox.com/");
            EconClient = new RestClient("https://economy.roblox.com/");
            AccountClient = new RestClient("https://accountsettings.roblox.com/");
            GameJoinClient = new RestClient("https://gamejoin.roblox.com/");
            UsersClient = new RestClient("https://users.roblox.com");
            FriendsClient = new RestClient("https://friends.roblox.com");
            Web13Client = new RestClient("https://web.roblox.com/");

            foreach (var Client in new RestClient[] { MainClient, AvatarClient, AuthClient, EconClient, AccountClient, GameJoinClient, UsersClient, FriendsClient, Web13Client })
                Client.CachePolicy = new System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.BypassCache);

            ApplyTheme();

            RGForm.RecentGameSelected += (sender, e) => { PlaceID.Text = e.Game.Details?.placeId.ToString(); };

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
                OpenApp.Size = ImportByCookie.Size;
                ArgumentsB.Visible = true;
            }

            if (General.Get<bool>("HideUsernames"))
                HideUsernamesCheckbox.Checked = true;

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
                                bool ShouldUpdate = Utilities.YesNoPrompt("Roblox Account Manager", "An update is available", "Would you like to update now?");

                                if (ShouldUpdate)
                                {
                                    if (File.Exists(AFN))
                                    {
                                        Process.Start(AFN, "skip");
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
                if (new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator))
                    Elevated = true;

                // MessageBox.Show("Some features may not work properly if you ran the account manager as admin!", "Roblox Account Manager", MessageBoxButtons.OK, MessageBoxIcon.Error); // I don't think this is an issue anymore
            }
            catch { }

            try
            {
                if (Developer.Get<bool>("EnableWebServer"))
                {
                    string Port = WebServer.Exists("WebServerPort") ? WebServer.Get("WebServerPort") : "7963";

                    List<string> Prefixes = new List<string>() { $"http://localhost:{Port}/" };

                    if (WebServer.Get<bool>("AllowExternalConnections"))
                        if (Elevated)
                            Prefixes.Add($"http://*:{Port}/");
                        else
                            using (Process proc = new Process() { StartInfo = new ProcessStartInfo(AppDomain.CurrentDomain.FriendlyName, "-adminRequested") { Verb = "runas" } })
                                try
                                {
                                    proc.Start();
                                    Environment.Exit(1);
                                }
                                catch { }


                    AltManagerWS = new WebServer(SendResponse, Prefixes.ToArray());
                    AltManagerWS.Run();
                }
            }
            catch (Exception x) { MessageBox.Show($"Failed to start webserver!\n\n{x}", "Roblox Account Manager", MessageBoxButtons.OK, MessageBoxIcon.Error); }

            Task.Run(() =>
            {
                WebClient WC = new WebClient();
                string VersionJSON = WC.DownloadString("https://clientsettings.roblox.com/v1/client-version/WindowsPlayer");

                if (JObject.Parse(VersionJSON).TryGetValue("clientVersionUpload", out JToken token))
                    CurrentVersion = token.Value<string>();
            });

            IniSettings.Save("RAMSettings.ini");

            PlaceID.AutoCompleteCustomSource = new AutoCompleteStringCollection();
            PlaceID.AutoCompleteMode = AutoCompleteMode.Suggest;
            PlaceID.AutoCompleteSource = AutoCompleteSource.CustomSource;

            Task.Run(LoadRecentGames);

            if (General.Get<bool>("ShuffleJobId"))
                ShuffleIcon_Click(null, EventArgs.Empty);
        }

        public void ApplyTheme()
        {
            BackColor = ThemeEditor.FormsBackground;
            ForeColor = ThemeEditor.FormsForeground;

            if (AccountsView.BackColor != ThemeEditor.AccountBackground || AccountsView.ForeColor != ThemeEditor.AccountForeground)
            {
                AccountsView.BackColor = ThemeEditor.AccountBackground;
                AccountsView.ForeColor = ThemeEditor.AccountForeground;

                RefreshView();
            }

            AccountsView.HeaderStyle = ThemeEditor.ShowHeaders ? (AccountsView.ShowGroups ? ColumnHeaderStyle.Nonclickable : ColumnHeaderStyle.Clickable) : ColumnHeaderStyle.None;
            AccountsView.CellEditActivation = ObjectListView.CellEditActivateMode.DoubleClick;

            foreach (Control control in Controls)
            {
                if (control is PictureBox)
                {
                    control.BackColor = Color.Transparent;
                    if (ThemeEditor.LightImages && control.GetLuminance(out float L) && L < 0.3) control.ColorImage(255, 255, 255);
                }
                else if (control is Button || control is CheckBox)
                {
                    if (control is Button)
                    {
                        if (ThemeEditor.LightImages && control.GetLuminance(out float L) && L < 0.3)
                            control.ColorImage(255, 255, 255);

                        Button b = control as Button;
                        b.FlatStyle = ThemeEditor.ButtonStyle;
                        b.FlatAppearance.BorderColor = ThemeEditor.ButtonsBorder;
                    }

                    if (!(control is CheckBox)) control.BackColor = ThemeEditor.ButtonsBackground;
                    control.ForeColor = ThemeEditor.ButtonsForeground;
                }
                else if (control is TextBox || control is RichTextBox)
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
                else if (control is Label)
                {
                    control.BackColor = ThemeEditor.LabelTransparent ? Color.Transparent : ThemeEditor.LabelBackground;
                    control.ForeColor = ThemeEditor.LabelForeground;
                }
            }

            aaform.ApplyTheme();
            afform.ApplyTheme();
            ServerListForm.ApplyTheme();
            UtilsForm.ApplyTheme();
            ImportAccountsForm.ApplyTheme();
            FieldsForm.ApplyTheme();
            ThemeForm.ApplyTheme();
            RGForm.ApplyTheme();

            if (ControlForm != null) ControlForm.ApplyTheme();
            if (SettingsForm != null) SettingsForm.ApplyTheme();
        }

        private async void LoadRecentGames()
        {
            RecentGames = new List<Game>();

            if (File.Exists(RecentGamesFilePath))
            {
                List<Game> Games = JsonConvert.DeserializeObject<List<Game>>(File.ReadAllText(RecentGamesFilePath));

                RGForm.LoadGames(Games);

                foreach (Game RG in Games)
                    await AddRecentGame(RG, true);
            }
        }

        private async Task AddRecentGame(Game RG, bool Loading = false)
        {
            await RG.WaitForDetails();

            RecentGames.RemoveAll(g => g?.Details?.placeId == RG.Details?.placeId);

            while (RecentGames.Count > General.Get<int>("MaxRecentGames"))
            {
                this.InvokeIfRequired(() => PlaceID.AutoCompleteCustomSource.Remove(RecentGames[0].Details?.filteredName));
                RecentGames.RemoveAt(0);
            }

            RecentGames.Add(RG);

            this.InvokeIfRequired(() => PlaceID.AutoCompleteCustomSource.Add(RG.Details.filteredName));

            if (!Loading)
            {
                this.InvokeIfRequired(() => RecentGameAdded?.Invoke(this, new GameArgs(RG)));

                lock (rgSaveLock)
                    File.WriteAllText(RecentGamesFilePath, JsonConvert.SerializeObject(RecentGames));
            }
        }

        private readonly List<ServerData> AttemptedJoins = new List<ServerData>();

        private string WebServerResponse(object Message, bool Success) => JsonConvert.SerializeObject(new { Success, Message });

        private string SendResponse(HttpListenerContext Context)
        {
            HttpListenerRequest request = Context.Request;

            bool V2 = request.Url.AbsolutePath.StartsWith("/v2/");
            string AbsolutePath = V2 ? request.Url.AbsolutePath.Substring(3) : request.Url.AbsolutePath;

            if (!request.IsLocal && !WebServer.Get<bool>("AllowExternalConnections")) return V2 ? WebServerResponse("External connections are not allowed", false) : "";
            if (AbsolutePath == "/favicon.ico") return ""; // always return nothing

            if (AbsolutePath == "/Running") return V2 ? WebServerResponse("Roblox Account Manager is running", true) : "true";

            string Body = new StreamReader(request.InputStream).ReadToEnd();
            string Method = AbsolutePath.Substring(1);
            string Account = request.QueryString["Account"];
            string Password = request.QueryString["Password"];

            Context.Response.StatusCode = 401;

            if (WebServer.Get<bool>("EveryRequestRequiresPassword") && (WSPassword.Length < 6 || Password != WSPassword)) return V2 ? WebServerResponse("Invalid Password, make sure your password contains 6 or more characters", false) : "Invalid Password";

            if ((Method == "GetCookie" || Method == "GetAccounts" || Method == "LaunchAccount") && (WSPassword.Length < 6 || Password != WSPassword)) return V2 ? WebServerResponse("Invalid Password, make sure your password contains 6 or more characters", false) : "Invalid Password";

            Context.Response.StatusCode = 200;

            if (Method == "GetAccounts")
            {
                if (!WebServer.Get<bool>("AllowGetAccounts")) return V2 ? WebServerResponse("Method `GetAccounts` not allowed", false) : "Method not allowed";

                string Names = "";
                string GroupFilter = request.QueryString["Group"];

                foreach (Account acc in AccountsList)
                {
                    if (!string.IsNullOrEmpty(GroupFilter) && acc.Group != GroupFilter) continue;

                    Names += acc.Username + ",";
                }

                return V2 ? WebServerResponse(Names.Remove(Names.Length - 1), true) : Names.Remove(Names.Length - 1);
            }

            if (Method == "GetAccountsJson")
            {
                if (!WebServer.Get<bool>("AllowGetAccounts")) return V2 ? WebServerResponse("Method `GetAccountsJson` not allowed", false) : "Method not allowed";

                string GroupFilter = request.QueryString["Group"];
                bool ShowCookies = request.QueryString["IncludeCookies"] == "true" && WebServer.Get<bool>("AllowGetCookie");

                List<object> Objects = new List<object>();

                foreach (Account acc in AccountsList)
                {
                    if (!string.IsNullOrEmpty(GroupFilter) && acc.Group != GroupFilter) continue;

                    object AccountObject = new
                    {
                        acc.Username,
                        acc.UserID,
                        acc.Alias,
                        acc.Description,
                        acc.Group,
                        acc.CSRFToken,
                        LastUsed = acc.LastUse.ToRobloxTick(),
                        Cookie = ShowCookies ? acc.SecurityToken : null,
                        acc.Fields,
                    };

                    Objects.Add(AccountObject);
                }

                return V2 ? WebServerResponse(JsonConvert.SerializeObject(Objects), true) : JsonConvert.SerializeObject(Objects);
            }

            Context.Response.StatusCode = 400;

            if (Method == "ImportCookie")
            {
                Account New = AddAccount(request.QueryString["Cookie"]);

                bool Success = New != null;

                if (Success)
                    Context.Response.StatusCode = 200;

                return V2 ? WebServerResponse(Success ? "Cookie successfully imported" : "An error occured importing the cookie", Success) : (Success ? "true" : "false");
            }

            if (string.IsNullOrEmpty(Account)) return V2 ? WebServerResponse("Empty Account", false) : "Empty Account";

            Account account = AccountsList.FirstOrDefault(x => x.Username == Account || x.UserID.ToString() == Account);

            if (account == null || !account.GetCSRFToken(out string Token)) return V2 ? WebServerResponse("Invalid Account, the account's cookie may have expired and resulted in the account being logged out", false) : "Invalid Account";

            Context.Response.StatusCode = 401;

            if (Method == "GetCookie")
            {
                if (!WebServer.Get<bool>("AllowGetCookie")) return V2 ? WebServerResponse("Method `GetCookie` not allowed", false) : "Method not allowed";

                Context.Response.StatusCode = 200;

                return V2 ? WebServerResponse(account.SecurityToken, true) : account.SecurityToken;
            }

            if (Method == "LaunchAccount")
            {
                if (!WebServer.Get<bool>("AllowLaunchAccount")) return V2 ? WebServerResponse("Method `LaunchAccount` not allowed", false) : "Method not allowed";

                bool ValidPlaceId = long.TryParse(request.QueryString["PlaceId"], out long PlaceId); if (!ValidPlaceId) return V2 ? WebServerResponse("Invalid PlaceId provided", false) : "Invalid PlaceId";

                string JobID = !string.IsNullOrEmpty(request.QueryString["JobId"]) ? request.QueryString["JobId"] : "";
                string FollowUser = request.QueryString["FollowUser"];
                string JoinVIP = request.QueryString["JoinVIP"];

                string Res = string.Empty; account.JoinServer(PlaceId, JobID, FollowUser == "true", JoinVIP == "true").ContinueWith(result => Res = result.Result).Wait();

                Context.Response.StatusCode = 200;

                return JsonConvert.SerializeObject(new { Message = $"Launched {Account} to {PlaceId}", Result = Res /* Support any old scripts that may use this value */, Success = true });
            }

            if (Method == "FollowUser") // https://github.com/ic3w0lf22/Roblox-Account-Manager/pull/52
            {
                if (!WebServer.Get<bool>("AllowLaunchAccount")) return V2 ? WebServerResponse("Method `FollowUser` not allowed", false) : "Method not allowed";

                string User = request.QueryString["Username"]; if (string.IsNullOrEmpty(User)) { Context.Response.StatusCode = 400; return V2 ? WebServerResponse("Invalid Username Parameter", false) : "Invalid Username Parameter"; }

                if (!GetUserID(User, out long UserId))
                    return V2 ? WebServerResponse("Failed to get UserId", false) : "Failed to get UserId";

                string Res = string.Empty; account.JoinServer(UserId, "", true).ContinueWith(result => Res = result.Result).Wait();

                Context.Response.StatusCode = 200;

                string Message = JsonConvert.SerializeObject(new { Message = $"Joining {User}'s game on {Account}", Result = Res });

                return V2 ? WebServerResponse(Message, true) : Message;
            }

            Context.Response.StatusCode = 200;

            if (Method == "GetCSRFToken") return V2 ? WebServerResponse(Token, true) : Token;
            if (Method == "GetAlias") return V2 ? WebServerResponse(account.Alias, true) : account.Alias;
            if (Method == "GetDescription") return V2 ? WebServerResponse(account.Description, true) : account.Description;

            if (Method == "BlockUser" && !string.IsNullOrEmpty(request.QueryString["UserId"]))
                try
                {
                    var Res = account.BlockUserId(request.QueryString["UserId"], Context: Context);

                    Context.Response.StatusCode = (int)Res.StatusCode;

                    return V2 ? WebServerResponse(Res.Content, Res.IsSuccessful) : Res.Content;
                }
                catch (Exception x) { return V2 ? WebServerResponse(x.Message, false) : x.Message; }
            if (Method == "UnblockUser" && !string.IsNullOrEmpty(request.QueryString["UserId"]))
                try
                {
                    var Res = account.UnblockUserId(request.QueryString["UserId"], Context: Context);

                    Context.Response.StatusCode = (int)Res.StatusCode;

                    return V2 ? WebServerResponse(Res.Content, Res.IsSuccessful) : Res.Content;
                }
                catch (Exception x) { return V2 ? WebServerResponse(x.Message, false) : x.Message; }
            if (Method == "GetBlockedList") try
                {
                    var Res = account.GetBlockedList(Context);

                    Context.Response.StatusCode = (int)Res.StatusCode;

                    return V2 ? WebServerResponse(Res.Content, Res.IsSuccessful) : Res.Content;
                }
                catch (Exception x) { return V2 ? WebServerResponse(x.Message, false) : x.Message; }
            if (Method == "UnblockEveryone") return V2 ? WebServerResponse(account.UnblockEveryone(Context), true) : account.UnblockEveryone(Context);

            if (Method == "SetServer" && !string.IsNullOrEmpty(request.QueryString["PlaceId"]) && !string.IsNullOrEmpty(request.QueryString["JobId"]))
            {
                string RSP = account.SetServer(Convert.ToInt64(request.QueryString["PlaceId"]), request.QueryString["JobId"], out bool Success);

                if (!Success)
                    Context.Response.StatusCode = 400;

                return V2 ? WebServerResponse(RSP, Success) : RSP;
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

                        return V2 ? WebServerResponse("Too many failed attempts", false) : "Too many failed attempts";
                    }

                    ServerData server = RBX_Alt_Manager.ServerList.servers[i];

                    if (AttemptedJoins.FirstOrDefault(x => x.id == server.id) != null) continue;
                    if (AttemptedJoins.Count > 100) AttemptedJoins.Clear();

                    AttemptedJoins.Add(server);

                    attempts++;

                    res = account.SetServer(RBX_Alt_Manager.ServerList.CurrentPlaceID, server.id, out bool iSuccess);

                    if (iSuccess)
                        return V2 ? WebServerResponse(res, iSuccess) : res;
                }

                bool Success = !string.IsNullOrEmpty(res);

                if (!Success) Context.Response.StatusCode = 400;

                return V2 ? WebServerResponse(Success ? "Failed" : res, Success) : Success ? "Failed" : res;
            }

            if (Method == "GetField" && !string.IsNullOrEmpty(request.QueryString["Field"])) return V2 ? WebServerResponse(account.GetField(request.QueryString["Field"]), true) : account.GetField(request.QueryString["Field"]);

            Context.Response.StatusCode = 401;

            if (Method == "SetField" && !string.IsNullOrEmpty(request.QueryString["Field"]) && !string.IsNullOrEmpty(request.QueryString["Value"]))
            {
                if (!WebServer.Get<bool>("AllowAccountEditing")) return V2 ? WebServerResponse("Method `SetField` not allowed", false) : "Method not allowed";

                account.SetField(request.QueryString["Field"], request.QueryString["Value"]);

                Context.Response.StatusCode = 200;

                string Res = $"Set Field {request.QueryString["Field"]} to {request.QueryString["Value"]} for {account.Username}";

                return V2 ? WebServerResponse(Res, true) : Res;
            }
            if (Method == "RemoveField" && !string.IsNullOrEmpty(request.QueryString["Field"]))
            {
                if (!WebServer.Get<bool>("AllowAccountEditing")) return V2 ? WebServerResponse("Method `RemoveField` not allowed", false) : "Method not allowed";

                account.RemoveField(request.QueryString["Field"]);

                Context.Response.StatusCode = 200;

                string Res = $"Removed Field {request.QueryString["Field"]} from {account.Username}";

                return V2 ? WebServerResponse(Res, true) : Res;
            }

            if (Method == "SetAvatar" && Body.TryParseJson(out object _))
            {
                account.SetAvatar(Body);

                Context.Response.StatusCode = 200;

                string Res = $"Attempting to set avatar of {account.Username} to {Body}";

                return V2 ? WebServerResponse(Res, true) : Res;
            }

            if (Method == "SetAlias" && !string.IsNullOrEmpty(Body))
            {
                if (!WebServer.Get<bool>("AllowAccountEditing")) return V2 ? WebServerResponse("Method `SetAlias` not allowed", false) : "Method not allowed";

                account.Alias = Body;
                UpdateAccountView(account);

                Context.Response.StatusCode = 200;

                string Res = $"Set Alias of {account.Username} to {Body}";

                return V2 ? WebServerResponse(Res, true) : Res;
            }
            if (Method == "SetDescription" && !string.IsNullOrEmpty(Body))
            {
                if (!WebServer.Get<bool>("AllowAccountEditing")) return V2 ? WebServerResponse("Method `SetDescription` not allowed", false) : "Method not allowed";

                account.Description = Body;
                UpdateAccountView(account);

                Context.Response.StatusCode = 200;

                string Res = $"Set Description of {account.Username} to {Body}";

                return V2 ? WebServerResponse(Res, true) : Res;
            }
            if (Method == "AppendDescription" && !string.IsNullOrEmpty(Body))
            {
                if (!WebServer.Get<bool>("AllowAccountEditing")) return V2 ? WebServerResponse("Method `AppendDescription` not allowed", false) : "Method not allowed";

                account.Description += Body;
                UpdateAccountView(account);

                Context.Response.StatusCode = 200;

                string Res = $"Appended Description of {account.Username} with {Body}";

                return V2 ? WebServerResponse(Res, true) : Res;
            }

            Context.Response.StatusCode = 404;

            return V2 ? WebServerResponse("404 not found", false) : "";
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
                catch { }
            }

            if (AccountControl.Get<bool>("StartOnLaunch"))
                LaunchNexus.PerformClick();
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

            Game G = RecentGames.FirstOrDefault(RG => RG.Details.filteredName == PlaceID.Text);

            if (G != null)
                PlaceID.Text = G.Details.placeId.ToString();

            PlaceID.Text = IDMatch.Success ? IDMatch.Groups[1].Value : Regex.Replace(PlaceID.Text, "[^0-9]", "");

            bool VIPServer = JobID.TextLength > 4 && JobID.Text.Substring(0, 4) == "VIP:";

            if (!long.TryParse(PlaceID.Text, out long PlaceId)) return;

            if (!PlaceTimer.Enabled)
                _ = Task.Run(() => AddRecentGame(new Game(PlaceId)));

            CancelLaunching();

            bool LaunchMultiple = AccountsView.SelectedObjects.Count > 1;

            new Thread(async () => // finally fixing an ancient bug in a dumb way, p.s. i do not condone this.
            {
                if (LaunchMultiple)
                {
                    LauncherToken = new CancellationTokenSource();

                    await LaunchAccounts(SelectedAccounts, PlaceId, VIPServer ? JobID.Text.Substring(4) : JobID.Text, false, VIPServer);
                }
                else if (SelectedAccount != null)
                {
                    string res = await SelectedAccount.JoinServer(PlaceId, VIPServer ? JobID.Text.Substring(4) : JobID.Text, false, VIPServer);

                    if (!res.Contains("Success"))
                        MessageBox.Show(res);
                }
            }).Start();
        }

        private async void Follow_Click(object sender, EventArgs e)
        {
            if (!GetUserID(UserID.Text, out long UserId))
            {
                MessageBox.Show("Failed to get UserId");
                return;
            }

            CancelLaunching();

            if (AccountsView.SelectedObjects.Count > 1)
            {
                LauncherToken = new CancellationTokenSource();

                await LaunchAccounts(SelectedAccounts, UserId, "", true);
            }
            else if (SelectedAccount != null)
            {
                string res = await SelectedAccount.JoinServer(UserId, "", true);

                if (!res.Contains("Success"))
                    MessageBox.Show(res);
            }
        }

        private void ServerList_Click(object sender, EventArgs e)
        {
            if (AccountsList.Count == 0 || LastValidAccount == null)
                MessageBox.Show("Some features may not work unless there is a valid account", "Roblox Account Manager", MessageBoxButtons.OK, MessageBoxIcon.Warning);

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
            General.Set("HideUsernames", HideUsernamesCheckbox.Checked ? "true" : "false");

            AccountsView.BeginUpdate();

            Username.Width = HideUsernamesCheckbox.Checked ? 0 : (int)(120 * Program.Scale);

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
            if (SelectedAccount != null)
            {
                if (SelectedAccount.GetAuthTicket(out string STicket))
                    Clipboard.SetText(STicket);

                return;
            }

            if (SelectedAccounts.Count < 1) return;

            List<string> Tickets = new List<string>();

            foreach (Account acc in SelectedAccounts)
            {
                if (acc.GetAuthTicket(out string Ticket))
                    Tickets.Add($"{acc.Username}:{Ticket}");
            }

            if (Tickets.Count > 0)
                Clipboard.SetText(string.Join("\n", Tickets));
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
                Passwords.Add($"{account.Password}");

            Clipboard.SetText(string.Join("\n", Passwords));
        }

        private void copyUserPassComboToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<string> Combos = new List<string>();

            foreach (Account account in AccountsView.SelectedObjects)
                Combos.Add($"{account.Username}:{account.Password}");

            Clipboard.SetText(string.Join("\n", Combos));
        }

        private void copyUserIdToolStripMenuItem_Click(object sender, EventArgs e)
        {
            List<string> UserIds = new List<string>();

            foreach (Account account in AccountsView.SelectedObjects)
                UserIds.Add(account.UserID.ToString());

            Clipboard.SetText(string.Join("\n", UserIds));
        }

        private void PlaceID_TextChanged(object sender, EventArgs e)
        {
            if (PlaceTimer.Enabled) PlaceTimer.Stop();

            PlaceTimer.Start();
        }

        private async void PlaceTimer_Tick(object sender, EventArgs e)
        {
            if (EconClient == null) return;

            PlaceTimer.Stop();

            RestRequest request = new RestRequest($"v2/assets/{PlaceID.Text}/details", Method.GET);
            request.AddHeader("Accept", "application/json");
            IRestResponse response = await EconClient.ExecuteAsync(request);

            if (response.IsSuccessful && response.StatusCode == HttpStatusCode.OK && response.Content.StartsWith("{") && response.Content.EndsWith("}"))
            {
                ProductInfo placeInfo = JsonConvert.DeserializeObject<ProductInfo>(response.Content);

                Utilities.InvokeIfRequired(this, () => CurrentPlace.Text = placeInfo.Name);
            }
        }

        private void moveToToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (AccountsView.SelectedObjects.Count == 0) return;

            string GroupName = ShowDialog("Group Name", "Move Account to Group", SelectedAccount != null ? SelectedAccount.Group : string.Empty);

            if (string.IsNullOrEmpty(GroupName)) GroupName = "Default";

            foreach (Account acc in AccountsView.SelectedObjects)
                acc.Group = GroupName;

            RefreshView();
            SaveAccounts();
        }

        private void copyGroupToolStripMenuItem_Click(object sender, EventArgs e) => Clipboard.SetText(SelectedAccount?.Group ?? "No Account Selected");

        private void copyAppLinkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedAccount == null) return;

            if (SelectedAccount.GetAuthTicket(out string Ticket))
            {
                double LaunchTime = Math.Floor((DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds * 1000);

                Random r = new Random();
                Clipboard.SetText(string.Format("<roblox-player://1/1+launchmode:app+gameinfo:{0}+launchtime:{1}+browsertrackerid:{2}+robloxLocale:en_us+gameLocale:en_us>", Ticket, LaunchTime, r.Next(500000, 600000).ToString() + r.Next(10000, 90000).ToString()));
            }
        }

        private void JoinDiscord_Click(object sender, EventArgs e) => Process.Start("https://discord.gg/MsEH7smXY8");

        private void OpenApp_Click(object sender, EventArgs e) => SelectedAccount?.LaunchApp();

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
                if (!(e.SourceModels[i - 1] is Account dragged)) continue;

                dragged.Group = droppedOn.Group;

                AccountsList.Remove(dragged);
                AccountsList.Insert(Index, dragged);
            }

            RefreshView(e.SourceModels[e.SourceModels.Count - 1]);
            SaveAccounts();
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

        private void toggleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AccountsView.ShowGroups = !AccountsView.ShowGroups;

            if (AccountsView.HeaderStyle != ColumnHeaderStyle.None) AccountsView.HeaderStyle = AccountsView.ShowGroups ? ColumnHeaderStyle.Nonclickable : ColumnHeaderStyle.Clickable;
        }

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
                ControlForm = new AccountControl
                {
                    StartPosition = FormStartPosition.Manual,
                    Top = Bottom,
                    Left = Left
                };
                ControlForm.Show();
                ControlForm.ApplyTheme();
            }
        }

        private async Task LaunchAccounts(List<Account> Accounts, long PlaceID, string JobID, bool FollowUser = false, bool VIPServer = false)
        {
            int Delay = General.Exists("AccountJoinDelay") ? General.Get<int>("AccountJoinDelay") : 8;

            bool AsyncJoin = General.Get<bool>("AsyncJoin");
            CancellationTokenSource Token = LauncherToken;

            foreach (Account account in Accounts)
            {
                if (Token.IsCancellationRequested) break;

                long PlaceId = PlaceID;
                string JobId = JobID;

                if (!FollowUser)
                {
                    if (!string.IsNullOrEmpty(account.GetField("SavedPlaceId")) && long.TryParse(account.GetField("SavedPlaceId"), out long PID)) PlaceId = PID;
                    if (!string.IsNullOrEmpty(account.GetField("SavedJobId"))) JobId = account.GetField("SavedJobId");
                }

                await account.JoinServer(PlaceId, JobId, FollowUser, VIPServer);

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

        private void infoToolStripMenuItem1_Click(object sender, EventArgs e) =>
            MessageBox.Show("Roblox Account Manager created by ic3w0lf under the GNU GPLv3 license.", "Roblox Account Manager", MessageBoxButtons.OK, MessageBoxIcon.Information);

        private void groupsToolStripMenuItem_Click(object sender, EventArgs e) =>
            MessageBox.Show("Groups can be sorted by naming them a number then whatever you want.\nFor example: You can put Group Apple on top by naming it '001 Apple' or '1Apple'.\nThe numbers will be hidden from the name but will be correctly sorted depending on the number.\nAccounts can also be dragged into groups.", "Roblox Account Manager", MessageBoxButtons.OK, MessageBoxIcon.Information);

        private void DonateButton_Click(object sender, EventArgs e) =>
            Process.Start("https://ic3w0lf22.github.io/donate.html");

        private void ConfigButton_Click(object sender, EventArgs e)
        {
            SettingsForm ??= new SettingsForm();

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

        private void HistoryIcon_MouseHover(object sender, EventArgs e) => RGForm.ShowForm();

        private void ShuffleIcon_Click(object sender, EventArgs e)
        {
            ShuffleJobID = !ShuffleJobID;

            if (sender != null)
            {
                General.Set("ShuffleJobId", ShuffleJobID ? "true" : "false");
                IniSettings.Save("RAMSettings.ini");
            }

            if (ShuffleJobID)
                if (ThemeEditor.LightImages)
                    ShuffleIcon.ColorImage(87, 245, 102);
                else
                    ShuffleIcon.ColorImage(57, 152, 22);
            else
            {
                if (BackColor.GetBrightness() < 0.5)
                    ShuffleIcon.ColorImage(255, 255, 255);
                else
                    ShuffleIcon.ColorImage(0, 0, 0);
            }
        }

        private void ShowDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(Path.Combine(Environment.CurrentDirectory, "AccountDumps")))
                Directory.CreateDirectory(Path.Combine(Environment.CurrentDirectory, "AccountDumps"));

            foreach (Account Account in AccountsView.SelectedObjects)
            {
                Task.Run(async () =>
                {
                    var UserInfo = await Account.GetUserInfo();
                    double AccountAge = -1;

                    if (DateTime.TryParse(UserInfo["created"].Value<string>(), out DateTime CreationTime))
                        AccountAge = (DateTime.UtcNow - CreationTime).TotalDays;

                    StringBuilder builder = new StringBuilder();

                    builder.AppendLine($"Username: {Account.Username}");
                    builder.AppendLine($"UserId: {Account.UserID}");
                    builder.AppendLine($"Robux: {await Account.GetRobux()}");
                    builder.AppendLine($"Account Age: {(AccountAge >= 0 ? $"{AccountAge:F1}" : "UNKNOWN")}");
                    builder.AppendLine($"Email Status: {await Account.GetEmailJSON()}");
                    builder.AppendLine($"User Info: {UserInfo}");
                    builder.AppendLine($"Other: {await Account.GetMobileInfo()}");
                    builder.AppendLine($"Fields: {JsonConvert.SerializeObject(Account.Fields)}");

                    string FileName = Path.Combine(Environment.CurrentDirectory, "AccountDumps", Account.Username + ".txt");

                    File.WriteAllText(FileName, builder.ToString());

                    Process.Start(FileName);
                });
            }
        }
    }
}
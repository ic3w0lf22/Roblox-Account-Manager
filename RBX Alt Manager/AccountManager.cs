using BrightIdeasSoftware;
using ICSharpCode.SharpZipLib.Zip;
using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PuppeteerSharp;
using RBX_Alt_Manager.Classes;
using RBX_Alt_Manager.Forms;
using RBX_Alt_Manager.Properties;
using RestSharp;
using Sodium;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebSocketSharp;

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
        private static bool PuppeteerSupported;
        public static string CurrentVersion;
        public OLVListItem SelectedAccountItem { get; private set; }
        private WebServer AltManagerWS;
        private string WSPassword { get; set; }
        public System.Timers.Timer AutoCookieRefresh { get; private set; }

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

        private bool IsResettingPassword;
        private bool IsDownloadingChromium;
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
                General.Set("WindowScale", Screen.PrimaryScreen.Bounds.Height <= Screen.PrimaryScreen.Bounds.Width /*scuffed*/ ? Math.Max(Math.Min(Screen.PrimaryScreen.Bounds.Height / 1080f, 2f), 1f).ToString(".0#", CultureInfo.InvariantCulture) : "1.0");

                if (Program.Scale > 1)
                    if (!Utilities.YesNoPrompt("Roblox Account Manager", "RAM has detected you have a monitor larger than average", $"Would you like to keep the WindowScale setting of {Program.Scale:F2}?", false))
                        General.Set("WindowScale", "1.0");
                    else
                        MessageBox.Show("In case the font scaling is incorrect, open RAMSettings.ini and change \"ScaleFonts=true\" to \"ScaleFonts=false\" without the quotes.", "Roblox Account Manager", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            if (!General.Exists("ScaleFonts")) General.Set("ScaleFonts", "true");
            if (!General.Exists("AutoCookieRefresh")) General.Set("AutoCookieRefresh", "true");
            if (!General.Exists("AutoCloseLastProcess")) General.Set("AutoCloseLastProcess", "true");
            if (!General.Exists("ShowPresence")) General.Set("ShowPresence", "true");
            if (!General.Exists("PresenceUpdateRate")) General.Set("PresenceUpdateRate", "5");
            if (!General.Exists("UnlockFPS")) General.Set("UnlockFPS", "false");
            if (!General.Exists("MaxFPSValue")) General.Set("MaxFPSValue", "120");
            if (!General.Exists("UseCefSharpBrowser")) General.Set("UseCefSharpBrowser", "false");

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

            AccountsView.SetObjects(AccountsList);

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

            var VCKey = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\WOW6432Node\Microsoft\VisualStudio\14.0\VC\Runtimes\X86");

            if (!Prompts.Exists("VCPrompted") && (VCKey == null || (VCKey is RegistryKey && VCKey.GetValue("Bld") is int VCVersion && VCVersion < 32532)))
                Task.Run(async () => // Make sure the user has the latest 2015-2022 vcredist installed
                {
                    using HttpClient Client = new HttpClient();
                    byte[] bs = await Client.GetByteArrayAsync("https://aka.ms/vs/17/release/vc_redist.x86.exe");
                    string FN = Path.Combine(Path.GetTempPath(), "vcredist.tmp");

                    File.WriteAllBytes(FN, bs);

                    Process.Start(new ProcessStartInfo(FN) { UseShellExecute = false, Arguments = "/q /norestart" }).WaitForExit();

                    Prompts.Set("VCPrompted", "1");
                });
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

        private static ReadOnlyMemory<byte> PasswordHash; // Store the hash after the data is successfully decrypted so we can encrypt again.

        private void LoadAccounts(byte[] Hash = null)
        {
            bool EnteredPassword = false;
            byte[] Data = File.Exists(SaveFilePath) ? File.ReadAllBytes(SaveFilePath) : Array.Empty<byte>();

            if (Data.Length > 0)
            {
                var Header = new ReadOnlySpan<byte>(Data, 0, Cryptography.RAMHeader.Length);

                if (Header.SequenceEqual(Cryptography.RAMHeader))
                {
                    if (Hash == null)
                    {
                        EncryptionSelectionPanel.Visible = false;
                        PasswordSelectionPanel.Visible = false;
                        PasswordLayoutPanel.Visible = true;
                        PasswordPanel.Visible = true;
                        PasswordPanel.BringToFront();
                        PasswordTextBox.Focus();

                        return;
                    }

                    Data = Cryptography.Decrypt(Data, Hash);
                    AccountsList = JsonConvert.DeserializeObject<List<Account>>(Encoding.UTF8.GetString(Data));
                    PasswordHash = new ReadOnlyMemory<byte>(ProtectedData.Protect(Hash, Array.Empty<byte>(), DataProtectionScope.CurrentUser));

                    PasswordPanel.Visible = false;
                    EnteredPassword = true;
                }
                else
                    try { AccountsList = JsonConvert.DeserializeObject<List<Account>>(Encoding.UTF8.GetString(ProtectedData.Unprotect(Data, Entropy, DataProtectionScope.CurrentUser))); }
                    catch (CryptographicException e)
                    {
                        try { AccountsList = JsonConvert.DeserializeObject<List<Account>>(Encoding.UTF8.GetString(Data)); }
                        catch
                        {
                            File.WriteAllBytes(SaveFilePath + ".bak", Data);

                            MessageBox.Show($"Failed to load accounts!\nA backup file was created in case the data can be recovered.\n\n{e.Message}");
                        }
                    }
            }

            AccountsList ??= new List<Account>();

            if (!EnteredPassword && AccountsList.Count == 0 && File.Exists($"{SaveFilePath}.backup") && File.ReadAllBytes($"{SaveFilePath}.backup") is byte[] BackupData && BackupData.Length > 0)
            {
                var Header = new ReadOnlySpan<byte>(BackupData, 0, Cryptography.RAMHeader.Length);

                if (Header.SequenceEqual(Cryptography.RAMHeader) && MessageBox.Show("The existing backup file is password-locked, would you like to attempt to load it?", "Roblox Account Manager", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    if (File.Exists(SaveFilePath))
                    {
                        if (File.Exists($"{SaveFilePath}.old")) File.Delete($"{SaveFilePath}.old");

                        File.Move(SaveFilePath, $"{SaveFilePath}.old");
                    }

                    File.Move($"{SaveFilePath}.backup", SaveFilePath);

                    LoadAccounts();

                    return;
                }

                if (MessageBox.Show("No accounts were loaded but there is a backup file, would you like to load the backup file?", "Roblox Account Manager", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {
                    try
                    {
                        string Decoded = Encoding.UTF8.GetString(ProtectedData.Unprotect(BackupData, Entropy, DataProtectionScope.CurrentUser));

                        AccountsList = JsonConvert.DeserializeObject<List<Account>>(Decoded);
                    }
                    catch
                    {
                        try { AccountsList = JsonConvert.DeserializeObject<List<Account>>(Encoding.UTF8.GetString(BackupData)); }
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

        public static void SaveAccounts(bool BypassRateLimit = false, bool BypassCountCheck = false)
        {
            if ((!BypassRateLimit && (DateTime.Now - startTime).Seconds < 2) || (!BypassCountCheck && AccountsList.Count == 0)) return;

            lock (saveLock)
            {
                byte[] OldInfo = File.Exists(SaveFilePath) ? File.ReadAllBytes(SaveFilePath) : Array.Empty<byte>();
                string SaveData = JsonConvert.SerializeObject(AccountsList);

                FileInfo OldFile = new FileInfo(SaveFilePath);
                FileInfo Backup = new FileInfo($"{SaveFilePath}.backup");

                if (!Backup.Exists || (Backup.Exists && (DateTime.Now - Backup.LastWriteTime).TotalMinutes > 60 * 8))
                    File.WriteAllBytes(Backup.FullName, OldInfo);

                if (!PasswordHash.IsEmpty)
                    File.WriteAllBytes(SaveFilePath, Cryptography.Encrypt(SaveData, ProtectedData.Unprotect(PasswordHash.ToArray(), Array.Empty<byte>(), DataProtectionScope.CurrentUser)));
                else
                {
                    if (File.Exists(Path.Combine(Environment.CurrentDirectory, "NoEncryption.IUnderstandTheRisks.iautamor")))
                        File.WriteAllBytes(SaveFilePath, Encoding.UTF8.GetBytes(SaveData));
                    else
                        File.WriteAllBytes(SaveFilePath, ProtectedData.Protect(Encoding.UTF8.GetBytes(SaveData), Entropy, DataProtectionScope.LocalMachine));
                }
            }
        }

        public void ResetEncryption(bool ManualReset = false)
        {
            foreach (var Form in Application.OpenForms.OfType<Form>())
                if (Form != this)
                    Form.Hide();

            IsResettingPassword = true;

            PasswordLayoutPanel.Visible = !PasswordHash.IsEmpty && ManualReset;
            PasswordSelectionPanel.Visible = false;
            EncryptionSelectionPanel.Visible = PasswordHash.IsEmpty || !ManualReset;

            PasswordPanel.Visible = true;
            PasswordPanel.BringToFront();
        }

        private void PasswordTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                UnlockButton.PerformClick();

                e.Handled = true;
            }
        }

        private void Error(string Message)
        {
            Program.Logger.Error(Message);

            throw new Exception(Message);
        }

        private void UnlockButton_Click(object sender, EventArgs e)
        {
            try
            {
                byte[] Hash = CryptoHash.Hash(PasswordTextBox.Text);

                if (PasswordTextBox.Text.Length < 4)
                    Error("Invalid password, your password must contain 4 or more characters");

                if (IsResettingPassword)
                {
                    byte[] Data = File.Exists(SaveFilePath) ? File.ReadAllBytes(SaveFilePath) : Array.Empty<byte>();

                    if (Data.Length > 0)
                    {
                        var Header = new ReadOnlySpan<byte>(Data, 0, Cryptography.RAMHeader.Length);

                        if (Header.SequenceEqual(Cryptography.RAMHeader))
                        {
                            if (Hash == null)
                            {
                                EncryptionSelectionPanel.Visible = false;
                                PasswordSelectionPanel.Visible = false;
                                PasswordLayoutPanel.Visible = true;
                                PasswordPanel.Visible = true;
                                PasswordPanel.BringToFront();
                                PasswordTextBox.Focus();

                                return;
                            }

                            Cryptography.Decrypt(Data, Hash);

                            PasswordLayoutPanel.Visible = false;
                            EncryptionSelectionPanel.Visible = true;
                            IsResettingPassword = false;
                        }
                    }
                }
                else
                    LoadAccounts(Hash);
            }
            catch (Exception exception)
            {
                MessageBox.Show($"Incorrect Password!\n\n{exception.Message}", "Roblox Account Manager", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally { PasswordTextBox.Text = string.Empty; PasswordTextBox.Focus(); }
        }

        private void DefaultEncryptionButton_Click(object sender, EventArgs e)
        {
            PasswordHash = Array.Empty<byte>();
            SaveAccounts(true, true);

            PasswordPanel.Visible = false;
        }

        private void PasswordEncryptionButton_Click(object sender, EventArgs e)
        {
            EncryptionSelectionPanel.Visible = false;
            PasswordLayoutPanel.Visible = false;
            PasswordSelectionPanel.Visible = true;
        }

        private ReadOnlyMemory<byte> LastHash = null;

        private void SetPasswordButton_Click(object sender, EventArgs e)
        {
            if (PasswordSelectionTB.Text.Length < 4)
            {
                MessageBox.Show("Invalid password, your password must contain 4 or more characters", "Roblox Account Manager", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            byte[] Hash = CryptoHash.Hash(PasswordSelectionTB.Text);

            PasswordHash = new ReadOnlyMemory<byte>(ProtectedData.Protect(Hash, Array.Empty<byte>(), DataProtectionScope.CurrentUser));

            if (LastHash.IsEmpty)
            {
                LastHash = new ReadOnlyMemory<byte>(PasswordHash.ToArray());
                PasswordSelectionTB.Text = string.Empty;
                MessageBox.Show("Please confirm your password.", "Roblox Account Manager", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
            else
            {
                if (ProtectedData.Unprotect(LastHash.ToArray(), Array.Empty<byte>(), DataProtectionScope.CurrentUser).SequenceEqual(Hash.ToArray()))
                {
                    SaveAccounts(true, true);

                    PasswordSelectionTB.Text = string.Empty;
                    PasswordPanel.Visible = false;

                    LastHash = null;
                }
                else
                    MessageBox.Show("You have entered the wrong password, please try again.", "Roblox Account Manager", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        CancellationTokenSource PasswordSelectionCancellationToken;

        private void PasswordSelectionTB_TextChanged(object sender, EventArgs e)
        {
            PasswordSelectionCancellationToken?.Cancel();

            SetPasswordButton.Enabled = false;

            PasswordSelectionCancellationToken = new CancellationTokenSource();
            var Token = PasswordSelectionCancellationToken.Token;

            Task.Run(async () =>
            {
                await Task.Delay(500); // Wait until the user has stopped typing to enable the continue button

                if (Token.IsCancellationRequested)
                    return;

                AccountsView.InvokeIfRequired(() => SetPasswordButton.Enabled = true);
            }, PasswordSelectionCancellationToken.Token);
        }

        private void PasswordPanel_VisibleChanged(object sender, EventArgs e)
        {
            foreach (Control Control in Controls)
                if (Control != PasswordPanel)
                    Control.Enabled = !PasswordPanel.Visible;
        }

        public static bool GetUserID(string Username, out long UserId, out RestResponse response)
        {
            RestRequest request = LastValidAccount?.MakeRequest("v1/usernames/users", Method.Post) ?? new RestRequest("v1/usernames/users", Method.Post);
            request.AddJsonBody(new { usernames = new string[] { Username } });

            response = UsersClient.Execute(request);

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

        public static Account AddAccount(string SecurityToken, string Password = "", string AccountJSON = null)
        {
            Account account = new Account(SecurityToken, AccountJSON);

            if (account.Valid)
            {
                account.Password = Password;

                Account exists = AccountsList.AsReadOnly().FirstOrDefault(acc => acc.UserID == account.UserID);

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

                SaveAccounts(true);

                return account;
            }

            return null;
        }

        public static string ShowDialog(string text, string caption, string defaultText = "", bool big = false) // tbh pasted from stackoverflow
        {
            Form prompt = new Form()
            {
                Width = 340,
                Height = big ? 420 : 125,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MaximizeBox = false,
                Text = caption,
                StartPosition = FormStartPosition.CenterScreen
            };

            Label textLabel = new Label() { Left = 15, Top = 10, Text = text, AutoSize = true };
            Control textBox;
            Button confirmation = new Button() { Text = "OK", Left = 15, Width = 100, Top = big ? 350 : 50, DialogResult = DialogResult.OK };

            if (big) textBox = new RichTextBox() { Left = 15, Top = 15 + textLabel.Size.Height, Width = 295, Height = 330 - textLabel.Size.Height, Text = defaultText };
            else textBox = new TextBox() { Left = 15, Top = 25, Width = 295, Text = defaultText };

            confirmation.Click += (sender, e) => { prompt.Close(); };
            prompt.Controls.Add(textBox);
            prompt.Controls.Add(confirmation);
            prompt.Controls.Add(textLabel);
            if (!big) prompt.AcceptButton = confirmation;

            prompt.Rescale();

            return prompt.ShowDialog() == DialogResult.OK ? textBox.Text : "/UC";
        }

        private void AccountManager_Load(object sender, EventArgs e)
        {
            PasswordPanel.Dock = DockStyle.Fill;

            string AFN = Path.Combine(Directory.GetCurrentDirectory(), "Auto Update.exe");
            string AU2FN = Path.Combine(Directory.GetCurrentDirectory(), "AU.exe");

            if (File.Exists(AFN)) File.Delete(AFN);
            if (File.Exists(AU2FN)) File.Delete(AU2FN);

            DirectoryInfo UpdateDir = new DirectoryInfo(Path.Combine(Environment.CurrentDirectory, "Update"));

            if (UpdateDir.Exists)
                UpdateDir.RecursiveDelete();

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
            GameJoinClient = new RestClient(new RestClientOptions("https://gamejoin.roblox.com/") { UserAgent = "Roblox/WinInet" });
            UsersClient = new RestClient("https://users.roblox.com");
            FriendsClient = new RestClient("https://friends.roblox.com");
            Web13Client = new RestClient("https://web.roblox.com/");

            if (File.Exists(SaveFilePath))
                LoadAccounts();
            else
                ResetEncryption();

            ApplyTheme();

            RGForm.RecentGameSelected += (sender, e) => { PlaceID.Text = e.Game.Details?.placeId.ToString(); };

            PlaceID.Text = General.Exists("SavedPlaceId") ? General.Get("SavedPlaceId") : "5315046213";
            UserID.Text = General.Exists("SavedFollowUser") ? General.Get("SavedFollowUser") : string.Empty;

            if (!Developer.Get<bool>("DevMode"))
            {
                AccountsStrip.Items.Remove(viewFieldsToolStripMenuItem);
                AccountsStrip.Items.Remove(getAuthenticationTicketToolStripMenuItem);
                AccountsStrip.Items.Remove(copyRbxplayerLinkToolStripMenuItem);
                AccountsStrip.Items.Remove(copySecurityTokenToolStripMenuItem);
                AccountsStrip.Items.Remove(copyAppLinkToolStripMenuItem);
            }
            else
                ArgumentsB.Visible = true;

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
                            string Current = fvi.FileVersion.TrimEnd('.', '0').Replace(".", string.Empty);
                            string New = match.Groups[1].Value.TrimEnd('.', '0').Replace(".", string.Empty);

                            if (Current.Length > New.Length)
                                New = New.PadRight(Current.Length, '0');
                            else if (New.Length > Current.Length)
                                Current = Current.PadRight(New.Length, '0');

                            if (double.TryParse(New, out double NV) && double.TryParse(Current, out double CV) && NV > CV)
                            {
                                bool ShouldUpdate = Utilities.YesNoPrompt("Roblox Account Manager", "An update is available", "Would you like to update now?");

                                if (ShouldUpdate)
                                {
                                    File.WriteAllBytes(AFN, File.ReadAllBytes(Application.ExecutablePath));
                                    Process.Start(AFN, "-update");
                                    Environment.Exit(1);
                                    //if (File.Exists(AFN))
                                    //{
                                    //    Process.Start(AFN, "skip");
                                    //    Environment.Exit(1);
                                    //}
                                    //else
                                    //{
                                    //    MessageBox.Show("You do not have the auto updater downloaded, go to the github page and download the latest release.");
                                    //    Process.Start("https://github.com/ic3w0lf22/Roblox-Account-Manager/releases");
                                    //}
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

                    List<string> Prefixes = new List<string>() { $"http://localhost:{Port}/" };

                    if (WebServer.Get<bool>("AllowExternalConnections"))
                        if (Program.Elevated)
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
            Task.Run(RobloxProcess.UpdateMatches);

            if (General.Get<bool>("ShuffleJobId"))
                ShuffleIcon_Click(null, EventArgs.Empty);

            if (General.Get<bool>("AutoCookieRefresh"))
            {
                AutoCookieRefresh = new System.Timers.Timer(60000 * 5) { Enabled = true };
                AutoCookieRefresh.Elapsed += async (s, e) =>
                {
                    int Count = 0;

                    foreach (var Account in AccountsList)
                    {
                        if (Account.GetField("NoCookieRefresh") != "true" && (DateTime.Now - Account.LastUse).TotalDays > 20 && (DateTime.Now - Account.LastAttemptedRefresh).TotalDays >= 7)
                        {
                            Program.Logger.Info($"Attempting to refresh {Account.Username} | Last Use: {Account.LastUse}");

                            Account.LastAttemptedRefresh = DateTime.Now;

                            if (Account.LogOutOfOtherSessions(true)) Count++;

                            await Task.Delay(5000);
                        }
                    };
                };
            }

            var PresenceTimer = new System.Timers.Timer(60000 * 2) { Enabled = true };
            PresenceTimer.Elapsed += (s, e) => AccountsView.InvokeIfRequired(async () => await UpdatePresence());
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

            Controls.ApplyTheme();

            afform.ApplyTheme();
            ServerListForm.ApplyTheme();
            UtilsForm.ApplyTheme();
            ImportAccountsForm.ApplyTheme();
            FieldsForm.ApplyTheme();
            ThemeForm.ApplyTheme();
            RGForm.ApplyTheme();

            ControlForm?.ApplyTheme();
            SettingsForm?.ApplyTheme();
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

            string Reply(string Response, bool Success = false, int Code = -1, string Raw = null)
            {
                Context.Response.StatusCode = Code > 0 ? Code : (Success ? 200 : 400);

                return V2 ? WebServerResponse(Response, Success) : (Raw ?? Response);
            }

            if (!request.IsLocal && !WebServer.Get<bool>("AllowExternalConnections")) return Reply("External connections are not allowed", false, 401, string.Empty);
            if (AbsolutePath == "/favicon.ico") return ""; // always return nothing

            if (AbsolutePath == "/Running") return Reply("Roblox Account Manager is running", true, Raw: "true");

            string Body = new StreamReader(request.InputStream).ReadToEnd();
            string Method = AbsolutePath.Substring(1);
            string Account = request.QueryString["Account"];
            string Password = request.QueryString["Password"];

            if (WebServer.Get<bool>("EveryRequestRequiresPassword") && (WSPassword.Length < 6 || Password != WSPassword)) return Reply("Invalid Password, make sure your password contains 6 or more characters", false, 401, "Invalid Password");

            if ((Method == "GetCookie" || Method == "GetAccounts" || Method == "LaunchAccount" || Method == "FollowUser") && ((WSPassword != null && WSPassword.Length < 6) || (Password != null && Password != WSPassword))) return Reply("Invalid Password, make sure your password contains 6 or more characters", false, 401, "Invalid Password");

            if (Method == "GetAccounts")
            {
                if (!WebServer.Get<bool>("AllowGetAccounts")) return Reply("Method `GetAccounts` not allowed", false, 401, "Method not allowed");

                string Names = "";
                string GroupFilter = request.QueryString["Group"];

                foreach (Account acc in AccountsList)
                {
                    if (!string.IsNullOrEmpty(GroupFilter) && acc.Group != GroupFilter) continue;

                    Names += acc.Username + ",";
                }

                return Reply(Names.Remove(Names.Length - 1), true, Raw: Names.Remove(Names.Length - 1));
            }

            if (Method == "GetAccountsJson")
            {
                if (!WebServer.Get<bool>("AllowGetAccounts")) return Reply("Method `GetAccountsJson` not allowed", false, 401, "Method not allowed");

                string GroupFilter = request.QueryString["Group"];
                bool ShowCookies = WSPassword.Length >= 6 && Password != WSPassword && request.QueryString["IncludeCookies"] == "true" && WebServer.Get<bool>("AllowGetCookie");

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

                return Reply(JsonConvert.SerializeObject(Objects), true);
            }

            if (Method == "ImportCookie")
            {
                Account New = AddAccount(request.QueryString["Cookie"]);

                bool Success = New != null;

                return Reply(Success ? "Cookie successfully imported" : "[ImportCookie] An error was encountered importing the cookie", Success, Raw: Success ? "true" : "false");
            }

            if (string.IsNullOrEmpty(Account)) return Reply("Empty Account", false);

            Account account = AccountsList.FirstOrDefault(x => x.Username == Account || x.UserID.ToString() == Account);

            if (account == null || !account.GetCSRFToken(out string Token)) return Reply("Invalid Account, the account's cookie may have expired and resulted in the account being logged out", false, Raw: "Invalid Account");

            if (Method == "GetCookie")
            {
                if (!WebServer.Get<bool>("AllowGetCookie")) return Reply("Method `GetCookie` not allowed", false, 401, "Method not allowed");

                return Reply(account.SecurityToken, true);
            }

            if (Method == "LaunchAccount")
            {
                if (!WebServer.Get<bool>("AllowLaunchAccount")) return Reply("Method `LaunchAccount` not allowed", false, 401, "Method not allowed");

                bool ValidPlaceId = long.TryParse(request.QueryString["PlaceId"], out long PlaceId); if (!ValidPlaceId) return Reply("Invalid PlaceId provided", false, Raw: "Invalid PlaceId");

                string JobID = !string.IsNullOrEmpty(request.QueryString["JobId"]) ? request.QueryString["JobId"] : "";
                string FollowUser = request.QueryString["FollowUser"];
                string JoinVIP = request.QueryString["JoinVIP"];

                account.JoinServer(PlaceId, JobID, FollowUser == "true", JoinVIP == "true");

                return Reply($"Launched {Account} to {PlaceId}", true);
            }

            if (Method == "FollowUser") // https://github.com/ic3w0lf22/Roblox-Account-Manager/pull/52
            {
                if (!WebServer.Get<bool>("AllowLaunchAccount")) return Reply("Method `FollowUser` not allowed", false, 401, "Method not allowed");

                string User = request.QueryString["Username"]; if (string.IsNullOrEmpty(User)) return Reply("Invalid Username Parameter", false);

                if (!GetUserID(User, out long UserId, out var Response))
                    return Reply($"[{Response.StatusCode} {Response.StatusDescription}] Failed to get UserId: {Response.Content}", false);

                account.JoinServer(UserId, "", true);

                return Reply($"Joining {User}'s game on {Account}", true);
            }

            if (Method == "GetCSRFToken") return Reply(Token, true);
            if (Method == "GetAlias") return Reply(account.Alias, true);
            if (Method == "GetDescription") return Reply(account.Description, true);

            if (Method == "BlockUser" && !string.IsNullOrEmpty(request.QueryString["UserId"]))
                try
                {
                    var Res = account.BlockUserId(request.QueryString["UserId"], Context: Context);

                    return Reply(Res.Content, Res.IsSuccessful, (int)Res.StatusCode);
                }
                catch (Exception x) { return Reply(x.Message, false, 500); }
            if (Method == "UnblockUser" && !string.IsNullOrEmpty(request.QueryString["UserId"]))
                try
                {
                    var Res = account.UnblockUserId(request.QueryString["UserId"], Context: Context);

                    return Reply(Res.Content, Res.IsSuccessful, (int)Res.StatusCode);
                }
                catch (Exception x) { return Reply(x.Message, false, 500); }
            if (Method == "GetBlockedList") try
                {
                    var Res = account.GetBlockedList(Context);

                    return Reply(Res.Content, Res.IsSuccessful, (int)Res.StatusCode);
                }
                catch (Exception x) { return Reply(x.Message, false, 500); }
            if (Method == "UnblockEveryone" && account.UnblockEveryone(out string UbRes) is bool UbSuccess) return Reply(UbRes, UbSuccess);

            if (Method == "SetServer" && !string.IsNullOrEmpty(request.QueryString["PlaceId"]) && !string.IsNullOrEmpty(request.QueryString["JobId"]))
            {
                string RSP = account.SetServer(Convert.ToInt64(request.QueryString["PlaceId"]), request.QueryString["JobId"], out bool Success);

                return Reply(RSP, Success);
            }

            if (Method == "SetRecommendedServer")
            {
                int attempts = 0;
                string res = "-1";

                for (int i = RBX_Alt_Manager.ServerList.servers.Count - 1; i > 0; i--)
                {
                    if (attempts > 10)
                        return Reply("Too many failed attempts", false);

                    ServerData server = RBX_Alt_Manager.ServerList.servers[i];

                    if (AttemptedJoins.FirstOrDefault(x => x.id == server.id) != null) continue;
                    if (AttemptedJoins.Count > 100) AttemptedJoins.Clear();

                    AttemptedJoins.Add(server);

                    attempts++;

                    res = account.SetServer(!string.IsNullOrEmpty(request.QueryString["PlaceId"]) ? Convert.ToInt64(request.QueryString["PlaceId"]) : RBX_Alt_Manager.ServerList.CurrentPlaceID, server.id, out bool iSuccess);

                    if (iSuccess)
                        return Reply(res, iSuccess);
                }

                bool Success = !string.IsNullOrEmpty(res);

                return Reply(Success ? "Failed" : res, Success);
            }

            if (Method == "GetField" && !string.IsNullOrEmpty(request.QueryString["Field"])) return Reply(account.GetField(request.QueryString["Field"]), true);

            if (Method == "SetField" && !string.IsNullOrEmpty(request.QueryString["Field"]) && !string.IsNullOrEmpty(request.QueryString["Value"]))
            {
                if (!WebServer.Get<bool>("AllowAccountEditing")) return Reply("Method `SetField` not allowed", false, 401, "Method not allowed");

                account.SetField(request.QueryString["Field"], request.QueryString["Value"]);

                return Reply($"Set Field {request.QueryString["Field"]} to {request.QueryString["Value"]} for {account.Username}", true);
            }
            if (Method == "RemoveField" && !string.IsNullOrEmpty(request.QueryString["Field"]))
            {
                if (!WebServer.Get<bool>("AllowAccountEditing")) return Reply("Method `RemoveField` not allowed", false, 401, "Method not allowed");

                account.RemoveField(request.QueryString["Field"]);

                return Reply($"Removed Field {request.QueryString["Field"]} from {account.Username}", true);
            }

            if (Method == "SetAvatar" && Body.TryParseJson(out object _))
            {
                account.SetAvatar(Body);

                return Reply($"Attempting to set avatar of {account.Username} to {Body}", true);
            }

            if (Method == "SetAlias" && !string.IsNullOrEmpty(Body))
            {
                if (!WebServer.Get<bool>("AllowAccountEditing")) return Reply("Method `SetAlias` not allowed", false, Raw: "Method not allowed");

                account.Alias = Body;
                UpdateAccountView(account);

                return Reply($"Set Alias of {account.Username} to {Body}", true);
            }
            if (Method == "SetDescription" && !string.IsNullOrEmpty(Body))
            {
                if (!WebServer.Get<bool>("AllowAccountEditing")) Reply("Method `SetDescription` not allowed", false, Raw: "Method not allowed");

                account.Description = Body;
                UpdateAccountView(account);

                return Reply($"Set Description of {account.Username} to {Body}", true);
            }
            if (Method == "AppendDescription" && !string.IsNullOrEmpty(Body))
            {
                if (!WebServer.Get<bool>("AllowAccountEditing")) return V2 ? WebServerResponse("Method `AppendDescription` not allowed", false) : "Method not allowed";

                account.Description += Body;
                UpdateAccountView(account);

                return Reply($"Appended Description of {account.Username} with {Body}", true);
            }

            return Reply("404 not found", false, 404);
        }

        private void AccountManager_Shown(object sender, EventArgs e)
        {
            if (!UpdateMultiRoblox() && !General.Get<bool>("HideRbxAlert"))
                MessageBox.Show("WARNING: Roblox is currently running, multi roblox will not work until you restart the account manager with roblox closed.", "Roblox Account Manager", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            int Major = Environment.OSVersion.Version.Major, Minor = Environment.OSVersion.Version.Minor;

            PuppeteerSupported = !(Major < 6 || (Major == 6 && Minor <= 1));

            if (General.Get<bool>("UseCefSharpBrowser")) PuppeteerSupported = false;

            if (!PuppeteerSupported)
            {
                AddAccountsStrip.Items.Remove(bulkUserPassToolStripMenuItem);
                AddAccountsStrip.Items.Remove(customURLJSToolStripMenuItem);
                OpenBrowserStrip.Items.Remove(URLJSToolStripMenuItem);
                OpenBrowserStrip.Items.Remove(joinGroupToolStripMenuItem);
            }

            if (PuppeteerSupported && (!Directory.Exists(AccountBrowser.Fetcher.DownloadsFolder) || Directory.GetDirectories(AccountBrowser.Fetcher.DownloadsFolder).Length == 0))
            {
                Add.Visible = false;
                Remove.Visible = false;
                DownloadProgressBar.Visible = true;
                DLChromiumLabel.Visible = true;

                Task.Run(async () =>
                {
                    IsDownloadingChromium = true;

                    void DownloadProgressChanged(object s, DownloadProgressChangedEventArgs e) => DownloadProgressBar.InvokeIfRequired(() => { DownloadProgressBar.Value = e.ProgressPercentage; });

                    AccountBrowser.Fetcher.DownloadProgressChanged += DownloadProgressChanged;
                    await AccountBrowser.Fetcher.DownloadAsync(BrowserFetcher.DefaultChromiumRevision);
                    AccountBrowser.Fetcher.DownloadProgressChanged -= DownloadProgressChanged;

                    IsDownloadingChromium = false;

                    this.InvokeIfRequired(() =>
                    {
                        Add.Visible = true;
                        Remove.Visible = true;
                        DownloadProgressBar.Visible = false;
                        DLChromiumLabel.Visible = false;
                    });
                });
            }
            else if (!PuppeteerSupported)
            {
                FileInfo Cef = new FileInfo(Path.Combine(Environment.CurrentDirectory, "x86", "CefSharp.dll"));

                if (Cef.Exists)
                {
                    FileVersionInfo Info = FileVersionInfo.GetVersionInfo(Cef.FullName);

                    if (Info.ProductMajorPart != 109)
                        try { Directory.GetParent(Cef.FullName).RecursiveDelete(); } catch { }
                }

                if (!Directory.Exists(Path.Combine(Environment.CurrentDirectory, "x86")))
                {
                    var Existing = new DirectoryInfo(Path.Combine(Environment.CurrentDirectory, "x86"));

                    DLChromiumLabel.Text = "Downloading CefSharp...";

                    Add.Visible = false;
                    Remove.Visible = false;
                    DownloadProgressBar.Visible = true;
                    DLChromiumLabel.Visible = true;

                    Task.Run(async () =>
                    {
                        IsDownloadingChromium = true;

                        using HttpClient client = new HttpClient();

                        string FileName = Path.GetTempFileName(), DownloadUrl = Resources.CefSharpDownload;

                        var TotalDownloadSize = (await client.SendAsync(new HttpRequestMessage(HttpMethod.Head, DownloadUrl))).Content.Headers.ContentLength.Value;
                        Progress<float> progress = new Progress<float>(progress => DownloadProgressBar.InvokeIfRequired(() => DownloadProgressBar.Value = (int)(progress * 100)));

                        using (var file = new FileStream(FileName, FileMode.Create, FileAccess.Write, FileShare.None))
                            await client.DownloadAsync(DownloadUrl, file, progress);

                        if (Existing.Exists) Existing.RecursiveDelete();

                        System.IO.Compression.ZipFile.ExtractToDirectory(FileName, Environment.CurrentDirectory);

                        IsDownloadingChromium = false;

                        this.InvokeIfRequired(() =>
                        {
                            Add.Visible = true;
                            Remove.Visible = true;
                            DownloadProgressBar.Visible = false;
                            DLChromiumLabel.Visible = false;
                        });
                    });
                }
            }

            if (AccountControl.Get<bool>("StartOnLaunch"))
                LaunchNexus.PerformClick();
        }

        public bool UpdateMultiRoblox()
        {
            bool Enabled = General.Get<bool>("EnableMultiRbx");

            if (Enabled && rbxMultiMutex == null)
                try
                {
                    rbxMultiMutex = new Mutex(true, "ROBLOX_singletonMutex");

                    if (!rbxMultiMutex.WaitOne(TimeSpan.Zero, true))
                        return false;
                }
                catch { return false; }
            else if (!Enabled && rbxMultiMutex != null)
            {
                rbxMultiMutex.Close();
                rbxMultiMutex = null;
            }

            return true;
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

        private async void Add_Click(object sender, EventArgs e)
        {
            if (PuppeteerSupported)
            {
                Add.Enabled = false;

                try { await new AccountBrowser().Login(); }
                catch (Exception x)
                {
                    Program.Logger.Error($"[Add_Click] An error was encountered attempting to login: {x}");

                    if (Utilities.YesNoPrompt($"An error was encountered attempting to login", "You may have a corrupted chromium installation", "Would you like to re-install chromium?", false))
                    {
                        MessageBox.Show("Roblox Account Manager will now close since it can't delete the folder while it's in use.", "", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        if (Directory.GetFiles(AccountBrowser.Fetcher.DownloadsFolder).Length <= 1 && Directory.GetDirectories(AccountBrowser.Fetcher.DownloadsFolder).Length <= 1)
                            Process.Start("cmd.exe", $"/c rmdir /s /q \"{AccountBrowser.Fetcher.DownloadsFolder}\"");
                        else
                            Process.Start("explorer.exe", "/select, " + AccountBrowser.Fetcher.DownloadsFolder);

                        Environment.Exit(0);
                    }
                }

                Add.Enabled = true;
            }
            else
                CefBrowser.Instance.Login();
        }

        private void DownloadProgressBar_Click(object sender, EventArgs e)
        {
            static void ShowManualInstallInstructions()
            {
                string Temp = Path.Combine(Path.GetTempPath(), "manual install instructions.html");

                string DownloadLink = PuppeteerSupported ? (string)typeof(BrowserFetcher).GetMethod("GetDownloadURL", BindingFlags.Static | BindingFlags.NonPublic).Invoke(null, new object[] { AccountBrowser.Fetcher.Product, AccountBrowser.Fetcher.Platform, AccountBrowser.Fetcher.DownloadHost, BrowserFetcher.DefaultChromiumRevision }) : Resources.CefSharpDownload;
                string Directory = PuppeteerSupported ? Path.Combine(AccountBrowser.Fetcher.DownloadsFolder, $"{AccountBrowser.Fetcher.Platform}-{BrowserFetcher.DefaultChromiumRevision}") : Path.Combine(Environment.CurrentDirectory);

                File.WriteAllText(Temp, string.Format(Resources.ManualInstallHTML, PuppeteerSupported ? "Chromium" : "CefSharp", DownloadLink, PuppeteerSupported ? "chrome-win" : "x86", Directory));

                Process.Start(new ProcessStartInfo(Temp) { UseShellExecute = true });
                Process.Start(new ProcessStartInfo("cmd") { Arguments = $"/c mkdir \"{Directory}\"", CreateNoWindow = true });
            }

            if (TaskDialog.IsPlatformSupported)
            {
                TaskDialog Dialog = new TaskDialog()
                {
                    Caption = "Add Account",
                    InstructionText = $"{(PuppeteerSupported ? "Chromium" : "CefSharp")} is still being downloaded",
                    Text = "If this is not working for you, you can choose to manually install",
                    Icon = TaskDialogStandardIcon.Information
                };

                TaskDialogButton Manual = new TaskDialogButton("Manual", "Download Manually");
                TaskDialogButton Wait = new TaskDialogButton("Wait", "Wait");

                Wait.Click += (s, e) => Dialog.Close();
                Manual.Click += (s, e) =>
                {
                    Dialog.Close();

                    ShowManualInstallInstructions();
                };

                Dialog.Controls.Add(Manual);
                Dialog.Controls.Add(Wait);
                Wait.Default = true;

                Dialog.Show();
            }
            else if (MessageBox.Show($"{(PuppeteerSupported ? "Chromium" : "CefSharp")} is still downloading, you may have to wait a while before adding an account.\n\nNot working? You can choose to manually install by pressing \"Yes\"", "Roblox Account Manager", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Information) == DialogResult.Yes)
                ShowManualInstallInstructions();
        }

        private void DLChromiumLabel_Click(object sender, EventArgs e) => DownloadProgressBar_Click(sender, e);

        private void manualToolStripMenuItem_Click(object sender, EventArgs e) => Add.PerformClick();

        private void addAccountsToolStripMenuItem_Click(object sender, EventArgs e) => Add.PerformClick();

        private void byCookieToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ImportAccountsForm.Show();
            ImportAccountsForm.WindowState = FormWindowState.Normal;
            ImportAccountsForm.BringToFront();
        }

        private async void bulkUserPassToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string Combos = ShowDialog("Separate the accounts with new lines\nMust be in user:pass form", "Import by User:Pass", big: true);

            if (Combos == "/UC") return;

            List<string> ComboList = new List<string>(Combos.Split('\n'));

            var Size = new System.Numerics.Vector2(455, 485);
            AccountBrowser.CreateGrid(Size);

            for (int i = 0; i < ComboList.Count; i++)
            {
                string Combo = ComboList[i];

                if (!Combo.Contains(':')) continue;

                var LoginTask = new AccountBrowser() { Index = i, Size = Size }.Login(Combo.Substring(0, Combo.IndexOf(':')), Combo.Substring(Combo.IndexOf(":") + 1));

                if ((i + 1) % 2 == 0) await LoginTask;
            }
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
            if (!GetUserID(UserID.Text, out long UserId, out var Response))
            {
                MessageBox.Show($"[{Response.StatusCode} {Response.StatusDescription}] Failed to get UserId: {Response.Content}");
                return;
            }
    
            if (!(await Presence.GetPresenceSingular(UserId) is UserPresence Status && Status.userPresenceType == UserPresenceType.InGame && Status.placeId is long FollowPlaceID && FollowPlaceID > 0) &&
                !Utilities.YesNoPrompt("Warning", "The user you are trying to follow is not in game or has their joins off", "Do you want to attempt to join anyways?")) return;

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
            if (IsDownloadingChromium && !Utilities.YesNoPrompt("Roblox Account Manager", $"{(PuppeteerSupported ? "Chromium" : "CefSharp")} is still being downloaded, exiting may corrupt your chromium installation and prevent account manager from working", "Exit anyways?", false))
            {
                e.Cancel = true;

                return;
            }

            AltManagerWS?.Stop();

            if (PlaceID == null || string.IsNullOrEmpty(PlaceID.Text)) return;

            General.Set("SavedPlaceId", PlaceID.Text);
            General.Set("SavedFollowUser", UserID.Text);
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
            if (afform != null)
                if (afform.Visible)
                    afform.HideForm();
                else
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

            RestRequest request = new RestRequest($"v2/assets/{PlaceID.Text}/details", Method.Get);
            request.AddHeader("Accept", "application/json");
            RestResponse response = await EconClient.ExecuteAsync(request);

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

            if (GroupName == "/UC") return; // User Cancelled
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

        private void OpenBrowser_Click(object sender, EventArgs e)
        {
            if (PuppeteerSupported)
                foreach (Account account in AccountsView.SelectedObjects)
                    new AccountBrowser(account);
            else if (!PuppeteerSupported && SelectedAccount != null)
                CefBrowser.Instance.EnterBrowserMode(SelectedAccount);
        }

        private void customURLToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Uri.TryCreate(ShowDialog("URL", "Open Browser"), UriKind.Absolute, out Uri Link))
                if (PuppeteerSupported)
                    foreach (Account account in AccountsView.SelectedObjects)
                        new AccountBrowser(account, Link.ToString(), string.Empty);
                else if (!PuppeteerSupported && SelectedAccount != null)
                    CefBrowser.Instance.EnterBrowserMode(SelectedAccount, Link.ToString());
        }

        private void URLJSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Utilities.YesNoPrompt("Warning", "Your accounts may be at risk using this feature", "Do not paste in javascript unless you know what it does, your account's cookies can easily be logged through javascript.\n\nPress Yes to continue", true)) return;

            if (Uri.TryCreate(ShowDialog("URL", "Open Browser"), UriKind.Absolute, out Uri Link))
            {
                string Script = ShowDialog("Javascript", "Open Browser", big: true);

                foreach (Account account in AccountsView.SelectedObjects)
                    new AccountBrowser(account, Link.ToString(), Script);
            }
        }

        private void joinGroupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Uri.TryCreate(ShowDialog("Group Link", "Open Browser"), UriKind.Absolute, out Uri Link))
            {
                foreach (Account account in AccountsView.SelectedObjects)
                    new AccountBrowser(account, Link.ToString(), PostNavigation: async (page) =>
                    {
                        await (await page.WaitForSelectorAsync("#group-join-button", new WaitForSelectorOptions() { Timeout = 12000 })).ClickAsync();
                    });
            }
        }

        private void customURLJSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int Count = 1;

            if (ModifierKeys == Keys.Shift)
                int.TryParse(ShowDialog("Amount (Limited to 15)", "Launch Browser", "1"), out Count);

            if (Uri.TryCreate(ShowDialog("URL", "Launch Browser", "https://roblox.com/"), UriKind.Absolute, out Uri Link))
            {
                string Script = ShowDialog("Javascript", "Launch Browser", big: true);

                var Size = new System.Numerics.Vector2(550, 440);
                AccountBrowser.CreateGrid(Size);

                for (int i = 0; i < Math.Min(Count, 15); i++) {
                    var Browser = new AccountBrowser() { Size = Size, Index = i };

                    _ = Browser.LaunchBrowser(Url: Link.ToString(), Script: Script, PostNavigation: async (p) => await Browser.LoginTask(p));
                }
            }
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
            if (ModifierKeys == Keys.Shift)
            {
                List<Account> HasSaved = new List<Account>();

                foreach (Account account in AccountsList)
                    if (account.Fields.ContainsKey("SavedPlaceId") || account.Fields.ContainsKey("SavedJobId"))
                        HasSaved.Add(account);

                if (HasSaved.Count > 0 && MessageBox.Show($"Are you sure you want to remove {HasSaved.Count} saved Place Ids?", "Roblox Account Manager", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.OK)
                    foreach (Account account in HasSaved)
                    {
                        account.RemoveField("SavedPlaceId");
                        account.RemoveField("SavedJobId");
                    }
            }

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
                AccountsView.BuildGroups();
            }
        }

        private async void quickLogInToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedAccount == null) return;

            if (!Utilities.YesNoPrompt("Quick Log In", "Only enter codes that you requested\nNever enter another user's code", $"Do you understand?", SaveIfNo: false))
                return;

            if (Clipboard.ContainsText() && Clipboard.GetText() is string ClipCode && ClipCode.Length == 6 && await SelectedAccount.QuickLogIn(ClipCode))
                return;

            string Code = ShowDialog("Code", "Quick Log In");

            if (Code.Length != 6) { MessageBox.Show("Quick Log In codes requires 6 characters", "Quick Log In", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

            await SelectedAccount.QuickLogIn(Code);
        }

        private void toggleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AccountsView.ShowGroups = !AccountsView.ShowGroups;

            if (AccountsView.HeaderStyle != ColumnHeaderStyle.None) AccountsView.HeaderStyle = AccountsView.ShowGroups ? ColumnHeaderStyle.Nonclickable : ColumnHeaderStyle.Clickable;

            AccountsView.BuildGroups();
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

        CancellationTokenSource PresenceCancellationToken;

        private void AccountsView_Scroll(object sender, ScrollEventArgs e)
        {
            if (PresenceCancellationToken != null || !General.Get<bool>("ShowPresence"))
                PresenceCancellationToken.Cancel();

            PresenceCancellationToken = new CancellationTokenSource();
            var Token = PresenceCancellationToken.Token;

            Task.Run(async () =>
            {
                await Task.Delay(3500); // Wait until the user has stopped scrolling before updating account presence

                if (Token.IsCancellationRequested)
                    return;

                AccountsView.InvokeIfRequired(async () => await UpdatePresence());
            }, PresenceCancellationToken.Token);
        }

        private async Task UpdatePresence()
        {
            if (!General.Get<bool>("ShowPresence")) return;

            List<Account> VisibleAccounts = new List<Account>();

            var Bounds = AccountsView.ClientRectangle;
            int Padding = (int)(AccountsView.HeaderStyle == ColumnHeaderStyle.None ? 4f * Program.Scale : 20f * Program.Scale);

            for (int Y = Padding; Y < Bounds.Height - (Padding / 2); Y += (int)(6f * Program.Scale))
            {
                var Item = AccountsView.GetItemAt(4, Y);

                if (Item != null && AccountsView.GetModelObject(Item.Index) is Account account && !VisibleAccounts.Contains(account))
                    VisibleAccounts.Add(account);
            }

            try { await Presence.UpdatePresence(VisibleAccounts.Select(account => account.UserID).ToArray()); } catch { }
        }

        private void JobID_Click( object sender, EventArgs e )
        {
            JobID.SelectAll(); // Allows quick replacing of the JobID with a click and ctrl-v.
        }

        private void PlaceID_Click( object sender, EventArgs e )
        {
            PlaceID.SelectAll(); // Allows quick replacing of the PlaceID with a click and ctrl-v.
        }
    }
}
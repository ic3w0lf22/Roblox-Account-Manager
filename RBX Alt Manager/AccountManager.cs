using Microsoft.Win32;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RBX_Alt_Manager
{
    public partial class AccountManager : Form
    {
        public static List<RbxProcess> RbxProcesses = new List<RbxProcess>();
        public static List<Account> AccountsList = new List<Account>();
        public static Account SelectedAccount;
        public static string CurrentVersion;
        public static RestClient apiclient;
        public static RestClient client;
        public static RestClient econclient;
        private AccountAdder aaform;
        private ServerList ServerListForm;
        private static DateTime startTime = DateTime.Now;
        public ListViewItem SelectedAccountItem;
        // public Account SelectedAccount;
        private ListViewItem LastViewItem;
        private string RbxJoinPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "rbx-join.exe");
        private string CommandValue;
        private RegistryKey ManagerKey;
        private string ManagerKeyName = "RbxAccountManager";

        private delegate void SafeCallDelegateAccount(Account account);
        private delegate void SafeCallDelegateRemoveAt(int Index);
        private delegate int SafeCallDelegateInvite(object Item);

        public AccountManager()
        {
            InitializeComponent();
        }

        private static string SaveFilePath = Path.Combine(Environment.CurrentDirectory, "AccountData.json");

        private void LoadAccounts()
        {
            if (File.Exists(SaveFilePath))
            {
                try
                {
                    AccountsList = JsonConvert.DeserializeObject<List<Account>>(File.ReadAllText(SaveFilePath));

                    foreach (Account acc in AccountsList)
                        AddAccountToList(acc);
                }
                catch
                {
                    File.WriteAllText(SaveFilePath + ".bak", File.ReadAllText(SaveFilePath));
                }
            }
        }

        public static void SaveAccounts()
        {
            if ((DateTime.Now - startTime).Seconds < 5 || AccountsList.Count == 0) return;

            string OldInfo = File.Exists(SaveFilePath) ? File.ReadAllText(SaveFilePath) : "";
            string SaveData = JsonConvert.SerializeObject(AccountsList);
            int OldSize = Encoding.Unicode.GetByteCount(OldInfo);
            int NewSize = Encoding.Unicode.GetByteCount(SaveData);
            FileInfo OldFile = new FileInfo(SaveFilePath);

            if (OldFile.Exists && NewSize < OldSize || (OldFile.Exists && (DateTime.Now - OldFile.LastWriteTime).TotalHours > 36))
                File.WriteAllText(SaveFilePath + ".backup", OldInfo);

            File.WriteAllText(SaveFilePath, SaveData);
        }

        public static int GetUserID(string Username)
        {
            RestRequest request = new RestRequest("users/get-by-username?username=" + Username, Method.GET);
            request.AddHeader("Accept", "application/json");
            IRestResponse response = response = apiclient.Execute(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                UsernameReponse userData = JsonConvert.DeserializeObject<UsernameReponse>(response.Content);

                return userData.Id;
            }

            return -1;
        }

        public void AddAccountToList(Account account)
        {
            string Name = account.Username + " " + account.Alias;

            if (AccountsView.InvokeRequired)
            {
                var addItem = new SafeCallDelegateAccount(AddAccountToList);
                AccountsView.Invoke(addItem, new object[] { account });
            }
            else
                AccountsView.Items.Add(new ListViewItem(new string[] { account.Username, account.Alias, account.Description.Replace("\n", ""), account.Username }));
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
                    AccountsList.Add(account);

                Program.MainForm.AddAccountToList(account);
                SaveAccounts();
            }
            else MessageBox.Show(res);
        }

        private void AccountManager_Load(object sender, EventArgs e)
        {
            CommandValue = RbxJoinPath + " \"%1\"";

            if (!File.Exists(RbxJoinPath))
            {
                MessageBox.Show("Failed to find rbx-join executable", "Roblox Account Manager", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                Close();
            }

            RegistryKey MainKey = Registry.ClassesRoot.OpenSubKey("rbx-join");
            RegistryKey CommandKey = Registry.ClassesRoot.OpenSubKey(@"rbx-join\shell\open\command");

            if (MainKey == null || CommandKey == null || (string)CommandKey.GetValue("") != CommandValue)
                MessageBox.Show("The rbx-join protocol is not setup, run RegisterRbxJoinProtocol.exe to set it up", "rbx-join", MessageBoxButtons.OK, MessageBoxIcon.Information);

            RegistryKey HandleKey = Registry.CurrentUser.OpenSubKey(@"Software\Sysinternals\Handle");

            if (HandleKey == null || HandleKey.GetValue("EulaAccepted") == null || (int)HandleKey.GetValue("EulaAccepted") != 1)
                Process.Start("handle.exe");

            ManagerKey = Registry.CurrentUser.OpenSubKey(ManagerKeyName, RegistryKeyPermissionCheck.ReadWriteSubTree);

            if (ManagerKey == null)
                ManagerKey = Registry.CurrentUser.CreateSubKey(ManagerKeyName, RegistryKeyPermissionCheck.ReadWriteSubTree);

            PlaceID.Text = ManagerKey.GetValue("SavedPlaceId", 3016661674).ToString();

            SetupNamedPipe();

            aaform = new AccountAdder();
            ServerListForm = new ServerList();

            AccountsView.Items.Clear();

            RobloxProcessTimer.Start();

            apiclient = new RestClient("https://api.roblox.com/");
            apiclient.CachePolicy = new System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.BypassCache);

            client = new RestClient("https://auth.roblox.com/");
            client.CachePolicy = new System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.BypassCache);

            econclient = new RestClient("https://economy.roblox.com/");
            econclient.CachePolicy = new System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.BypassCache);

            WebClient WC = new WebClient();
            string VersionJSON = WC.DownloadString("https://clientsettings.roblox.com/v1/client-version/WindowsPlayer");
            JObject j = JObject.Parse(VersionJSON);
            if (j.TryGetValue("clientVersionUpload", out JToken token))
                CurrentVersion = token.Value<string>();

            Task.Run(() =>
            {
                try
                {
                    Assembly assembly = Assembly.GetExecutingAssembly();
                    FileVersionInfo fvi = FileVersionInfo.GetVersionInfo(assembly.Location);
                    string version = fvi.FileVersion.Substring(0, 3);
                    WC.Headers["User-Agent"] = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/81.0.4044.138 Safari/537.36";
                    string Releases = WC.DownloadString("https://api.github.com/repos/ic3w0lf22/Roblox-Account-Manager/releases/latest");
                    Match match = Regex.Match(Releases, @"""tag_name"":\s*""?([^""]+)");
                    if (match.Success && match.Groups[1].Value != version)
                    {
                        DialogResult result = MessageBox.Show("An update is available, click ok to be redirected to the download page", "Roblox Account Manager", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);

                        if (result == DialogResult.OK)
                            Process.Start("https://github.com/ic3w0lf22/Roblox-Account-Manager/releases");
                    }
                }
                catch { }
            });
        }

        private void AccountManager_Shown(object sender, EventArgs e)
        {
            LoadAccounts();
        }

        private void Remove_Click(object sender, EventArgs e)
        {
            if (SelectedAccount == null) return;

            DialogResult result = MessageBox.Show("Are you sure?", "Remove", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                AccountsView.Items.Remove(SelectedAccountItem);

                AccountsList.RemoveAll(x => x == SelectedAccount);
                SaveAccounts();
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
            if (LastViewItem != null)
            {
                LastViewItem.ForeColor = Color.Black;
                LastViewItem.BackColor = Color.White;
            }

            if (AccountsView.SelectedItems.Count != 1)
            {
                SelectedAccountItem = null;
                return;
            }

            SelectedAccountItem = AccountsView.SelectedItems[0];
            SelectedAccountItem.ForeColor = Color.White;
            SelectedAccountItem.BackColor = Color.DodgerBlue;
            LastViewItem = SelectedAccountItem;

            AccountsView.HideSelection = true;

            try
            {
                string Item = SelectedAccountItem.SubItems[3].Text;
                Account account = AccountsList.FirstOrDefault(x => Item.Length >= x.Username.Length && x.Username == Item.Substring(0, x.Username.Length));

                if (account != null)
                {
                    SelectedAccount = account;
                    Alias.Text = account.Alias;
                    DescriptionBox.Text = account.Description;
                }
            }
            catch { }
        }

        private void SetAlias_Click(object sender, EventArgs e)
        {
            if (SelectedAccount == null) return;

            SelectedAccount.Alias = Alias.Text;
            SelectedAccountItem.SubItems[3].Text = SelectedAccount.Username;
            SelectedAccountItem.SubItems[1].Text = SelectedAccount.Alias;
        }

        private void SetDescription_Click(object sender, EventArgs e)
        {
            if (SelectedAccount == null) return;

            SelectedAccount.Description = DescriptionBox.Text;
            SelectedAccountItem.SubItems[2].Text = SelectedAccount.Description.Replace("\n", "");
        }

        private void JoinServer_Click(object sender, EventArgs e)
        {
            if (SelectedAccount == null) return;

            bool VIPServer = JobID.TextLength > 4 ? JobID.Text.Substring(0, 4) == "VIP:" : false;
            Console.WriteLine(VIPServer);
            string res = SelectedAccount.JoinServer(Convert.ToInt64(PlaceID.Text), VIPServer ? JobID.Text.Substring(4) : JobID.Text, false, VIPServer);

            if (!res.Contains("Success"))
                MessageBox.Show(res);
        }

        private void Follow_Click(object sender, EventArgs e)
        {
            if (SelectedAccount == null) return;

            int UserId = GetUserID(UserID.Text);

            if (UserId < 0)
            {
                MessageBox.Show("Failed to get UserId");
                return;
            }

            string res = SelectedAccount.JoinServer(UserId, "", true);

            if (!res.Contains("Success"))
                MessageBox.Show(res);
        }

        private void PlaceID_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void OpenBrowser_Click(object sender, EventArgs e)
        {
            if (SelectedAccount == null) return;

            RestRequest tokenrequest = new RestRequest("v1/authentication-ticket/", Method.POST);

            tokenrequest.AddCookie(".ROBLOSECURITY", SelectedAccount.SecurityToken);
            tokenrequest.AddHeader("Referer", "https://www.roblox.com/games/171336322/testing");

            IRestResponse response = AccountManager.client.Execute(tokenrequest);
            Parameter result = response.Headers.FirstOrDefault(x => x.Name == "x-csrf-token");

            string Token = "";

            if (result != null)
                Token = (string)result.Value;
            else
                return;

            if (string.IsNullOrEmpty(Token) || result == null)
                return;

            RestRequest request = new RestRequest("v1/purchases/products/502391436", Method.POST);
            request.AddCookie(".ROBLOSECURITY", SelectedAccount.SecurityToken);
            request.AddHeader("X-CSRF-TOKEN", Token);
            request.AddHeader("Accept", "application/json");
            request.AddParameter("application/json", "{\"expectedCurrency\":1,\"expectedPrice\":350,\"expectedSellerId\":875316944}", ParameterType.RequestBody);
            request.AddHeader("Referer", "https://www.roblox.com/games/171336322/testing");
            response = econclient.Execute(request);
            MessageBox.Show(response.Content);
        }

        private void ServerList_Click(object sender, EventArgs e)
        {
            if (ServerListForm.Visible)
                ServerListForm.BringToFront();
            else
                ServerListForm.Show();

            ServerListForm.StartPosition = FormStartPosition.Manual;
            ServerListForm.Top = this.Top;
            ServerListForm.Left = this.Right;
        }

        private void HideUsernamesCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            foreach (ListViewItem Item in AccountsView.Items)
            {
                Account account = AccountsList.FirstOrDefault(x => Item.SubItems[3].Text.Length >= x.Username.Length && x.Username == Item.SubItems[3].Text.Substring(0, x.Username.Length));
                Item.Text = HideUsernamesCheckbox.Checked ? Regex.Replace(account.Username, ".", "*") : account.Username;
            }
        }

        private void addAccountsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (aaform != null && aaform.Visible)
                aaform.HideForm();

            aaform.ShowForm();
        }

        private void removeAccountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SelectedAccount == null) return;

            DialogResult result = MessageBox.Show("Are you sure?", "Remove", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                AccountsView.Items.Remove(SelectedAccountItem);

                AccountsList.RemoveAll(x => x == SelectedAccount);
                SaveAccounts();
            }
        }

        private void infoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Roblox Account Manager created by ic3w0lf under the GNU GPLv3 license.", "Roblox Account Manager", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private int AddInviteLinkItem(object Item)
        {
            if (Item == null || string.IsNullOrEmpty((string)Item)) return -1;

            if (InviteLinks.InvokeRequired)
            {
                SafeCallDelegateInvite addItem = new SafeCallDelegateInvite(AddInviteLinkItem);
                return (int)InviteLinks.Invoke(addItem, new object[] { Item });
            }
            else
                return InviteLinks.Items.Add(Item);
        }

        private void RemoveInviteLinkItem(int Index)
        {
            if (InviteLinks.InvokeRequired)
            {
                SafeCallDelegateRemoveAt removeAt = new SafeCallDelegateRemoveAt(RemoveInviteLinkItem);
                InviteLinks.Invoke(removeAt, new object[] { Index });
            }
            else
                InviteLinks.Items.RemoveAt(Index);
        }

        // some code might be scuffed cuz visual studio deleted 2 days worth of progress and got it off of dnspy

        private void RobloxProcessTimer_Tick(object sender, EventArgs e)
        {
            List<RbxProcess> Processes = RbxProcesses.ToList();

            foreach (Process p in Process.GetProcessesByName("RobloxPlayerBeta"))
            {
                if (!Processes.Any(x => x.RobloxProcess.MainWindowHandle == p.MainWindowHandle))
                {
                    RbxProcess rbx = new RbxProcess(p);
                    RbxProcesses.Add(rbx);

                    Task.Run(() =>
                    {
                        Task.Delay(1000);
                        DateTime setupTime = DateTime.Now;
                        rbx.Setup();

                        do
                            Task.Delay(500);
                        while (string.IsNullOrEmpty(rbx.UserName) && (DateTime.Now - setupTime).TotalSeconds < 5.0);

                        if (rbx.Working && p.Responding)
                        {
                            p.EnableRaisingEvents = true;
                            p.Exited += new EventHandler(RobloxProcess_Exited);
                            rbx.Index = AddInviteLinkItem(rbx.UserName);
                        }
                        else
                            RbxProcesses.Remove(rbx);
                    });
                }
            }
        }

        private void RobloxProcess_Exited(object sender, EventArgs e)
        {
            Process Exited = (Process)sender;

            RbxProcess process = RbxProcesses.FirstOrDefault(x => x.RobloxProcess == Exited);

            if (process == null) return;

            RbxProcesses.Remove(process);
            Console.WriteLine(process.Index);

            for (int Index = 0; Index < InviteLinks.Items.Count; Index++)
            {
                if ((string)InviteLinks.Items[Index] == process.UserName)
                {
                    RemoveInviteLinkItem(Index);
                    break;
                }
            }
        }

        private void SetupNamedPipe()
        {
            Task.Factory.StartNew(() =>
            {
                NamedPipeServerStream pipe = new NamedPipeServerStream("AccountManagerPipe", PipeDirection.InOut, -1, PipeTransmissionMode.Message);

                while (true)
                {
                    pipe.WaitForConnection();
                    byte[] messageBytes = ReadMessage(pipe);
                    string line = Encoding.UTF8.GetString(messageBytes);

                    if (string.IsNullOrEmpty(line)) continue;

                    if (line == "acclist")
                    {
                        string AccountString = HideUsernamesCheckbox.Checked ? "hidden\n" : "";

                        foreach (Account acc in AccountsList)
                            AccountString = AccountString + acc.Username + "::" + acc.Alias + "\n";

                        if (AccountString.Length > 1)
                            AccountString = AccountString.Substring(0, AccountString.Length - 1);

                        byte[] response = Encoding.UTF8.GetBytes(AccountString);
                        pipe.Write(response, 0, response.Length);
                    }
                    else if (line.Substring(0, 4) == "play")
                    {
#if DEBUG
                        Console.WriteLine(line);
#endif
                        Match match = Regex.Match(line, "play\\-(\\w+)\\-(\\d+)\\-?(\\w+-\\w+-\\w+-\\w+-\\w+)?");

                        if (match.Success)
                        {
                            string AccountName = match.Groups[1].Value;
                            long PlaceId = Convert.ToInt64(match.Groups[2].Value);
                            string JobId = (match.Groups.Count == 4) ? match.Groups[3].Value : "";

                            Account account = AccountsList.FirstOrDefault((Account x) => AccountName.Length >= x.Username.Length && x.Username == AccountName.Substring(0, x.Username.Length));
                            account.JoinServer(PlaceId, JobId, false);
                        }
                    }

                    pipe.Disconnect();
                }
            });
        }

        private static byte[] ReadMessage(PipeStream pipe)
        {
            byte[] buffer = new byte[1024];
            byte[] result;
            using (MemoryStream ms = new MemoryStream())
            {
                do
                {
                    int readBytes = pipe.Read(buffer, 0, buffer.Length);
                    ms.Write(buffer, 0, readBytes);
                }
                while (!pipe.IsMessageComplete);
                result = ms.ToArray();
            }
            return result;
        }

        private void InviteLinks_TextUpdate(object sender, EventArgs e)
        {
            InviteLinks.Text = "Copy Invite Link";
        }

        private void InviteLinks_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (InviteLinks.SelectedIndex < 0 || InviteLinks.SelectedItem == null) return;

            RbxProcess rbx = RbxProcesses.FirstOrDefault(x => x.UserName == (string)InviteLinks.SelectedItem);

            if (rbx != null)
            {
                Clipboard.SetText(string.Format("<rbx-join://{0}/{1}>", rbx.PlaceId, rbx.JobId));
                MessageBox.Show("Copied to Clipboard!");
            }
        }

        private void AccountManager_FormClosed(object sender, FormClosedEventArgs e)
        {
            ManagerKey.SetValue("SavedPlaceId", PlaceID.Text);
        }

        private void BrowserButton_Click(object sender, EventArgs e)
        {
            if (SelectedAccount == null) return;

            if (aaform != null && aaform.Visible)
                aaform.HideForm();

            aaform.ShowForm();
            aaform.BrowserMode = true;
            CefSharp.Cookie ck = new CefSharp.Cookie();
            ck.Name = ".ROBLOSECURITY";
            ck.Value = SelectedAccount.SecurityToken;
            CefSharp.Cef.GetGlobalCookieManager().SetCookie("https://www.roblox.com", ck);
            aaform.chromeBrowser.Load("https://www.roblox.com/home");
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
    }
}
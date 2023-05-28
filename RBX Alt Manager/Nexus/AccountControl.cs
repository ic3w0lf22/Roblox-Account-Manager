using BrightIdeasSoftware;
using Newtonsoft.Json;
using RBX_Alt_Manager.Classes;
using RBX_Alt_Manager.Nexus;
using RBX_Alt_Manager.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebSocketSharp;
using WebSocketSharp.Net.WebSockets;
using WebSocketSharp.Server;

//this.ScriptTabs = ThemeEditor.UseNormalTabControls ? new System.Windows.Forms.TabControl() : new RBX_Alt_Manager.Classes.NBTabControl();
//this.ACTabs = ThemeEditor.UseNormalTabControls ? new System.Windows.Forms.TabControl() : new RBX_Alt_Manager.Classes.NBTabControl();

namespace RBX_Alt_Manager.Forms
{
    public partial class AccountControl : Form
    {
        private static readonly string ACFile = Path.Combine(Environment.CurrentDirectory, "AccountControlData.json");
        private static readonly object SaveLock = new object();

        public static AccountControl Instance;
        public static WebSocketServer Server;

        public Dictionary<WebSocketContext, ControlledAccount> ContextList = new Dictionary<WebSocketContext, ControlledAccount>();
        public List<ControlledAccount> Accounts = new List<ControlledAccount>();

        private readonly Dictionary<string, Control> CustomElements = new Dictionary<string, Control>();
        private Control LastControl;
        private StreamWriter OutputWriter;

        private bool SettingsLoaded;

        [DllImport("user32", EntryPoint = "SendMessageA", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        private static extern int SendMessage(int hwnd, int wMsg, int wParam, int lParam);

        public AccountControl()
        {
            Instance = this;

            AccountManager.SetDarkBar(Handle);

            InitializeComponent();
            this.Rescale();
        }

        #region Save File

        private void SaveAccounts()
        {
            lock (SaveLock)
                File.WriteAllText(ACFile, JsonConvert.SerializeObject(Accounts));
        }

        private void LoadAccounts()
        {
            if (File.Exists(ACFile))
            {
                try
                {
                    string JSON = File.ReadAllText(ACFile);

                    if (string.IsNullOrEmpty(JSON)) return;

                    Accounts = JsonConvert.DeserializeObject<List<ControlledAccount>>(JSON);

                    foreach (ControlledAccount cAccount in Accounts)
                    {
                        Account account = AccountManager.AccountsList.FirstOrDefault(x => x.Username == cAccount.Username);

                        if (account != null)
                            cAccount.LinkedAccount = account;
                    }

                    Accounts.RemoveAll(x => x.LinkedAccount == null);
                }
                catch { }
            }

            AccountsView.SetObjects(Accounts);
        }

        #endregion

        #region Server

        private void OpenServer()
        {
            if (!int.TryParse(AccountManager.AccountControl.Get("NexusPort"), out int Port))
                throw new Exception("Failed to start server, invalid Port setting");

            if (Port < 1 || Port > 65535)
                throw new Exception("Port can not be less than 1 or more than 65535");

            Server = new WebSocketServer(AccountManager.AccountControl.Get<bool>("AllowExternalConnections") ? IPAddress.Any : IPAddress.Loopback, Port, false);

#if DEBUG
            Server.Log.Level = LogLevel.Debug;
#endif

            Server.AddWebSocketService<WebsocketServer>("/Nexus");

            Server.Start();

            Process.GetCurrentProcess().WaitForExit();
        }

        public void EmitMessage(string Message, bool ToAll = false)
        {
            foreach (ControlledAccount account in ToAll ? AccountsView.Objects : AccountsView.CheckedObjects)
                account.SendMessage(Message);
        }

        #endregion

        #region Custom Controls

        public void LogMessage(string Message)
        {
            if (SaveOutputToFileCheck.Checked)
            {
                if (!Directory.Exists("Output"))
                    Directory.CreateDirectory("Output");

                if (OutputWriter == null)
                {
                    OutputWriter = File.AppendText("Output/Output_" + DateTime.Now.ToString("MM dd.hh mm") + ".txt");
                    OutputWriter.AutoFlush = true;
                }

                OutputWriter.WriteLine(Message);
            }

            TextBox l = new TextBox
            {
                Text = Message,
                ReadOnly = true,
                Multiline = true,
                WordWrap = true,
                TabStop = false,
                BorderStyle = 0,

                Margin = new Padding(1),

                MinimumSize = new Size(240, 13),
                MaximumSize = new Size(240, 120),
                BackColor = ThemeEditor.AccountBackground,
                ForeColor = ThemeEditor.FormsForeground
            };

            int numberOfLines = SendMessage(l.Handle.ToInt32(), 0xBA, 0, 0);
            l.Height = ((l.Font.Height + 2) * numberOfLines) - 6;

            bool ScrollToBottom = OutputPanel.VerticalScroll.Value - (OutputPanel.VerticalScroll.Maximum - OutputPanel.Height + 20) > -20;

            OutputPanel.Controls.Add(l);

            if (ScrollToBottom) OutputPanel.ScrollControlIntoView(l);
        }

        public void AddCustomButton(string Name, string Text, Size size, Padding margin)
        {
            if (CustomElements.ContainsKey(Name))
                return;

            Button button = new Button
            {
                Name = Name,
                Size = size,
                Margin = margin,
                Text = Text,
                UseVisualStyleBackColor = true
            };

            button.Click += CustomButton_Click;

            LastControl = button;

            ControlsPanel.Controls.Add(button);

            CustomElements.Add(Name, button);

            ApplyTheme(ControlsPanel.Controls);
        }

        public void AddCustomTextBox(string Name, string Text, Size size, Padding margin)
        {
            if (CustomElements.ContainsKey(Name))
                return;

            TextBox textBox = new TextBox
            {
                Name = Name,
                Size = size,
                Margin = margin,
                Text = Text
            };

            LastControl = textBox;

            ControlsPanel.Controls.Add(textBox);

            CustomElements.Add(Name, textBox);

            ApplyTheme(ControlsPanel.Controls);
        }

        public void AddCustomNumericUpDown(string Name, decimal DefaultValue, int DecimalPlaces, decimal Increment, Size size, Padding margin)
        {
            if (CustomElements.ContainsKey(Name))
                return;

            NumericUpDown control = new NumericUpDown
            {
                DecimalPlaces = DecimalPlaces,
                Increment = Increment,
                Name = Name,
                Margin = margin,
                Size = size,
                Value = DefaultValue,
                Minimum = decimal.MinValue,
                Maximum = decimal.MaxValue
            };

            LastControl = control;

            ControlsPanel.Controls.Add(control);

            CustomElements.Add(Name, control);

            ApplyTheme(ControlsPanel.Controls);
        }

        public void AddCustomLabel(string Name, string Text, Padding margin)
        {
            if (CustomElements.ContainsKey(Name))
                return;

            Label label = new Label
            {
                Name = Name,
                Margin = margin,
                Text = Text
            };

            LastControl = label;

            ControlsPanel.Controls.Add(label);

            CustomElements.Add(Name, label);

            ApplyTheme(ControlsPanel.Controls);
        }

        public void NewLine()
        {
            if (LastControl != null)
                ControlsPanel.SetFlowBreak(LastControl, true);
        }

        public string GetTextFromElement(string Name)
        {
            if (CustomElements.TryGetValue(Name, out Control control))
                return control.Text;

            return string.Empty;
        }

        private void CustomButton_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;

            EmitMessage($"ButtonClicked:{btn.Name}");
        }

        #endregion

        #region Events

        private void AccountControl_Load(object sender, EventArgs e)
        {
            cStatus.AspectGetter = delegate (object row)
            {
                ControlledAccount acc = (ControlledAccount)row;

                return acc.Status;
            };
            cStatus.Renderer = new MappedImageRenderer(new object[] {
                AccountStatus.Online, Resources.online,
                AccountStatus.Offline, Resources.offline
            });

            Task Listener = new Task(new Action(OpenServer));
            Listener.Start();

            try { Listener.Wait(50); }
            catch (InvalidOperationException x)
            {
                MessageBox.Show($"{x.Message} {x.StackTrace}", "Account Control", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Hide();
            }

            SaveOutputToFileCheck.Checked = AccountManager.AccountControl.Get<bool>("SaveOutput");
            AllowExternalConnectionsCB.Checked = AccountManager.AccountControl.Get<bool>("AllowExternalConnections");
            StartOnLaunch.Checked = AccountManager.AccountControl.Get<bool>("StartOnLaunch");
            PortNumber.Value = AccountManager.AccountControl.Get<decimal>("NexusPort");
            RelaunchDelayNumber.Value = AccountManager.AccountControl.Get<decimal>("RelaunchDelay");
            LauncherDelayNumber.Value = AccountManager.AccountControl.Get<decimal>("LauncherDelayNumber");
            AutoMinimizeCB.Checked = AccountManager.AccountControl.Get<bool>("AutoMinimizeEnabled");
            AutoCloseCB.Checked = AccountManager.AccountControl.Get<bool>("AutoCloseEnabled");
            InternetCheckCB.Checked = AccountManager.AccountControl.Get<bool>("InternetCheck");
            UsePresenceCB.Checked = AccountManager.AccountControl.Get<bool>("UsePresence");
            AutoMinIntervalNum.Value = Math.Max(Math.Min(AccountManager.AccountControl.Get<decimal>("AutoMinimizeInterval"), AutoMinIntervalNum.Minimum), AutoMinIntervalNum.Maximum);
            AutoCloseIntervalNum.Value = Math.Max(Math.Min(AccountManager.AccountControl.Get<decimal>("AutoCloseInterval"), AutoCloseIntervalNum.Maximum), AutoCloseIntervalNum.Minimum);
            MaxInstancesNum.Value = Math.Max(Math.Min(AccountManager.AccountControl.Get<int>("MaxInstances"), MaxInstancesNum.Maximum), MaxInstancesNum.Minimum);
            AutoCloseType.SelectedIndex = AccountManager.AccountControl.Get<int>("AutoCloseType");

            SettingsLoaded = true;

            LoadAccounts();
        }

        private void AccountsView_DragOver(object sender, DragEventArgs e)
        {
            if (sender != null && sender is ObjectListView)
                e.Effect = DragDropEffects.Copy;
        }

        private void AccountsView_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.CommaSeparatedValue))
            {
                string Data = (string)e.Data.GetData(DataFormats.CommaSeparatedValue);

                foreach (string Line in Data.Split('\n'))
                {
                    string Username = string.Empty;

                    Match match = Regex.Match(Line, @"""(\w+)""");

                    if (match.Success)
                        Username = match.Groups[1].Value;

                    if (!string.IsNullOrEmpty(Username))
                    {
                        Account account = AccountManager.AccountsList.FirstOrDefault(x => x.Username == Username);

                        if (account != null && !Accounts.Exists(x => x.LinkedAccount == account))
                        {
                            ControlledAccount cAccount = new ControlledAccount(account);

                            Accounts.Add(cAccount);
                            AccountsView.AddObject(cAccount);

                            SaveAccounts();
                        }
                    }
                }
            }
        }

        private void ShowScriptBox_Click(object sender, EventArgs e) => ScriptLayoutPanel.Size = new Size(ScriptLayoutPanel.Width, ScriptLayoutPanel.Height > 20 ? 11 : 212);

        private void OutputToggle_Click(object sender, EventArgs e) => OutputFlowPanel.Size = new Size(OutputFlowPanel.Width, OutputFlowPanel.Height > 20 ? 11 : 160);

        private void SendCommand_Click(object sender, EventArgs e) => EmitMessage(CommandText.Text);

        private void ExecuteButton_Click(object sender, EventArgs e) => EmitMessage($"execute {ScriptBox.Text}");

        private void AutoMinimizeCB_CheckedChanged(object sender, EventArgs e)
        {
            MinimzeTimer.Enabled = AutoMinimizeCB.Checked;

            if (!SettingsLoaded) return;

            AccountManager.AccountControl.Set("AutoMinimizeEnabled", AutoMinimizeCB.Checked ? "true" : "false");
            AccountManager.IniSettings.Save("RAMSettings.ini");
        }

        private void AutoRelaunchCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (AutoRelaunchCheckBox.CheckState == CheckState.Indeterminate) return;

            foreach (ControlledAccount account in AccountsView.SelectedObjects)
                account.AutoRelaunch = AutoRelaunchCheckBox.Checked;

            SaveAccounts();
        }

        private void AccountsView_SelectionChanged(object sender, EventArgs e)
        {
            List<ControlledAccount> Objects = AccountsView.SelectedObjects.OfType<ControlledAccount>().ToList();

            if (Objects.Count == 0) return;

            ControlledAccount main = Objects[0];

            AutoRelaunchCheckBox.CheckState = Objects.Exists(x => x.AutoRelaunch != main.AutoRelaunch) ? CheckState.Indeterminate : (main.AutoRelaunch ? CheckState.Checked : CheckState.Unchecked);

            PlaceIdText.Text = Objects[0].PlaceId.ToString();
            JobIdText.Text = Objects[0].JobId;

            AutoExecuteScriptBox.Text = Objects[0].AutoExecute;
        }

        private void ClearButton_Click(object sender, EventArgs e) => ScriptBox.Clear();

        private void ClearOutputButton_Click(object sender, EventArgs e) => OutputPanel.Controls.Clear();

        private void SaveOutputToFileCheck_CheckedChanged(object sender, EventArgs e)
        {
            AccountManager.AccountControl.Set("SaveOutput", SaveOutputToFileCheck.Checked ? "true" : "false");
            AccountManager.IniSettings.Save("RAMSettings.ini");
        }

        private void ClearAutoExecScript_Click(object sender, EventArgs e) => AutoExecuteScriptBox.Clear();

        private void AutoExecuteScriptBox_Leave(object sender, EventArgs e)
        {
            if (AccountsView.SelectedObjects.Count == 0) return;

            if (AccountsView.SelectedObjects.Count == 1 || (AccountsView.SelectedObjects.Count > 1 && MessageBox.Show($"Are you sure you want to apply these edits to {AccountsView.SelectedObjects.Count} accounts?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes))
            {
                foreach (ControlledAccount account in AccountsView.SelectedObjects)
                    account.AutoExecute = AutoExecuteScriptBox.Text;
            }

            SaveAccounts();
        }

        private void PlaceIdText_Leave(object sender, EventArgs e)
        {
            if (long.TryParse(PlaceIdText.Text, out long PID))
                foreach (ControlledAccount account in AccountsView.SelectedObjects)
                    account.PlaceId = PID;

            SaveAccounts();
        }

        private void JobIdText_Leave(object sender, EventArgs e)
        {
            foreach (ControlledAccount account in AccountsView.SelectedObjects)
                account.JobId = JobIdText.Text;

            SaveAccounts();
        }

        private void CommandText_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Modifiers == Keys.None && e.KeyCode == Keys.Enter)
                EmitMessage(CommandText.Text);
        }

        private void CommandText_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
                e.Handled = true;
        }

        private void NexusDL_Click(object sender, EventArgs e)  
        {
            string path = Path.Combine(Environment.CurrentDirectory, "Nexus.lua");

            File.WriteAllText(path, Resources.NexusLoader);

            Process.Start("explorer.exe", "/select, " + path);
        }

        private void NexusDocsButton_Click(object sender, EventArgs e) => Process.Start("https://github.com/ic3w0lf22/Roblox-Account-Manager/blob/master/RBX%20Alt%20Manager/Nexus/NexusDocs.md");

        private async void AutoRelaunchTimer_Tick(object sender, EventArgs e)
        {
            if (!double.TryParse(AccountManager.AccountControl.Get("RelaunchDelay"), out double RelaunchDelay)) return;
            if (AccountManager.AccountControl.Get<bool>("InternetCheck") && !Utilities.IsConnectedToInternet()) return;

            try
            {
                bool UsePresence = AccountManager.AccountControl.Get<bool>("UsePresence");

                if (UsePresence) await Presence.UpdatePresence(Accounts.Select(a => a.LinkedAccount.UserID).ToArray());

                foreach (ControlledAccount account in Accounts)
                {
                    if (account.AutoRelaunch)
                    {
                        if ((UsePresence && account.LinkedAccount.Presence.userPresenceType != UserPresenceType.InGame) || (!UsePresence && (DateTime.Now - account.LastPing).TotalSeconds > account.RelaunchDelay))
                        {
                            Program.Logger.Info($"Relaunch Delay: {RelaunchDelay} | Current Time: {DateTime.Now}");
                            Program.Logger.Info($"Relaunching {account.Username} to {account.PlaceId}, time since last relaunch: {(DateTime.Now - account.LastPing).TotalSeconds} seconds [{account.LastPing}] | Linked: {account.LinkedAccount}");

                            account.LastPing = DateTime.Now;
                            account.RelaunchDelay = RelaunchDelay;

                            await account.LinkedAccount.JoinServer(account.PlaceId, account.JobId);

                            break;
                        }
                    }
                }

                ClearDeadProcesses();
            }
            catch (Exception x) { Program.Logger.Error($"An error occured launching an account from auto relaunch: {x.Message} Trace: {x.StackTrace}"); }
        }

        private void AccountControl_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Visible = false;
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure?", "Account Control", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                foreach (ControlledAccount acc in AccountsView.SelectedObjects)
                    Accounts.Remove(acc);

                AccountsView.SetObjects(Accounts);
                SaveAccounts();
            }
        }

        private void copyJobIdToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (AccountsView.SelectedObject != null)
                Clipboard.SetText(((ControlledAccount)AccountsView.SelectedObject).InGameJobId);
        }

        private void AllowExternalConnectionsCB_CheckedChanged(object sender, EventArgs e)
        {
            if (!SettingsLoaded) return;

            AccountManager.AccountControl.Set("AllowExternalConnections", AllowExternalConnectionsCB.Checked ? "true" : "false");
            AccountManager.IniSettings.Save("RAMSettings.ini");
        }

        private void StartOnLaunch_CheckedChanged(object sender, EventArgs e)
        {
            if (!SettingsLoaded) return;

            AccountManager.AccountControl.Set("StartOnLaunch", StartOnLaunch.Checked ? "true" : "false");
            AccountManager.IniSettings.Save("RAMSettings.ini");
        }

        private void RelaunchDelayNumber_ValueChanged(object sender, EventArgs e)
        {
            if (!SettingsLoaded) return;

            AccountManager.AccountControl.Set("RelaunchDelay", RelaunchDelayNumber.Value.ToString());
            AccountManager.IniSettings.Save("RAMSettings.ini");
        }

        private void LauncherDelayNumber_ValueChanged(object sender, EventArgs e)
        {
            AutoRelaunchTimer.Interval = (int)LauncherDelayNumber.Value * 1000;

            if (!SettingsLoaded) return;

            AccountManager.AccountControl.Set("LauncherDelay", LauncherDelayNumber.Value.ToString());
            AccountManager.IniSettings.Save("RAMSettings.ini");
        }

        private void PortNumber_ValueChanged(object sender, EventArgs e)
        {
            if (!SettingsLoaded) return;

            AccountManager.AccountControl.Set("NexusPort", PortNumber.Value.ToString());
            AccountManager.IniSettings.Save("RAMSettings.ini");
        }

        private void MinimizeRoblox_Click(object sender, EventArgs e)
        {
            foreach (Process p in Process.GetProcessesByName("RobloxPlayerBeta"))
                Utilities.PostMessage(p.MainWindowHandle, 0x0112, 0xF020, 0);
        }

        private void CloseRoblox_Click(object sender, EventArgs e)
        {
            foreach (Process p in Process.GetProcessesByName("RobloxPlayerBeta"))
                try { p.Kill(); } catch { }
        }

        private void MinimzeTimer_Tick(object sender, EventArgs e)
        {
            foreach (Process p in Process.GetProcessesByName("RobloxPlayerBeta"))
                Utilities.PostMessage(p.MainWindowHandle, 0x0112, 0xF020, 0);
        }

        private void CloseTimer_Tick(object sender, EventArgs e)
        {
            int ActualCount = 0;
            Process[] Processes = Process.GetProcessesByName("RobloxPlayerBeta");

            foreach (Process p in Processes) if (p.MainWindowHandle != IntPtr.Zero) ActualCount++;

            foreach (Process p in Processes)
                try
                { // Roblox's second process has elevated permissions meaning we won't be able to kill the second process unless we also have elevated permissions.
                    if (ActualCount > MaxInstancesNum.Value)
                        p.Kill();
                    else if (AutoCloseType.SelectedIndex == 0 && (DateTime.Now - p.StartTime).TotalMinutes > (int)AutoCloseIntervalNum.Value) // Per Instance
                        p.Kill();
                    else if (AutoCloseType.SelectedIndex == 1) // Global
                        p.Kill();
                }
                catch (Exception x) { Program.Logger.Error($"AutoClose: Cannot access Roblox process ({p.Id}): {x.Message}"); }
        }

        private void AutoCloseCB_CheckedChanged(object sender, EventArgs e)
        {
            CloseTimer.Enabled = AutoCloseCB.Checked;

            if (!SettingsLoaded) return;

            AccountManager.AccountControl.Set("AutoCloseEnabled", AutoCloseCB.Checked ? "true" : "false");
            AccountManager.IniSettings.Save("RAMSettings.ini");
        }

        private void InternetCheckCB_CheckedChanged(object sender, EventArgs e)
        {
            if (!SettingsLoaded) return;

            AccountManager.AccountControl.Set("InternetCheck", InternetCheckCB.Checked ? "true" : "false");
            AccountManager.IniSettings.Save("RAMSettings.ini");
        }

        private void UsePresenceCB_CheckedChanged(object sender, EventArgs e)
        {
            if (!SettingsLoaded) return;

            AccountManager.AccountControl.Set("UsePresence", UsePresenceCB.Checked ? "true" : "false");
            AccountManager.IniSettings.Save("RAMSettings.ini");
        }

        private void AutoMinIntervalNum_ValueChanged(object sender, EventArgs e)
        {
            MinimzeTimer.Interval = (int)AutoMinIntervalNum.Value * 1000;

            if (!SettingsLoaded) return;

            AccountManager.AccountControl.Set("AutoMinimizeInterval", AutoMinIntervalNum.Value.ToString());
            AccountManager.IniSettings.Save("RAMSettings.ini");
        }

        private void AutoCloseIntervalNum_ValueChanged(object sender, EventArgs e)
        {
            CloseTimer.Interval = AutoCloseType.SelectedIndex == 0 ? 3000 : (int)AutoCloseIntervalNum.Value * 60 * 1000;

            if (!SettingsLoaded) return;

            AccountManager.AccountControl.Set("AutoCloseInterval", AutoCloseIntervalNum.Value.ToString());
            AccountManager.IniSettings.Save("RAMSettings.ini");
        }

        private void MaxInstancesNum_ValueChanged(object sender, EventArgs e)
        {
            if (!SettingsLoaded) return;

            AccountManager.AccountControl.Set("MaxInstances", ((int)MaxInstancesNum.Value).ToString());
            AccountManager.IniSettings.Save("RAMSettings.ini");
        }

        private void AutoCloseType_SelectedIndexChanged(object sender, EventArgs e)
        {
            CloseTimer.Interval = AutoCloseType.SelectedIndex == 0 ? 3000 : (int)AutoCloseIntervalNum.Value * 60 * 1000; // Per Instance = 0 | Global = 1

            if (!SettingsLoaded) return;

            AccountManager.AccountControl.Set("AutoCloseType", AutoCloseType.SelectedIndex.ToString());
            AccountManager.IniSettings.Save("RAMSettings.ini");
        }

        #endregion

        #region Themes

        public void ApplyTheme()
        {
            BackColor = ThemeEditor.FormsBackground;
            ForeColor = ThemeEditor.FormsForeground;

            if (AccountsView.BackColor != ThemeEditor.AccountBackground || AccountsView.ForeColor != ThemeEditor.AccountForeground)
            {
                AccountsView.BackColor = ThemeEditor.AccountBackground;
                AccountsView.ForeColor = ThemeEditor.AccountForeground;

                AccountsView.BuildList(true);
                AccountsView.BuildGroups();
            }

            ApplyTheme(Controls);
        }

        public void ApplyTheme(Control.ControlCollection _Controls)
        {
            foreach (Control control in _Controls)
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
                else if (control is ListBox || control is ObjectListView)
                {
                    if (control is ObjectListView view) view.HeaderStyle = ThemeEditor.ShowHeaders ? ColumnHeaderStyle.Clickable : ColumnHeaderStyle.None;
                    control.BackColor = ThemeEditor.ButtonsBackground;
                    control.ForeColor = ThemeEditor.ButtonsForeground;
                }
                else if (control is TabPage)
                {
                    ApplyTheme(control.Controls);

                    control.BackColor = ThemeEditor.ButtonsBackground;
                    control.ForeColor = ThemeEditor.ButtonsForeground;
                }
                else if (control is FastColoredTextBoxNS.FastColoredTextBox)
                    control.ForeColor = Color.Black;
                else if (control is FlowLayoutPanel || control is Panel || control is TabControl)
                    ApplyTheme(control.Controls);
            }
        }

        #endregion

        #region Methods

        private void ClearDeadProcesses()
        {
            foreach (Process process in Process.GetProcesses())
            {
                if (process.ProcessName == "RobloxPlayerBeta" && Utilities.MD5(process.MainWindowTitle) == "6B19DD3A0E36191A937AB6FD96869A9D")
                    process.Kill();
            }
        }

        #endregion
    }
}
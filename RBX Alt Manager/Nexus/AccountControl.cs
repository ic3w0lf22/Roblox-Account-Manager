using BrightIdeasSoftware;
using Newtonsoft.Json;
using RBX_Alt_Manager.Classes;
using RBX_Alt_Manager.Nexus;
using RBX_Alt_Manager.Properties;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace RBX_Alt_Manager.Forms
{
    public partial class AccountControl : Form
    {
        private static string ACFile = Path.Combine(Environment.CurrentDirectory, "AccountControlData.json");
        private static object SaveLock = new object();

        public static AccountControl Instance;
        public static WebSocketServer Server;

        public Dictionary<WebSocketContext, ControlledAccount> ContextList = new Dictionary<WebSocketContext, ControlledAccount>();
        public List<ControlledAccount> Accounts = new List<ControlledAccount>();

        private Dictionary<string, Control> CustomElements = new Dictionary<string, Control>();
        private Control LastControl;
        private StreamWriter OutputWriter;

        [DllImport("user32", EntryPoint = "SendMessageA", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
        private static extern int SendMessage(int hwnd, int wMsg, int wParam, int lParam);

        public AccountControl()
        {
            Instance = this;

            AccountManager.SetDarkBar(Handle);

            InitializeComponent();

            LoadAccounts();
        }

        #region Save File

        private void SaveAccounts()
        {
            lock (SaveLock)
            {
                File.WriteAllText(ACFile, JsonConvert.SerializeObject(Accounts));
            }
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
                        else
                            Accounts.Remove(cAccount);
                    }
                }
                catch { }
            }

            AccountsView.SetObjects(Accounts);
        }

        #endregion

        #region Server

        private void OpenServer()
        {
            Server = new WebSocketServer(IPAddress.Loopback, 5242, false);

#if DEBUG
            Server.Log.Level = LogLevel.Debug;
#endif

            Server.AddWebSocketService<WebsocketServer>("/Nexus");

            Server.Start();

            Process.GetCurrentProcess().WaitForExit();
        }

        private void EmitMessage(string Message)
        {
            foreach (ControlledAccount account in AccountsView.CheckedObjects)
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

            TextBox l = new TextBox();
            l.Text = Message;
            l.ReadOnly = true;
            l.Multiline = true;
            l.WordWrap = true;
            l.TabStop = false;
            l.BorderStyle = 0;

            l.Margin = new Padding(1);

            l.MinimumSize = new Size(240, 13);
            l.MaximumSize = new Size(240, 120);
            l.BackColor = ThemeEditor.AccountBackground;
            l.ForeColor = ThemeEditor.FormsForeground;

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

            Button button = new Button();
            button.Name = Name;
            button.Size = size;
            button.Margin = margin;
            button.Text = Text;
            button.UseVisualStyleBackColor = true;

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

            TextBox textBox = new TextBox();
            textBox.Name = Name;
            textBox.Size = size;
            textBox.Margin = margin;
            textBox.Text = Text;

            LastControl = textBox;

            ControlsPanel.Controls.Add(textBox);

            CustomElements.Add(Name, textBox);

            ApplyTheme(ControlsPanel.Controls);
        }

        public void AddCustomNumericUpDown(string Name, decimal DefaultValue, int DecimalPlaces, decimal Increment, Size size, Padding margin)
        {
            if (CustomElements.ContainsKey(Name))
                return;

            NumericUpDown control = new NumericUpDown();

            control.DecimalPlaces = DecimalPlaces;
            control.Increment = Increment;
            control.Name = Name;
            control.Margin = margin;
            control.Size = size;
            control.Value = DefaultValue;
            control.Minimum = decimal.MinValue;
            control.Maximum = decimal.MaxValue;

            LastControl = control;

            ControlsPanel.Controls.Add(control);

            CustomElements.Add(Name, control);

            ApplyTheme(ControlsPanel.Controls);
        }

        public void AddCustomLabel(string Name, string Text, Padding margin)
        {
            if (CustomElements.ContainsKey(Name))
                return;

            Label label = new Label();
            label.Name = Name;
            label.Margin = margin;
            label.Text = Text;

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

            SaveOutputToFileCheck.Checked = AccountManager.IniSettings.Read("SaveOutput", "AccountControl") == "true";
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

        private void SaveOutputToFileCheck_CheckedChanged(object sender, EventArgs e) => AccountManager.IniSettings.Write("SaveOutput", SaveOutputToFileCheck.Checked ? "true" : "false", "AccountControl");

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

            File.WriteAllText(path, "pcall(function() loadstring(game:HttpGet'https://raw.githubusercontent.com/ic3w0lf22/Roblox-Account-Manager/master/RBX%20Alt%20Manager/Nexus/Nexus.lua')() end)");

            Process.Start("explorer.exe", "/select, " + path);
        }

        private void NexusDocsButton_Click(object sender, EventArgs e) => Process.Start("https://github.com/ic3w0lf22/Roblox-Account-Manager/blob/master/RBX%20Alt%20Manager/Nexus/NexusDocs.md");

        private void AutoRelaunchTimer_Tick(object sender, EventArgs e)
        {
            if (!double.TryParse(AccountManager.IniSettings.Read("RelaunchDelay", "AccountControl"), out double RelaunchDelay)) return;

            foreach (ControlledAccount account in Accounts)
            {
                if (account.AutoRelaunch && (DateTime.Now - account.LastPing).TotalSeconds > account.RelaunchDelay)
                {
                    account.LastPing = DateTime.Now;
                    account.RelaunchDelay = RelaunchDelay;

                    account.LinkedAccount.JoinServer(account.PlaceId, account.JobId);

                    break;
                }
            }

            ClearDeadProcesses();
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
                else if (control is ListBox)
                {
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
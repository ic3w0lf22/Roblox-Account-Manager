using RBX_Alt_Manager.Classes;
using System.Windows.Forms;

namespace RBX_Alt_Manager.Forms
{
    partial class AccountControl
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AccountControl));
            this.ControlsPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.AutoRelaunchCheckBox = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.PlaceIdText = new RBX_Alt_Manager.Classes.BorderedTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.JobIdText = new RBX_Alt_Manager.Classes.BorderedTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.CommandText = new RBX_Alt_Manager.Classes.BorderedTextBox();
            this.SendCommand = new System.Windows.Forms.Button();
            this.ScriptLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.ShowScriptBox = new System.Windows.Forms.Button();
            this.ScriptTabs = new RBX_Alt_Manager.Classes.NBTabControl();
            this.ExecutionPage = new System.Windows.Forms.TabPage();
            this.ScriptBox = new FastColoredTextBoxNS.FastColoredTextBox();
            this.ClearButton = new System.Windows.Forms.Button();
            this.ExecuteButton = new System.Windows.Forms.Button();
            this.AutoexecPage = new System.Windows.Forms.TabPage();
            this.AutoExecuteScriptBox = new FastColoredTextBoxNS.FastColoredTextBox();
            this.ClearAutoExecScript = new System.Windows.Forms.Button();
            this.OutputFlowPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.OutputToggle = new System.Windows.Forms.Button();
            this.OutputPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.ClearOutputButton = new System.Windows.Forms.Button();
            this.SaveOutputToFileCheck = new System.Windows.Forms.CheckBox();
            this.ACTabs = new RBX_Alt_Manager.Classes.NBTabControl();
            this.ControlPage = new System.Windows.Forms.TabPage();
            this.CPanel = new System.Windows.Forms.Panel();
            this.AccountsView = new BrightIdeasSoftware.ObjectListView();
            this.cCheckBoxes = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.cStatus = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.StatusRenderer = new BrightIdeasSoftware.MultiImageRenderer();
            this.cUsername = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.cJobId = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.ACStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyJobIdToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SettingsTab = new System.Windows.Forms.TabPage();
            this.SettingsLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.StartOnLaunch = new System.Windows.Forms.CheckBox();
            this.AllowExternalConnectionsCB = new System.Windows.Forms.CheckBox();
            this.InternetCheckCB = new System.Windows.Forms.CheckBox();
            this.UsePresenceCB = new System.Windows.Forms.CheckBox();
            this.RLLabel = new System.Windows.Forms.Label();
            this.RelaunchDelayNumber = new System.Windows.Forms.NumericUpDown();
            this.LDLabel = new System.Windows.Forms.Label();
            this.LauncherDelayNumber = new System.Windows.Forms.NumericUpDown();
            this.PortLabel = new System.Windows.Forms.Label();
            this.PortNumber = new System.Windows.Forms.NumericUpDown();
            this.MinimizeRoblox = new System.Windows.Forms.Button();
            this.AutoMinimizeCB = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.AutoMinIntervalNum = new System.Windows.Forms.NumericUpDown();
            this.CloseRoblox = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.AutoCloseType = new System.Windows.Forms.ComboBox();
            this.ACLabel = new System.Windows.Forms.Label();
            this.AutoCloseIntervalNum = new System.Windows.Forms.NumericUpDown();
            this.MaxInstanceLabel = new System.Windows.Forms.Label();
            this.MaxInstancesNum = new System.Windows.Forms.NumericUpDown();
            this.AutoCloseCB = new System.Windows.Forms.CheckBox();
            this.HelpPage = new System.Windows.Forms.TabPage();
            this.label7 = new System.Windows.Forms.Label();
            this.NexusDocsButton = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.NexusDL = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.AutoRelaunchTimer = new System.Windows.Forms.Timer(this.components);
            this.MinimzeTimer = new System.Windows.Forms.Timer(this.components);
            this.CloseTimer = new System.Windows.Forms.Timer(this.components);
            this.Helper = new System.Windows.Forms.ToolTip(this.components);
            this.ControlsPanel.SuspendLayout();
            this.ScriptLayoutPanel.SuspendLayout();
            this.ScriptTabs.SuspendLayout();
            this.ExecutionPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ScriptBox)).BeginInit();
            this.AutoexecPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AutoExecuteScriptBox)).BeginInit();
            this.OutputFlowPanel.SuspendLayout();
            this.ACTabs.SuspendLayout();
            this.ControlPage.SuspendLayout();
            this.CPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AccountsView)).BeginInit();
            this.ACStrip.SuspendLayout();
            this.SettingsTab.SuspendLayout();
            this.SettingsLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RelaunchDelayNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LauncherDelayNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PortNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AutoMinIntervalNum)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AutoCloseIntervalNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MaxInstancesNum)).BeginInit();
            this.HelpPage.SuspendLayout();
            this.SuspendLayout();
            // 
            // ControlsPanel
            // 
            this.ControlsPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ControlsPanel.AutoSize = true;
            this.ControlsPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ControlsPanel.Controls.Add(this.AutoRelaunchCheckBox);
            this.ControlsPanel.Controls.Add(this.label1);
            this.ControlsPanel.Controls.Add(this.PlaceIdText);
            this.ControlsPanel.Controls.Add(this.label2);
            this.ControlsPanel.Controls.Add(this.JobIdText);
            this.ControlsPanel.Controls.Add(this.label3);
            this.ControlsPanel.Controls.Add(this.CommandText);
            this.ControlsPanel.Controls.Add(this.SendCommand);
            this.ControlsPanel.Controls.Add(this.ScriptLayoutPanel);
            this.ControlsPanel.Controls.Add(this.OutputFlowPanel);
            this.ControlsPanel.Location = new System.Drawing.Point(0, 0);
            this.ControlsPanel.Name = "ControlsPanel";
            this.ControlsPanel.Padding = new System.Windows.Forms.Padding(5);
            this.ControlsPanel.Size = new System.Drawing.Size(284, 503);
            this.ControlsPanel.TabIndex = 0;
            // 
            // AutoRelaunchCheckBox
            // 
            this.AutoRelaunchCheckBox.AutoSize = true;
            this.ControlsPanel.SetFlowBreak(this.AutoRelaunchCheckBox, true);
            this.AutoRelaunchCheckBox.Location = new System.Drawing.Point(8, 8);
            this.AutoRelaunchCheckBox.Name = "AutoRelaunchCheckBox";
            this.AutoRelaunchCheckBox.Size = new System.Drawing.Size(97, 17);
            this.AutoRelaunchCheckBox.TabIndex = 0;
            this.AutoRelaunchCheckBox.Text = "Auto Relaunch";
            this.AutoRelaunchCheckBox.UseVisualStyleBackColor = true;
            this.AutoRelaunchCheckBox.CheckedChanged += new System.EventHandler(this.AutoRelaunchCheckBox_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 33);
            this.label1.Margin = new System.Windows.Forms.Padding(3, 5, 9, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Place ID";
            // 
            // PlaceIdText
            // 
            this.PlaceIdText.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(122)))), ((int)(((byte)(122)))));
            this.PlaceIdText.Location = new System.Drawing.Point(68, 31);
            this.PlaceIdText.Name = "PlaceIdText";
            this.PlaceIdText.Size = new System.Drawing.Size(207, 20);
            this.PlaceIdText.TabIndex = 1;
            this.PlaceIdText.Leave += new System.EventHandler(this.PlaceIdText_Leave);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 59);
            this.label2.Margin = new System.Windows.Forms.Padding(13, 5, 9, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Job ID";
            // 
            // JobIdText
            // 
            this.JobIdText.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(122)))), ((int)(((byte)(122)))));
            this.JobIdText.Location = new System.Drawing.Point(68, 57);
            this.JobIdText.Name = "JobIdText";
            this.JobIdText.Size = new System.Drawing.Size(207, 20);
            this.JobIdText.TabIndex = 2;
            this.JobIdText.Leave += new System.EventHandler(this.JobIdText_Leave);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(8, 85);
            this.label3.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 13);
            this.label3.TabIndex = 6;
            this.label3.Text = "Command";
            // 
            // CommandText
            // 
            this.CommandText.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(122)))), ((int)(((byte)(122)))));
            this.CommandText.Location = new System.Drawing.Point(68, 83);
            this.CommandText.Name = "CommandText";
            this.CommandText.Size = new System.Drawing.Size(153, 20);
            this.CommandText.TabIndex = 7;
            this.CommandText.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.CommandText_KeyPress);
            this.CommandText.KeyUp += new System.Windows.Forms.KeyEventHandler(this.CommandText_KeyUp);
            // 
            // SendCommand
            // 
            this.SendCommand.Location = new System.Drawing.Point(227, 82);
            this.SendCommand.Margin = new System.Windows.Forms.Padding(3, 2, 3, 3);
            this.SendCommand.Name = "SendCommand";
            this.SendCommand.Size = new System.Drawing.Size(49, 22);
            this.SendCommand.TabIndex = 9;
            this.SendCommand.Text = "Send";
            this.SendCommand.UseVisualStyleBackColor = true;
            this.SendCommand.Click += new System.EventHandler(this.SendCommand_Click);
            // 
            // ScriptLayoutPanel
            // 
            this.ScriptLayoutPanel.Controls.Add(this.ShowScriptBox);
            this.ScriptLayoutPanel.Controls.Add(this.ScriptTabs);
            this.ScriptLayoutPanel.Location = new System.Drawing.Point(8, 110);
            this.ScriptLayoutPanel.Name = "ScriptLayoutPanel";
            this.ScriptLayoutPanel.Size = new System.Drawing.Size(268, 212);
            this.ScriptLayoutPanel.TabIndex = 12;
            // 
            // ShowScriptBox
            // 
            this.ShowScriptBox.Location = new System.Drawing.Point(3, 1);
            this.ShowScriptBox.Margin = new System.Windows.Forms.Padding(3, 1, 3, 0);
            this.ShowScriptBox.Name = "ShowScriptBox";
            this.ShowScriptBox.Size = new System.Drawing.Size(262, 10);
            this.ShowScriptBox.TabIndex = 12;
            this.ShowScriptBox.Text = " ";
            this.ShowScriptBox.UseVisualStyleBackColor = true;
            this.ShowScriptBox.Click += new System.EventHandler(this.ShowScriptBox_Click);
            // 
            // ScriptTabs
            // 
            this.ScriptTabs.Controls.Add(this.ExecutionPage);
            this.ScriptTabs.Controls.Add(this.AutoexecPage);
            this.ScriptTabs.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.ScriptTabs.Location = new System.Drawing.Point(3, 13);
            this.ScriptTabs.Margin = new System.Windows.Forms.Padding(3, 2, 3, 3);
            this.ScriptTabs.Name = "ScriptTabs";
            this.ScriptTabs.SelectedIndex = 0;
            this.ScriptTabs.Size = new System.Drawing.Size(262, 200);
            this.ScriptTabs.TabIndex = 13;
            // 
            // ExecutionPage
            // 
            this.ExecutionPage.Controls.Add(this.ScriptBox);
            this.ExecutionPage.Controls.Add(this.ClearButton);
            this.ExecutionPage.Controls.Add(this.ExecuteButton);
            this.ExecutionPage.Location = new System.Drawing.Point(4, 25);
            this.ExecutionPage.Name = "ExecutionPage";
            this.ExecutionPage.Padding = new System.Windows.Forms.Padding(3);
            this.ExecutionPage.Size = new System.Drawing.Size(254, 171);
            this.ExecutionPage.TabIndex = 0;
            this.ExecutionPage.Text = "Executor";
            this.ExecutionPage.UseVisualStyleBackColor = true;
            // 
            // ScriptBox
            // 
            this.ScriptBox.AutoCompleteBracketsList = new char[] {
        '(',
        ')',
        '{',
        '}',
        '[',
        ']',
        '\"',
        '\"',
        '\'',
        '\''};
            this.ScriptBox.AutoIndentCharsPatterns = "\r\n^\\s*[\\w\\.]+(\\s\\w+)?\\s*(?<range>=)\\s*(?<range>.+)\r\n";
            this.ScriptBox.AutoScrollMinSize = new System.Drawing.Size(267, 98);
            this.ScriptBox.BackBrush = null;
            this.ScriptBox.BracketsHighlightStrategy = FastColoredTextBoxNS.BracketsHighlightStrategy.Strategy2;
            this.ScriptBox.CharHeight = 14;
            this.ScriptBox.CharWidth = 8;
            this.ScriptBox.CommentPrefix = "--";
            this.ScriptBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.ScriptBox.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.ScriptBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.ScriptBox.Font = new System.Drawing.Font("Courier New", 9.75F);
            this.ScriptBox.IsReplaceMode = false;
            this.ScriptBox.Language = FastColoredTextBoxNS.Language.Lua;
            this.ScriptBox.LeftBracket = '(';
            this.ScriptBox.LeftBracket2 = '{';
            this.ScriptBox.Location = new System.Drawing.Point(3, 3);
            this.ScriptBox.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.ScriptBox.Name = "ScriptBox";
            this.ScriptBox.Paddings = new System.Windows.Forms.Padding(0);
            this.ScriptBox.RightBracket = ')';
            this.ScriptBox.RightBracket2 = '}';
            this.ScriptBox.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.ScriptBox.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("ScriptBox.ServiceColors")));
            this.ScriptBox.Size = new System.Drawing.Size(248, 136);
            this.ScriptBox.TabIndex = 8;
            this.ScriptBox.Tag = "UseControlFont";
            this.ScriptBox.Text = "print(\"Hello World!\")\r\n\r\n-- THIS IS NOT A BUILT-IN\r\n-- EXECUTOR, ONLY A PROXY.\r\n-" +
    "- YOU MUST CONNECT YOUR\r\n-- OWN EXECUTOR TO THIS\r\n-- WITH NEXUS (Click Help Tab)" +
    "";
            this.ScriptBox.Zoom = 100;
            // 
            // ClearButton
            // 
            this.ClearButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ClearButton.Location = new System.Drawing.Point(173, 142);
            this.ClearButton.Name = "ClearButton";
            this.ClearButton.Size = new System.Drawing.Size(75, 23);
            this.ClearButton.TabIndex = 11;
            this.ClearButton.Text = "Clear";
            this.ClearButton.UseVisualStyleBackColor = true;
            this.ClearButton.Click += new System.EventHandler(this.ClearButton_Click);
            // 
            // ExecuteButton
            // 
            this.ExecuteButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ExecuteButton.Location = new System.Drawing.Point(6, 142);
            this.ExecuteButton.Name = "ExecuteButton";
            this.ExecuteButton.Size = new System.Drawing.Size(75, 23);
            this.ExecuteButton.TabIndex = 10;
            this.ExecuteButton.Text = "Execute";
            this.ExecuteButton.UseVisualStyleBackColor = true;
            this.ExecuteButton.Click += new System.EventHandler(this.ExecuteButton_Click);
            // 
            // AutoexecPage
            // 
            this.AutoexecPage.Controls.Add(this.AutoExecuteScriptBox);
            this.AutoexecPage.Controls.Add(this.ClearAutoExecScript);
            this.AutoexecPage.Location = new System.Drawing.Point(4, 25);
            this.AutoexecPage.Name = "AutoexecPage";
            this.AutoexecPage.Padding = new System.Windows.Forms.Padding(3);
            this.AutoexecPage.Size = new System.Drawing.Size(254, 171);
            this.AutoexecPage.TabIndex = 1;
            this.AutoexecPage.Text = "Auto Execute";
            this.AutoexecPage.UseVisualStyleBackColor = true;
            // 
            // AutoExecuteScriptBox
            // 
            this.AutoExecuteScriptBox.AutoCompleteBracketsList = new char[] {
        '(',
        ')',
        '{',
        '}',
        '[',
        ']',
        '\"',
        '\"',
        '\'',
        '\''};
            this.AutoExecuteScriptBox.AutoIndentCharsPatterns = "\r\n^\\s*[\\w\\.]+(\\s\\w+)?\\s*(?<range>=)\\s*(?<range>.+)\r\n";
            this.AutoExecuteScriptBox.AutoScrollMinSize = new System.Drawing.Size(10, 14);
            this.AutoExecuteScriptBox.BackBrush = null;
            this.AutoExecuteScriptBox.BracketsHighlightStrategy = FastColoredTextBoxNS.BracketsHighlightStrategy.Strategy2;
            this.AutoExecuteScriptBox.CharHeight = 14;
            this.AutoExecuteScriptBox.CharWidth = 8;
            this.AutoExecuteScriptBox.CommentPrefix = "--";
            this.AutoExecuteScriptBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.AutoExecuteScriptBox.DisabledColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))), ((int)(((byte)(180)))));
            this.AutoExecuteScriptBox.Dock = System.Windows.Forms.DockStyle.Top;
            this.AutoExecuteScriptBox.Font = new System.Drawing.Font("Courier New", 9.75F);
            this.AutoExecuteScriptBox.IsReplaceMode = false;
            this.AutoExecuteScriptBox.Language = FastColoredTextBoxNS.Language.Lua;
            this.AutoExecuteScriptBox.LeftBracket = '(';
            this.AutoExecuteScriptBox.LeftBracket2 = '{';
            this.AutoExecuteScriptBox.Location = new System.Drawing.Point(3, 3);
            this.AutoExecuteScriptBox.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.AutoExecuteScriptBox.Name = "AutoExecuteScriptBox";
            this.AutoExecuteScriptBox.Paddings = new System.Windows.Forms.Padding(0);
            this.AutoExecuteScriptBox.RightBracket = ')';
            this.AutoExecuteScriptBox.RightBracket2 = '}';
            this.AutoExecuteScriptBox.SelectionColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(255)))));
            this.AutoExecuteScriptBox.ServiceColors = ((FastColoredTextBoxNS.ServiceColors)(resources.GetObject("AutoExecuteScriptBox.ServiceColors")));
            this.AutoExecuteScriptBox.Size = new System.Drawing.Size(248, 136);
            this.AutoExecuteScriptBox.TabIndex = 12;
            this.AutoExecuteScriptBox.Text = " ";
            this.AutoExecuteScriptBox.Zoom = 100;
            this.AutoExecuteScriptBox.Leave += new System.EventHandler(this.AutoExecuteScriptBox_Leave);
            // 
            // ClearAutoExecScript
            // 
            this.ClearAutoExecScript.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ClearAutoExecScript.Location = new System.Drawing.Point(173, 142);
            this.ClearAutoExecScript.Name = "ClearAutoExecScript";
            this.ClearAutoExecScript.Size = new System.Drawing.Size(75, 23);
            this.ClearAutoExecScript.TabIndex = 13;
            this.ClearAutoExecScript.Text = "Clear";
            this.ClearAutoExecScript.UseVisualStyleBackColor = true;
            this.ClearAutoExecScript.Click += new System.EventHandler(this.ClearAutoExecScript_Click);
            // 
            // OutputFlowPanel
            // 
            this.OutputFlowPanel.Controls.Add(this.OutputToggle);
            this.OutputFlowPanel.Controls.Add(this.OutputPanel);
            this.OutputFlowPanel.Controls.Add(this.ClearOutputButton);
            this.OutputFlowPanel.Controls.Add(this.SaveOutputToFileCheck);
            this.OutputFlowPanel.Location = new System.Drawing.Point(8, 328);
            this.OutputFlowPanel.Name = "OutputFlowPanel";
            this.OutputFlowPanel.Size = new System.Drawing.Size(268, 167);
            this.OutputFlowPanel.TabIndex = 13;
            // 
            // OutputToggle
            // 
            this.OutputToggle.Location = new System.Drawing.Point(3, 1);
            this.OutputToggle.Margin = new System.Windows.Forms.Padding(3, 1, 3, 0);
            this.OutputToggle.Name = "OutputToggle";
            this.OutputToggle.Size = new System.Drawing.Size(262, 10);
            this.OutputToggle.TabIndex = 13;
            this.OutputToggle.UseVisualStyleBackColor = true;
            this.OutputToggle.Click += new System.EventHandler(this.OutputToggle_Click);
            // 
            // OutputPanel
            // 
            this.OutputPanel.AutoScroll = true;
            this.OutputPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.OutputPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.OutputPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.OutputPanel.Location = new System.Drawing.Point(3, 11);
            this.OutputPanel.Margin = new System.Windows.Forms.Padding(3, 0, 3, 3);
            this.OutputPanel.Name = "OutputPanel";
            this.OutputPanel.Size = new System.Drawing.Size(262, 120);
            this.OutputPanel.TabIndex = 14;
            this.OutputPanel.WrapContents = false;
            // 
            // ClearOutputButton
            // 
            this.ClearOutputButton.Location = new System.Drawing.Point(3, 136);
            this.ClearOutputButton.Margin = new System.Windows.Forms.Padding(3, 2, 3, 3);
            this.ClearOutputButton.Name = "ClearOutputButton";
            this.ClearOutputButton.Size = new System.Drawing.Size(75, 23);
            this.ClearOutputButton.TabIndex = 12;
            this.ClearOutputButton.Text = "Clear";
            this.ClearOutputButton.UseVisualStyleBackColor = true;
            this.ClearOutputButton.Click += new System.EventHandler(this.ClearOutputButton_Click);
            // 
            // SaveOutputToFileCheck
            // 
            this.SaveOutputToFileCheck.AutoSize = true;
            this.OutputFlowPanel.SetFlowBreak(this.SaveOutputToFileCheck, true);
            this.SaveOutputToFileCheck.Location = new System.Drawing.Point(84, 137);
            this.SaveOutputToFileCheck.Name = "SaveOutputToFileCheck";
            this.SaveOutputToFileCheck.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.SaveOutputToFileCheck.Size = new System.Drawing.Size(117, 20);
            this.SaveOutputToFileCheck.TabIndex = 15;
            this.SaveOutputToFileCheck.Text = "Save Output to File";
            this.SaveOutputToFileCheck.UseVisualStyleBackColor = true;
            this.SaveOutputToFileCheck.CheckedChanged += new System.EventHandler(this.SaveOutputToFileCheck_CheckedChanged);
            // 
            // ACTabs
            // 
            this.ACTabs.Controls.Add(this.ControlPage);
            this.ACTabs.Controls.Add(this.SettingsTab);
            this.ACTabs.Controls.Add(this.HelpPage);
            this.ACTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ACTabs.Location = new System.Drawing.Point(0, 0);
            this.ACTabs.Name = "ACTabs";
            this.ACTabs.SelectedIndex = 0;
            this.ACTabs.Size = new System.Drawing.Size(593, 394);
            this.ACTabs.TabIndex = 1;
            // 
            // ControlPage
            // 
            this.ControlPage.Controls.Add(this.CPanel);
            this.ControlPage.Controls.Add(this.AccountsView);
            this.ControlPage.Location = new System.Drawing.Point(4, 25);
            this.ControlPage.Name = "ControlPage";
            this.ControlPage.Padding = new System.Windows.Forms.Padding(3);
            this.ControlPage.Size = new System.Drawing.Size(585, 365);
            this.ControlPage.TabIndex = 0;
            this.ControlPage.Text = "Control Panel";
            this.ControlPage.UseVisualStyleBackColor = true;
            // 
            // CPanel
            // 
            this.CPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CPanel.AutoScroll = true;
            this.CPanel.Controls.Add(this.ControlsPanel);
            this.CPanel.Location = new System.Drawing.Point(279, 7);
            this.CPanel.Name = "CPanel";
            this.CPanel.Size = new System.Drawing.Size(298, 353);
            this.CPanel.TabIndex = 2;
            // 
            // AccountsView
            // 
            this.AccountsView.AllColumns.Add(this.cCheckBoxes);
            this.AccountsView.AllColumns.Add(this.cStatus);
            this.AccountsView.AllColumns.Add(this.cUsername);
            this.AccountsView.AllColumns.Add(this.cJobId);
            this.AccountsView.AllowColumnReorder = true;
            this.AccountsView.AllowDrop = true;
            this.AccountsView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AccountsView.CellEditUseWholeCell = false;
            this.AccountsView.CheckBoxes = true;
            this.AccountsView.CheckedAspectName = "IsChecked";
            this.AccountsView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.cCheckBoxes,
            this.cStatus,
            this.cUsername,
            this.cJobId});
            this.AccountsView.ContextMenuStrip = this.ACStrip;
            this.AccountsView.Cursor = System.Windows.Forms.Cursors.Default;
            this.AccountsView.FullRowSelect = true;
            this.AccountsView.HideSelection = false;
            this.AccountsView.Location = new System.Drawing.Point(8, 7);
            this.AccountsView.Name = "AccountsView";
            this.AccountsView.ShowGroups = false;
            this.AccountsView.ShowImagesOnSubItems = true;
            this.AccountsView.Size = new System.Drawing.Size(265, 353);
            this.AccountsView.TabIndex = 1;
            this.AccountsView.UseCompatibleStateImageBehavior = false;
            this.AccountsView.UseSubItemCheckBoxes = true;
            this.AccountsView.View = System.Windows.Forms.View.Details;
            this.AccountsView.SelectionChanged += new System.EventHandler(this.AccountsView_SelectionChanged);
            this.AccountsView.DragDrop += new System.Windows.Forms.DragEventHandler(this.AccountsView_DragDrop);
            this.AccountsView.DragOver += new System.Windows.Forms.DragEventHandler(this.AccountsView_DragOver);
            // 
            // cCheckBoxes
            // 
            this.cCheckBoxes.AspectName = "IsChecked";
            this.cCheckBoxes.HeaderCheckBox = true;
            this.cCheckBoxes.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.cCheckBoxes.Text = "";
            this.cCheckBoxes.Width = 20;
            // 
            // cStatus
            // 
            this.cStatus.AspectName = "";
            this.cStatus.HeaderTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.cStatus.Renderer = this.StatusRenderer;
            this.cStatus.Text = "";
            this.cStatus.Width = 20;
            // 
            // StatusRenderer
            // 
            this.StatusRenderer.ImageName = "offline";
            this.StatusRenderer.MaxNumberImages = 2;
            // 
            // cUsername
            // 
            this.cUsername.AspectName = "Username";
            this.cUsername.Text = "Username";
            this.cUsername.Width = 125;
            // 
            // cJobId
            // 
            this.cJobId.AspectName = "InGameJobId";
            this.cJobId.Text = "Job ID";
            this.cJobId.Width = 89;
            // 
            // ACStrip
            // 
            this.ACStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyJobIdToolStripMenuItem,
            this.removeToolStripMenuItem});
            this.ACStrip.Name = "ACStrip";
            this.ACStrip.Size = new System.Drawing.Size(134, 48);
            // 
            // copyJobIdToolStripMenuItem
            // 
            this.copyJobIdToolStripMenuItem.Name = "copyJobIdToolStripMenuItem";
            this.copyJobIdToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.copyJobIdToolStripMenuItem.Text = "Copy JobId";
            this.copyJobIdToolStripMenuItem.Click += new System.EventHandler(this.copyJobIdToolStripMenuItem_Click);
            // 
            // removeToolStripMenuItem
            // 
            this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            this.removeToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.removeToolStripMenuItem.Text = "Remove";
            this.removeToolStripMenuItem.Click += new System.EventHandler(this.removeToolStripMenuItem_Click);
            // 
            // SettingsTab
            // 
            this.SettingsTab.Controls.Add(this.SettingsLayoutPanel);
            this.SettingsTab.Location = new System.Drawing.Point(4, 25);
            this.SettingsTab.Name = "SettingsTab";
            this.SettingsTab.Padding = new System.Windows.Forms.Padding(3);
            this.SettingsTab.Size = new System.Drawing.Size(585, 365);
            this.SettingsTab.TabIndex = 3;
            this.SettingsTab.Text = "Settings";
            this.SettingsTab.UseVisualStyleBackColor = true;
            // 
            // SettingsLayoutPanel
            // 
            this.SettingsLayoutPanel.Controls.Add(this.StartOnLaunch);
            this.SettingsLayoutPanel.Controls.Add(this.AllowExternalConnectionsCB);
            this.SettingsLayoutPanel.Controls.Add(this.InternetCheckCB);
            this.SettingsLayoutPanel.Controls.Add(this.UsePresenceCB);
            this.SettingsLayoutPanel.Controls.Add(this.RLLabel);
            this.SettingsLayoutPanel.Controls.Add(this.RelaunchDelayNumber);
            this.SettingsLayoutPanel.Controls.Add(this.LDLabel);
            this.SettingsLayoutPanel.Controls.Add(this.LauncherDelayNumber);
            this.SettingsLayoutPanel.Controls.Add(this.PortLabel);
            this.SettingsLayoutPanel.Controls.Add(this.PortNumber);
            this.SettingsLayoutPanel.Controls.Add(this.MinimizeRoblox);
            this.SettingsLayoutPanel.Controls.Add(this.AutoMinimizeCB);
            this.SettingsLayoutPanel.Controls.Add(this.label8);
            this.SettingsLayoutPanel.Controls.Add(this.AutoMinIntervalNum);
            this.SettingsLayoutPanel.Controls.Add(this.CloseRoblox);
            this.SettingsLayoutPanel.Controls.Add(this.tableLayoutPanel1);
            this.SettingsLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SettingsLayoutPanel.Location = new System.Drawing.Point(3, 3);
            this.SettingsLayoutPanel.Name = "SettingsLayoutPanel";
            this.SettingsLayoutPanel.Padding = new System.Windows.Forms.Padding(12);
            this.SettingsLayoutPanel.Size = new System.Drawing.Size(579, 359);
            this.SettingsLayoutPanel.TabIndex = 2;
            // 
            // StartOnLaunch
            // 
            this.StartOnLaunch.AutoSize = true;
            this.SettingsLayoutPanel.SetFlowBreak(this.StartOnLaunch, true);
            this.StartOnLaunch.Location = new System.Drawing.Point(15, 15);
            this.StartOnLaunch.Name = "StartOnLaunch";
            this.StartOnLaunch.Size = new System.Drawing.Size(223, 17);
            this.StartOnLaunch.TabIndex = 15;
            this.StartOnLaunch.Text = "Start Nexus on Account Manager Launch";
            this.StartOnLaunch.UseVisualStyleBackColor = true;
            this.StartOnLaunch.CheckedChanged += new System.EventHandler(this.StartOnLaunch_CheckedChanged);
            // 
            // AllowExternalConnectionsCB
            // 
            this.AllowExternalConnectionsCB.AutoSize = true;
            this.SettingsLayoutPanel.SetFlowBreak(this.AllowExternalConnectionsCB, true);
            this.AllowExternalConnectionsCB.Location = new System.Drawing.Point(15, 38);
            this.AllowExternalConnectionsCB.Name = "AllowExternalConnectionsCB";
            this.AllowExternalConnectionsCB.Size = new System.Drawing.Size(154, 17);
            this.AllowExternalConnectionsCB.TabIndex = 7;
            this.AllowExternalConnectionsCB.Text = "Allow External Connections";
            this.AllowExternalConnectionsCB.UseVisualStyleBackColor = true;
            this.AllowExternalConnectionsCB.CheckedChanged += new System.EventHandler(this.AllowExternalConnectionsCB_CheckedChanged);
            // 
            // InternetCheckCB
            // 
            this.InternetCheckCB.AutoSize = true;
            this.SettingsLayoutPanel.SetFlowBreak(this.InternetCheckCB, true);
            this.InternetCheckCB.Location = new System.Drawing.Point(15, 61);
            this.InternetCheckCB.Name = "InternetCheckCB";
            this.InternetCheckCB.Size = new System.Drawing.Size(184, 17);
            this.InternetCheckCB.TabIndex = 24;
            this.InternetCheckCB.Text = "Check for Internet Before Launch";
            this.InternetCheckCB.UseVisualStyleBackColor = true;
            this.InternetCheckCB.CheckedChanged += new System.EventHandler(this.InternetCheckCB_CheckedChanged);
            // 
            // UsePresenceCB
            // 
            this.UsePresenceCB.AutoSize = true;
            this.UsePresenceCB.Cursor = System.Windows.Forms.Cursors.Help;
            this.SettingsLayoutPanel.SetFlowBreak(this.UsePresenceCB, true);
            this.UsePresenceCB.Location = new System.Drawing.Point(15, 84);
            this.UsePresenceCB.Name = "UsePresenceCB";
            this.UsePresenceCB.Size = new System.Drawing.Size(113, 17);
            this.UsePresenceCB.TabIndex = 25;
            this.UsePresenceCB.Text = "Use Presence API";
            this.Helper.SetToolTip(this.UsePresenceCB, "Uses Roblox\'s presence API to check if an\r\naccount is online instead of having Ne" +
        "xus\r\nconnect to the program when re-launching");
            this.UsePresenceCB.UseVisualStyleBackColor = true;
            this.UsePresenceCB.CheckedChanged += new System.EventHandler(this.UsePresenceCB_CheckedChanged);
            // 
            // RLLabel
            // 
            this.RLLabel.AutoSize = true;
            this.RLLabel.Location = new System.Drawing.Point(15, 108);
            this.RLLabel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 0);
            this.RLLabel.Name = "RLLabel";
            this.RLLabel.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RLLabel.Size = new System.Drawing.Size(171, 13);
            this.RLLabel.TabIndex = 11;
            this.RLLabel.Text = "Relaunch Delay Per Account (sec)";
            // 
            // RelaunchDelayNumber
            // 
            this.SettingsLayoutPanel.SetFlowBreak(this.RelaunchDelayNumber, true);
            this.RelaunchDelayNumber.Location = new System.Drawing.Point(192, 105);
            this.RelaunchDelayNumber.Margin = new System.Windows.Forms.Padding(3, 1, 3, 0);
            this.RelaunchDelayNumber.Maximum = new decimal(new int[] {
            3600,
            0,
            0,
            0});
            this.RelaunchDelayNumber.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.RelaunchDelayNumber.Name = "RelaunchDelayNumber";
            this.RelaunchDelayNumber.Size = new System.Drawing.Size(56, 20);
            this.RelaunchDelayNumber.TabIndex = 10;
            this.RelaunchDelayNumber.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.RelaunchDelayNumber.ValueChanged += new System.EventHandler(this.RelaunchDelayNumber_ValueChanged);
            // 
            // LDLabel
            // 
            this.LDLabel.Location = new System.Drawing.Point(15, 129);
            this.LDLabel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 0);
            this.LDLabel.Name = "LDLabel";
            this.LDLabel.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.LDLabel.Size = new System.Drawing.Size(171, 13);
            this.LDLabel.TabIndex = 23;
            this.LDLabel.Text = "Launcher Delay (sec)";
            // 
            // LauncherDelayNumber
            // 
            this.SettingsLayoutPanel.SetFlowBreak(this.LauncherDelayNumber, true);
            this.LauncherDelayNumber.Location = new System.Drawing.Point(192, 126);
            this.LauncherDelayNumber.Margin = new System.Windows.Forms.Padding(3, 1, 3, 0);
            this.LauncherDelayNumber.Maximum = new decimal(new int[] {
            3600,
            0,
            0,
            0});
            this.LauncherDelayNumber.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.LauncherDelayNumber.Name = "LauncherDelayNumber";
            this.LauncherDelayNumber.Size = new System.Drawing.Size(56, 20);
            this.LauncherDelayNumber.TabIndex = 22;
            this.LauncherDelayNumber.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.LauncherDelayNumber.ValueChanged += new System.EventHandler(this.LauncherDelayNumber_ValueChanged);
            // 
            // PortLabel
            // 
            this.PortLabel.Location = new System.Drawing.Point(15, 150);
            this.PortLabel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 0);
            this.PortLabel.Name = "PortLabel";
            this.PortLabel.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.PortLabel.Size = new System.Drawing.Size(171, 13);
            this.PortLabel.TabIndex = 9;
            this.PortLabel.Text = "Port";
            // 
            // PortNumber
            // 
            this.SettingsLayoutPanel.SetFlowBreak(this.PortNumber, true);
            this.PortNumber.Location = new System.Drawing.Point(192, 147);
            this.PortNumber.Margin = new System.Windows.Forms.Padding(3, 1, 3, 0);
            this.PortNumber.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.PortNumber.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.PortNumber.Name = "PortNumber";
            this.PortNumber.Size = new System.Drawing.Size(56, 20);
            this.PortNumber.TabIndex = 8;
            this.PortNumber.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.PortNumber.ValueChanged += new System.EventHandler(this.PortNumber_ValueChanged);
            // 
            // MinimizeRoblox
            // 
            this.MinimizeRoblox.Location = new System.Drawing.Point(15, 170);
            this.MinimizeRoblox.Name = "MinimizeRoblox";
            this.MinimizeRoblox.Size = new System.Drawing.Size(145, 23);
            this.MinimizeRoblox.TabIndex = 12;
            this.MinimizeRoblox.Text = "Minimize Roblox";
            this.MinimizeRoblox.UseVisualStyleBackColor = true;
            this.MinimizeRoblox.Click += new System.EventHandler(this.MinimizeRoblox_Click);
            // 
            // AutoMinimizeCB
            // 
            this.AutoMinimizeCB.Location = new System.Drawing.Point(166, 174);
            this.AutoMinimizeCB.Margin = new System.Windows.Forms.Padding(3, 7, 3, 3);
            this.AutoMinimizeCB.Name = "AutoMinimizeCB";
            this.AutoMinimizeCB.Size = new System.Drawing.Size(91, 17);
            this.AutoMinimizeCB.TabIndex = 14;
            this.AutoMinimizeCB.Text = "Auto Minimize";
            this.AutoMinimizeCB.UseVisualStyleBackColor = true;
            this.AutoMinimizeCB.CheckedChanged += new System.EventHandler(this.AutoMinimizeCB_CheckedChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(263, 167);
            this.label8.Name = "label8";
            this.label8.Padding = new System.Windows.Forms.Padding(0, 8, 0, 0);
            this.label8.Size = new System.Drawing.Size(68, 21);
            this.label8.TabIndex = 19;
            this.label8.Text = "Interval (sec)";
            // 
            // AutoMinIntervalNum
            // 
            this.SettingsLayoutPanel.SetFlowBreak(this.AutoMinIntervalNum, true);
            this.AutoMinIntervalNum.Location = new System.Drawing.Point(337, 170);
            this.AutoMinIntervalNum.Maximum = new decimal(new int[] {
            3000,
            0,
            0,
            0});
            this.AutoMinIntervalNum.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.AutoMinIntervalNum.Name = "AutoMinIntervalNum";
            this.AutoMinIntervalNum.Size = new System.Drawing.Size(69, 20);
            this.AutoMinIntervalNum.TabIndex = 20;
            this.AutoMinIntervalNum.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.AutoMinIntervalNum.ValueChanged += new System.EventHandler(this.AutoMinIntervalNum_ValueChanged);
            // 
            // CloseRoblox
            // 
            this.CloseRoblox.Location = new System.Drawing.Point(15, 199);
            this.CloseRoblox.Name = "CloseRoblox";
            this.CloseRoblox.Size = new System.Drawing.Size(145, 23);
            this.CloseRoblox.TabIndex = 13;
            this.CloseRoblox.Text = "Close Roblox";
            this.CloseRoblox.UseVisualStyleBackColor = true;
            this.CloseRoblox.Click += new System.EventHandler(this.CloseRoblox_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.AutoCloseType, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.ACLabel, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.AutoCloseIntervalNum, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.MaxInstanceLabel, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.MaxInstancesNum, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.AutoCloseCB, 0, 0);
            this.tableLayoutPanel1.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(166, 199);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(194, 79);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // AutoCloseType
            // 
            this.AutoCloseType.FormattingEnabled = true;
            this.AutoCloseType.ItemHeight = 13;
            this.AutoCloseType.Items.AddRange(new object[] {
            "Per Instance",
            "Global"});
            this.AutoCloseType.Location = new System.Drawing.Point(100, 3);
            this.AutoCloseType.Name = "AutoCloseType";
            this.AutoCloseType.Size = new System.Drawing.Size(90, 21);
            this.AutoCloseType.TabIndex = 21;
            this.AutoCloseType.Text = "Per Instance";
            this.AutoCloseType.SelectedIndexChanged += new System.EventHandler(this.AutoCloseType_SelectedIndexChanged);
            // 
            // ACLabel
            // 
            this.ACLabel.AutoSize = true;
            this.ACLabel.Location = new System.Drawing.Point(3, 27);
            this.ACLabel.Name = "ACLabel";
            this.ACLabel.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.ACLabel.Size = new System.Drawing.Size(67, 17);
            this.ACLabel.TabIndex = 17;
            this.ACLabel.Text = "Interval (min)";
            // 
            // AutoCloseIntervalNum
            // 
            this.AutoCloseIntervalNum.Location = new System.Drawing.Point(100, 30);
            this.AutoCloseIntervalNum.Maximum = new decimal(new int[] {
            6000,
            0,
            0,
            0});
            this.AutoCloseIntervalNum.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.AutoCloseIntervalNum.Name = "AutoCloseIntervalNum";
            this.AutoCloseIntervalNum.Size = new System.Drawing.Size(69, 20);
            this.AutoCloseIntervalNum.TabIndex = 18;
            this.AutoCloseIntervalNum.Value = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.AutoCloseIntervalNum.ValueChanged += new System.EventHandler(this.AutoCloseIntervalNum_ValueChanged);
            // 
            // MaxInstanceLabel
            // 
            this.MaxInstanceLabel.AutoSize = true;
            this.MaxInstanceLabel.Location = new System.Drawing.Point(3, 53);
            this.MaxInstanceLabel.Name = "MaxInstanceLabel";
            this.MaxInstanceLabel.Padding = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.MaxInstanceLabel.Size = new System.Drawing.Size(76, 17);
            this.MaxInstanceLabel.TabIndex = 27;
            this.MaxInstanceLabel.Text = "Max Instances";
            this.Helper.SetToolTip(this.MaxInstanceLabel, "Will close every single Roblox process if there\r\nare over a specified amount of i" +
        "nstances open");
            // 
            // MaxInstancesNum
            // 
            this.MaxInstancesNum.Location = new System.Drawing.Point(100, 56);
            this.MaxInstancesNum.Maximum = new decimal(new int[] {
            500,
            0,
            0,
            0});
            this.MaxInstancesNum.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.MaxInstancesNum.Name = "MaxInstancesNum";
            this.MaxInstancesNum.Size = new System.Drawing.Size(69, 20);
            this.MaxInstancesNum.TabIndex = 28;
            this.MaxInstancesNum.Value = new decimal(new int[] {
            25,
            0,
            0,
            0});
            this.MaxInstancesNum.ValueChanged += new System.EventHandler(this.MaxInstancesNum_ValueChanged);
            // 
            // AutoCloseCB
            // 
            this.AutoCloseCB.Location = new System.Drawing.Point(3, 7);
            this.AutoCloseCB.Margin = new System.Windows.Forms.Padding(3, 7, 3, 3);
            this.AutoCloseCB.Name = "AutoCloseCB";
            this.AutoCloseCB.Size = new System.Drawing.Size(91, 17);
            this.AutoCloseCB.TabIndex = 16;
            this.AutoCloseCB.Text = "Auto Close";
            this.AutoCloseCB.UseVisualStyleBackColor = true;
            this.AutoCloseCB.CheckedChanged += new System.EventHandler(this.AutoCloseCB_CheckedChanged);
            // 
            // HelpPage
            // 
            this.HelpPage.Controls.Add(this.label7);
            this.HelpPage.Controls.Add(this.NexusDocsButton);
            this.HelpPage.Controls.Add(this.label6);
            this.HelpPage.Controls.Add(this.NexusDL);
            this.HelpPage.Controls.Add(this.label5);
            this.HelpPage.Controls.Add(this.label4);
            this.HelpPage.Location = new System.Drawing.Point(4, 25);
            this.HelpPage.Name = "HelpPage";
            this.HelpPage.Size = new System.Drawing.Size(585, 365);
            this.HelpPage.TabIndex = 2;
            this.HelpPage.Text = "Help";
            this.HelpPage.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(8, 79);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(346, 39);
            this.label7.TabIndex = 5;
            this.label7.Text = resources.GetString("label7.Text");
            // 
            // NexusDocsButton
            // 
            this.NexusDocsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.NexusDocsButton.Location = new System.Drawing.Point(92, 337);
            this.NexusDocsButton.Name = "NexusDocsButton";
            this.NexusDocsButton.Size = new System.Drawing.Size(94, 23);
            this.NexusDocsButton.TabIndex = 4;
            this.NexusDocsButton.Text = "Documentation";
            this.NexusDocsButton.UseVisualStyleBackColor = true;
            this.NexusDocsButton.Click += new System.EventHandler(this.NexusDocsButton_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(8, 133);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(301, 13);
            this.label6.TabIndex = 3;
            this.label6.Text = "Found a bug? Make sure to report it in the discord or on github";
            // 
            // NexusDL
            // 
            this.NexusDL.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.NexusDL.Location = new System.Drawing.Point(11, 337);
            this.NexusDL.Name = "NexusDL";
            this.NexusDL.Size = new System.Drawing.Size(75, 23);
            this.NexusDL.TabIndex = 2;
            this.NexusDL.Text = "Nexus.lua";
            this.NexusDL.UseVisualStyleBackColor = true;
            this.NexusDL.Click += new System.EventHandler(this.NexusDL_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 53);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(417, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "To connect your accounts, make sure to download Nexus.lua into your autoexec fold" +
    "er";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(8, 13);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(327, 26);
            this.label4.TabIndex = 0;
            this.label4.Text = "To add an account into Account Control, simply select the accounts\r\nfrom the main" +
    " form and drag them down into the Control Panel";
            // 
            // AutoRelaunchTimer
            // 
            this.AutoRelaunchTimer.Enabled = true;
            this.AutoRelaunchTimer.Interval = 9000;
            this.AutoRelaunchTimer.Tick += new System.EventHandler(this.AutoRelaunchTimer_Tick);
            // 
            // MinimzeTimer
            // 
            this.MinimzeTimer.Interval = 5000;
            this.MinimzeTimer.Tick += new System.EventHandler(this.MinimzeTimer_Tick);
            // 
            // CloseTimer
            // 
            this.CloseTimer.Interval = 1200000;
            this.CloseTimer.Tick += new System.EventHandler(this.CloseTimer_Tick);
            // 
            // AccountControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(593, 394);
            this.Controls.Add(this.ACTabs);
            this.MinimumSize = new System.Drawing.Size(475, 200);
            this.Name = "AccountControl";
            this.ShowIcon = false;
            this.Text = "Account Control";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AccountControl_FormClosing);
            this.Load += new System.EventHandler(this.AccountControl_Load);
            this.ControlsPanel.ResumeLayout(false);
            this.ControlsPanel.PerformLayout();
            this.ScriptLayoutPanel.ResumeLayout(false);
            this.ScriptTabs.ResumeLayout(false);
            this.ExecutionPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ScriptBox)).EndInit();
            this.AutoexecPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.AutoExecuteScriptBox)).EndInit();
            this.OutputFlowPanel.ResumeLayout(false);
            this.OutputFlowPanel.PerformLayout();
            this.ACTabs.ResumeLayout(false);
            this.ControlPage.ResumeLayout(false);
            this.CPanel.ResumeLayout(false);
            this.CPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AccountsView)).EndInit();
            this.ACStrip.ResumeLayout(false);
            this.SettingsTab.ResumeLayout(false);
            this.SettingsLayoutPanel.ResumeLayout(false);
            this.SettingsLayoutPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RelaunchDelayNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LauncherDelayNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PortNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AutoMinIntervalNum)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AutoCloseIntervalNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MaxInstancesNum)).EndInit();
            this.HelpPage.ResumeLayout(false);
            this.HelpPage.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel ControlsPanel;
        private System.Windows.Forms.TabPage ControlPage;
        public BrightIdeasSoftware.ObjectListView AccountsView;
        private BrightIdeasSoftware.OLVColumn cStatus;
        private BrightIdeasSoftware.OLVColumn cUsername;
        private BrightIdeasSoftware.OLVColumn cJobId;
        private BrightIdeasSoftware.MultiImageRenderer StatusRenderer;
        private BrightIdeasSoftware.OLVColumn cCheckBoxes;
        private System.Windows.Forms.CheckBox AutoRelaunchCheckBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private FastColoredTextBoxNS.FastColoredTextBox ScriptBox;
        private System.Windows.Forms.Button SendCommand;
        private System.Windows.Forms.Button ExecuteButton;
        private System.Windows.Forms.Button ClearButton;
        private System.Windows.Forms.FlowLayoutPanel ScriptLayoutPanel;
        private System.Windows.Forms.Button ShowScriptBox;
        private System.Windows.Forms.Panel CPanel;
        private System.Windows.Forms.FlowLayoutPanel OutputFlowPanel;
        private System.Windows.Forms.Button OutputToggle;
        private System.Windows.Forms.FlowLayoutPanel OutputPanel;
        private System.Windows.Forms.Button ClearOutputButton;
        private System.Windows.Forms.CheckBox SaveOutputToFileCheck;
        private System.Windows.Forms.TabPage ExecutionPage;
        private System.Windows.Forms.TabPage AutoexecPage;
        private FastColoredTextBoxNS.FastColoredTextBox AutoExecuteScriptBox;
        private System.Windows.Forms.Button ClearAutoExecScript;
        private System.Windows.Forms.Timer AutoRelaunchTimer;
        private System.Windows.Forms.TabPage HelpPage;
        private System.Windows.Forms.Button NexusDL;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button NexusDocsButton;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ContextMenuStrip ACStrip;
        private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;
        private System.Windows.Forms.TabPage SettingsTab;
        private System.Windows.Forms.FlowLayoutPanel SettingsLayoutPanel;
        private System.Windows.Forms.Label RLLabel;
        private System.Windows.Forms.NumericUpDown RelaunchDelayNumber;
        private System.Windows.Forms.Label PortLabel;
        private System.Windows.Forms.NumericUpDown PortNumber;
        private System.Windows.Forms.CheckBox AllowExternalConnectionsCB;
        private System.Windows.Forms.ToolStripMenuItem copyJobIdToolStripMenuItem;
        private System.Windows.Forms.Button MinimizeRoblox;
        private System.Windows.Forms.Button CloseRoblox;
        private System.Windows.Forms.CheckBox AutoMinimizeCB;
        private System.Windows.Forms.Timer MinimzeTimer;
        private BorderedTextBox PlaceIdText;
        private BorderedTextBox JobIdText;
        private BorderedTextBox CommandText;
        private System.Windows.Forms.CheckBox StartOnLaunch;
        private NBTabControl ACTabs;
        private NBTabControl ScriptTabs;
        private CheckBox AutoCloseCB;
        private Label ACLabel;
        private Label label8;
        private NumericUpDown AutoMinIntervalNum;
        private NumericUpDown AutoCloseIntervalNum;
        private ComboBox AutoCloseType;
        private Timer CloseTimer;
        private Label LDLabel;
        private NumericUpDown LauncherDelayNumber;
        private CheckBox InternetCheckCB;
        private CheckBox UsePresenceCB;
        private ToolTip Helper;
        private Label MaxInstanceLabel;
        private NumericUpDown MaxInstancesNum;
        private TableLayoutPanel tableLayoutPanel1;
    }
}
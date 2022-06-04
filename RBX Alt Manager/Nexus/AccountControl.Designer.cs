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
            this.PlaceIdText = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.JobIdText = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.CommandText = new System.Windows.Forms.TextBox();
            this.SendCommand = new System.Windows.Forms.Button();
            this.ScriptLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.ShowScriptBox = new System.Windows.Forms.Button();
            this.ScriptTabs = new System.Windows.Forms.TabControl();
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
            this.ACTabs = new System.Windows.Forms.TabControl();
            this.ControlPage = new System.Windows.Forms.TabPage();
            this.CPanel = new System.Windows.Forms.Panel();
            this.AccountsView = new BrightIdeasSoftware.ObjectListView();
            this.cCheckBoxes = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.cStatus = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.StatusRenderer = new BrightIdeasSoftware.MultiImageRenderer();
            this.cUsername = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.cJobId = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.ACStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SettingsTab = new System.Windows.Forms.TabPage();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.AllowExternalConnectionsCB = new System.Windows.Forms.CheckBox();
            this.RLLabel = new System.Windows.Forms.Label();
            this.RelaunchDelayNumber = new System.Windows.Forms.NumericUpDown();
            this.PortLabel = new System.Windows.Forms.Label();
            this.PortNumber = new System.Windows.Forms.NumericUpDown();
            this.HelpPage = new System.Windows.Forms.TabPage();
            this.label7 = new System.Windows.Forms.Label();
            this.NexusDocsButton = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.NexusDL = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.AutoRelaunchTimer = new System.Windows.Forms.Timer(this.components);
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
            this.flowLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RelaunchDelayNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.PortNumber)).BeginInit();
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
            this.ScriptTabs.Location = new System.Drawing.Point(3, 12);
            this.ScriptTabs.Margin = new System.Windows.Forms.Padding(3, 1, 3, 3);
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
            this.ExecutionPage.Location = new System.Drawing.Point(4, 22);
            this.ExecutionPage.Name = "ExecutionPage";
            this.ExecutionPage.Padding = new System.Windows.Forms.Padding(3);
            this.ExecutionPage.Size = new System.Drawing.Size(254, 174);
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
            this.ScriptBox.AutoScrollMinSize = new System.Drawing.Size(195, 14);
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
            this.ScriptBox.Text = "print(\"Hello World!\")";
            this.ScriptBox.Zoom = 100;
            // 
            // ClearButton
            // 
            this.ClearButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ClearButton.Location = new System.Drawing.Point(173, 145);
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
            this.ExecuteButton.Location = new System.Drawing.Point(6, 145);
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
            this.AutoexecPage.Location = new System.Drawing.Point(4, 22);
            this.AutoexecPage.Name = "AutoexecPage";
            this.AutoexecPage.Padding = new System.Windows.Forms.Padding(3);
            this.AutoexecPage.Size = new System.Drawing.Size(254, 174);
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
            this.ClearAutoExecScript.Location = new System.Drawing.Point(173, 145);
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
            this.ControlPage.Location = new System.Drawing.Point(4, 22);
            this.ControlPage.Name = "ControlPage";
            this.ControlPage.Padding = new System.Windows.Forms.Padding(3);
            this.ControlPage.Size = new System.Drawing.Size(585, 368);
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
            this.removeToolStripMenuItem});
            this.ACStrip.Name = "ACStrip";
            this.ACStrip.Size = new System.Drawing.Size(118, 26);
            // 
            // removeToolStripMenuItem
            // 
            this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            this.removeToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.removeToolStripMenuItem.Text = "Remove";
            this.removeToolStripMenuItem.Click += new System.EventHandler(this.removeToolStripMenuItem_Click);
            // 
            // SettingsTab
            // 
            this.SettingsTab.Controls.Add(this.flowLayoutPanel2);
            this.SettingsTab.Location = new System.Drawing.Point(4, 22);
            this.SettingsTab.Name = "SettingsTab";
            this.SettingsTab.Padding = new System.Windows.Forms.Padding(3);
            this.SettingsTab.Size = new System.Drawing.Size(585, 368);
            this.SettingsTab.TabIndex = 3;
            this.SettingsTab.Text = "Settings";
            this.SettingsTab.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.AllowExternalConnectionsCB);
            this.flowLayoutPanel2.Controls.Add(this.RLLabel);
            this.flowLayoutPanel2.Controls.Add(this.RelaunchDelayNumber);
            this.flowLayoutPanel2.Controls.Add(this.PortLabel);
            this.flowLayoutPanel2.Controls.Add(this.PortNumber);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Padding = new System.Windows.Forms.Padding(12);
            this.flowLayoutPanel2.Size = new System.Drawing.Size(579, 362);
            this.flowLayoutPanel2.TabIndex = 2;
            // 
            // AllowExternalConnectionsCB
            // 
            this.AllowExternalConnectionsCB.AutoSize = true;
            this.flowLayoutPanel2.SetFlowBreak(this.AllowExternalConnectionsCB, true);
            this.AllowExternalConnectionsCB.Location = new System.Drawing.Point(15, 15);
            this.AllowExternalConnectionsCB.Name = "AllowExternalConnectionsCB";
            this.AllowExternalConnectionsCB.Size = new System.Drawing.Size(154, 17);
            this.AllowExternalConnectionsCB.TabIndex = 7;
            this.AllowExternalConnectionsCB.Text = "Allow External Connections";
            this.AllowExternalConnectionsCB.UseVisualStyleBackColor = true;
            this.AllowExternalConnectionsCB.CheckedChanged += new System.EventHandler(this.AllowExternalConnectionsCB_CheckedChanged);
            // 
            // RLLabel
            // 
            this.RLLabel.AutoSize = true;
            this.RLLabel.Location = new System.Drawing.Point(15, 39);
            this.RLLabel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 0);
            this.RLLabel.Name = "RLLabel";
            this.RLLabel.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RLLabel.Size = new System.Drawing.Size(83, 13);
            this.RLLabel.TabIndex = 11;
            this.RLLabel.Text = "Relaunch Delay";
            // 
            // RelaunchDelayNumber
            // 
            this.flowLayoutPanel2.SetFlowBreak(this.RelaunchDelayNumber, true);
            this.RelaunchDelayNumber.Location = new System.Drawing.Point(104, 36);
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
            // PortLabel
            // 
            this.PortLabel.AutoSize = true;
            this.PortLabel.Location = new System.Drawing.Point(15, 60);
            this.PortLabel.Margin = new System.Windows.Forms.Padding(3, 4, 60, 0);
            this.PortLabel.Name = "PortLabel";
            this.PortLabel.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.PortLabel.Size = new System.Drawing.Size(26, 13);
            this.PortLabel.TabIndex = 9;
            this.PortLabel.Text = "Port";
            // 
            // PortNumber
            // 
            this.flowLayoutPanel2.SetFlowBreak(this.PortNumber, true);
            this.PortNumber.Location = new System.Drawing.Point(104, 57);
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
            // HelpPage
            // 
            this.HelpPage.Controls.Add(this.label7);
            this.HelpPage.Controls.Add(this.NexusDocsButton);
            this.HelpPage.Controls.Add(this.label6);
            this.HelpPage.Controls.Add(this.NexusDL);
            this.HelpPage.Controls.Add(this.label5);
            this.HelpPage.Controls.Add(this.label4);
            this.HelpPage.Location = new System.Drawing.Point(4, 22);
            this.HelpPage.Name = "HelpPage";
            this.HelpPage.Size = new System.Drawing.Size(585, 368);
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
            this.NexusDocsButton.Size = new System.Drawing.Size(87, 23);
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
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.RelaunchDelayNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.PortNumber)).EndInit();
            this.HelpPage.ResumeLayout(false);
            this.HelpPage.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel ControlsPanel;
        private System.Windows.Forms.TabControl ACTabs;
        private System.Windows.Forms.TabPage ControlPage;
        public BrightIdeasSoftware.ObjectListView AccountsView;
        private BrightIdeasSoftware.OLVColumn cStatus;
        private BrightIdeasSoftware.OLVColumn cUsername;
        private BrightIdeasSoftware.OLVColumn cJobId;
        private BrightIdeasSoftware.MultiImageRenderer StatusRenderer;
        private BrightIdeasSoftware.OLVColumn cCheckBoxes;
        private System.Windows.Forms.CheckBox AutoRelaunchCheckBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox PlaceIdText;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox JobIdText;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox CommandText;
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
        private System.Windows.Forms.TabControl ScriptTabs;
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
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Label RLLabel;
        private System.Windows.Forms.NumericUpDown RelaunchDelayNumber;
        private System.Windows.Forms.Label PortLabel;
        private System.Windows.Forms.NumericUpDown PortNumber;
        private System.Windows.Forms.CheckBox AllowExternalConnectionsCB;
    }
}
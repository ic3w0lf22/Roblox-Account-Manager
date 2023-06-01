
using RBX_Alt_Manager.Classes;

namespace RBX_Alt_Manager.Forms
{
    partial class SettingsForm
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
            this.SettingsLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.AutoUpdateCB = new System.Windows.Forms.CheckBox();
            this.AsyncJoinCB = new System.Windows.Forms.CheckBox();
            this.DelayLabel = new System.Windows.Forms.Label();
            this.LaunchDelayNumber = new System.Windows.Forms.NumericUpDown();
            this.SavePasswordCB = new System.Windows.Forms.CheckBox();
            this.DisableAgingAlertCB = new System.Windows.Forms.CheckBox();
            this.HideMRobloxCB = new System.Windows.Forms.CheckBox();
            this.StartOnPCStartup = new System.Windows.Forms.CheckBox();
            this.ShuffleLowestServerCB = new System.Windows.Forms.CheckBox();
            this.MultiRobloxCB = new System.Windows.Forms.CheckBox();
            this.AutoCookieRefreshCB = new System.Windows.Forms.CheckBox();
            this.RegionFormatLabel = new System.Windows.Forms.Label();
            this.RegionFormatTB = new System.Windows.Forms.TextBox();
            this.MRGLabel = new System.Windows.Forms.Label();
            this.MaxRecentGamesNumber = new System.Windows.Forms.NumericUpDown();
            this.RSLabel = new System.Windows.Forms.Label();
            this.EncryptionSelectionButton = new System.Windows.Forms.Button();
            this.Helper = new System.Windows.Forms.ToolTip(this.components);
            this.WSPWLabel = new System.Windows.Forms.Label();
            this.PasswordTextBox = new System.Windows.Forms.TextBox();
            this.SettingsTC = new RBX_Alt_Manager.Classes.NBTabControl();
            this.GeneralTab = new System.Windows.Forms.TabPage();
            this.DeveloperTab = new System.Windows.Forms.TabPage();
            this.DevSettingsLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.EnableDMCB = new System.Windows.Forms.CheckBox();
            this.EnableWSCB = new System.Windows.Forms.CheckBox();
            this.PortLabel = new System.Windows.Forms.Label();
            this.PortNumber = new System.Windows.Forms.NumericUpDown();
            this.ERRPCB = new System.Windows.Forms.CheckBox();
            this.AllowGCCB = new System.Windows.Forms.CheckBox();
            this.AllowGACB = new System.Windows.Forms.CheckBox();
            this.AllowLACB = new System.Windows.Forms.CheckBox();
            this.AllowAECB = new System.Windows.Forms.CheckBox();
            this.DisableImagesCB = new System.Windows.Forms.CheckBox();
            this.AllowExternalConnectionsCB = new System.Windows.Forms.CheckBox();
            this.MiscellaneousTab = new System.Windows.Forms.TabPage();
            this.MiscellaneousFlowPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.PresenceCB = new System.Windows.Forms.CheckBox();
            this.PresenceUpdateLabel = new System.Windows.Forms.Label();
            this.PresenceUpdateRateNum = new System.Windows.Forms.NumericUpDown();
            this.UnlockFPSCB = new System.Windows.Forms.CheckBox();
            this.FPSCapLabel = new System.Windows.Forms.Label();
            this.MaxFPSValue = new System.Windows.Forms.NumericUpDown();
            this.OverrideWithCustomCB = new System.Windows.Forms.CheckBox();
            this.CustomClientSettingsDialog = new System.Windows.Forms.OpenFileDialog();
            this.ForceUpdateButton = new System.Windows.Forms.Button();
            this.SettingsLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LaunchDelayNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MaxRecentGamesNumber)).BeginInit();
            this.SettingsTC.SuspendLayout();
            this.GeneralTab.SuspendLayout();
            this.DeveloperTab.SuspendLayout();
            this.DevSettingsLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PortNumber)).BeginInit();
            this.MiscellaneousTab.SuspendLayout();
            this.MiscellaneousFlowPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PresenceUpdateRateNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MaxFPSValue)).BeginInit();
            this.SuspendLayout();
            // 
            // SettingsLayoutPanel
            // 
            this.SettingsLayoutPanel.Controls.Add(this.AutoUpdateCB);
            this.SettingsLayoutPanel.Controls.Add(this.AsyncJoinCB);
            this.SettingsLayoutPanel.Controls.Add(this.DelayLabel);
            this.SettingsLayoutPanel.Controls.Add(this.LaunchDelayNumber);
            this.SettingsLayoutPanel.Controls.Add(this.SavePasswordCB);
            this.SettingsLayoutPanel.Controls.Add(this.DisableAgingAlertCB);
            this.SettingsLayoutPanel.Controls.Add(this.HideMRobloxCB);
            this.SettingsLayoutPanel.Controls.Add(this.StartOnPCStartup);
            this.SettingsLayoutPanel.Controls.Add(this.ShuffleLowestServerCB);
            this.SettingsLayoutPanel.Controls.Add(this.MultiRobloxCB);
            this.SettingsLayoutPanel.Controls.Add(this.AutoCookieRefreshCB);
            this.SettingsLayoutPanel.Controls.Add(this.RegionFormatLabel);
            this.SettingsLayoutPanel.Controls.Add(this.RegionFormatTB);
            this.SettingsLayoutPanel.Controls.Add(this.MRGLabel);
            this.SettingsLayoutPanel.Controls.Add(this.MaxRecentGamesNumber);
            this.SettingsLayoutPanel.Controls.Add(this.RSLabel);
            this.SettingsLayoutPanel.Controls.Add(this.EncryptionSelectionButton);
            this.SettingsLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SettingsLayoutPanel.Location = new System.Drawing.Point(3, 3);
            this.SettingsLayoutPanel.Name = "SettingsLayoutPanel";
            this.SettingsLayoutPanel.Padding = new System.Windows.Forms.Padding(12);
            this.SettingsLayoutPanel.Size = new System.Drawing.Size(303, 334);
            this.SettingsLayoutPanel.TabIndex = 0;
            // 
            // AutoUpdateCB
            // 
            this.AutoUpdateCB.AutoSize = true;
            this.SettingsLayoutPanel.SetFlowBreak(this.AutoUpdateCB, true);
            this.AutoUpdateCB.Location = new System.Drawing.Point(15, 15);
            this.AutoUpdateCB.Name = "AutoUpdateCB";
            this.AutoUpdateCB.Size = new System.Drawing.Size(115, 17);
            this.AutoUpdateCB.TabIndex = 0;
            this.AutoUpdateCB.Text = "Check for Updates";
            this.AutoUpdateCB.UseVisualStyleBackColor = true;
            this.AutoUpdateCB.CheckedChanged += new System.EventHandler(this.AutoUpdateCB_CheckedChanged);
            // 
            // AsyncJoinCB
            // 
            this.AsyncJoinCB.AutoSize = true;
            this.AsyncJoinCB.Location = new System.Drawing.Point(15, 38);
            this.AsyncJoinCB.Margin = new System.Windows.Forms.Padding(3, 3, 20, 3);
            this.AsyncJoinCB.Name = "AsyncJoinCB";
            this.AsyncJoinCB.Size = new System.Drawing.Size(108, 17);
            this.AsyncJoinCB.TabIndex = 1;
            this.AsyncJoinCB.Text = "Async Launching";
            this.Helper.SetToolTip(this.AsyncJoinCB, "Bulk asynchrounous joining meaning it will wait until the first account is launch" +
        "ed, then proceeds to launch the next.");
            this.AsyncJoinCB.UseVisualStyleBackColor = true;
            this.AsyncJoinCB.CheckedChanged += new System.EventHandler(this.AsyncJoinCB_CheckedChanged);
            // 
            // DelayLabel
            // 
            this.DelayLabel.AutoSize = true;
            this.DelayLabel.Location = new System.Drawing.Point(146, 39);
            this.DelayLabel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 0);
            this.DelayLabel.Name = "DelayLabel";
            this.DelayLabel.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.DelayLabel.Size = new System.Drawing.Size(73, 13);
            this.DelayLabel.TabIndex = 11;
            this.DelayLabel.Text = "Launch Delay";
            // 
            // LaunchDelayNumber
            // 
            this.SettingsLayoutPanel.SetFlowBreak(this.LaunchDelayNumber, true);
            this.LaunchDelayNumber.Location = new System.Drawing.Point(225, 36);
            this.LaunchDelayNumber.Margin = new System.Windows.Forms.Padding(3, 1, 3, 0);
            this.LaunchDelayNumber.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.LaunchDelayNumber.Name = "LaunchDelayNumber";
            this.LaunchDelayNumber.Size = new System.Drawing.Size(56, 20);
            this.LaunchDelayNumber.TabIndex = 10;
            this.LaunchDelayNumber.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.LaunchDelayNumber.ValueChanged += new System.EventHandler(this.LaunchDelayNumber_ValueChanged);
            // 
            // SavePasswordCB
            // 
            this.SavePasswordCB.AutoSize = true;
            this.SettingsLayoutPanel.SetFlowBreak(this.SavePasswordCB, true);
            this.SavePasswordCB.Location = new System.Drawing.Point(15, 61);
            this.SavePasswordCB.Name = "SavePasswordCB";
            this.SavePasswordCB.Size = new System.Drawing.Size(105, 17);
            this.SavePasswordCB.TabIndex = 2;
            this.SavePasswordCB.Text = "Save Passwords";
            this.Helper.SetToolTip(this.SavePasswordCB, "Save passwords when logging in.");
            this.SavePasswordCB.UseVisualStyleBackColor = true;
            this.SavePasswordCB.CheckedChanged += new System.EventHandler(this.SavePasswordCB_CheckedChanged);
            // 
            // DisableAgingAlertCB
            // 
            this.DisableAgingAlertCB.AutoSize = true;
            this.SettingsLayoutPanel.SetFlowBreak(this.DisableAgingAlertCB, true);
            this.DisableAgingAlertCB.Location = new System.Drawing.Point(15, 84);
            this.DisableAgingAlertCB.Name = "DisableAgingAlertCB";
            this.DisableAgingAlertCB.Size = new System.Drawing.Size(115, 17);
            this.DisableAgingAlertCB.TabIndex = 3;
            this.DisableAgingAlertCB.Text = "Disable Aging Alert";
            this.Helper.SetToolTip(this.DisableAgingAlertCB, "The \"aging alert\" is those yellow/red dots that appear on an account when an acco" +
        "unt hasn\'t been used in up to 20+ days.");
            this.DisableAgingAlertCB.UseVisualStyleBackColor = true;
            this.DisableAgingAlertCB.CheckedChanged += new System.EventHandler(this.DisableAgingAlertCB_CheckedChanged);
            // 
            // HideMRobloxCB
            // 
            this.HideMRobloxCB.AutoSize = true;
            this.SettingsLayoutPanel.SetFlowBreak(this.HideMRobloxCB, true);
            this.HideMRobloxCB.Location = new System.Drawing.Point(15, 107);
            this.HideMRobloxCB.Name = "HideMRobloxCB";
            this.HideMRobloxCB.Size = new System.Drawing.Size(133, 17);
            this.HideMRobloxCB.TabIndex = 4;
            this.HideMRobloxCB.Text = "Hide Multi Roblox Alert";
            this.HideMRobloxCB.UseVisualStyleBackColor = true;
            this.HideMRobloxCB.CheckedChanged += new System.EventHandler(this.HideMRobloxCB_CheckedChanged);
            // 
            // StartOnPCStartup
            // 
            this.StartOnPCStartup.AutoSize = true;
            this.SettingsLayoutPanel.SetFlowBreak(this.StartOnPCStartup, true);
            this.StartOnPCStartup.Location = new System.Drawing.Point(15, 130);
            this.StartOnPCStartup.Name = "StartOnPCStartup";
            this.StartOnPCStartup.Size = new System.Drawing.Size(145, 17);
            this.StartOnPCStartup.TabIndex = 21;
            this.StartOnPCStartup.Text = "Run on Windows Startup";
            this.StartOnPCStartup.UseVisualStyleBackColor = true;
            this.StartOnPCStartup.CheckedChanged += new System.EventHandler(this.StartOnPCStartup_CheckedChanged);
            // 
            // ShuffleLowestServerCB
            // 
            this.ShuffleLowestServerCB.AutoSize = true;
            this.SettingsLayoutPanel.SetFlowBreak(this.ShuffleLowestServerCB, true);
            this.ShuffleLowestServerCB.Location = new System.Drawing.Point(15, 153);
            this.ShuffleLowestServerCB.Name = "ShuffleLowestServerCB";
            this.ShuffleLowestServerCB.Size = new System.Drawing.Size(174, 17);
            this.ShuffleLowestServerCB.TabIndex = 22;
            this.ShuffleLowestServerCB.Text = "Shuffle Chooses Lowest Server";
            this.ShuffleLowestServerCB.UseVisualStyleBackColor = true;
            this.ShuffleLowestServerCB.CheckedChanged += new System.EventHandler(this.ShuffleLowestServerCB_CheckedChanged);
            // 
            // MultiRobloxCB
            // 
            this.MultiRobloxCB.AutoSize = true;
            this.SettingsLayoutPanel.SetFlowBreak(this.MultiRobloxCB, true);
            this.MultiRobloxCB.Location = new System.Drawing.Point(15, 176);
            this.MultiRobloxCB.Name = "MultiRobloxCB";
            this.MultiRobloxCB.Size = new System.Drawing.Size(84, 17);
            this.MultiRobloxCB.TabIndex = 24;
            this.MultiRobloxCB.Text = "Multi Roblox";
            this.MultiRobloxCB.UseVisualStyleBackColor = true;
            this.MultiRobloxCB.CheckedChanged += new System.EventHandler(this.MultiRobloxCB_CheckedChanged);
            // 
            // AutoCookieRefreshCB
            // 
            this.AutoCookieRefreshCB.AutoSize = true;
            this.SettingsLayoutPanel.SetFlowBreak(this.AutoCookieRefreshCB, true);
            this.AutoCookieRefreshCB.Location = new System.Drawing.Point(15, 199);
            this.AutoCookieRefreshCB.Name = "AutoCookieRefreshCB";
            this.AutoCookieRefreshCB.Size = new System.Drawing.Size(129, 17);
            this.AutoCookieRefreshCB.TabIndex = 25;
            this.AutoCookieRefreshCB.Text = "Auto Refresh Cookies";
            this.Helper.SetToolTip(this.AutoCookieRefreshCB, "Warning: Logs out of other account settings");
            this.AutoCookieRefreshCB.UseVisualStyleBackColor = true;
            this.AutoCookieRefreshCB.CheckedChanged += new System.EventHandler(this.AutoCookieRefreshCB_CheckedChanged);
            // 
            // RegionFormatLabel
            // 
            this.RegionFormatLabel.AutoSize = true;
            this.RegionFormatLabel.Location = new System.Drawing.Point(15, 223);
            this.RegionFormatLabel.Margin = new System.Windows.Forms.Padding(3, 4, 35, 0);
            this.RegionFormatLabel.Name = "RegionFormatLabel";
            this.RegionFormatLabel.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.RegionFormatLabel.Size = new System.Drawing.Size(76, 13);
            this.RegionFormatLabel.TabIndex = 17;
            this.RegionFormatLabel.Text = "Region Format";
            this.Helper.SetToolTip(this.RegionFormatLabel, "Requires 6 or more characters, not recommended to use your main password\r\n\r\n");
            // 
            // RegionFormatTB
            // 
            this.SettingsLayoutPanel.SetFlowBreak(this.RegionFormatTB, true);
            this.RegionFormatTB.Location = new System.Drawing.Point(129, 222);
            this.RegionFormatTB.Name = "RegionFormatTB";
            this.RegionFormatTB.Size = new System.Drawing.Size(152, 20);
            this.RegionFormatTB.TabIndex = 18;
            this.Helper.SetToolTip(this.RegionFormatTB, "Requires 6 or more characters, not recommended to use your main password");
            this.RegionFormatTB.TextChanged += new System.EventHandler(this.RegionFormatTB_TextChanged);
            // 
            // MRGLabel
            // 
            this.MRGLabel.AutoSize = true;
            this.MRGLabel.Location = new System.Drawing.Point(15, 249);
            this.MRGLabel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 0);
            this.MRGLabel.Name = "MRGLabel";
            this.MRGLabel.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.MRGLabel.Size = new System.Drawing.Size(101, 13);
            this.MRGLabel.TabIndex = 20;
            this.MRGLabel.Text = "Max Recent Games";
            // 
            // MaxRecentGamesNumber
            // 
            this.SettingsLayoutPanel.SetFlowBreak(this.MaxRecentGamesNumber, true);
            this.MaxRecentGamesNumber.Location = new System.Drawing.Point(129, 246);
            this.MaxRecentGamesNumber.Margin = new System.Windows.Forms.Padding(10, 1, 3, 0);
            this.MaxRecentGamesNumber.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.MaxRecentGamesNumber.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.MaxRecentGamesNumber.Name = "MaxRecentGamesNumber";
            this.MaxRecentGamesNumber.Size = new System.Drawing.Size(56, 20);
            this.MaxRecentGamesNumber.TabIndex = 19;
            this.MaxRecentGamesNumber.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.MaxRecentGamesNumber.ValueChanged += new System.EventHandler(this.MaxRecentGamesNumber_ValueChanged);
            // 
            // RSLabel
            // 
            this.RSLabel.AutoSize = true;
            this.SettingsLayoutPanel.SetFlowBreak(this.RSLabel, true);
            this.RSLabel.Location = new System.Drawing.Point(15, 270);
            this.RSLabel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 0);
            this.RSLabel.Name = "RSLabel";
            this.RSLabel.Size = new System.Drawing.Size(263, 26);
            this.RSLabel.TabIndex = 12;
            this.RSLabel.Text = "Some settings may require the program to be restarted such as WebServer Port and " +
    "Disable Aging Alert";
            // 
            // EncryptionSelectionButton
            // 
            this.SettingsLayoutPanel.SetFlowBreak(this.EncryptionSelectionButton, true);
            this.EncryptionSelectionButton.Location = new System.Drawing.Point(15, 299);
            this.EncryptionSelectionButton.Name = "EncryptionSelectionButton";
            this.EncryptionSelectionButton.Size = new System.Drawing.Size(263, 23);
            this.EncryptionSelectionButton.TabIndex = 23;
            this.EncryptionSelectionButton.Text = "Reset Encryption Method";
            this.EncryptionSelectionButton.UseVisualStyleBackColor = true;
            this.EncryptionSelectionButton.Click += new System.EventHandler(this.EncryptionSelectionButton_Click);
            // 
            // WSPWLabel
            // 
            this.WSPWLabel.AutoSize = true;
            this.WSPWLabel.Location = new System.Drawing.Point(15, 223);
            this.WSPWLabel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 0);
            this.WSPWLabel.Name = "WSPWLabel";
            this.WSPWLabel.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.WSPWLabel.Size = new System.Drawing.Size(108, 13);
            this.WSPWLabel.TabIndex = 15;
            this.WSPWLabel.Text = "Webserver Password";
            this.Helper.SetToolTip(this.WSPWLabel, "Requires 6 or more characters, not recommended to use your main password\r\n\r\n");
            // 
            // PasswordTextBox
            // 
            this.PasswordTextBox.Location = new System.Drawing.Point(129, 222);
            this.PasswordTextBox.Name = "PasswordTextBox";
            this.PasswordTextBox.Size = new System.Drawing.Size(152, 20);
            this.PasswordTextBox.TabIndex = 16;
            this.Helper.SetToolTip(this.PasswordTextBox, "Requires 6 or more characters, not recommended to use your main password");
            this.PasswordTextBox.TextChanged += new System.EventHandler(this.PasswordTextBox_TextChanged);
            // 
            // SettingsTC
            // 
            this.SettingsTC.Controls.Add(this.GeneralTab);
            this.SettingsTC.Controls.Add(this.DeveloperTab);
            this.SettingsTC.Controls.Add(this.MiscellaneousTab);
            this.SettingsTC.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SettingsTC.Location = new System.Drawing.Point(0, 0);
            this.SettingsTC.Name = "SettingsTC";
            this.SettingsTC.SelectedIndex = 0;
            this.SettingsTC.Size = new System.Drawing.Size(317, 369);
            this.SettingsTC.TabIndex = 18;
            // 
            // GeneralTab
            // 
            this.GeneralTab.Controls.Add(this.SettingsLayoutPanel);
            this.GeneralTab.Location = new System.Drawing.Point(4, 25);
            this.GeneralTab.Name = "GeneralTab";
            this.GeneralTab.Padding = new System.Windows.Forms.Padding(3);
            this.GeneralTab.Size = new System.Drawing.Size(309, 340);
            this.GeneralTab.TabIndex = 0;
            this.GeneralTab.Text = "General";
            this.GeneralTab.UseVisualStyleBackColor = true;
            // 
            // DeveloperTab
            // 
            this.DeveloperTab.BackColor = System.Drawing.Color.Transparent;
            this.DeveloperTab.Controls.Add(this.DevSettingsLayoutPanel);
            this.DeveloperTab.Location = new System.Drawing.Point(4, 25);
            this.DeveloperTab.Name = "DeveloperTab";
            this.DeveloperTab.Padding = new System.Windows.Forms.Padding(3);
            this.DeveloperTab.Size = new System.Drawing.Size(309, 340);
            this.DeveloperTab.TabIndex = 1;
            this.DeveloperTab.Text = "Developer";
            // 
            // DevSettingsLayoutPanel
            // 
            this.DevSettingsLayoutPanel.BackColor = System.Drawing.Color.Transparent;
            this.DevSettingsLayoutPanel.Controls.Add(this.EnableDMCB);
            this.DevSettingsLayoutPanel.Controls.Add(this.EnableWSCB);
            this.DevSettingsLayoutPanel.Controls.Add(this.PortLabel);
            this.DevSettingsLayoutPanel.Controls.Add(this.PortNumber);
            this.DevSettingsLayoutPanel.Controls.Add(this.ERRPCB);
            this.DevSettingsLayoutPanel.Controls.Add(this.AllowGCCB);
            this.DevSettingsLayoutPanel.Controls.Add(this.AllowGACB);
            this.DevSettingsLayoutPanel.Controls.Add(this.AllowLACB);
            this.DevSettingsLayoutPanel.Controls.Add(this.AllowAECB);
            this.DevSettingsLayoutPanel.Controls.Add(this.DisableImagesCB);
            this.DevSettingsLayoutPanel.Controls.Add(this.AllowExternalConnectionsCB);
            this.DevSettingsLayoutPanel.Controls.Add(this.WSPWLabel);
            this.DevSettingsLayoutPanel.Controls.Add(this.PasswordTextBox);
            this.DevSettingsLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DevSettingsLayoutPanel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.DevSettingsLayoutPanel.Location = new System.Drawing.Point(3, 3);
            this.DevSettingsLayoutPanel.Name = "DevSettingsLayoutPanel";
            this.DevSettingsLayoutPanel.Padding = new System.Windows.Forms.Padding(12);
            this.DevSettingsLayoutPanel.Size = new System.Drawing.Size(303, 334);
            this.DevSettingsLayoutPanel.TabIndex = 2;
            // 
            // EnableDMCB
            // 
            this.EnableDMCB.AutoSize = true;
            this.DevSettingsLayoutPanel.SetFlowBreak(this.EnableDMCB, true);
            this.EnableDMCB.Location = new System.Drawing.Point(15, 15);
            this.EnableDMCB.Name = "EnableDMCB";
            this.EnableDMCB.Size = new System.Drawing.Size(141, 17);
            this.EnableDMCB.TabIndex = 5;
            this.EnableDMCB.Text = "Enable Developer Mode";
            this.EnableDMCB.UseVisualStyleBackColor = true;
            this.EnableDMCB.CheckedChanged += new System.EventHandler(this.EnableDMCB_CheckedChanged);
            // 
            // EnableWSCB
            // 
            this.EnableWSCB.AutoSize = true;
            this.EnableWSCB.Location = new System.Drawing.Point(15, 38);
            this.EnableWSCB.Margin = new System.Windows.Forms.Padding(3, 3, 35, 3);
            this.EnableWSCB.Name = "EnableWSCB";
            this.EnableWSCB.Size = new System.Drawing.Size(119, 17);
            this.EnableWSCB.TabIndex = 6;
            this.EnableWSCB.Text = "Enable Web Server";
            this.EnableWSCB.UseVisualStyleBackColor = true;
            this.EnableWSCB.CheckedChanged += new System.EventHandler(this.EnableWSCB_CheckedChanged);
            // 
            // PortLabel
            // 
            this.PortLabel.AutoSize = true;
            this.PortLabel.Location = new System.Drawing.Point(172, 39);
            this.PortLabel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 0);
            this.PortLabel.Name = "PortLabel";
            this.PortLabel.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.PortLabel.Size = new System.Drawing.Size(26, 13);
            this.PortLabel.TabIndex = 9;
            this.PortLabel.Text = "Port";
            // 
            // PortNumber
            // 
            this.DevSettingsLayoutPanel.SetFlowBreak(this.PortNumber, true);
            this.PortNumber.Location = new System.Drawing.Point(204, 36);
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
            // ERRPCB
            // 
            this.ERRPCB.AutoSize = true;
            this.DevSettingsLayoutPanel.SetFlowBreak(this.ERRPCB, true);
            this.ERRPCB.Location = new System.Drawing.Point(15, 61);
            this.ERRPCB.Name = "ERRPCB";
            this.ERRPCB.Size = new System.Drawing.Size(190, 17);
            this.ERRPCB.TabIndex = 17;
            this.ERRPCB.Text = "Every Request Requires Password";
            this.ERRPCB.UseVisualStyleBackColor = true;
            this.ERRPCB.CheckedChanged += new System.EventHandler(this.ERRPCB_CheckedChanged);
            // 
            // AllowGCCB
            // 
            this.AllowGCCB.AutoSize = true;
            this.DevSettingsLayoutPanel.SetFlowBreak(this.AllowGCCB, true);
            this.AllowGCCB.Location = new System.Drawing.Point(15, 84);
            this.AllowGCCB.Name = "AllowGCCB";
            this.AllowGCCB.Size = new System.Drawing.Size(143, 17);
            this.AllowGCCB.TabIndex = 7;
            this.AllowGCCB.Text = "Allow GetCookie Method";
            this.AllowGCCB.UseVisualStyleBackColor = true;
            this.AllowGCCB.CheckedChanged += new System.EventHandler(this.AllowGCCB_CheckedChanged);
            // 
            // AllowGACB
            // 
            this.AllowGACB.AutoSize = true;
            this.DevSettingsLayoutPanel.SetFlowBreak(this.AllowGACB, true);
            this.AllowGACB.Location = new System.Drawing.Point(15, 107);
            this.AllowGACB.Name = "AllowGACB";
            this.AllowGACB.Size = new System.Drawing.Size(155, 17);
            this.AllowGACB.TabIndex = 12;
            this.AllowGACB.Text = "Allow GetAccounts Method";
            this.AllowGACB.UseVisualStyleBackColor = true;
            this.AllowGACB.CheckedChanged += new System.EventHandler(this.AllowGACB_CheckedChanged);
            // 
            // AllowLACB
            // 
            this.AllowLACB.AutoSize = true;
            this.DevSettingsLayoutPanel.SetFlowBreak(this.AllowLACB, true);
            this.AllowLACB.Location = new System.Drawing.Point(15, 130);
            this.AllowLACB.Name = "AllowLACB";
            this.AllowLACB.Size = new System.Drawing.Size(169, 17);
            this.AllowLACB.TabIndex = 13;
            this.AllowLACB.Text = "Allow LaunchAccount Method";
            this.AllowLACB.UseVisualStyleBackColor = true;
            this.AllowLACB.CheckedChanged += new System.EventHandler(this.AllowLACB_CheckedChanged);
            // 
            // AllowAECB
            // 
            this.AllowAECB.AutoSize = true;
            this.DevSettingsLayoutPanel.SetFlowBreak(this.AllowAECB, true);
            this.AllowAECB.Location = new System.Drawing.Point(15, 153);
            this.AllowAECB.Name = "AllowAECB";
            this.AllowAECB.Size = new System.Drawing.Size(198, 17);
            this.AllowAECB.TabIndex = 14;
            this.AllowAECB.Text = "Allow Account Modification Methods";
            this.AllowAECB.UseVisualStyleBackColor = true;
            this.AllowAECB.CheckedChanged += new System.EventHandler(this.AllowAECB_CheckedChanged);
            // 
            // DisableImagesCB
            // 
            this.DisableImagesCB.AutoSize = true;
            this.DevSettingsLayoutPanel.SetFlowBreak(this.DisableImagesCB, true);
            this.DisableImagesCB.Location = new System.Drawing.Point(15, 176);
            this.DisableImagesCB.Name = "DisableImagesCB";
            this.DisableImagesCB.Size = new System.Drawing.Size(201, 17);
            this.DisableImagesCB.TabIndex = 18;
            this.DisableImagesCB.Text = "Disable Image Loading [Less Ram %]";
            this.DisableImagesCB.UseVisualStyleBackColor = true;
            this.DisableImagesCB.CheckedChanged += new System.EventHandler(this.DisableImagesCB_CheckedChanged);
            // 
            // AllowExternalConnectionsCB
            // 
            this.AllowExternalConnectionsCB.AutoSize = true;
            this.DevSettingsLayoutPanel.SetFlowBreak(this.AllowExternalConnectionsCB, true);
            this.AllowExternalConnectionsCB.Location = new System.Drawing.Point(15, 199);
            this.AllowExternalConnectionsCB.Name = "AllowExternalConnectionsCB";
            this.AllowExternalConnectionsCB.Size = new System.Drawing.Size(154, 17);
            this.AllowExternalConnectionsCB.TabIndex = 19;
            this.AllowExternalConnectionsCB.Text = "Allow External Connections";
            this.AllowExternalConnectionsCB.UseVisualStyleBackColor = true;
            this.AllowExternalConnectionsCB.CheckedChanged += new System.EventHandler(this.AllowExternalConnectionsCB_CheckedChanged);
            // 
            // MiscellaneousTab
            // 
            this.MiscellaneousTab.Controls.Add(this.MiscellaneousFlowPanel);
            this.MiscellaneousTab.Location = new System.Drawing.Point(4, 25);
            this.MiscellaneousTab.Name = "MiscellaneousTab";
            this.MiscellaneousTab.Padding = new System.Windows.Forms.Padding(3);
            this.MiscellaneousTab.Size = new System.Drawing.Size(309, 340);
            this.MiscellaneousTab.TabIndex = 2;
            this.MiscellaneousTab.Text = "Miscellaneous";
            this.MiscellaneousTab.UseVisualStyleBackColor = true;
            // 
            // MiscellaneousFlowPanel
            // 
            this.MiscellaneousFlowPanel.Controls.Add(this.PresenceCB);
            this.MiscellaneousFlowPanel.Controls.Add(this.PresenceUpdateLabel);
            this.MiscellaneousFlowPanel.Controls.Add(this.PresenceUpdateRateNum);
            this.MiscellaneousFlowPanel.Controls.Add(this.UnlockFPSCB);
            this.MiscellaneousFlowPanel.Controls.Add(this.FPSCapLabel);
            this.MiscellaneousFlowPanel.Controls.Add(this.MaxFPSValue);
            this.MiscellaneousFlowPanel.Controls.Add(this.OverrideWithCustomCB);
            this.MiscellaneousFlowPanel.Controls.Add(this.ForceUpdateButton);
            this.MiscellaneousFlowPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MiscellaneousFlowPanel.Location = new System.Drawing.Point(3, 3);
            this.MiscellaneousFlowPanel.Name = "MiscellaneousFlowPanel";
            this.MiscellaneousFlowPanel.Padding = new System.Windows.Forms.Padding(12);
            this.MiscellaneousFlowPanel.Size = new System.Drawing.Size(303, 334);
            this.MiscellaneousFlowPanel.TabIndex = 1;
            // 
            // PresenceCB
            // 
            this.PresenceCB.AutoSize = true;
            this.PresenceCB.Location = new System.Drawing.Point(15, 15);
            this.PresenceCB.Name = "PresenceCB";
            this.PresenceCB.Size = new System.Drawing.Size(144, 17);
            this.PresenceCB.TabIndex = 13;
            this.PresenceCB.Text = "Show Account Presence";
            this.PresenceCB.UseVisualStyleBackColor = true;
            this.PresenceCB.CheckedChanged += new System.EventHandler(this.PresenceCB_CheckedChanged);
            // 
            // PresenceUpdateLabel
            // 
            this.PresenceUpdateLabel.AutoSize = true;
            this.PresenceUpdateLabel.Location = new System.Drawing.Point(165, 16);
            this.PresenceUpdateLabel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 0);
            this.PresenceUpdateLabel.Name = "PresenceUpdateLabel";
            this.PresenceUpdateLabel.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.PresenceUpdateLabel.Size = new System.Drawing.Size(69, 13);
            this.PresenceUpdateLabel.TabIndex = 15;
            this.PresenceUpdateLabel.Text = "Refresh (min)";
            // 
            // PresenceUpdateRateNum
            // 
            this.MiscellaneousFlowPanel.SetFlowBreak(this.PresenceUpdateRateNum, true);
            this.PresenceUpdateRateNum.Location = new System.Drawing.Point(240, 13);
            this.PresenceUpdateRateNum.Margin = new System.Windows.Forms.Padding(3, 1, 3, 0);
            this.PresenceUpdateRateNum.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.PresenceUpdateRateNum.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.PresenceUpdateRateNum.Name = "PresenceUpdateRateNum";
            this.PresenceUpdateRateNum.Size = new System.Drawing.Size(44, 20);
            this.PresenceUpdateRateNum.TabIndex = 14;
            this.PresenceUpdateRateNum.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.PresenceUpdateRateNum.ValueChanged += new System.EventHandler(this.PresenceUpdateRateNum_ValueChanged);
            // 
            // UnlockFPSCB
            // 
            this.UnlockFPSCB.AutoSize = true;
            this.UnlockFPSCB.Location = new System.Drawing.Point(15, 38);
            this.UnlockFPSCB.Margin = new System.Windows.Forms.Padding(3, 3, 60, 3);
            this.UnlockFPSCB.Name = "UnlockFPSCB";
            this.UnlockFPSCB.Size = new System.Drawing.Size(83, 17);
            this.UnlockFPSCB.TabIndex = 0;
            this.UnlockFPSCB.Text = "Unlock FPS";
            this.UnlockFPSCB.UseVisualStyleBackColor = true;
            this.UnlockFPSCB.CheckedChanged += new System.EventHandler(this.UnlockFPSCB_CheckedChanged);
            // 
            // FPSCapLabel
            // 
            this.FPSCapLabel.AutoSize = true;
            this.FPSCapLabel.Location = new System.Drawing.Point(161, 39);
            this.FPSCapLabel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 0);
            this.FPSCapLabel.Name = "FPSCapLabel";
            this.FPSCapLabel.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.FPSCapLabel.Size = new System.Drawing.Size(50, 13);
            this.FPSCapLabel.TabIndex = 11;
            this.FPSCapLabel.Text = "Max FPS";
            // 
            // MaxFPSValue
            // 
            this.MiscellaneousFlowPanel.SetFlowBreak(this.MaxFPSValue, true);
            this.MaxFPSValue.Location = new System.Drawing.Point(217, 36);
            this.MaxFPSValue.Margin = new System.Windows.Forms.Padding(3, 1, 3, 0);
            this.MaxFPSValue.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.MaxFPSValue.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.MaxFPSValue.Name = "MaxFPSValue";
            this.MaxFPSValue.Size = new System.Drawing.Size(56, 20);
            this.MaxFPSValue.TabIndex = 10;
            this.MaxFPSValue.Value = new decimal(new int[] {
            120,
            0,
            0,
            0});
            this.MaxFPSValue.ValueChanged += new System.EventHandler(this.MaxFPSValue_ValueChanged);
            // 
            // OverrideWithCustomCB
            // 
            this.OverrideWithCustomCB.AutoSize = true;
            this.OverrideWithCustomCB.Location = new System.Drawing.Point(15, 61);
            this.OverrideWithCustomCB.Margin = new System.Windows.Forms.Padding(3, 3, 60, 3);
            this.OverrideWithCustomCB.Name = "OverrideWithCustomCB";
            this.OverrideWithCustomCB.Size = new System.Drawing.Size(169, 17);
            this.OverrideWithCustomCB.TabIndex = 12;
            this.OverrideWithCustomCB.Text = "Use Custom ClientAppSettings";
            this.OverrideWithCustomCB.UseVisualStyleBackColor = true;
            this.OverrideWithCustomCB.CheckedChanged += new System.EventHandler(this.OverrideWithCustomCB_CheckedChanged);
            // 
            // CustomClientSettingsDialog
            // 
            this.CustomClientSettingsDialog.DefaultExt = "json";
            this.CustomClientSettingsDialog.FileName = "ClientAppSettings.json";
            this.CustomClientSettingsDialog.Filter = "Json Files|*.json|All Files|*.*";
            // 
            // ForceUpdateButton
            // 
            this.ForceUpdateButton.Location = new System.Drawing.Point(15, 84);
            this.ForceUpdateButton.Name = "ForceUpdateButton";
            this.ForceUpdateButton.Size = new System.Drawing.Size(269, 23);
            this.ForceUpdateButton.TabIndex = 16;
            this.ForceUpdateButton.Text = "Force Update";
            this.ForceUpdateButton.UseVisualStyleBackColor = true;
            this.ForceUpdateButton.Click += new System.EventHandler(this.ForceUpdateButton_Click);
            // 
            // SettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(317, 369);
            this.Controls.Add(this.SettingsTC);
            this.MinimumSize = new System.Drawing.Size(333, 408);
            this.Name = "SettingsForm";
            this.ShowIcon = false;
            this.Text = "Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SettingsForm_FormClosing);
            this.Load += new System.EventHandler(this.SettingsForm_Load);
            this.SettingsLayoutPanel.ResumeLayout(false);
            this.SettingsLayoutPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LaunchDelayNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MaxRecentGamesNumber)).EndInit();
            this.SettingsTC.ResumeLayout(false);
            this.GeneralTab.ResumeLayout(false);
            this.DeveloperTab.ResumeLayout(false);
            this.DevSettingsLayoutPanel.ResumeLayout(false);
            this.DevSettingsLayoutPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PortNumber)).EndInit();
            this.MiscellaneousTab.ResumeLayout(false);
            this.MiscellaneousFlowPanel.ResumeLayout(false);
            this.MiscellaneousFlowPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PresenceUpdateRateNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MaxFPSValue)).EndInit();
            this.ResumeLayout(false);

        }

#endregion

        private System.Windows.Forms.FlowLayoutPanel SettingsLayoutPanel;
        private System.Windows.Forms.CheckBox AutoUpdateCB;
        private System.Windows.Forms.CheckBox AsyncJoinCB;
        private System.Windows.Forms.CheckBox SavePasswordCB;
        private System.Windows.Forms.CheckBox DisableAgingAlertCB;
        private System.Windows.Forms.CheckBox HideMRobloxCB;
        private System.Windows.Forms.Label DelayLabel;
        private System.Windows.Forms.NumericUpDown LaunchDelayNumber;
        private System.Windows.Forms.ToolTip Helper;
        private System.Windows.Forms.TabPage GeneralTab;
        private System.Windows.Forms.TabPage DeveloperTab;
        private System.Windows.Forms.FlowLayoutPanel DevSettingsLayoutPanel;
        private System.Windows.Forms.CheckBox EnableDMCB;
        private System.Windows.Forms.CheckBox EnableWSCB;
        private System.Windows.Forms.Label PortLabel;
        private System.Windows.Forms.NumericUpDown PortNumber;
        private System.Windows.Forms.CheckBox AllowGCCB;
        private System.Windows.Forms.CheckBox AllowGACB;
        private System.Windows.Forms.CheckBox AllowLACB;
        private System.Windows.Forms.CheckBox AllowAECB;
        private System.Windows.Forms.Label WSPWLabel;
        private System.Windows.Forms.TextBox PasswordTextBox;
        private System.Windows.Forms.CheckBox ERRPCB;
        private System.Windows.Forms.Label RSLabel;
        private System.Windows.Forms.Label RegionFormatLabel;
        private System.Windows.Forms.TextBox RegionFormatTB;
        private System.Windows.Forms.Label MRGLabel;
        private System.Windows.Forms.NumericUpDown MaxRecentGamesNumber;
        private System.Windows.Forms.CheckBox StartOnPCStartup;
        private System.Windows.Forms.CheckBox DisableImagesCB;
        private System.Windows.Forms.CheckBox ShuffleLowestServerCB;
        private NBTabControl SettingsTC;
        private System.Windows.Forms.CheckBox AllowExternalConnectionsCB;
        private System.Windows.Forms.TabPage MiscellaneousTab;
        private System.Windows.Forms.FlowLayoutPanel MiscellaneousFlowPanel;
        private System.Windows.Forms.CheckBox UnlockFPSCB;
        private System.Windows.Forms.Label FPSCapLabel;
        private System.Windows.Forms.NumericUpDown MaxFPSValue;
        private System.Windows.Forms.CheckBox OverrideWithCustomCB;
        private System.Windows.Forms.Button EncryptionSelectionButton;
        private System.Windows.Forms.CheckBox MultiRobloxCB;
        private System.Windows.Forms.CheckBox PresenceCB;
        private System.Windows.Forms.Label PresenceUpdateLabel;
        private System.Windows.Forms.NumericUpDown PresenceUpdateRateNum;
        private System.Windows.Forms.OpenFileDialog CustomClientSettingsDialog;
        private System.Windows.Forms.CheckBox AutoCookieRefreshCB;
        private System.Windows.Forms.Button ForceUpdateButton;
    }
}
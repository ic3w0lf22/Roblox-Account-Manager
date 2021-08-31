namespace RBX_Alt_Manager
{
    partial class AccountManager
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AccountManager));
            this.PlaceID = new System.Windows.Forms.TextBox();
            this.JobID = new System.Windows.Forms.TextBox();
            this.LabelJobID = new System.Windows.Forms.Label();
            this.Add = new System.Windows.Forms.Button();
            this.Remove = new System.Windows.Forms.Button();
            this.JoinServer = new System.Windows.Forms.Button();
            this.SetDescription = new System.Windows.Forms.Button();
            this.DescriptionBox = new System.Windows.Forms.RichTextBox();
            this.SetAlias = new System.Windows.Forms.Button();
            this.Alias = new System.Windows.Forms.TextBox();
            this.Follow = new System.Windows.Forms.Button();
            this.UserID = new System.Windows.Forms.TextBox();
            this.LabelUserID = new System.Windows.Forms.Label();
            this.ServerList = new System.Windows.Forms.Button();
            this.AccountsView = new System.Windows.Forms.ListView();
            this.AccountName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.AliasColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Description = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.AccountsStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addAccountsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeAccountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyUsernameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.moveGroupUpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.moveToToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.moveUpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.moveDownToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.infoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.getAuthenticationTicketToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copySecurityTokenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyRbxplayerLinkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyAppLinkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.HideUsernamesCheckbox = new System.Windows.Forms.CheckBox();
            this.RobloxProcessTimer = new System.Windows.Forms.Timer(this.components);
            this.RefreshTip = new System.Windows.Forms.ToolTip(this.components);
            this.BrowserButton = new System.Windows.Forms.Button();
            this.ArgumentsB = new System.Windows.Forms.Button();
            this.helloToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helloV2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.securityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.setPasswordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainStrip = new System.Windows.Forms.MenuStrip();
            this.CurrentPlace = new System.Windows.Forms.Label();
            this.LabelPlaceID = new System.Windows.Forms.Label();
            this.PlaceTimer = new System.Windows.Forms.Timer(this.components);
            this.JoinDiscord = new System.Windows.Forms.Button();
            this.OpenApp = new System.Windows.Forms.Button();
            this.ImportByCookie = new System.Windows.Forms.Button();
            this.AccountsStrip.SuspendLayout();
            this.MainStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // PlaceID
            // 
            this.PlaceID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.PlaceID.Location = new System.Drawing.Point(504, 43);
            this.PlaceID.Name = "PlaceID";
            this.PlaceID.Size = new System.Drawing.Size(84, 20);
            this.PlaceID.TabIndex = 1;
            this.PlaceID.Text = "3016661674";
            this.PlaceID.TextChanged += new System.EventHandler(this.PlaceID_TextChanged);
            this.PlaceID.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.PlaceID_KeyPress);
            // 
            // JobID
            // 
            this.JobID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.JobID.Location = new System.Drawing.Point(594, 43);
            this.JobID.Name = "JobID";
            this.JobID.Size = new System.Drawing.Size(178, 20);
            this.JobID.TabIndex = 2;
            // 
            // LabelJobID
            // 
            this.LabelJobID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LabelJobID.AutoSize = true;
            this.LabelJobID.Location = new System.Drawing.Point(591, 27);
            this.LabelJobID.Name = "LabelJobID";
            this.LabelJobID.Size = new System.Drawing.Size(38, 13);
            this.LabelJobID.TabIndex = 4;
            this.LabelJobID.Text = "Job ID";
            // 
            // Add
            // 
            this.Add.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Add.Location = new System.Drawing.Point(13, 266);
            this.Add.Name = "Add";
            this.Add.Size = new System.Drawing.Size(95, 23);
            this.Add.TabIndex = 13;
            this.Add.Text = "Add Account";
            this.Add.UseVisualStyleBackColor = true;
            this.Add.Click += new System.EventHandler(this.Add_Click);
            // 
            // Remove
            // 
            this.Remove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Remove.Location = new System.Drawing.Point(114, 266);
            this.Remove.Name = "Remove";
            this.Remove.Size = new System.Drawing.Size(95, 23);
            this.Remove.TabIndex = 14;
            this.Remove.Text = "Remove";
            this.Remove.UseVisualStyleBackColor = true;
            this.Remove.Click += new System.EventHandler(this.Remove_Click);
            // 
            // JoinServer
            // 
            this.JoinServer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.JoinServer.Location = new System.Drawing.Point(504, 69);
            this.JoinServer.Name = "JoinServer";
            this.JoinServer.Size = new System.Drawing.Size(168, 23);
            this.JoinServer.TabIndex = 3;
            this.JoinServer.Text = "Join Server";
            this.JoinServer.UseVisualStyleBackColor = true;
            this.JoinServer.Click += new System.EventHandler(this.JoinServer_Click);
            // 
            // SetDescription
            // 
            this.SetDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SetDescription.Location = new System.Drawing.Point(504, 266);
            this.SetDescription.Name = "SetDescription";
            this.SetDescription.Size = new System.Drawing.Size(133, 23);
            this.SetDescription.TabIndex = 11;
            this.SetDescription.Text = "Set Description";
            this.SetDescription.UseVisualStyleBackColor = true;
            this.SetDescription.Click += new System.EventHandler(this.SetDescription_Click);
            // 
            // DescriptionBox
            // 
            this.DescriptionBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.DescriptionBox.Location = new System.Drawing.Point(504, 150);
            this.DescriptionBox.Name = "DescriptionBox";
            this.DescriptionBox.Size = new System.Drawing.Size(268, 109);
            this.DescriptionBox.TabIndex = 10;
            this.DescriptionBox.Text = "Description";
            // 
            // SetAlias
            // 
            this.SetAlias.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SetAlias.Location = new System.Drawing.Point(716, 122);
            this.SetAlias.Name = "SetAlias";
            this.SetAlias.Size = new System.Drawing.Size(56, 23);
            this.SetAlias.TabIndex = 9;
            this.SetAlias.Text = "Set Alias";
            this.SetAlias.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.SetAlias.UseVisualStyleBackColor = true;
            this.SetAlias.Click += new System.EventHandler(this.SetAlias_Click);
            // 
            // Alias
            // 
            this.Alias.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Alias.Location = new System.Drawing.Point(504, 124);
            this.Alias.MaxLength = 30;
            this.Alias.Name = "Alias";
            this.Alias.Size = new System.Drawing.Size(206, 20);
            this.Alias.TabIndex = 8;
            this.Alias.Text = "Alias";
            // 
            // Follow
            // 
            this.Follow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Follow.Location = new System.Drawing.Point(716, 96);
            this.Follow.Name = "Follow";
            this.Follow.Size = new System.Drawing.Size(56, 23);
            this.Follow.TabIndex = 7;
            this.Follow.Text = "Follow";
            this.Follow.UseVisualStyleBackColor = true;
            this.Follow.Click += new System.EventHandler(this.Follow_Click);
            // 
            // UserID
            // 
            this.UserID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.UserID.Location = new System.Drawing.Point(566, 98);
            this.UserID.Name = "UserID";
            this.UserID.Size = new System.Drawing.Size(144, 20);
            this.UserID.TabIndex = 6;
            // 
            // LabelUserID
            // 
            this.LabelUserID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LabelUserID.AutoSize = true;
            this.LabelUserID.Location = new System.Drawing.Point(505, 101);
            this.LabelUserID.Name = "LabelUserID";
            this.LabelUserID.Size = new System.Drawing.Size(55, 13);
            this.LabelUserID.TabIndex = 14;
            this.LabelUserID.Text = "Username";
            // 
            // ServerList
            // 
            this.ServerList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ServerList.Location = new System.Drawing.Point(707, 69);
            this.ServerList.Name = "ServerList";
            this.ServerList.Size = new System.Drawing.Size(65, 23);
            this.ServerList.TabIndex = 5;
            this.ServerList.Text = "Server List";
            this.ServerList.UseVisualStyleBackColor = true;
            this.ServerList.Click += new System.EventHandler(this.ServerList_Click);
            // 
            // AccountsView
            // 
            this.AccountsView.AllowColumnReorder = true;
            this.AccountsView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AccountsView.BackColor = System.Drawing.SystemColors.Window;
            this.AccountsView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.AccountName,
            this.AliasColumn,
            this.Description});
            this.AccountsView.ContextMenuStrip = this.AccountsStrip;
            this.AccountsView.ForeColor = System.Drawing.Color.Black;
            this.AccountsView.FullRowSelect = true;
            this.AccountsView.GridLines = true;
            this.AccountsView.HideSelection = false;
            this.AccountsView.Location = new System.Drawing.Point(13, 12);
            this.AccountsView.MultiSelect = false;
            this.AccountsView.Name = "AccountsView";
            this.AccountsView.Size = new System.Drawing.Size(485, 247);
            this.AccountsView.TabIndex = 17;
            this.AccountsView.UseCompatibleStateImageBehavior = false;
            this.AccountsView.View = System.Windows.Forms.View.Details;
            this.AccountsView.SelectedIndexChanged += new System.EventHandler(this.AccountsView_SelectedIndexChanged);
            this.AccountsView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.AccountsView_MouseDown);
            this.AccountsView.MouseMove += new System.Windows.Forms.MouseEventHandler(this.AccountsView_MouseMove);
            this.AccountsView.MouseUp += new System.Windows.Forms.MouseEventHandler(this.AccountsView_MouseUp);
            // 
            // AccountName
            // 
            this.AccountName.Tag = "";
            this.AccountName.Text = "Name";
            this.AccountName.Width = 120;
            // 
            // AliasColumn
            // 
            this.AliasColumn.Text = "Alias";
            this.AliasColumn.Width = 112;
            // 
            // Description
            // 
            this.Description.Text = "Description";
            this.Description.Width = 229;
            // 
            // AccountsStrip
            // 
            this.AccountsStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addAccountsToolStripMenuItem,
            this.removeAccountToolStripMenuItem,
            this.copyUsernameToolStripMenuItem,
            this.moveGroupUpToolStripMenuItem,
            this.infoToolStripMenuItem,
            this.getAuthenticationTicketToolStripMenuItem,
            this.copySecurityTokenToolStripMenuItem,
            this.copyRbxplayerLinkToolStripMenuItem,
            this.copyAppLinkToolStripMenuItem});
            this.AccountsStrip.Name = "contextMenuStrip1";
            this.AccountsStrip.Size = new System.Drawing.Size(209, 202);
            // 
            // addAccountsToolStripMenuItem
            // 
            this.addAccountsToolStripMenuItem.Name = "addAccountsToolStripMenuItem";
            this.addAccountsToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.addAccountsToolStripMenuItem.Text = "Add Account";
            this.addAccountsToolStripMenuItem.Click += new System.EventHandler(this.addAccountsToolStripMenuItem_Click);
            // 
            // removeAccountToolStripMenuItem
            // 
            this.removeAccountToolStripMenuItem.Name = "removeAccountToolStripMenuItem";
            this.removeAccountToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.removeAccountToolStripMenuItem.Text = "Remove Account";
            this.removeAccountToolStripMenuItem.Click += new System.EventHandler(this.removeAccountToolStripMenuItem_Click);
            // 
            // copyUsernameToolStripMenuItem
            // 
            this.copyUsernameToolStripMenuItem.Name = "copyUsernameToolStripMenuItem";
            this.copyUsernameToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.copyUsernameToolStripMenuItem.Text = "Copy Username";
            this.copyUsernameToolStripMenuItem.Click += new System.EventHandler(this.copyUsernameToolStripMenuItem_Click);
            // 
            // moveGroupUpToolStripMenuItem
            // 
            this.moveGroupUpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.moveToToolStripMenuItem,
            this.moveUpToolStripMenuItem,
            this.moveDownToolStripMenuItem});
            this.moveGroupUpToolStripMenuItem.Name = "moveGroupUpToolStripMenuItem";
            this.moveGroupUpToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.moveGroupUpToolStripMenuItem.Text = "Groups";
            // 
            // moveToToolStripMenuItem
            // 
            this.moveToToolStripMenuItem.Name = "moveToToolStripMenuItem";
            this.moveToToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.moveToToolStripMenuItem.Text = "Move Account To";
            this.moveToToolStripMenuItem.Click += new System.EventHandler(this.moveToToolStripMenuItem_Click);
            // 
            // moveUpToolStripMenuItem
            // 
            this.moveUpToolStripMenuItem.Name = "moveUpToolStripMenuItem";
            this.moveUpToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.moveUpToolStripMenuItem.Text = "Move Group Up";
            this.moveUpToolStripMenuItem.Click += new System.EventHandler(this.moveUpToolStripMenuItem_Click);
            // 
            // moveDownToolStripMenuItem
            // 
            this.moveDownToolStripMenuItem.Name = "moveDownToolStripMenuItem";
            this.moveDownToolStripMenuItem.Size = new System.Drawing.Size(174, 22);
            this.moveDownToolStripMenuItem.Text = "Move Group Down";
            this.moveDownToolStripMenuItem.Click += new System.EventHandler(this.moveDownToolStripMenuItem_Click);
            // 
            // infoToolStripMenuItem
            // 
            this.infoToolStripMenuItem.Name = "infoToolStripMenuItem";
            this.infoToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.infoToolStripMenuItem.Text = "Info";
            this.infoToolStripMenuItem.Click += new System.EventHandler(this.infoToolStripMenuItem_Click);
            // 
            // getAuthenticationTicketToolStripMenuItem
            // 
            this.getAuthenticationTicketToolStripMenuItem.Name = "getAuthenticationTicketToolStripMenuItem";
            this.getAuthenticationTicketToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.getAuthenticationTicketToolStripMenuItem.Text = "Get Authentication Ticket";
            this.getAuthenticationTicketToolStripMenuItem.Click += new System.EventHandler(this.getAuthenticationTicketToolStripMenuItem_Click);
            // 
            // copySecurityTokenToolStripMenuItem
            // 
            this.copySecurityTokenToolStripMenuItem.Name = "copySecurityTokenToolStripMenuItem";
            this.copySecurityTokenToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.copySecurityTokenToolStripMenuItem.Text = "Copy Security Token";
            this.copySecurityTokenToolStripMenuItem.Click += new System.EventHandler(this.copySecurityTokenToolStripMenuItem_Click);
            // 
            // copyRbxplayerLinkToolStripMenuItem
            // 
            this.copyRbxplayerLinkToolStripMenuItem.Name = "copyRbxplayerLinkToolStripMenuItem";
            this.copyRbxplayerLinkToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.copyRbxplayerLinkToolStripMenuItem.Text = "Copy rbx-player Link";
            this.copyRbxplayerLinkToolStripMenuItem.Click += new System.EventHandler(this.copyRbxplayerLinkToolStripMenuItem_Click);
            // 
            // copyAppLinkToolStripMenuItem
            // 
            this.copyAppLinkToolStripMenuItem.Name = "copyAppLinkToolStripMenuItem";
            this.copyAppLinkToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.copyAppLinkToolStripMenuItem.Text = "Copy App Link";
            this.copyAppLinkToolStripMenuItem.Click += new System.EventHandler(this.copyAppLinkToolStripMenuItem_Click);
            // 
            // HideUsernamesCheckbox
            // 
            this.HideUsernamesCheckbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.HideUsernamesCheckbox.AutoSize = true;
            this.HideUsernamesCheckbox.Location = new System.Drawing.Point(215, 270);
            this.HideUsernamesCheckbox.Name = "HideUsernamesCheckbox";
            this.HideUsernamesCheckbox.Size = new System.Drawing.Size(104, 17);
            this.HideUsernamesCheckbox.TabIndex = 15;
            this.HideUsernamesCheckbox.Text = "Hide Usernames";
            this.HideUsernamesCheckbox.UseVisualStyleBackColor = true;
            this.HideUsernamesCheckbox.CheckedChanged += new System.EventHandler(this.HideUsernamesCheckbox_CheckedChanged);
            // 
            // RefreshTip
            // 
            this.RefreshTip.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.RefreshTip.ToolTipTitle = "Refresh";
            // 
            // BrowserButton
            // 
            this.BrowserButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BrowserButton.Location = new System.Drawing.Point(639, 266);
            this.BrowserButton.Name = "BrowserButton";
            this.BrowserButton.Size = new System.Drawing.Size(133, 23);
            this.BrowserButton.TabIndex = 12;
            this.BrowserButton.Text = "Open Account Utilities";
            this.BrowserButton.UseVisualStyleBackColor = true;
            this.BrowserButton.Click += new System.EventHandler(this.BrowserButton_Click);
            // 
            // ArgumentsB
            // 
            this.ArgumentsB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ArgumentsB.Location = new System.Drawing.Point(678, 69);
            this.ArgumentsB.Name = "ArgumentsB";
            this.ArgumentsB.Size = new System.Drawing.Size(23, 23);
            this.ArgumentsB.TabIndex = 4;
            this.ArgumentsB.Text = "A";
            this.ArgumentsB.UseVisualStyleBackColor = true;
            this.ArgumentsB.Click += new System.EventHandler(this.ArgumentsB_Click);
            // 
            // helloToolStripMenuItem
            // 
            this.helloToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helloV2ToolStripMenuItem,
            this.hiToolStripMenuItem});
            this.helloToolStripMenuItem.Name = "helloToolStripMenuItem";
            this.helloToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.helloToolStripMenuItem.Text = "Hello";
            // 
            // helloV2ToolStripMenuItem
            // 
            this.helloV2ToolStripMenuItem.Name = "helloV2ToolStripMenuItem";
            this.helloV2ToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.helloV2ToolStripMenuItem.Text = "Hello v2";
            // 
            // hiToolStripMenuItem
            // 
            this.hiToolStripMenuItem.Name = "hiToolStripMenuItem";
            this.hiToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.hiToolStripMenuItem.Text = "Hi";
            // 
            // securityToolStripMenuItem
            // 
            this.securityToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.setPasswordToolStripMenuItem});
            this.securityToolStripMenuItem.Name = "securityToolStripMenuItem";
            this.securityToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.securityToolStripMenuItem.Text = "Security";
            // 
            // setPasswordToolStripMenuItem
            // 
            this.setPasswordToolStripMenuItem.Name = "setPasswordToolStripMenuItem";
            this.setPasswordToolStripMenuItem.Size = new System.Drawing.Size(182, 22);
            this.setPasswordToolStripMenuItem.Text = "Set Master Password";
            // 
            // MainStrip
            // 
            this.MainStrip.Enabled = false;
            this.MainStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.helloToolStripMenuItem,
            this.securityToolStripMenuItem});
            this.MainStrip.Location = new System.Drawing.Point(0, 0);
            this.MainStrip.Name = "MainStrip";
            this.MainStrip.Size = new System.Drawing.Size(783, 24);
            this.MainStrip.TabIndex = 20;
            this.MainStrip.Text = "MainStrip";
            this.MainStrip.Visible = false;
            // 
            // CurrentPlace
            // 
            this.CurrentPlace.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CurrentPlace.AutoSize = true;
            this.CurrentPlace.Location = new System.Drawing.Point(504, 12);
            this.CurrentPlace.Name = "CurrentPlace";
            this.CurrentPlace.Size = new System.Drawing.Size(71, 13);
            this.CurrentPlace.TabIndex = 21;
            this.CurrentPlace.Text = "Current Place";
            // 
            // LabelPlaceID
            // 
            this.LabelPlaceID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LabelPlaceID.AutoSize = true;
            this.LabelPlaceID.Location = new System.Drawing.Point(504, 27);
            this.LabelPlaceID.Name = "LabelPlaceID";
            this.LabelPlaceID.Size = new System.Drawing.Size(48, 13);
            this.LabelPlaceID.TabIndex = 3;
            this.LabelPlaceID.Text = "Place ID";
            // 
            // PlaceTimer
            // 
            this.PlaceTimer.Interval = 400;
            this.PlaceTimer.Tick += new System.EventHandler(this.PlaceTimer_Tick);
            // 
            // JoinDiscord
            // 
            this.JoinDiscord.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.JoinDiscord.Image = global::RBX_Alt_Manager.Properties.Resources.disc;
            this.JoinDiscord.Location = new System.Drawing.Point(474, 266);
            this.JoinDiscord.Name = "JoinDiscord";
            this.JoinDiscord.Size = new System.Drawing.Size(23, 23);
            this.JoinDiscord.TabIndex = 22;
            this.JoinDiscord.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.JoinDiscord.UseVisualStyleBackColor = true;
            this.JoinDiscord.Click += new System.EventHandler(this.JoinDiscord_Click);
            // 
            // OpenApp
            // 
            this.OpenApp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.OpenApp.Location = new System.Drawing.Point(325, 266);
            this.OpenApp.Name = "OpenApp";
            this.OpenApp.Size = new System.Drawing.Size(143, 23);
            this.OpenApp.TabIndex = 23;
            this.OpenApp.Text = "Open App";
            this.OpenApp.UseVisualStyleBackColor = true;
            this.OpenApp.Click += new System.EventHandler(this.OpenApp_Click);
            // 
            // ImportByCookie
            // 
            this.ImportByCookie.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ImportByCookie.Location = new System.Drawing.Point(325, 266);
            this.ImportByCookie.Name = "ImportByCookie";
            this.ImportByCookie.Size = new System.Drawing.Size(70, 23);
            this.ImportByCookie.TabIndex = 24;
            this.ImportByCookie.Text = "Import";
            this.ImportByCookie.UseVisualStyleBackColor = true;
            this.ImportByCookie.Visible = false;
            this.ImportByCookie.Click += new System.EventHandler(this.ImportByCookie_Click);
            // 
            // AccountManager
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 301);
            this.Controls.Add(this.ImportByCookie);
            this.Controls.Add(this.OpenApp);
            this.Controls.Add(this.JoinDiscord);
            this.Controls.Add(this.CurrentPlace);
            this.Controls.Add(this.ArgumentsB);
            this.Controls.Add(this.BrowserButton);
            this.Controls.Add(this.HideUsernamesCheckbox);
            this.Controls.Add(this.MainStrip);
            this.Controls.Add(this.AccountsView);
            this.Controls.Add(this.ServerList);
            this.Controls.Add(this.LabelUserID);
            this.Controls.Add(this.UserID);
            this.Controls.Add(this.Follow);
            this.Controls.Add(this.Alias);
            this.Controls.Add(this.SetAlias);
            this.Controls.Add(this.DescriptionBox);
            this.Controls.Add(this.SetDescription);
            this.Controls.Add(this.JoinServer);
            this.Controls.Add(this.Remove);
            this.Controls.Add(this.Add);
            this.Controls.Add(this.LabelJobID);
            this.Controls.Add(this.LabelPlaceID);
            this.Controls.Add(this.JobID);
            this.Controls.Add(this.PlaceID);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.MainStrip;
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(800, 340);
            this.Name = "AccountManager";
            this.Text = "Roblox Account Manager";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AccountManager_FormClosed);
            this.Load += new System.EventHandler(this.AccountManager_Load);
            this.Shown += new System.EventHandler(this.AccountManager_Shown);
            this.AccountsStrip.ResumeLayout(false);
            this.MainStrip.ResumeLayout(false);
            this.MainStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.TextBox PlaceID;
        public System.Windows.Forms.TextBox JobID;
        private System.Windows.Forms.Label LabelJobID;
        private System.Windows.Forms.Button Add;
        private System.Windows.Forms.Button Remove;
        private System.Windows.Forms.Button JoinServer;
        private System.Windows.Forms.Button SetDescription;
        private System.Windows.Forms.RichTextBox DescriptionBox;
        private System.Windows.Forms.Button SetAlias;
        private System.Windows.Forms.TextBox Alias;
        private System.Windows.Forms.Button Follow;
        private System.Windows.Forms.TextBox UserID;
        private System.Windows.Forms.Label LabelUserID;
        private System.Windows.Forms.Button ServerList;
        public System.Windows.Forms.ListView AccountsView;
        private System.Windows.Forms.ColumnHeader AccountName;
        private System.Windows.Forms.ColumnHeader AliasColumn;
        private System.Windows.Forms.ColumnHeader Description;
        private System.Windows.Forms.ContextMenuStrip AccountsStrip;
        private System.Windows.Forms.ToolStripMenuItem addAccountsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeAccountToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem infoToolStripMenuItem;
        private System.Windows.Forms.CheckBox HideUsernamesCheckbox;
        private System.Windows.Forms.Timer RobloxProcessTimer;
        private System.Windows.Forms.ToolStripMenuItem getAuthenticationTicketToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyRbxplayerLinkToolStripMenuItem;
        private System.Windows.Forms.ToolTip RefreshTip;
        private System.Windows.Forms.ToolStripMenuItem copyUsernameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copySecurityTokenToolStripMenuItem;
        private System.Windows.Forms.Button BrowserButton;
        private System.Windows.Forms.Button ArgumentsB;
        private System.Windows.Forms.ToolStripMenuItem helloToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helloV2ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hiToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem securityToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem setPasswordToolStripMenuItem;
        private System.Windows.Forms.MenuStrip MainStrip;
        private System.Windows.Forms.Label CurrentPlace;
        private System.Windows.Forms.Label LabelPlaceID;
        private System.Windows.Forms.Timer PlaceTimer;
        private System.Windows.Forms.ToolStripMenuItem moveGroupUpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem moveToToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem moveUpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem moveDownToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyAppLinkToolStripMenuItem;
        private System.Windows.Forms.Button JoinDiscord;
        private System.Windows.Forms.Button OpenApp;
        private System.Windows.Forms.Button ImportByCookie;
    }
}
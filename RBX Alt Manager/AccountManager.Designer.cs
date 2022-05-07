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
            this.LabelJobID = new System.Windows.Forms.Label();
            this.Add = new System.Windows.Forms.Button();
            this.Remove = new System.Windows.Forms.Button();
            this.JoinServer = new System.Windows.Forms.Button();
            this.SetDescription = new System.Windows.Forms.Button();
            this.SetAlias = new System.Windows.Forms.Button();
            this.Follow = new System.Windows.Forms.Button();
            this.LabelUserID = new System.Windows.Forms.Label();
            this.ServerList = new System.Windows.Forms.Button();
            this.AccountsStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addAccountsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeAccountToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyUsernameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyProfileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sortAlphabeticallyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.moveGroupUpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toggleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.moveToToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.infoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.infoToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.viewFieldsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.getAuthenticationTicketToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copySecurityTokenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyRbxplayerLinkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyAppLinkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.HideUsernamesCheckbox = new System.Windows.Forms.CheckBox();
            this.BrowserButton = new System.Windows.Forms.Button();
            this.ArgumentsB = new System.Windows.Forms.Button();
            this.CurrentPlace = new System.Windows.Forms.Label();
            this.LabelPlaceID = new System.Windows.Forms.Label();
            this.PlaceTimer = new System.Windows.Forms.Timer(this.components);
            this.JoinDiscord = new System.Windows.Forms.Button();
            this.OpenApp = new System.Windows.Forms.Button();
            this.ImportByCookie = new System.Windows.Forms.Button();
            this.SaveToAccount = new System.Windows.Forms.Button();
            this.SaveTooltip = new System.Windows.Forms.ToolTip(this.components);
            this.JobID = new RBX_Alt_Manager.Classes.BorderedTextBox();
            this.DonateButton = new System.Windows.Forms.Button();
            this.SaveTimer = new System.Windows.Forms.Timer(this.components);
            this.AccountsView = new BrightIdeasSoftware.ObjectListView();
            this.Username = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.AccountAlias = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.Description = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.Group = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.EditTheme = new System.Windows.Forms.Button();
            this.UserID = new RBX_Alt_Manager.Classes.BorderedTextBox();
            this.Alias = new RBX_Alt_Manager.Classes.BorderedTextBox();
            this.DescriptionBox = new RBX_Alt_Manager.Classes.BorderedRichTextBox();
            this.PlaceID = new RBX_Alt_Manager.Classes.BorderedTextBox();
            this.LaunchNexus = new System.Windows.Forms.Button();
            this.copyPasswordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AccountsStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.AccountsView)).BeginInit();
            this.SuspendLayout();
            // 
            // LabelJobID
            // 
            this.LabelJobID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LabelJobID.AutoSize = true;
            this.LabelJobID.Location = new System.Drawing.Point(591, 27);
            this.LabelJobID.Name = "LabelJobID";
            this.LabelJobID.Size = new System.Drawing.Size(38, 13);
            this.LabelJobID.TabIndex = 1000;
            this.LabelJobID.Text = "Job ID";
            this.SaveTooltip.SetToolTip(this.LabelJobID, "Job ID is a unique ID assigned to every roblox server.");
            // 
            // Add
            // 
            this.Add.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Add.Location = new System.Drawing.Point(13, 266);
            this.Add.Name = "Add";
            this.Add.Size = new System.Drawing.Size(95, 23);
            this.Add.TabIndex = 14;
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
            this.Remove.TabIndex = 15;
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
            this.JoinServer.TabIndex = 4;
            this.JoinServer.Text = "Join Server";
            this.JoinServer.UseVisualStyleBackColor = true;
            this.JoinServer.Click += new System.EventHandler(this.JoinServer_Click);
            // 
            // SetDescription
            // 
            this.SetDescription.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SetDescription.Location = new System.Drawing.Point(503, 236);
            this.SetDescription.Name = "SetDescription";
            this.SetDescription.Size = new System.Drawing.Size(133, 23);
            this.SetDescription.TabIndex = 12;
            this.SetDescription.Text = "Set Description";
            this.SetDescription.UseVisualStyleBackColor = true;
            this.SetDescription.Click += new System.EventHandler(this.SetDescription_Click);
            // 
            // SetAlias
            // 
            this.SetAlias.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SetAlias.Location = new System.Drawing.Point(707, 122);
            this.SetAlias.Name = "SetAlias";
            this.SetAlias.Size = new System.Drawing.Size(65, 23);
            this.SetAlias.TabIndex = 10;
            this.SetAlias.Text = "Set Alias";
            this.SetAlias.UseVisualStyleBackColor = true;
            this.SetAlias.Click += new System.EventHandler(this.SetAlias_Click);
            // 
            // Follow
            // 
            this.Follow.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Follow.Location = new System.Drawing.Point(707, 96);
            this.Follow.Name = "Follow";
            this.Follow.Size = new System.Drawing.Size(65, 23);
            this.Follow.TabIndex = 8;
            this.Follow.Text = "Follow";
            this.SaveTooltip.SetToolTip(this.Follow, "Follows a user into their game.");
            this.Follow.UseVisualStyleBackColor = true;
            this.Follow.Click += new System.EventHandler(this.Follow_Click);
            // 
            // LabelUserID
            // 
            this.LabelUserID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LabelUserID.AutoSize = true;
            this.LabelUserID.Location = new System.Drawing.Point(505, 101);
            this.LabelUserID.Name = "LabelUserID";
            this.LabelUserID.Size = new System.Drawing.Size(55, 13);
            this.LabelUserID.TabIndex = 1000;
            this.LabelUserID.Text = "Username";
            // 
            // ServerList
            // 
            this.ServerList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ServerList.Location = new System.Drawing.Point(707, 69);
            this.ServerList.Name = "ServerList";
            this.ServerList.Size = new System.Drawing.Size(65, 23);
            this.ServerList.TabIndex = 6;
            this.ServerList.Text = "Server List";
            this.SaveTooltip.SetToolTip(this.ServerList, "Contains Server List, Games List, and Favorites List");
            this.ServerList.UseVisualStyleBackColor = true;
            this.ServerList.Click += new System.EventHandler(this.ServerList_Click);
            // 
            // AccountsStrip
            // 
            this.AccountsStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addAccountsToolStripMenuItem,
            this.removeAccountToolStripMenuItem,
            this.copyUsernameToolStripMenuItem,
            this.copyPasswordToolStripMenuItem,
            this.copyProfileToolStripMenuItem,
            this.sortAlphabeticallyToolStripMenuItem,
            this.moveGroupUpToolStripMenuItem,
            this.infoToolStripMenuItem,
            this.viewFieldsToolStripMenuItem,
            this.getAuthenticationTicketToolStripMenuItem,
            this.copySecurityTokenToolStripMenuItem,
            this.copyRbxplayerLinkToolStripMenuItem,
            this.copyAppLinkToolStripMenuItem});
            this.AccountsStrip.Name = "contextMenuStrip1";
            this.AccountsStrip.Size = new System.Drawing.Size(209, 312);
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
            // copyProfileToolStripMenuItem
            // 
            this.copyProfileToolStripMenuItem.Name = "copyProfileToolStripMenuItem";
            this.copyProfileToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.copyProfileToolStripMenuItem.Text = "Copy Profile";
            this.copyProfileToolStripMenuItem.Click += new System.EventHandler(this.copyProfileToolStripMenuItem_Click);
            // 
            // sortAlphabeticallyToolStripMenuItem
            // 
            this.sortAlphabeticallyToolStripMenuItem.Name = "sortAlphabeticallyToolStripMenuItem";
            this.sortAlphabeticallyToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.sortAlphabeticallyToolStripMenuItem.Text = "Sort Alphabetically";
            this.sortAlphabeticallyToolStripMenuItem.Click += new System.EventHandler(this.sortAlphabeticallyToolStripMenuItem_Click);
            // 
            // moveGroupUpToolStripMenuItem
            // 
            this.moveGroupUpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toggleToolStripMenuItem,
            this.moveToToolStripMenuItem});
            this.moveGroupUpToolStripMenuItem.Name = "moveGroupUpToolStripMenuItem";
            this.moveGroupUpToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.moveGroupUpToolStripMenuItem.Text = "Groups";
            // 
            // toggleToolStripMenuItem
            // 
            this.toggleToolStripMenuItem.Name = "toggleToolStripMenuItem";
            this.toggleToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.toggleToolStripMenuItem.Text = "Toggle";
            this.toggleToolStripMenuItem.Click += new System.EventHandler(this.toggleToolStripMenuItem_Click);
            // 
            // moveToToolStripMenuItem
            // 
            this.moveToToolStripMenuItem.Name = "moveToToolStripMenuItem";
            this.moveToToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.moveToToolStripMenuItem.Text = "Move Account To";
            this.moveToToolStripMenuItem.Click += new System.EventHandler(this.moveToToolStripMenuItem_Click);
            // 
            // infoToolStripMenuItem
            // 
            this.infoToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.groupsToolStripMenuItem,
            this.infoToolStripMenuItem1});
            this.infoToolStripMenuItem.Name = "infoToolStripMenuItem";
            this.infoToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.infoToolStripMenuItem.Text = "Help";
            // 
            // groupsToolStripMenuItem
            // 
            this.groupsToolStripMenuItem.Name = "groupsToolStripMenuItem";
            this.groupsToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.groupsToolStripMenuItem.Text = "Groups";
            this.groupsToolStripMenuItem.Click += new System.EventHandler(this.groupsToolStripMenuItem_Click);
            // 
            // infoToolStripMenuItem1
            // 
            this.infoToolStripMenuItem1.Name = "infoToolStripMenuItem1";
            this.infoToolStripMenuItem1.Size = new System.Drawing.Size(112, 22);
            this.infoToolStripMenuItem1.Text = "Info";
            this.infoToolStripMenuItem1.Click += new System.EventHandler(this.infoToolStripMenuItem1_Click);
            // 
            // viewFieldsToolStripMenuItem
            // 
            this.viewFieldsToolStripMenuItem.Name = "viewFieldsToolStripMenuItem";
            this.viewFieldsToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.viewFieldsToolStripMenuItem.Text = "View Fields";
            this.viewFieldsToolStripMenuItem.Click += new System.EventHandler(this.viewFieldsToolStripMenuItem_Click);
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
            this.HideUsernamesCheckbox.TabIndex = 16;
            this.HideUsernamesCheckbox.Text = "Hide Usernames";
            this.HideUsernamesCheckbox.UseVisualStyleBackColor = true;
            this.HideUsernamesCheckbox.CheckedChanged += new System.EventHandler(this.HideUsernamesCheckbox_CheckedChanged);
            // 
            // BrowserButton
            // 
            this.BrowserButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BrowserButton.Location = new System.Drawing.Point(639, 236);
            this.BrowserButton.Name = "BrowserButton";
            this.BrowserButton.Size = new System.Drawing.Size(133, 23);
            this.BrowserButton.TabIndex = 13;
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
            this.ArgumentsB.TabIndex = 5;
            this.ArgumentsB.Text = "A";
            this.ArgumentsB.UseVisualStyleBackColor = true;
            this.ArgumentsB.Click += new System.EventHandler(this.ArgumentsB_Click);
            // 
            // CurrentPlace
            // 
            this.CurrentPlace.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.CurrentPlace.AutoSize = true;
            this.CurrentPlace.Location = new System.Drawing.Point(504, 12);
            this.CurrentPlace.Name = "CurrentPlace";
            this.CurrentPlace.Size = new System.Drawing.Size(71, 13);
            this.CurrentPlace.TabIndex = 1000;
            this.CurrentPlace.Text = "Current Place";
            // 
            // LabelPlaceID
            // 
            this.LabelPlaceID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LabelPlaceID.AutoSize = true;
            this.LabelPlaceID.Location = new System.Drawing.Point(504, 27);
            this.LabelPlaceID.Name = "LabelPlaceID";
            this.LabelPlaceID.Size = new System.Drawing.Size(48, 13);
            this.LabelPlaceID.TabIndex = 1000;
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
            this.JoinDiscord.TabIndex = 19;
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
            this.OpenApp.TabIndex = 18;
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
            this.ImportByCookie.TabIndex = 17;
            this.ImportByCookie.Text = "Import";
            this.ImportByCookie.UseVisualStyleBackColor = true;
            this.ImportByCookie.Visible = false;
            this.ImportByCookie.Click += new System.EventHandler(this.ImportByCookie_Click);
            // 
            // SaveToAccount
            // 
            this.SaveToAccount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SaveToAccount.FlatAppearance.BorderSize = 0;
            this.SaveToAccount.Image = ((System.Drawing.Image)(resources.GetObject("SaveToAccount.Image")));
            this.SaveToAccount.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
            this.SaveToAccount.Location = new System.Drawing.Point(750, 42);
            this.SaveToAccount.Name = "SaveToAccount";
            this.SaveToAccount.Size = new System.Drawing.Size(22, 22);
            this.SaveToAccount.TabIndex = 3;
            this.SaveTooltip.SetToolTip(this.SaveToAccount, "Saves the PlaceId + JobId to the selected account\r\nTo remove, clear out the text " +
        "boxes on the left and click Save");
            this.SaveToAccount.UseVisualStyleBackColor = true;
            this.SaveToAccount.Click += new System.EventHandler(this.SaveToAccount_Click);
            // 
            // SaveTooltip
            // 
            this.SaveTooltip.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            // 
            // JobID
            // 
            this.JobID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.JobID.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(122)))), ((int)(((byte)(122)))));
            this.JobID.Location = new System.Drawing.Point(594, 43);
            this.JobID.Name = "JobID";
            this.JobID.Size = new System.Drawing.Size(150, 20);
            this.JobID.TabIndex = 2;
            this.SaveTooltip.SetToolTip(this.JobID, "Job ID is a unique ID assigned to every roblox server.\r\nYou may also put a Privat" +
        "e Server link in this box to join it.");
            this.JobID.TextChanged += new System.EventHandler(this.JobID_TextChanged);
            // 
            // DonateButton
            // 
            this.DonateButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.DonateButton.FlatAppearance.BorderSize = 0;
            this.DonateButton.Image = global::RBX_Alt_Manager.Properties.Resources.donation;
            this.DonateButton.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
            this.DonateButton.Location = new System.Drawing.Point(750, 6);
            this.DonateButton.Name = "DonateButton";
            this.DonateButton.Size = new System.Drawing.Size(24, 24);
            this.DonateButton.TabIndex = 1001;
            this.SaveTooltip.SetToolTip(this.DonateButton, "Saves the PlaceId + JobId to the selected account\r\nTo remove, clear out the text " +
        "boxes on the left and click Save");
            this.DonateButton.UseVisualStyleBackColor = true;
            this.DonateButton.Click += new System.EventHandler(this.DonateButton_Click);
            // 
            // SaveTimer
            // 
            this.SaveTimer.Interval = 2500;
            this.SaveTimer.Tick += new System.EventHandler(this.SaveTimer_Tick);
            // 
            // AccountsView
            // 
            this.AccountsView.AllColumns.Add(this.Username);
            this.AccountsView.AllColumns.Add(this.AccountAlias);
            this.AccountsView.AllColumns.Add(this.Description);
            this.AccountsView.AllColumns.Add(this.Group);
            this.AccountsView.AllowDrop = true;
            this.AccountsView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AccountsView.CellEditUseWholeCell = false;
            this.AccountsView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Username,
            this.AccountAlias,
            this.Description,
            this.Group});
            this.AccountsView.ContextMenuStrip = this.AccountsStrip;
            this.AccountsView.Cursor = System.Windows.Forms.Cursors.Default;
            this.AccountsView.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.75F);
            this.AccountsView.FullRowSelect = true;
            this.AccountsView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.AccountsView.HideSelection = false;
            this.AccountsView.IsSimpleDragSource = true;
            this.AccountsView.IsSimpleDropSink = true;
            this.AccountsView.Location = new System.Drawing.Point(13, 13);
            this.AccountsView.Name = "AccountsView";
            this.AccountsView.ShowSortIndicators = false;
            this.AccountsView.Size = new System.Drawing.Size(485, 246);
            this.AccountsView.SortGroupItemsByPrimaryColumn = false;
            this.AccountsView.TabIndex = 20;
            this.AccountsView.UseCompatibleStateImageBehavior = false;
            this.AccountsView.View = System.Windows.Forms.View.Details;
            this.AccountsView.ModelCanDrop += new System.EventHandler<BrightIdeasSoftware.ModelDropEventArgs>(this.AccountsView_ModelCanDrop);
            this.AccountsView.ModelDropped += new System.EventHandler<BrightIdeasSoftware.ModelDropEventArgs>(this.AccountsView_ModelDropped);
            this.AccountsView.SelectedIndexChanged += new System.EventHandler(this.AccountsView_SelectedIndexChanged);
            // 
            // Username
            // 
            this.Username.AspectName = "Username";
            this.Username.Sortable = false;
            this.Username.Text = "Username";
            this.Username.Width = 130;
            // 
            // AccountAlias
            // 
            this.AccountAlias.AspectName = "Alias";
            this.AccountAlias.Text = "Alias";
            this.AccountAlias.Width = 120;
            // 
            // Description
            // 
            this.Description.AspectName = "Description";
            this.Description.Text = "Description";
            this.Description.Width = 200;
            // 
            // Group
            // 
            this.Group.AspectName = "Group";
            this.Group.MaximumWidth = 0;
            this.Group.MinimumWidth = 0;
            this.Group.Text = "";
            this.Group.Width = 0;
            // 
            // EditTheme
            // 
            this.EditTheme.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.EditTheme.Location = new System.Drawing.Point(503, 266);
            this.EditTheme.Name = "EditTheme";
            this.EditTheme.Size = new System.Drawing.Size(133, 23);
            this.EditTheme.TabIndex = 13;
            this.EditTheme.Text = "Edit Theme";
            this.EditTheme.UseVisualStyleBackColor = true;
            this.EditTheme.Click += new System.EventHandler(this.EditTheme_Click);
            // 
            // UserID
            // 
            this.UserID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.UserID.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(122)))), ((int)(((byte)(122)))));
            this.UserID.Location = new System.Drawing.Point(566, 98);
            this.UserID.Name = "UserID";
            this.UserID.Size = new System.Drawing.Size(135, 20);
            this.UserID.TabIndex = 7;
            // 
            // Alias
            // 
            this.Alias.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Alias.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(122)))), ((int)(((byte)(122)))));
            this.Alias.Location = new System.Drawing.Point(504, 124);
            this.Alias.MaxLength = 30;
            this.Alias.Name = "Alias";
            this.Alias.Size = new System.Drawing.Size(197, 20);
            this.Alias.TabIndex = 9;
            this.Alias.Text = "Alias";
            // 
            // DescriptionBox
            // 
            this.DescriptionBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.DescriptionBox.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(122)))), ((int)(((byte)(122)))));
            this.DescriptionBox.Location = new System.Drawing.Point(504, 150);
            this.DescriptionBox.Name = "DescriptionBox";
            this.DescriptionBox.Size = new System.Drawing.Size(268, 80);
            this.DescriptionBox.TabIndex = 11;
            this.DescriptionBox.Text = "Description";
            // 
            // PlaceID
            // 
            this.PlaceID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.PlaceID.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(122)))), ((int)(((byte)(122)))));
            this.PlaceID.Location = new System.Drawing.Point(504, 43);
            this.PlaceID.Name = "PlaceID";
            this.PlaceID.Size = new System.Drawing.Size(84, 20);
            this.PlaceID.TabIndex = 1;
            this.PlaceID.Text = "5315046213";
            this.PlaceID.TextChanged += new System.EventHandler(this.PlaceID_TextChanged);
            // 
            // LaunchNexus
            // 
            this.LaunchNexus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LaunchNexus.Location = new System.Drawing.Point(639, 266);
            this.LaunchNexus.Name = "LaunchNexus";
            this.LaunchNexus.Size = new System.Drawing.Size(133, 23);
            this.LaunchNexus.TabIndex = 14;
            this.LaunchNexus.Text = "Account Control";
            this.LaunchNexus.UseVisualStyleBackColor = true;
            this.LaunchNexus.Click += new System.EventHandler(this.LaunchNexus_Click);
            // 
            // copyPasswordToolStripMenuItem
            // 
            this.copyPasswordToolStripMenuItem.Name = "copyPasswordToolStripMenuItem";
            this.copyPasswordToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.copyPasswordToolStripMenuItem.Text = "Copy Password";
            this.copyPasswordToolStripMenuItem.Click += new System.EventHandler(this.copyPasswordToolStripMenuItem_Click);
            // 
            // AccountManager
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 301);
            this.Controls.Add(this.LaunchNexus);
            this.Controls.Add(this.DonateButton);
            this.Controls.Add(this.EditTheme);
            this.Controls.Add(this.SaveToAccount);
            this.Controls.Add(this.ImportByCookie);
            this.Controls.Add(this.OpenApp);
            this.Controls.Add(this.JoinDiscord);
            this.Controls.Add(this.CurrentPlace);
            this.Controls.Add(this.ArgumentsB);
            this.Controls.Add(this.BrowserButton);
            this.Controls.Add(this.HideUsernamesCheckbox);
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
            this.Controls.Add(this.AccountsView);
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(800, 340);
            this.Name = "AccountManager";
            this.Text = "Roblox Account Manager";
            this.HelpButtonClicked += new System.ComponentModel.CancelEventHandler(this.AccountManager_HelpButtonClicked);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AccountManager_FormClosing);
            this.Load += new System.EventHandler(this.AccountManager_Load);
            this.Shown += new System.EventHandler(this.AccountManager_Shown);
            this.AccountsStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.AccountsView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public Classes.BorderedTextBox PlaceID;
        public Classes.BorderedTextBox JobID;
        private System.Windows.Forms.Label LabelJobID;
        private System.Windows.Forms.Button Add;
        private System.Windows.Forms.Button Remove;
        private System.Windows.Forms.Button JoinServer;
        private System.Windows.Forms.Button SetDescription;
        private Classes.BorderedRichTextBox DescriptionBox;
        private System.Windows.Forms.Button SetAlias;
        private Classes.BorderedTextBox Alias;
        private System.Windows.Forms.Button Follow;
        private Classes.BorderedTextBox UserID;
        private System.Windows.Forms.Label LabelUserID;
        private System.Windows.Forms.Button ServerList;
        private System.Windows.Forms.ContextMenuStrip AccountsStrip;
        private System.Windows.Forms.ToolStripMenuItem addAccountsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeAccountToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem infoToolStripMenuItem;
        private System.Windows.Forms.CheckBox HideUsernamesCheckbox;
        private System.Windows.Forms.ToolStripMenuItem getAuthenticationTicketToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyRbxplayerLinkToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyUsernameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copySecurityTokenToolStripMenuItem;
        private System.Windows.Forms.Button BrowserButton;
        private System.Windows.Forms.Button ArgumentsB;
        private System.Windows.Forms.Label CurrentPlace;
        private System.Windows.Forms.Label LabelPlaceID;
        private System.Windows.Forms.Timer PlaceTimer;
        private System.Windows.Forms.ToolStripMenuItem moveGroupUpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem moveToToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyAppLinkToolStripMenuItem;
        private System.Windows.Forms.Button JoinDiscord;
        private System.Windows.Forms.Button OpenApp;
        private System.Windows.Forms.Button ImportByCookie;
        private System.Windows.Forms.ToolStripMenuItem copyProfileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewFieldsToolStripMenuItem;
        private System.Windows.Forms.Button SaveToAccount;
        private System.Windows.Forms.ToolTip SaveTooltip;
        private System.Windows.Forms.Timer SaveTimer;
        public BrightIdeasSoftware.ObjectListView AccountsView;
        private BrightIdeasSoftware.OLVColumn Group;
        private BrightIdeasSoftware.OLVColumn Username;
        private BrightIdeasSoftware.OLVColumn AccountAlias;
        private BrightIdeasSoftware.OLVColumn Description;
        private System.Windows.Forms.ToolStripMenuItem sortAlphabeticallyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toggleToolStripMenuItem;
        private System.Windows.Forms.Button EditTheme;
        private System.Windows.Forms.ToolStripMenuItem groupsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem infoToolStripMenuItem1;
        private System.Windows.Forms.Button DonateButton;
        private System.Windows.Forms.Button LaunchNexus;
        private System.Windows.Forms.ToolStripMenuItem copyPasswordToolStripMenuItem;
    }
}
using RBX_Alt_Manager.Classes;

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
            this.AddAccountsStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.manualToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.bulkUserPassToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.byCookieToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.customURLJSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyUsernameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyPasswordToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyUserPassComboToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyProfileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyUserIdToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sortAlphabeticallyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quickLogInToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.moveGroupUpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toggleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.moveToToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyGroupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.infoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.infoToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.viewFieldsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ShowDetailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
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
            this.OpenBrowserStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.customURLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.URLJSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.joinGroupToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveToAccount = new System.Windows.Forms.Button();
            this.SaveTooltip = new System.Windows.Forms.ToolTip(this.components);
            this.ShuffleIcon = new System.Windows.Forms.PictureBox();
            this.DefaultEncryptionButton = new System.Windows.Forms.Button();
            this.PasswordEncryptionButton = new System.Windows.Forms.Button();
            this.JobID = new RBX_Alt_Manager.Classes.BorderedTextBox();
            this.DonateButton = new System.Windows.Forms.Button();
            this.AccountsView = new BrightIdeasSoftware.ObjectListView();
            this.Username = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.AccountAlias = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.Description = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.Group = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.LastUsedColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.EditTheme = new System.Windows.Forms.Button();
            this.LaunchNexus = new System.Windows.Forms.Button();
            this.ConfigButton = new System.Windows.Forms.Button();
            this.HistoryIcon = new System.Windows.Forms.PictureBox();
            this.PasswordPanel = new System.Windows.Forms.Panel();
            this.PasswordLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            this.UnlockButton = new System.Windows.Forms.Button();
            this.PasswordTextBox = new System.Windows.Forms.TextBox();
            this.AccessLabel = new System.Windows.Forms.Label();
            this.PasswordRequiredLabel = new System.Windows.Forms.Label();
            this.PasswordSelectionPanel = new System.Windows.Forms.TableLayoutPanel();
            this.SetPasswordButton = new System.Windows.Forms.Button();
            this.PasswordSelectionTB = new System.Windows.Forms.TextBox();
            this.Password2Label = new System.Windows.Forms.Label();
            this.EncryptionSelectionPanel = new System.Windows.Forms.TableLayoutPanel();
            this.EncSelectionLabel = new System.Windows.Forms.Label();
            this.DownloadProgressBar = new System.Windows.Forms.ProgressBar();
            this.PresenceUpdateTimer = new System.Windows.Forms.Timer(this.components);
            this.DLChromiumLabel = new System.Windows.Forms.Label();
            this.Add = new RBX_Alt_Manager.Classes.MenuButton();
            this.OpenBrowser = new RBX_Alt_Manager.Classes.MenuButton();
            this.UserID = new RBX_Alt_Manager.Classes.BorderedTextBox();
            this.Alias = new RBX_Alt_Manager.Classes.BorderedTextBox();
            this.DescriptionBox = new RBX_Alt_Manager.Classes.BorderedRichTextBox();
            this.PlaceID = new RBX_Alt_Manager.Classes.BorderedTextBox();
            this.AddAccountsStrip.SuspendLayout();
            this.AccountsStrip.SuspendLayout();
            this.OpenBrowserStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ShuffleIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.AccountsView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.HistoryIcon)).BeginInit();
            this.PasswordPanel.SuspendLayout();
            this.PasswordLayoutPanel.SuspendLayout();
            this.PasswordSelectionPanel.SuspendLayout();
            this.EncryptionSelectionPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // LabelJobID
            // 
            this.LabelJobID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.LabelJobID.AutoSize = true;
            this.LabelJobID.Location = new System.Drawing.Point(600, 27);
            this.LabelJobID.Name = "LabelJobID";
            this.LabelJobID.Size = new System.Drawing.Size(38, 13);
            this.LabelJobID.TabIndex = 1000;
            this.LabelJobID.Text = "Job ID";
            this.SaveTooltip.SetToolTip(this.LabelJobID, "Job ID is a unique ID assigned to every roblox server.");
            // 
            // AddAccountsStrip
            // 
            this.AddAccountsStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.manualToolStripMenuItem,
            this.bulkUserPassToolStripMenuItem,
            this.byCookieToolStripMenuItem,
            this.customURLJSToolStripMenuItem});
            this.AddAccountsStrip.Name = "AddAccountsStrip";
            this.AddAccountsStrip.Size = new System.Drawing.Size(173, 92);
            // 
            // manualToolStripMenuItem
            // 
            this.manualToolStripMenuItem.Name = "manualToolStripMenuItem";
            this.manualToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.manualToolStripMenuItem.Text = "Manual Login";
            this.manualToolStripMenuItem.Click += new System.EventHandler(this.manualToolStripMenuItem_Click);
            // 
            // bulkUserPassToolStripMenuItem
            // 
            this.bulkUserPassToolStripMenuItem.Name = "bulkUserPassToolStripMenuItem";
            this.bulkUserPassToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.bulkUserPassToolStripMenuItem.Text = "User:Pass";
            this.bulkUserPassToolStripMenuItem.Click += new System.EventHandler(this.bulkUserPassToolStripMenuItem_Click);
            // 
            // byCookieToolStripMenuItem
            // 
            this.byCookieToolStripMenuItem.Name = "byCookieToolStripMenuItem";
            this.byCookieToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.byCookieToolStripMenuItem.Text = "Cookie(s)";
            this.byCookieToolStripMenuItem.Click += new System.EventHandler(this.byCookieToolStripMenuItem_Click);
            // 
            // customURLJSToolStripMenuItem
            // 
            this.customURLJSToolStripMenuItem.Name = "customURLJSToolStripMenuItem";
            this.customURLJSToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
            this.customURLJSToolStripMenuItem.Text = "Custom (URL + JS)";
            this.customURLJSToolStripMenuItem.Click += new System.EventHandler(this.customURLJSToolStripMenuItem_Click);
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
            this.JoinServer.Location = new System.Drawing.Point(503, 67);
            this.JoinServer.Name = "JoinServer";
            this.JoinServer.Size = new System.Drawing.Size(198, 23);
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
            this.ServerList.Location = new System.Drawing.Point(707, 67);
            this.ServerList.Name = "ServerList";
            this.ServerList.Size = new System.Drawing.Size(65, 23);
            this.ServerList.TabIndex = 6;
            this.ServerList.Text = "Utilities";
            this.SaveTooltip.SetToolTip(this.ServerList, "Contains Server List, Games List, and Favorites List");
            this.ServerList.UseVisualStyleBackColor = true;
            this.ServerList.Click += new System.EventHandler(this.ServerList_Click);
            // 
            // AccountsStrip
            // 
            this.AccountsStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addAccountsToolStripMenuItem,
            this.removeAccountToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.sortAlphabeticallyToolStripMenuItem,
            this.quickLogInToolStripMenuItem,
            this.moveGroupUpToolStripMenuItem,
            this.infoToolStripMenuItem,
            this.viewFieldsToolStripMenuItem,
            this.ShowDetailsToolStripMenuItem,
            this.getAuthenticationTicketToolStripMenuItem,
            this.copySecurityTokenToolStripMenuItem,
            this.copyRbxplayerLinkToolStripMenuItem,
            this.copyAppLinkToolStripMenuItem});
            this.AccountsStrip.Name = "contextMenuStrip1";
            this.AccountsStrip.Size = new System.Drawing.Size(209, 290);
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
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyUsernameToolStripMenuItem,
            this.copyPasswordToolStripMenuItem,
            this.copyUserPassComboToolStripMenuItem,
            this.copyProfileToolStripMenuItem,
            this.copyUserIdToolStripMenuItem});
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.copyToolStripMenuItem.Text = "Copy";
            // 
            // copyUsernameToolStripMenuItem
            // 
            this.copyUsernameToolStripMenuItem.Name = "copyUsernameToolStripMenuItem";
            this.copyUsernameToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.copyUsernameToolStripMenuItem.Text = "Copy Username";
            this.copyUsernameToolStripMenuItem.Click += new System.EventHandler(this.copyUsernameToolStripMenuItem_Click);
            // 
            // copyPasswordToolStripMenuItem
            // 
            this.copyPasswordToolStripMenuItem.Name = "copyPasswordToolStripMenuItem";
            this.copyPasswordToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.copyPasswordToolStripMenuItem.Text = "Copy Password";
            this.copyPasswordToolStripMenuItem.Click += new System.EventHandler(this.copyPasswordToolStripMenuItem_Click);
            // 
            // copyUserPassComboToolStripMenuItem
            // 
            this.copyUserPassComboToolStripMenuItem.Name = "copyUserPassComboToolStripMenuItem";
            this.copyUserPassComboToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.copyUserPassComboToolStripMenuItem.Text = "Copy User Pass Combo";
            this.copyUserPassComboToolStripMenuItem.Click += new System.EventHandler(this.copyUserPassComboToolStripMenuItem_Click);
            // 
            // copyProfileToolStripMenuItem
            // 
            this.copyProfileToolStripMenuItem.Name = "copyProfileToolStripMenuItem";
            this.copyProfileToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.copyProfileToolStripMenuItem.Text = "Copy Profile";
            this.copyProfileToolStripMenuItem.Click += new System.EventHandler(this.copyProfileToolStripMenuItem_Click);
            // 
            // copyUserIdToolStripMenuItem
            // 
            this.copyUserIdToolStripMenuItem.Name = "copyUserIdToolStripMenuItem";
            this.copyUserIdToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.copyUserIdToolStripMenuItem.Text = "Copy UserId";
            this.copyUserIdToolStripMenuItem.Click += new System.EventHandler(this.copyUserIdToolStripMenuItem_Click);
            // 
            // sortAlphabeticallyToolStripMenuItem
            // 
            this.sortAlphabeticallyToolStripMenuItem.Name = "sortAlphabeticallyToolStripMenuItem";
            this.sortAlphabeticallyToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.sortAlphabeticallyToolStripMenuItem.Text = "Sort Alphabetically";
            this.sortAlphabeticallyToolStripMenuItem.Click += new System.EventHandler(this.sortAlphabeticallyToolStripMenuItem_Click);
            // 
            // quickLogInToolStripMenuItem
            // 
            this.quickLogInToolStripMenuItem.Name = "quickLogInToolStripMenuItem";
            this.quickLogInToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.quickLogInToolStripMenuItem.Text = "Quick Log In";
            this.quickLogInToolStripMenuItem.Click += new System.EventHandler(this.quickLogInToolStripMenuItem_Click);
            // 
            // moveGroupUpToolStripMenuItem
            // 
            this.moveGroupUpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toggleToolStripMenuItem,
            this.moveToToolStripMenuItem,
            this.copyGroupToolStripMenuItem});
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
            // copyGroupToolStripMenuItem
            // 
            this.copyGroupToolStripMenuItem.Name = "copyGroupToolStripMenuItem";
            this.copyGroupToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
            this.copyGroupToolStripMenuItem.Text = "Copy Group";
            this.copyGroupToolStripMenuItem.Click += new System.EventHandler(this.copyGroupToolStripMenuItem_Click);
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
            // ShowDetailsToolStripMenuItem
            // 
            this.ShowDetailsToolStripMenuItem.Name = "ShowDetailsToolStripMenuItem";
            this.ShowDetailsToolStripMenuItem.Size = new System.Drawing.Size(208, 22);
            this.ShowDetailsToolStripMenuItem.Text = "Dump Details";
            this.ShowDetailsToolStripMenuItem.Click += new System.EventHandler(this.ShowDetailsToolStripMenuItem_Click);
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
            this.BrowserButton.Text = "Account Utilities";
            this.BrowserButton.UseVisualStyleBackColor = true;
            this.BrowserButton.Click += new System.EventHandler(this.BrowserButton_Click);
            // 
            // ArgumentsB
            // 
            this.ArgumentsB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ArgumentsB.Location = new System.Drawing.Point(691, 7);
            this.ArgumentsB.Name = "ArgumentsB";
            this.ArgumentsB.Size = new System.Drawing.Size(23, 23);
            this.ArgumentsB.TabIndex = 5;
            this.ArgumentsB.Text = "A";
            this.ArgumentsB.UseVisualStyleBackColor = true;
            this.ArgumentsB.Visible = false;
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
            // OpenBrowserStrip
            // 
            this.OpenBrowserStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.customURLToolStripMenuItem,
            this.URLJSToolStripMenuItem,
            this.joinGroupToolStripMenuItem});
            this.OpenBrowserStrip.Name = "OpenBrowserStrip";
            this.OpenBrowserStrip.Size = new System.Drawing.Size(161, 70);
            // 
            // customURLToolStripMenuItem
            // 
            this.customURLToolStripMenuItem.Name = "customURLToolStripMenuItem";
            this.customURLToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.customURLToolStripMenuItem.Text = "URL";
            this.customURLToolStripMenuItem.Click += new System.EventHandler(this.customURLToolStripMenuItem_Click);
            // 
            // URLJSToolStripMenuItem
            // 
            this.URLJSToolStripMenuItem.Name = "URLJSToolStripMenuItem";
            this.URLJSToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.URLJSToolStripMenuItem.Text = "URL + Javascript";
            this.URLJSToolStripMenuItem.Click += new System.EventHandler(this.URLJSToolStripMenuItem_Click);
            // 
            // joinGroupToolStripMenuItem
            // 
            this.joinGroupToolStripMenuItem.Name = "joinGroupToolStripMenuItem";
            this.joinGroupToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.joinGroupToolStripMenuItem.Text = "Join Group";
            this.joinGroupToolStripMenuItem.Click += new System.EventHandler(this.joinGroupToolStripMenuItem_Click);
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
            // ShuffleIcon
            // 
            this.ShuffleIcon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ShuffleIcon.BackColor = System.Drawing.Color.Transparent;
            this.ShuffleIcon.Image = global::RBX_Alt_Manager.Properties.Resources.ShuffleIcon;
            this.ShuffleIcon.Location = new System.Drawing.Point(583, 45);
            this.ShuffleIcon.Name = "ShuffleIcon";
            this.ShuffleIcon.Size = new System.Drawing.Size(18, 16);
            this.ShuffleIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ShuffleIcon.TabIndex = 1004;
            this.ShuffleIcon.TabStop = false;
            this.SaveTooltip.SetToolTip(this.ShuffleIcon, "Selects a random JobId every single time you press \"Join Server\"\r\nThis setting wi" +
        "ll be ignored if you have a JobId set");
            this.ShuffleIcon.Click += new System.EventHandler(this.ShuffleIcon_Click);
            // 
            // DefaultEncryptionButton
            // 
            this.DefaultEncryptionButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DefaultEncryptionButton.Cursor = System.Windows.Forms.Cursors.Help;
            this.DefaultEncryptionButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DefaultEncryptionButton.Location = new System.Drawing.Point(3, 59);
            this.DefaultEncryptionButton.Name = "DefaultEncryptionButton";
            this.DefaultEncryptionButton.Size = new System.Drawing.Size(474, 50);
            this.DefaultEncryptionButton.TabIndex = 3;
            this.DefaultEncryptionButton.Tag = "UseControlFont";
            this.DefaultEncryptionButton.Text = "Default Encryption";
            this.SaveTooltip.SetToolTip(this.DefaultEncryptionButton, "This encryption method doesn\'t allow sharing\r\nyour account data across multiple d" +
        "evices\r\nunless you manually decrypt the data");
            this.DefaultEncryptionButton.UseVisualStyleBackColor = true;
            this.DefaultEncryptionButton.Click += new System.EventHandler(this.DefaultEncryptionButton_Click);
            // 
            // PasswordEncryptionButton
            // 
            this.PasswordEncryptionButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PasswordEncryptionButton.Cursor = System.Windows.Forms.Cursors.Help;
            this.PasswordEncryptionButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PasswordEncryptionButton.Location = new System.Drawing.Point(3, 115);
            this.PasswordEncryptionButton.Name = "PasswordEncryptionButton";
            this.PasswordEncryptionButton.Size = new System.Drawing.Size(474, 51);
            this.PasswordEncryptionButton.TabIndex = 5;
            this.PasswordEncryptionButton.Tag = "UseControlFont";
            this.PasswordEncryptionButton.Text = "Password Locked (Recommended)";
            this.SaveTooltip.SetToolTip(this.PasswordEncryptionButton, "This encryption method allows sharing\r\nyour account data across multiple devices\r" +
        "\nand is recommended in order to prevent loss\r\nof data which can also be easily s" +
        "tored securely");
            this.PasswordEncryptionButton.UseVisualStyleBackColor = true;
            this.PasswordEncryptionButton.Click += new System.EventHandler(this.PasswordEncryptionButton_Click);
            // 
            // JobID
            // 
            this.JobID.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.JobID.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(122)))), ((int)(((byte)(122)))));
            this.JobID.Location = new System.Drawing.Point(602, 43);
            this.JobID.Name = "JobID";
            this.JobID.Size = new System.Drawing.Size(143, 20);
            this.JobID.TabIndex = 2;
            this.SaveTooltip.SetToolTip(this.JobID, "Job ID is a unique ID assigned to every roblox server.\r\nYou may also put a Privat" +
        "e Server link in this box to join it.");
            this.JobID.Click += new System.EventHandler(this.JobID_Click);
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
            this.DonateButton.UseVisualStyleBackColor = true;
            this.DonateButton.Click += new System.EventHandler(this.DonateButton_Click);
            // 
            // AccountsView
            // 
            this.AccountsView.AllColumns.Add(this.Username);
            this.AccountsView.AllColumns.Add(this.AccountAlias);
            this.AccountsView.AllColumns.Add(this.Description);
            this.AccountsView.AllColumns.Add(this.Group);
            this.AccountsView.AllColumns.Add(this.LastUsedColumn);
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
            this.AccountsView.Scroll += new System.EventHandler<System.Windows.Forms.ScrollEventArgs>(this.AccountsView_Scroll);
            this.AccountsView.SelectedIndexChanged += new System.EventHandler(this.AccountsView_SelectedIndexChanged);
            // 
            // Username
            // 
            this.Username.AspectName = "Username";
            this.Username.IsEditable = false;
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
            this.Group.Text = "";
            this.Group.Width = 0;
            // 
            // LastUsedColumn
            // 
            this.LastUsedColumn.AspectName = "LastUse";
            this.LastUsedColumn.AspectToStringFormat = "{0:MM/dd/yyyy hh:mm tt}";
            this.LastUsedColumn.DisplayIndex = 4;
            this.LastUsedColumn.IsVisible = false;
            this.LastUsedColumn.Text = "Last Used";
            this.LastUsedColumn.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.LastUsedColumn.Width = 130;
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
            // ConfigButton
            // 
            this.ConfigButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ConfigButton.FlatAppearance.BorderSize = 0;
            this.ConfigButton.Image = global::RBX_Alt_Manager.Properties.Resources.configIcon;
            this.ConfigButton.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
            this.ConfigButton.Location = new System.Drawing.Point(720, 6);
            this.ConfigButton.Name = "ConfigButton";
            this.ConfigButton.Size = new System.Drawing.Size(24, 24);
            this.ConfigButton.TabIndex = 1002;
            this.ConfigButton.UseVisualStyleBackColor = true;
            this.ConfigButton.Click += new System.EventHandler(this.ConfigButton_Click);
            // 
            // HistoryIcon
            // 
            this.HistoryIcon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.HistoryIcon.BackColor = System.Drawing.Color.Transparent;
            this.HistoryIcon.Image = global::RBX_Alt_Manager.Properties.Resources.icons8_history_32;
            this.HistoryIcon.Location = new System.Drawing.Point(583, 27);
            this.HistoryIcon.Name = "HistoryIcon";
            this.HistoryIcon.Size = new System.Drawing.Size(16, 16);
            this.HistoryIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.HistoryIcon.TabIndex = 1003;
            this.HistoryIcon.TabStop = false;
            this.HistoryIcon.MouseHover += new System.EventHandler(this.HistoryIcon_MouseHover);
            // 
            // PasswordPanel
            // 
            this.PasswordPanel.Controls.Add(this.PasswordLayoutPanel);
            this.PasswordPanel.Controls.Add(this.PasswordSelectionPanel);
            this.PasswordPanel.Controls.Add(this.EncryptionSelectionPanel);
            this.PasswordPanel.Location = new System.Drawing.Point(1200, 0);
            this.PasswordPanel.Name = "PasswordPanel";
            this.PasswordPanel.Size = new System.Drawing.Size(784, 301);
            this.PasswordPanel.TabIndex = 1005;
            this.PasswordPanel.Visible = false;
            this.PasswordPanel.VisibleChanged += new System.EventHandler(this.PasswordPanel_VisibleChanged);
            // 
            // PasswordLayoutPanel
            // 
            this.PasswordLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PasswordLayoutPanel.ColumnCount = 1;
            this.PasswordLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.PasswordLayoutPanel.Controls.Add(this.UnlockButton, 0, 3);
            this.PasswordLayoutPanel.Controls.Add(this.PasswordTextBox, 0, 2);
            this.PasswordLayoutPanel.Controls.Add(this.AccessLabel, 0, 0);
            this.PasswordLayoutPanel.Controls.Add(this.PasswordRequiredLabel, 0, 1);
            this.PasswordLayoutPanel.Location = new System.Drawing.Point(192, 85);
            this.PasswordLayoutPanel.Name = "PasswordLayoutPanel";
            this.PasswordLayoutPanel.RowCount = 4;
            this.PasswordLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.PasswordLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.PasswordLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
            this.PasswordLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.PasswordLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.PasswordLayoutPanel.Size = new System.Drawing.Size(400, 130);
            this.PasswordLayoutPanel.TabIndex = 5;
            // 
            // UnlockButton
            // 
            this.UnlockButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.UnlockButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UnlockButton.Location = new System.Drawing.Point(3, 92);
            this.UnlockButton.Name = "UnlockButton";
            this.UnlockButton.Size = new System.Drawing.Size(394, 35);
            this.UnlockButton.TabIndex = 3;
            this.UnlockButton.Tag = "UseControlFont";
            this.UnlockButton.Text = "Continue";
            this.UnlockButton.UseVisualStyleBackColor = true;
            this.UnlockButton.Click += new System.EventHandler(this.UnlockButton_Click);
            // 
            // PasswordTextBox
            // 
            this.PasswordTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PasswordTextBox.Location = new System.Drawing.Point(3, 67);
            this.PasswordTextBox.Name = "PasswordTextBox";
            this.PasswordTextBox.PasswordChar = '*';
            this.PasswordTextBox.Size = new System.Drawing.Size(394, 20);
            this.PasswordTextBox.TabIndex = 2;
            this.PasswordTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.PasswordTextBox_KeyPress);
            // 
            // AccessLabel
            // 
            this.AccessLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.AccessLabel.AutoSize = true;
            this.AccessLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AccessLabel.Location = new System.Drawing.Point(3, 0);
            this.AccessLabel.Name = "AccessLabel";
            this.AccessLabel.Size = new System.Drawing.Size(394, 25);
            this.AccessLabel.TabIndex = 1;
            this.AccessLabel.Tag = "UseControlFont";
            this.AccessLabel.Text = "Restricted Access";
            this.AccessLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PasswordRequiredLabel
            // 
            this.PasswordRequiredLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PasswordRequiredLabel.AutoSize = true;
            this.PasswordRequiredLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.PasswordRequiredLabel.Location = new System.Drawing.Point(3, 25);
            this.PasswordRequiredLabel.Name = "PasswordRequiredLabel";
            this.PasswordRequiredLabel.Size = new System.Drawing.Size(394, 39);
            this.PasswordRequiredLabel.TabIndex = 0;
            this.PasswordRequiredLabel.Tag = "UseControlFont";
            this.PasswordRequiredLabel.Text = "Please enter your password to continue";
            this.PasswordRequiredLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // PasswordSelectionPanel
            // 
            this.PasswordSelectionPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PasswordSelectionPanel.ColumnCount = 1;
            this.PasswordSelectionPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.PasswordSelectionPanel.Controls.Add(this.SetPasswordButton, 0, 2);
            this.PasswordSelectionPanel.Controls.Add(this.PasswordSelectionTB, 0, 1);
            this.PasswordSelectionPanel.Controls.Add(this.Password2Label, 0, 0);
            this.PasswordSelectionPanel.Location = new System.Drawing.Point(242, 80);
            this.PasswordSelectionPanel.Name = "PasswordSelectionPanel";
            this.PasswordSelectionPanel.RowCount = 3;
            this.PasswordSelectionPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.PasswordSelectionPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.PasswordSelectionPanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.PasswordSelectionPanel.Size = new System.Drawing.Size(300, 88);
            this.PasswordSelectionPanel.TabIndex = 6;
            // 
            // SetPasswordButton
            // 
            this.SetPasswordButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SetPasswordButton.Enabled = false;
            this.SetPasswordButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.SetPasswordButton.Location = new System.Drawing.Point(3, 51);
            this.SetPasswordButton.Name = "SetPasswordButton";
            this.SetPasswordButton.Size = new System.Drawing.Size(294, 34);
            this.SetPasswordButton.TabIndex = 3;
            this.SetPasswordButton.Tag = "UseControlFont";
            this.SetPasswordButton.Text = "Continue";
            this.SetPasswordButton.UseVisualStyleBackColor = true;
            this.SetPasswordButton.Click += new System.EventHandler(this.SetPasswordButton_Click);
            // 
            // PasswordSelectionTB
            // 
            this.PasswordSelectionTB.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PasswordSelectionTB.Location = new System.Drawing.Point(3, 25);
            this.PasswordSelectionTB.Name = "PasswordSelectionTB";
            this.PasswordSelectionTB.PasswordChar = '*';
            this.PasswordSelectionTB.ShortcutsEnabled = false;
            this.PasswordSelectionTB.Size = new System.Drawing.Size(294, 20);
            this.PasswordSelectionTB.TabIndex = 2;
            this.PasswordSelectionTB.TextChanged += new System.EventHandler(this.PasswordSelectionTB_TextChanged);
            // 
            // Password2Label
            // 
            this.Password2Label.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Password2Label.AutoSize = true;
            this.Password2Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.Password2Label.Location = new System.Drawing.Point(3, 0);
            this.Password2Label.Name = "Password2Label";
            this.Password2Label.Size = new System.Drawing.Size(294, 22);
            this.Password2Label.TabIndex = 0;
            this.Password2Label.Tag = "UseControlFont";
            this.Password2Label.Text = "Password";
            this.Password2Label.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // EncryptionSelectionPanel
            // 
            this.EncryptionSelectionPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.EncryptionSelectionPanel.ColumnCount = 1;
            this.EncryptionSelectionPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.EncryptionSelectionPanel.Controls.Add(this.PasswordEncryptionButton, 0, 2);
            this.EncryptionSelectionPanel.Controls.Add(this.EncSelectionLabel, 0, 0);
            this.EncryptionSelectionPanel.Controls.Add(this.DefaultEncryptionButton, 0, 1);
            this.EncryptionSelectionPanel.Location = new System.Drawing.Point(152, 65);
            this.EncryptionSelectionPanel.Name = "EncryptionSelectionPanel";
            this.EncryptionSelectionPanel.RowCount = 3;
            this.EncryptionSelectionPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.EncryptionSelectionPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.EncryptionSelectionPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.EncryptionSelectionPanel.Size = new System.Drawing.Size(480, 169);
            this.EncryptionSelectionPanel.TabIndex = 6;
            this.EncryptionSelectionPanel.Visible = false;
            // 
            // EncSelectionLabel
            // 
            this.EncSelectionLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.EncSelectionLabel.AutoSize = true;
            this.EncSelectionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.EncSelectionLabel.Location = new System.Drawing.Point(3, 0);
            this.EncSelectionLabel.Name = "EncSelectionLabel";
            this.EncSelectionLabel.Size = new System.Drawing.Size(474, 56);
            this.EncSelectionLabel.TabIndex = 4;
            this.EncSelectionLabel.Tag = "UseControlFont";
            this.EncSelectionLabel.Text = "Please select how you want your data to be secured";
            this.EncSelectionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DownloadProgressBar
            // 
            this.DownloadProgressBar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DownloadProgressBar.BackColor = System.Drawing.SystemColors.MenuText;
            this.DownloadProgressBar.Cursor = System.Windows.Forms.Cursors.Help;
            this.DownloadProgressBar.Location = new System.Drawing.Point(13, 277);
            this.DownloadProgressBar.Name = "DownloadProgressBar";
            this.DownloadProgressBar.Size = new System.Drawing.Size(196, 11);
            this.DownloadProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.DownloadProgressBar.TabIndex = 7;
            this.DownloadProgressBar.Visible = false;
            this.DownloadProgressBar.Click += new System.EventHandler(this.DownloadProgressBar_Click);
            // 
            // PresenceUpdateTimer
            // 
            this.PresenceUpdateTimer.Interval = 60000;
            // 
            // DLChromiumLabel
            // 
            this.DLChromiumLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.DLChromiumLabel.AutoSize = true;
            this.DLChromiumLabel.Cursor = System.Windows.Forms.Cursors.Help;
            this.DLChromiumLabel.Location = new System.Drawing.Point(49, 261);
            this.DLChromiumLabel.Name = "DLChromiumLabel";
            this.DLChromiumLabel.Size = new System.Drawing.Size(127, 13);
            this.DLChromiumLabel.TabIndex = 1006;
            this.DLChromiumLabel.Text = "Downloading Chromium...";
            this.DLChromiumLabel.Visible = false;
            this.DLChromiumLabel.Click += new System.EventHandler(this.DLChromiumLabel_Click);
            // 
            // Add
            // 
            this.Add.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.Add.Location = new System.Drawing.Point(13, 266);
            this.Add.Menu = this.AddAccountsStrip;
            this.Add.Name = "Add";
            this.Add.Size = new System.Drawing.Size(95, 23);
            this.Add.TabIndex = 14;
            this.Add.Text = "Add Account";
            this.Add.UseVisualStyleBackColor = true;
            this.Add.Click += new System.EventHandler(this.Add_Click);
            // 
            // OpenBrowser
            // 
            this.OpenBrowser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.OpenBrowser.Location = new System.Drawing.Point(325, 266);
            this.OpenBrowser.Menu = this.OpenBrowserStrip;
            this.OpenBrowser.Name = "OpenBrowser";
            this.OpenBrowser.Size = new System.Drawing.Size(143, 23);
            this.OpenBrowser.TabIndex = 18;
            this.OpenBrowser.Text = "Open Browser";
            this.OpenBrowser.UseVisualStyleBackColor = true;
            this.OpenBrowser.Click += new System.EventHandler(this.OpenBrowser_Click);
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
            this.PlaceID.Size = new System.Drawing.Size(78, 20);
            this.PlaceID.TabIndex = 1;
            this.PlaceID.Text = "5315046213";
            this.PlaceID.Click += new System.EventHandler(this.PlaceID_Click);
            this.PlaceID.TextChanged += new System.EventHandler(this.PlaceID_TextChanged);
            // 
            // AccountManager
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 301);
            this.Controls.Add(this.Remove);
            this.Controls.Add(this.Add);
            this.Controls.Add(this.DLChromiumLabel);
            this.Controls.Add(this.ShuffleIcon);
            this.Controls.Add(this.HistoryIcon);
            this.Controls.Add(this.ConfigButton);
            this.Controls.Add(this.LaunchNexus);
            this.Controls.Add(this.DonateButton);
            this.Controls.Add(this.EditTheme);
            this.Controls.Add(this.SaveToAccount);
            this.Controls.Add(this.OpenBrowser);
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
            this.Controls.Add(this.LabelJobID);
            this.Controls.Add(this.LabelPlaceID);
            this.Controls.Add(this.JobID);
            this.Controls.Add(this.PlaceID);
            this.Controls.Add(this.AccountsView);
            this.Controls.Add(this.PasswordPanel);
            this.Controls.Add(this.DownloadProgressBar);
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(800, 340);
            this.Name = "AccountManager";
            this.Text = "Roblox Account Manager";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AccountManager_FormClosing);
            this.Load += new System.EventHandler(this.AccountManager_Load);
            this.Shown += new System.EventHandler(this.AccountManager_Shown);
            this.AddAccountsStrip.ResumeLayout(false);
            this.AccountsStrip.ResumeLayout(false);
            this.OpenBrowserStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ShuffleIcon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.AccountsView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.HistoryIcon)).EndInit();
            this.PasswordPanel.ResumeLayout(false);
            this.PasswordLayoutPanel.ResumeLayout(false);
            this.PasswordLayoutPanel.PerformLayout();
            this.PasswordSelectionPanel.ResumeLayout(false);
            this.PasswordSelectionPanel.PerformLayout();
            this.EncryptionSelectionPanel.ResumeLayout(false);
            this.EncryptionSelectionPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public Classes.BorderedTextBox PlaceID;
        public Classes.BorderedTextBox JobID;
        private System.Windows.Forms.Label LabelJobID;
        private MenuButton Add;
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
        private MenuButton OpenBrowser;
        private System.Windows.Forms.ToolStripMenuItem copyProfileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewFieldsToolStripMenuItem;
        private System.Windows.Forms.Button SaveToAccount;
        private System.Windows.Forms.ToolTip SaveTooltip;
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
        private System.Windows.Forms.Button ConfigButton;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyUserIdToolStripMenuItem;
        private System.Windows.Forms.PictureBox HistoryIcon;
        private BrightIdeasSoftware.OLVColumn LastUsedColumn;
        private System.Windows.Forms.ToolStripMenuItem copyGroupToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyUserPassComboToolStripMenuItem;
        private System.Windows.Forms.PictureBox ShuffleIcon;
        private System.Windows.Forms.ToolStripMenuItem ShowDetailsToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip AddAccountsStrip;
        private System.Windows.Forms.ToolStripMenuItem manualToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem bulkUserPassToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem byCookieToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip OpenBrowserStrip;
        private System.Windows.Forms.ToolStripMenuItem customURLToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem URLJSToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem joinGroupToolStripMenuItem;
        private System.Windows.Forms.Panel PasswordPanel;
        private System.Windows.Forms.TextBox PasswordTextBox;
        private System.Windows.Forms.Label AccessLabel;
        private System.Windows.Forms.Button UnlockButton;
        private System.Windows.Forms.TableLayoutPanel PasswordLayoutPanel;
        private System.Windows.Forms.Label PasswordRequiredLabel;
        private System.Windows.Forms.TableLayoutPanel EncryptionSelectionPanel;
        private System.Windows.Forms.Label EncSelectionLabel;
        private System.Windows.Forms.Button DefaultEncryptionButton;
        private System.Windows.Forms.Button PasswordEncryptionButton;
        private System.Windows.Forms.TableLayoutPanel PasswordSelectionPanel;
        private System.Windows.Forms.Button SetPasswordButton;
        private System.Windows.Forms.TextBox PasswordSelectionTB;
        private System.Windows.Forms.Label Password2Label;
        private System.Windows.Forms.Timer PresenceUpdateTimer;
        private System.Windows.Forms.ToolStripMenuItem quickLogInToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem customURLJSToolStripMenuItem;
        private System.Windows.Forms.ProgressBar DownloadProgressBar;
        private System.Windows.Forms.Label DLChromiumLabel;
    }
}
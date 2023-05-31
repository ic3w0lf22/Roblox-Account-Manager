using RBX_Alt_Manager.Classes;
using RBX_Alt_Manager.Forms;

namespace RBX_Alt_Manager
{
    partial class ServerList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ServerList));
            this.ServerID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Players = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Ping = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.FPS = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ServerListStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.joinServerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyJobIdToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loadRegionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.GamesStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.joinGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addToFavoritesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyPlaceIdToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.ID = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.FavoritesStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyPlaceIDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.placeIdCol = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.universeIdCol = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.ServerTypeColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.OPITip = new System.Windows.Forms.ToolTip(this.components);
            this.OtherPlaceId = new RBX_Alt_Manager.Classes.BorderedTextBox();
            this.Username = new RBX_Alt_Manager.Classes.BorderedTextBox();
            this.Favorite = new System.Windows.Forms.Button();
            this.VerifyDataModelCB = new System.Windows.Forms.CheckBox();
            this.WatcherTimer = new System.Windows.Forms.Timer(this.components);
            this.OpenLogsButton = new System.Windows.Forms.Button();
            this.Tabs = new RBX_Alt_Manager.Classes.NBTabControl();
            this.ServersTab = new System.Windows.Forms.TabPage();
            this.ServerListView = new BrightIdeasSoftware.ObjectListView();
            this.JobId = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.Playing = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.PingColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.RegionColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.RefreshServers = new System.Windows.Forms.Button();
            this.UsernameLabel = new System.Windows.Forms.Label();
            this.SearchPlayer = new System.Windows.Forms.Button();
            this.GamesPage = new System.Windows.Forms.TabPage();
            this.ListViewCB = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.PageNum = new System.Windows.Forms.NumericUpDown();
            this.Search = new System.Windows.Forms.Button();
            this.Term = new RBX_Alt_Manager.Classes.BorderedTextBox();
            this.GameListPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.GamesListView = new BrightIdeasSoftware.ObjectListView();
            this.name = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.playerCount = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.likeRatio = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.FavoritesPage = new System.Windows.Forms.TabPage();
            this.FavoriteListViewCB = new System.Windows.Forms.CheckBox();
            this.FavoriteGamesPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.FavoritesListView = new BrightIdeasSoftware.ObjectListView();
            this.GameName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.UniversePage = new System.Windows.Forms.TabPage();
            this.ViewUniverse = new System.Windows.Forms.Button();
            this.uUniverseIdLabel = new System.Windows.Forms.Label();
            this.UniverseIDTB = new RBX_Alt_Manager.Classes.BorderedTextBox();
            this.PlaceIDUniTB = new RBX_Alt_Manager.Classes.BorderedTextBox();
            this.uPlaceIDLabel = new System.Windows.Forms.Label();
            this.GetUniverseID = new System.Windows.Forms.Button();
            this.UniverseGamesPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.OutfitsPage = new System.Windows.Forms.TabPage();
            this.WearCustomButton = new System.Windows.Forms.Button();
            this.OutfitUsernameTB = new RBX_Alt_Manager.Classes.BorderedTextBox();
            this.OutfitUsernameLabel = new System.Windows.Forms.Label();
            this.ViewOutfits = new System.Windows.Forms.Button();
            this.OutfitsPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.RobloxScan = new System.Windows.Forms.TabPage();
            this.WatcherPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.RobloxScannerCB = new System.Windows.Forms.CheckBox();
            this.ScanESLabel = new System.Windows.Forms.Label();
            this.ScanIntervalN = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.ReadIntervalN = new System.Windows.Forms.NumericUpDown();
            this.ExitIfBetaDetectedCB = new System.Windows.Forms.CheckBox();
            this.ExitIfNoConnectionCB = new System.Windows.Forms.CheckBox();
            this.TimeoutNum = new System.Windows.Forms.NumericUpDown();
            this.ConnectionSecondsLabel = new System.Windows.Forms.Label();
            this.IgnoreExistingProcesses = new System.Windows.Forms.CheckBox();
            this.RbxMemoryCB = new System.Windows.Forms.CheckBox();
            this.RbxMemoryLTNum = new System.Windows.Forms.NumericUpDown();
            this.MBLabel = new System.Windows.Forms.Label();
            this.CloseRbxWindowTitleCB = new System.Windows.Forms.CheckBox();
            this.RbxWindowNameTB = new System.Windows.Forms.TextBox();
            this.SaveWindowPositionsCB = new System.Windows.Forms.CheckBox();
            this.ServerListStrip.SuspendLayout();
            this.GamesStrip.SuspendLayout();
            this.FavoritesStrip.SuspendLayout();
            this.Tabs.SuspendLayout();
            this.ServersTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ServerListView)).BeginInit();
            this.GamesPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PageNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GamesListView)).BeginInit();
            this.FavoritesPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FavoritesListView)).BeginInit();
            this.UniversePage.SuspendLayout();
            this.OutfitsPage.SuspendLayout();
            this.RobloxScan.SuspendLayout();
            this.WatcherPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ScanIntervalN)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ReadIntervalN)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TimeoutNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.RbxMemoryLTNum)).BeginInit();
            this.SuspendLayout();
            // 
            // ServerID
            // 
            this.ServerID.Text = "Server JobId";
            this.ServerID.Width = 220;
            // 
            // Players
            // 
            this.Players.Text = "Players";
            this.Players.Width = 50;
            // 
            // Ping
            // 
            this.Ping.Text = "Ping";
            this.Ping.Width = 50;
            // 
            // FPS
            // 
            this.FPS.Text = "FPS";
            this.FPS.Width = 70;
            // 
            // ServerListStrip
            // 
            this.ServerListStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.joinServerToolStripMenuItem,
            this.copyJobIdToolStripMenuItem,
            this.loadRegionToolStripMenuItem});
            this.ServerListStrip.Name = "ServerListStrip";
            this.ServerListStrip.Size = new System.Drawing.Size(141, 70);
            // 
            // joinServerToolStripMenuItem
            // 
            this.joinServerToolStripMenuItem.Name = "joinServerToolStripMenuItem";
            this.joinServerToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.joinServerToolStripMenuItem.Text = "Join Server";
            this.joinServerToolStripMenuItem.Click += new System.EventHandler(this.joinServerToolStripMenuItem_Click);
            // 
            // copyJobIdToolStripMenuItem
            // 
            this.copyJobIdToolStripMenuItem.Name = "copyJobIdToolStripMenuItem";
            this.copyJobIdToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.copyJobIdToolStripMenuItem.Text = "Copy JobId";
            this.copyJobIdToolStripMenuItem.Click += new System.EventHandler(this.copyJobIdToolStripMenuItem_Click);
            // 
            // loadRegionToolStripMenuItem
            // 
            this.loadRegionToolStripMenuItem.Name = "loadRegionToolStripMenuItem";
            this.loadRegionToolStripMenuItem.Size = new System.Drawing.Size(140, 22);
            this.loadRegionToolStripMenuItem.Text = "Load Region";
            this.loadRegionToolStripMenuItem.Click += new System.EventHandler(this.loadRegionToolStripMenuItem_Click);
            // 
            // GamesStrip
            // 
            this.GamesStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.joinGameToolStripMenuItem,
            this.addToFavoritesToolStripMenuItem,
            this.copyPlaceIdToolStripMenuItem1});
            this.GamesStrip.Name = "GamesStrip";
            this.GamesStrip.Size = new System.Drawing.Size(144, 70);
            // 
            // joinGameToolStripMenuItem
            // 
            this.joinGameToolStripMenuItem.Name = "joinGameToolStripMenuItem";
            this.joinGameToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.joinGameToolStripMenuItem.Text = "Join Game";
            this.joinGameToolStripMenuItem.Click += new System.EventHandler(this.joinGameToolStripMenuItem_Click);
            // 
            // addToFavoritesToolStripMenuItem
            // 
            this.addToFavoritesToolStripMenuItem.Name = "addToFavoritesToolStripMenuItem";
            this.addToFavoritesToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.addToFavoritesToolStripMenuItem.Text = "Favorite";
            this.addToFavoritesToolStripMenuItem.Click += new System.EventHandler(this.addToFavoritesToolStripMenuItem_Click);
            // 
            // copyPlaceIdToolStripMenuItem1
            // 
            this.copyPlaceIdToolStripMenuItem1.Name = "copyPlaceIdToolStripMenuItem1";
            this.copyPlaceIdToolStripMenuItem1.Size = new System.Drawing.Size(143, 22);
            this.copyPlaceIdToolStripMenuItem1.Text = "Copy PlaceId";
            this.copyPlaceIdToolStripMenuItem1.Click += new System.EventHandler(this.copyPlaceIdToolStripMenuItem1_Click);
            // 
            // ID
            // 
            this.ID.AspectName = "PlaceID";
            this.ID.DisplayIndex = 1;
            this.ID.IsVisible = false;
            this.ID.Text = "PlaceID";
            // 
            // FavoritesStrip
            // 
            this.FavoritesStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem2,
            this.removeToolStripMenuItem,
            this.copyPlaceIDToolStripMenuItem});
            this.FavoritesStrip.Name = "GamesStrip";
            this.FavoritesStrip.Size = new System.Drawing.Size(145, 92);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(144, 22);
            this.toolStripMenuItem1.Text = "Join Game";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(144, 22);
            this.toolStripMenuItem2.Text = "Rename";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // removeToolStripMenuItem
            // 
            this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            this.removeToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.removeToolStripMenuItem.Text = "Remove";
            this.removeToolStripMenuItem.Click += new System.EventHandler(this.removeToolStripMenuItem_Click);
            // 
            // copyPlaceIDToolStripMenuItem
            // 
            this.copyPlaceIDToolStripMenuItem.Name = "copyPlaceIDToolStripMenuItem";
            this.copyPlaceIDToolStripMenuItem.Size = new System.Drawing.Size(144, 22);
            this.copyPlaceIDToolStripMenuItem.Text = "Copy PlaceID";
            this.copyPlaceIDToolStripMenuItem.Click += new System.EventHandler(this.copyPlaceIDToolStripMenuItem_Click);
            // 
            // placeIdCol
            // 
            this.placeIdCol.AspectName = "placeId";
            this.placeIdCol.DisplayIndex = 3;
            this.placeIdCol.IsVisible = false;
            this.placeIdCol.Text = "Place Id";
            this.placeIdCol.Width = 78;
            // 
            // universeIdCol
            // 
            this.universeIdCol.AspectName = "universeId";
            this.universeIdCol.IsVisible = false;
            this.universeIdCol.Text = "Universe Id";
            this.universeIdCol.Width = 90;
            // 
            // ServerTypeColumn
            // 
            this.ServerTypeColumn.AspectName = "type";
            this.ServerTypeColumn.DisplayIndex = 3;
            this.ServerTypeColumn.IsVisible = false;
            this.ServerTypeColumn.Text = "Type";
            this.ServerTypeColumn.Width = 45;
            // 
            // OtherPlaceId
            // 
            this.OtherPlaceId.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.OtherPlaceId.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(122)))), ((int)(((byte)(122)))));
            this.OtherPlaceId.Location = new System.Drawing.Point(102, 9);
            this.OtherPlaceId.Name = "OtherPlaceId";
            this.OtherPlaceId.Size = new System.Drawing.Size(111, 20);
            this.OtherPlaceId.TabIndex = 6;
            this.OPITip.SetToolTip(this.OtherPlaceId, resources.GetString("OtherPlaceId.ToolTip"));
            // 
            // Username
            // 
            this.Username.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Username.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(122)))), ((int)(((byte)(122)))));
            this.Username.Location = new System.Drawing.Point(290, 9);
            this.Username.Name = "Username";
            this.Username.Size = new System.Drawing.Size(117, 20);
            this.Username.TabIndex = 1;
            this.OPITip.SetToolTip(this.Username, "The Roblox Username of who you are searching for (PlaceId must be correct or it m" +
        "ay not find anyone)");
            // 
            // Favorite
            // 
            this.Favorite.Location = new System.Drawing.Point(6, 6);
            this.Favorite.Name = "Favorite";
            this.Favorite.Size = new System.Drawing.Size(130, 23);
            this.Favorite.TabIndex = 8;
            this.Favorite.Text = "Favorite Current Game";
            this.OPITip.SetToolTip(this.Favorite, "To add a private server to your favorites, make sure the entire private server li" +
        "nk is in the JobId box.");
            this.Favorite.UseVisualStyleBackColor = true;
            this.Favorite.Click += new System.EventHandler(this.Favorite_Click);
            // 
            // VerifyDataModelCB
            // 
            this.VerifyDataModelCB.AutoSize = true;
            this.WatcherPanel.SetFlowBreak(this.VerifyDataModelCB, true);
            this.VerifyDataModelCB.Location = new System.Drawing.Point(11, 158);
            this.VerifyDataModelCB.Name = "VerifyDataModelCB";
            this.VerifyDataModelCB.Size = new System.Drawing.Size(136, 17);
            this.VerifyDataModelCB.TabIndex = 6;
            this.VerifyDataModelCB.Text = "Data Model Verification";
            this.OPITip.SetToolTip(this.VerifyDataModelCB, "Verifies that the closing signal is valid.\r\nThis was added because games can prin" +
        "t anything to roblox\'s logs including the text RAM scans for to detect the beta " +
        "app.");
            this.VerifyDataModelCB.UseVisualStyleBackColor = true;
            this.VerifyDataModelCB.CheckedChanged += new System.EventHandler(this.VerifyDataModelCB_CheckedChanged);
            // 
            // WatcherTimer
            // 
            this.WatcherTimer.Interval = 12000;
            this.WatcherTimer.Tick += new System.EventHandler(this.WatcherTimer_Tick);
            // 
            // OpenLogsButton
            // 
            this.OpenLogsButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.OpenLogsButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.OpenLogsButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 6F);
            this.OpenLogsButton.Location = new System.Drawing.Point(475, 283);
            this.OpenLogsButton.Name = "OpenLogsButton";
            this.OpenLogsButton.Size = new System.Drawing.Size(16, 16);
            this.OpenLogsButton.TabIndex = 16;
            this.OpenLogsButton.Tag = "NoScaling";
            this.OpenLogsButton.Text = "L";
            this.OpenLogsButton.UseVisualStyleBackColor = true;
            this.OpenLogsButton.Visible = false;
            this.OpenLogsButton.Click += new System.EventHandler(this.OpenLogsButton_Click);
            // 
            // Tabs
            // 
            this.Tabs.Controls.Add(this.ServersTab);
            this.Tabs.Controls.Add(this.GamesPage);
            this.Tabs.Controls.Add(this.FavoritesPage);
            this.Tabs.Controls.Add(this.UniversePage);
            this.Tabs.Controls.Add(this.OutfitsPage);
            this.Tabs.Controls.Add(this.RobloxScan);
            this.Tabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Tabs.Location = new System.Drawing.Point(0, 0);
            this.Tabs.Name = "Tabs";
            this.Tabs.SelectedIndex = 0;
            this.Tabs.Size = new System.Drawing.Size(499, 307);
            this.Tabs.TabIndex = 6;
            this.Tabs.SelectedIndexChanged += new System.EventHandler(this.Tabs_SelectedIndexChanged);
            // 
            // ServersTab
            // 
            this.ServersTab.BackColor = System.Drawing.Color.Transparent;
            this.ServersTab.Controls.Add(this.OtherPlaceId);
            this.ServersTab.Controls.Add(this.ServerListView);
            this.ServersTab.Controls.Add(this.Username);
            this.ServersTab.Controls.Add(this.RefreshServers);
            this.ServersTab.Controls.Add(this.UsernameLabel);
            this.ServersTab.Controls.Add(this.SearchPlayer);
            this.ServersTab.Location = new System.Drawing.Point(4, 25);
            this.ServersTab.Name = "ServersTab";
            this.ServersTab.Padding = new System.Windows.Forms.Padding(3);
            this.ServersTab.Size = new System.Drawing.Size(491, 278);
            this.ServersTab.TabIndex = 0;
            this.ServersTab.Text = "Servers";
            // 
            // ServerListView
            // 
            this.ServerListView.AllColumns.Add(this.JobId);
            this.ServerListView.AllColumns.Add(this.Playing);
            this.ServerListView.AllColumns.Add(this.PingColumn);
            this.ServerListView.AllColumns.Add(this.ServerTypeColumn);
            this.ServerListView.AllColumns.Add(this.RegionColumn);
            this.ServerListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ServerListView.CellEditUseWholeCell = false;
            this.ServerListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.JobId,
            this.Playing,
            this.PingColumn,
            this.RegionColumn});
            this.ServerListView.ContextMenuStrip = this.ServerListStrip;
            this.ServerListView.Cursor = System.Windows.Forms.Cursors.Default;
            this.ServerListView.GridLines = true;
            this.ServerListView.HideSelection = false;
            this.ServerListView.Location = new System.Drawing.Point(7, 37);
            this.ServerListView.MultiSelect = false;
            this.ServerListView.Name = "ServerListView";
            this.ServerListView.ShowGroups = false;
            this.ServerListView.Size = new System.Drawing.Size(481, 236);
            this.ServerListView.TabIndex = 5;
            this.ServerListView.UseCompatibleStateImageBehavior = false;
            this.ServerListView.View = System.Windows.Forms.View.Details;
            this.ServerListView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ServerListView_MouseDoubleClick);
            // 
            // JobId
            // 
            this.JobId.AspectName = "id";
            this.JobId.Text = "Job ID";
            this.JobId.Width = 226;
            // 
            // Playing
            // 
            this.Playing.AspectName = "playing";
            this.Playing.Text = "Playing";
            this.Playing.Width = 52;
            // 
            // PingColumn
            // 
            this.PingColumn.AspectName = "ping";
            this.PingColumn.Text = "Ping";
            this.PingColumn.Width = 40;
            // 
            // RegionColumn
            // 
            this.RegionColumn.AspectName = "region";
            this.RegionColumn.Text = "Region";
            this.RegionColumn.Width = 113;
            // 
            // RefreshServers
            // 
            this.RefreshServers.Location = new System.Drawing.Point(6, 8);
            this.RefreshServers.Name = "RefreshServers";
            this.RefreshServers.Size = new System.Drawing.Size(75, 23);
            this.RefreshServers.TabIndex = 0;
            this.RefreshServers.Text = "Refresh";
            this.RefreshServers.UseVisualStyleBackColor = true;
            this.RefreshServers.Click += new System.EventHandler(this.RefreshServers_Click);
            // 
            // UsernameLabel
            // 
            this.UsernameLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.UsernameLabel.AutoSize = true;
            this.UsernameLabel.Location = new System.Drawing.Point(229, 13);
            this.UsernameLabel.Name = "UsernameLabel";
            this.UsernameLabel.Size = new System.Drawing.Size(55, 13);
            this.UsernameLabel.TabIndex = 3;
            this.UsernameLabel.Text = "Username";
            // 
            // SearchPlayer
            // 
            this.SearchPlayer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SearchPlayer.Location = new System.Drawing.Point(413, 8);
            this.SearchPlayer.Name = "SearchPlayer";
            this.SearchPlayer.Size = new System.Drawing.Size(75, 21);
            this.SearchPlayer.TabIndex = 2;
            this.SearchPlayer.Text = "Search";
            this.SearchPlayer.UseVisualStyleBackColor = true;
            this.SearchPlayer.Click += new System.EventHandler(this.SearchPlayer_Click);
            // 
            // GamesPage
            // 
            this.GamesPage.Controls.Add(this.ListViewCB);
            this.GamesPage.Controls.Add(this.label1);
            this.GamesPage.Controls.Add(this.PageNum);
            this.GamesPage.Controls.Add(this.Search);
            this.GamesPage.Controls.Add(this.Term);
            this.GamesPage.Controls.Add(this.GameListPanel);
            this.GamesPage.Controls.Add(this.GamesListView);
            this.GamesPage.Location = new System.Drawing.Point(4, 25);
            this.GamesPage.Name = "GamesPage";
            this.GamesPage.Padding = new System.Windows.Forms.Padding(3);
            this.GamesPage.Size = new System.Drawing.Size(491, 278);
            this.GamesPage.TabIndex = 3;
            this.GamesPage.Text = "Games";
            this.GamesPage.UseVisualStyleBackColor = true;
            // 
            // ListViewCB
            // 
            this.ListViewCB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ListViewCB.AutoSize = true;
            this.ListViewCB.Location = new System.Drawing.Point(266, 7);
            this.ListViewCB.Name = "ListViewCB";
            this.ListViewCB.Size = new System.Drawing.Size(68, 17);
            this.ListViewCB.TabIndex = 12;
            this.ListViewCB.Text = "List View";
            this.ListViewCB.UseVisualStyleBackColor = true;
            this.ListViewCB.CheckedChanged += new System.EventHandler(this.ListViewCB_CheckedChanged);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(353, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Page";
            // 
            // PageNum
            // 
            this.PageNum.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.PageNum.Location = new System.Drawing.Point(391, 6);
            this.PageNum.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.PageNum.Name = "PageNum";
            this.PageNum.Size = new System.Drawing.Size(40, 20);
            this.PageNum.TabIndex = 9;
            // 
            // Search
            // 
            this.Search.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Search.Location = new System.Drawing.Point(437, 5);
            this.Search.Name = "Search";
            this.Search.Size = new System.Drawing.Size(54, 22);
            this.Search.TabIndex = 8;
            this.Search.Text = "Search";
            this.Search.UseVisualStyleBackColor = true;
            this.Search.Click += new System.EventHandler(this.Search_Click);
            // 
            // Term
            // 
            this.Term.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Term.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(122)))), ((int)(((byte)(122)))));
            this.Term.Location = new System.Drawing.Point(6, 6);
            this.Term.Name = "Term";
            this.Term.Size = new System.Drawing.Size(254, 20);
            this.Term.TabIndex = 7;
            this.Term.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Term_KeyPress);
            // 
            // GameListPanel
            // 
            this.GameListPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GameListPanel.AutoScroll = true;
            this.GameListPanel.Location = new System.Drawing.Point(3, 28);
            this.GameListPanel.Name = "GameListPanel";
            this.GameListPanel.Size = new System.Drawing.Size(485, 247);
            this.GameListPanel.TabIndex = 11;
            // 
            // GamesListView
            // 
            this.GamesListView.AllColumns.Add(this.name);
            this.GamesListView.AllColumns.Add(this.playerCount);
            this.GamesListView.AllColumns.Add(this.likeRatio);
            this.GamesListView.AllColumns.Add(this.placeIdCol);
            this.GamesListView.AllColumns.Add(this.universeIdCol);
            this.GamesListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GamesListView.CellEditUseWholeCell = false;
            this.GamesListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.name,
            this.playerCount,
            this.likeRatio});
            this.GamesListView.ContextMenuStrip = this.GamesStrip;
            this.GamesListView.Cursor = System.Windows.Forms.Cursors.Default;
            this.GamesListView.FullRowSelect = true;
            this.GamesListView.GridLines = true;
            this.GamesListView.HideSelection = false;
            this.GamesListView.Location = new System.Drawing.Point(3, 31);
            this.GamesListView.Name = "GamesListView";
            this.GamesListView.ShowGroups = false;
            this.GamesListView.Size = new System.Drawing.Size(485, 247);
            this.GamesListView.TabIndex = 6;
            this.GamesListView.UseCompatibleStateImageBehavior = false;
            this.GamesListView.View = System.Windows.Forms.View.Details;
            this.GamesListView.Visible = false;
            this.GamesListView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.GamesListView_MouseClick);
            // 
            // name
            // 
            this.name.AspectName = "Name";
            this.name.Text = "Game Name";
            this.name.Width = 310;
            // 
            // playerCount
            // 
            this.playerCount.AspectName = "PlayerCount";
            this.playerCount.Text = "Players";
            // 
            // likeRatio
            // 
            this.likeRatio.AspectName = "LikeRatio";
            this.likeRatio.Text = "Like %";
            // 
            // FavoritesPage
            // 
            this.FavoritesPage.Controls.Add(this.FavoriteListViewCB);
            this.FavoritesPage.Controls.Add(this.Favorite);
            this.FavoritesPage.Controls.Add(this.FavoriteGamesPanel);
            this.FavoritesPage.Controls.Add(this.FavoritesListView);
            this.FavoritesPage.Location = new System.Drawing.Point(4, 25);
            this.FavoritesPage.Name = "FavoritesPage";
            this.FavoritesPage.Padding = new System.Windows.Forms.Padding(3);
            this.FavoritesPage.Size = new System.Drawing.Size(491, 278);
            this.FavoritesPage.TabIndex = 2;
            this.FavoritesPage.Text = "Favorites";
            this.FavoritesPage.UseVisualStyleBackColor = true;
            // 
            // FavoriteListViewCB
            // 
            this.FavoriteListViewCB.AutoSize = true;
            this.FavoriteListViewCB.Location = new System.Drawing.Point(142, 10);
            this.FavoriteListViewCB.Name = "FavoriteListViewCB";
            this.FavoriteListViewCB.Size = new System.Drawing.Size(68, 17);
            this.FavoriteListViewCB.TabIndex = 13;
            this.FavoriteListViewCB.Text = "List View";
            this.FavoriteListViewCB.UseVisualStyleBackColor = true;
            this.FavoriteListViewCB.CheckedChanged += new System.EventHandler(this.FavoriteListViewCB_CheckedChanged);
            // 
            // FavoriteGamesPanel
            // 
            this.FavoriteGamesPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FavoriteGamesPanel.AutoScroll = true;
            this.FavoriteGamesPanel.Location = new System.Drawing.Point(3, 31);
            this.FavoriteGamesPanel.Name = "FavoriteGamesPanel";
            this.FavoriteGamesPanel.Size = new System.Drawing.Size(485, 247);
            this.FavoriteGamesPanel.TabIndex = 14;
            // 
            // FavoritesListView
            // 
            this.FavoritesListView.AllColumns.Add(this.GameName);
            this.FavoritesListView.AllColumns.Add(this.ID);
            this.FavoritesListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FavoritesListView.CellEditUseWholeCell = false;
            this.FavoritesListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.GameName});
            this.FavoritesListView.ContextMenuStrip = this.FavoritesStrip;
            this.FavoritesListView.Cursor = System.Windows.Forms.Cursors.Default;
            this.FavoritesListView.FullRowSelect = true;
            this.FavoritesListView.GridLines = true;
            this.FavoritesListView.HideSelection = false;
            this.FavoritesListView.Location = new System.Drawing.Point(3, 31);
            this.FavoritesListView.MultiSelect = false;
            this.FavoritesListView.Name = "FavoritesListView";
            this.FavoritesListView.ShowGroups = false;
            this.FavoritesListView.Size = new System.Drawing.Size(485, 244);
            this.FavoritesListView.TabIndex = 7;
            this.FavoritesListView.UseCompatibleStateImageBehavior = false;
            this.FavoritesListView.View = System.Windows.Forms.View.Details;
            this.FavoritesListView.Visible = false;
            this.FavoritesListView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.FavoritesListView_MouseClick);
            // 
            // GameName
            // 
            this.GameName.AspectName = "Name";
            this.GameName.Text = "Game Name";
            this.GameName.Width = 331;
            // 
            // UniversePage
            // 
            this.UniversePage.Controls.Add(this.ViewUniverse);
            this.UniversePage.Controls.Add(this.uUniverseIdLabel);
            this.UniversePage.Controls.Add(this.UniverseIDTB);
            this.UniversePage.Controls.Add(this.PlaceIDUniTB);
            this.UniversePage.Controls.Add(this.uPlaceIDLabel);
            this.UniversePage.Controls.Add(this.GetUniverseID);
            this.UniversePage.Controls.Add(this.UniverseGamesPanel);
            this.UniversePage.Location = new System.Drawing.Point(4, 25);
            this.UniversePage.Name = "UniversePage";
            this.UniversePage.Size = new System.Drawing.Size(491, 278);
            this.UniversePage.TabIndex = 4;
            this.UniversePage.Text = "Universe";
            this.UniversePage.UseVisualStyleBackColor = true;
            // 
            // ViewUniverse
            // 
            this.ViewUniverse.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ViewUniverse.Location = new System.Drawing.Point(413, 5);
            this.ViewUniverse.Name = "ViewUniverse";
            this.ViewUniverse.Size = new System.Drawing.Size(75, 22);
            this.ViewUniverse.TabIndex = 4;
            this.ViewUniverse.Text = "View Games";
            this.ViewUniverse.UseVisualStyleBackColor = true;
            this.ViewUniverse.Click += new System.EventHandler(this.ViewUniverse_Click);
            // 
            // uUniverseIdLabel
            // 
            this.uUniverseIdLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.uUniverseIdLabel.AutoSize = true;
            this.uUniverseIdLabel.Location = new System.Drawing.Point(253, 10);
            this.uUniverseIdLabel.Name = "uUniverseIdLabel";
            this.uUniverseIdLabel.Size = new System.Drawing.Size(63, 13);
            this.uUniverseIdLabel.TabIndex = 0;
            this.uUniverseIdLabel.Text = "Universe ID";
            // 
            // UniverseIDTB
            // 
            this.UniverseIDTB.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.UniverseIDTB.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(122)))), ((int)(((byte)(122)))));
            this.UniverseIDTB.Location = new System.Drawing.Point(322, 6);
            this.UniverseIDTB.Name = "UniverseIDTB";
            this.UniverseIDTB.Size = new System.Drawing.Size(85, 20);
            this.UniverseIDTB.TabIndex = 3;
            // 
            // PlaceIDUniTB
            // 
            this.PlaceIDUniTB.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(122)))), ((int)(((byte)(122)))));
            this.PlaceIDUniTB.Location = new System.Drawing.Point(64, 6);
            this.PlaceIDUniTB.Name = "PlaceIDUniTB";
            this.PlaceIDUniTB.Size = new System.Drawing.Size(70, 20);
            this.PlaceIDUniTB.TabIndex = 1;
            // 
            // uPlaceIDLabel
            // 
            this.uPlaceIDLabel.AutoSize = true;
            this.uPlaceIDLabel.Location = new System.Drawing.Point(10, 9);
            this.uPlaceIDLabel.Name = "uPlaceIDLabel";
            this.uPlaceIDLabel.Size = new System.Drawing.Size(48, 13);
            this.uPlaceIDLabel.TabIndex = 0;
            this.uPlaceIDLabel.Text = "Place ID";
            // 
            // GetUniverseID
            // 
            this.GetUniverseID.Location = new System.Drawing.Point(140, 5);
            this.GetUniverseID.Name = "GetUniverseID";
            this.GetUniverseID.Size = new System.Drawing.Size(92, 22);
            this.GetUniverseID.TabIndex = 2;
            this.GetUniverseID.Text = "Get Universe ID";
            this.GetUniverseID.UseVisualStyleBackColor = true;
            this.GetUniverseID.Click += new System.EventHandler(this.GetUniverseID_Click);
            // 
            // UniverseGamesPanel
            // 
            this.UniverseGamesPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.UniverseGamesPanel.AutoScroll = true;
            this.UniverseGamesPanel.Location = new System.Drawing.Point(0, 28);
            this.UniverseGamesPanel.Name = "UniverseGamesPanel";
            this.UniverseGamesPanel.Size = new System.Drawing.Size(491, 250);
            this.UniverseGamesPanel.TabIndex = 0;
            // 
            // OutfitsPage
            // 
            this.OutfitsPage.Controls.Add(this.WearCustomButton);
            this.OutfitsPage.Controls.Add(this.OutfitUsernameTB);
            this.OutfitsPage.Controls.Add(this.OutfitUsernameLabel);
            this.OutfitsPage.Controls.Add(this.ViewOutfits);
            this.OutfitsPage.Controls.Add(this.OutfitsPanel);
            this.OutfitsPage.Location = new System.Drawing.Point(4, 25);
            this.OutfitsPage.Name = "OutfitsPage";
            this.OutfitsPage.Size = new System.Drawing.Size(491, 278);
            this.OutfitsPage.TabIndex = 6;
            this.OutfitsPage.Text = "Outfits";
            this.OutfitsPage.UseVisualStyleBackColor = true;
            // 
            // WearCustomButton
            // 
            this.WearCustomButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.WearCustomButton.Location = new System.Drawing.Point(407, 3);
            this.WearCustomButton.Name = "WearCustomButton";
            this.WearCustomButton.Size = new System.Drawing.Size(81, 22);
            this.WearCustomButton.TabIndex = 7;
            this.WearCustomButton.Text = "Wear Custom";
            this.WearCustomButton.UseVisualStyleBackColor = true;
            this.WearCustomButton.Click += new System.EventHandler(this.WearCustomButton_Click);
            // 
            // OutfitUsernameTB
            // 
            this.OutfitUsernameTB.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(122)))), ((int)(((byte)(122)))));
            this.OutfitUsernameTB.Location = new System.Drawing.Point(71, 4);
            this.OutfitUsernameTB.Name = "OutfitUsernameTB";
            this.OutfitUsernameTB.Size = new System.Drawing.Size(101, 20);
            this.OutfitUsernameTB.TabIndex = 5;
            // 
            // OutfitUsernameLabel
            // 
            this.OutfitUsernameLabel.AutoSize = true;
            this.OutfitUsernameLabel.Location = new System.Drawing.Point(10, 7);
            this.OutfitUsernameLabel.Name = "OutfitUsernameLabel";
            this.OutfitUsernameLabel.Size = new System.Drawing.Size(55, 13);
            this.OutfitUsernameLabel.TabIndex = 3;
            this.OutfitUsernameLabel.Text = "Username";
            // 
            // ViewOutfits
            // 
            this.ViewOutfits.Location = new System.Drawing.Point(178, 3);
            this.ViewOutfits.Name = "ViewOutfits";
            this.ViewOutfits.Size = new System.Drawing.Size(81, 22);
            this.ViewOutfits.TabIndex = 6;
            this.ViewOutfits.Text = "View Outfits";
            this.ViewOutfits.UseVisualStyleBackColor = true;
            this.ViewOutfits.Click += new System.EventHandler(this.ViewOutfits_Click);
            // 
            // OutfitsPanel
            // 
            this.OutfitsPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.OutfitsPanel.AutoScroll = true;
            this.OutfitsPanel.Location = new System.Drawing.Point(0, 26);
            this.OutfitsPanel.Name = "OutfitsPanel";
            this.OutfitsPanel.Size = new System.Drawing.Size(491, 250);
            this.OutfitsPanel.TabIndex = 4;
            // 
            // RobloxScan
            // 
            this.RobloxScan.Controls.Add(this.WatcherPanel);
            this.RobloxScan.Location = new System.Drawing.Point(4, 25);
            this.RobloxScan.Name = "RobloxScan";
            this.RobloxScan.Size = new System.Drawing.Size(491, 278);
            this.RobloxScan.TabIndex = 5;
            this.RobloxScan.Text = "Watcher";
            this.RobloxScan.UseVisualStyleBackColor = true;
            // 
            // WatcherPanel
            // 
            this.WatcherPanel.Controls.Add(this.RobloxScannerCB);
            this.WatcherPanel.Controls.Add(this.ScanESLabel);
            this.WatcherPanel.Controls.Add(this.ScanIntervalN);
            this.WatcherPanel.Controls.Add(this.label2);
            this.WatcherPanel.Controls.Add(this.ReadIntervalN);
            this.WatcherPanel.Controls.Add(this.ExitIfBetaDetectedCB);
            this.WatcherPanel.Controls.Add(this.ExitIfNoConnectionCB);
            this.WatcherPanel.Controls.Add(this.TimeoutNum);
            this.WatcherPanel.Controls.Add(this.ConnectionSecondsLabel);
            this.WatcherPanel.Controls.Add(this.SaveWindowPositionsCB);
            this.WatcherPanel.Controls.Add(this.VerifyDataModelCB);
            this.WatcherPanel.Controls.Add(this.IgnoreExistingProcesses);
            this.WatcherPanel.Controls.Add(this.RbxMemoryCB);
            this.WatcherPanel.Controls.Add(this.RbxMemoryLTNum);
            this.WatcherPanel.Controls.Add(this.MBLabel);
            this.WatcherPanel.Controls.Add(this.CloseRbxWindowTitleCB);
            this.WatcherPanel.Controls.Add(this.RbxWindowNameTB);
            this.WatcherPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.WatcherPanel.Location = new System.Drawing.Point(0, 0);
            this.WatcherPanel.Name = "WatcherPanel";
            this.WatcherPanel.Padding = new System.Windows.Forms.Padding(8);
            this.WatcherPanel.Size = new System.Drawing.Size(491, 278);
            this.WatcherPanel.TabIndex = 0;
            // 
            // RobloxScannerCB
            // 
            this.RobloxScannerCB.AutoSize = true;
            this.WatcherPanel.SetFlowBreak(this.RobloxScannerCB, true);
            this.RobloxScannerCB.Location = new System.Drawing.Point(11, 11);
            this.RobloxScannerCB.Name = "RobloxScannerCB";
            this.RobloxScannerCB.Size = new System.Drawing.Size(139, 17);
            this.RobloxScannerCB.TabIndex = 0;
            this.RobloxScannerCB.Text = "Enable Roblox Watcher";
            this.RobloxScannerCB.UseVisualStyleBackColor = true;
            this.RobloxScannerCB.CheckedChanged += new System.EventHandler(this.RobloxScannerCB_CheckedChanged);
            // 
            // ScanESLabel
            // 
            this.ScanESLabel.AutoSize = true;
            this.ScanESLabel.Location = new System.Drawing.Point(11, 35);
            this.ScanESLabel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 0);
            this.ScanESLabel.Name = "ScanESLabel";
            this.ScanESLabel.Size = new System.Drawing.Size(96, 13);
            this.ScanESLabel.TabIndex = 1;
            this.ScanESLabel.Text = "Scan Interval (sec)";
            // 
            // ScanIntervalN
            // 
            this.WatcherPanel.SetFlowBreak(this.ScanIntervalN, true);
            this.ScanIntervalN.Location = new System.Drawing.Point(113, 34);
            this.ScanIntervalN.Maximum = new decimal(new int[] {
            600,
            0,
            0,
            0});
            this.ScanIntervalN.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.ScanIntervalN.Name = "ScanIntervalN";
            this.ScanIntervalN.Size = new System.Drawing.Size(70, 20);
            this.ScanIntervalN.TabIndex = 2;
            this.ScanIntervalN.Value = new decimal(new int[] {
            12,
            0,
            0,
            0});
            this.ScanIntervalN.ValueChanged += new System.EventHandler(this.ScanIntervalN_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 61);
            this.label2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Read Interval (ms)";
            // 
            // ReadIntervalN
            // 
            this.WatcherPanel.SetFlowBreak(this.ReadIntervalN, true);
            this.ReadIntervalN.Location = new System.Drawing.Point(110, 60);
            this.ReadIntervalN.Maximum = new decimal(new int[] {
            3000,
            0,
            0,
            0});
            this.ReadIntervalN.Minimum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.ReadIntervalN.Name = "ReadIntervalN";
            this.ReadIntervalN.Size = new System.Drawing.Size(70, 20);
            this.ReadIntervalN.TabIndex = 5;
            this.ReadIntervalN.Value = new decimal(new int[] {
            250,
            0,
            0,
            0});
            this.ReadIntervalN.ValueChanged += new System.EventHandler(this.ReadIntervalN_ValueChanged);
            // 
            // ExitIfBetaDetectedCB
            // 
            this.ExitIfBetaDetectedCB.AutoSize = true;
            this.WatcherPanel.SetFlowBreak(this.ExitIfBetaDetectedCB, true);
            this.ExitIfBetaDetectedCB.Location = new System.Drawing.Point(11, 86);
            this.ExitIfBetaDetectedCB.Name = "ExitIfBetaDetectedCB";
            this.ExitIfBetaDetectedCB.Size = new System.Drawing.Size(184, 17);
            this.ExitIfBetaDetectedCB.TabIndex = 3;
            this.ExitIfBetaDetectedCB.Text = "Exit if Beta Home Menu Detected";
            this.ExitIfBetaDetectedCB.UseVisualStyleBackColor = true;
            this.ExitIfBetaDetectedCB.CheckedChanged += new System.EventHandler(this.ExitIfBetaDetectedCB_CheckedChanged);
            // 
            // ExitIfNoConnectionCB
            // 
            this.ExitIfNoConnectionCB.AutoSize = true;
            this.ExitIfNoConnectionCB.Location = new System.Drawing.Point(11, 109);
            this.ExitIfNoConnectionCB.Name = "ExitIfNoConnectionCB";
            this.ExitIfNoConnectionCB.Size = new System.Drawing.Size(186, 17);
            this.ExitIfNoConnectionCB.TabIndex = 13;
            this.ExitIfNoConnectionCB.Text = "Exit if No Connection to Server for";
            this.ExitIfNoConnectionCB.UseVisualStyleBackColor = true;
            this.ExitIfNoConnectionCB.CheckedChanged += new System.EventHandler(this.ExitIfNoConnectionCB_CheckedChanged);
            // 
            // TimeoutNum
            // 
            this.TimeoutNum.Location = new System.Drawing.Point(203, 109);
            this.TimeoutNum.Maximum = new decimal(new int[] {
            3600,
            0,
            0,
            0});
            this.TimeoutNum.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.TimeoutNum.Name = "TimeoutNum";
            this.TimeoutNum.Size = new System.Drawing.Size(52, 20);
            this.TimeoutNum.TabIndex = 14;
            this.TimeoutNum.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.TimeoutNum.ValueChanged += new System.EventHandler(this.TimeoutNum_ValueChanged);
            // 
            // ConnectionSecondsLabel
            // 
            this.ConnectionSecondsLabel.AutoSize = true;
            this.WatcherPanel.SetFlowBreak(this.ConnectionSecondsLabel, true);
            this.ConnectionSecondsLabel.Location = new System.Drawing.Point(261, 112);
            this.ConnectionSecondsLabel.Margin = new System.Windows.Forms.Padding(3, 6, 3, 0);
            this.ConnectionSecondsLabel.Name = "ConnectionSecondsLabel";
            this.ConnectionSecondsLabel.Size = new System.Drawing.Size(49, 13);
            this.ConnectionSecondsLabel.TabIndex = 15;
            this.ConnectionSecondsLabel.Text = "Seconds";
            // 
            // IgnoreExistingProcesses
            // 
            this.IgnoreExistingProcesses.AutoSize = true;
            this.WatcherPanel.SetFlowBreak(this.IgnoreExistingProcesses, true);
            this.IgnoreExistingProcesses.Location = new System.Drawing.Point(11, 181);
            this.IgnoreExistingProcesses.Name = "IgnoreExistingProcesses";
            this.IgnoreExistingProcesses.Size = new System.Drawing.Size(218, 17);
            this.IgnoreExistingProcesses.TabIndex = 7;
            this.IgnoreExistingProcesses.Text = "Ignore Existing Processes During Startup";
            this.IgnoreExistingProcesses.UseVisualStyleBackColor = true;
            this.IgnoreExistingProcesses.CheckedChanged += new System.EventHandler(this.IgnoreExistingProcesses_CheckedChanged);
            // 
            // RbxMemoryCB
            // 
            this.RbxMemoryCB.AutoSize = true;
            this.RbxMemoryCB.Location = new System.Drawing.Point(11, 204);
            this.RbxMemoryCB.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.RbxMemoryCB.Name = "RbxMemoryCB";
            this.RbxMemoryCB.Size = new System.Drawing.Size(199, 17);
            this.RbxMemoryCB.TabIndex = 8;
            this.RbxMemoryCB.Text = "Close Roblox if Memory is Less Than";
            this.RbxMemoryCB.UseVisualStyleBackColor = true;
            this.RbxMemoryCB.CheckedChanged += new System.EventHandler(this.RbxMemoryCB_CheckedChanged);
            // 
            // RbxMemoryLTNum
            // 
            this.RbxMemoryLTNum.Location = new System.Drawing.Point(213, 204);
            this.RbxMemoryLTNum.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.RbxMemoryLTNum.Minimum = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.RbxMemoryLTNum.Name = "RbxMemoryLTNum";
            this.RbxMemoryLTNum.Size = new System.Drawing.Size(52, 20);
            this.RbxMemoryLTNum.TabIndex = 9;
            this.RbxMemoryLTNum.Value = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.RbxMemoryLTNum.ValueChanged += new System.EventHandler(this.RbxMemoryLTNum_ValueChanged);
            // 
            // MBLabel
            // 
            this.MBLabel.AutoSize = true;
            this.WatcherPanel.SetFlowBreak(this.MBLabel, true);
            this.MBLabel.Location = new System.Drawing.Point(271, 207);
            this.MBLabel.Margin = new System.Windows.Forms.Padding(3, 6, 3, 0);
            this.MBLabel.Name = "MBLabel";
            this.MBLabel.Size = new System.Drawing.Size(23, 13);
            this.MBLabel.TabIndex = 10;
            this.MBLabel.Text = "MB";
            // 
            // CloseRbxWindowTitleCB
            // 
            this.CloseRbxWindowTitleCB.AutoSize = true;
            this.CloseRbxWindowTitleCB.Location = new System.Drawing.Point(11, 230);
            this.CloseRbxWindowTitleCB.Margin = new System.Windows.Forms.Padding(3, 3, 0, 3);
            this.CloseRbxWindowTitleCB.Name = "CloseRbxWindowTitleCB";
            this.CloseRbxWindowTitleCB.Size = new System.Drawing.Size(180, 17);
            this.CloseRbxWindowTitleCB.TabIndex = 11;
            this.CloseRbxWindowTitleCB.Text = "Close Roblox if WindowTitle Isn\'t";
            this.CloseRbxWindowTitleCB.UseVisualStyleBackColor = true;
            this.CloseRbxWindowTitleCB.CheckedChanged += new System.EventHandler(this.CloseRbxWindowTitleCB_CheckedChanged);
            // 
            // RbxWindowNameTB
            // 
            this.RbxWindowNameTB.AutoCompleteCustomSource.AddRange(new string[] {
            "Roblox"});
            this.RbxWindowNameTB.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.WatcherPanel.SetFlowBreak(this.RbxWindowNameTB, true);
            this.RbxWindowNameTB.Location = new System.Drawing.Point(194, 230);
            this.RbxWindowNameTB.Name = "RbxWindowNameTB";
            this.RbxWindowNameTB.Size = new System.Drawing.Size(52, 20);
            this.RbxWindowNameTB.TabIndex = 12;
            this.RbxWindowNameTB.Text = "Roblox";
            this.RbxWindowNameTB.TextChanged += new System.EventHandler(this.RbxWindowNameTB_TextChanged);
            // 
            // SaveWindowPositionsCB
            // 
            this.SaveWindowPositionsCB.AutoSize = true;
            this.WatcherPanel.SetFlowBreak(this.SaveWindowPositionsCB, true);
            this.SaveWindowPositionsCB.Location = new System.Drawing.Point(11, 135);
            this.SaveWindowPositionsCB.Name = "SaveWindowPositionsCB";
            this.SaveWindowPositionsCB.Size = new System.Drawing.Size(138, 17);
            this.SaveWindowPositionsCB.TabIndex = 16;
            this.SaveWindowPositionsCB.Text = "Save Window Positions";
            this.OPITip.SetToolTip(this.SaveWindowPositionsCB, "Verifies that the closing signal is valid.\r\nThis was added because games can prin" +
        "t anything to roblox\'s logs including the text RAM scans for to detect the beta " +
        "app.");
            this.SaveWindowPositionsCB.UseVisualStyleBackColor = true;
            this.SaveWindowPositionsCB.CheckedChanged += new System.EventHandler(this.SaveWindowPositionsCB_CheckedChanged);
            // 
            // ServerList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(499, 307);
            this.Controls.Add(this.OpenLogsButton);
            this.Controls.Add(this.Tabs);
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(500, 346);
            this.Name = "ServerList";
            this.Text = "Utilities";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ServerList_FormClosing);
            this.Load += new System.EventHandler(this.ServerList_Load);
            this.ServerListStrip.ResumeLayout(false);
            this.GamesStrip.ResumeLayout(false);
            this.FavoritesStrip.ResumeLayout(false);
            this.Tabs.ResumeLayout(false);
            this.ServersTab.ResumeLayout(false);
            this.ServersTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ServerListView)).EndInit();
            this.GamesPage.ResumeLayout(false);
            this.GamesPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PageNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GamesListView)).EndInit();
            this.FavoritesPage.ResumeLayout(false);
            this.FavoritesPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FavoritesListView)).EndInit();
            this.UniversePage.ResumeLayout(false);
            this.UniversePage.PerformLayout();
            this.OutfitsPage.ResumeLayout(false);
            this.OutfitsPage.PerformLayout();
            this.RobloxScan.ResumeLayout(false);
            this.WatcherPanel.ResumeLayout(false);
            this.WatcherPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ScanIntervalN)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ReadIntervalN)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TimeoutNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.RbxMemoryLTNum)).EndInit();
            this.ResumeLayout(false);

        }

#endregion
        private System.Windows.Forms.ColumnHeader ServerID;
        private System.Windows.Forms.ColumnHeader Players;
        private System.Windows.Forms.ColumnHeader Ping;
        private System.Windows.Forms.ColumnHeader FPS;
        private System.Windows.Forms.ContextMenuStrip ServerListStrip;
        private System.Windows.Forms.ToolStripMenuItem joinServerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyJobIdToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip GamesStrip;
        private System.Windows.Forms.ToolStripMenuItem joinGameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addToFavoritesToolStripMenuItem;
        private BrightIdeasSoftware.OLVColumn ID;
        private System.Windows.Forms.ContextMenuStrip FavoritesStrip;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;
        private System.Windows.Forms.TabPage GamesPage;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown PageNum;
        private System.Windows.Forms.Button Search;
        private Classes.BorderedTextBox Term;
        private BrightIdeasSoftware.ObjectListView GamesListView;
        private BrightIdeasSoftware.OLVColumn name;
        private BrightIdeasSoftware.OLVColumn playerCount;
        private BrightIdeasSoftware.OLVColumn likeRatio;
        private System.Windows.Forms.TabPage FavoritesPage;
        private System.Windows.Forms.Button Favorite;
        private BrightIdeasSoftware.ObjectListView FavoritesListView;
        private BrightIdeasSoftware.OLVColumn GameName;
        private System.Windows.Forms.TabPage ServersTab;
        private BrightIdeasSoftware.ObjectListView ServerListView;
        private BrightIdeasSoftware.OLVColumn JobId;
        private BrightIdeasSoftware.OLVColumn Playing;
        private BrightIdeasSoftware.OLVColumn PingColumn;
        private BrightIdeasSoftware.OLVColumn ServerTypeColumn;
        private Classes.BorderedTextBox Username;
        private System.Windows.Forms.Button RefreshServers;
        private System.Windows.Forms.Label UsernameLabel;
        private System.Windows.Forms.Button SearchPlayer;
        private Classes.BorderedTextBox OtherPlaceId;
        private System.Windows.Forms.ToolTip OPITip;
        private System.Windows.Forms.ToolStripMenuItem copyPlaceIDToolStripMenuItem;
        private BrightIdeasSoftware.OLVColumn RegionColumn;
        private System.Windows.Forms.ToolStripMenuItem loadRegionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyPlaceIdToolStripMenuItem1;
        private BrightIdeasSoftware.OLVColumn placeIdCol;
        private BrightIdeasSoftware.OLVColumn universeIdCol;
        private System.Windows.Forms.TabPage UniversePage;
        private System.Windows.Forms.Label uUniverseIdLabel;
        private Classes.BorderedTextBox UniverseIDTB;
        private Classes.BorderedTextBox PlaceIDUniTB;
        private System.Windows.Forms.Label uPlaceIDLabel;
        private System.Windows.Forms.Button GetUniverseID;
        private System.Windows.Forms.FlowLayoutPanel UniverseGamesPanel;
        private System.Windows.Forms.Button ViewUniverse;
        private System.Windows.Forms.CheckBox ListViewCB;
        private System.Windows.Forms.FlowLayoutPanel GameListPanel;
        private System.Windows.Forms.CheckBox FavoriteListViewCB;
        private System.Windows.Forms.FlowLayoutPanel FavoriteGamesPanel;
        private System.Windows.Forms.TabPage RobloxScan;
        private System.Windows.Forms.FlowLayoutPanel WatcherPanel;
        private System.Windows.Forms.CheckBox RobloxScannerCB;
        private NBTabControl Tabs;
        private System.Windows.Forms.Label ScanESLabel;
        private System.Windows.Forms.NumericUpDown ScanIntervalN;
        private System.Windows.Forms.CheckBox ExitIfBetaDetectedCB;
        private System.Windows.Forms.Timer WatcherTimer;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown ReadIntervalN;
        private System.Windows.Forms.TabPage OutfitsPage;
        private BorderedTextBox OutfitUsernameTB;
        private System.Windows.Forms.Label OutfitUsernameLabel;
        private System.Windows.Forms.Button ViewOutfits;
        private System.Windows.Forms.FlowLayoutPanel OutfitsPanel;
        private System.Windows.Forms.Button WearCustomButton;
        private System.Windows.Forms.CheckBox VerifyDataModelCB;
        private System.Windows.Forms.CheckBox IgnoreExistingProcesses;
        private System.Windows.Forms.CheckBox RbxMemoryCB;
        private System.Windows.Forms.NumericUpDown RbxMemoryLTNum;
        private System.Windows.Forms.Label MBLabel;
        private System.Windows.Forms.CheckBox CloseRbxWindowTitleCB;
        private System.Windows.Forms.TextBox RbxWindowNameTB;
        private System.Windows.Forms.CheckBox ExitIfNoConnectionCB;
        private System.Windows.Forms.NumericUpDown TimeoutNum;
        private System.Windows.Forms.Label ConnectionSecondsLabel;
        private System.Windows.Forms.Button OpenLogsButton;
        private System.Windows.Forms.CheckBox SaveWindowPositionsCB;
    }
}
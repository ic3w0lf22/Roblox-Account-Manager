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
            this.GamesPage = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.PageNum = new System.Windows.Forms.NumericUpDown();
            this.Search = new System.Windows.Forms.Button();
            this.Term = new RBX_Alt_Manager.Classes.BorderedTextBox();
            this.GamesListView = new BrightIdeasSoftware.ObjectListView();
            this.name = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.playerCount = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.likeRatio = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.placeIdCol = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.universeIdCol = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.FavoritesPage = new System.Windows.Forms.TabPage();
            this.Favorite = new System.Windows.Forms.Button();
            this.FavoritesListView = new BrightIdeasSoftware.ObjectListView();
            this.GameName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.ServersTab = new System.Windows.Forms.TabPage();
            this.OtherPlaceId = new RBX_Alt_Manager.Classes.BorderedTextBox();
            this.ServerListView = new BrightIdeasSoftware.ObjectListView();
            this.JobId = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.Playing = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.PingColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.ServerTypeColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.RegionColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.Username = new RBX_Alt_Manager.Classes.BorderedTextBox();
            this.RefreshServers = new System.Windows.Forms.Button();
            this.UsernameLabel = new System.Windows.Forms.Label();
            this.SearchPlayer = new System.Windows.Forms.Button();
            this.Tabs = new System.Windows.Forms.TabControl();
            this.OPITip = new System.Windows.Forms.ToolTip(this.components);
            this.ServerListStrip.SuspendLayout();
            this.GamesStrip.SuspendLayout();
            this.FavoritesStrip.SuspendLayout();
            this.GamesPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PageNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GamesListView)).BeginInit();
            this.FavoritesPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FavoritesListView)).BeginInit();
            this.ServersTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ServerListView)).BeginInit();
            this.Tabs.SuspendLayout();
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
            // GamesPage
            // 
            this.GamesPage.Controls.Add(this.label1);
            this.GamesPage.Controls.Add(this.PageNum);
            this.GamesPage.Controls.Add(this.Search);
            this.GamesPage.Controls.Add(this.Term);
            this.GamesPage.Controls.Add(this.GamesListView);
            this.GamesPage.Location = new System.Drawing.Point(4, 22);
            this.GamesPage.Name = "GamesPage";
            this.GamesPage.Padding = new System.Windows.Forms.Padding(3);
            this.GamesPage.Size = new System.Drawing.Size(476, 281);
            this.GamesPage.TabIndex = 3;
            this.GamesPage.Text = "Games";
            this.GamesPage.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(350, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Pg";
            // 
            // PageNum
            // 
            this.PageNum.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.PageNum.Location = new System.Drawing.Point(370, 6);
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
            this.Search.Location = new System.Drawing.Point(416, 5);
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
            this.Term.Size = new System.Drawing.Size(344, 20);
            this.Term.TabIndex = 7;
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
            this.GamesListView.Size = new System.Drawing.Size(470, 247);
            this.GamesListView.TabIndex = 6;
            this.GamesListView.UseCompatibleStateImageBehavior = false;
            this.GamesListView.View = System.Windows.Forms.View.Details;
            this.GamesListView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.GamesListView_MouseClick);
            // 
            // name
            // 
            this.name.AspectName = "name";
            this.name.Text = "Game Name";
            this.name.Width = 310;
            // 
            // playerCount
            // 
            this.playerCount.AspectName = "playerCount";
            this.playerCount.Text = "Players";
            // 
            // likeRatio
            // 
            this.likeRatio.AspectName = "likeRatio";
            this.likeRatio.Text = "Like %";
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
            // FavoritesPage
            // 
            this.FavoritesPage.Controls.Add(this.Favorite);
            this.FavoritesPage.Controls.Add(this.FavoritesListView);
            this.FavoritesPage.Location = new System.Drawing.Point(4, 22);
            this.FavoritesPage.Name = "FavoritesPage";
            this.FavoritesPage.Padding = new System.Windows.Forms.Padding(3);
            this.FavoritesPage.Size = new System.Drawing.Size(476, 281);
            this.FavoritesPage.TabIndex = 2;
            this.FavoritesPage.Text = "Favorites";
            this.FavoritesPage.UseVisualStyleBackColor = true;
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
            this.FavoritesListView.Location = new System.Drawing.Point(3, 34);
            this.FavoritesListView.MultiSelect = false;
            this.FavoritesListView.Name = "FavoritesListView";
            this.FavoritesListView.ShowGroups = false;
            this.FavoritesListView.Size = new System.Drawing.Size(470, 244);
            this.FavoritesListView.TabIndex = 7;
            this.FavoritesListView.UseCompatibleStateImageBehavior = false;
            this.FavoritesListView.View = System.Windows.Forms.View.Details;
            this.FavoritesListView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.FavoritesListView_MouseClick);
            // 
            // GameName
            // 
            this.GameName.AspectName = "Name";
            this.GameName.Text = "Game Name";
            this.GameName.Width = 330;
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
            this.ServersTab.Location = new System.Drawing.Point(4, 22);
            this.ServersTab.Name = "ServersTab";
            this.ServersTab.Padding = new System.Windows.Forms.Padding(3);
            this.ServersTab.Size = new System.Drawing.Size(476, 281);
            this.ServersTab.TabIndex = 0;
            this.ServersTab.Text = "Servers";
            // 
            // OtherPlaceId
            // 
            this.OtherPlaceId.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.OtherPlaceId.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(122)))), ((int)(((byte)(122)))));
            this.OtherPlaceId.Location = new System.Drawing.Point(87, 9);
            this.OtherPlaceId.Name = "OtherPlaceId";
            this.OtherPlaceId.Size = new System.Drawing.Size(111, 20);
            this.OtherPlaceId.TabIndex = 6;
            this.OPITip.SetToolTip(this.OtherPlaceId, resources.GetString("OtherPlaceId.ToolTip"));
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
            this.ServerListView.Size = new System.Drawing.Size(456, 236);
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
            // ServerTypeColumn
            // 
            this.ServerTypeColumn.AspectName = "type";
            this.ServerTypeColumn.DisplayIndex = 3;
            this.ServerTypeColumn.IsVisible = false;
            this.ServerTypeColumn.Text = "Type";
            this.ServerTypeColumn.Width = 45;
            // 
            // RegionColumn
            // 
            this.RegionColumn.AspectName = "region";
            this.RegionColumn.Text = "Region";
            this.RegionColumn.Width = 113;
            // 
            // Username
            // 
            this.Username.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Username.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(122)))), ((int)(((byte)(122)))), ((int)(((byte)(122)))));
            this.Username.Location = new System.Drawing.Point(265, 9);
            this.Username.Name = "Username";
            this.Username.Size = new System.Drawing.Size(117, 20);
            this.Username.TabIndex = 1;
            this.OPITip.SetToolTip(this.Username, "The Roblox Username of who you are searching for (PlaceId must be correct or it m" +
        "ay not find anyone)");
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
            this.UsernameLabel.Location = new System.Drawing.Point(204, 13);
            this.UsernameLabel.Name = "UsernameLabel";
            this.UsernameLabel.Size = new System.Drawing.Size(55, 13);
            this.UsernameLabel.TabIndex = 3;
            this.UsernameLabel.Text = "Username";
            // 
            // SearchPlayer
            // 
            this.SearchPlayer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SearchPlayer.Location = new System.Drawing.Point(388, 8);
            this.SearchPlayer.Name = "SearchPlayer";
            this.SearchPlayer.Size = new System.Drawing.Size(75, 21);
            this.SearchPlayer.TabIndex = 2;
            this.SearchPlayer.Text = "Search";
            this.SearchPlayer.UseVisualStyleBackColor = true;
            this.SearchPlayer.Click += new System.EventHandler(this.SearchPlayer_Click);
            // 
            // Tabs
            // 
            this.Tabs.Controls.Add(this.ServersTab);
            this.Tabs.Controls.Add(this.GamesPage);
            this.Tabs.Controls.Add(this.FavoritesPage);
            this.Tabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Tabs.Location = new System.Drawing.Point(0, 0);
            this.Tabs.Name = "Tabs";
            this.Tabs.SelectedIndex = 0;
            this.Tabs.Size = new System.Drawing.Size(484, 307);
            this.Tabs.TabIndex = 6;
            // 
            // ServerList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 307);
            this.Controls.Add(this.Tabs);
            this.HelpButton = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(500, 346);
            this.Name = "ServerList";
            this.Text = "Server List";
            this.HelpButtonClicked += new System.ComponentModel.CancelEventHandler(this.ServerList_HelpButtonClicked);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ServerList_FormClosing);
            this.Load += new System.EventHandler(this.ServerList_Load);
            this.ServerListStrip.ResumeLayout(false);
            this.GamesStrip.ResumeLayout(false);
            this.FavoritesStrip.ResumeLayout(false);
            this.GamesPage.ResumeLayout(false);
            this.GamesPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PageNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GamesListView)).EndInit();
            this.FavoritesPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.FavoritesListView)).EndInit();
            this.ServersTab.ResumeLayout(false);
            this.ServersTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ServerListView)).EndInit();
            this.Tabs.ResumeLayout(false);
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
        private System.Windows.Forms.TabControl Tabs;
        private Classes.BorderedTextBox OtherPlaceId;
        private System.Windows.Forms.ToolTip OPITip;
        private System.Windows.Forms.ToolStripMenuItem copyPlaceIDToolStripMenuItem;
        private BrightIdeasSoftware.OLVColumn RegionColumn;
        private System.Windows.Forms.ToolStripMenuItem loadRegionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyPlaceIdToolStripMenuItem1;
        private BrightIdeasSoftware.OLVColumn placeIdCol;
        private BrightIdeasSoftware.OLVColumn universeIdCol;
    }
}
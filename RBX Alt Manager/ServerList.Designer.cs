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
            this.RefreshServers = new System.Windows.Forms.Button();
            this.SearchPlayer = new System.Windows.Forms.Button();
            this.Username = new System.Windows.Forms.TextBox();
            this.UsernameLabel = new System.Windows.Forms.Label();
            this.ServerID = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Players = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Ping = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.FPS = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ServerListStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.joinServerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyJobIdToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyJoinLinkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ServerListView = new BrightIdeasSoftware.ObjectListView();
            this.JobId = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.Playing = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.PingColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.ServerTypeColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.Tabs = new System.Windows.Forms.TabControl();
            this.ServersTab = new System.Windows.Forms.TabPage();
            this.GamesTab = new System.Windows.Forms.TabPage();
            this.GamesControl = new System.Windows.Forms.TabControl();
            this.ListPage = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.PageNum = new System.Windows.Forms.NumericUpDown();
            this.Search = new System.Windows.Forms.Button();
            this.Term = new System.Windows.Forms.TextBox();
            this.GamesListView = new BrightIdeasSoftware.ObjectListView();
            this.name = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.playerCount = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.likeRatio = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.GamesStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.joinGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addToFavoritesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.FavoritesPage = new System.Windows.Forms.TabPage();
            this.FavoritesListView = new BrightIdeasSoftware.ObjectListView();
            this.GameName = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.ID = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.FavoritesStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ServerListStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ServerListView)).BeginInit();
            this.Tabs.SuspendLayout();
            this.ServersTab.SuspendLayout();
            this.GamesTab.SuspendLayout();
            this.GamesControl.SuspendLayout();
            this.ListPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PageNum)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GamesListView)).BeginInit();
            this.GamesStrip.SuspendLayout();
            this.FavoritesPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.FavoritesListView)).BeginInit();
            this.FavoritesStrip.SuspendLayout();
            this.SuspendLayout();
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
            // Username
            // 
            this.Username.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Username.Location = new System.Drawing.Point(220, 9);
            this.Username.Name = "Username";
            this.Username.Size = new System.Drawing.Size(162, 20);
            this.Username.TabIndex = 1;
            // 
            // UsernameLabel
            // 
            this.UsernameLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.UsernameLabel.AutoSize = true;
            this.UsernameLabel.Location = new System.Drawing.Point(159, 12);
            this.UsernameLabel.Name = "UsernameLabel";
            this.UsernameLabel.Size = new System.Drawing.Size(55, 13);
            this.UsernameLabel.TabIndex = 3;
            this.UsernameLabel.Text = "Username";
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
            this.copyJoinLinkToolStripMenuItem});
            this.ServerListStrip.Name = "ServerListStrip";
            this.ServerListStrip.Size = new System.Drawing.Size(152, 70);
            // 
            // joinServerToolStripMenuItem
            // 
            this.joinServerToolStripMenuItem.Name = "joinServerToolStripMenuItem";
            this.joinServerToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.joinServerToolStripMenuItem.Text = "Join Server";
            this.joinServerToolStripMenuItem.Click += new System.EventHandler(this.joinServerToolStripMenuItem_Click);
            // 
            // copyJobIdToolStripMenuItem
            // 
            this.copyJobIdToolStripMenuItem.Name = "copyJobIdToolStripMenuItem";
            this.copyJobIdToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.copyJobIdToolStripMenuItem.Text = "Copy JobId";
            this.copyJobIdToolStripMenuItem.Click += new System.EventHandler(this.copyJobIdToolStripMenuItem_Click);
            // 
            // copyJoinLinkToolStripMenuItem
            // 
            this.copyJoinLinkToolStripMenuItem.Name = "copyJoinLinkToolStripMenuItem";
            this.copyJoinLinkToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.copyJoinLinkToolStripMenuItem.Text = "Copy Join Link";
            this.copyJoinLinkToolStripMenuItem.Click += new System.EventHandler(this.copyJoinLinkToolStripMenuItem_Click);
            // 
            // ServerListView
            // 
            this.ServerListView.AllColumns.Add(this.JobId);
            this.ServerListView.AllColumns.Add(this.Playing);
            this.ServerListView.AllColumns.Add(this.PingColumn);
            this.ServerListView.AllColumns.Add(this.ServerTypeColumn);
            this.ServerListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ServerListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.JobId,
            this.Playing,
            this.PingColumn,
            this.ServerTypeColumn});
            this.ServerListView.ContextMenuStrip = this.ServerListStrip;
            this.ServerListView.Cursor = System.Windows.Forms.Cursors.Default;
            this.ServerListView.GridLines = true;
            this.ServerListView.HideSelection = false;
            this.ServerListView.Location = new System.Drawing.Point(7, 37);
            this.ServerListView.MultiSelect = false;
            this.ServerListView.Name = "ServerListView";
            this.ServerListView.ShowGroups = false;
            this.ServerListView.Size = new System.Drawing.Size(455, 236);
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
            // 
            // PingColumn
            // 
            this.PingColumn.AspectName = "ping";
            this.PingColumn.Text = "Ping";
            // 
            // ServerTypeColumn
            // 
            this.ServerTypeColumn.AspectName = "type";
            this.ServerTypeColumn.Text = "Type";
            this.ServerTypeColumn.Width = 45;
            // 
            // Tabs
            // 
            this.Tabs.Controls.Add(this.ServersTab);
            this.Tabs.Controls.Add(this.GamesTab);
            this.Tabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Tabs.Location = new System.Drawing.Point(0, 0);
            this.Tabs.Name = "Tabs";
            this.Tabs.SelectedIndex = 0;
            this.Tabs.Size = new System.Drawing.Size(484, 307);
            this.Tabs.TabIndex = 6;
            // 
            // ServersTab
            // 
            this.ServersTab.Controls.Add(this.ServerListView);
            this.ServersTab.Controls.Add(this.RefreshServers);
            this.ServersTab.Controls.Add(this.Username);
            this.ServersTab.Controls.Add(this.UsernameLabel);
            this.ServersTab.Controls.Add(this.SearchPlayer);
            this.ServersTab.Location = new System.Drawing.Point(4, 22);
            this.ServersTab.Name = "ServersTab";
            this.ServersTab.Padding = new System.Windows.Forms.Padding(3);
            this.ServersTab.Size = new System.Drawing.Size(476, 281);
            this.ServersTab.TabIndex = 0;
            this.ServersTab.Text = "Servers";
            this.ServersTab.UseVisualStyleBackColor = true;
            // 
            // GamesTab
            // 
            this.GamesTab.Controls.Add(this.GamesControl);
            this.GamesTab.Location = new System.Drawing.Point(4, 22);
            this.GamesTab.Name = "GamesTab";
            this.GamesTab.Padding = new System.Windows.Forms.Padding(3);
            this.GamesTab.Size = new System.Drawing.Size(476, 281);
            this.GamesTab.TabIndex = 1;
            this.GamesTab.Text = "Games";
            this.GamesTab.UseVisualStyleBackColor = true;
            // 
            // GamesControl
            // 
            this.GamesControl.Controls.Add(this.ListPage);
            this.GamesControl.Controls.Add(this.FavoritesPage);
            this.GamesControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GamesControl.Location = new System.Drawing.Point(3, 3);
            this.GamesControl.Name = "GamesControl";
            this.GamesControl.SelectedIndex = 0;
            this.GamesControl.Size = new System.Drawing.Size(470, 275);
            this.GamesControl.TabIndex = 7;
            // 
            // ListPage
            // 
            this.ListPage.Controls.Add(this.label1);
            this.ListPage.Controls.Add(this.PageNum);
            this.ListPage.Controls.Add(this.Search);
            this.ListPage.Controls.Add(this.Term);
            this.ListPage.Controls.Add(this.GamesListView);
            this.ListPage.Location = new System.Drawing.Point(4, 22);
            this.ListPage.Name = "ListPage";
            this.ListPage.Padding = new System.Windows.Forms.Padding(3);
            this.ListPage.Size = new System.Drawing.Size(462, 249);
            this.ListPage.TabIndex = 0;
            this.ListPage.Text = "List";
            this.ListPage.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(336, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(20, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Pg";
            // 
            // PageNum
            // 
            this.PageNum.Location = new System.Drawing.Point(356, 6);
            this.PageNum.Name = "PageNum";
            this.PageNum.Size = new System.Drawing.Size(40, 20);
            this.PageNum.TabIndex = 9;
            // 
            // Search
            // 
            this.Search.Location = new System.Drawing.Point(402, 5);
            this.Search.Name = "Search";
            this.Search.Size = new System.Drawing.Size(54, 22);
            this.Search.TabIndex = 8;
            this.Search.Text = "Search";
            this.Search.UseVisualStyleBackColor = true;
            this.Search.Click += new System.EventHandler(this.Search_Click);
            // 
            // Term
            // 
            this.Term.Location = new System.Drawing.Point(6, 6);
            this.Term.Name = "Term";
            this.Term.Size = new System.Drawing.Size(330, 20);
            this.Term.TabIndex = 7;
            // 
            // GamesListView
            // 
            this.GamesListView.AllColumns.Add(this.name);
            this.GamesListView.AllColumns.Add(this.playerCount);
            this.GamesListView.AllColumns.Add(this.likeRatio);
            this.GamesListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
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
            this.GamesListView.MultiSelect = false;
            this.GamesListView.Name = "GamesListView";
            this.GamesListView.SelectAllOnControlA = false;
            this.GamesListView.ShowGroups = false;
            this.GamesListView.Size = new System.Drawing.Size(456, 215);
            this.GamesListView.TabIndex = 6;
            this.GamesListView.UseCompatibleStateImageBehavior = false;
            this.GamesListView.View = System.Windows.Forms.View.Details;
            this.GamesListView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.GamesListView_MouseClick);
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
            // GamesStrip
            // 
            this.GamesStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.joinGameToolStripMenuItem,
            this.addToFavoritesToolStripMenuItem});
            this.GamesStrip.Name = "GamesStrip";
            this.GamesStrip.Size = new System.Drawing.Size(130, 48);
            // 
            // joinGameToolStripMenuItem
            // 
            this.joinGameToolStripMenuItem.Name = "joinGameToolStripMenuItem";
            this.joinGameToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.joinGameToolStripMenuItem.Text = "Join Game";
            this.joinGameToolStripMenuItem.Click += new System.EventHandler(this.joinGameToolStripMenuItem_Click);
            // 
            // addToFavoritesToolStripMenuItem
            // 
            this.addToFavoritesToolStripMenuItem.Name = "addToFavoritesToolStripMenuItem";
            this.addToFavoritesToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.addToFavoritesToolStripMenuItem.Text = "Favorite";
            this.addToFavoritesToolStripMenuItem.Click += new System.EventHandler(this.addToFavoritesToolStripMenuItem_Click);
            // 
            // FavoritesPage
            // 
            this.FavoritesPage.Controls.Add(this.FavoritesListView);
            this.FavoritesPage.Location = new System.Drawing.Point(4, 22);
            this.FavoritesPage.Name = "FavoritesPage";
            this.FavoritesPage.Padding = new System.Windows.Forms.Padding(3);
            this.FavoritesPage.Size = new System.Drawing.Size(462, 249);
            this.FavoritesPage.TabIndex = 1;
            this.FavoritesPage.Text = "Favorites";
            this.FavoritesPage.UseVisualStyleBackColor = true;
            // 
            // FavoritesListView
            // 
            this.FavoritesListView.AllColumns.Add(this.GameName);
            this.FavoritesListView.AllColumns.Add(this.ID);
            this.FavoritesListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.GameName});
            this.FavoritesListView.ContextMenuStrip = this.FavoritesStrip;
            this.FavoritesListView.Cursor = System.Windows.Forms.Cursors.Default;
            this.FavoritesListView.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.FavoritesListView.FullRowSelect = true;
            this.FavoritesListView.GridLines = true;
            this.FavoritesListView.HideSelection = false;
            this.FavoritesListView.Location = new System.Drawing.Point(3, 6);
            this.FavoritesListView.MultiSelect = false;
            this.FavoritesListView.Name = "FavoritesListView";
            this.FavoritesListView.ShowGroups = false;
            this.FavoritesListView.Size = new System.Drawing.Size(456, 240);
            this.FavoritesListView.TabIndex = 7;
            this.FavoritesListView.UseCompatibleStateImageBehavior = false;
            this.FavoritesListView.View = System.Windows.Forms.View.Details;
            this.FavoritesListView.MouseClick += new System.Windows.Forms.MouseEventHandler(this.FavoritesListView_MouseClick);
            // 
            // GameName
            // 
            this.GameName.AspectName = "Name";
            this.GameName.Text = "Game Name";
            this.GameName.Width = 330;
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
            this.removeToolStripMenuItem});
            this.FavoritesStrip.Name = "GamesStrip";
            this.FavoritesStrip.Size = new System.Drawing.Size(130, 70);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(129, 22);
            this.toolStripMenuItem1.Text = "Join Game";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(129, 22);
            this.toolStripMenuItem2.Text = "Rename";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // removeToolStripMenuItem
            // 
            this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            this.removeToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.removeToolStripMenuItem.Text = "Remove";
            this.removeToolStripMenuItem.Click += new System.EventHandler(this.removeToolStripMenuItem_Click);
            // 
            // ServerList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(484, 307);
            this.Controls.Add(this.Tabs);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(500, 346);
            this.Name = "ServerList";
            this.Text = "Server List";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ServerList_FormClosing);
            this.Load += new System.EventHandler(this.ServerList_Load);
            this.ServerListStrip.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ServerListView)).EndInit();
            this.Tabs.ResumeLayout(false);
            this.ServersTab.ResumeLayout(false);
            this.ServersTab.PerformLayout();
            this.GamesTab.ResumeLayout(false);
            this.GamesControl.ResumeLayout(false);
            this.ListPage.ResumeLayout(false);
            this.ListPage.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PageNum)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GamesListView)).EndInit();
            this.GamesStrip.ResumeLayout(false);
            this.FavoritesPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.FavoritesListView)).EndInit();
            this.FavoritesStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button RefreshServers;
        private System.Windows.Forms.Button SearchPlayer;
        private System.Windows.Forms.TextBox Username;
        private System.Windows.Forms.Label UsernameLabel;
        private System.Windows.Forms.ColumnHeader ServerID;
        private System.Windows.Forms.ColumnHeader Players;
        private System.Windows.Forms.ColumnHeader Ping;
        private System.Windows.Forms.ColumnHeader FPS;
        private System.Windows.Forms.ContextMenuStrip ServerListStrip;
        private System.Windows.Forms.ToolStripMenuItem joinServerToolStripMenuItem;
        private BrightIdeasSoftware.ObjectListView ServerListView;
        private BrightIdeasSoftware.OLVColumn JobId;
        private BrightIdeasSoftware.OLVColumn Playing;
        private BrightIdeasSoftware.OLVColumn PingColumn;
        private System.Windows.Forms.ToolStripMenuItem copyJobIdToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyJoinLinkToolStripMenuItem;
        private BrightIdeasSoftware.OLVColumn ServerTypeColumn;
        private System.Windows.Forms.TabControl Tabs;
        private System.Windows.Forms.TabPage ServersTab;
        private System.Windows.Forms.TabPage GamesTab;
        private BrightIdeasSoftware.ObjectListView GamesListView;
        private BrightIdeasSoftware.OLVColumn name;
        private BrightIdeasSoftware.OLVColumn playerCount;
        private BrightIdeasSoftware.OLVColumn likeRatio;
        private System.Windows.Forms.TabControl GamesControl;
        private System.Windows.Forms.TabPage ListPage;
        private System.Windows.Forms.Button Search;
        private System.Windows.Forms.TextBox Term;
        private System.Windows.Forms.ContextMenuStrip GamesStrip;
        private System.Windows.Forms.ToolStripMenuItem joinGameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addToFavoritesToolStripMenuItem;
        private System.Windows.Forms.TabPage FavoritesPage;
        private BrightIdeasSoftware.ObjectListView FavoritesListView;
        private BrightIdeasSoftware.OLVColumn GameName;
        private BrightIdeasSoftware.OLVColumn ID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown PageNum;
        private System.Windows.Forms.ContextMenuStrip FavoritesStrip;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;
    }
}
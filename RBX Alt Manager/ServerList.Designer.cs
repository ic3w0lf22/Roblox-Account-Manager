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
            this.FPSColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.ServerTypeColumn = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.Tabs = new System.Windows.Forms.TabControl();
            this.ServersTab = new System.Windows.Forms.TabPage();
            this.GamesTab = new System.Windows.Forms.TabPage();
            this.ServerListStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ServerListView)).BeginInit();
            this.Tabs.SuspendLayout();
            this.ServersTab.SuspendLayout();
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
            this.ServerListView.AllColumns.Add(this.FPSColumn);
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
            // FPSColumn
            // 
            this.FPSColumn.AspectName = "fps";
            this.FPSColumn.DisplayIndex = 3;
            this.FPSColumn.IsVisible = false;
            this.FPSColumn.Text = "FPS";
            this.FPSColumn.Width = 61;
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
            this.GamesTab.Location = new System.Drawing.Point(4, 22);
            this.GamesTab.Name = "GamesTab";
            this.GamesTab.Padding = new System.Windows.Forms.Padding(3);
            this.GamesTab.Size = new System.Drawing.Size(476, 281);
            this.GamesTab.TabIndex = 1;
            this.GamesTab.Text = "Games";
            this.GamesTab.UseVisualStyleBackColor = true;
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
        private BrightIdeasSoftware.OLVColumn FPSColumn;
        private System.Windows.Forms.ToolStripMenuItem copyJobIdToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyJoinLinkToolStripMenuItem;
        private BrightIdeasSoftware.OLVColumn ServerTypeColumn;
        private System.Windows.Forms.TabControl Tabs;
        private System.Windows.Forms.TabPage ServersTab;
        private System.Windows.Forms.TabPage GamesTab;
    }
}
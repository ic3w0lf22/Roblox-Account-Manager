namespace RBX_Alt_Manager.Forms
{
    partial class RecentGamesForm
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
            this.GamesPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // GamesPanel
            // 
            this.GamesPanel.AutoSize = true;
            this.GamesPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.GamesPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.GamesPanel.Location = new System.Drawing.Point(0, 0);
            this.GamesPanel.MaximumSize = new System.Drawing.Size(650, 5000);
            this.GamesPanel.Name = "GamesPanel";
            this.GamesPanel.Size = new System.Drawing.Size(200, 200);
            this.GamesPanel.TabIndex = 0;
            this.GamesPanel.MouseLeave += new System.EventHandler(this.GamesPanel_MouseLeave);
            // 
            // RecentGamesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(200, 200);
            this.Controls.Add(this.GamesPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "RecentGamesForm";
            this.ShowInTaskbar = false;
            this.Text = "RecentGamesForm";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RecentGamesForm_FormClosing);
            this.Shown += new System.EventHandler(this.RecentGamesForm_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel GamesPanel;
    }
}
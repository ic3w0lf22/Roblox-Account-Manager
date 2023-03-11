namespace RBX_Alt_Manager.Classes
{
    partial class GameControl
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.GameImage = new System.Windows.Forms.PictureBox();
            this.GameContextStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyPlaceLinkToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyPlaceIdToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyNameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyPlaceDetailsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.GameName = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.GameImage)).BeginInit();
            this.GameContextStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // GameImage
            // 
            this.GameImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.GameImage.ContextMenuStrip = this.GameContextStrip;
            this.GameImage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.GameImage.Location = new System.Drawing.Point(4, 4);
            this.GameImage.Name = "GameImage";
            this.GameImage.Size = new System.Drawing.Size(100, 100);
            this.GameImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.GameImage.TabIndex = 0;
            this.GameImage.TabStop = false;
            this.GameImage.MouseClick += new System.Windows.Forms.MouseEventHandler(this.MouseClicked);
            // 
            // GameContextStrip
            // 
            this.GameContextStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyPlaceLinkToolStripMenuItem,
            this.copyPlaceIdToolStripMenuItem,
            this.copyNameToolStripMenuItem,
            this.copyPlaceDetailsToolStripMenuItem});
            this.GameContextStrip.Name = "GameContextStrip";
            this.GameContextStrip.Size = new System.Drawing.Size(172, 92);
            // 
            // copyPlaceLinkToolStripMenuItem
            // 
            this.copyPlaceLinkToolStripMenuItem.Name = "copyPlaceLinkToolStripMenuItem";
            this.copyPlaceLinkToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.copyPlaceLinkToolStripMenuItem.Text = "Copy Place Link";
            this.copyPlaceLinkToolStripMenuItem.Click += new System.EventHandler(this.copyPlaceLinkToolStripMenuItem_Click);
            // 
            // copyPlaceIdToolStripMenuItem
            // 
            this.copyPlaceIdToolStripMenuItem.Name = "copyPlaceIdToolStripMenuItem";
            this.copyPlaceIdToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.copyPlaceIdToolStripMenuItem.Text = "Copy PlaceId";
            this.copyPlaceIdToolStripMenuItem.Click += new System.EventHandler(this.copyPlaceIdToolStripMenuItem_Click);
            // 
            // copyNameToolStripMenuItem
            // 
            this.copyNameToolStripMenuItem.Name = "copyNameToolStripMenuItem";
            this.copyNameToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.copyNameToolStripMenuItem.Text = "Copy Name";
            this.copyNameToolStripMenuItem.Click += new System.EventHandler(this.copyNameToolStripMenuItem_Click);
            // 
            // copyPlaceDetailsToolStripMenuItem
            // 
            this.copyPlaceDetailsToolStripMenuItem.Name = "copyPlaceDetailsToolStripMenuItem";
            this.copyPlaceDetailsToolStripMenuItem.Size = new System.Drawing.Size(171, 22);
            this.copyPlaceDetailsToolStripMenuItem.Text = "Copy Place Details";
            this.copyPlaceDetailsToolStripMenuItem.Click += new System.EventHandler(this.copyPlaceDetailsToolStripMenuItem_Click);
            // 
            // GameName
            // 
            this.GameName.AutoSize = true;
            this.GameName.ContextMenuStrip = this.GameContextStrip;
            this.GameName.Cursor = System.Windows.Forms.Cursors.Hand;
            this.GameName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.GameName.Location = new System.Drawing.Point(5, 107);
            this.GameName.MaximumSize = new System.Drawing.Size(100, 0);
            this.GameName.MinimumSize = new System.Drawing.Size(100, 40);
            this.GameName.Name = "GameName";
            this.GameName.Size = new System.Drawing.Size(100, 40);
            this.GameName.TabIndex = 2;
            this.GameName.Text = "...";
            this.GameName.MouseClick += new System.Windows.Forms.MouseEventHandler(this.MouseClicked);
            // 
            // GameControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.GameImage);
            this.Controls.Add(this.GameName);
            this.Name = "GameControl";
            this.Size = new System.Drawing.Size(108, 154);
            ((System.ComponentModel.ISupportInitialize)(this.GameImage)).EndInit();
            this.GameContextStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox GameImage;
        private System.Windows.Forms.Label GameName;
        private System.Windows.Forms.ContextMenuStrip GameContextStrip;
        private System.Windows.Forms.ToolStripMenuItem copyPlaceIdToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyNameToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyPlaceDetailsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyPlaceLinkToolStripMenuItem;
    }
}

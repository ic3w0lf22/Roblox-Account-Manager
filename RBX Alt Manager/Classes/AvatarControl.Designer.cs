namespace RBX_Alt_Manager.Classes
{
    partial class AvatarControl
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
            this.AvatarImage = new System.Windows.Forms.PictureBox();
            this.AvatarContextStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.wearAvatarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyAvatarJSONToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.AvatarName = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.AvatarImage)).BeginInit();
            this.AvatarContextStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // AvatarImage
            // 
            this.AvatarImage.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.AvatarImage.ContextMenuStrip = this.AvatarContextStrip;
            this.AvatarImage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.AvatarImage.Location = new System.Drawing.Point(4, 6);
            this.AvatarImage.Name = "AvatarImage";
            this.AvatarImage.Size = new System.Drawing.Size(100, 100);
            this.AvatarImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.AvatarImage.TabIndex = 3;
            this.AvatarImage.TabStop = false;
            // 
            // AvatarContextStrip
            // 
            this.AvatarContextStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.wearAvatarToolStripMenuItem,
            this.copyAvatarJSONToolStripMenuItem});
            this.AvatarContextStrip.Name = "GameContextStrip";
            this.AvatarContextStrip.Size = new System.Drawing.Size(171, 48);
            // 
            // wearAvatarToolStripMenuItem
            // 
            this.wearAvatarToolStripMenuItem.Name = "wearAvatarToolStripMenuItem";
            this.wearAvatarToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.wearAvatarToolStripMenuItem.Text = "Wear Avatar";
            this.wearAvatarToolStripMenuItem.Click += new System.EventHandler(this.wearAvatarToolStripMenuItem_Click);
            // 
            // copyAvatarJSONToolStripMenuItem
            // 
            this.copyAvatarJSONToolStripMenuItem.Name = "copyAvatarJSONToolStripMenuItem";
            this.copyAvatarJSONToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.copyAvatarJSONToolStripMenuItem.Text = "Copy Avatar JSON";
            this.copyAvatarJSONToolStripMenuItem.Click += new System.EventHandler(this.copyAvatarJSONToolStripMenuItem_Click);
            // 
            // AvatarName
            // 
            this.AvatarName.AutoSize = true;
            this.AvatarName.Cursor = System.Windows.Forms.Cursors.Hand;
            this.AvatarName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AvatarName.Location = new System.Drawing.Point(5, 109);
            this.AvatarName.MaximumSize = new System.Drawing.Size(100, 0);
            this.AvatarName.MinimumSize = new System.Drawing.Size(100, 40);
            this.AvatarName.Name = "AvatarName";
            this.AvatarName.Size = new System.Drawing.Size(100, 40);
            this.AvatarName.TabIndex = 4;
            this.AvatarName.Text = "...";
            // 
            // AvatarControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.AvatarImage);
            this.Controls.Add(this.AvatarName);
            this.Name = "AvatarControl";
            this.Size = new System.Drawing.Size(108, 154);
            this.Load += new System.EventHandler(this.AvatarControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.AvatarImage)).EndInit();
            this.AvatarContextStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox AvatarImage;
        private System.Windows.Forms.ContextMenuStrip AvatarContextStrip;
        private System.Windows.Forms.Label AvatarName;
        private System.Windows.Forms.ToolStripMenuItem copyAvatarJSONToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem wearAvatarToolStripMenuItem;
    }
}

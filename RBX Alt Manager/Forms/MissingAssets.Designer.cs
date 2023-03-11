namespace RBX_Alt_Manager.Forms
{
    partial class MissingAssets
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
            this.AssetPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // AssetPanel
            // 
            this.AssetPanel.AutoScroll = true;
            this.AssetPanel.BackColor = System.Drawing.Color.Transparent;
            this.AssetPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.AssetPanel.Location = new System.Drawing.Point(0, 0);
            this.AssetPanel.Margin = new System.Windows.Forms.Padding(40);
            this.AssetPanel.Name = "AssetPanel";
            this.AssetPanel.Size = new System.Drawing.Size(784, 511);
            this.AssetPanel.TabIndex = 0;
            // 
            // MissingAssets
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 511);
            this.Controls.Add(this.AssetPanel);
            this.Name = "MissingAssets";
            this.ShowIcon = false;
            this.Text = "MissingAssets";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel AssetPanel;
    }
}
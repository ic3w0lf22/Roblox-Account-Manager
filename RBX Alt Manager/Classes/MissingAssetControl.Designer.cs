namespace RBX_Alt_Manager.Classes
{
    partial class MissingAssetControl
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
            this.AssetNameLabel = new System.Windows.Forms.LinkLabel();
            this.BuyButton = new System.Windows.Forms.Button();
            this.PriceLabel = new System.Windows.Forms.Label();
            this.AssetImage = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.AssetImage)).BeginInit();
            this.SuspendLayout();
            // 
            // AssetNameLabel
            // 
            this.AssetNameLabel.AutoSize = true;
            this.AssetNameLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AssetNameLabel.Location = new System.Drawing.Point(59, 14);
            this.AssetNameLabel.Name = "AssetNameLabel";
            this.AssetNameLabel.Size = new System.Drawing.Size(124, 31);
            this.AssetNameLabel.TabIndex = 1;
            this.AssetNameLabel.TabStop = true;
            this.AssetNameLabel.Tag = "UseControlFont";
            this.AssetNameLabel.Text = "<Name>";
            // 
            // BuyButton
            // 
            this.BuyButton.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.BuyButton.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BuyButton.Location = new System.Drawing.Point(353, 14);
            this.BuyButton.Name = "BuyButton";
            this.BuyButton.Size = new System.Drawing.Size(65, 31);
            this.BuyButton.TabIndex = 2;
            this.BuyButton.Tag = "UseControlFont";
            this.BuyButton.Text = "Buy";
            this.BuyButton.UseVisualStyleBackColor = true;
            this.BuyButton.Click += new System.EventHandler(this.BuyButton_Click);
            // 
            // PriceLabel
            // 
            this.PriceLabel.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.PriceLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.PriceLabel.Location = new System.Drawing.Point(194, 19);
            this.PriceLabel.Name = "PriceLabel";
            this.PriceLabel.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.PriceLabel.Size = new System.Drawing.Size(159, 20);
            this.PriceLabel.TabIndex = 1;
            this.PriceLabel.Tag = "UseControlFont";
            this.PriceLabel.Text = "<Price>";
            // 
            // AssetImage
            // 
            this.AssetImage.BackColor = System.Drawing.Color.Transparent;
            this.AssetImage.Location = new System.Drawing.Point(3, 3);
            this.AssetImage.Name = "AssetImage";
            this.AssetImage.Size = new System.Drawing.Size(54, 54);
            this.AssetImage.TabIndex = 0;
            this.AssetImage.TabStop = false;
            // 
            // MissingAssetControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.AssetNameLabel);
            this.Controls.Add(this.BuyButton);
            this.Controls.Add(this.AssetImage);
            this.Controls.Add(this.PriceLabel);
            this.Name = "MissingAssetControl";
            this.Size = new System.Drawing.Size(421, 60);
            this.Load += new System.EventHandler(this.MissingAssetControl_Load);
            ((System.ComponentModel.ISupportInitialize)(this.AssetImage)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox AssetImage;
        private System.Windows.Forms.LinkLabel AssetNameLabel;
        private System.Windows.Forms.Button BuyButton;
        private System.Windows.Forms.Label PriceLabel;
    }
}

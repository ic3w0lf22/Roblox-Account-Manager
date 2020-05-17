namespace rbx_join
{
    partial class rbxjoin
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
            this.CloseButton = new System.Windows.Forms.Button();
            this.AccountsView = new System.Windows.Forms.ListView();
            this.ChooseAccountColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.ChooseAccountLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // CloseButton
            // 
            this.CloseButton.Location = new System.Drawing.Point(12, 354);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(268, 23);
            this.CloseButton.TabIndex = 0;
            this.CloseButton.Text = "Close";
            this.CloseButton.UseVisualStyleBackColor = true;
            this.CloseButton.Click += new System.EventHandler(this.Close_Click);
            // 
            // AccountsView
            // 
            this.AccountsView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.ChooseAccountColumn});
            this.AccountsView.FullRowSelect = true;
            this.AccountsView.HideSelection = false;
            this.AccountsView.Location = new System.Drawing.Point(12, 40);
            this.AccountsView.MultiSelect = false;
            this.AccountsView.Name = "AccountsView";
            this.AccountsView.Size = new System.Drawing.Size(268, 308);
            this.AccountsView.TabIndex = 1;
            this.AccountsView.UseCompatibleStateImageBehavior = false;
            this.AccountsView.View = System.Windows.Forms.View.Details;
            this.AccountsView.SelectedIndexChanged += new System.EventHandler(this.AccountsView_SelectedIndexChanged);
            this.AccountsView.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.AccountsView_MouseDoubleClick);
            // 
            // ChooseAccountColumn
            // 
            this.ChooseAccountColumn.Text = "Choose Account";
            this.ChooseAccountColumn.Width = 241;
            // 
            // ChooseAccountLabel
            // 
            this.ChooseAccountLabel.AutoSize = true;
            this.ChooseAccountLabel.Font = new System.Drawing.Font("Microsoft YaHei", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ChooseAccountLabel.Location = new System.Drawing.Point(57, 9);
            this.ChooseAccountLabel.Name = "ChooseAccountLabel";
            this.ChooseAccountLabel.Size = new System.Drawing.Size(185, 28);
            this.ChooseAccountLabel.TabIndex = 2;
            this.ChooseAccountLabel.Text = "Choose Account";
            this.ChooseAccountLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // rbxjoin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 389);
            this.Controls.Add(this.ChooseAccountLabel);
            this.Controls.Add(this.AccountsView);
            this.Controls.Add(this.CloseButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "rbxjoin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Join Roblox Game";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.rbxjoin_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button CloseButton;
        private System.Windows.Forms.ListView AccountsView;
        private System.Windows.Forms.ColumnHeader ChooseAccountColumn;
        private System.Windows.Forms.Label ChooseAccountLabel;
    }
}


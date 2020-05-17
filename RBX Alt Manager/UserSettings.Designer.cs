namespace RBX_Alt_Manager
{
    partial class UserSettings
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
            this.LabelAccount = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // LabelAccount
            // 
            this.LabelAccount.AutoSize = true;
            this.LabelAccount.Location = new System.Drawing.Point(12, 9);
            this.LabelAccount.Name = "LabelAccount";
            this.LabelAccount.Size = new System.Drawing.Size(119, 13);
            this.LabelAccount.TabIndex = 0;
            this.LabelAccount.Text = "Viewing Account: None";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(293, 25);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(116, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Set Following Privacy";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // UserSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(421, 259);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.LabelAccount);
            this.Name = "UserSettings";
            this.Text = "UserSettings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LabelAccount;
        private System.Windows.Forms.Button button1;
    }
}
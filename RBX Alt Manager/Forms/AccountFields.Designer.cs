namespace RBX_Alt_Manager
{
    partial class AccountFields
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
            this.AccountName = new System.Windows.Forms.Label();
            this.Add = new System.Windows.Forms.Button();
            this.Success = new System.Windows.Forms.Timer(this.components);
            this.FieldsPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.SuspendLayout();
            // 
            // AccountName
            // 
            this.AccountName.AutoSize = true;
            this.AccountName.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AccountName.Location = new System.Drawing.Point(12, 9);
            this.AccountName.Name = "AccountName";
            this.AccountName.Size = new System.Drawing.Size(132, 20);
            this.AccountName.TabIndex = 0;
            this.AccountName.Text = "Viewing Fields of ";
            // 
            // Add
            // 
            this.Add.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Add.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.Add.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Add.ForeColor = System.Drawing.Color.Black;
            this.Add.Location = new System.Drawing.Point(257, 9);
            this.Add.Name = "Add";
            this.Add.Size = new System.Drawing.Size(20, 20);
            this.Add.TabIndex = 1;
            this.Add.Text = "+";
            this.Add.UseVisualStyleBackColor = true;
            this.Add.Click += new System.EventHandler(this.Add_Click);
            // 
            // Success
            // 
            this.Success.Interval = 300;
            this.Success.Tick += new System.EventHandler(this.Success_Tick);
            // 
            // FieldsPanel
            // 
            this.FieldsPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FieldsPanel.AutoScroll = true;
            this.FieldsPanel.Location = new System.Drawing.Point(0, 35);
            this.FieldsPanel.Name = "FieldsPanel";
            this.FieldsPanel.Size = new System.Drawing.Size(289, 226);
            this.FieldsPanel.TabIndex = 2;
            // 
            // AccountFields
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(289, 261);
            this.Controls.Add(this.FieldsPanel);
            this.Controls.Add(this.Add);
            this.Controls.Add(this.AccountName);
            this.HelpButton = true;
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(305, 1000);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(305, 200);
            this.Name = "AccountFields";
            this.ShowIcon = false;
            this.Text = "Account Fields";
            this.HelpButtonClicked += new System.ComponentModel.CancelEventHandler(this.AccountFields_HelpButtonClicked);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AccountFields_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label AccountName;
        private System.Windows.Forms.Button Add;
        private System.Windows.Forms.Timer Success;
        private System.Windows.Forms.FlowLayoutPanel FieldsPanel;
    }
}
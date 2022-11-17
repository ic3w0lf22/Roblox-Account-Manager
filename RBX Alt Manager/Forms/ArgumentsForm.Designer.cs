namespace RBX_Alt_Manager
{
    partial class ArgumentsForm
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
            this.TeleportCB = new System.Windows.Forms.CheckBox();
            this.OldJoin = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // TeleportCB
            // 
            this.TeleportCB.AutoSize = true;
            this.TeleportCB.Location = new System.Drawing.Point(13, 13);
            this.TeleportCB.Name = "TeleportCB";
            this.TeleportCB.Size = new System.Drawing.Size(72, 17);
            this.TeleportCB.TabIndex = 0;
            this.TeleportCB.Text = "isTeleport";
            this.TeleportCB.UseVisualStyleBackColor = true;
            this.TeleportCB.CheckedChanged += new System.EventHandler(this.TeleportCB_CheckedChanged);
            // 
            // OldJoin
            // 
            this.OldJoin.AutoSize = true;
            this.OldJoin.Location = new System.Drawing.Point(13, 36);
            this.OldJoin.Name = "OldJoin";
            this.OldJoin.Size = new System.Drawing.Size(125, 17);
            this.OldJoin.TabIndex = 1;
            this.OldJoin.Text = "Use Old Join Method";
            this.OldJoin.UseVisualStyleBackColor = true;
            this.OldJoin.CheckedChanged += new System.EventHandler(this.OldJoin_CheckedChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(10, 85);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 2;
            this.button1.Text = "Set Version";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(10, 59);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(248, 20);
            this.textBox1.TabIndex = 3;
            // 
            // ArgumentsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(273, 182);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.OldJoin);
            this.Controls.Add(this.TeleportCB);
            this.MaximumSize = new System.Drawing.Size(289, 221);
            this.MinimumSize = new System.Drawing.Size(289, 221);
            this.Name = "ArgumentsForm";
            this.ShowIcon = false;
            this.Text = "ArgumentsForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ArgumentsForm_FormClosing);
            this.Load += new System.EventHandler(this.ArgumentsForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox TeleportCB;
        private System.Windows.Forms.CheckBox OldJoin;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
    }
}
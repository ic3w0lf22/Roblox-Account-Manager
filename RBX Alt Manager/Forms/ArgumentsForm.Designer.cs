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
            // ArgumentsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(273, 182);
            this.Controls.Add(this.OldJoin);
            this.Controls.Add(this.TeleportCB);
            this.MaximumSize = new System.Drawing.Size(289, 221);
            this.MinimumSize = new System.Drawing.Size(289, 221);
            this.Name = "ArgumentsForm";
            this.Text = "ArgumentsForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ArgumentsForm_FormClosing);
            this.Load += new System.EventHandler(this.ArgumentsForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox TeleportCB;
        private System.Windows.Forms.CheckBox OldJoin;
    }
}
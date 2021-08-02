namespace RBX_Alt_Manager
{
    partial class AccountUtils
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
            this.WhoFollow = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.button7 = new System.Windows.Forms.Button();
            this.EmailTip = new System.Windows.Forms.ToolTip(this.components);
            this.label3 = new System.Windows.Forms.Label();
            this.Username = new System.Windows.Forms.TextBox();
            this.Block = new System.Windows.Forms.Button();
            this.OpenApp = new System.Windows.Forms.Button();
            this.SetDisplayName = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // WhoFollow
            // 
            this.WhoFollow.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.WhoFollow.FormattingEnabled = true;
            this.WhoFollow.Items.AddRange(new object[] {
            "Everyone",
            "Friends, Followed, Followers",
            "Friends, Followed",
            "Friends",
            "No one"});
            this.WhoFollow.Location = new System.Drawing.Point(398, 109);
            this.WhoFollow.Name = "WhoFollow";
            this.WhoFollow.Size = new System.Drawing.Size(108, 21);
            this.WhoFollow.TabIndex = 13;
            this.WhoFollow.Text = "Who Can Follow";
            this.WhoFollow.SelectedIndexChanged += new System.EventHandler(this.WhoFollow_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 13);
            this.label1.TabIndex = 1000;
            this.label1.Text = "Current Password";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 24);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(205, 20);
            this.textBox1.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(221, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(78, 13);
            this.label2.TabIndex = 1000;
            this.label2.Text = "New Password";
            // 
            // textBox2
            // 
            this.textBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox2.Location = new System.Drawing.Point(224, 24);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(167, 20);
            this.textBox2.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(397, 23);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(109, 22);
            this.button1.TabIndex = 3;
            this.button1.Text = "Change Password";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(11, 52);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(207, 22);
            this.button2.TabIndex = 4;
            this.button2.Text = "Sign out of all other sessions";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button3.Location = new System.Drawing.Point(397, 52);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(109, 22);
            this.button3.TabIndex = 6;
            this.button3.Text = "Change Email";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // textBox3
            // 
            this.textBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox3.Location = new System.Drawing.Point(224, 53);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(167, 20);
            this.textBox3.TabIndex = 5;
            this.textBox3.Text = "Email";
            this.EmailTip.SetToolTip(this.textBox3, "Email requires the \"Current Password\" to be filled in");
            // 
            // textBox5
            // 
            this.textBox5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox5.Location = new System.Drawing.Point(283, 109);
            this.textBox5.MaxLength = 4;
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(52, 20);
            this.textBox5.TabIndex = 11;
            this.textBox5.Text = "Pin";
            this.textBox5.Enter += new System.EventHandler(this.textBox5_Enter);
            this.textBox5.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox5_KeyPress);
            // 
            // button7
            // 
            this.button7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button7.Location = new System.Drawing.Point(341, 108);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(51, 22);
            this.button7.TabIndex = 12;
            this.button7.Text = "Unlock";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // EmailTip
            // 
            this.EmailTip.AutoPopDelay = 1000;
            this.EmailTip.InitialDelay = 500;
            this.EmailTip.ReshowDelay = 100;
            this.EmailTip.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.EmailTip.ToolTipTitle = "Change Email";
            this.EmailTip.UseAnimation = false;
            this.EmailTip.UseFading = false;
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 109);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(193, 26);
            this.label3.TabIndex = 1001;
            this.label3.Text = "The account you have selected on the\r\nalt manager is the account being edited\r\n";
            // 
            // Username
            // 
            this.Username.Location = new System.Drawing.Point(12, 80);
            this.Username.Name = "Username";
            this.Username.Size = new System.Drawing.Size(80, 20);
            this.Username.TabIndex = 7;
            // 
            // Block
            // 
            this.Block.Location = new System.Drawing.Point(98, 79);
            this.Block.Name = "Block";
            this.Block.Size = new System.Drawing.Size(50, 22);
            this.Block.TabIndex = 8;
            this.Block.Text = "Block";
            this.Block.UseVisualStyleBackColor = true;
            this.Block.Click += new System.EventHandler(this.Block_Click);
            // 
            // OpenApp
            // 
            this.OpenApp.Location = new System.Drawing.Point(153, 79);
            this.OpenApp.Name = "OpenApp";
            this.OpenApp.Size = new System.Drawing.Size(65, 22);
            this.OpenApp.TabIndex = 9;
            this.OpenApp.Text = "Open App";
            this.OpenApp.UseVisualStyleBackColor = true;
            this.OpenApp.Click += new System.EventHandler(this.OpenApp_Click);
            // 
            // SetDisplayName
            // 
            this.SetDisplayName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SetDisplayName.Location = new System.Drawing.Point(397, 79);
            this.SetDisplayName.Name = "SetDisplayName";
            this.SetDisplayName.Size = new System.Drawing.Size(109, 22);
            this.SetDisplayName.TabIndex = 1003;
            this.SetDisplayName.Text = "Set Display Name";
            this.SetDisplayName.UseVisualStyleBackColor = true;
            this.SetDisplayName.Click += new System.EventHandler(this.SetDisplayName_Click);
            // 
            // AccountUtils
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(518, 142);
            this.Controls.Add(this.SetDisplayName);
            this.Controls.Add(this.OpenApp);
            this.Controls.Add(this.Block);
            this.Controls.Add(this.Username);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.textBox5);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.WhoFollow);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MinimumSize = new System.Drawing.Size(534, 181);
            this.Name = "AccountUtils";
            this.Text = " Account Utilities";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AccountUtils_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox WhoFollow;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.ToolTip EmailTip;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox Username;
        private System.Windows.Forms.Button Block;
        private System.Windows.Forms.Button OpenApp;
        private System.Windows.Forms.Button SetDisplayName;
    }
}
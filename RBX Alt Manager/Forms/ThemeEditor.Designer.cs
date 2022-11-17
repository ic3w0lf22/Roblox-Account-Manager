namespace RBX_Alt_Manager.Forms
{
    partial class ThemeEditor
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
            this.SetBG = new System.Windows.Forms.Button();
            this.SelectColor = new System.Windows.Forms.ColorDialog();
            this.Selection = new System.Windows.Forms.ListBox();
            this.SetFG = new System.Windows.Forms.Button();
            this.SetBorder = new System.Windows.Forms.Button();
            this.ChangeStyle = new System.Windows.Forms.Button();
            this.HideHeaders = new System.Windows.Forms.Button();
            this.ToggleDarkTopBar = new System.Windows.Forms.Button();
            this.ToggleTransparentBG = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // SetBG
            // 
            this.SetBG.Location = new System.Drawing.Point(139, 13);
            this.SetBG.Name = "SetBG";
            this.SetBG.Size = new System.Drawing.Size(108, 23);
            this.SetBG.TabIndex = 0;
            this.SetBG.Text = "Set Background";
            this.SetBG.UseVisualStyleBackColor = true;
            this.SetBG.Click += new System.EventHandler(this.SetBG_Click);
            // 
            // Selection
            // 
            this.Selection.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.Selection.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Selection.FormattingEnabled = true;
            this.Selection.ItemHeight = 18;
            this.Selection.Items.AddRange(new object[] {
            "Accounts",
            "Buttons",
            "Forms",
            "Text Boxes",
            "Labels"});
            this.Selection.Location = new System.Drawing.Point(13, 13);
            this.Selection.Name = "Selection";
            this.Selection.Size = new System.Drawing.Size(120, 130);
            this.Selection.TabIndex = 1;
            this.Selection.Tag = "UseControlFont";
            this.Selection.SelectedIndexChanged += new System.EventHandler(this.Selection_SelectedIndexChanged);
            // 
            // SetFG
            // 
            this.SetFG.Location = new System.Drawing.Point(139, 42);
            this.SetFG.Name = "SetFG";
            this.SetFG.Size = new System.Drawing.Size(108, 23);
            this.SetFG.TabIndex = 2;
            this.SetFG.Text = "Set Foreground";
            this.SetFG.UseVisualStyleBackColor = true;
            this.SetFG.Click += new System.EventHandler(this.SetFG_Click);
            // 
            // SetBorder
            // 
            this.SetBorder.Location = new System.Drawing.Point(139, 71);
            this.SetBorder.Name = "SetBorder";
            this.SetBorder.Size = new System.Drawing.Size(108, 23);
            this.SetBorder.TabIndex = 3;
            this.SetBorder.Text = "Set Border Color";
            this.SetBorder.UseVisualStyleBackColor = true;
            this.SetBorder.Visible = false;
            this.SetBorder.Click += new System.EventHandler(this.SetBorder_Click);
            // 
            // ChangeStyle
            // 
            this.ChangeStyle.Location = new System.Drawing.Point(139, 100);
            this.ChangeStyle.Name = "ChangeStyle";
            this.ChangeStyle.Size = new System.Drawing.Size(108, 23);
            this.ChangeStyle.TabIndex = 4;
            this.ChangeStyle.Text = "Change Style";
            this.ChangeStyle.UseVisualStyleBackColor = true;
            this.ChangeStyle.Visible = false;
            this.ChangeStyle.Click += new System.EventHandler(this.ChangeStyle_Click);
            // 
            // HideHeaders
            // 
            this.HideHeaders.Location = new System.Drawing.Point(139, 71);
            this.HideHeaders.Name = "HideHeaders";
            this.HideHeaders.Size = new System.Drawing.Size(108, 23);
            this.HideHeaders.TabIndex = 5;
            this.HideHeaders.Text = "Hide Headers";
            this.HideHeaders.UseVisualStyleBackColor = true;
            this.HideHeaders.Visible = false;
            this.HideHeaders.Click += new System.EventHandler(this.HideHeaders_Click);
            // 
            // ToggleDarkTopBar
            // 
            this.ToggleDarkTopBar.Location = new System.Drawing.Point(139, 71);
            this.ToggleDarkTopBar.Name = "ToggleDarkTopBar";
            this.ToggleDarkTopBar.Size = new System.Drawing.Size(108, 23);
            this.ToggleDarkTopBar.TabIndex = 6;
            this.ToggleDarkTopBar.Text = "Dark Top Bar";
            this.ToggleDarkTopBar.UseVisualStyleBackColor = true;
            this.ToggleDarkTopBar.Visible = false;
            this.ToggleDarkTopBar.Click += new System.EventHandler(this.ToggleDarkTopBar_Click);
            // 
            // ToggleTransparentBG
            // 
            this.ToggleTransparentBG.Location = new System.Drawing.Point(139, 71);
            this.ToggleTransparentBG.Name = "ToggleTransparentBG";
            this.ToggleTransparentBG.Size = new System.Drawing.Size(108, 23);
            this.ToggleTransparentBG.TabIndex = 7;
            this.ToggleTransparentBG.Text = "Transparent BG";
            this.ToggleTransparentBG.UseVisualStyleBackColor = true;
            this.ToggleTransparentBG.Visible = false;
            this.ToggleTransparentBG.Click += new System.EventHandler(this.ToggleTransparentBG_Click);
            // 
            // ThemeEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(259, 155);
            this.Controls.Add(this.ToggleTransparentBG);
            this.Controls.Add(this.ToggleDarkTopBar);
            this.Controls.Add(this.HideHeaders);
            this.Controls.Add(this.ChangeStyle);
            this.Controls.Add(this.SetBorder);
            this.Controls.Add(this.SetFG);
            this.Controls.Add(this.Selection);
            this.Controls.Add(this.SetBG);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ThemeEditor";
            this.ShowIcon = false;
            this.Text = "Theme Editor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ThemeEditor_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button SetBG;
        private System.Windows.Forms.ColorDialog SelectColor;
        private System.Windows.Forms.ListBox Selection;
        private System.Windows.Forms.Button SetFG;
        private System.Windows.Forms.Button SetBorder;
        private System.Windows.Forms.Button ChangeStyle;
        private System.Windows.Forms.Button HideHeaders;
        private System.Windows.Forms.Button ToggleDarkTopBar;
        private System.Windows.Forms.Button ToggleTransparentBG;
    }
}
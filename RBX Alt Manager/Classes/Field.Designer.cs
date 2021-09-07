namespace RBX_Alt_Manager.Classes
{
    partial class Field
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
            this.FieldBox = new System.Windows.Forms.TextBox();
            this.ValueBox = new System.Windows.Forms.TextBox();
            this.Delete = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // FieldBox
            // 
            this.FieldBox.Location = new System.Drawing.Point(3, 3);
            this.FieldBox.Name = "FieldBox";
            this.FieldBox.Size = new System.Drawing.Size(115, 20);
            this.FieldBox.TabIndex = 0;
            // 
            // ValueBox
            // 
            this.ValueBox.Location = new System.Drawing.Point(124, 3);
            this.ValueBox.Name = "ValueBox";
            this.ValueBox.Size = new System.Drawing.Size(115, 20);
            this.ValueBox.TabIndex = 1;
            this.ValueBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.ValueBox_KeyPress);
            // 
            // Delete
            // 
            this.Delete.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Delete.ForeColor = System.Drawing.Color.Red;
            this.Delete.Location = new System.Drawing.Point(245, 2);
            this.Delete.Name = "Delete";
            this.Delete.Size = new System.Drawing.Size(22, 22);
            this.Delete.TabIndex = 2;
            this.Delete.Text = "X";
            this.Delete.UseVisualStyleBackColor = true;
            this.Delete.Click += new System.EventHandler(this.Delete_Click);
            // 
            // Field
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.Delete);
            this.Controls.Add(this.ValueBox);
            this.Controls.Add(this.FieldBox);
            this.Name = "Field";
            this.Size = new System.Drawing.Size(269, 26);
            this.Load += new System.EventHandler(this.Field_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox FieldBox;
        private System.Windows.Forms.TextBox ValueBox;
        private System.Windows.Forms.Button Delete;
    }
}

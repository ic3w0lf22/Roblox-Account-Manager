using RBX_Alt_Manager.Forms;
using System;
using System.Windows.Forms;

namespace RBX_Alt_Manager
{
    public partial class ArgumentsForm : Form
    {
        private delegate void SafeCallDelegate();

        public ArgumentsForm()
        {
            AccountManager.SetDarkBar(Handle);

            InitializeComponent();
        }

        public void ApplyTheme()
        {
            BackColor = ThemeEditor.FormsBackground;
            ForeColor = ThemeEditor.FormsForeground;

            foreach (Control control in this.Controls)
            {
                if (control is Button || control is CheckBox)
                {
                    if (control is Button)
                    {
                        Button b = control as Button;
                        b.FlatStyle = ThemeEditor.ButtonStyle;
                        b.FlatAppearance.BorderColor = ThemeEditor.ButtonsBorder;
                    }

                    if (!(control is CheckBox)) control.BackColor = ThemeEditor.ButtonsBackground;
                    control.ForeColor = ThemeEditor.ButtonsForeground;
                }
                else if (control is TextBox || control is RichTextBox || control is Label)
                {
                    if (control is Classes.BorderedTextBox)
                    {
                        Classes.BorderedTextBox b = control as Classes.BorderedTextBox;
                        b.BorderColor = ThemeEditor.TextBoxesBorder;
                    }

                    if (control is Classes.BorderedRichTextBox)
                    {
                        Classes.BorderedRichTextBox b = control as Classes.BorderedRichTextBox;
                        b.BorderColor = ThemeEditor.TextBoxesBorder;
                    }

                    control.BackColor = ThemeEditor.TextBoxesBackground;
                    control.ForeColor = ThemeEditor.TextBoxesForeground;
                }
                else if (control is ListBox)
                {
                    control.BackColor = ThemeEditor.ButtonsBackground;
                    control.ForeColor = ThemeEditor.ButtonsForeground;
                }
            }
        }

        private void TeleportCB_CheckedChanged(object sender, EventArgs e)
        {
            AccountManager.IsTeleport = TeleportCB.Checked;
        }

        private void ArgumentsForm_Load(object sender, EventArgs e)
        {

        }

        private void CloseForm()
        {
            if (this.InvokeRequired)
            {
                var close = new SafeCallDelegate(CloseForm);
                this.Invoke(close, new object[] { });
            }
            else
                Close();
        }

        public void HideForm()
        {
            if (this.InvokeRequired)
            {
                var hide = new SafeCallDelegate(HideForm);
                this.Invoke(hide, new object[] { });
            }
            else
                Hide();
        }

        public void ShowForm()
        {
            Show();
        }

        private void ArgumentsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            HideForm();
            e.Cancel = true;
        }

        private void OldJoin_CheckedChanged(object sender, EventArgs e)
        {
            AccountManager.UseOldJoin = OldJoin.Checked;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            AccountManager.CurrentVersion = textBox1.Text;
        }
    }
}
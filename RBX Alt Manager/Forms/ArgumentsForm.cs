using RBX_Alt_Manager.Forms;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace RBX_Alt_Manager
{
    public partial class ArgumentsForm : Form
    {
        public ArgumentsForm()
        {
            AccountManager.SetDarkBar(Handle);

            InitializeComponent();
            this.Rescale();
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
                else if (control is TextBox || control is RichTextBox)
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
                else if (control is Label)
                {
                    control.BackColor = ThemeEditor.LabelTransparent ? Color.Transparent : ThemeEditor.LabelBackground;
                    control.ForeColor = ThemeEditor.LabelForeground;
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

        public void HideForm() => this.InvokeIfRequired(() => Hide());

        public void ShowForm() => Show();

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
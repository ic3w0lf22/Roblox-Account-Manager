using RBX_Alt_Manager.Forms;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace RBX_Alt_Manager
{
    public partial class AccountUtils : Form
    {
        // sorry i got lazy with this part of code and didnt name any elemennts

        public AccountUtils()
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

        private void AccountUtils_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void WhoFollow_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (AccountManager.SelectedAccount == null) return;

            AccountManager.SelectedAccount.SetFollowPrivacy(WhoFollow.SelectedIndex);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (AccountManager.SelectedAccount == null) return;

            DialogResult result = MessageBox.Show($"Are you sure you want sign out of all other sessions?", "Account Utilities", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
                AccountManager.SelectedAccount.LogOutOfOtherSessions();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (AccountManager.SelectedAccount == null) return;

            AccountManager.SelectedAccount.UnlockPin(textBox5.Text);
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (AccountManager.SelectedAccount == null) return;

            if (e.KeyChar == (char)Keys.Enter)
            {
                AccountManager.SelectedAccount.UnlockPin(textBox5.Text);
                button7.Focus();

                return;
            }

            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox5_Enter(object sender, EventArgs e)
        {
            textBox5.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (AccountManager.SelectedAccount == null) return;

            AccountManager.SelectedAccount.ChangePassword(textBox1.Text, textBox2.Text);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (AccountManager.SelectedAccount == null) return;

            AccountManager.SelectedAccount.ChangeEmail(textBox1.Text, textBox3.Text);
        }

        private void Block_Click(object sender, EventArgs e)
        {
            if (AccountManager.SelectedAccount == null) return;

            bool WasUnblocked = false;

            try
            {
                if (AccountManager.SelectedAccount.TogglePlayerBlocked(Username.Text, ref WasUnblocked))
                    MessageBox.Show($"{(WasUnblocked ? "Unb" : "B")}locked {Username.Text}");
                else
                    MessageBox.Show($"Failed to {(WasUnblocked ? "Unb" : "B")}lock {Username.Text}");
            }
            catch (Exception x) { MessageBox.Show($"Failed to {(WasUnblocked ? "Unb" : "B")}lock {Username.Text}\n\n{x.Message}"); }
        }

        private void SetDisplayName_Click(object sender, EventArgs e)
        {
            if (AccountManager.SelectedAccount == null) return;

            try
            {
                AccountManager.SelectedAccount.SetDisplayName(DisplayName.Text);
                MessageBox.Show($"Successfully set {AccountManager.SelectedAccount.Username}'s Display Name to {DisplayName.Text}", "Account Utilities", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception x) { MessageBox.Show(x.Message, "Account Utilities", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        private void AddFriend_Click(object sender, EventArgs e)
        {
            if (AccountManager.SelectedAccount == null) return;

            AccountManager.SelectedAccount.SendFriendRequest(Username.Text);
        }
    }
}
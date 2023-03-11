using RBX_Alt_Manager.Classes;
using RBX_Alt_Manager.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace RBX_Alt_Manager
{
    public partial class AccountFields : Form
    {
        Account Viewing;
        Color LastColor;

        public AccountFields()
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

        private void AccountFields_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        public void FlashGreen()
        {
            LastColor = AccountName.ForeColor;
            AccountName.ForeColor = Color.Green;

            Success.Start();
        }

        private void AddField(string Field, string Value)
        {
            Field f = new Field(Viewing, Field, Value);
            FieldsPanel.Controls.Add(f);
        }

        public void View(Account account)
        {
            if (account == null) return;

            Viewing = account;

            FieldsPanel.Controls.Clear();

            AccountName.Text = "Viewing Fields of " + account.Username;

            foreach (KeyValuePair<string, string> Key in account.Fields) AddField(Key.Key, Key.Value);

            Show();
            WindowState = FormWindowState.Normal;
            BringToFront();
        }

        private void AccountFields_HelpButtonClicked(object sender, CancelEventArgs e)
        {
            e.Cancel = true;

            MessageBox.Show("To edit a field, press enter on the value (right) side", "Account Fields", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Add_Click(object sender, EventArgs e)
        {
            AddField("Field", "Value");
        }

        private void Success_Tick(object sender, EventArgs e)
        {
            Success.Stop();

            AccountName.ForeColor = LastColor;
        }
    }
}
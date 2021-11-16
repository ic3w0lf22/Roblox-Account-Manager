using RBX_Alt_Manager.Forms;
using RestSharp;
using System;
using System.Net;
using System.Windows.Forms;

namespace RBX_Alt_Manager
{
    public partial class ImportForm : Form
    {
        public ImportForm()
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

        private void ImportForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        private void ImportButton_Click(object sender, EventArgs e)
        {
            foreach (string Token in Accounts.Text.Split('\n'))
            {
                RestRequest myAcc = new RestRequest("my/account/json", Method.GET);

                myAcc.AddCookie(".ROBLOSECURITY", Token);

                IRestResponse response = AccountManager.MainClient.Execute(myAcc);

                if (response.StatusCode == HttpStatusCode.OK && response.Content.Contains("DisplayName")) // shitty check i know ...
                    AccountManager.AddAccount(Token, response.Content);
            }
        }
    }
}
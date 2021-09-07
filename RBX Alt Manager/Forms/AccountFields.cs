using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using RBX_Alt_Manager.Classes;

namespace RBX_Alt_Manager
{
    public partial class AccountFields : Form
    {
        Account Viewing;
        int FieldCount = 0;
        int StartingControlCount = 0;

        public AccountFields()
        {
            InitializeComponent();

            StartingControlCount = Controls.Count - 1;
        }

        private void AccountFields_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        public void FlashGreen()
        {
            AccountName.ForeColor = Color.Green;

            Success.Start();
        }

        private void AddField(string Field, string Value)
        {
            /*TextBox FieldBox = new TextBox
            {
                Text = Field,
                Location = new Point(20, 45 + (25 * FieldCount)),
                Size = new Size(125, 25),
                Parent = this
            };

            TextBox ValueBox = new TextBox
            {
                Text = Value,
                Location = new Point(20 + 125 + 5, 45 + (25 * FieldCount)),
                Size = new Size(125, 25),
                Parent = this
            };

            Button XButton = new Button
            {
                Text = "X",
                Location = new Point(20 + 125 + 10 + 125, 45 + (25 * FieldCount) - 1),
                Size = new Size(22, 22),
                Font = new Font("Verdana", 9, FontStyle.Bold),
                ForeColor = Color.Red,
                Parent = this
            };

            ValueBox.KeyPress += Value_KeyPress;
            XButton.MouseClick += XButton_MouseClick;*/

            Field f = new Field(Viewing, Field, Value);
            f.Parent = this;
            f.Location = new Point(10, 45 + (25 * FieldCount));

            FieldCount += 1;
        }

        public void View(Account account)
        {
            if (account == null) return;

            Viewing = account;

            for (int i = Controls.Count - 1; i > StartingControlCount; i--)
                Controls.RemoveAt(i);

            FieldCount = 0;
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

            AccountName.ForeColor = SystemColors.ControlText;
        }
    }
}

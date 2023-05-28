using System;
using System.Windows.Forms;

namespace RBX_Alt_Manager.Classes
{
    public partial class Field : UserControl
    {
        Account account;
        string FieldName;
        string FieldValue;

        public Field(Account account, string Field, string Value)
        {
            InitializeComponent();

            this.account = account;
            FieldName = Field;
            FieldValue = Value;
        }

        private void Field_Load(object sender, EventArgs e)
        {
            FieldBox.Text = FieldName;
            ValueBox.Text = FieldValue;
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            Parent.Controls.Remove(this);

            account.RemoveField(FieldBox.Text);
        }

        private void ValueBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox ValueBox = sender as TextBox;

            if (e.KeyChar == 13)
            {
                if (FieldBox.Text != FieldName)
                {
                    account.RemoveField(FieldName);

                    FieldName = FieldBox.Text;
                }

                account.SetField(FieldBox.Text, ValueBox.Text);

                (FindForm() as AccountFields)?.FlashGreen();
            }
        }
    }
}
using System;
using System.Drawing;
using System.Windows.Forms;

namespace RBX_Alt_Manager.Classes
{
    public partial class Field : UserControl
    {
        Account account;
        string FieldName;
        string FieldValue;
        AccountFields parent;

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

            parent = Parent as AccountFields;
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            int idx = Parent.Controls.GetChildIndex(this); Console.WriteLine(idx);

            for (int i = idx; i < Parent.Controls.Count; i++)
            {
                Control control = Parent.Controls[i];

                control.Location = new Point(control.Location.X, control.Location.Y - 25);
            }

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

                parent.FlashGreen();
            }
        }
    }
}
using System;
using System.Windows.Forms;

namespace RBX_Alt_Manager
{
    public partial class ArgumentsForm : Form
    {
        private delegate void SafeCallDelegate();

        public ArgumentsForm()
        {
            InitializeComponent();
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
    }
}
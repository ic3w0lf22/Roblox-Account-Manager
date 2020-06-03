using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace rbx_join
{
    public partial class rbxjoin : Form
    {
        public rbxjoin()
        {
            InitializeComponent();
        }

        private long PlaceId;
        private string JobId;

        private void rbxjoin_Load(object sender, EventArgs e)
        {
            string[] Arguments = Environment.GetCommandLineArgs();

            if (Arguments.Length == 2 && Arguments[1].Contains("rbx-join"))
            {
                Match match = Regex.Match(Arguments[1], @"rbx-join:\/\/(\d+)\/?(\w+-\w+-\w+-\w+-\w+)?");

                if (match.Success)
                {
                    PlaceId = Convert.ToInt64(match.Groups[1].Value);
                    JobId = match.Groups.Count == 3 ? match.Groups[2].Value : "";
                }
            }

            StartPosition = FormStartPosition.Manual;
            Top = 50;
            Left = Screen.PrimaryScreen.WorkingArea.Width / 2 - (Width / 2);

            try
            {
                using (var pipe = new NamedPipeClientStream("localhost", "AccountManagerPipe", PipeDirection.InOut))
                {
                    pipe.Connect(2500);
                    pipe.ReadMode = PipeTransmissionMode.Message;

                    byte[] bytes = Encoding.Default.GetBytes("acclist");
                    pipe.Write(bytes, 0, bytes.Length);

                    var message = ReadMessage(pipe);
                    string result = Encoding.UTF8.GetString(message);

                    if (string.IsNullOrEmpty(result)) Close();

                    string[] Accounts = result.Split(new[] { "\n" }, StringSplitOptions.None);
                    bool Hide = false;

                    if (Accounts.Length > 0 && Accounts[0] == "hidden")
                        Hide = true;

                    foreach (string Account in Accounts)
                    {
                        if (!Account.Contains("::")) continue;

                        ListViewItem lm = new ListViewItem();
                        int CIndex = Account.IndexOf("::");
                        string Name = Account.Substring(0, CIndex);
                        string Alias = Account.Substring(CIndex + 2);

                        lm.Text = (Hide ? Regex.Replace(Name, ".", "*") : Name) + (!string.IsNullOrWhiteSpace(Alias) ? ": " + Alias : "");
                        lm.ToolTipText = Alias;
                        lm.Name = Name;
                        AccountsView.Items.Add(lm);
                    }
                }
            }
            catch (Exception x)
            {
                MessageBox.Show("Error, try restarting the Account Manager\n" + x.ToString(), "rbx-join", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
        }

        private static byte[] ReadMessage(PipeStream pipe)
        {
            byte[] buffer = new byte[1024];
            using (var ms = new MemoryStream())
            {
                do
                {
                    var readBytes = pipe.Read(buffer, 0, buffer.Length);
                    ms.Write(buffer, 0, readBytes);
                }
                while (!pipe.IsMessageComplete);

                return ms.ToArray();
            }
        }

        private void Close_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void AccountsView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (AccountsView.SelectedItems.Count > 1) return;

            ListViewItem self = AccountsView.SelectedItems[0];

            using (var pipe = new NamedPipeClientStream("localhost", "AccountManagerPipe", PipeDirection.InOut))
            {
                pipe.Connect(5000);
                pipe.ReadMode = PipeTransmissionMode.Message;

                // string Account = self.Text.Contains(":") ? self.Text.Substring(0, self.Text.IndexOf(":")) : self.Text;

                byte[] bytes = Encoding.Default.GetBytes("play-" + self.Name + "-" + PlaceId + (!string.IsNullOrEmpty(JobId) ? "-" + JobId : ""));
                pipe.Write(bytes, 0, bytes.Length);

                this.Close();
            }
        }

        private void AccountsView_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}

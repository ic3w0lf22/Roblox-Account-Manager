using Microsoft.Win32;
using System;
using System.IO;
using System.Windows.Forms;

namespace RegisterRbxJoinProtocol
{
    public partial class Setup : Form
    {
        public Setup()
        {
            InitializeComponent();
        }

        private string RbxJoinPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "rbx-join.exe");

        private void Setup_Load(object sender, EventArgs e)
        {
            try
            {
                if (Registry.ClassesRoot.OpenSubKey("rbx-join") != null)
                    Registry.ClassesRoot.DeleteSubKeyTree("rbx-join");

                RegistryKey key = Registry.ClassesRoot.CreateSubKey("rbx-join");
                key.SetValue("", "URL: rbx-join Protocol");
                key.SetValue("URL Protocol", "rbx-join");

                key = key.CreateSubKey(@"shell\open\command");
                key.SetValue("", RbxJoinPath + " \"%1\"");

                MessageBox.Show("Successfully registered the rbx-join protocol!", "rbx-join", MessageBoxButtons.OK);
            }
            catch (Exception x) { MessageBox.Show("Failed to register the rbx-join protocol: \n" + x.Message, "rbx-join", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            
            Close();
        }
    }
}
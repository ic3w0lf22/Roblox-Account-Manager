using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RBX_Alt_Manager
{
    public partial class ImportForm : Form
    {
        public ImportForm()
        {
            InitializeComponent();
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
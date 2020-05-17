using BrightIdeasSoftware;
using Newtonsoft.Json;
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
    public partial class ServerList : Form
    {
        public ServerList()
        {
            InitializeComponent();
        }

        public static RestClient rbxclient;
        public static RestClient gamesclient;
        private long PlaceId;

        private void ServerList_Load(object sender, EventArgs e)
        {
            rbxclient = new RestClient("https://roblox.com/");
            rbxclient.CachePolicy = new System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.BypassCache);

            gamesclient = new RestClient("https://games.roblox.com/");
            gamesclient.CachePolicy = new System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.BypassCache);
        }

        private void ServerList_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void RefreshServers_Click(object sender, EventArgs e)
        {
            if (!Int64.TryParse(Program.MainForm.PlaceID.Text, out PlaceId)) return;

            ServerListView.Items.Clear();
            IRestResponse response;

            ServersInfo publicInfo = new ServersInfo();
            ServersInfo vipInfo = new ServersInfo();
            publicInfo.nextPageCursor = "";
            vipInfo.nextPageCursor = "";
            List<ServerData> servers = new List<ServerData>();

            Task.Factory.StartNew(() =>
            {
                while (publicInfo.nextPageCursor != null)
                {
                    RestRequest request = new RestRequest("v1/games/" + Program.MainForm.PlaceID.Text + " /servers/public?sortOrder=Asc&limit=100" + (string.IsNullOrEmpty(publicInfo.nextPageCursor) ? "" : "&cursor=" + publicInfo.nextPageCursor), Method.GET);
                    response = gamesclient.Execute(request);

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        publicInfo = JsonConvert.DeserializeObject<ServersInfo>(response.Content);

                        foreach (ServerData data in publicInfo.data)
                        {
                            data.type = "Public";
                            servers.Add(data);
                        }
                    }
                }

                if (AccountManager.SelectedAccount != null)
                {
                    while (vipInfo.nextPageCursor != null)
                    {
                        RestRequest request = new RestRequest("v1/games/" + Program.MainForm.PlaceID.Text + " /servers/VIP?sortOrder=Asc&limit=100" + (string.IsNullOrEmpty(vipInfo.nextPageCursor) ? "" : "&cursor=" + vipInfo.nextPageCursor), Method.GET);
                        request.AddCookie(".ROBLOSECURITY", AccountManager.SelectedAccount.SecurityToken);
                        request.AddHeader("Accept", "application/json");
                        response = gamesclient.Execute(request);

                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            vipInfo = JsonConvert.DeserializeObject<ServersInfo>(response.Content);

                            foreach (ServerData data in vipInfo.data)
                            {
                                data.id = data.name;
                                data.type = "VIP";
                                servers.Add(data);
                            }
                        }
                    }
                }

                ServerListView.SetObjects(servers);
            });
        }

        private void ServerListView_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ServerData server = ServerListView.SelectedObject as ServerData;

            if (server != null)
                Program.MainForm.JobID.Text = server.type == "VIP" ? "VIP:" + server.accessCode : server.id;
        }

        private void joinServerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListView AccountsView = Program.MainForm.AccountsView;

            if (AccountsView.SelectedItems.Count != 1 || AccountManager.SelectedAccount == null) return;

            string res = AccountManager.SelectedAccount.JoinServer(Convert.ToInt64(Program.MainForm.PlaceID.Text), ServerListView.SelectedItem.Text, false, false);

            if (!res.Contains("Success"))
                MessageBox.Show(res);
        }

        private void SearchPlayer_Click(object sender, EventArgs e)
        {
            if (AccountManager.SelectedAccount == null)
            {
                MessageBox.Show("Select an account on the main form", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            int UserID = AccountManager.GetUserID(Username.Text);

            if (UserID < 0)
            {
                MessageBox.Show("Failed to get UserID of " + Username.Text, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string token = AccountManager.SelectedAccount.SecurityToken;
            RestRequest request = new RestRequest("headshot-thumbnail/json?userId=" + UserID.ToString() + "&width=48&height=48", Method.GET);
            request.AddCookie(".ROBLOSECURITY", token);
            request.AddHeader("Accept", "application/json");
            request.AddHeader("Host", "www.roblox.com");
            IRestResponse response = rbxclient.Execute(request);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                MessageBox.Show("Failed to get AvatarUrl of " + Username.Text, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            Avatar avatar = JsonConvert.DeserializeObject<Avatar>(response.Content);
            int index = 0;

            request = new RestRequest("games/getgameinstancesjson?placeId=" + Program.MainForm.PlaceID.Text + "&startIndex=" + index.ToString());
            request.AddCookie(".ROBLOSECURITY", token);
            request.AddHeader("Host", "www.roblox.com");
            response = rbxclient.Execute(request);

            if (response.StatusCode != HttpStatusCode.OK)
            {
                MessageBox.Show("Failed to get game instances, try selecting a different account", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            GameInstancesCollection instances = JsonConvert.DeserializeObject<GameInstancesCollection>(response.Content);
            string UserFound = "";
            ServerData serverData = new ServerData();

            foreach (GameInstance t in instances.Collection)
            {
                foreach (GamePlayer p in t.CurrentPlayers)
                {
                    if (p.Thumbnail.Url == avatar.Url)
                        UserFound = t.Guid;
                }
            }

            while (instances.Collection.Count != 0)
            {
                if (!string.IsNullOrEmpty(UserFound)) break;

                index += 10;
                request = new RestRequest("games/getgameinstancesjson?placeId=" + Program.MainForm.PlaceID.Text + "&startIndex=" + index.ToString());
                request.AddCookie(".ROBLOSECURITY", token);
                request.AddHeader("Host", "www.roblox.com");
                response = rbxclient.Execute(request);
                instances = JsonConvert.DeserializeObject<GameInstancesCollection>(response.Content);

                if (instances == null) break;

                foreach (GameInstance t in instances.Collection)
                {
                    foreach (GamePlayer p in t.CurrentPlayers)
                    {
                        if (p.Thumbnail.Url == avatar.Url)
                        {
                            UserFound = t.Guid;
                            serverData.id = t.Guid;
                            serverData.playing = t.CurrentPlayers.Count;
                            serverData.ping = t.Ping;
                            serverData.fps = t.Fps;
                        }
                    }
                }
            }

            if (!string.IsNullOrEmpty(UserFound))
            {
                ServerListView.ClearObjects();
                ServerListView.SetObjects(new List<ServerData> { serverData });
            }
            else MessageBox.Show("User not found!", "Search", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }

        private void copyJobIdToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ServerListView.SelectedItem != null)
                Clipboard.SetText(ServerListView.SelectedItem.Text);
        }

        private void copyJoinLinkToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.ServerListView.SelectedItem != null)
                Clipboard.SetText("<rbx-join://" + PlaceId.ToString() + "/" + this.ServerListView.SelectedItem.Text + ">");
        }

        private void ServerListView_BeforeSorting(object sender, BeforeSortingEventArgs e)
        {

        }
    }
}
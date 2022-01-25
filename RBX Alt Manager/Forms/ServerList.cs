using BrightIdeasSoftware;
using Newtonsoft.Json;
using RBX_Alt_Manager.Forms;
using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RBX_Alt_Manager
{
    public partial class ServerList : Form
    {
        public ServerList()
        {
            AccountManager.SetDarkBar(Handle);

            InitializeComponent();

            if (ThemeEditor.UseDarkTopBar) Icon = Properties.Resources.server_icon_white;
        }

        public void ApplyTheme()
        {
            BackColor = ThemeEditor.FormsBackground;
            ForeColor = ThemeEditor.FormsForeground;

            foreach (TabPage tab in Tabs.TabPages)
            {
                tab.BackColor = ThemeEditor.FormsBackground;
                tab.ForeColor = ThemeEditor.FormsForeground;

                foreach (Control control in tab.Controls)
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
                    else if (control is ListBox || control is ObjectListView)
                    {
                        if (control is ObjectListView) ((ObjectListView)control).HeaderStyle = ThemeEditor.ShowHeaders ? ColumnHeaderStyle.Clickable : ColumnHeaderStyle.None;
                        control.BackColor = ThemeEditor.ButtonsBackground;
                        control.ForeColor = ThemeEditor.ButtonsForeground;
                    }
                }
            }
        }

        public static RestClient rbxclient;
        public static RestClient gamesclient;
        public bool Busy;
        private int Page = 0;
        private List<FavoriteGame> Favorites;
        private DateTime startTime;
        private string FavGamesFN = Path.Combine(Environment.CurrentDirectory, "FavoriteGames.json");
        private delegate void SafeCallDelegateFavorite(FavoriteGame game);
        public static List<ServerData> servers = new List<ServerData>();
        public static long CurrentPlaceID = 0;

        private void ServerList_Load(object sender, EventArgs e)
        {
            rbxclient = new RestClient("https://roblox.com/");
            rbxclient.CachePolicy = new System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.BypassCache);

            gamesclient = new RestClient("https://games.roblox.com/");
            gamesclient.CachePolicy = new System.Net.Cache.RequestCachePolicy(System.Net.Cache.RequestCacheLevel.BypassCache);

            startTime = DateTime.Now;

            if (File.Exists(FavGamesFN))
            {
                try
                {
                    Favorites = JsonConvert.DeserializeObject<List<FavoriteGame>>(File.ReadAllText(FavGamesFN));

                    FavoritesListView.SetObjects(Favorites);
                }
                catch (Exception x)
                {
                    MessageBox.Show("Failed to load favorite games!\n" + x);
                }
            }
            else
                Favorites = new List<FavoriteGame>();
        }

        private void ServerList_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }

        private void RefreshServers_Click(object sender, EventArgs e)
        {
            if (Busy || !Int64.TryParse(Program.MainForm.PlaceID.Text, out long PlaceId))
            {
                MessageBox.Show("Server List is currently busy", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (OtherPlaceId.Text.Length > 3 && Int64.TryParse(OtherPlaceId.Text, out long OPI))
                PlaceId = OPI;

            CurrentPlaceID = PlaceId;

            servers.Clear();
            ServerListView.Items.Clear();
            IRestResponse response;

            ServersInfo publicInfo = new ServersInfo();
            ServersInfo vipInfo = new ServersInfo();

            publicInfo.nextPageCursor = "";
            vipInfo.nextPageCursor = "";

            Task.Factory.StartNew(async () =>
            {
                Busy = true;

                while (publicInfo.nextPageCursor != null)
                {
                    RestRequest request = new RestRequest("v1/games/" + PlaceId + "/servers/public?sortOrder=Asc&limit=100" + (string.IsNullOrEmpty(publicInfo.nextPageCursor) ? "" : "&cursor=" + publicInfo.nextPageCursor), Method.GET);
                    response = await gamesclient.ExecuteAsync(request);

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        publicInfo = JsonConvert.DeserializeObject<ServersInfo>(response.Content);

                        List<ServerData> sservers = new List<ServerData>();

                        foreach (ServerData data in publicInfo.data)
                        {
                            data.type = "Public";
                            servers.Add(data);
                            sservers.Add(data);
                        }

                        ServerListView.AddObjects(sservers);
                    }
                }

                Busy = false;

                if (AccountManager.SelectedAccount != null)
                {
                    while (vipInfo.nextPageCursor != null)
                    {
                        RestRequest request = new RestRequest("v1/games/" + PlaceId + " /servers/VIP?sortOrder=Asc&limit=25" + (string.IsNullOrEmpty(vipInfo.nextPageCursor) ? "" : "&cursor=" + vipInfo.nextPageCursor), Method.GET);
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
            if (AccountManager.SelectedAccount == null) return;

            if (OtherPlaceId.Text.Length > 3)
            {
                string res = AccountManager.SelectedAccount.SetServer(Convert.ToInt64(OtherPlaceId.Text), ServerListView.SelectedItem.Text);

                if (!res.Contains("Success"))
                    MessageBox.Show(res);
                else
                    Console.Beep();
            }
            else
            {
                string res = AccountManager.SelectedAccount.JoinServer(Convert.ToInt64(Program.MainForm.PlaceID.Text), ServerListView.SelectedItem.Text, false, false);

                if (!res.Contains("Success"))
                    MessageBox.Show(res);
            }
        }

        private void SearchPlayer_Click(object sender, EventArgs e)
        {
            if (Busy)
            {
                MessageBox.Show("Server List is currently busy", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (AccountManager.SelectedAccount == null)
            {
                MessageBox.Show("Select an account on the main form", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            long UserID = AccountManager.GetUserID(Username.Text);

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

            if (Busy || !Int64.TryParse(Program.MainForm.PlaceID.Text, out long PlaceId)) return;

            if (OtherPlaceId.Text.Length > 3 && Int64.TryParse(OtherPlaceId.Text, out long OPI))
                PlaceId = OPI;

            Avatar avatar = JsonConvert.DeserializeObject<Avatar>(response.Content);
            int index = 0;

            Console.WriteLine(avatar.Url);

            request = new RestRequest("games/getgameinstancesjson?placeId=" + PlaceId + "&startIndex=" + index.ToString());
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
                if (t.CurrentPlayers.Find(p => p.Thumbnail.Url == avatar.Url) != null)
                {
                    UserFound = t.Guid;
                    serverData.id = t.Guid;
                    serverData.playing = t.CurrentPlayers.Count;
                    serverData.ping = t.Ping;
                    serverData.fps = t.Fps;
                }
            }

            request = new RestRequest("games/getgameinstancesjson?placeId=" + PlaceId + "&startIndex=0");
            request.AddCookie(".ROBLOSECURITY", token);
            request.AddHeader("Host", "www.roblox.com");
            response = rbxclient.Execute(request);

            if (response.StatusCode != HttpStatusCode.OK) return;

            instances = JsonConvert.DeserializeObject<GameInstancesCollection>(response.Content);

            if (instances == null) return;

            Busy = true;

            for (int i = 0; i < instances.TotalCollectionSize; i += 50)
            { // wow this search is so shittily made
                int startIndex = i;
                int tindex = startIndex;
                bool FirstTime = true;
                GameInstancesCollection tinstances = instances;

                Task.Run(() =>
                {
                    while ((tinstances.Collection != null && tinstances.Collection.Count != 0) || FirstTime)
                    {
                        if (!string.IsNullOrEmpty(UserFound) || tindex >= startIndex + 50) break;

                        request = new RestRequest("games/getgameinstancesjson?placeId=" + PlaceId + "&startIndex=" + tindex.ToString());
                        request.AddCookie(".ROBLOSECURITY", token);
                        request.AddHeader("Host", "www.roblox.com");
                        response = rbxclient.Execute(request);

                        Console.WriteLine(response.Content);
                        tinstances = JsonConvert.DeserializeObject<GameInstancesCollection>(response.Content);

                        FirstTime = false;

                        tindex += 10;

                        if (tinstances == null || tinstances.Collection == null) break;

                        foreach (GameInstance t in tinstances.Collection)
                        {
                            if (t.CurrentPlayers.Find(p => p.Thumbnail.Url == avatar.Url) != null)
                            {
                                UserFound = t.Guid;
                                serverData.id = t.Guid;
                                serverData.playing = t.CurrentPlayers.Count;
                                serverData.ping = t.Ping;
                                serverData.fps = t.Fps;
                            }
                        }
                    }

                    if (!string.IsNullOrEmpty(UserFound))
                    {
                        ServerListView.ClearObjects();
                        ServerListView.SetObjects(new List<ServerData> { serverData });
                    }

                    Busy = false;
                });
            }
        }

        private void copyJobIdToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ServerListView.SelectedItem != null)
                Clipboard.SetText(ServerListView.SelectedItem.Text);
        }

        private void Search_Click(object sender, EventArgs e)
        {
            if (Busy || !Int32.TryParse(PageNum.Text, out Page)) return;

            IRestResponse response;
            GamesListView.ClearObjects();

            Task.Factory.StartNew(() =>
            {
                RestRequest request = new RestRequest($"https://games.roblox.com/v1/games/list?model.keyword={Term.Text}&model.startRows={Page * 50}&model.maxRows=50", Method.GET);

                response = gamesclient.Execute(request);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    GameList gamesList = JsonConvert.DeserializeObject<GameList>(response.Content);

                    foreach (Game game in gamesList.games)
                    {
                        int LikeRatio = 0;

                        if (game.totalUpVotes > 0)
                        {
                            double totalVotes = game.totalUpVotes + game.totalDownVotes;
                            LikeRatio = (int)((decimal)(game.totalUpVotes / totalVotes) * 100);
                        }

                        GamesListView.AddObject(new ListGame(game.name, game.playerCount, LikeRatio, game.placeId));
                    }
                }

                Busy = false;
            });
        }

        private void GamesListView_MouseClick(object sender, MouseEventArgs e)
        {
            ListGame game = GamesListView.SelectedObject as ListGame;

            if (game != null)
                Program.MainForm.PlaceID.Text = game.placeId.ToString();
        }

        private void FavoritesListView_MouseClick(object sender, MouseEventArgs e)
        {
            FavoriteGame game = FavoritesListView.SelectedObject as FavoriteGame;

            if (game != null)
            {
                Program.MainForm.PlaceID.Text = game.PlaceID.ToString();
                Program.MainForm.JobID.Text = !string.IsNullOrEmpty(game.PrivateServer) ? game.PrivateServer.ToString() : "";
            }
        }

        private void SaveFavorites()
        {
            if ((DateTime.Now - startTime).Seconds < 5 || Favorites.Count == 0) return;

            string SaveData = JsonConvert.SerializeObject(Favorites);

            File.WriteAllText(FavGamesFN, SaveData);
        }

        public void AddFavoriteToList(FavoriteGame game)
        {
            if (FavoritesListView.InvokeRequired)
            {
                var addItem = new SafeCallDelegateFavorite(AddFavoriteToList);
                FavoritesListView.Invoke(addItem, new object[] { game });
            }
            else
            {
                if (string.IsNullOrEmpty(game.PrivateServer) && Favorites.Find(x => x.PlaceID == game.PlaceID) != null) return;

                Favorites.Add(game);
                FavoritesListView.AddObject(game);
            }
        }

        private void joinGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListGame game = GamesListView.SelectedObject as ListGame;

            if (game != null)
            {
                if (AccountManager.SelectedAccount == null) return;

                string res = AccountManager.SelectedAccount.JoinServer(game.placeId);

                if (!res.Contains("Success"))
                    MessageBox.Show(res);
            }
        }

        private void addToFavoritesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListGame game = GamesListView.SelectedObject as ListGame;

            if (game != null)
            {
                AddFavoriteToList(new FavoriteGame(game.name, game.placeId));
                SaveFavorites();
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FavoriteGame game = FavoritesListView.SelectedObject as FavoriteGame;

            if (game != null)
            {
                if (AccountManager.SelectedAccount == null) return;

                string res;

                if (string.IsNullOrEmpty(game.PrivateServer))
                    res = AccountManager.SelectedAccount.JoinServer(game.PlaceID);
                else
                    res = AccountManager.SelectedAccount.JoinServer(game.PlaceID, game.PrivateServer);

                if (res != "Success")
                    MessageBox.Show(res);
            }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            FavoriteGame game = FavoritesListView.SelectedObject as FavoriteGame;

            if (game != null)
            {
                string Result = AccountManager.ShowDialog("Rename", "Name");

                if (!string.IsNullOrEmpty(Result))
                {
                    game.Name = Result;
                    FavoritesListView.SelectedItem.SubItems[0].Text = Result;
                    SaveFavorites();
                }
            }
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FavoriteGame game = FavoritesListView.SelectedObject as FavoriteGame;

            if (game != null)
            {
                DialogResult res = MessageBox.Show("Are you sure?", "Remove Favorite", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (res == DialogResult.Yes)
                {
                    FavoritesListView.Items.Remove(FavoritesListView.SelectedItem);
                    Favorites.Remove(game);
                    SaveFavorites();
                }
            }
        }

        private void Favorite_Click(object sender, EventArgs e)
        {
            string PlaceId = AccountManager.CurrentPlaceId;

            if (!string.IsNullOrEmpty(AccountManager.CurrentJobId) && AccountManager.CurrentJobId.Contains("privateServerLinkCode") && Regex.IsMatch(AccountManager.CurrentJobId, @"\/games\/(\d+)\/"))
                PlaceId = Regex.Match(AccountManager.CurrentJobId, @"\/games\/(\d+)\/").Groups[1].Value;

            RestRequest request = new RestRequest("Marketplace/ProductInfo?assetId=" + PlaceId, Method.GET);
            request.AddHeader("Accept", "application/json");
            IRestResponse response = AccountManager.APIClient.Execute(request);

            if (response.IsSuccessful && response.StatusCode == HttpStatusCode.OK)
            {
                ProductInfo placeInfo = JsonConvert.DeserializeObject<ProductInfo>(response.Content);

                if (!string.IsNullOrEmpty(AccountManager.CurrentJobId) && AccountManager.CurrentJobId.Contains("privateServerLinkCode"))
                    AddFavoriteToList(new FavoriteGame(placeInfo.Name + " (VIP)", Convert.ToInt64(AccountManager.CurrentPlaceId), AccountManager.CurrentJobId));
                else
                    AddFavoriteToList(new FavoriteGame(placeInfo.Name, Convert.ToInt64(AccountManager.CurrentPlaceId)));

                SaveFavorites();
            }
        }

        private void ServerList_HelpButtonClicked(object sender, CancelEventArgs e) =>
            MessageBox.Show("Some elements may have tooltips, hover over them for about 2 seconds to see instructions.", "Roblox Account Manager", MessageBoxButtons.OK, MessageBoxIcon.Information);

        private void copyPlaceIDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FavoriteGame game = FavoritesListView.SelectedObject as FavoriteGame;

            if (game != null)
                Clipboard.SetText(game.PlaceID.ToString());
        }
    }
}
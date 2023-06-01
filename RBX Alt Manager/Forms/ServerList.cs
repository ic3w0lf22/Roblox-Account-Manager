#pragma warning disable CS4014

using BrightIdeasSoftware;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RBX_Alt_Manager.Classes;
using RBX_Alt_Manager.Forms;
using RBX_Alt_Manager.Properties;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using static RBX_Alt_Manager.Program;

namespace RBX_Alt_Manager
{
    public partial class ServerList : Form
    {
        public ServerList()
        {
            AccountManager.SetDarkBar(Handle);

            InitializeComponent();
            this.Rescale();

            if (ThemeEditor.UseDarkTopBar) Icon = Properties.Resources.server_icon_white;

            RobloxClient = new RestClient("https://roblox.com/");
            ThumbClient = new RestClient("https://thumbnails.roblox.com/");
            GamesClient = new RestClient("https://games.roblox.com/");
            DevelopClient = new RestClient("https://develop.roblox.com/");

            if (!AccountManager.Watcher.Exists("VerifyDataModel")) AccountManager.Watcher.Set("VerifyDataModel", "true");
            if (!AccountManager.Watcher.Exists("IgnoreExistingProcesses")) AccountManager.Watcher.Set("IgnoreExistingProcesses", "true");
            if (!AccountManager.Watcher.Exists("ExpectedWindowTitle")) AccountManager.Watcher.Set("ExpectedWindowTitle", "Roblox");

            RobloxScannerCB.Checked = AccountManager.Watcher.Get<bool>("Enabled");
            ExitIfBetaDetectedCB.Checked = AccountManager.Watcher.Get<bool>("ExitOnBeta");
            ExitIfNoConnectionCB.Checked = AccountManager.Watcher.Get<bool>("ExitIfNoConnection");
            SaveWindowPositionsCB.Checked = AccountManager.Watcher.Get<bool>("SaveWindowPositions");
            VerifyDataModelCB.Checked = AccountManager.Watcher.Get<bool>("VerifyDataModel");
            IgnoreExistingProcesses.Checked = AccountManager.Watcher.Get<bool>("IgnoreExistingProcesses");
            CloseRbxWindowTitleCB.Checked = AccountManager.Watcher.Get<bool>("CloseRbxWindowTitle");
            RbxMemoryCB.Checked = AccountManager.Watcher.Get<bool>("CloseRbxMemory");

            RbxWindowNameTB.Text = AccountManager.Watcher.Get<string>("ExpectedWindowTitle");

            RbxMemoryLTNum.Value = AccountManager.Watcher.Exists("MemoryLowValue") ? Utilities.Clamp(AccountManager.Watcher.Get<decimal>("MemoryLowValue"), RbxMemoryLTNum.Minimum, RbxMemoryLTNum.Maximum) : 200;
            TimeoutNum.Value = AccountManager.Watcher.Exists("NoConnectionTimeout") ? Utilities.Clamp(AccountManager.Watcher.Get<decimal>("NoConnectionTimeout"), TimeoutNum.Minimum, TimeoutNum.Maximum) : 60;

            ScanIntervalN.Value = AccountManager.Watcher.Exists("ScanInterval") ? AccountManager.Watcher.Get<int>("ScanInterval") : 6;
            ReadIntervalN.Value = AccountManager.Watcher.Exists("ReadInterval") ? AccountManager.Watcher.Get<int>("ReadInterval") : 250;
        }

        #region Themes

        public void ApplyTheme()
        {
            BackColor = ThemeEditor.FormsBackground;
            ForeColor = ThemeEditor.FormsForeground;

            ApplyTheme(Controls);
            //ApplyTheme(Tabs.TabPages);
        }

        public void ApplyTheme(Control.ControlCollection _Controls)
        {
            foreach (Control control in _Controls)
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
                else if (control is TextBox || control is RichTextBox)
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
                else if (control is Label)
                {
                    control.BackColor = ThemeEditor.LabelTransparent ? Color.Transparent : ThemeEditor.LabelBackground;
                    control.ForeColor = ThemeEditor.LabelForeground;
                }
                else if (control is ListBox || control is ObjectListView)
                {
                    if (control is ObjectListView view) view.HeaderStyle = ThemeEditor.ShowHeaders ? ColumnHeaderStyle.Clickable : ColumnHeaderStyle.None;
                    control.BackColor = ThemeEditor.ButtonsBackground;
                    control.ForeColor = ThemeEditor.ButtonsForeground;
                }
                else if (control is TabPage)
                {
                    ApplyTheme(control.Controls);

                    control.BackColor = ThemeEditor.ButtonsBackground;
                    control.ForeColor = ThemeEditor.ButtonsForeground;
                }
                else if (control is FastColoredTextBoxNS.FastColoredTextBox)
                    control.ForeColor = Color.Black;
                else if (control is FlowLayoutPanel || control is Panel || control is TabControl)
                    ApplyTheme(control.Controls);
            }
        }

        #endregion

        public static RestClient RobloxClient;
        public static RestClient ThumbClient;
        public static RestClient DevelopClient;
        public static RestClient GamesClient;
        private int Page = 0;
        private List<FavoriteGame> Favorites;
        private readonly string FavGamesFN = Path.Combine(Environment.CurrentDirectory, "FavoriteGames.json");
        public static List<ServerData> servers = new List<ServerData>();
        public static long CurrentPlaceID = 0;
        private readonly object RLLock = new object();
        private readonly object SaveLock = new object();

        private bool IsBusy;
        private readonly Dictionary<int, string> Errors = new Dictionary<int, string>
        {
            { 6, "Server Full" },
            { 11, "Server no longer available" },
            { 12, "No Access" }
        };
        private Dictionary<FavoriteGame, GameControl> FavoriteControls = new Dictionary<FavoriteGame, GameControl>(); // NOTE: dont forget to remove from dict when a favorite is removed

        public bool Busy
        {
            get => IsBusy;
            set
            {
                IsBusy = value;
                Utilities.InvokeIfRequired(RefreshServers, () => { RefreshServers.Text = value ? "Cancel" : "Refresh"; });
            }
        }

        private void ServerList_Load(object sender, EventArgs e)
        {
            if (File.Exists(FavGamesFN))
            {
                try
                {
                    Favorites = JsonConvert.DeserializeObject<List<FavoriteGame>>(File.ReadAllText(FavGamesFN));

                    FavoritesListView.SetObjects(Favorites);

                    foreach (FavoriteGame game in Favorites)
                        AddFavoriteControl(game);
                }
                catch (Exception x)
                {
                    MessageBox.Show("Failed to load favorite games!\n" + x);
                }
            }
            else
                Favorites = new List<FavoriteGame>();

            foreach (PropertyInfo prop in typeof(PageGame).GetProperties()) // was 2lazy to add em all manually : |
            {
                if (GamesListView.AllColumns.Find(x => x.AspectName == prop.Name) == null)
                    GamesListView.AllColumns.Add(new OLVColumn
                    {
                        Text = prop.Name,
                        AspectName = prop.Name,
                        IsVisible = false
                    });
            }
        }

        private void ServerList_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            Hide();
        }

        private void RefreshServers_Click(object sender, EventArgs e)
        {
            if (!long.TryParse(AccountManager.Instance.PlaceID.Text, out long PlaceId))
                return;

            if (Busy)
            {
                Busy = false;

                return;
            }

            if (OtherPlaceId.Text.Length > 3 && Int64.TryParse(OtherPlaceId.Text, out long OPI))
                PlaceId = OPI;

            CurrentPlaceID = PlaceId;

            servers.Clear();
            ServerListView.Items.Clear();
            RestResponse response;

            ServersInfo publicInfo = new ServersInfo();
            ServersInfo vipInfo = new ServersInfo();

            Task.Factory.StartNew(async () =>
            {
                Busy = true;

                while (publicInfo.nextPageCursor != null && Busy)
                {
                    RestRequest request = new RestRequest("v1/games/" + PlaceId + "/servers/public?sortOrder=Asc&limit=100" + (publicInfo.nextPageCursor == "_" ? "" : "&cursor=" + publicInfo.nextPageCursor), Method.Get);
                    response = await GamesClient.ExecuteAsync(request);

                    if (response.StatusCode == HttpStatusCode.OK && Busy)
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
                        RestRequest request = AccountManager.SelectedAccount.MakeRequest("v1/games/" + PlaceId + "/servers/VIP?sortOrder=Asc&limit=25" + (vipInfo.nextPageCursor == "_" ? "" : "&cursor=" + vipInfo.nextPageCursor), Method.Get).AddHeader("Accept", "application/json");
                        response = GamesClient.Execute(request);

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
            if (ServerListView.SelectedObject is ServerData server)
                AccountManager.Instance.JobID.Text = server.type == "VIP" ? "VIP:" + server.accessCode : server.id;
        }

        private async void joinServerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (AccountManager.SelectedAccount == null) return;

            if (OtherPlaceId.Text.Length > 3)
            {
                string res = AccountManager.SelectedAccount.SetServer(Convert.ToInt64(OtherPlaceId.Text), ServerListView.SelectedItem.Text, out bool Success);

                if (!Success)
                    MessageBox.Show(res);
                else
                    Console.Beep();
            }
            else
            {
                string res = await AccountManager.SelectedAccount.JoinServer(Convert.ToInt64(AccountManager.Instance.PlaceID.Text), ServerListView.SelectedItem.Text, false, false);

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

            if (!AccountManager.GetUserID(Username.Text, out long UserID, out _))
            {
                MessageBox.Show($"Failed to get UserID of {Username.Text}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            RestRequest avrequest = new RestRequest($"v1/users/avatar-headshot?size=48x48&format=png&userIds={UserID}", Method.Get);

            RestResponse avresponse = ThumbClient.Execute(avrequest);

            if (avresponse.StatusCode != HttpStatusCode.OK)
            {
                MessageBox.Show($"Failed to get AvatarUrl of {Username.Text}\n\n{avresponse.StatusCode}: {avresponse.Content}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (Busy || !Int64.TryParse(AccountManager.Instance.PlaceID.Text, out long PlaceId)) return;

            if (OtherPlaceId.Text.Length > 3 && Int64.TryParse(OtherPlaceId.Text, out long OPI))
                PlaceId = OPI;

            AvatarRoot avatarData = JsonConvert.DeserializeObject<AvatarRoot>(avresponse.Content);

            if (avatarData == null || avatarData.data.Count == 0) return;

            Avatar avatar = avatarData.data[0];

            ServersInfo publicInfo = new ServersInfo();
            ServersInfo vipInfo = new ServersInfo();

            Task.Factory.StartNew(async () =>
            {
                Busy = true;

                while (!string.IsNullOrEmpty(publicInfo.nextPageCursor) && Busy)
                {
                    RestRequest request = new RestRequest("v1/games/" + PlaceId + "/servers/public?limit=100" + (publicInfo.nextPageCursor == "_" ? "" : "&cursor=" + publicInfo.nextPageCursor), Method.Get);

                    RestResponse response = await GamesClient.ExecuteAsync(request);

                    if (response.StatusCode == HttpStatusCode.OK && Busy)
                    {
                        publicInfo = JsonConvert.DeserializeObject<ServersInfo>(response.Content);

                        foreach (ServerData server in publicInfo.data)
                        {
                            if (!Busy) break;

                            RestRequest batchRequest = new RestRequest("v1/batch", Method.Post);

                            batchRequest.AddJsonBody(server.playerTokens.ConvertAll(s => new TokenRequest(s)));

                            RestResponse batchResponse = await ThumbClient.ExecuteAsync(batchRequest);

                            TokenAvatarRoot avatars = JsonConvert.DeserializeObject<TokenAvatarRoot>(batchResponse.Content);

                            if (avatars.data.Exists(x => x.imageUrl == avatar.imageUrl))
                            {
                                Busy = false;
                                ServerListView.ClearObjects();
                                ServerListView.SetObjects(new List<ServerData> { server });
                            }
                        }

                        await Task.Delay(750);
                    }
                }

                Busy = false;
            });
        }

        private void copyJobIdToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ServerListView.SelectedItem != null)
                Clipboard.SetText(ServerListView.SelectedItem.Text);
        }

        private async void Search_Click(object sender, EventArgs e)
        {
            if (Busy || !int.TryParse(PageNum.Text, out Page)) return;

            GamesListView.ClearObjects();
            GameListPanel.Controls.Clear();

            bool NoTerm = string.IsNullOrEmpty(Term.Text);
            RestResponse Response = null;

            if (NoTerm)
                Response = await GamesClient.ExecuteAsync(new RestRequest(string.Format("v1/games/list?model.startRows={0}&model.maxRows=50", Page * 50), Method.Get));
            else
                Response = await AccountManager.MainClient.ExecuteAsync(new RestRequest($"games/list-json?keyword={Term.Text}&startRows={Page * 40}&maxRows=40", Method.Get));

            lock (RLLock)
            {
                List<GameControl> GControls = new List<GameControl>();

                if (Response.StatusCode == HttpStatusCode.OK)
                {
                    List<PageGame> gamesList = NoTerm ? new List<PageGame>() : JsonConvert.DeserializeObject<List<PageGame>>(Response.Content);

                    if (NoTerm)
                    {
                        dynamic Result = JObject.Parse(Response.Content);

                        foreach (dynamic game in Result?.games)
                            gamesList.Add(new PageGame((long)game.placeId, (string)game.name));
                    }

                    foreach (PageGame game in gamesList)
                    {
                        int LikeRatio = 0;

                        if (game.TotalUpVotes > 0)
                        {
                            double totalVotes = game.TotalUpVotes + game.TotalDownVotes;
                            LikeRatio = (int)((decimal)(game.TotalUpVotes / totalVotes) * 100);
                        }

                        game.LikeRatio = LikeRatio;

                        GamesListView.AddObject(game);

                        GameControl RControl = new GameControl(game);

                        RControl.Selected += (s, args) => AccountManager.Instance.PlaceID.Text = $"{args.Game.Details?.placeId}";

                        GControls.Add(RControl);
                    }
                }

                if (GControls != null && GControls.Count > 0)
                    GameListPanel.InvokeIfRequired(() => GameListPanel.Controls.AddRange(GControls.ToArray()));

                Busy = false;
            }
        }

        private void Term_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
                Search.PerformClick();
        }

        private void GamesListView_MouseClick(object sender, MouseEventArgs e)
        {
            if (GamesListView.SelectedObject is PageGame game)
                AccountManager.Instance.PlaceID.Text = game.PlaceID.ToString();
        }

        private void FavoritesListView_MouseClick(object sender, MouseEventArgs e)
        {
            if (FavoritesListView.SelectedObject is FavoriteGame game)
            {
                AccountManager.Instance.PlaceID.Text = game.Details.placeId.ToString();
                AccountManager.Instance.JobID.Text = !string.IsNullOrEmpty(game.PrivateServer) ? game.PrivateServer.ToString() : "";
            }
        }

        private void SaveFavorites()
        {
            Logger.Info($"Removing {Favorites.Where(x => x.Details == null).Count()} favorite games because they have no GameDetails available!");

            if (Favorites.Count == 0) return;

            Favorites.RemoveAll(f => f.Details == null); // Past favorite games' GameDetails are cached anyways so no need to worry about this

            lock (SaveLock)
                File.WriteAllText(FavGamesFN, JsonConvert.SerializeObject(Favorites));
        }

        public void AddFavoriteToList(FavoriteGame game)
        {
            this.InvokeIfRequired(() =>
            {
                if (string.IsNullOrEmpty(game.PrivateServer) && Favorites.Find(x => x.Details?.placeId == game.Details?.placeId) != null) return;

                Favorites.Add(game);
                FavoritesListView.AddObject(game);

                AddFavoriteControl(game);
            });
        }

        private void AddFavoriteControl(FavoriteGame game)
        {
            GameControl RControl = new GameControl(game) { Favorite = game };

            if (game.Details != null) game.Details.name = game.Name;

            RControl.Selected += (s, args) =>
            {
                AccountManager.Instance.PlaceID.Text = $"{args.Game.Details.placeId}";
                AccountManager.Instance.JobID.Text = AccountManager.Instance.JobID.Text = !string.IsNullOrEmpty(RControl.Favorite?.PrivateServer) ? RControl.Favorite?.PrivateServer : string.Empty;
            };

            RControl.SetContext(FavoritesStrip);

            FavoriteGamesPanel.Controls.Add(RControl);
            FavoriteControls.Add(game, RControl);
        }

        private async void joinGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (GamesListView.SelectedObject is PageGame game)
            {
                if (AccountManager.SelectedAccount == null) return;

                string res = await AccountManager.SelectedAccount.JoinServer(game.PlaceID);

                if (!res.Contains("Success"))
                    MessageBox.Show(res);
            }
        }

        private void addToFavoritesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (GamesListView.SelectedObject is PageGame game)
                AddFavoriteToList(new FavoriteGame(game.Name, game.PlaceID, SaveFavorites));
        }

        private async void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (((ToolStripMenuItem)sender).GetSource()?.Parent is GameControl gc && (gc?.Favorite ?? FavoritesListView?.SelectedObject) is FavoriteGame game)
            {
                if (AccountManager.SelectedAccount == null) return;

                string res;

                if (string.IsNullOrEmpty(game.PrivateServer))
                    res = await AccountManager.SelectedAccount.JoinServer(game.Details.placeId);
                else
                    res = await AccountManager.SelectedAccount.JoinServer(game.Details.placeId, game.PrivateServer);

                if (res != "Success")
                    MessageBox.Show(res);
            }
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (((ToolStripMenuItem)sender).GetSource()?.Parent is GameControl gc && (gc?.Favorite ?? FavoritesListView?.SelectedObject) is FavoriteGame game)
            {
                string Result = AccountManager.ShowDialog("Rename", "Name");

                if (!string.IsNullOrEmpty(Result))
                {
                    game.Name = Result;

                    SaveFavorites();

                    if (FavoriteControls.TryGetValue(game, out GameControl GC)) GC.Rename(Result);
                }
            }
        }

        private void removeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (((ToolStripMenuItem)sender).GetSource()?.Parent is GameControl gc && (gc?.Favorite ?? FavoritesListView?.SelectedObject) is FavoriteGame game)
            {
                DialogResult res = MessageBox.Show("Are you sure?", "Remove Favorite", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (res == DialogResult.Yes)
                {
                    FavoritesListView.RemoveObject(game);
                    Favorites.Remove(game);
                    SaveFavorites();

                    if (FavoriteControls.TryGetValue(game, out GameControl GC))
                    {
                        FavoriteGamesPanel.Controls.Remove(GC);
                        FavoriteControls.Remove(game);
                    }
                }
            }
        }

        private async void Favorite_Click(object sender, EventArgs e)
        {
            string PlaceId = AccountManager.CurrentPlaceId;

            if (!string.IsNullOrEmpty(AccountManager.CurrentJobId) && AccountManager.CurrentJobId.Contains("privateServerLinkCode") && Regex.IsMatch(AccountManager.CurrentJobId, @"\/games\/(\d+)\/"))
                PlaceId = Regex.Match(AccountManager.CurrentJobId, @"\/games\/(\d+)\/").Groups[1].Value;

            RestRequest request = new RestRequest($"v2/assets/{PlaceId}/details", Method.Get);
            request.AddHeader("Accept", "application/json");
            RestResponse response = await AccountManager.EconClient.ExecuteAsync(request);

            Logger.Info($"MarketResponse for {PlaceId}: [{response.StatusCode}] {response.Content}");

            if (response.IsSuccessful && response.StatusCode == HttpStatusCode.OK)
            {
                ProductInfo placeInfo = JsonConvert.DeserializeObject<ProductInfo>(response.Content);

                if (!string.IsNullOrEmpty(AccountManager.CurrentJobId) && AccountManager.CurrentJobId.Contains("privateServerLinkCode"))
                    AddFavoriteToList(new FavoriteGame($"{placeInfo.Name} (VIP)", Convert.ToInt64(AccountManager.CurrentPlaceId), AccountManager.CurrentJobId, SaveFavorites));
                else
                    AddFavoriteToList(new FavoriteGame(placeInfo.Name, Convert.ToInt64(AccountManager.CurrentPlaceId), SaveFavorites));
            }
            else
                MessageBox.Show($"[{response.StatusCode} {response.StatusDescription}]\n{response.Content}", $"Can't add {PlaceId} to favorites!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void copyPlaceIDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (((ToolStripMenuItem)sender).GetSource()?.Parent is GameControl gc && (gc?.Favorite ?? FavoritesListView?.SelectedObject) is FavoriteGame game)
                Clipboard.SetText(game.Details.placeId.ToString());
        }

        private void loadRegionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Account acc = AccountManager.SelectedAccount ?? AccountManager.LastValidAccount;

            if (acc == null) return;
            if (!acc.GetCSRFToken(out string Token)) return;

            int Index = ServerListView.SelectedIndex;
            List<ServerData> Servers = new List<ServerData>();

            for (int i = Index; i < Index + 16; i++) // only attempt to load 16. during testing, i was being ratelimited (?)
            {
                ServerData server = (ServerData)ServerListView.GetItem(i)?.RowObject;

                if (server == null) break;

                Servers.Add(server);

                if (server.regionLoaded) continue;

                server.region = "Loading";
            }

            ServerListView.RefreshObjects(Servers);

            Task.Run(async () =>
            {
                var WC = new WebClient();
                var RNG = new Random();
                var pinger = new Ping();
                int t = 0;
                bool IsUniversePlace = false;

                // Assume the PlaceId in the main window is the universe's lobby place id
                if (long.TryParse(AccountManager.Instance.PlaceID.Text, out long PlaceId))
                    IsUniversePlace = PlaceId != CurrentPlaceID;

                if (IsUniversePlace) await AccountManager.GameJoinClient.ExecuteAsync(acc.MakeRequest("v1/join-game", Method.Post).AddHeader("Content-Type", "application/json").AddJsonBody(new { placeId = PlaceId }));

                foreach (ServerData server in Servers)
                {
                pingServer:
                    if (t > 4)
                    {
                        if (!server.regionLoaded)
                            server.region = "Failed";

                        ServerListView.InvokeIfRequired(() => ServerListView.RefreshObject(server));

                        t = 0;

                        continue;
                    }

                    t++;

                    if (server.regionLoaded && !string.IsNullOrEmpty(server.ip))
                    {
                        PingReply reply = await pinger.SendPingAsync(server.ip, 400);

                        if (reply.Status == IPStatus.Success)
                        {
                            t = 0;

                            server.ping = Convert.ToInt32(reply.RoundtripTime);
                            ServerListView.InvokeIfRequired(() => ServerListView.RefreshObject(server));
                        }
                        else
                            goto pingServer;

                        continue;
                    }

                    RestRequest request = new RestRequest("v1/join-game-instance", Method.Post);
                    request.AddCookie(".ROBLOSECURITY", acc.SecurityToken, "/", ".roblox.com");
                    request.AddHeader("Content-Type", "application/json");

                    if (IsUniversePlace)
                        request.AddJsonBody(new { gameId = server.id, placeId = CurrentPlaceID, isTeleport = true });
                    else
                        request.AddJsonBody(new { gameId = server.id, placeId = CurrentPlaceID });

                    RestResponse response = await AccountManager.GameJoinClient.ExecuteAsync(request);

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        JObject JS = JObject.Parse(response.Content);
                        string IP = JS?["joinScript"]?.Value<JObject>()?["MachineAddress"]?.Value<string>() ?? string.Empty;

                        if (string.IsNullOrEmpty(IP))
                        {
                            if (!Errors.TryGetValue(JS?["status"]?.Value<int>() ?? 0, out string ErrorMessage))
                                ErrorMessage = JS?["status"]?.Value<string>() ?? "?";

                            server.region = $"{JS?["message"]?.Value<string>() ?? "Error:"} {ErrorMessage}";
                            ServerListView.InvokeIfRequired(() => ServerListView.RefreshObject(server));

                            continue;
                        }

                        int N2 = RNG.Next(32, 50);
                        WC.Headers["User-Agent"] = $"Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.{N2} (KHTML, like Gecko) Chrome/{string.Format("{0}.{1}.{2}.{3}", RNG.Next(93, 110), 0, RNG.Next(4950, 5162), RNG.Next(80, 212))} Safari/537.{N2}";

                        string IpJSON = await WC.DownloadStringTaskAsync(AccountManager.General.Get<string>("IPApiLink").Replace("<ip>", IP));
                        JObject IPJS = JObject.Parse(IpJSON);
                        string Format = AccountManager.General.Get<string>("ServerRegionFormat");

                        Logger.Info($"IP API Response: {IPJS}");

                        IPJS.Add("address", IP);
                        IPJS.Add("port", JS?["joinScript"]?.Value<JObject>()?["ServerPort"]?.Value<string>() ?? string.Empty);

                        if (string.IsNullOrEmpty(Format)) Format = "<city>, <region_code>";

                        string Region = Format;

                        foreach (Match m in Regex.Matches(Format, @"<(\w+)>"))
                            Region = Region.Replace(m.Value, IPJS?[m.Groups[1].Value]?.Value<string>() ?? "?");

                        server.ip = IP;
                        server.region = Region;
                        server.regionLoaded = true;

                        ServerListView.InvokeIfRequired(() => ServerListView.RefreshObject(server));

                        goto pingServer;
                    }
                    else
                        server.region = $"{response.StatusCode}";

                    ServerListView.InvokeIfRequired(() => ServerListView.RefreshObject(server));

                    t = 0;
                }

                pinger.Dispose();
            });
        }

        private void copyPlaceIdToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            List<long> Ids = new List<long>();

            foreach (PageGame game in GamesListView.SelectedObjects)
                Ids.Add(game.PlaceID);

            if (Ids.Count > 0)
                Clipboard.SetText(string.Join("\n", Ids));
        }

        private void GetUniverseID_Click(object sender, EventArgs e)
        {
            Account acc = AccountManager.LastValidAccount ?? AccountManager.SelectedAccount;

            if (acc == null) return;

            RestRequest DetailsReq = acc.MakeRequest($"v1/games/multiget-place-details?placeIds={PlaceIDUniTB.Text}");

            RestResponse DetailsResp = GamesClient.Execute(DetailsReq);

            if (DetailsResp.IsSuccessful)
            {
                var Details = JArray.Parse(DetailsResp.Content);

                if (Details.HasValues)
                    UniverseIDTB.Text = Details[0]?["universeId"]?.Value<string>() ?? UniverseIDTB.Text;
            }
        }

        private void ViewUniverse_Click(object sender, EventArgs e)
        {
            UniverseGamesPanel.SuspendLayout();
            UniverseGamesPanel.Controls.Clear();

            GetUniversePlaces(UniverseIDTB.Text, (UniverseData) =>
            {
                foreach (var Place in UniverseData["data"])
                {
                    GameControl RControl = new GameControl(new Game(Place["id"].Value<long>(), Place["name"].Value<string>()));

                    RControl.Selected += (s, args) => AccountManager.Instance.PlaceID.Text = $"{args.Game.Details.placeId}";

                    UniverseGamesPanel.Controls.Add(RControl);
                }

                UniverseGamesPanel.ResumeLayout();
            });
        }

        private async Task GetUniversePlaces(string UniverseId, Action<JObject> Callback, string Cursor = "")
        {
            RestRequest DetailsReq = new RestRequest($"v1/universes/{UniverseIDTB.Text}/places?sortOrder=Asc&limit=100&cursor={Cursor}");

            RestResponse DetailsResp = await DevelopClient.ExecuteAsync(DetailsReq);

            if (DetailsResp.IsSuccessful)
            {
                var Details = JObject.Parse(DetailsResp.Content);

                if (Details.ContainsKey("data"))
                    Callback(Details);

                if (Details.ContainsKey("nextPageCursor") && !string.IsNullOrEmpty(Details["nextPageCursor"].Value<string>()))
                    GetUniversePlaces(UniverseId, Callback, Details["nextPageCursor"].Value<string>());
            }
        }

        private void ListViewCB_CheckedChanged(object sender, EventArgs e)
        {
            GameListPanel.Visible = !ListViewCB.Checked;
            GamesListView.Visible = ListViewCB.Checked;
        }

        private void FavoriteListViewCB_CheckedChanged(object sender, EventArgs e)
        {
            FavoriteGamesPanel.Visible = !FavoriteListViewCB.Checked;
            FavoritesListView.Visible = FavoriteListViewCB.Checked;
        }

        private void RobloxScannerCB_CheckedChanged(object sender, EventArgs e)
        {
            if (!RobloxScannerCB.Checked)
            {
                WatcherTimer.Enabled = false;
                RobloxWatcher.ReadTimer.Enabled = false;

                AccountManager.Watcher.Set("Enabled", "false");
                AccountManager.IniSettings.Save("RAMSettings.ini");

                return;
            }

            if (!File.Exists(RobloxWatcher.HandlePath))
                File.WriteAllBytes(RobloxWatcher.HandlePath, Resources.handle);

            if (!RobloxWatcher.IsHandleEulaAccepted() && Utilities.YesNoPrompt("Roblox Watcher", "This feature uses SysInternals Handle which requires you to accept a EULA.", "Do you want to continue?"))
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = RobloxWatcher.HandlePath,
                    Arguments = "-p 99999",
                    UseShellExecute = false,
                }).WaitForExit();
            }

            if (RobloxWatcher.IsHandleEulaAccepted())
            {
                WatcherTimer.Enabled = RobloxScannerCB.Checked;

                RobloxWatcher.ReadTimer.Enabled = true;
            }
            else
                RobloxScannerCB.Checked = false;

            AccountManager.Watcher.Set("Enabled", RobloxScannerCB.Checked ? "true" : "false");
            AccountManager.IniSettings.Save("RAMSettings.ini");
        }

        private void ScanIntervalN_ValueChanged(object sender, EventArgs e)
        {
            WatcherTimer.Interval = (int)(ScanIntervalN.Value * 1000);

            AccountManager.Watcher.Set("ScanInterval", ScanIntervalN.Value.ToString());
            AccountManager.IniSettings.Save("RAMSettings.ini");
        }

        private void ReadIntervalN_ValueChanged(object sender, EventArgs e)
        {
            RobloxWatcher.ReadTimer.Interval = (double)Math.Max(ReadIntervalN.Value, 50);

            AccountManager.Watcher.Set("ReadInterval", ReadIntervalN.Value.ToString());
            AccountManager.IniSettings.Save("RAMSettings.ini");
        }

        private void WatcherTimer_Tick(object sender, EventArgs e) => RobloxWatcher.CheckProcesses();

        private void ExitIfBetaDetectedCB_CheckedChanged(object sender, EventArgs e)
        {
            AccountManager.Watcher.Set("ExitOnBeta", ExitIfBetaDetectedCB.Checked ? "true" : "false");
            AccountManager.IniSettings.Save("RAMSettings.ini");
        }

        private void ExitIfNoConnectionCB_CheckedChanged(object sender, EventArgs e)
        {
            AccountManager.Watcher.Set("ExitIfNoConnection", ExitIfNoConnectionCB.Checked ? "true" : "false");
            AccountManager.IniSettings.Save("RAMSettings.ini");
        }

        private void TimeoutNum_ValueChanged(object sender, EventArgs e)
        {
            AccountManager.Watcher.Set("NoConnectionTimeout", TimeoutNum.Value.ToString());
            AccountManager.IniSettings.Save("RAMSettings.ini");
        }

        private async void ViewOutfits_Click(object sender, EventArgs e)
        {
            if (!AccountManager.GetUserID(OutfitUsernameTB.Text, out long UserId, out _)) return;

            OutfitsPanel.SuspendLayout();
            OutfitsPanel.Controls.Clear();

            RestRequest OutfitsRequest = new RestRequest($"v1/users/{UserId}/outfits?page=1&itemsPerPage=50"); // hoping the outfit limit is 50 so i dont have to go through multiple pages in the future

            RestResponse DetailsResp = await AccountManager.AvatarClient.ExecuteAsync(OutfitsRequest);

            if (DetailsResp.IsSuccessful)
            {
                var Details = JObject.Parse(DetailsResp.Content);

                if (Details.ContainsKey("data"))
                    foreach (var Outfit in Details["data"])
                        OutfitsPanel.Controls.Add(new AvatarControl(Outfit["name"].Value<string>(), Outfit["id"].Value<long>()));
            }

            OutfitsPanel.ResumeLayout();
        }

        private void WearCustomButton_Click(object sender, EventArgs e)
        {
            if (AccountManager.SelectedAccount == null) return;

            string AvatarJSON = AccountManager.ShowDialog("Avatar JSON", "Wear Avatar");

            if (AvatarJSON.TryParseJson(out object _))
                AccountManager.SelectedAccount.SetAvatar(AvatarJSON);
        }

        private void SaveWindowPositionsCB_CheckedChanged(object sender, EventArgs e)
        {
            RobloxWatcher.RememberWindowPositions = SaveWindowPositionsCB.Checked;

            AccountManager.Watcher.Set("SaveWindowPositions", SaveWindowPositionsCB.Checked ? "true" : "false");
            AccountManager.IniSettings.Save("RAMSettings.ini");
        }

        private void VerifyDataModelCB_CheckedChanged(object sender, EventArgs e)
        {
            RobloxWatcher.VerifyDataModel = VerifyDataModelCB.Checked;

            AccountManager.Watcher.Set("VerifyDataModel", VerifyDataModelCB.Checked ? "true" : "false");
            AccountManager.IniSettings.Save("RAMSettings.ini");
        }

        private void IgnoreExistingProcesses_CheckedChanged(object sender, EventArgs e)
        {
            RobloxWatcher.IgnoreExistingProcesses = IgnoreExistingProcesses.Checked;

            AccountManager.Watcher.Set("IgnoreExistingProcesses", IgnoreExistingProcesses.Checked ? "true" : "false");
            AccountManager.IniSettings.Save("RAMSettings.ini");
        }

        private void RbxMemoryCB_CheckedChanged(object sender, EventArgs e)
        {
            RobloxWatcher.CloseIfMemoryLow = RbxMemoryCB.Checked;

            AccountManager.Watcher.Set("CloseRbxMemory", RbxMemoryCB.Checked ? "true" : "false");
            AccountManager.IniSettings.Save("RAMSettings.ini");
        }

        private void RbxMemoryLTNum_ValueChanged(object sender, EventArgs e)
        {
            RobloxWatcher.MemoryLowValue = (int)RbxMemoryLTNum.Value;

            AccountManager.Watcher.Set("MemoryLowValue", RbxMemoryLTNum.Value.ToString());
            AccountManager.IniSettings.Save("RAMSettings.ini");
        }

        private void CloseRbxWindowTitleCB_CheckedChanged(object sender, EventArgs e)
        {
            RobloxWatcher.CloseIfWindowTitle = CloseRbxWindowTitleCB.Checked;

            AccountManager.Watcher.Set("CloseRbxWindowTitle", CloseRbxWindowTitleCB.Checked ? "true" : "false");
            AccountManager.IniSettings.Save("RAMSettings.ini");
        }

        private void RbxWindowNameTB_TextChanged(object sender, EventArgs e)
        {
            RobloxWatcher.ExpectedWindowTitle = RbxWindowNameTB.Text;

            AccountManager.Watcher.Set("ExpectedWindowTitle", RbxWindowNameTB.Text);
            AccountManager.IniSettings.Save("RAMSettings.ini");
        }

        private void Tabs_SelectedIndexChanged(object sender, EventArgs e)
        {
            OpenLogsButton.Visible = Tabs.SelectedIndex == 5;

            if (Tabs.SelectedIndex == 5)
                OpenLogsButton.BringToFront();
        }

        private void OpenLogsButton_Click(object sender, EventArgs e) => Process.Start("explorer.exe", Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Roblox", "logs"));
    }
}
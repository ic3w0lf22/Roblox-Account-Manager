using RestSharp;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RBX_Alt_Manager.Classes
{
    public partial class GameControl : UserControl
    {
        public Game Game;
        public FavoriteGame Favorite;
        public event EventHandler<GameArgs> Selected;
        public event EventHandler<EventArgs> Exited;

        public GameControl(Game game)
        {
            InitializeComponent();
            GameName.Rescale();

            Game = game;

            if (!string.IsNullOrEmpty(Game.Details?.name)) GameName.Text = Game.Details?.name;

            ParentChanged += (s, e) => { if (Parent == null) Dispose(); };

            Task.Run(async () =>
            {
                await Game.WaitForDetails();

                if (Disposing) return;

                this.InvokeIfRequired(() => GameName.Text = Game.Details.name);
                
                GameImage.LoadAsync(Game.ImageUrl);
            });
        }

        public void Rename(string NewName) => GameName.Text = NewName;

        public void SetContext(ContextMenuStrip CMS)
        {
            GameName.ContextMenuStrip = CMS;
            GameImage.ContextMenuStrip = CMS;
        }

        private void MouseClicked(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                Selected?.Invoke(this, new GameArgs(Game));
        }

        private void Exit(Action action)
        {
            Exited?.Invoke(this, EventArgs.Empty);

            action.Invoke();
        }

        private void copyPlaceIdToolStripMenuItem_Click(object sender, EventArgs e) =>
            Exit(() => Clipboard.SetText(Game.Details.placeId.ToString()));

        private void copyNameToolStripMenuItem_Click(object sender, EventArgs e) =>
            Exit(() => Clipboard.SetText(Game.Details.name));

        private void copyPlaceLinkToolStripMenuItem_Click(object sender, EventArgs e) =>
            Exit(() => Clipboard.SetText($"https://www.roblox.com/games/{Game.Details.placeId}/-"));

        private void copyPlaceDetailsToolStripMenuItem_Click(object sender, EventArgs e) => Exit(() =>
        {
            if (AccountManager.LastValidAccount == null)
            {
                MessageBox.Show("Select a valid account then try again", "Roblox Account Manager", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            RestRequest DetailsRequest = new RestRequest($"v1/games/multiget-place-details?placeIds={Game.Details.placeId}");
            DetailsRequest.AddCookie(".ROBLOSECURITY", AccountManager.LastValidAccount?.SecurityToken, "/", ".roblox.com");

            Clipboard.SetText(ServerList.GamesClient.Execute(DetailsRequest).Content);
        });
    }
}
using Newtonsoft.Json;
using RBX_Alt_Manager.Classes;
using System;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RBX_Alt_Manager
{
    public class Game
    {
        public GameDetails Details;
        public string ImageUrl;
        [JsonIgnore] public Action Callback;

        public Game() { } // Default Constructor so that Details isn't overriden with Unknown when deserializing

        public Game(long PlaceId) : this(PlaceId, "Unknown") { }

        public Game(long PlaceId, string Name)
        {
            if (Batch.PlaceDetails.TryGetValue(PlaceId, out GameDetails ExistingDetails) && !string.IsNullOrEmpty(ImageUrl))
                Details = ExistingDetails;
            else
                Task.Run(async () =>
                {
                    ImageUrl = await Batch.GetGameIcon(PlaceId);

                    Details = Batch.PlaceDetails.ContainsKey(PlaceId) ? Batch.PlaceDetails[PlaceId] : (Details == null /* Recent/Favorite Games save the details, only updates if available */ ? GameDetails.Unknown(PlaceId, Name) : Details);
                });
        }

        public async Task WaitForDetails(int Delay = 60)
        {
            while (Details == null)
                await Task.Delay(Delay);

            Callback?.Invoke();
        }
    }

    public class GameDetails
    {
        private string _Name = "";

        public string filteredName;
        public string name
        {
            get => _Name;
            set
            {
                _Name = value;

                value = Regex.Replace(value, @"[\s]?\[[^]]*\][\s]?", "");
                value = Regex.Replace(value, "[^a-zA-Z0-9% ._]", string.Empty);

                filteredName = value;
            }
        }

        public long placeId;
        public string description;
        public string sourceName;
        public string sourceDescription;
        public string url;
        public string builder;
        public long builderId;
        public bool hasVerifiedBadge;
        public bool isPlayable;
        public string reasonProhibited;
        public long universeId;
        public long universeRootPlaceId;
        public long price;
        public string imageToken;

        public static GameDetails Unknown(long PlaceID = 0, string Name = "Unknown") => new GameDetails() { placeId = PlaceID, name = Name };
    }

    public class PageGame : Game
    {
        public long CreatorID { get; set; }
        public string CreatorName { get; set; }
        public string CreatorUrl { get; set; }
        public long Plays { get; set; }
        public long Price { get; set; }
        public long ProductID { get; set; }
        public bool IsOwned { get; set; }
        public bool IsVotingEnabled { get; set; }
        public long TotalUpVotes { get; set; }
        public long TotalDownVotes { get; set; }
        public long TotalBought { get; set; }
        public long UniverseID { get; set; }
        public bool HasErrorOcurred { get; set; }
        public string GameDetailReferralUrl { get; set; }
        public string Url { get; set; }
        public object RetryUrl { get; set; }
        public bool Final { get; set; }
        public string Name { get; set; }
        public long PlaceID { get; set; }
        public int PlayerCount { get; set; }
        public long ImageId { get; set; }
        public int? LikeRatio; // Doesn't actually exist in roblox's return value

        [JsonConstructor] public PageGame(long PlaceID, string Name) : base(PlaceID, Name) { this.PlaceID = PlaceID; this.Name = Name; }
    }

    public class FavoriteGame : Game
    {
        public string Name;
        public string PrivateServer;
        public long PlaceID;

        [JsonConstructor]
        public FavoriteGame(long PlaceID, Action Callback = null) : base(PlaceID)
        {
            Name = "???";
            PrivateServer = "";
            this.PlaceID = PlaceID;
            this.Callback = Callback;
        }

        public FavoriteGame(string Name, long PlaceID) : this(PlaceID) => this.Name = Name;
        public FavoriteGame(string Name, long PlaceID, Action Callback) : this(PlaceID, Callback) => this.Name = Name;
        public FavoriteGame(string Name, long PlaceID, string PrivateServer) : this(Name, PlaceID) => this.PrivateServer = PrivateServer;
        public FavoriteGame(string Name, long PlaceID, string PrivateServer, Action Callback) : this(Name, PlaceID, Callback) => this.PrivateServer = PrivateServer;
    }

    public class GameArgs : EventArgs
    {
        public Game Game { get; set; }

        public GameArgs(Game Game) => this.Game = Game;
    }
}
using Newtonsoft.Json;
using RBX_Alt_Manager.Classes;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RBX_Alt_Manager
{
    public class Game
    {
        public GameDetails Details;
        public string ImageUrl;

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
        public long creatorId { get; set; }
        public string creatorName { get; set; }
        public string creatorType { get; set; }
        public int totalUpVotes { get; set; }
        public int totalDownVotes { get; set; }
        public object universeId { get; set; }
        public string name { get; set; }
        public long placeId { get; set; }
        public int playerCount { get; set; }
        public string imageToken { get; set; }
        public bool isSponsored { get; set; }
        public string nativeAdData { get; set; }
        public bool isShowSponsoredLabel { get; set; }
        public int? price { get; set; }
        public object analyticsIdentifier { get; set; }
        public object gameDescription { get; set; }
        public int? likeRatio;

        [JsonConstructor] public PageGame(long placeId, string name) : base(placeId, name) { this.placeId = placeId; this.name = name; }
    }

    public class FavoriteGame : Game
    {
        public string Name;
        public string PrivateServer;
        public long PlaceID;

        [JsonConstructor]
        public FavoriteGame(long PlaceID) : base(PlaceID)
        {
            Name = "???";
            PrivateServer = "";
            this.PlaceID = PlaceID;
        }

        public FavoriteGame(string Name, long PlaceID) : this(PlaceID) =>
            this.Name = Name;

        public FavoriteGame(string Name, long PlaceID, string PrivateServer) : this(Name, PlaceID) =>
            this.PrivateServer = PrivateServer;
    }

    public class GameArgs : EventArgs
    {
        public Game Game { get; set; }

        public GameArgs(Game Game) => this.Game = Game;
    }

    public class GameList
    {
        public List<PageGame> games { get; set; }
        public object suggestedKeyword { get; set; }
        public object correctedKeyword { get; set; }
        public object filteredKeyword { get; set; }
        public bool hasMoreRows { get; set; }
        public object nextPageExclusiveStartId { get; set; }
        public object featuredSearchUniverseId { get; set; }
        public bool emphasis { get; set; }
        public object cutOffIndex { get; set; }
        public string algorithm { get; set; }
        public string algorithmQueryType { get; set; }
        public string suggestionAlgorithm { get; set; }
        public List<object> relatedGames { get; set; }
    }
}
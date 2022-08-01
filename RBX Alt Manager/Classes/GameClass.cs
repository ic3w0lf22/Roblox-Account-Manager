using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace RBX_Alt_Manager
{
    public class Game
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
    }

    public class FavoriteGame
    {
        public string Name;
        public long PlaceID;
        public string PrivateServer;

        public FavoriteGame()
        {
            Name = "???";
            PlaceID = 1818;
            PrivateServer = "";
        }

        public FavoriteGame(string Name, long PlaceID) : this()
        {
            this.Name = Name;
            this.PlaceID = PlaceID;
        }

        public FavoriteGame(string Name, long PlaceID, string PrivateServer) : this(Name, PlaceID) =>
            this.PrivateServer = PrivateServer;
    }

    public class RecentGame
    {
        private string _Name = "";

        public string Name
        {
            get => _Name;
            set
            {
                value = Regex.Replace(value, @"[\s]?\[[^]]*\][\s]?", "");
                value = Regex.Replace(value, "[^a-zA-Z0-9% ._]", string.Empty);
                _Name = value;
            }
        }

        public long PlaceId;
    }

    public class GameList
    {
        public List<Game> games { get; set; }
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

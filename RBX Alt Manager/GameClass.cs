using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }

    public class ListGame
    {
        public string name;
        public int playerCount;
        public int likeRatio;
        public long placeId;

        public ListGame(string Name, int PC, int LR, long placeID)
        {
            name = Name;
            playerCount = PC;
            likeRatio = LR;
            placeId = placeID;
        }
    }

    public class FavoriteGame
    {
        public string Name;
        public long PlaceID;

        public FavoriteGame(string Name, long PlaceID)
        {
            this.Name = Name;
            this.PlaceID = PlaceID;
        }
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

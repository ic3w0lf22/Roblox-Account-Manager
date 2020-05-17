using System.Collections.Generic;

namespace RBX_Alt_Manager
{
    public class GameInstance
    {
        public int Capacity;
        public int Ping;
        public string Fps;
        public bool ShowSlowGameMessage;
        public string Guid;
        public long PlaceId;
        public List<GamePlayer> CurrentPlayers;
        public bool UserCanJoin;
        public bool ShowShutdownButton;
        public string JoinScript;
        public string FriendsDescription;
        public string FriendsMouseover;
        public string PlayersCapacity;
    }
}
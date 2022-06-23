using System.Collections.Generic;

namespace RBX_Alt_Manager
{
    public class ServerData
    {
        public string id { get; set; }
        public int maxPlayers { get; set; }
        public int playing { get; set; }
        public List<string> playerTokens { get; set; }
        public List<object> players { get; set; }
        public string fps { get; set; }
        public int ping { get; set; }
        public string name;
        public long vipServerId;
        public string accessCode;
        public string type;
        public string region;
        public string ip;
        public bool regionLoaded;

        public ServerData()
        {
        }

        public ServerData(string id, int maxPlayers, int playing, string fps, int ping)
        {
            this.id = id;
            this.maxPlayers = maxPlayers;
            this.playing = playing;
            this.fps = fps;
            this.ping = ping;
        }
    }
    public class ServersInfo
    {
        public string previousPageCursor;
        public string nextPageCursor;
        public List<ServerData> data;

        public ServersInfo()
        {
            previousPageCursor = "_";
            nextPageCursor = "_";
            data = new List<ServerData>();
        }
    }
}
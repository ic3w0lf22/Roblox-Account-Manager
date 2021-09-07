using System.Collections.Generic;

namespace RBX_Alt_Manager
{
    public class ServerData
    {
        public string id;
        public string name;
        public int maxPlayers;
        public int playing;
        public string fps;
        public int ping;
        public long vipServerId;
        public string accessCode;
        public string type;

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
    }
}
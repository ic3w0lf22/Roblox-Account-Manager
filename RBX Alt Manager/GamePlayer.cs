namespace RBX_Alt_Manager
{
    public class Thumbnail
    {
        public long AssetId;
        public string AssetHash;
        public int AssetTypeId;
        public string Url;
        public bool IsFinal;
    }
    public class GamePlayer
    {
        public long Id;
        public string Username;
        public Thumbnail Thumbnail;
    }
}
using System.Collections.Generic;

namespace RBX_Alt_Manager
{
    public class Avatar
    {
        public long targetId { get; set; }
        public string state { get; set; }
        public string imageUrl { get; set; }
    }

    public class AvatarRoot
    {
        public List<Avatar> data { get; set; }
    }

    public class TokenRequest
    {
        public string requestId { get; set; }
        public string type { get; set; }
        public long targetId { get; set; }
        public string token { get; set; }
        public string format { get; set; }
        public string size { get; set; }

        public TokenRequest(string Token)
        {
            requestId = $"0:{Token}:AvatarHeadshot:48x48:png:regular";
            type = "AvatarHeadShot";
            targetId = 0;
            format = "png";
            size = "48x48";
            token = Token;
        }
    }

    public class TokenAvatar
    {
        public string requestId { get; set; }
        public int errorCode { get; set; }
        public string errorMessage { get; set; }
        public long targetId { get; set; }
        public string state { get; set; }
        public string imageUrl { get; set; }
    }

    public class TokenAvatarRoot
    {
        public List<TokenAvatar> data { get; set; }
    }
}
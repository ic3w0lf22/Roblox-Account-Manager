#pragma warning disable CS8632
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace RBX_Alt_Manager.Classes
{
    public static class Presence
    {
        public static Dictionary<UserPresenceType, Color> Colors = new Dictionary<UserPresenceType, Color> {
            { UserPresenceType.Online, Color.FromArgb(255, 0, 162, 255) },
            { UserPresenceType.InGame, Color.FromArgb(255, 2, 183, 87) },
            { UserPresenceType.InStudio, Color.FromArgb(255, 70, 41, 216) }
        };

        public static RestClient PresenceClient = new RestClient("https://presence.roblox.com/");

        public static async Task UpdatePresence(params long[] UserIds)
        {
            if (!Utilities.IsConnectedToInternet()) return;

            List<Account> Updated = new List<Account>();
            var Response = await PresenceClient.PostAsync(new RestRequest("v1/presence/users", Method.Post).AddJsonBody(new { userIds = UserIds }));

            if (Response.IsSuccessful && JsonConvert.DeserializeObject(Response.Content) is JObject Data && Data.ContainsKey("userPresences"))
            {
                foreach (var Presence in Data["userPresences"].ToObject<List<UserPresence>>())
                    if (AccountManager.AccountsList.FirstOrDefault(acc => acc.UserID == Presence.userId) is Account account)
                    {
                        account.Presence = Presence;

                        Updated.Add(account);
                    }
            }

            AccountManager.Instance.InvokeIfRequired(() => AccountManager.Instance.AccountsView.RefreshObjects(Updated));
        }

        public static async Task<Dictionary<long, UserPresence>> GetPresence(params long[] UserIds)
        {
            if (!Utilities.IsConnectedToInternet()) return null;

            var Dict = new Dictionary<long, UserPresence>();
            var Response = await PresenceClient.PostAsync(new RestRequest("v1/presence/users", Method.Post).AddJsonBody(new { userIds = UserIds }));

            if (Response.IsSuccessful && JsonConvert.DeserializeObject(Response.Content) is JObject Data && Data.ContainsKey("userPresences"))
            {
                foreach (var Presence in Data["userPresences"].ToObject<List<UserPresence>>())
                    Dict.Add(Presence.userId, Presence);

                return Dict;
            }

            return null;
        }

        public static async Task<UserPresence> GetPresenceSingular(long UserId) => ((await GetPresence(UserId)) is Dictionary<long, UserPresence> Dict && Dict.ContainsKey(UserId)) ? Dict[UserId] : null;
    }

    public class UserPresence
    {
        public UserPresenceType userPresenceType { get; set; }
        public string lastLocation { get; set; }
        public long? placeId { get; set; }
        public long? rootPlaceId { get; set; }
        public string? gameId { get; set; }
        public long? universeId { get; set; }
        public long userId { get; set; }
        public DateTime lastOnline { get; set; }
    }

    public enum UserPresenceType
    {
        Offline,
        Online,
        InGame,
        InStudio
    }
}
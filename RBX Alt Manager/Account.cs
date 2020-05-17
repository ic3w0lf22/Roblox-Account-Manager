using RestSharp;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace RBX_Alt_Manager
{
    public class Account
    {
        public bool Valid;
        public string SecurityToken;
        public string Username;
        private string _Alias = "";
        private string _Description = "";
        public int UserID;

        public string Alias
        {
            get => _Alias;
            set
            {
                if (value == null || value.Length > 50)
                    return;

                _Alias = value;
                AccountManager.SaveAccounts();
            }
        }
        public string Description
        {
            get => _Description;
            set
            {
                if (value == null || value.Length > 5000)
                    return;

                _Description = value;
                AccountManager.SaveAccounts();
            }
        }

        public string Validate(string Token, string UserData)
        {
            if (!string.IsNullOrEmpty (UserData)&& UserData.Contains("UserEmail"))
            {
                Valid = true;
                SecurityToken = Token;
                Match MUsername = Regex.Match(UserData, @"""Name"":""(\w+)""");
                Match MUserID = Regex.Match(UserData, @"""UserId"":(\d+)");
                if (MUsername.Success && MUsername.Groups.Count >= 2) Username = MUsername.Groups[1].ToString();
                if (MUserID.Success && MUserID.Groups.Count >= 2) UserID = Convert.ToInt32(MUserID.Groups[1].Value);
                return "Success";
            }

            return "false";
        }

        public string JoinServer(long PlaceID, string JobID = "", bool FollowUser = false, bool JoinVIP = false)
        {
            string CurrentVersion = AccountManager.CurrentVersion;

            if (string.IsNullOrEmpty(CurrentVersion)) return "ERROR: No Roblox Version";

            RestRequest request = new RestRequest("v1/authentication-ticket/", Method.POST);

            request.AddCookie(".ROBLOSECURITY", SecurityToken);
            request.AddHeader("Referer", "https://www.roblox.com/games/171336322/testing");

            IRestResponse response = AccountManager.client.Execute(request);
            Parameter result = response.Headers.FirstOrDefault(x => x.Name == "x-csrf-token");

            string Token = "";

            if (result != null)
                Token = (string)result.Value;
            else
                return "ERROR: Account Session Expired, right click the account and press re-auth. (1)";

            if (string.IsNullOrEmpty(Token) || result == null)
                return "ERROR: Account Session Expired, right click the account and press re-auth. (2)";

            request = new RestRequest("/v1/authentication-ticket/", Method.POST);
            request.AddCookie(".ROBLOSECURITY", SecurityToken);
            request.AddHeader("X-CSRF-TOKEN", Token);
            request.AddHeader("Referer", "https://www.roblox.com/games/171336322/testing");
            response = AccountManager.client.Execute(request);

            Parameter Ticket = response.Headers.FirstOrDefault(x => x.Name == "rbx-authentication-ticket");

            if (Ticket != null)
            {
                Token = (string)Ticket.Value;
                string RPath = @"C:\Program Files (x86)\Roblox\Versions\" + CurrentVersion;

                if (!Directory.Exists(RPath))
                    RPath = Path.Combine(Environment.GetEnvironmentVariable("LocalAppData"), @"Roblox\Versions\" + CurrentVersion);
                if (!Directory.Exists(RPath))
                    return "ERROR: Failed to find ROBLOX executable";

                RPath = RPath + @"\RobloxPlayerBeta.exe";
                ProcessStartInfo Roblox = new ProcessStartInfo(RPath);
                if (JoinVIP)
                {
                    Roblox.Arguments = string.Format("--play -a https://www.roblox.com/Login/Negotiate.ashx -t {0} -j \"https://assetgame.roblox.com/game/PlaceLauncher.ashx?request=RequestPrivateGame&placeId={1}&accessCode={2}\"", Token, PlaceID, JobID);
                }
                else if (FollowUser)
                    Roblox.Arguments = string.Format("--play -a https://www.roblox.com/Login/Negotiate.ashx -t {0} -j \"https://assetgame.roblox.com/game/PlaceLauncher.ashx?request=RequestFollowUser&userId={1}\"", Token, PlaceID);
                else
                    Roblox.Arguments = string.Format("--play -a https://www.roblox.com/Login/Negotiate.ashx -t {0} -j \"https://assetgame.roblox.com/game/PlaceLauncher.ashx?request=RequestGame{3}&placeId={1}{2}&isPlayTogetherGame=false\"", Token, PlaceID, "&gameId=" + JobID, string.IsNullOrEmpty(JobID) ? "" : "Job");
                Process.Start(Roblox);
                return "Success";
            }
            else
                return "ERROR: Invalid Authentication Ticket";
        }
    }
}
using Microsoft.Win32;
using RestSharp;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace RBX_Alt_Manager
{
    public class Account
    {
        public bool Valid;
        public string SecurityToken;
        public string Username;
        private string _Alias = "";
        private string _Description = "";
        public long UserID;

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
            if (!string.IsNullOrEmpty(UserData) && UserData.Contains("UserEmail"))
            {
                Valid = true;
                SecurityToken = Token;
                Match MUsername = Regex.Match(UserData, @"""Name"":""(\w+)""");
                Match MUserID = Regex.Match(UserData, @"""UserId"":(\d+)");
                if (MUsername.Success && MUsername.Groups.Count >= 2) Username = MUsername.Groups[1].ToString();
                if (MUserID.Success && MUserID.Groups.Count >= 2) UserID = Convert.ToInt64(MUserID.Groups[1].Value);
                return "Success";
            }

            return "false";
        }

        public static long LongRandom(long min, long max, Random rand)
        {
            long result = rand.Next((Int32)(min >> 32), (Int32)(max >> 32));
            result = (result << 32);
            result = result | (long)rand.Next((Int32)min, (Int32)max);
            return result;
        }

        public string JoinServer(long PlaceID, string JobID = "", bool FollowUser = false, bool JoinVIP = false)
        {
            Random r = new Random();
            RegistryKey RbxCmdPath = Registry.ClassesRoot.OpenSubKey(@"roblox-player\shell\open\command", RegistryKeyPermissionCheck.ReadSubTree);
            
            string LaunchPath = RbxCmdPath.GetValue("").ToString();
            string CurrentVersion = AccountManager.CurrentVersion;

            bool UseRegistryPath = !string.IsNullOrEmpty(LaunchPath);

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
                return "ERROR: Account Session Expired, right click the account and press re-auth or try again. (1)";

            if (string.IsNullOrEmpty(Token) || result == null)
                return "ERROR: Account Session Expired, right click the account and press re-auth or try again. (2)";

            request = new RestRequest("/v1/authentication-ticket/", Method.POST);
            request.AddCookie(".ROBLOSECURITY", SecurityToken);
            request.AddHeader("X-CSRF-TOKEN", Token);
            request.AddHeader("Referer", "https://www.roblox.com/games/171336322/testing");
            response = AccountManager.client.Execute(request);

            Parameter Ticket = response.Headers.FirstOrDefault(x => x.Name == "rbx-authentication-ticket");

            if (Ticket != null)
            {
                Token = (string)Ticket.Value;

                string LinkCode = Regex.Match(JobID, "privateServerLinkCode=(.+)")?.Groups[1]?.Value;
                string AccessCode = JobID;

                if (!string.IsNullOrEmpty(LinkCode))
                { 
                    request = new RestRequest(string.Format("/games/{0}?privateServerLinkCode={1}", PlaceID, LinkCode), Method.GET);
                    request.AddCookie(".ROBLOSECURITY", SecurityToken);
                    request.AddHeader("X-CSRF-TOKEN", Token);
                    request.AddHeader("Referer", "https://www.roblox.com/games/171336322/testing");

                    response = AccountManager.mainclient.Execute(request);

                    if (response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string pattern = "Roblox.GameLauncher.joinPrivateGame\\(\\d+,'(\\w+\\-\\w+\\-\\w+\\-\\w+\\-\\w+)";
                        Regex regex = new Regex(pattern);
                        MatchCollection matches = regex.Matches(response.Content);

                        if (matches.Count > 0 && matches[0].Groups.Count > 0)
                        {
                            JoinVIP = true;
                            AccessCode = matches[0].Groups[1].Value;
                        }
                    }
                }

                if (UseRegistryPath && !AccountManager.UseOldJoin)
                {
                    if (JoinVIP)
                    {
                        string Argument = string.Format("roblox-player:1+launchmode:play+gameinfo:{0}+launchtime:{4}+placelauncherurl:https://assetgame.roblox.com/game/PlaceLauncher.ashx?request=RequestPrivateGame&placeId={1}&accessCode={2}&linkCode={3}+browsertrackerid:{5}+robloxLocale:en_us+gameLocale:en_us", Token, PlaceID, AccessCode, LinkCode, DateTime.Now.Ticks, LongRandom(50000000000, 60000000000, r));

                        Process.Start(Argument);
                    }
                    else if (FollowUser)
                        Process.Start(string.Format("roblox-player:1+launchmode:play+gameinfo:{0}+launchtime:{2}+placelauncherurl:https://assetgame.roblox.com/game/PlaceLauncher.ashx?request=RequestFollowUser&userId={1}+browsertrackerid:{3}+robloxLocale:en_us+gameLocale:en_us", Token, PlaceID, DateTime.Now.Ticks, LongRandom(50000000000, 60000000000, r)));
                    else
                        Process.Start(string.Format("roblox-player:1+launchmode:play+gameinfo:{0}+launchtime:{4}+placelauncherurl:https://assetgame.roblox.com/game/PlaceLauncher.ashx?request=RequestGame{3}&placeId={1}{2}{6}+browsertrackerid:{5}+robloxLocale:en_us+gameLocale:en_us", Token, PlaceID, "&gameId=" + JobID, string.IsNullOrEmpty(JobID) ? "" : "Job", DateTime.Now.Ticks, LongRandom(50000000000, 60000000000, r), AccountManager.IsTeleport ? "&isTeleport=true" : ""));

                    return "Success";
                }
                else // probably will never happen
                {
                    string RPath = @"C:\Program Files (x86)\Roblox\Versions\" + CurrentVersion;

                    if (!Directory.Exists(RPath))
                        RPath = Path.Combine(Environment.GetEnvironmentVariable("LocalAppData"), @"Roblox\Versions\" + CurrentVersion);
                    if (!Directory.Exists(RPath))
                        return "ERROR: Failed to find ROBLOX executable, either restart the account manager or make sure your roblox is updated";

                    RPath = RPath + @"\RobloxPlayerBeta.exe";
                    ProcessStartInfo Roblox = new ProcessStartInfo(RPath);
                    if (JoinVIP)
                        Roblox.Arguments = string.Format("--play -a https://www.roblox.com/Login/Negotiate.ashx -t {0} -j \"https://assetgame.roblox.com/game/PlaceLauncher.ashx?request=RequestPrivateGame&placeId={1}&accessCode={2}&linkCode=\"", Token, PlaceID, JobID, LinkCode);
                    else if (FollowUser)
                        Roblox.Arguments = string.Format("--play -a https://www.roblox.com/Login/Negotiate.ashx -t {0} -j \"https://assetgame.roblox.com/game/PlaceLauncher.ashx?request=RequestFollowUser&userId={1}\"", Token, PlaceID);
                    else
                        Roblox.Arguments = string.Format("--play -a https://www.roblox.com/Login/Negotiate.ashx -t {0} -j \"https://assetgame.roblox.com/game/PlaceLauncher.ashx?request=RequestGame{3}&placeId={1}{2}&isPlayTogetherGame=false\"", Token, PlaceID, "&gameId=" + JobID, string.IsNullOrEmpty(JobID) ? "" : "Job");
                    Process.Start(Roblox);
                    return "Success";
                }
            }
            else
                return "ERROR: Invalid Authentication Ticket";
        }
    }
}
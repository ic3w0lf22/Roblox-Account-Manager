using Microsoft.Win32;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

#pragma warning disable CS0618

namespace RBX_Alt_Manager
{
    public class PinStatus
    {
        public bool isEnabled { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public double unlockedUntil { get; set; }
    }

    public class Account
    {
        public bool Valid;
        public string SecurityToken;
        public string Username;
        private string _Alias = "";
        private string _Description = "";
        public string Group { get; set; }
        public long UserID;
        [JsonIgnore] public DateTime PinUnlocked;
        [JsonIgnore] public DateTime TokenSet;
        [JsonIgnore] public DateTime LastAppLaunch;
        [JsonIgnore] public string CSRFToken;

        private string BrowserTrackerID;

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

        public string GetCSRFToken()
        {
            if ((DateTime.Now - TokenSet).TotalMinutes < 5) return CSRFToken;

            RestRequest request = new RestRequest("v1/authentication-ticket/", Method.POST);

            request.AddCookie(".ROBLOSECURITY", SecurityToken);
            request.AddHeader("Referer", "https://www.roblox.com/games/171336322/testing");

            IRestResponse response = AccountManager.client.Execute(request);
            Parameter result = response.Headers.FirstOrDefault(x => x.Name == "x-csrf-token");

            string Token = "";

            if (result != null)
                Token = (string)result.Value;

            CSRFToken = Token;

            return Token;
        }

        public bool CheckPin(bool Internal = false)
        {
            string Token = GetCSRFToken();

            if (string.IsNullOrEmpty(Token))
            {
                if (!Internal) MessageBox.Show("Invalid Account Session!", "Account Manager", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }

            if (DateTime.Now < PinUnlocked)
            {
                return true;
            }

            RestRequest request = new RestRequest("v1/account/pin/", Method.GET);

            request.AddCookie(".ROBLOSECURITY", SecurityToken);
            request.AddHeader("Referer", "https://www.roblox.com/");

            IRestResponse response = AccountManager.client.Execute(request);

            if (response.IsSuccessful && response.StatusCode == HttpStatusCode.OK)
            {
                PinStatus pinInfo = JsonConvert.DeserializeObject<PinStatus>(response.Content);

                if (!pinInfo.isEnabled) return true;
                if (pinInfo.isEnabled && pinInfo.unlockedUntil > 0) return true;
            }

            if (!Internal) MessageBox.Show("Pin required!", "Account Manager", MessageBoxButtons.OK, MessageBoxIcon.Error);

            return false;
        }

        public bool UnlockPin(string Pin)
        {
            if (Pin.Length != 4) return false;
            if (CheckPin(true)) return true;

            RestRequest request = new RestRequest("v1/account/pin/unlock", Method.POST);

            request.AddCookie(".ROBLOSECURITY", SecurityToken);
            request.AddHeader("Referer", "https://www.roblox.com/");
            request.AddHeader("X-CSRF-TOKEN", GetCSRFToken());
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("pin", Pin);

            IRestResponse response = AccountManager.client.Execute(request);

            if (response.IsSuccessful && response.StatusCode == HttpStatusCode.OK)
            {
                PinStatus pinInfo = JsonConvert.DeserializeObject<PinStatus>(response.Content);

                if (pinInfo.isEnabled && pinInfo.unlockedUntil > 0)
                    PinUnlocked = DateTime.Now.AddSeconds(pinInfo.unlockedUntil);

                if (PinUnlocked > DateTime.Now)
                {
                    MessageBox.Show("Pin unlocked for 5 minutes", "Account Manager", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    return true;
                }
            }

            return false;
        }

        public bool SetFollowPrivacy(int Privacy)
        {
            if (!CheckPin()) return false;

            RestRequest request = new RestRequest("account/settings/follow-me-privacy", Method.POST);

            request.AddCookie(".ROBLOSECURITY", SecurityToken);
            request.AddHeader("Referer", "https://www.roblox.com/my/account");
            request.AddHeader("X-CSRF-TOKEN", GetCSRFToken());
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");

            switch (Privacy)
            {
                case 0:
                    request.AddParameter("FollowMePrivacy", "All");
                    break;
                case 1:
                    request.AddParameter("FollowMePrivacy", "Followers");
                    break;
                case 2:
                    request.AddParameter("FollowMePrivacy", "Following");
                    break;
                case 3:
                    request.AddParameter("FollowMePrivacy", "Friends");
                    break;
                case 4:
                    request.AddParameter("FollowMePrivacy", "NoOne");
                    break;
            }

            IRestResponse response = AccountManager.mainclient.Execute(request);

            if (response.IsSuccessful && response.StatusCode == HttpStatusCode.OK) return true;

            return false;
        }

        public bool ChangePassword(string Current, string New)
        {
            if (!CheckPin()) return false;

            RestRequest request = new RestRequest("v2/user/passwords/change", Method.POST);

            request.AddCookie(".ROBLOSECURITY", SecurityToken);
            request.AddHeader("Referer", "https://www.roblox.com/");
            request.AddHeader("X-CSRF-TOKEN", GetCSRFToken());
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("currentPassword", Current);
            request.AddParameter("newPassword", New);

            IRestResponse response = AccountManager.client.Execute(request);

            if (response.IsSuccessful && response.StatusCode == HttpStatusCode.OK)
            {
                RestResponseCookie SToken = response.Cookies.FirstOrDefault(x => x.Name == ".ROBLOSECURITY");

                if (SToken != null)
                {
                    SecurityToken = SToken.Value;
                    AccountManager.SaveAccounts();
                }
                else
                    MessageBox.Show("An error occured while changing passwords, you will need to re-login with your new password!", "Account Manager", MessageBoxButtons.OK, MessageBoxIcon.Error);

                MessageBox.Show("Password changed!", "Account Manager", MessageBoxButtons.OK, MessageBoxIcon.Information);

                return true;
            }

            MessageBox.Show("Failed to change password!", "Account Manager", MessageBoxButtons.OK, MessageBoxIcon.Error);

            return false;
        }

        public bool ChangeEmail(string Password, string NewEmail)
        {
            if (!CheckPin()) return false;

            RestRequest request = new RestRequest("v1/email", Method.POST);

            request.AddCookie(".ROBLOSECURITY", SecurityToken);
            request.AddHeader("Referer", "https://www.roblox.com/");
            request.AddHeader("X-CSRF-TOKEN", GetCSRFToken());
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("password", Password);
            request.AddParameter("emailAddress", NewEmail);

            IRestResponse response = AccountManager.AccountClient.Execute(request);

            if (response.IsSuccessful && response.StatusCode == HttpStatusCode.OK)
            {
                MessageBox.Show("Email changed!", "Account Manager", MessageBoxButtons.OK, MessageBoxIcon.Information);

                return true;
            }

            MessageBox.Show("Failed to change email, maybe your password is incorrect!", "Account Manager", MessageBoxButtons.OK, MessageBoxIcon.Error);

            return false;
        }

        public bool LogOutOfOtherSessions()
        {
            if (!CheckPin()) return false;

            RestRequest request = new RestRequest("authentication/signoutfromallsessionsandreauthenticate", Method.POST);

            request.AddCookie(".ROBLOSECURITY", SecurityToken);
            request.AddHeader("Referer", "https://www.roblox.com/");
            request.AddHeader("X-CSRF-TOKEN", GetCSRFToken());
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");

            IRestResponse response = AccountManager.mainclient.Execute(request);

            if (response.IsSuccessful && response.StatusCode == HttpStatusCode.OK)
            {
                RestResponseCookie SToken = response.Cookies.FirstOrDefault(x => x.Name == ".ROBLOSECURITY");

                if (SToken != null)
                {
                    SecurityToken = SToken.Value;
                    AccountManager.SaveAccounts();
                }
                else
                    MessageBox.Show("An error occured, you will need to re-login!", "Account Manager", MessageBoxButtons.OK, MessageBoxIcon.Error);

                MessageBox.Show("Signed out of all other sessions!", "Account Manager", MessageBoxButtons.OK, MessageBoxIcon.Information);

                return true;
            }

            MessageBox.Show("Failed to log out of other sessions!", "Account Manager", MessageBoxButtons.OK, MessageBoxIcon.Error);

            return false;
        }

        public bool BlockPlayer(string Username)
        {
            if (!CheckPin()) return false;

            long BlockeeID = AccountManager.GetUserID(Username);

            RestRequest request = new RestRequest($"userblock/getblockedusers?userId={UserID}&page=1", Method.GET);

            request.AddCookie(".ROBLOSECURITY", SecurityToken);

            IRestResponse response = AccountManager.apiclient.Execute(request);

            if (response.IsSuccessful && response.StatusCode == HttpStatusCode.OK)
            {
                if (!Regex.IsMatch(response.Content, @"\b" + BlockeeID.ToString() + @"\b"))
                {
                    RestRequest blockReq = new RestRequest("userblock/blockuser", Method.POST);

                    blockReq.AddCookie(".ROBLOSECURITY", SecurityToken);
                    blockReq.AddHeader("Referer", "https://www.roblox.com/");
                    blockReq.AddHeader("X-CSRF-TOKEN", GetCSRFToken());
                    blockReq.AddHeader("Content-Type", "application/json");
                    blockReq.AddJsonBody(new { blockeeId = BlockeeID.ToString() });

                    IRestResponse blockRes = AccountManager.mainclient.Execute(blockReq);

                    if (blockRes.Content.Contains(@"""success"":true"))
                        MessageBox.Show("Blocked " + Username);
                    else
                        MessageBox.Show("Failed to Block " + Username);
                }
                else
                {
                    RestRequest blockReq = new RestRequest("userblock/unblockuser", Method.POST);

                    blockReq.AddCookie(".ROBLOSECURITY", SecurityToken);
                    blockReq.AddHeader("Referer", "https://www.roblox.com/");
                    blockReq.AddHeader("X-CSRF-TOKEN", GetCSRFToken());
                    blockReq.AddHeader("Content-Type", "application/json");
                    blockReq.AddJsonBody(new { blockeeId = BlockeeID.ToString() });

                    IRestResponse blockRes = AccountManager.mainclient.Execute(blockReq);

                    if (blockRes.Content.Contains(@"""success"":true"))
                        MessageBox.Show("Unblocked " + Username);
                    else
                        MessageBox.Show("Failed to Unblock " + Username);
                }

                return true;
            }

            MessageBox.Show("Failed to block user!", "Account Manager", MessageBoxButtons.OK, MessageBoxIcon.Error);

            return false;
        }

        public string JoinServer(long PlaceID, string JobID = "", bool FollowUser = false, bool JoinVIP = false)
        {
            if (string.IsNullOrEmpty(BrowserTrackerID))
            {
                Random r = new Random();
                BrowserTrackerID = r.Next(500000, 600000).ToString() + r.Next(10000, 90000).ToString(); // longrandom doesnt work shrug
            }

            RegistryKey RbxCmdPath = Registry.ClassesRoot.OpenSubKey(@"roblox-player\shell\open\command", RegistryKeyPermissionCheck.ReadSubTree);

            string LaunchPath = RbxCmdPath.GetValue("").ToString();
            string CurrentVersion = AccountManager.CurrentVersion;

            bool UseRegistryPath = !string.IsNullOrEmpty(LaunchPath);

            if (string.IsNullOrEmpty(CurrentVersion)) return "ERROR: No Roblox Version";

            RestRequest request = new RestRequest("v1/authentication-ticket/", Method.POST);

            request.AddCookie(".ROBLOSECURITY", SecurityToken);
            request.AddHeader("Referer", "https://www.roblox.com/games/171336322/testing");

            string Token = GetCSRFToken();

            if (string.IsNullOrEmpty(Token))
                return "ERROR: Account Session Expired, re-add the account or try again. (1)";

            request = new RestRequest("/v1/authentication-ticket/", Method.POST);
            request.AddCookie(".ROBLOSECURITY", SecurityToken);
            request.AddHeader("X-CSRF-TOKEN", Token);
            request.AddHeader("Referer", "https://www.roblox.com/games/171336322/testing");

            IRestResponse response = AccountManager.client.Execute(request);

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
                    double LaunchTime = Math.Floor((DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds * 1000);

                    if (JoinVIP)
                    {
                        string Argument = string.Format("roblox-player:1+launchmode:play+gameinfo:{0}+launchtime:{4}+placelauncherurl:https://assetgame.roblox.com/game/PlaceLauncher.ashx?request=RequestPrivateGame&placeId={1}&accessCode={2}&linkCode={3}+browsertrackerid:{5}+robloxLocale:en_us+gameLocale:en_us", Token, PlaceID, AccessCode, LinkCode, LaunchTime, BrowserTrackerID);

                        Process.Start(Argument);
                    }
                    else if (FollowUser)
                        Process.Start(string.Format("roblox-player:1+launchmode:play+gameinfo:{0}+launchtime:{2}+placelauncherurl:https://assetgame.roblox.com/game/PlaceLauncher.ashx?request=RequestFollowUser&userId={1}+browsertrackerid:{3}+robloxLocale:en_us+gameLocale:en_us", Token, PlaceID, LaunchTime, BrowserTrackerID));
                    else
                        Process.Start($"roblox-player:1+launchmode:play+gameinfo:{Token}+launchtime:{LaunchTime}+placelauncherurl:https://assetgame.roblox.com/game/PlaceLauncher.ashx?request=RequestGame{ (string.IsNullOrEmpty(JobID) ? "" : "Job") }&browserTrackerId={BrowserTrackerID}&placeId={PlaceID}{(string.IsNullOrEmpty(JobID) ? "" : ("&gameId=" + JobID))}&isPlayTogetherGame=false{(AccountManager.IsTeleport ? "&isTeleport=true" : "")}+browsertrackerid:{BrowserTrackerID}+robloxLocale:en_us+gameLocale:en_us");

                    return "Success";
                }
                else // probably will never happen, SHOULDNT EVER HAPPEN
                {
                    /*string RPath = @"C:\Program Files (x86)\Roblox\Versions\" + CurrentVersion;

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
                    Process.Start(Roblox);*/
                    return "How did this happen...";
                }
            }
            else
                return "ERROR: Invalid Authentication Ticket";
        }

        public string LaunchApp()
        {
            if ((DateTime.Now - LastAppLaunch).TotalSeconds < 15)
                return "Woah slow down";

            if (string.IsNullOrEmpty(BrowserTrackerID))
            {
                Random r = new Random();
                BrowserTrackerID = r.Next(500000, 600000).ToString() + r.Next(10000, 90000).ToString(); // longrandom doesnt work shrug
            }

            RegistryKey RbxCmdPath = Registry.ClassesRoot.OpenSubKey(@"roblox-player\shell\open\command", RegistryKeyPermissionCheck.ReadSubTree);

            string LaunchPath = RbxCmdPath.GetValue("").ToString();
            string CurrentVersion = AccountManager.CurrentVersion;

            bool UseRegistryPath = !string.IsNullOrEmpty(LaunchPath);

            if (string.IsNullOrEmpty(CurrentVersion)) return "ERROR: No Roblox Version";

            RestRequest request = new RestRequest("v1/authentication-ticket/", Method.POST);

            request.AddCookie(".ROBLOSECURITY", SecurityToken);
            request.AddHeader("Referer", "https://www.roblox.com/games/171336322/testing");

            string Token = GetCSRFToken();

            if (string.IsNullOrEmpty(Token))
                return "ERROR: Account Session Expired, re-add the account or try again. (1)";

            request = new RestRequest("/v1/authentication-ticket/", Method.POST);
            request.AddCookie(".ROBLOSECURITY", SecurityToken);
            request.AddHeader("X-CSRF-TOKEN", Token);
            request.AddHeader("Referer", "https://www.roblox.com/games/171336322/testing");

            IRestResponse response = AccountManager.client.Execute(request);

            Parameter Ticket = response.Headers.FirstOrDefault(x => x.Name == "rbx-authentication-ticket");

            if (Ticket != null)
            {
                Token = (string)Ticket.Value;

                double LaunchTime = Math.Floor((DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds * 1000);

                Process.Start($"roblox-player:1+launchmode:app+gameinfo:{Token}+launchtime:{LaunchTime}+browsertrackerid:{BrowserTrackerID}+robloxLocale:en_us+gameLocale:en_us");

                return "Success";
            }
            else
                return "ERROR: Invalid Authentication Ticket";
        }
    }
}
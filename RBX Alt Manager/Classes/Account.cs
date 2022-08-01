#pragma warning disable CS0618

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;

namespace RBX_Alt_Manager
{
    public class Account : IComparable<Account>
    {
        public bool Valid;
        public string SecurityToken;
        public string Username;
        public DateTime LastUse;
        private string _Alias = "";
        private string _Description = "";
        private string _Password = "";
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)] public string Group { get; set; } = "Default";
        public long UserID;
        public Dictionary<string, string> Fields = new Dictionary<string, string>();
        [JsonIgnore] public DateTime PinUnlocked;
        [JsonIgnore] public DateTime TokenSet;
        [JsonIgnore] public DateTime LastAppLaunch;
        [JsonIgnore] public string CSRFToken;

        public int CompareTo(Account compareTo)
        {
            if (compareTo == null)
                return 1;

            else
                return Group.CompareTo(compareTo.Group);
        }

        private string BrowserTrackerID;

        public string Alias
        {
            get => _Alias;
            set
            {
                if (value == null || value.Length > 50)
                    return;

                _Alias = value;
                AccountManager.DelayedSaveAccounts();
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
                AccountManager.DelayedSaveAccounts();
            }
        }
        public string Password
        {
            get => _Password;
            set
            {
                if (value == null || value.Length > 5000)
                    return;

                _Password = value;
                AccountManager.DelayedSaveAccounts();
            }
        }

        public Account() { }

        public Account(string Token)
        {
            RestRequest DataRequest = new RestRequest("my/account/json", Method.GET);

            DataRequest.AddCookie(".ROBLOSECURITY", Token);

            IRestResponse response = AccountManager.MainClient.Execute(DataRequest);

            if (response.StatusCode == HttpStatusCode.OK && Utilities.TryParseJson(response.Content, out AccountJson Data))
            {
                Username = Data.Name;
                UserID = Data.UserId;

                Valid = true;
                SecurityToken = Token;

                LastUse = DateTime.Now;
            }
        }

        public bool GetAuthTicket(out string Ticket)
        {
            Ticket = string.Empty;

            RestRequest request = new RestRequest("/v1/authentication-ticket/", Method.POST);
            request.AddCookie(".ROBLOSECURITY", SecurityToken);
            request.AddHeader("X-CSRF-TOKEN", GetCSRFToken());
            request.AddHeader("Referer", "https://www.roblox.com/games/606849621/Jailbreak");

            IRestResponse response = AccountManager.AuthClient.Execute(request);

            Parameter TicketHeader = response.Headers.FirstOrDefault(x => x.Name == "rbx-authentication-ticket");

            if (TicketHeader != null)
            {
                Ticket = (string)TicketHeader.Value;

                return true;
            }

            return false;
        }

        public string GetCSRFToken(bool ForceRequest = false)
        {
            if (!ForceRequest && (DateTime.Now - TokenSet).TotalMinutes < 3) return CSRFToken;

            RestRequest request = new RestRequest("v1/authentication-ticket/", Method.POST);

            request.AddCookie(".ROBLOSECURITY", SecurityToken);
            request.AddHeader("Referer", "https://www.roblox.com/games/606849621/Jailbreak");

            IRestResponse response = AccountManager.AuthClient.Execute(request);
            Parameter result = response.Headers.FirstOrDefault(x => x.Name == "x-csrf-token");

            string Token = string.Empty;

            if (result != null)
            {
                Token = (string)result.Value;
                LastUse = DateTime.Now;

                AccountManager.DelayedSaveAccounts();
            }

            CSRFToken = Token;
            TokenSet = DateTime.Now;

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

            IRestResponse response = AccountManager.AuthClient.Execute(request);

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

            IRestResponse response = AccountManager.AuthClient.Execute(request);

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

            IRestResponse response = AccountManager.MainClient.Execute(request);

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

            IRestResponse response = AccountManager.AuthClient.Execute(request);

            if (response.IsSuccessful && response.StatusCode == HttpStatusCode.OK)
            {
                Password = New;

                RestResponseCookie SToken = response.Cookies.FirstOrDefault(x => x.Name == ".ROBLOSECURITY");

                if (SToken != null)
                {
                    SecurityToken = SToken.Value;
                    AccountManager.DelayedSaveAccounts();
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

            IRestResponse response = AccountManager.MainClient.Execute(request);

            if (response.IsSuccessful && response.StatusCode == HttpStatusCode.OK)
            {
                RestResponseCookie SToken = response.Cookies.FirstOrDefault(x => x.Name == ".ROBLOSECURITY");

                if (SToken != null)
                {
                    SecurityToken = SToken.Value;
                    AccountManager.DelayedSaveAccounts();
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
            if (!AccountManager.GetUserID(Username, out long BlockeeID)) return false;

            RestRequest request = new RestRequest($"userblock/getblockedusers?userId={UserID}&page=1", Method.GET);

            request.AddCookie(".ROBLOSECURITY", SecurityToken);

            IRestResponse response = AccountManager.APIClient.Execute(request);

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

                    IRestResponse blockRes = AccountManager.MainClient.Execute(blockReq);

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

                    IRestResponse blockRes = AccountManager.MainClient.Execute(blockReq);

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

        public string BlockUserId(string UserID, bool SkipPinCheck = false, HttpListenerContext Context = null)
        {
            if (Context != null) Context.Response.StatusCode = 401;

            if (!SkipPinCheck && !CheckPin(true)) return "Pin Locked";

            RestRequest blockReq = new RestRequest("userblock/blockuser", Method.POST);

            blockReq.AddCookie(".ROBLOSECURITY", SecurityToken);
            blockReq.AddHeader("Referer", "https://www.roblox.com/");
            blockReq.AddHeader("X-CSRF-TOKEN", GetCSRFToken());
            blockReq.AddHeader("Content-Type", "application/json");
            blockReq.AddJsonBody(new { blockeeId = UserID });

            IRestResponse blockRes = AccountManager.MainClient.Execute(blockReq);

            if (Context != null)
                Context.Response.StatusCode = (int)blockRes.StatusCode;

            return blockRes.Content;
        }

        public string UnblockUserId(string UserID, bool SkipPinCheck = false, HttpListenerContext Context = null)
        {
            if (Context != null) Context.Response.StatusCode = 401;

            if (!SkipPinCheck && !CheckPin(true)) return "Pin Locked";

            RestRequest blockReq = new RestRequest("userblock/unblockuser", Method.POST);

            blockReq.AddCookie(".ROBLOSECURITY", SecurityToken);
            blockReq.AddHeader("Referer", "https://www.roblox.com/");
            blockReq.AddHeader("X-CSRF-TOKEN", GetCSRFToken());
            blockReq.AddHeader("Content-Type", "application/json");
            blockReq.AddJsonBody(new { blockeeId = UserID });

            IRestResponse blockRes = AccountManager.MainClient.Execute(blockReq);

            if (Context != null) Context.Response.StatusCode = (int)blockRes.StatusCode;

            return blockRes.Content;
        }

        public string UnblockEveryone(HttpListenerContext Context = null)
        {
            if (Context != null) Context.Response.StatusCode = 401;

            if (!CheckPin(true)) return "Pin is Locked";

            RestRequest request = new RestRequest($"userblock/getblockedusers?page=1", Method.GET);

            request.AddCookie(".ROBLOSECURITY", SecurityToken);

            IRestResponse response = AccountManager.APIClient.Execute(request);

            if (response.IsSuccessful && response.StatusCode == HttpStatusCode.OK)
            {
                if (Context != null) Context.Response.StatusCode = 200;

                Task.Run(async () =>
                {
                    Match R = Regex.Match(response.Content, "\"userList\":\\[(.+)\\]");

                    if (R.Success && R.Groups.Count == 2)
                    {
                        string[] UserIDs = R.Groups[1].Value.Split(',');

                        foreach (string UserId in UserIDs)
                        {
                            if (!UnblockUserId(UserId, true).Contains("true"))
                            {
                                await Task.Delay(20000);

                                UnblockUserId(UserId, true);

                                if (!CheckPin(true))
                                    break;
                            }
                        }
                    }
                });

                return "Unblocking Everyone";
            }

            if (Context != null) Context.Response.StatusCode = 400;

            return "Failed to unblock everyone";
        }

        public string GetBlockedList(HttpListenerContext Context = null)
        {
            if (Context != null) Context.Response.StatusCode = 401;

            if (!CheckPin(true)) return "Pin is Locked";

            RestRequest request = new RestRequest($"userblock/getblockedusers?page=1", Method.GET);

            request.AddCookie(".ROBLOSECURITY", SecurityToken);

            IRestResponse response = AccountManager.APIClient.Execute(request);

            if (Context != null) Context.Response.StatusCode = (int)response.StatusCode;

            return response.Content;
        }

        public string ParseAccessCode(IRestResponse response)
        {
            string pattern = "Roblox.GameLauncher.joinPrivateGame\\(\\d+\\,\\s*'(\\w+\\-\\w+\\-\\w+\\-\\w+\\-\\w+)'";
            Regex regex = new Regex(pattern);
            MatchCollection matches = regex.Matches(response.Content);

            if (matches.Count > 0 && matches[0].Groups.Count > 0)
                return matches[0].Groups[1].Value;

            return "Fail";
        }

        public string JoinServer(long PlaceID, string JobID = "", bool FollowUser = false, bool JoinVIP = false)
        {
            if (string.IsNullOrEmpty(BrowserTrackerID))
            {
                Random r = new Random();
                BrowserTrackerID = r.Next(100000, 120000).ToString() + r.Next(100000, 900000).ToString();
            }

            string Token = GetCSRFToken();

            if (string.IsNullOrEmpty(Token))
                return "ERROR: Account Session Expired, re-add the account or try again. (1)";

            if (GetAuthTicket(out string Ticket))
            {
                string LinkCode = string.IsNullOrEmpty(JobID) ? string.Empty : Regex.Match(JobID, "privateServerLinkCode=(.+)")?.Groups[1]?.Value;
                string AccessCode = JobID;

                if (!string.IsNullOrEmpty(LinkCode))
                {
                    RestRequest request = new RestRequest(string.Format("/games/{0}?privateServerLinkCode={1}", PlaceID, LinkCode), Method.GET);
                    request.AddCookie(".ROBLOSECURITY", SecurityToken);
                    request.AddHeader("X-CSRF-TOKEN", Token);
                    request.AddHeader("Referer", "https://www.roblox.com/games/606849621/Jailbreak");

                    IRestResponse response = AccountManager.MainClient.Execute(request);

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        string parsedCode = ParseAccessCode(response);

                        if (parsedCode != "Fail")
                        {
                            JoinVIP = true;
                            AccessCode = parsedCode;
                        }
                    }
                    else if (response.StatusCode == HttpStatusCode.Redirect) // thx wally (p.s. i hate wally)
                    {
                        request = new RestRequest(string.Format("/games/{0}?privateServerLinkCode={1}", PlaceID, LinkCode), Method.GET);

                        request.AddCookie(".ROBLOSECURITY", SecurityToken);
                        request.AddHeader("X-CSRF-TOKEN", Token);
                        request.AddHeader("Referer", "https://www.roblox.com/games/606849621/Jailbreak");

                        IRestResponse result = AccountManager.Web13Client.Execute(request);

                        if (result.StatusCode == HttpStatusCode.OK)
                        {
                            string parsedCode = ParseAccessCode(result);

                            if (parsedCode != "Fail")
                            {
                                JoinVIP = true;
                                AccessCode = parsedCode;
                            }
                        }
                    }
                }

                double LaunchTime = Math.Floor((DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds * 1000);

                if (AccountManager.UseOldJoin)
                {
                    string RPath = @"C:\Program Files (x86)\Roblox\Versions\" + AccountManager.CurrentVersion;

                    if (!Directory.Exists(RPath))
                        RPath = Path.Combine(Environment.GetEnvironmentVariable("LocalAppData"), @"Roblox\Versions\" + AccountManager.CurrentVersion);

                    if (!Directory.Exists(RPath))
                        return "ERROR: Failed to find ROBLOX executable";

                    RPath = RPath + @"\RobloxPlayerBeta.exe";

                    Task.Run(() => // somehow some people are crashing when roblox is tryna launch??? (probably bad executor making the process hang)
                    {
                        AccountManager.Instance.NextAccount();

                        ProcessStartInfo Roblox = new ProcessStartInfo(RPath);

                        if (JoinVIP)
                            Roblox.Arguments = string.Format("--play -a https://auth.roblox.com/v1/authentication-ticket/redeem -t {0} -j \"https://assetgame.roblox.com/game/PlaceLauncher.ashx?request=RequestPrivateGame&placeId={1}&accessCode={2}&linkCode={3}\"", Ticket, PlaceID, AccessCode, LinkCode);
                        else if (FollowUser)
                            Roblox.Arguments = string.Format("--play -a https://auth.roblox.com/v1/authentication-ticket/redeem -t {0} -j \"https://assetgame.roblox.com/game/PlaceLauncher.ashx?request=RequestFollowUser&userId={1}\"", Ticket, PlaceID);
                        else
                            Roblox.Arguments = string.Format("--play -a https://auth.roblox.com/v1/authentication-ticket/redeem -t {0} -j \"https://assetgame.roblox.com/game/PlaceLauncher.ashx?request=RequestGame{3}&placeId={1}{2}&isPlayTogetherGame=false\"", Ticket, PlaceID, "&gameId=" + JobID, string.IsNullOrEmpty(JobID) ? "" : "Job");

                        Process.Start(Roblox);
                    });

                    return "Success";
                }
                else
                {
                    Task.Run(() => // somehow some people are crashing when roblox is tryna launch??? (probably bad executor making the process hang)
                    {
                        try
                        {
                            if (JoinVIP)
                                Process.Start($"roblox-player:1+launchmode:play+gameinfo:{Ticket}+launchtime:{LaunchTime}+placelauncherurl:{HttpUtility.UrlEncode($"https://assetgame.roblox.com/game/PlaceLauncher.ashx?request=RequestPrivateGame&placeId={PlaceID}&accessCode={AccessCode}&linkCode={LinkCode}")}+browsertrackerid:{BrowserTrackerID}+robloxLocale:en_us+gameLocale:en_us+channel:").WaitForExit();
                            else if (FollowUser)
                                Process.Start($"roblox-player:1+launchmode:play+gameinfo:{Ticket}+launchtime:{LaunchTime}+placelauncherurl:{HttpUtility.UrlEncode($"https://assetgame.roblox.com/game/PlaceLauncher.ashx?request=RequestFollowUser&userId={PlaceID}")}+browsertrackerid:{BrowserTrackerID}+robloxLocale:en_us+gameLocale:en_us+channel:").WaitForExit();
                            else
                                Process.Start($"roblox-player:1+launchmode:play+gameinfo:{Ticket}+launchtime:{LaunchTime}+placelauncherurl:{HttpUtility.UrlEncode($"https://assetgame.roblox.com/game/PlaceLauncher.ashx?request=RequestGame{ (string.IsNullOrEmpty(JobID) ? "" : "Job") }&browserTrackerId={BrowserTrackerID}&placeId={PlaceID}{(string.IsNullOrEmpty(JobID) ? "" : ("&gameId=" + JobID))}&isPlayTogetherGame=false{(AccountManager.IsTeleport ? "&isTeleport=true" : "")}")}+browsertrackerid:{BrowserTrackerID}+robloxLocale:en_us+gameLocale:en_us+channel:").WaitForExit();

                            AccountManager.Instance.NextAccount();
                        }
                        catch
                        {
                            Utilities.InvokeIfRequired(AccountManager.Instance, () => MessageBox.Show("ERROR: Failed to launch roblox.\nTry reinstalling roblox", "Roblox Account Manager", MessageBoxButtons.OK, MessageBoxIcon.Error));
                            AccountManager.Instance.CancelLaunching();
                            AccountManager.Instance.NextAccount();
                        }
                    });

                    return "Success";
                }
            }
            else
                return "ERROR: Invalid Authentication Ticket";
        }

        public string SetServer(long PlaceID, string JobID, out bool Successful)
        {
            Successful = false;

            string Token = GetCSRFToken();

            if (string.IsNullOrEmpty(Token))
                return "ERROR: Account Session Expired, re-add the account or try again. (1)";

            RestRequest request = new RestRequest("v1/join-game-instance", Method.POST);
            request.AddCookie(".ROBLOSECURITY", SecurityToken);
            request.AddHeader("Content-Type", "application/json");
            request.AddJsonBody(new { gameId = JobID, placeId = PlaceID });

            AccountManager.GameJoinClient.UserAgent = "Roblox/WinInet";

            IRestResponse response = AccountManager.GameJoinClient.Execute(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                Successful = true;
                return Regex.IsMatch(response.Content, "\"joinScriptUrl\":null") ? response.Content : "Success";
            }
            else
                return $"Failed {response.StatusCode}: {response.Content} {response.ErrorMessage}";
        }

        public string LaunchApp()
        {
            if ((DateTime.Now - LastAppLaunch).TotalSeconds < 15)
                return "Woah slow down";

            if (string.IsNullOrEmpty(BrowserTrackerID))
            {
                Random r = new Random();
                BrowserTrackerID = r.Next(100000, 120000).ToString() + r.Next(100000, 900000).ToString();
            }

            if (GetAuthTicket(out string Ticket))
            {
                double LaunchTime = Math.Floor((DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds * 1000);

                Process.Start($"roblox-player:1+launchmode:app+gameinfo:{Ticket}+launchtime:{LaunchTime}+browsertrackerid:{BrowserTrackerID}+robloxLocale:en_us+gameLocale:en_us");

                return "Success";
            }
            else
                return "ERROR: Invalid Authentication Ticket";
        }

        public bool SendFriendRequest(string Username)
        {
            if (!AccountManager.GetUserID(Username, out long UserId)) return false;

            RestRequest friendRequest = new RestRequest($"/v1/users/{UserId}/request-friendship", Method.POST);
            friendRequest.AddCookie(".ROBLOSECURITY", SecurityToken);
            friendRequest.AddHeader("X-CSRF-TOKEN", GetCSRFToken());

            IRestResponse friendResponse = AccountManager.FriendsClient.Execute(friendRequest);

            return friendResponse.IsSuccessful && friendResponse.StatusCode == HttpStatusCode.OK;
        }

        public void SetDisplayName(string DisplayName)
        {
            RestRequest dpRequest = new RestRequest($"/v1/users/{UserID}/display-names", Method.PATCH);
            dpRequest.AddCookie(".ROBLOSECURITY", SecurityToken);
            dpRequest.AddHeader("X-CSRF-TOKEN", GetCSRFToken());
            dpRequest.AddJsonBody(new { newDisplayName = DisplayName });

            IRestResponse dpResponse = AccountManager.UsersClient.Execute(dpRequest);

            if (dpResponse.StatusCode != HttpStatusCode.OK)
                throw new Exception(JObject.Parse(dpResponse.Content)?["errors"]?[0]?["message"].Value<string>() ?? $"Something went wrong\n{dpResponse.StatusCode}: {dpResponse.Content}");
        }

        public string GetField(string Name) => Fields.ContainsKey(Name) ? Fields[Name] : "";
        public void SetField(string Name, string Value) { Fields[Name] = Value; AccountManager.DelayedSaveAccounts(); }
        public void RemoveField(string Name) { Fields.Remove(Name); AccountManager.DelayedSaveAccounts(); }
    }

    public class AccountJson
    {
        public long UserId { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string UserEmail { get; set; }
        public bool IsEmailVerified { get; set; }
        public int AgeBracket { get; set; }
        public bool UserAbove13 { get; set; }
    }

    public class PinStatus
    {
        public bool isEnabled { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public double unlockedUntil { get; set; }
    }
}
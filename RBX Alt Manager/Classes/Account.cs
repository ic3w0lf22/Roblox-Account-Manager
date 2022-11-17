using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RBX_Alt_Manager.Forms;
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
        public string Password
        {
            get => _Password;
            set
            {
                if (value == null || value.Length > 5000)
                    return;

                _Password = value;
                AccountManager.SaveAccounts();
            }
        }

        public Account() { }

        public Account(string Cookie)
        {
            RestRequest DataRequest = new RestRequest("my/account/json", Method.GET);

            DataRequest.AddCookie(".ROBLOSECURITY", Cookie);

            IRestResponse response = AccountManager.MainClient.Execute(DataRequest);

            if (response.StatusCode == HttpStatusCode.OK && Utilities.TryParseJson(response.Content, out AccountJson Data))
            {
                Username = Data.Name;
                UserID = Data.UserId;

                Valid = true;
                SecurityToken = Cookie;

                LastUse = DateTime.Now;

                AccountManager.LastValidAccount = this;
            }
        }

        public bool GetAuthTicket(out string Ticket)
        {
            Ticket = string.Empty;

            if (!GetCSRFToken(out string Token)) return false;

            RestRequest request = new RestRequest("/v1/authentication-ticket/", Method.POST);
            request.AddCookie(".ROBLOSECURITY", SecurityToken);
            request.AddHeader("X-CSRF-TOKEN", Token);
            request.AddHeader("Referer", "https://www.roblox.com/games/185655149/Welcome-to-Bloxburg");

            IRestResponse response = AccountManager.AuthClient.Execute(request);

            Parameter TicketHeader = response.Headers.FirstOrDefault(x => x.Name == "rbx-authentication-ticket");

            if (TicketHeader != null)
            {
                Ticket = (string)TicketHeader.Value;

                return true;
            }

            return false;
        }

        public bool GetCSRFToken(out string Result)
        {
            RestRequest request = new RestRequest("v1/authentication-ticket/", Method.POST);

            request.AddCookie(".ROBLOSECURITY", SecurityToken);
            request.AddHeader("Referer", "https://www.roblox.com/games/185655149/Welcome-to-Bloxburg");

            IRestResponse response = AccountManager.AuthClient.Execute(request);

            if (response.StatusCode != HttpStatusCode.Forbidden)
            {
                Result = $"[{(int)response.StatusCode} {response.StatusCode}] {response.Content}";
                return false;
            }

            Parameter result = response.Headers.FirstOrDefault(x => x.Name == "x-csrf-token");

            string Token = string.Empty;

            if (result != null)
            {
                Token = (string)result.Value;
                LastUse = DateTime.Now;

                AccountManager.LastValidAccount = this;
                AccountManager.SaveAccounts();
            }

            CSRFToken = Token;
            TokenSet = DateTime.Now;
            Result = Token;

            return !string.IsNullOrEmpty(Result);
        }

        public bool CheckPin(bool Internal = false)
        {
            if (!GetCSRFToken(out _))
            {
                if (!Internal) MessageBox.Show("Invalid Account Session!", "Account Manager", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }

            if (DateTime.Now < PinUnlocked)
                return true;

            RestRequest request = new RestRequest("v1/account/pin/", Method.GET);

            request.AddCookie(".ROBLOSECURITY", SecurityToken);
            request.AddHeader("Referer", "https://www.roblox.com/");

            IRestResponse response = AccountManager.AuthClient.Execute(request);

            if (response.IsSuccessful && response.StatusCode == HttpStatusCode.OK)
            {
                JObject pinInfo = JObject.Parse(response.Content);

                if (!pinInfo["isEnabled"].Value<bool>() || pinInfo["unlockedUntil"].Value<int>() > 0) return true;
            }

            if (!Internal) MessageBox.Show("Pin required!", "Account Manager", MessageBoxButtons.OK, MessageBoxIcon.Error);

            return false;
        }

        public bool UnlockPin(string Pin)
        {
            if (Pin.Length != 4) return false;
            if (CheckPin(true)) return true;

            if (!GetCSRFToken(out string Token)) return false;

            RestRequest request = new RestRequest("v1/account/pin/unlock", Method.POST);

            request.AddCookie(".ROBLOSECURITY", SecurityToken);
            request.AddHeader("Referer", "https://www.roblox.com/");
            request.AddHeader("X-CSRF-TOKEN", Token);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("pin", Pin);

            IRestResponse response = AccountManager.AuthClient.Execute(request);

            if (response.IsSuccessful && response.StatusCode == HttpStatusCode.OK)
            {
                JObject pinInfo = JObject.Parse(response.Content);

                if (pinInfo["isEnabled"].Value<bool>() && pinInfo["unlockedUntil"].Value<int>() > 0)
                    PinUnlocked = DateTime.Now.AddSeconds(pinInfo["unlockedUntil"].Value<int>());

                if (PinUnlocked > DateTime.Now)
                {
                    MessageBox.Show("Pin unlocked for 5 minutes", "Account Manager", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    return true;
                }
            }

            return false;
        }

        public JToken GetMobileInfo()
        {
            RestRequest DataRequest = new RestRequest("mobileapi/userinfo", Method.GET);

            DataRequest.AddCookie(".ROBLOSECURITY", SecurityToken);

            IRestResponse response = AccountManager.MainClient.Execute(DataRequest);

            if (response.StatusCode == HttpStatusCode.OK && Utilities.TryParseJson(response.Content, out JToken Data))
                return Data;

            return null;
        }

        public long GetRobux() => GetMobileInfo()?["RobuxBalance"]?.Value<long>() ?? 0;

        public bool SetFollowPrivacy(int Privacy)
        {
            if (!CheckPin()) return false;
            if (!GetCSRFToken(out string Token)) return false;

            RestRequest request = new RestRequest("account/settings/follow-me-privacy", Method.POST);

            request.AddCookie(".ROBLOSECURITY", SecurityToken);
            request.AddHeader("Referer", "https://www.roblox.com/my/account");
            request.AddHeader("X-CSRF-TOKEN", Token);
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
            if (!GetCSRFToken(out string Token)) return false;

            RestRequest request = new RestRequest("v2/user/passwords/change", Method.POST);

            request.AddCookie(".ROBLOSECURITY", SecurityToken);
            request.AddHeader("Referer", "https://www.roblox.com/");
            request.AddHeader("X-CSRF-TOKEN", Token);
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
            if (!GetCSRFToken(out string Token)) return false;

            RestRequest request = new RestRequest("v1/email", Method.POST);

            request.AddCookie(".ROBLOSECURITY", SecurityToken);
            request.AddHeader("Referer", "https://www.roblox.com/");
            request.AddHeader("X-CSRF-TOKEN", Token);
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
            if (!GetCSRFToken(out string Token)) return false;

            RestRequest request = new RestRequest("authentication/signoutfromallsessionsandreauthenticate", Method.POST);

            request.AddCookie(".ROBLOSECURITY", SecurityToken);
            request.AddHeader("Referer", "https://www.roblox.com/");
            request.AddHeader("X-CSRF-TOKEN", Token);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");

            IRestResponse response = AccountManager.MainClient.Execute(request);

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

        public bool TogglePlayerBlocked(string Username, ref bool Unblocked)
        {
            if (!CheckPin()) throw new Exception("Pin is Locked!");
            if (!AccountManager.GetUserID(Username, out long BlockeeID)) throw new Exception($"Failed to obtain UserId of {Username}!");

            IRestResponse BlockedResponse = GetBlockedList();

            if (!BlockedResponse.IsSuccessful) throw new Exception("Failed to obtain blocked users list!");

            string BlockedUsers = BlockedResponse.Content;

            if (!Regex.IsMatch(BlockedUsers, $"\\b{BlockeeID}\\b"))
                return BlockUserId($"{BlockeeID}").IsSuccessful;

            Unblocked = true;

            return BlockUserId($"{BlockeeID}", Unblock: true).IsSuccessful;
        }

        public IRestResponse BlockUserId(string UserID, bool SkipPinCheck = false, HttpListenerContext Context = null, bool Unblock = false)
        {
            if (Context != null) Context.Response.StatusCode = 401;
            if (!SkipPinCheck && !CheckPin(true)) throw new Exception("Pin Locked");
            if (!GetCSRFToken(out string Token)) throw new Exception("Invalid X-CSRF-Token");

            RestRequest blockReq = new RestRequest($"v1/users/{UserID}/{(Unblock ? "unblock" : "block")}", Method.POST);

            blockReq.AddCookie(".ROBLOSECURITY", SecurityToken);
            blockReq.AddHeader("X-CSRF-TOKEN", Token);

            IRestResponse blockRes = AccountManager.AccountClient.Execute(blockReq);

            Program.Logger.Info($"Block Response for {UserID} | Unblocking: {Unblock}: [{blockRes.StatusCode}] {blockRes.Content}");

            if (Context != null)
                Context.Response.StatusCode = (int)blockRes.StatusCode;

            return blockRes;
        }

        public IRestResponse UnblockUserId(string UserID, bool SkipPinCheck = false, HttpListenerContext Context = null) => BlockUserId(UserID, SkipPinCheck, Context, true);

        public string UnblockEveryone(HttpListenerContext Context = null)
        {
            if (Context != null) Context.Response.StatusCode = 401;

            if (!CheckPin(true)) return "Pin is Locked";

            IRestResponse response = GetBlockedList();

            if (response.IsSuccessful && response.StatusCode == HttpStatusCode.OK)
            {
                if (Context != null) Context.Response.StatusCode = 200;

                Task.Run(async () =>
                {
                    JObject List = JObject.Parse(response.Content);

                    if (List.ContainsKey("blockedUsers"))
                    {
                        foreach (var User in List["blockedUsers"])
                        {
                            if (!UnblockUserId(User["userId"].Value<string>(), true).IsSuccessful)
                            {
                                await Task.Delay(20000);

                                UnblockUserId(User["userId"].Value<string>(), true);

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

        public IRestResponse GetBlockedList(HttpListenerContext Context = null)
        {
            if (Context != null) Context.Response.StatusCode = 401;

            if (!CheckPin(true)) throw new Exception("Pin is Locked");

            RestRequest request = new RestRequest($"v1/users/get-detailed-blocked-users", Method.GET);

            request.AddCookie(".ROBLOSECURITY", SecurityToken);

            IRestResponse response = AccountManager.AccountClient.Execute(request);

            if (Context != null) Context.Response.StatusCode = (int)response.StatusCode;

            return response;
        }

        public bool ParseAccessCode(IRestResponse response, out string Code)
        {
            Code = "";

            Match match = Regex.Match(response.Content, "Roblox.GameLauncher.joinPrivateGame\\(\\d+\\,\\s*'(\\w+\\-\\w+\\-\\w+\\-\\w+\\-\\w+)'");
            
            if (match.Success && match.Groups.Count == 2)
            {
                Code = match.Groups[1]?.Value ?? string.Empty;

                return true;
            }

            return false;
        }

        public async Task<string> JoinServer(long PlaceID, string JobID = "", bool FollowUser = false, bool JoinVIP = false) // oh god i am not refactoring everything to be async im sorry
        {
            if (string.IsNullOrEmpty(BrowserTrackerID))
            {
                Random r = new Random();

                BrowserTrackerID = r.Next(100000, 175000).ToString() + r.Next(100000, 900000).ToString(); // oh god this is ugly
            }

            if (!GetCSRFToken(out string Token)) return $"ERROR: Account Session Expired, re-add the account or try again. (Invalid X-CSRF-Token)\n{Token}";

            if (AccountManager.ShuffleJobID && string.IsNullOrEmpty(JobID))
                JobID = await Utilities.GetRandomJobId(PlaceID);

            if (GetAuthTicket(out string Ticket))
            {
                string LinkCode = string.IsNullOrEmpty(JobID) ? string.Empty : Regex.Match(JobID, "privateServerLinkCode=(.+)")?.Groups[1]?.Value;
                string AccessCode = JobID;

                if (!string.IsNullOrEmpty(LinkCode))
                {
                    RestRequest request = new RestRequest(string.Format("/games/{0}?privateServerLinkCode={1}", PlaceID, LinkCode), Method.GET);
                    request.AddCookie(".ROBLOSECURITY", SecurityToken);
                    request.AddHeader("X-CSRF-TOKEN", Token);
                    request.AddHeader("Referer", "https://www.roblox.com/games/185655149/Welcome-to-Bloxburg");

                    IRestResponse response = await AccountManager.MainClient.ExecuteAsync(request);

                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        if (ParseAccessCode(response, out string Code))
                        {
                            JoinVIP = true;
                            AccessCode = Code;
                        }
                    }
                    else if (response.StatusCode == HttpStatusCode.Redirect) // thx wally (p.s. i hate wally)
                    {
                        request = new RestRequest(string.Format("/games/{0}?privateServerLinkCode={1}", PlaceID, LinkCode), Method.GET);

                        request.AddCookie(".ROBLOSECURITY", SecurityToken);
                        request.AddHeader("X-CSRF-TOKEN", Token);
                        request.AddHeader("Referer", "https://www.roblox.com/games/185655149/Welcome-to-Bloxburg");

                        IRestResponse result = await AccountManager.Web13Client.ExecuteAsync(request);

                        if (result.StatusCode == HttpStatusCode.OK)
                        {
                            if (ParseAccessCode(response, out string Code))
                            {
                                JoinVIP = true;
                                AccessCode = Code;
                            }
                        }
                    }
                }

                if (JoinVIP)
                {
                    var request = new RestRequest("/account/settings/private-server-invite-privacy");

                    request.AddCookie(".ROBLOSECURITY", SecurityToken);
                    request.AddHeader("X-CSRF-TOKEN", Token);
                    request.AddHeader("Referer", "https://www.roblox.com/my/account");

                    IRestResponse result = await AccountManager.MainClient.ExecuteAsync(request);

                    if (result.IsSuccessful && !result.Content.Contains("\"AllUsers\""))
                    {
                        AccountManager.Instance.InvokeIfRequired(() =>
                        {
                            if (Utilities.YesNoPrompt("Roblox Account Manager", "Account Manager has detected your account's privacy settings do not allow you to join private servers.", "Would you like to change this setting to Everyone now?"))
                            {
                                if (!CheckPin(true)) return;

                                var setRequest = new RestRequest("/account/settings/private-server-invite-privacy", Method.POST);

                                setRequest.AddCookie(".ROBLOSECURITY", SecurityToken);

                                setRequest.AddHeader("X-CSRF-TOKEN", Token);
                                setRequest.AddHeader("Referer", "https://www.roblox.com/my/account");
                                setRequest.AddHeader("Content-Type", "application/x-www-form-urlencoded");

                                setRequest.AddParameter("PrivateServerInvitePrivacy", "AllUsers");

                                AccountManager.MainClient.Execute(setRequest);
                            }
                        });
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

                    RPath += @"\RobloxPlayerBeta.exe";

                    AccountManager.Instance.NextAccount();

                    await Task.Run(() =>
                    {
                        ProcessStartInfo Roblox = new ProcessStartInfo(RPath);

                        if (JoinVIP)
                            Roblox.Arguments = string.Format("--app -t {0} -j \"https://assetgame.roblox.com/game/PlaceLauncher.ashx?request=RequestPrivateGame&placeId={1}&accessCode={2}&linkCode={3}\"", Ticket, PlaceID, AccessCode, LinkCode);
                        else if (FollowUser)
                            Roblox.Arguments = string.Format("--app -t {0} -j \"https://assetgame.roblox.com/game/PlaceLauncher.ashx?request=RequestFollowUser&userId={1}\"", Ticket, PlaceID);
                        else
                            Roblox.Arguments = string.Format("--app -t {0} -j \"https://assetgame.roblox.com/game/PlaceLauncher.ashx?request=RequestGame{3}&placeId={1}{2}&isPlayTogetherGame=false\"", Ticket, PlaceID, "&gameId=" + JobID, string.IsNullOrEmpty(JobID) ? "" : "Job");

                        Process.Start(Roblox);
                    });

                    return "Success";
                }
                else
                {
                    await Task.Run(() => // prevents roblox launcher hanging our main process
                    {
                        try
                        {
                            if (JoinVIP)
                                Process.Start($"roblox-player:1+launchmode:play+gameinfo:{Ticket}+launchtime:{LaunchTime}+placelauncherurl:{HttpUtility.UrlEncode($"https://assetgame.roblox.com/game/PlaceLauncher.ashx?request=RequestPrivateGame&placeId={PlaceID}&accessCode={AccessCode}&linkCode={LinkCode}")}+browsertrackerid:{BrowserTrackerID}+robloxLocale:en_us+gameLocale:en_us+channel:+LaunchExp:InApp").WaitForExit();
                            else if (FollowUser)
                                Process.Start($"roblox-player:1+launchmode:play+gameinfo:{Ticket}+launchtime:{LaunchTime}+placelauncherurl:{HttpUtility.UrlEncode($"https://assetgame.roblox.com/game/PlaceLauncher.ashx?request=RequestFollowUser&userId={PlaceID}")}+browsertrackerid:{BrowserTrackerID}+robloxLocale:en_us+gameLocale:en_us+channel:+LaunchExp:InApp").WaitForExit();
                            else
                                Process.Start($"roblox-player:1+launchmode:play+gameinfo:{Ticket}+launchtime:{LaunchTime}+placelauncherurl:{HttpUtility.UrlEncode($"https://assetgame.roblox.com/game/PlaceLauncher.ashx?request=RequestGame{(string.IsNullOrEmpty(JobID) ? "" : "Job")}&browserTrackerId={BrowserTrackerID}&placeId={PlaceID}{(string.IsNullOrEmpty(JobID) ? "" : ("&gameId=" + JobID))}&isPlayTogetherGame=false{(AccountManager.IsTeleport ? "&isTeleport=true" : "")}")}+browsertrackerid:{BrowserTrackerID}+robloxLocale:en_us+gameLocale:en_us+channel:+LaunchExp:InApp").WaitForExit();

                            AccountManager.Instance.NextAccount();
                        }
                        catch (Exception x)
                        {
                            Utilities.InvokeIfRequired(AccountManager.Instance, () => MessageBox.Show("ERROR: Failed to launch roblox.\nTry reinstalling roblox\n\n" + x.Message + " " + x.StackTrace, "Roblox Account Manager", MessageBoxButtons.OK, MessageBoxIcon.Error));
                            AccountManager.Instance.CancelLaunching();
                            AccountManager.Instance.NextAccount();
                        }
                    });

                    return "Success";
                }
            }
            else
                return "ERROR: Invalid Authentication Ticket, re-add the account or try again\n(Failed to get Authentication Ticket, Roblox has probably signed you out)";
        }

        public string SetServer(long PlaceID, string JobID, out bool Successful)
        {
            Successful = false;

            if (!GetCSRFToken(out string Token)) return $"ERROR: Account Session Expired, re-add the account or try again. (Invalid X-CSRF-Token)\n{Token}";

            if (string.IsNullOrEmpty(Token))
                return "ERROR: Account Session Expired, re-add the account or try again. (Invalid X-CSRF-Token)";

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
                return "ERROR: Invalid Authentication Ticket, re-add the account or try again\n(Failed to get Authentication Ticket, Roblox has probably signed you out)";
        }

        public bool SendFriendRequest(string Username)
        {
            if (!AccountManager.GetUserID(Username, out long UserId)) return false;
            if (!GetCSRFToken(out string Token)) return false;

            RestRequest friendRequest = new RestRequest($"/v1/users/{UserId}/request-friendship", Method.POST);
            friendRequest.AddCookie(".ROBLOSECURITY", SecurityToken);
            friendRequest.AddHeader("X-CSRF-TOKEN", Token);

            IRestResponse friendResponse = AccountManager.FriendsClient.Execute(friendRequest);

            return friendResponse.IsSuccessful && friendResponse.StatusCode == HttpStatusCode.OK;
        }

        public void SetDisplayName(string DisplayName)
        {
            if (!GetCSRFToken(out string Token)) return;

            RestRequest dpRequest = new RestRequest($"/v1/users/{UserID}/display-names", Method.PATCH);
            dpRequest.AddCookie(".ROBLOSECURITY", SecurityToken);
            dpRequest.AddHeader("X-CSRF-TOKEN", Token);
            dpRequest.AddJsonBody(new { newDisplayName = DisplayName });

            IRestResponse dpResponse = AccountManager.UsersClient.Execute(dpRequest);

            if (dpResponse.StatusCode != HttpStatusCode.OK)
                throw new Exception(JObject.Parse(dpResponse.Content)?["errors"]?[0]?["message"].Value<string>() ?? $"Something went wrong\n{dpResponse.StatusCode}: {dpResponse.Content}");
        }

        public void SetAvatar(string AvatarJSONData)
        {
            JObject Avatar = JObject.Parse(AvatarJSONData);

            if (!GetCSRFToken(out string Token)) return;

            RestRequest request = new RestRequest("v1/avatar/set-player-avatar-type", Method.POST);
            request.AddCookie(".ROBLOSECURITY", SecurityToken);
            request.AddHeader("X-CSRF-TOKEN", Token);
            request.AddJsonBody(new { playerAvatarType = Avatar["playerAvatarType"].Value<string>() });

            AccountManager.AvatarClient.Execute(request);

            JToken ScaleObject = Avatar.ContainsKey("scales") ? Avatar["scales"] : (Avatar.ContainsKey("scale") ? Avatar["scale"] : null);

            if (ScaleObject != null)
            {
                request = new RestRequest("v1/avatar/set-scales", Method.POST);
                request.AddCookie(".ROBLOSECURITY", SecurityToken);
                request.AddHeader("X-CSRF-TOKEN", Token);
                request.AddJsonBody(ScaleObject.ToString());

                AccountManager.AvatarClient.Execute(request);
            }

            if (Avatar.ContainsKey("bodyColors"))
            {
                request = new RestRequest("v1/avatar/set-body-colors", Method.POST);
                request.AddCookie(".ROBLOSECURITY", SecurityToken);
                request.AddHeader("X-CSRF-TOKEN", Token);
                request.AddJsonBody(Avatar["bodyColors"].ToString());

                AccountManager.AvatarClient.Execute(request);
            }

            if (Avatar.ContainsKey("assets"))
            {
                request = new RestRequest("v2/avatar/set-wearing-assets", Method.POST);
                request.AddCookie(".ROBLOSECURITY", SecurityToken);
                request.AddHeader("X-CSRF-TOKEN", Token);
                request.AddJsonBody($"{{\"assets\":{Avatar["assets"]}}}");

                IRestResponse Response = AccountManager.AvatarClient.Execute(request);

                if (Response.IsSuccessful)
                {
                    var ResponseJson = JObject.Parse(Response.Content);

                    if (ResponseJson.ContainsKey("invalidAssetIds"))
                        AccountManager.Instance.InvokeIfRequired(() => new MissingAssets(this, ResponseJson["invalidAssetIds"].Select(asset => asset.Value<long>()).ToArray()).Show());
                }
            }
        }

        public string GetField(string Name) => Fields.ContainsKey(Name) ? Fields[Name] : "";
        public void SetField(string Name, string Value) { Fields[Name] = Value; AccountManager.SaveAccounts(); }
        public void RemoveField(string Name) { Fields.Remove(Name); AccountManager.SaveAccounts(); }
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
}
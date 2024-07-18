using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RBX_Alt_Manager.Classes;
using RBX_Alt_Manager.Forms;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
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
        public DateTime LastAttemptedRefresh;
        [JsonIgnore] public DateTime PinUnlocked;
        [JsonIgnore] public DateTime TokenSet;
        [JsonIgnore] public DateTime LastAppLaunch;
        [JsonIgnore] public string CSRFToken;
        [JsonIgnore] public UserPresence Presence;

        [DllImport("user32.dll", SetLastError = true)]
        static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        public int CompareTo(Account compareTo)
        {
            if (compareTo == null)
                return 1;
            else
                return Group.CompareTo(compareTo.Group);
        }

        public string BrowserTrackerID;

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

        public Account(string Cookie, string AccountJSON = null)
        {
            SecurityToken = Cookie;
            
            AccountJSON ??= AccountManager.MainClient.Execute(MakeRequest("my/account/json", Method.Get)).Content;

            if (!string.IsNullOrEmpty(AccountJSON) && Utilities.TryParseJson(AccountJSON, out AccountJson Data))
            {
                Username = Data.Name;
                UserID = Data.UserId;

                Valid = true;

                LastUse = DateTime.Now;

                AccountManager.LastValidAccount = this;
            }
        }

        public RestRequest MakeRequest(string url, Method method = Method.Get) => new RestRequest(url, method).AddCookie(".ROBLOSECURITY", SecurityToken, "/", ".roblox.com");

        public bool GetAuthTicket(out string Ticket)
        {
            Ticket = string.Empty;

            if (!GetCSRFToken(out string Token)) return false;

            RestRequest request = MakeRequest("/v1/authentication-ticket/", Method.Post).AddHeader("X-CSRF-TOKEN", Token).AddHeader("Referer", "https://www.roblox.com/games/4924922222/Brookhaven-RP");

            RestResponse response = AccountManager.AuthClient.Execute(request);

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
            RestRequest request = MakeRequest("v1/authentication-ticket/", Method.Post).AddHeader("Referer", "https://www.roblox.com/games/4924922222/Brookhaven-RP");

            RestResponse response = AccountManager.AuthClient.Execute(request);

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

            RestRequest request = MakeRequest("v1/account/pin/", Method.Get).AddHeader("Referer", "https://www.roblox.com/");

            RestResponse response = AccountManager.AuthClient.Execute(request);

            if (response.IsSuccessful && response.StatusCode == HttpStatusCode.OK)
            {
                JObject pinInfo = JObject.Parse(response.Content);

                if (!pinInfo["isEnabled"].Value<bool>() || (pinInfo["unlockedUntil"].Type != JTokenType.Null && pinInfo["unlockedUntil"].Value<int>() > 0)) return true;
            }

            if (!Internal) MessageBox.Show("Pin required!", "Account Manager", MessageBoxButtons.OK, MessageBoxIcon.Error);

            return false;
        }

        public bool UnlockPin(string Pin)
        {
            if (Pin.Length != 4) return false;
            if (CheckPin(true)) return true;

            if (!GetCSRFToken(out string Token)) return false;

            RestRequest request = MakeRequest("v1/account/pin/unlock", Method.Post)
                .AddHeader("Referer", "https://www.roblox.com/")
                .AddHeader("X-CSRF-TOKEN", Token)
                .AddHeader("Content-Type", "application/x-www-form-urlencoded")
                .AddParameter("pin", Pin);

            RestResponse response = AccountManager.AuthClient.Execute(request);

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

        public async Task<string> GetEmailJSON()
        {
            RestRequest DataRequest = MakeRequest("v1/email", Method.Get);

            RestResponse response = await AccountManager.AccountClient.ExecuteAsync(DataRequest);

            return response.Content;
        }

        public async Task<JToken> GetMobileInfo()
        {
            RestRequest DataRequest = MakeRequest("mobileapi/userinfo", Method.Get);

            RestResponse response = await AccountManager.MainClient.ExecuteAsync(DataRequest);

            if (response.StatusCode == HttpStatusCode.OK && Utilities.TryParseJson(response.Content, out JToken Data))
                return Data;

            return null;
        }

        public async Task<JToken> GetUserInfo()
        {
            RestRequest DataRequest = MakeRequest($"v1/users/{UserID}", Method.Get);

            RestResponse response = await AccountManager.UsersClient.ExecuteAsync(DataRequest);

            if (response.StatusCode == HttpStatusCode.OK && Utilities.TryParseJson(response.Content, out JToken Data))
                return Data;

            return null;
        }

        public async Task<long> GetRobux() => (await GetMobileInfo())?["RobuxBalance"]?.Value<long>() ?? 0;

        public bool SetFollowPrivacy(int Privacy)
        {
            if (!CheckPin()) return false;
            if (!GetCSRFToken(out string Token)) return false;

            RestRequest request = MakeRequest("account/settings/follow-me-privacy", Method.Post)
                .AddHeader("Referer", "https://www.roblox.com/my/account")
                .AddHeader("X-CSRF-TOKEN", Token)
                .AddHeader("Content-Type", "application/x-www-form-urlencoded");

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

            RestResponse response = AccountManager.MainClient.Execute(request);

            if (response.IsSuccessful && response.StatusCode == HttpStatusCode.OK) return true;

            return false;
        }

        public bool ChangePassword(string Current, string New)
        {
            if (!CheckPin()) return false;
            if (!GetCSRFToken(out string Token)) return false;

            RestRequest request = MakeRequest("v2/user/passwords/change", Method.Post)
                .AddHeader("Referer", "https://www.roblox.com/")
                .AddHeader("X-CSRF-TOKEN", Token)
                .AddHeader("Content-Type", "application/x-www-form-urlencoded")
                .AddParameter("currentPassword", Current)
                .AddParameter("newPassword", New);

            RestResponse response = AccountManager.AuthClient.Execute(request);

            if (response.IsSuccessful && response.StatusCode == HttpStatusCode.OK)
            {
                Password = New;

                var SToken = response.Cookies[".ROBLOSECURITY"];

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

            RestRequest request = MakeRequest("v1/email", Method.Post)
                .AddHeader("Referer", "https://www.roblox.com/")
                .AddHeader("X-CSRF-TOKEN", Token)
                .AddHeader("Content-Type", "application/x-www-form-urlencoded")
                .AddParameter("password", Password)
                .AddParameter("emailAddress", NewEmail);

            RestResponse response = AccountManager.AccountClient.Execute(request);

            if (response.IsSuccessful && response.StatusCode == HttpStatusCode.OK)
            {
                MessageBox.Show("Email changed!", "Account Manager", MessageBoxButtons.OK, MessageBoxIcon.Information);

                return true;
            }

            MessageBox.Show("Failed to change email, maybe your password is incorrect!", "Account Manager", MessageBoxButtons.OK, MessageBoxIcon.Error);

            return false;
        }

        public bool LogOutOfOtherSessions(bool Internal = false)
        {
            if (!CheckPin(Internal)) return false;
            if (!GetCSRFToken(out string Token)) return false;

            RestRequest request = MakeRequest("authentication/signoutfromallsessionsandreauthenticate", Method.Post)
                .AddHeader("Referer", "https://www.roblox.com/")
                .AddHeader("X-CSRF-TOKEN", Token)
                .AddHeader("Content-Type", "application/x-www-form-urlencoded");

            RestResponse response = AccountManager.MainClient.Execute(request);

            if (response.IsSuccessful && response.StatusCode == HttpStatusCode.OK)
            {
                var SToken = response.Cookies[".ROBLOSECURITY"];

                if (SToken != null)
                {
                    SecurityToken = SToken.Value;
                    AccountManager.SaveAccounts(true);
                }
                else if (!Internal)
                    MessageBox.Show("An error occured, you will need to re-login!", "Account Manager", MessageBoxButtons.OK, MessageBoxIcon.Error);

                if (!Internal) MessageBox.Show("Signed out of all other sessions!", "Account Manager", MessageBoxButtons.OK, MessageBoxIcon.Information);

                return true;
            }

            if (!Internal) MessageBox.Show("Failed to log out of other sessions!", "Account Manager", MessageBoxButtons.OK, MessageBoxIcon.Error);

            return false;
        }

        public bool TogglePlayerBlocked(string Username, ref bool Unblocked)
        {
            if (!CheckPin()) throw new Exception("Pin is Locked!");
            if (!AccountManager.GetUserID(Username, out long BlockeeID, out _)) throw new Exception($"Failed to obtain UserId of {Username}!");

            RestResponse BlockedResponse = GetBlockedList();

            if (!BlockedResponse.IsSuccessful) throw new Exception("Failed to obtain blocked users list!");

            string BlockedUsers = BlockedResponse.Content;

            if (!Regex.IsMatch(BlockedUsers, $"\\b{BlockeeID}\\b"))
                return BlockUserId($"{BlockeeID}").IsSuccessful;

            Unblocked = true;

            return BlockUserId($"{BlockeeID}", Unblock: true).IsSuccessful;
        }

        public RestResponse BlockUserId(string UserID, bool SkipPinCheck = false, HttpListenerContext Context = null, bool Unblock = false)
        {
            if (Context != null) Context.Response.StatusCode = 401;
            if (!SkipPinCheck && !CheckPin(true)) throw new Exception("Pin Locked");
            if (!GetCSRFToken(out string Token)) throw new Exception("Invalid X-CSRF-Token");

            RestRequest blockReq = MakeRequest($"v1/users/{UserID}/{(Unblock ? "unblock" : "block")}", Method.Post).AddHeader("X-CSRF-TOKEN", Token);

            RestResponse blockRes = AccountManager.AccountClient.Execute(blockReq);

            Program.Logger.Info($"Block Response for {UserID} | Unblocking: {Unblock}: [{blockRes.StatusCode}] {blockRes.Content}");

            if (Context != null)
                Context.Response.StatusCode = (int)blockRes.StatusCode;

            return blockRes;
        }

        public RestResponse UnblockUserId(string UserID, bool SkipPinCheck = false, HttpListenerContext Context = null) => BlockUserId(UserID, SkipPinCheck, Context, true);

        public bool UnblockEveryone(out string Response)
        {
            if (!CheckPin(true)) { Response = "Pin is Locked"; return false; }

            RestResponse response = GetBlockedList();

            if (response.IsSuccessful && response.StatusCode == HttpStatusCode.OK)
            {
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

                Response = "Unblocking Everyone";

                return true; 
            }

            Response = "Failed to unblock everyone";

            return false;
        }

        public RestResponse GetBlockedList(HttpListenerContext Context = null)
        {
            if (Context != null) Context.Response.StatusCode = 401;

            if (!CheckPin(true)) throw new Exception("Pin is Locked");

            RestRequest request = MakeRequest($"v1/users/get-detailed-blocked-users", Method.Get);

            RestResponse response = AccountManager.AccountClient.Execute(request);

            if (Context != null) Context.Response.StatusCode = (int)response.StatusCode;

            return response;
        }

        public bool ParseAccessCode(RestResponse response, out string Code)
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

        public async Task<string> JoinServer(long PlaceID, string JobID = "", bool FollowUser = false, bool JoinVIP = false, bool Internal = false) // oh god i am not refactoring everything to be async im sorry
        {
            if (string.IsNullOrEmpty(BrowserTrackerID))
            {
                Random r = new Random();

                BrowserTrackerID = r.Next(100000, 175000).ToString() + r.Next(100000, 900000).ToString(); // oh god this is ugly
            }

            try { ClientSettingsPatcher.PatchSettings(); } catch (Exception Ex) { Program.Logger.Error($"Failed to patch ClientAppSettings: {Ex}"); }

            if (!GetCSRFToken(out string Token)) return $"ERROR: Account Session Expired, re-add the account or try again. (Invalid X-CSRF-Token)\n{Token}";

            if (AccountManager.ShuffleJobID && string.IsNullOrEmpty(JobID))
                JobID = await Utilities.GetRandomJobId(PlaceID);

            if (GetAuthTicket(out string Ticket))
            {
                if (AccountManager.General.Get<bool>("AutoCloseLastProcess"))
                {
                    try
                    {
                        foreach(Process proc in Process.GetProcessesByName("RobloxPlayerBeta"))
                        {
                            var TrackerMatch = Regex.Match(proc.GetCommandLine(), @"\-b (\d+)");
                            string TrackerID = TrackerMatch.Success ? TrackerMatch.Groups[1].Value : string.Empty;

                            if (TrackerID == BrowserTrackerID)
                            {
                                try // ignore ObjectDisposedExceptions
                                {
                                    proc.CloseMainWindow();
                                    await Task.Delay(250);
                                    proc.CloseMainWindow(); // Allows Roblox to disconnect from the server so we don't get the "Same account launched" error
                                    await Task.Delay(250);
                                    proc.Kill();
                                }
                                catch { }
                            }
                        }
                    }
                    catch (Exception x) { Program.Logger.Error($"An error occured attempting to close {Username}'s last process(es): {x}"); }
                }

                string LinkCode = string.IsNullOrEmpty(JobID) ? string.Empty : Regex.Match(JobID, "privateServerLinkCode=(.+)")?.Groups[1]?.Value;
                string AccessCode = JobID;

                if (!string.IsNullOrEmpty(LinkCode))
                {
                    RestRequest request = MakeRequest(string.Format("/games/{0}?privateServerLinkCode={1}", PlaceID, LinkCode), Method.Get).AddHeader("X-CSRF-TOKEN", Token).AddHeader("Referer", "https://www.roblox.com/games/4924922222/Brookhaven-RP");

                    RestResponse response = await AccountManager.MainClient.ExecuteAsync(request);

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
                        request = MakeRequest(string.Format("/games/{0}?privateServerLinkCode={1}", PlaceID, LinkCode), Method.Get).AddHeader("X-CSRF-TOKEN", Token).AddHeader("Referer", "https://www.roblox.com/games/4924922222/Brookhaven-RP");

                        RestResponse result = await AccountManager.Web13Client.ExecuteAsync(request);

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
                    var request = MakeRequest("/account/settings/private-server-invite-privacy").AddHeader("X-CSRF-TOKEN", Token).AddHeader("Referer", "https://www.roblox.com/my/account");

                    RestResponse result = await AccountManager.MainClient.ExecuteAsync(request);

                    if (result.IsSuccessful && !result.Content.Contains("\"AllUsers\""))
                    {
                        AccountManager.Instance.InvokeIfRequired(() =>
                        {
                            if (Utilities.YesNoPrompt("Roblox Account Manager", "Account Manager has detected your account's privacy settings do not allow you to join private servers.", "Would you like to change this setting to Everyone now?"))
                            {
                                if (!CheckPin(true)) return;

                                var setRequest = MakeRequest("/account/settings/private-server-invite-privacy", Method.Post);

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
                    });

                    _ = Task.Run(AdjustWindowPosition);

                    return "Success";
                }
                else
                {
                    await Task.Run(() => // prevents roblox launcher hanging our main process
                    {
                        try
                        {
                            ProcessStartInfo LaunchInfo = new ProcessStartInfo();

                            if (JoinVIP)
                                LaunchInfo.FileName = $"roblox-player:1+launchmode:play+gameinfo:{Ticket}+launchtime:{LaunchTime}+placelauncherurl:{HttpUtility.UrlEncode($"https://assetgame.roblox.com/game/PlaceLauncher.ashx?request=RequestPrivateGame&placeId={PlaceID}&accessCode={AccessCode}&linkCode={LinkCode}")}+browsertrackerid:{BrowserTrackerID}+robloxLocale:en_us+gameLocale:en_us+channel:+LaunchExp:InApp";
                            else if (FollowUser)
                                LaunchInfo.FileName = $"roblox-player:1+launchmode:play+gameinfo:{Ticket}+launchtime:{LaunchTime}+placelauncherurl:{HttpUtility.UrlEncode($"https://assetgame.roblox.com/game/PlaceLauncher.ashx?request=RequestFollowUser&userId={PlaceID}")}+browsertrackerid:{BrowserTrackerID}+robloxLocale:en_us+gameLocale:en_us+channel:+LaunchExp:InApp";
                            else
                                LaunchInfo.FileName = $"roblox-player:1+launchmode:play+gameinfo:{Ticket}+launchtime:{LaunchTime}+placelauncherurl:{HttpUtility.UrlEncode($"https://assetgame.roblox.com/game/PlaceLauncher.ashx?request=RequestGame{(string.IsNullOrEmpty(JobID) ? "" : "Job")}&browserTrackerId={BrowserTrackerID}&placeId={PlaceID}{(string.IsNullOrEmpty(JobID) ? "" : ("&gameId=" + JobID))}&isPlayTogetherGame=false{(AccountManager.IsTeleport ? "&isTeleport=true" : "")}")}+browsertrackerid:{BrowserTrackerID}+robloxLocale:en_us+gameLocale:en_us+channel:+LaunchExp:InApp";
                            Process Launcher = Process.Start(LaunchInfo);
                            
                            Launcher.WaitForExit();

                            AccountManager.Instance.NextAccount();

                            _ = Task.Run(AdjustWindowPosition);
                        }
                        catch (Exception x)
                        {
                            Utilities.InvokeIfRequired(AccountManager.Instance, () => MessageBox.Show($"ERROR: Failed to launch Roblox! Try re-installing Roblox.\n\n{x.Message}{x.StackTrace}", "Roblox Account Manager", MessageBoxButtons.OK, MessageBoxIcon.Error));
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

        public async void AdjustWindowPosition()
        {
            if (!RobloxWatcher.RememberWindowPositions)
                return;

            if (!(int.TryParse(GetField("Window_Position_X"), out int PosX) && int.TryParse(GetField("Window_Position_Y"), out int PosY) && int.TryParse(GetField("Window_Width"), out int Width) && int.TryParse(GetField("Window_Height"), out int Height)))
                return;

            bool Found = false;
            DateTime Ends = DateTime.Now.AddSeconds(45);

            while (true)
            {
                await Task.Delay(350);

                foreach (var process in Process.GetProcessesByName("RobloxPlayerBeta").Reverse())
                {
                    if (process.MainWindowHandle == IntPtr.Zero) continue;

                    string CommandLine = process.GetCommandLine();

                    var TrackerMatch = Regex.Match(CommandLine, @"\-b (\d+)");
                    string TrackerID = TrackerMatch.Success ? TrackerMatch.Groups[1].Value : string.Empty;

                    if (TrackerID != BrowserTrackerID) continue;

                    Found = true;

                    MoveWindow(process.MainWindowHandle, PosX, PosY, Width, Height, true);

                    break;
                }

                if (Found) break;

                if (DateTime.Now > Ends) break;
            }
        }

        public string SetServer(long PlaceID, string JobID, out bool Successful)
        {
            Successful = false;

            if (!GetCSRFToken(out string Token)) return $"ERROR: Account Session Expired, re-add the account or try again. (Invalid X-CSRF-Token)\n{Token}";

            if (string.IsNullOrEmpty(Token))
                return "ERROR: Account Session Expired, re-add the account or try again. (Invalid X-CSRF-Token)";

            RestRequest request = MakeRequest("v1/join-game-instance", Method.Post).AddHeader("Content-Type", "application/json").AddJsonBody(new { gameId = JobID, placeId = PlaceID });

            RestResponse response = AccountManager.GameJoinClient.Execute(request);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                Successful = true;
                return Regex.IsMatch(response.Content, "\"joinScriptUrl\":[%s+]?null") ? response.Content : "Success";
            }
            else
                return $"Failed {response.StatusCode}: {response.Content} {response.ErrorMessage}";
        }

        public bool SendFriendRequest(string Username)
        {
            if (!AccountManager.GetUserID(Username, out long UserId, out _)) return false;
            if (!GetCSRFToken(out string Token)) return false;

            RestRequest friendRequest = MakeRequest($"/v1/users/{UserId}/request-friendship", Method.Post).AddHeader("X-CSRF-TOKEN", Token).AddHeader("Content-Type", "application/json");

            RestResponse friendResponse = AccountManager.FriendsClient.Execute(friendRequest);

            return friendResponse.IsSuccessful && friendResponse.StatusCode == HttpStatusCode.OK;
        }

        public void SetDisplayName(string DisplayName)
        {
            if (!GetCSRFToken(out string Token)) return;

            RestRequest dpRequest = MakeRequest($"/v1/users/{UserID}/display-names", Method.Patch).AddHeader("X-CSRF-TOKEN", Token).AddJsonBody(new { newDisplayName = DisplayName });

            RestResponse dpResponse = AccountManager.UsersClient.Execute(dpRequest);

            if (dpResponse.StatusCode != HttpStatusCode.OK)
                throw new Exception(JObject.Parse(dpResponse.Content)?["errors"]?[0]?["message"].Value<string>() ?? $"Something went wrong\n{dpResponse.StatusCode}: {dpResponse.Content}");
        }

        public void SetAvatar(string AvatarJSONData)
        {
            if (string.IsNullOrEmpty(AvatarJSONData)) return;
            if (!AvatarJSONData.TryParseJson(out JObject Avatar)) return;
            if (Avatar == null) return;
            if (!GetCSRFToken(out string Token)) return;

            RestRequest request;

            if (Avatar.ContainsKey("playerAvatarType"))
            {
                request = MakeRequest("v1/avatar/set-player-avatar-type", Method.Post).AddHeader("X-CSRF-TOKEN", Token).AddJsonBody(new { playerAvatarType = Avatar["playerAvatarType"].Value<string>() });

                AccountManager.AvatarClient.Execute(request);
            }

            JToken ScaleObject = Avatar.ContainsKey("scales") ? Avatar["scales"] : (Avatar.ContainsKey("scale") ? Avatar["scale"] : null);

            if (ScaleObject != null)
            {
                request = MakeRequest("v1/avatar/set-scales", Method.Post).AddHeader("X-CSRF-TOKEN", Token).AddJsonBody(ScaleObject.ToString());

                AccountManager.AvatarClient.Execute(request);
            }

            if (Avatar.ContainsKey("bodyColors"))
            {
                request = MakeRequest("v1/avatar/set-body-colors", Method.Post).AddHeader("X-CSRF-TOKEN", Token).AddJsonBody(Avatar["bodyColors"].ToString());

                AccountManager.AvatarClient.Execute(request);
            }

            if (Avatar.ContainsKey("assets"))
            {
                request = MakeRequest("v2/avatar/set-wearing-assets", Method.Post).AddHeader("X-CSRF-TOKEN", Token).AddJsonBody($"{{\"assets\":{Avatar["assets"]}}}");

                RestResponse Response = AccountManager.AvatarClient.Execute(request);

                if (Response.IsSuccessful)
                {
                    var ResponseJson = JObject.Parse(Response.Content);

                    if (ResponseJson.ContainsKey("invalidAssetIds"))
                        AccountManager.Instance.InvokeIfRequired(() => new MissingAssets(this, ResponseJson["invalidAssetIds"].Select(asset => asset.Value<long>()).ToArray()).Show());
                }
            }
        }

        public async Task<bool> QuickLogIn(string Code)
        {
            if (string.IsNullOrEmpty(Code) || Code.Length != 6) return false;
            if (!GetCSRFToken(out string Token)) return false;

            using var API = new RestClient("https://apis.roblox.com/");
            var Response = await API.PostAsync(MakeRequest("auth-token-service/v1/login/enterCode").AddHeader("X-CSRF-TOKEN", Token).AddJsonBody(new { code = Code }));

            if (Response.IsSuccessful && Response.Content.TryParseJson(out dynamic Info))
                if (Utilities.YesNoPrompt("Quick Log In", "Please confirm you are logging in with this device", $"Device: {Info?.deviceInfo ?? "Unknown"}\nLocation: {Info?.location ?? "Unknown"}"))
                    return (await API.PostAsync(MakeRequest("auth-token-service/v1/login/validateCode").AddHeader("X-CSRF-TOKEN", Token).AddJsonBody(new { code = Code }))).IsSuccessful;

            return false;
        }

        public string GetField(string Name) => Fields.ContainsKey(Name) ? Fields[Name] : string.Empty;
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
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RBX_Alt_Manager.Classes
{
    internal class RobloxProcess
    {
        private Process RbxProcess;
        private int LogFileRetries = 0;
        public FileInfo LogFile;
        private FileStream LogStream;
        private long LastPosition = 0;
        private long CurrentDataModel;
        private bool IsDMPaused;
        private bool StreamDisposed;
        private bool IsConnected;
        private DateTime DisconnectedTime;
        private string LastLine;
        private System.Timers.Timer WaitForExitTimer;

        const string TimestampRegex = @"[\d+\-]+T[\d+:]+\.\w+Z,[\d.+]+,\w+,\d+[\s+]?";

        private static readonly Dictionary<string, string> Matches = new Dictionary<string, string>{
            { "DataModelInit", @"\[FLog::UGCGameController\] UGCGameController, initialized DataModel\((\w+)\)" },
            { "DataModelInit2", @"\[FLog::SurfaceController\] SurfaceController\[_:1\]::start dataModel\((\w+)\)" },
            { "DataModelStop", @"\[FLog::UGCGameController\] UGCGameController::leave \(blocking:\d+\) dataModel\((\w+)\)" },
            { "DataModelStop2", @"\[FLog::SurfaceController\] SurfaceController\[_:1\]::stop" },
            { "DataModelPause", @"\[FLog::SurfaceController\] SurfaceController\[_:1\]::pause dataModel\((\w+)\), view\(\w+\), destroyView:\d+\." },
            { "ReturnToApp1", @"\[FLog::SingleSurfaceApp\] returnToLuaApp: \.\.\. App not yet initialized, returning from game\." },
            { "ReturnToApp2", @"\[FLog::SingleSurfaceApp\] returnToLuaApp: \.\.\. App has been initialized, returning from game\." }
        };

        public static async void UpdateMatches()
        {
            try
            {
                using (HttpClient Client = new HttpClient())
                {
                    string MatchList = await Client.GetStringAsync("https://github.com/ic3w0lf22/Roblox-Account-Manager/raw/master/RBX%20Alt%20Manager/Resources/WatcherRegexMatches.txt");

                    foreach (string Line in MatchList.Split('\n'))
                    {
                        int Split = Line.IndexOf('='); if (Split < 0) continue;

                        string Name = Line.Substring(0, Split);
                        string Value = Line.Substring(Split + 1);

                        if (Matches.ContainsKey(Name)) Matches.Remove(Name);

                        Matches.Add(Name, Value);
                    }
                }
            }
            catch (Exception exception)
            {
                Program.Logger.Error($"Couldn't update \"Matches\" dictionary: {exception}");
            }
        }

        public RobloxProcess(Process process)
        {
            Program.Logger.Info($"New RobloxProcess created for {process.Id}");

            RbxProcess = process;

            RobloxWatcher.LogFileRead += ReadLogFile;

            Task.Run(WaitForLogPath).ContinueWith(task => { if (task.IsFaulted) Program.Logger.Error($"WaitForLogPath Error: {task.Exception}"); });

            DisconnectedTime = DateTime.Now.AddSeconds(90);

            WaitForExitTimer = new System.Timers.Timer(500);
            WaitForExitTimer.Elapsed += (s, e) =>
            {
                if (AccountManager.Watcher.Get<bool>(" ExitIfNoConnection") && AccountManager.Watcher.Get<double>("NoConnectionTimeout") is double Timeout && Timeout > 0 && !IsConnected && (DateTime.Now - DisconnectedTime).TotalSeconds is double Seconds && Seconds > Timeout)
                    KillProcess($"Lost connection for more than {Seconds} second(s)");

                try
                {
                    if (!RbxProcess.HasExited && !Program.Closed)
                        return;

                    Program.Logger.Info($"{RbxProcess.Id} has exited");

                    RobloxWatcher.LogFileRead -= ReadLogFile;

                    RobloxWatcher.Instances.Remove(this);
                    RobloxWatcher.Seen.Remove(RbxProcess.Id);

                    LogStream?.Dispose();
                    WaitForExitTimer?.Dispose();
                }
                catch (Exception x) { Program.Logger.Error($"WaitForExit Error: {x}"); }
            };
            WaitForExitTimer.Start();
        }

        private void ReadLogFile(object s, EventArgs e)
        {
            if (StreamDisposed || LogStream == null || !LogStream.CanRead) return;

            try
            {
                if (LogStream.Length > LastPosition)
                {
                    int Length = (int)(LogStream.Length - LastPosition);

                    if (Length == 0 || Length > LogStream.Length) return;

                    LogStream.Seek(-Length, SeekOrigin.End);
                    byte[] Bytes = new byte[Length];
                    LogStream.Read(Bytes, 0, Length);
                    string String = Encoding.Default.GetString(Bytes);

                    string[] Lines = String.Split('\n');

                    for (int i = 0; i < Lines.Length; i++)
                    {
                        string Line = Lines[i];

                        if (Regex.IsMatch(Line, $@"^{TimestampRegex}\[FLog::Output\] ! Joining game '[\w+\-]{{36}}' place \d+ at [\d+\.]+"))
                        {
                            IsConnected = true;

                            continue;
                        }

                        if (Regex.IsMatch(Line, $@"^{TimestampRegex}\[FLog::Network\] Sending disconnect with reason: (\d+)"))
                        {
                            IsConnected = false;
                            DisconnectedTime = DateTime.Now;

                            continue;
                        }

                        Match DMI = Regex.Match(Line, Matches["DataModelInit"]);

                        if (!DMI.Success || DMI.Groups.Count != 2)
                            DMI = Regex.Match(Line, Matches["DataModelInit2"]);

                        if (CurrentDataModel <= 0 && DMI.Success && DMI.Groups.Count == 2 && long.TryParse(DMI.Groups[1].Value, System.Globalization.NumberStyles.HexNumber, null, out CurrentDataModel))
                        {
                            IsDMPaused = false;

                            Program.Logger.Info($"CurrentDataModel set to {CurrentDataModel} ({DMI})");

                            continue;
                        }

                        long MatchedDataModel = -2;

                        Match PDM = Regex.Match(Line, Matches["DataModelPause"]);

                        if (PDM.Success && PDM.Groups.Count == 2 && long.TryParse(PDM.Groups[1].Value, System.Globalization.NumberStyles.HexNumber, null, out MatchedDataModel) && MatchedDataModel == CurrentDataModel)
                        {
                            IsDMPaused = true;

                            continue;
                        }

                        Match RTA1 = Regex.Match(Line, Matches["ReturnToApp1"]);
                        Match RTA2 = Regex.Match(Line, Matches["ReturnToApp2"]);

                        if (RTA1.Success || RTA2.Success)
                        {
                            Program.Logger.Info($"RTA1: {RTA1}");
                            Program.Logger.Info($"RTA2: {RTA2}");
                            Program.Logger.Info($"Was Paused: {IsDMPaused}");
                            Program.Logger.Info($"Current Line: {i}");

                            if (RobloxWatcher.VerifyDataModel && !string.IsNullOrEmpty(LastLine))
                            {
                                Match DMS = Regex.Match(LastLine, Matches["DataModelStop"]);

                                if (IsDMPaused || (DMS.Success && DMS.Groups.Count == 2 && long.TryParse(DMS.Groups[1].Value, System.Globalization.NumberStyles.HexNumber, null, out MatchedDataModel) && MatchedDataModel == CurrentDataModel))
                                {
                                    CurrentDataModel = -1;
                                    Program.Logger.Info($"CurrentDataModel set to {CurrentDataModel} ({DMS}) | Position: {LastPosition}");

                                    if (AccountManager.Watcher.Get<bool>("ExitOnBeta") && KillProcess("Beta home menu detected"))
                                        return;
                                }
                            }
                            else
                            {
                                if (AccountManager.Watcher.Get<bool>("ExitOnBeta") && KillProcess("Beta home menu detected"))
                                    return;
                            }
                        }

                        if (RobloxWatcher.VerifyDataModel && IsDMPaused && Regex.IsMatch(Line, Matches["DataModelStop2"]))
                            CurrentDataModel = -1;

                        LastLine = Line;
                    }

                    LastPosition = LogStream.Length;
                }
            }
            catch (Exception x) { Program.Logger.Error($"An error occured while trying to read LogFile of {RbxProcess.Id}: {x}"); }
        }

        private bool KillProcess(string Reason)
        {
            if (!(RobloxWatcher.IgnoreExistingProcesses || (!RobloxWatcher.IgnoreExistingProcesses && LastPosition > 0))) return false;

            Program.Logger.Info($"Attempting to kill process {RbxProcess.Id}, reason: {Reason}");
            StreamDisposed = true;

            LogStream.Dispose();
            RbxProcess.Kill();

            return true;
        }

        private async Task WaitForLogPath()
        {
            if (LogFileRetries > 30) return;
            if (RbxProcess.HasExited) return;

            Program.Logger.Info($"Attempting to find LogFile for {RbxProcess.Id}, retries: {LogFileRetries}");

            LogFileRetries += 1;

            ProcessStartInfo handle = new ProcessStartInfo(RobloxWatcher.HandlePath)
            {
                UseShellExecute = false,
                CreateNoWindow = true,
                RedirectStandardOutput = true,
                Arguments = "-p " + RbxProcess.Id
            };

            Process HandleProc = Process.Start(handle);

            HandleProc.WaitForExit(6000);

            StreamReader reader = HandleProc.StandardOutput;
            string output = reader.ReadToEnd();

            Match LogMatch = Regex.Match(output, @"\w+: File.+(\w+:.+\\logs\\)([\d+.]+_\w+_Player_\w+_last.log)");

            if (LogMatch.Success && LogMatch.Groups.Count == 3)
            {
                string ParentDirectory = LogMatch.Groups[1].Value;

                Program.Logger.Info($"Parent Directory: {ParentDirectory}");

                if (ParentDirectory.Contains("?")) // fix for handle returning file paths with question marks (caused by russian/etc usernames), attempts to find the logs folder manually
                {
                    string OldParentDirectory = ParentDirectory;

                    ParentDirectory = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Roblox", "logs");

                    if (!Directory.Exists(ParentDirectory))
                        throw new DirectoryNotFoundException($"Couldn't find Roblox's logs folder [{OldParentDirectory}]");
                }

                LogFile = new FileInfo(Path.Combine(ParentDirectory, LogMatch.Groups[2].Value));
                LogStream = File.Open(LogFile.FullName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

                Program.Logger.Info($"Found LogFile Path: {LogFile}");
            }
            else
            {
                await Task.Delay(1500);

                await Task.Run(WaitForLogPath);
            }
        }
    }
}
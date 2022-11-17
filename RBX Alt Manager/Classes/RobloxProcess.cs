using System;
using System.Collections.Generic;
using System.Deployment.Application;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

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

        private static readonly Dictionary<string, string> Matches = new Dictionary<string, string>{
            { "DataModelInit", @"\[FLog::UGCGameController\] UGCGameController, initialized DataModel\((\w+)\)" },
            { "DataModelInit2", @"\[FLog::SurfaceController\] SurfaceController\[_:1\]::start dataModel\((\w+)\)" },
            { "DataModelStop", @"\[FLog::UGCGameController\] UGCGameController::leave \(blocking:\d+\) dataModel\((\w+)\)" },
            { "DataModelStop2", @"\[FLog::SurfaceController\] SurfaceController\[_:1\]::stop" },
            { "DataModelPause", @"\[FLog::SurfaceController\] SurfaceController\[_:1\]::pause dataModel\((\w+)\), view\(\w+\), destroyView:\d+\." },
            { "ReturnToApp1", @"\[FLog::SingleSurfaceApp\] SingleSurfaceAppImpl::returnToLuaApp: App not yet initialized, returning from game\." },
            { "ReturnToApp2", @"\[FLog::SingleSurfaceApp\] SingleSurfaceAppImpl::returnToLuaApp: App has been initialized, returning from game\." },
        };

        public RobloxProcess(Process process)
        {
            Program.Logger.Info($"New RobloxProcess created for {process.Id}");

            RbxProcess = process;

            RobloxWatcher.LogFileRead += ReadLogFile;

            new Thread(WaitForLogPath).Start();
            new Thread(WaitForExit).Start();
        }

        private void ReadLogFile(object s, EventArgs e)
        {
            if (LogStream == null) return;

            if (LogStream.Length > LastPosition)
            {
                int Length = (int)(LogStream.Length - LastPosition);

                LogStream.Seek(-Length, SeekOrigin.End);
                byte[] Bytes = new byte[Length];
                LogStream.Read(Bytes, 0, Length);
                string String = Encoding.Default.GetString(Bytes);

                string[] Lines = String.Split('\n');

                for (int i = 0; i < Lines.Length; i++)
                {
                    string Line = Lines[i];

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

                        if (RobloxWatcher.VerifyDataModel)
                        {
                            Match DMS = Regex.Match(Lines[i - 1], Matches["DataModelStop"]); // should always have lines[i-1] unless someone somehow manages to mess this up by 1 nanosecond

                            if (IsDMPaused || (DMS.Success && DMS.Groups.Count == 2 && long.TryParse(DMS.Groups[1].Value, System.Globalization.NumberStyles.HexNumber, null, out MatchedDataModel) && MatchedDataModel == CurrentDataModel))
                            {
                                CurrentDataModel = -1;
                                Program.Logger.Info($"CurrentDataModel set to {CurrentDataModel} ({DMS}) | Position: {LastPosition}");

                                if (KillProcess())
                                    return;
                            }
                        }
                        else
                        {
                            if (KillProcess())
                                return;
                        }
                    }

                    if (RobloxWatcher.VerifyDataModel && IsDMPaused && Regex.IsMatch(Line, Matches["DataModelStop2"]))
                        CurrentDataModel = -1;
                }

                LastPosition = LogStream.Length;
            }
        }

        private bool KillProcess()
        {
            if (AccountManager.Watcher.Get<bool>("ExitOnBeta") && (RobloxWatcher.IgnoreExistingProcesses || (!RobloxWatcher.IgnoreExistingProcesses && LastPosition > 0))) // Ignore processes that were already in Beta App state
            {
                LogStream.Dispose();
                RbxProcess.Kill();

                return true;
            }

            return false;
        }

        private void WaitForLogPath()
        {
            if (LogFileRetries > 20) return;
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

            HandleProc.WaitForExit(5000);

            StreamReader reader = HandleProc.StandardOutput;
            string output = reader.ReadToEnd();

            Match LogMatch = Regex.Match(output, @"\w+: File\s+(\w+:.+\\logs\\)([\d+.]+_\w+_Player_\w+_last.log)");

            if (LogMatch.Success && LogMatch.Groups.Count == 3)
            {
                LogFile = new FileInfo(Path.Combine(LogMatch.Groups[1].Value, LogMatch.Groups[2].Value));
                LogStream = File.Open(LogFile.FullName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

                Program.Logger.Info($"Found LogFile Path: {LogFile}");
            }
            else
            {
                Thread.Sleep(500);
                new Thread(WaitForLogPath).Start();
            }
        }

        private void WaitForExit()
        {
            while (!RbxProcess.HasExited) // Process.WaitForExit errors with `Access is denied` for roblox's second process so we just check if it exists in a loop
                Thread.Sleep(200);

            Program.Logger.Info($"{RbxProcess.Id} has exited");

            RobloxWatcher.LogFileRead -= ReadLogFile;

            RobloxWatcher.Instances.Remove(this);
            RobloxWatcher.Seen.Remove(RbxProcess.Id);

            LogStream?.Dispose();
        }
    }
}
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace RBX_Alt_Manager.Classes
{
    internal class RobloxWatcher
    {
        public static readonly string HandlePath = Path.Combine(Environment.CurrentDirectory, "handle.bin");

        public static HashSet<int> Seen = new HashSet<int>();
        public static List<RobloxProcess> Instances = new List<RobloxProcess>();
        public static int ReadInterval = 250;
        public static bool VerifyDataModel = true;
        public static bool IgnoreExistingProcesses = true;
        public static bool CloseIfMemoryLow = false;
        public static bool CloseIfWindowTitle = false;
        public static int MemoryLowValue = 200;
        public static string ExpectedWindowTitle = "Roblox";
        private static bool ReadLoopActive;

        public static event EventHandler<EventArgs> LogFileRead;

        public static void CheckProcesses()
        {
            foreach (var process in Process.GetProcessesByName("RobloxPlayerBeta"))
            {
                void Kill(string Reason) { Program.Logger.Info($"Attempting to kill process {process.Id}, reason: {Reason}"); try { process.Kill(); } catch { } }

                string CommandLine = process.GetCommandLine();

                // This ignores the second roblox process which would cause 268 (Unexpected client behavior) kicks if it were closed.
                if (string.IsNullOrEmpty(CommandLine)) continue; // Roblox's second process
                if (CommandLine.StartsWith("\\??\\")) continue; // Roblox's second process
                if (!CommandLine.Contains("-t ") && !CommandLine.Contains("-j ")) continue; // Check if this process was ran with an authentcation token and a joinScript

                try
                {
                    if ((DateTime.Now - process.StartTime).TotalSeconds > 30) // Roblox shouldn't take that long to startup, right? Surely nobody will be using a potato with these settings.
                    {
                        if (CloseIfMemoryLow && process.WorkingSet64 / 1024 / 1024 < MemoryLowValue)
                            Kill($"Low Memory ({process.WorkingSet64 / 1024 / 1024} < {MemoryLowValue})");

                        if (CloseIfWindowTitle && process.MainWindowTitle != ExpectedWindowTitle)
                            Kill($"Window Title isn't {ExpectedWindowTitle}, got {process.MainWindowTitle}");
                    }
                }
                catch (Exception x){ Program.Logger.Error($"Error with checking for Memory & Window Title: {x.Message}\n{x.StackTrace}"); }

                if (Seen.Contains(process.Id)) continue;

                try
                {
                    if (process.HasExited) continue; // Will throw an exception if we have no access, wrapped in a try-catch to ignore Roblox's second process which is ran with elevated permissions

                    Instances.Add(new RobloxProcess(process));
                    Seen.Add(process.Id);
                }
                catch (Exception x) { Program.Logger.Error($"Access to Process {process.Id} denied! This may be due to roblox being ran as admin or roblox's second process(This message can be ignored): {x.Message}"); }
            }
        }

        public static void StartReadLoop()
        {
            if (ReadLoopActive) return;

            Task.Run(ReadLoop);
        }

        private static async void ReadLoop()
        {
            ReadLoopActive = true;

            while (true)
            {
                await Task.Delay(ReadInterval);

                LogFileRead?.Invoke(null, new EventArgs());
            }
        }

        public static bool IsHandleEulaAccepted()
        {
            RegistryKey AcceptedHEULA = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Sysinternals\Handle");
            object EulaObject = AcceptedHEULA?.GetValue("EulaAccepted");

            return AcceptedHEULA != null && EulaObject != null && int.TryParse(EulaObject.ToString(), out int EULA) && EULA == 1;
        }
    }
}
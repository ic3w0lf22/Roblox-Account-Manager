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
        private static bool ReadLoopActive;

        public static event EventHandler<EventArgs> LogFileRead;

        public static void CheckProcesses()
        {
            foreach (var process in Process.GetProcessesByName("RobloxPlayerBeta"))
            {
                if (Seen.Contains(process.Id)) continue;

                string CommandLine = process.GetCommandLine();

                if (string.IsNullOrEmpty(CommandLine)) continue; // Roblox's second process
                if (CommandLine.StartsWith("\\??\\")) continue; // Roblox's second process
                if (!CommandLine.Contains("-t ") && !CommandLine.Contains("-j ")) continue; // Check if this process was ran with an authentcation token and a joinScript

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
            object EulaObject = AcceptedHEULA.GetValue("EulaAccepted");

            return AcceptedHEULA != null && EulaObject != null && int.TryParse(EulaObject.ToString(), out int EULA) && EULA == 1;
        }
    }
}
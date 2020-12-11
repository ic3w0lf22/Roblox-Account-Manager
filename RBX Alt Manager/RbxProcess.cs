using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RBX_Alt_Manager
{
    public class RbxProcess
    {
        public Process RobloxProcess;
        public bool Working;
        public string LogFile;
        public string UserName;
        public long UserId;
        public long PlaceId;
        public long UniverseId;
        public string JobId;
        public int Index = -1;

        public RbxProcess(Process process)
        {
            RobloxProcess = process;
        }

        public void Setup()
        {
            ProcessStartInfo handle = new ProcessStartInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "handle.exe"));
            handle.UseShellExecute = false;
            handle.CreateNoWindow = true;
            handle.RedirectStandardOutput = true;
            handle.Arguments = "-p " + RobloxProcess.Id;
#if DEBUG
            Console.WriteLine("Getting Process Handles");
#endif
            Process p = Process.Start(handle);

            p.WaitForExit(2500);

            StreamReader reader = p.StandardOutput;
            string output = reader.ReadToEnd();

#if DEBUG
            Console.WriteLine(output);
#endif

            Match LogMatch = Regex.Match(output, @"\w+: File\s+(\w+:.+\\Roblox\\logs\\log_\w+_\w+.txt)");

#if DEBUG
            Console.WriteLine("Getting Logs File Path");
            Console.WriteLine(LogMatch.Groups.Count);
            foreach (Group g in LogMatch.Groups)
                Console.WriteLine(g.Value);
#endif

            if (LogMatch.Success && LogMatch.Groups.Count == 2)
                LogFile = LogMatch.Groups[1].Value;

            if (string.IsNullOrEmpty(LogFile) || !File.Exists(LogFile))
            {
#if DEBUG
                Console.WriteLine("Failed to get logs");
#endif
                Working = false;
                return;
            }

            string Logs = "";

#if DEBUG
            Console.WriteLine("Attempting to read logs");
#endif
            try
            {
                var r = File.Open(LogFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);

                using (FileStream fileStream = new FileStream(
                        LogFile,
                        FileMode.Open,
                        FileAccess.Read,
                        FileShare.ReadWrite))
                {
                    using (StreamReader streamReader = new StreamReader(fileStream))
                        Logs = streamReader.ReadToEnd();
                }
            }
            catch (Exception x) {
                Console.WriteLine(x.ToString());
            }

            if (string.IsNullOrEmpty(Logs))
            {
#if DEBUG
                Console.WriteLine("Logs empty");
#endif
                Working = false;
                return;
            }

            Working = true;

            Match Info = Regex.Match(Logs, @"""joinScriptUrl"":""https:\/\/assetgame\.roblox\.com\/Game\/Join\.ashx\?ticketVersion=\d&ticket=%7b%22UserId%22%3a(\d+)%2c%22UserName%22%3a%22(\w+)%22%2c%22DisplayName%22%3a%22\w+%22%2c%22CharacterFetchUrl%22%3a%22.+%22%2c%22GameId%22%3a%22(\w+\-\w+\-\w+\-\w+\-\w+)%22%2c%22PlaceId%22%3a(\d+)%2c%22UniverseId%22%3a(\d+)");

            if (Info.Success && Info.Groups.Count == 6)
            {
                UserId = Convert.ToInt64(Info.Groups[1].Value);
                UserName = Info.Groups[2].Value;
                JobId = Info.Groups[3].Value;
                PlaceId = Convert.ToInt64(Info.Groups[4].Value);
                UniverseId = Convert.ToInt64(Info.Groups[5].Value);
            }
        }
    }
}
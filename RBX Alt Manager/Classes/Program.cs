using CefSharp;
using CefSharp.WinForms;
using log4net;
using Microsoft.Win32;
using RBX_Alt_Manager.Properties;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows.Forms;

namespace RBX_Alt_Manager
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>

        public static readonly ILog Logger = LogManager.GetLogger("Account Manager");
        public static bool Closed = false; // RobloxProcess.cs would cause the program to chill in the background as long roblox was also running
        public static float Scale
        {
            get
            {
                float _Scale = (AccountManager.General != null && AccountManager.General.Exists("WindowScale")) ? AccountManager.General.Get<float>("WindowScale") : 0f;

                if (_Scale > 0)
                    return AccountManager.General.Get<float>("WindowScale");

                return 1f;
            }
        }

        public static bool ScaleFonts
        {
            get
            {
                if (AccountManager.General != null && AccountManager.General.Exists("ScaleFonts"))
                    return AccountManager.General.Get<bool>("ScaleFonts");

                return true;
            }
        }

        private static Mutex mutex = new Mutex(true, "{93b3858f-3dac-4dc0-99cb-0476efc5adce}");

        [STAThread]
        static void Main()
        {
            if (!File.Exists($"{Application.ExecutablePath}.config") || !File.Exists(Path.Combine(Environment.CurrentDirectory, "RAMTheme.ini")) || !File.Exists(Path.Combine(Environment.CurrentDirectory, "log4.config")))
            {
                File.WriteAllText($"{Application.ExecutablePath}.config", Resources.AppConfig);
                File.WriteAllText(Path.Combine(Environment.CurrentDirectory, "RAMTheme.ini"), Resources.DefaultTheme);
                File.WriteAllText(Path.Combine(Environment.CurrentDirectory, "log4.config"), Resources.Log4Config);

                Application.Restart();
                Environment.Exit(0);
            }

            const string subkey = @"SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full\";

            using (var ndpKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32)?.OpenSubKey(subkey))
            {
                if (ndpKey == null || ndpKey.GetValue("Release") == null || (int)ndpKey.GetValue("Release") < 528040)
                {
                    if (MessageBox.Show("Failed to detect .NET Framework 4.8, would you like to install it now?\n(Required in order to use the account manager)\nIn case this error persists, fully re-install the account manager.", "Roblox Account Manager", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                        Process.Start("https://dotnet.microsoft.com/en-us/download/dotnet-framework");
                }
            }

            if (mutex.WaitOne(TimeSpan.Zero, true))
            {
                try
                {
                    CefSettings settings = new CefSettings();
#if !DEBUG
                    settings.LogSeverity = LogSeverity.Disable;
#endif

                    settings.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/102.0.0.0 Safari/537.36"; // just your normal browser visiting your website @ roblox! dont hurt alt manager pls : )

                    Cef.EnableHighDPISupport();
                    Cef.Initialize(settings);
                }
                catch (Exception x)
                {
                    if (Directory.Exists(Path.Combine(Environment.CurrentDirectory, "x86")))
                    {
                        if (MessageBox.Show("Failed to detect Visual Studio Redistributable, would you like to install it now?\n(Required in order to use the account manager)\nIn case this error persists, fully re-install the account manager.\n\n" + x, "Roblox Account Manager", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                        {
                            string TempPath = Path.GetTempFileName();
                            WebClient VCDL = new WebClient();

                            VCDL.DownloadFile("https://aka.ms/vs/17/release/vc_redist.x86.exe", TempPath);

                            ProcessStartInfo VC = new ProcessStartInfo(TempPath);
                            VC.UseShellExecute = false;

                            Process.Start(VC).WaitForExit();

                            Application.Restart();
                            Environment.Exit(0);
                        }
                        else
                        {
                            MessageBox.Show("Roblox Account Manager will not run unless vcredist is installed!", "Roblox Account Manager", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Environment.Exit(1);
                        }
                    }
                    else
                    {
                        string AFN = Path.Combine(Directory.GetCurrentDirectory(), "Auto Update.exe");

                        if (File.Exists(AFN))
                        {
                            Process.Start(AFN, "skip");
                            Environment.Exit(1);
                        }
                    }
                }

                try
                {
                    string CookiesFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), @"Roblox\LocalStorage\RobloxCookies.dat");
                    bool Apply773Fix = !(string.IsNullOrEmpty(CookiesFile) || !File.Exists(CookiesFile) || File.Exists(Path.Combine(Environment.CurrentDirectory, "no773fix.txt")));

                    if (!Apply773Fix) Logger.Error($"Not applying 773 error fix | Cookies File Exists: {File.Exists(CookiesFile)} | User No Fix File Exists: {File.Exists(Path.Combine(Environment.CurrentDirectory, "no773fix.txt"))}");

                    if (Apply773Fix) try { using (new FileStream(CookiesFile, FileMode.Open, FileAccess.ReadWrite, FileShare.None)) { } } catch { Apply773Fix = false; } // Check if the file is already locked by another program

                    using (Apply773Fix ? new FileStream(CookiesFile, FileMode.Open, FileAccess.ReadWrite, FileShare.None) : null)
                    {
                        Application.EnableVisualStyles();
                        Application.SetCompatibleTextRenderingDefault(false);
                        Application.Run(new AccountManager());
                    }
                }
                finally
                {
                    mutex.ReleaseMutex();
                }
            }
            else
                MessageBox.Show("Roblox Account Manager is already running!", "Roblox Account Manager", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
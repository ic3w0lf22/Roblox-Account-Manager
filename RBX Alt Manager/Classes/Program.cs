using log4net;
using Microsoft.Win32;
using RBX_Alt_Manager.Properties;
using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Net.Http;
using System.Reflection;
using System.Security.Principal;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebSocketSharp;

namespace RBX_Alt_Manager
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>

        public static readonly ILog Logger = LogManager.GetLogger("Account Manager");
        public static bool Closed = false; // RobloxProcess.cs would cause the program to chill in the background as long roblox was also running
        public static bool Elevated;
        public static float Scale
        {
            get
            {
                float _Scale = (AccountManager.General != null && AccountManager.General.Exists("WindowScale")) ? AccountManager.General.Get<float>("WindowScale") : 0f;

                if (_Scale > 3f) AccountManager.General.RemoveProperty("WindowScale");

                if (_Scale > 0 && _Scale <= 3)
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

#if !DEBUG
        private static readonly Mutex mutex = new Mutex(true, "{93b3858f-3dac-4dc0-99cb-0476efc5adce}");
#endif

        [STAThread]
        static void Main(params string[] Arguments)
        {
            int Stupid = 1337;

            try
            {
                if (Directory.GetParent(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)).FullName.Contains(Path.GetTempPath().Remove(Path.GetTempPath().Length - 1)))
                {
                    MessageBox.Show("Roblox Account Manager must be extracted in order to function correctly!", "Roblox Account Manager", MessageBoxButtons.OK);
                    Environment.Exit(Stupid);
                }
            }
            catch { }

            try
            {
                if (new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator))
                    Elevated = true;

                // MessageBox.Show("Some features may not work properly if you ran the account manager as admin!", "Roblox Account Manager", MessageBoxButtons.OK, MessageBoxIcon.Error); // I don't think this is an issue anymore
            }
            catch { }

#if DEBUG
            bool IsAutoUpdater = true;
#else
            bool IsAutoUpdater = Application.ExecutablePath.EndsWith("Auto Update.exe");
#endif

            if (!IsAutoUpdater && !File.Exists($"{Application.ExecutablePath}.config"))
            {
                var Parent = Directory.GetParent(Application.ExecutablePath);
                var Files = Parent.GetFiles();

                if (!File.Exists(Path.Combine(Parent.FullName, "AccountData.json")) && Files.Length > 1)
                {
                    if (!Utilities.YesNoPrompt("Roblox Account Manager", "It is recommended you install Roblox Account Manager to it's own folder", "Skip this check and install here anyways?", false))
                        Environment.Exit(4);
                }
            }

            if (Arguments.Length == 1 && Arguments[0] == "-update")
            {
                if (!IsAutoUpdater)
                {
                    string AFN = Path.Combine(Directory.GetCurrentDirectory(), "Auto Update.exe");

                    File.WriteAllBytes(AFN, File.ReadAllBytes(Application.ExecutablePath));
                    Process.Start(AFN, "-update");
                    Environment.Exit(2);
                }

                static void StartUpdater() {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new Auto_Update.AutoUpdater());
                }

                if (!Elevated)
                {
                    try
                    {
                        Process.Start(new ProcessStartInfo(Application.ExecutablePath) { Arguments = "-update", Verb = "runas" });
                        Environment.Exit(0);
                    }
                    catch{ StartUpdater(); }
                }
                else
                    StartUpdater();

                return;
            }

            Application.ApplicationExit += (s, e) => Closed = true;

            if (!File.Exists(Path.Combine(Environment.CurrentDirectory, "RAMTheme.ini")))
                File.WriteAllText(Path.Combine(Environment.CurrentDirectory, "RAMTheme.ini"), Resources.DefaultTheme);

            if (!(Arguments.Length == 1 && Arguments[0] == "-restart"))
            {
                string AppConfigPath = $"{Application.ExecutablePath}.config";
                string LogConfigPath = Path.Combine(Environment.CurrentDirectory, "log4.config");
                string AppConfigHash = Utilities.FileSHA256(AppConfigPath);
                string LogConfigHash = Utilities.FileSHA256(LogConfigPath);

                bool AppConfigValid = File.Exists(AppConfigPath) && AppConfigHash == Resources.AppConfigHash;
                bool Log4ConfigValid = File.Exists(LogConfigPath) && LogConfigHash == Resources.Log4ConfigHash;

                if (!AppConfigValid || !Log4ConfigValid)
                {
                    Logger.Warn($"Restarting app due to config hash mismatch\n[App.config]\n\tCurrent:  {AppConfigHash}\n\tExpected: {Resources.AppConfigHash}\n[log4.config]\n\tCurrent:  {LogConfigHash}\n\tExpected: {Resources.Log4ConfigHash}");

                    File.WriteAllBytes(AppConfigPath, Resources.App);
                    File.WriteAllBytes(LogConfigPath, Resources.log4);

                    Process.Start(Application.ExecutablePath, "-restart");
                    Environment.Exit(0);
                }
            }

            if (!File.Exists(Path.Combine(Environment.CurrentDirectory, "libsodium.dll")))
                File.WriteAllBytes(Path.Combine(Environment.CurrentDirectory, "libsodium.dll"), Resources.libsodium);

            const string subkey = @"SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full\";

            using (var ndpKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32)?.OpenSubKey(subkey))
            {
                if (ndpKey == null || ndpKey.GetValue("Release") == null || (int)ndpKey.GetValue("Release") < 461808)
                {
                    if (MessageBox.Show("Failed to detect .NET Framework 4.7.2, would you like to install it now?\n(Required in order to use the account manager)\nIn case this error persists, fully re-install the account manager.", "Roblox Account Manager", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                        Process.Start("https://dotnet.microsoft.com/en-us/download/dotnet-framework");
                }
            }

#if !DEBUG
            if (mutex.WaitOne(TimeSpan.Zero, true))
            {
#endif
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
#if DEBUG
            finally { }
#else
                finally
                {
                    mutex.ReleaseMutex();
                }
            }
            else
                MessageBox.Show("Roblox Account Manager is already running!", "Roblox Account Manager", MessageBoxButtons.OK, MessageBoxIcon.Error);
#endif
        }
    }
}
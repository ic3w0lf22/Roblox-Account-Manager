using CefSharp;
using CefSharp.WinForms;
using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace RBX_Alt_Manager
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>

        private static Mutex mutex = new Mutex(true, "{93b3858f-3dac-4dc0-99cb-0476efc5adce}");

        [STAThread]
        static void Main()
        {
            const string subkey = @"SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full\";

            using (var ndpKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32).OpenSubKey(subkey))
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

                    settings.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/100.0.4896.88 Safari/537.36"; // just your normal browser visiting your website @ roblox! dont hurt alt manager pls : )

                    Cef.EnableHighDPISupport();
                    Cef.Initialize(settings);
                }
                catch
                {
                    if (Directory.Exists(Path.Combine(Environment.CurrentDirectory, "x86")))
                    {
                        if (MessageBox.Show("Failed to detect Visual Studio Redistrituble, would you like to install it now?\n(Required in order to use the account manager)\nIn case this error persists, fully re-install the account manager.", "Roblox Account Manager", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
                        {
                            string TempPath = Path.GetTempFileName();
                            WebClient VCDL = new WebClient();

                            VCDL.DownloadFile("https://aka.ms/vs/17/release/vc_redist.x86.exe", TempPath);

                            ProcessStartInfo VC = new ProcessStartInfo(TempPath);
                            VC.UseShellExecute = false;

                            Process.Start(VC).WaitForExit();

                            MessageBox.Show("Roblox Account Manager must be restarted in order to function", "Roblox Account Manager", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Process.Start("explorer.exe", "/select, " + Assembly.GetExecutingAssembly().Location);
                            Environment.Exit(1);
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
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new AccountManager());
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
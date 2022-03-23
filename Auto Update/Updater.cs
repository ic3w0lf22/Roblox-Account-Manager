using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;

namespace Auto_Update
{
    public partial class AutoUpdater : Form
    {
        public AutoUpdater()
        {
            InitializeComponent();
        }

        private Thread currentThread;
        private string FileName = "Update.zip";
        private string UpdatePath = Path.Combine(Environment.CurrentDirectory, "Update");
        private delegate void SafeCallDelegateSetProgress(int Progress);
        private delegate void SafeCallDelegateSetStatus(string Text);

        private void AutoUpdater_Load(object sender, EventArgs e)
        {
            string[] args = Environment.GetCommandLineArgs();
            DialogResult result;

            if (args.Length > 1 && args[1] == "skip")
                result = DialogResult.Yes;
            else
                result = MessageBox.Show($"Auto Update?", "Alt Manager Auto Updater", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (Directory.Exists(UpdatePath)) Directory.Delete(UpdatePath, true);

            FileName = Path.Combine(Directory.GetCurrentDirectory(), FileName);

            if (result == DialogResult.Yes)
            {
                currentThread = new Thread(Download);
                currentThread.Start();
            }
            else
                this.Close();
        }

        private void SetStatus(string Text)
        {
            if (Status.InvokeRequired)
            {
                SafeCallDelegateSetStatus setStatus = new SafeCallDelegateSetStatus(SetStatus);
                Status.Invoke(setStatus, new object[] { Text });
            }
            else
                Status.Text = Text;

            return;
        }
        private void SetProgress(int Progress)
        {
            if (ProgressBar.InvokeRequired)
            {
                SafeCallDelegateSetProgress setProgress = new SafeCallDelegateSetProgress(SetProgress);
                ProgressBar.Invoke(setProgress, new object[] { Progress });
            }
            else
                ProgressBar.Value = Progress;

            return;
        }

        static double B2MB(double bytes) => Math.Round((bytes / 1024f) / 1024f, 2);

        private void progressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            SetStatus($"Downloaded {string.Format("{0:0.00}", B2MB(e.BytesReceived))}MB/{string.Format("{0:0.00}", B2MB(e.TotalBytesToReceive))}MB");
            SetProgress(e.ProgressPercentage);
        }

        void downloadCompleted(object sender, AsyncCompletedEventArgs e)
        {
            currentThread.Abort();
            SetStatus("Extracting Files...");
            SetProgress(100);
            currentThread = new Thread(Extract);
            currentThread.Start();
        }

        private void Download()
        {
            WebClient WC = new WebClient();
            WC.Headers["User-Agent"] = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/81.0.4044.138 Safari/537.36";
            string Releases = WC.DownloadString("https://api.github.com/repos/ic3w0lf22/Roblox-Account-Manager/releases/tags/0.0");
            Match match = Regex.Match(Releases, @"""browser_download_url"":\s*""?([^""]+)");

            if (match.Success && match.Groups.Count >= 2)
            {
                if (match.Groups[1].Value.Contains(".rar"))
                {
                    Environment.Exit(0);
                    return;
                }

                WebClient client = new WebClient();

                client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(progressChanged);
                client.DownloadFileCompleted += new AsyncCompletedEventHandler(downloadCompleted);
                client.DownloadFileAsync(new Uri(match.Groups[1].Value), FileName);
            }
        }

        private void Extract()
        {
            try
            {
                using (ZipArchive archive = ZipFile.OpenRead(FileName))
                {
                    archive.ExtractToDirectory(UpdatePath);

                    string UP = Path.Combine(Environment.CurrentDirectory, "Update", "Auto Update.exe");

                    if (File.Exists(UP))
                        File.Move(UP, Path.Combine(Environment.CurrentDirectory, "Update", "AU.exe"));

                    foreach (string s in Directory.GetFiles(UpdatePath))
                    {
                        string FN = Path.GetFileName(s);

                        if (File.Exists(Path.Combine(Environment.CurrentDirectory, FN)))
                            File.Delete(Path.Combine(Environment.CurrentDirectory, FN));
                    }

                    foreach (string s in Directory.GetDirectories(UpdatePath))
                    {
                        string FN = Path.GetFileName(s);

                        if (Directory.Exists(Path.Combine(Environment.CurrentDirectory, FN)))
                            Directory.Delete(Path.Combine(Environment.CurrentDirectory, FN), true);
                    }

                    DirectoryInfo UpdateDir = new DirectoryInfo(UpdatePath);

                    foreach (FileInfo file in UpdateDir.GetFiles())
                        file.MoveTo(Path.Combine(Environment.CurrentDirectory, file.Name));

                    foreach (DirectoryInfo dir in UpdateDir.GetDirectories())
                    {
                        dir.MoveTo(Path.Combine(Environment.CurrentDirectory, dir.Name));

                        foreach (FileInfo file in dir.GetFiles()) // remove old files from main directory
                        {
                            if (File.Exists(Path.Combine(Environment.CurrentDirectory, file.Name)))
                                File.Delete(Path.Combine(Environment.CurrentDirectory, file.Name));
                        }
                    }
                }
            }
            catch (Exception x)
            {
                SetStatus("Error");
                Invoke(new Action(() => { MessageBox.Show(this, "Something went wrong! " + x.Message); }));
                Environment.Exit(0);
            }

            SetStatus("Done!");
            Thread.Sleep(2500);
            File.Delete(FileName);

            if (File.Exists("RBX Alt Manager.exe"))
                Process.Start("RBX Alt Manager.exe");

            Environment.Exit(0);
        }

        private void AutoUpdater_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (currentThread != null && currentThread.IsAlive)
                currentThread.Abort();
        }
    }
}
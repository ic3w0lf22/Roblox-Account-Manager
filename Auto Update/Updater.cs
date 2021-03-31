using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
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
        private delegate void SafeCallDelegateSetProgress(int Progress);
        private delegate void SafeCallDelegateSetStatus(string Text);

        private void AutoUpdater_Load(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show($"Auto Update?", "Alt Manager Auto Updater", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
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

        static string GetMD5HashFromFile(Stream stream)
        {
            using (var md5 = new MD5CryptoServiceProvider())
            {
                var buffer = md5.ComputeHash(stream);
                var sb = new StringBuilder();

                for (int i = 0; i < buffer.Length; i++)
                    sb.Append(buffer[i].ToString("x2"));

                return sb.ToString();
            }
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
            string Releases = WC.DownloadString("https://api.github.com/repos/ic3w0lf22/Roblox-Account-Manager/releases/latest");
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
                    foreach (ZipArchiveEntry newFile in archive.Entries)
                    {
                        string path = Path.Combine(Directory.GetCurrentDirectory(), newFile.Name);

                        if (newFile.Name == System.Reflection.Assembly.GetExecutingAssembly().GetName().Name + ".exe") path = Path.Combine(Directory.GetCurrentDirectory(), "AU.exe");

                        if (File.Exists(path))
                        {
                            FileStream existingStream = File.OpenRead(path);
                            Stream newStream = newFile.Open();
                            string ExistingMD5 = GetMD5HashFromFile(existingStream);
                            string NewMD5 = GetMD5HashFromFile(newStream);

                            existingStream.Close();
                            newStream.Close();

                            if (ExistingMD5 != NewMD5)
                            {
                                try
                                {
                                    Console.WriteLine("Replacing " + path);
                                    File.Delete(path);
                                    newFile.ExtractToFile(path);
                                }
                                catch (Exception x)
                                {
                                    SetStatus("Error");
                                    this.Invoke(new Action(() => { MessageBox.Show(this, "Failed to extract file, make sure the alt manager is completely closed.\n" + x.Message); }));
                                    Environment.Exit(0);
                                }
                            }
                        }
                        else newFile.ExtractToFile(path);
                    }
                }
            } catch(Exception x)
            {
                SetStatus("Error");
                this.Invoke(new Action(() => { MessageBox.Show(this, "Something went wrong! " + x.Message); }));
                Environment.Exit(0);
            }

            SetStatus("Done!"); Thread.Sleep(2500); Environment.Exit(0);
        }

        private void AutoUpdater_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (currentThread != null && currentThread.IsAlive)
                currentThread.Abort();
        }
    }
}
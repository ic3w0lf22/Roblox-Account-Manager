using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
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

        private string FileName = "Update.zip";
        private readonly string UpdatePath = Path.Combine(Environment.CurrentDirectory, "Update");
        private long TotalDownloadSize = 0;
        private delegate void SafeCallDelegateSetProgress(int Progress);
        private delegate void SafeCallDelegateSetStatus(string Text);

        private async void AutoUpdater_Load(object sender, EventArgs e)
        {
            string[] args = Environment.GetCommandLineArgs();
            bool ShouldUpdate;

            if (args.Length > 1 && args[1] == "-skip")
                ShouldUpdate = true;
            else
                ShouldUpdate = MessageBox.Show($"Auto Update?", "Alt Manager Auto Updater", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes;

            DirectoryInfo UpdateDir = new DirectoryInfo(UpdatePath);

            if (UpdateDir.Exists) UpdateDir.RecursiveDelete();

            FileName = Path.Combine(Directory.GetCurrentDirectory(), FileName);

            if (ShouldUpdate) 
                await Download();
            else
                Close();
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

        static double B2MB(double bytes) => Math.Round(bytes / 1024f / 1024f, 2);

        private void progressChanged(float value)
        {
            SetStatus(value ==1 ? "Extracting Files..." : $"Downloaded {string.Format("{0:0}", value * 100f)}% of {string.Format("{0:0.00}", B2MB(TotalDownloadSize))}MB");
            SetProgress((int)(value * 100f));
        }

        private async Task Download()
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

                using (var client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromMinutes(20);

                    string DownloadUrl = match.Groups[1].Value;

                    TotalDownloadSize = (await client.SendAsync(new HttpRequestMessage(HttpMethod.Head, DownloadUrl))).Content.Headers.ContentLength.Value;

                    Progress<float> progress = new Progress<float>(progressChanged);

                    using (var file = new FileStream(FileName, FileMode.Create, FileAccess.Write, FileShare.None))
                        await client.DownloadAsync(match.Groups[1].Value, file, progress);

                    _=Task.Run(Extract);
                }
            }
        }

        private async Task Extract()
        {
            bool ErorrOccured = false;

            try
            {
                foreach (Process p in Process.GetProcessesByName("RBX Alt Manager"))
                    p.Kill();
            }
            catch { }

#if !DEBUG
            try
#endif
            {
                using (ZipArchive archive = ZipFile.OpenRead(FileName))
                {
                    archive.ExtractToDirectory(UpdatePath);

                    //string x86Path = Path.Combine(Environment.CurrentDirectory, "x86");
                    //string HashFile = Path.Combine(UpdatePath, "x86-hash.txt");

                    //if (File.Exists(HashFile) && Program.FolderHash(x86Path) != File.ReadAllText(HashFile))
                    //    Program.RecursiveDelete(new DirectoryInfo(x86Path)); // forcefully delete ALL old files in the x86 folder instead of replacing/inserting new files into the existing folder (only updates with package updates should have this hash file)

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
                        DirectoryInfo dir = new DirectoryInfo(Path.Combine(Environment.CurrentDirectory, Path.GetFileName(s)));

                        if (dir.Exists)
                            dir.RecursiveDelete();
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

                    UpdateDir.RecursiveDelete();
                }
            }
#if !DEBUG
            catch (Exception x)
            {
                ErorrOccured = true;
                SetStatus("Error");
                Invoke(new Action(() => {
                    var Result = MessageBox.Show(this, $"Something went wrong! \n{x.Message}\n{x.StackTrace}", "Auto Updater", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);

                    if (Result == DialogResult.Retry)
                    {
                        Application.Restart();
                            Environment.Exit(0);
                        }
                    else
                        Environment.Exit(0);
                }));
            }
#endif

            if (ErorrOccured)
                return;

            SetStatus("Done!");
            await Task.Delay(2500);
            File.Delete(FileName);

            if (File.Exists("RBX Alt Manager.exe"))
                RunAsDesktopUser(Path.Combine(Environment.CurrentDirectory, "RBX Alt Manager.exe"));

            Environment.Exit(0);
        }

        private static void RunAsDesktopUser(string fileName) // ahhh, pasting. (https://stackoverflow.com/a/40501607)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(fileName));

            // To start process as shell user you will need to carry out these steps:
            // 1. Enable the SeIncreaseQuotaPrivilege in your current token
            // 2. Get an HWND representing the desktop shell (GetShellWindow)
            // 3. Get the Process ID(PID) of the process associated with that window(GetWindowThreadProcessId)
            // 4. Open that process(OpenProcess)
            // 5. Get the access token from that process (OpenProcessToken)
            // 6. Make a primary token with that token(DuplicateTokenEx)
            // 7. Start the new process with that primary token(CreateProcessWithTokenW)

            var hProcessToken = IntPtr.Zero;
            // Enable SeIncreaseQuotaPrivilege in this process.  (This won't work if current process is not elevated.)
            try
            {
                var process = GetCurrentProcess();
                if (!OpenProcessToken(process, 0x0020, ref hProcessToken))
                    return;

                var tkp = new TOKEN_PRIVILEGES
                {
                    PrivilegeCount = 1,
                    Privileges = new LUID_AND_ATTRIBUTES[1]
                };

                if (!LookupPrivilegeValue(null, "SeIncreaseQuotaPrivilege", ref tkp.Privileges[0].Luid))
                    return;

                tkp.Privileges[0].Attributes = 0x00000002;

                if (!AdjustTokenPrivileges(hProcessToken, false, ref tkp, 0, IntPtr.Zero, IntPtr.Zero))
                    return;
            }
            finally
            {
                CloseHandle(hProcessToken);
            }

            // Get an HWND representing the desktop shell.
            // CAVEATS:  This will fail if the shell is not running (crashed or terminated), or the default shell has been
            // replaced with a custom shell.  This also won't return what you probably want if Explorer has been terminated and
            // restarted elevated.
            var hwnd = GetShellWindow();
            if (hwnd == IntPtr.Zero)
                return;

            var hShellProcess = IntPtr.Zero;
            var hShellProcessToken = IntPtr.Zero;
            var hPrimaryToken = IntPtr.Zero;
            try
            {
                // Get the PID of the desktop shell process.
                if (GetWindowThreadProcessId(hwnd, out uint dwPID) == 0)
                    return;

                // Open the desktop shell process in order to query it (get the token)
                hShellProcess = OpenProcess(ProcessAccessFlags.QueryInformation, false, dwPID);
                if (hShellProcess == IntPtr.Zero)
                    return;

                // Get the process token of the desktop shell.
                if (!OpenProcessToken(hShellProcess, 0x0002, ref hShellProcessToken))
                    return;

                var dwTokenRights = 395U;

                // Duplicate the shell's process token to get a primary token.
                // Based on experimentation, this is the minimal set of rights required for CreateProcessWithTokenW (contrary to current documentation).
                if (!DuplicateTokenEx(hShellProcessToken, dwTokenRights, IntPtr.Zero, SECURITY_IMPERSONATION_LEVEL.SecurityImpersonation, TOKEN_TYPE.TokenPrimary, out hPrimaryToken))
                    return;

                // Start the target process with the new token.
                var si = new STARTUPINFO();
                var pi = new PROCESS_INFORMATION();
                if (!CreateProcessWithTokenW(hPrimaryToken, 0, fileName, "", 0, IntPtr.Zero, Path.GetDirectoryName(fileName), ref si, out pi))
                    return;
            }
            finally
            {
                CloseHandle(hShellProcessToken);
                CloseHandle(hPrimaryToken);
                CloseHandle(hShellProcess);
            }
        }

        #region Interop

        private struct TOKEN_PRIVILEGES
        {
            public UInt32 PrivilegeCount;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public LUID_AND_ATTRIBUTES[] Privileges;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 4)]
        private struct LUID_AND_ATTRIBUTES
        {
            public LUID Luid;
            public UInt32 Attributes;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct LUID
        {
            public uint LowPart;
            public int HighPart;
        }

        [Flags]
        private enum ProcessAccessFlags : uint
        {
            All = 0x001F0FFF,
            Terminate = 0x00000001,
            CreateThread = 0x00000002,
            VirtualMemoryOperation = 0x00000008,
            VirtualMemoryRead = 0x00000010,
            VirtualMemoryWrite = 0x00000020,
            DuplicateHandle = 0x00000040,
            CreateProcess = 0x000000080,
            SetQuota = 0x00000100,
            SetInformation = 0x00000200,
            QueryInformation = 0x00000400,
            QueryLimitedInformation = 0x00001000,
            Synchronize = 0x00100000
        }

        private enum SECURITY_IMPERSONATION_LEVEL
        {
            SecurityAnonymous,
            SecurityIdentification,
            SecurityImpersonation,
            SecurityDelegation
        }

        private enum TOKEN_TYPE
        {
            TokenPrimary = 1,
            TokenImpersonation
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct PROCESS_INFORMATION
        {
            public IntPtr hProcess;
            public IntPtr hThread;
            public int dwProcessId;
            public int dwThreadId;
        }

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        private struct STARTUPINFO
        {
            public Int32 cb;
            public string lpReserved;
            public string lpDesktop;
            public string lpTitle;
            public Int32 dwX;
            public Int32 dwY;
            public Int32 dwXSize;
            public Int32 dwYSize;
            public Int32 dwXCountChars;
            public Int32 dwYCountChars;
            public Int32 dwFillAttribute;
            public Int32 dwFlags;
            public Int16 wShowWindow;
            public Int16 cbReserved2;
            public IntPtr lpReserved2;
            public IntPtr hStdInput;
            public IntPtr hStdOutput;
            public IntPtr hStdError;
        }

        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static extern IntPtr GetCurrentProcess();

        [DllImport("advapi32.dll", ExactSpelling = true, SetLastError = true)]
        private static extern bool OpenProcessToken(IntPtr h, int acc, ref IntPtr phtok);

        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern bool LookupPrivilegeValue(string host, string name, ref LUID pluid);

        [DllImport("advapi32.dll", ExactSpelling = true, SetLastError = true)]
        private static extern bool AdjustTokenPrivileges(IntPtr htok, bool disall, ref TOKEN_PRIVILEGES newst, int len, IntPtr prev, IntPtr relen);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool CloseHandle(IntPtr hObject);


        [DllImport("user32.dll")]
        private static extern IntPtr GetShellWindow();

        [DllImport("user32.dll", SetLastError = true)]
        private static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr OpenProcess(ProcessAccessFlags processAccess, bool bInheritHandle, uint processId);

        [DllImport("advapi32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool DuplicateTokenEx(IntPtr hExistingToken, uint dwDesiredAccess, IntPtr lpTokenAttributes, SECURITY_IMPERSONATION_LEVEL impersonationLevel, TOKEN_TYPE tokenType, out IntPtr phNewToken);

        [DllImport("advapi32", SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern bool CreateProcessWithTokenW(IntPtr hToken, int dwLogonFlags, string lpApplicationName, string lpCommandLine, int dwCreationFlags, IntPtr lpEnvironment, string lpCurrentDirectory, [In] ref STARTUPINFO lpStartupInfo, out PROCESS_INFORMATION lpProcessInformation);

        #endregion
    }

    #region Extensions
    public static class Extensions
    {
        // https://stackoverflow.com/a/46497896
        public static async Task DownloadAsync(this HttpClient client, string requestUri, Stream destination, IProgress<float> progress = null, CancellationToken cancellationToken = default)
        {
            // Get the http headers first to examine the content length
            using (var response = await client.GetAsync(requestUri, HttpCompletionOption.ResponseHeadersRead))
            {
                var contentLength = response.Content.Headers.ContentLength;

                using (var download = await response.Content.ReadAsStreamAsync())
                {
                    // Ignore progress reporting when no progress reporter was 
                    // passed or when the content length is unknown
                    if (progress == null || !contentLength.HasValue)
                    {
                        await download.CopyToAsync(destination);
                        return;
                    }

                    // Convert absolute progress (bytes downloaded) into relative progress (0% - 100%)
                    var relativeProgress = new Progress<long>(totalBytes => progress.Report((float)totalBytes / contentLength.Value));
                    // Use extension method to report progress while downloading
                    await CopyToAsync(download, destination, 81920, relativeProgress, cancellationToken);
                    progress.Report(1);
                }
            }
        }

        public static async Task CopyToAsync(Stream source, Stream destination, int bufferSize, IProgress<long> progress = null, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            if (!source.CanRead)
                throw new ArgumentException("Has to be readable", nameof(source));
            if (destination == null)
                throw new ArgumentNullException(nameof(destination));
            if (!destination.CanWrite)
                throw new ArgumentException("Has to be writable", nameof(destination));
            if (bufferSize < 0)
                throw new ArgumentOutOfRangeException(nameof(bufferSize));

            var buffer = new byte[bufferSize];
            long totalBytesRead = 0;
            int bytesRead;
            while ((bytesRead = await source.ReadAsync(buffer, 0, buffer.Length, cancellationToken).ConfigureAwait(false)) != 0)
            {
                await destination.WriteAsync(buffer, 0, bytesRead, cancellationToken).ConfigureAwait(false);
                totalBytesRead += bytesRead;
                progress?.Report(totalBytesRead);
            }
        }
    }
    #endregion

    public class nWebClient : WebClient
    {
        protected override WebRequest GetWebRequest(Uri address)
        {
            var req = base.GetWebRequest(address);
            req.Timeout = TimeSpan.FromMinutes(30).Milliseconds;
            return req;
        }
    }
}
using System;
using System.ComponentModel;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Runtime.InteropServices;
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
                RunAsDesktopUser(Path.Combine(Environment.CurrentDirectory, "RBX Alt Manager.exe"));

            Environment.Exit(0);
        }

        private void AutoUpdater_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (currentThread != null && currentThread.IsAlive)
                currentThread.Abort();
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
                uint dwPID;
                if (GetWindowThreadProcessId(hwnd, out dwPID) == 0)
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
}
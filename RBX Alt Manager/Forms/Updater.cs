using IWshRuntimeLibrary;
using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using File = System.IO.File;

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
            DirectoryInfo UpdateDir = new DirectoryInfo(UpdatePath);

            if (UpdateDir.Exists) UpdateDir.RecursiveDelete();

            FileName = Path.Combine(Directory.GetCurrentDirectory(), FileName);

            await Download();
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
            SetStatus(value == 1 ? "Extracting Files..." : $"Downloaded {string.Format("{0:0}", value * 100f)}% of {string.Format("{0:0.00}", B2MB(TotalDownloadSize))}MB");
            SetProgress((int)(value * 100f));
        }

        private async Task Download()
        {
#if DEBUG
            await Task.Run(Extract);
#else
            using var client = new HttpClient() { Timeout = TimeSpan.FromMinutes(20) };
            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/113.0.0.0 Safari/537.36");
            string Releases = await client.GetStringAsync("https://api.github.com/repos/ic3w0lf22/Roblox-Account-Manager/releases/tags/0.0");
            Match match = Regex.Match(Releases, @"""browser_download_url"":\s*""?([^""]+)");

            if (match.Success && match.Groups.Count >= 2)
            {
                if (match.Groups[1].Value.Contains(".rar"))
                {
                    Environment.Exit(247);
                    return;
                }

                string DownloadUrl = match.Groups[1].Value;

                TotalDownloadSize = (await client.SendAsync(new HttpRequestMessage(HttpMethod.Head, DownloadUrl))).Content.Headers.ContentLength.Value;

                Progress<float> progress = new Progress<float>(progressChanged);

                using (var file = new FileStream(FileName, FileMode.Create, FileAccess.Write, FileShare.None))
                    await client.DownloadAsync(DownloadUrl, file, progress);

                await Task.Run(Extract);
            }
#endif
        }

        private void Extract()
        {
            bool ErorrOccured = false;

            try
            {
                foreach (Process p in Process.GetProcessesByName("Roblox Account Manager"))
                    if (p.Id != Process.GetCurrentProcess().Id)
                        p.Kill();
            }
            catch { }

            FileInfo Current = new FileInfo(Application.ExecutablePath);

#if !DEBUG
            try
#endif
            {
                using ZipArchive archive = ZipFile.OpenRead(FileName);
                archive.ExtractToDirectory(UpdatePath);
                bool OldExecutableExists = File.Exists(Path.Combine(Environment.CurrentDirectory, "RBX Alt Manager.exe"));

                foreach (string s in Directory.GetFiles(UpdatePath))
                {
                    string FN = Path.Combine(Environment.CurrentDirectory, Path.GetFileName(s));

#if DEBUG
                    if (FN == Application.ExecutablePath) FN = Path.Combine(Environment.CurrentDirectory, "Test.exe");
#endif

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
                    if (file.Name != Current.Name)
                        if (file.Name != "RBX Alt Manager.exe" || (file.Name == "RBX Alt Manager.exe" && OldExecutableExists)) // allows old shortcuts to keep working
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
#if !DEBUG
            catch (Exception x)
            {
                ErorrOccured = true;
                SetStatus("Error");
                Invoke(new Action(() =>
                {
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

#if !DEBUG
            File.Delete(FileName);
#endif

            string ProgramFN = Path.Combine(Environment.CurrentDirectory, "Roblox Account Manager.exe");

            if (RBX_Alt_Manager.Program.Elevated)
            {
                if (Utilities.YesNoPrompt("Roblox Account Manager", "Create Start-Menu shortcut?", "", false))
                {
                    string StartMenuPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonStartMenu), "Programs", "Roblox Account Manager");

                    if (!Directory.Exists(StartMenuPath))
                        Directory.CreateDirectory(StartMenuPath);

                    IWshShortcut shortcut = (IWshShortcut)new WshShell().CreateShortcut(Path.Combine(StartMenuPath, "Roblox Account Manager.lnk"));

                    shortcut.Description = "Roblox Account Manager";
                    shortcut.TargetPath = ProgramFN;
                    shortcut.WorkingDirectory = Directory.GetParent(ProgramFN).FullName;
                    shortcut.Save();
                }

                if (File.Exists(ProgramFN))
                    RunAsDesktopUser(ProgramFN);
            }
            else
                Process.Start(ProgramFN);

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
}
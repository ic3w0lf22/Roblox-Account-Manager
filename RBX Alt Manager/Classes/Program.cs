using System;
using System.Threading;
using System.Windows.Forms;

namespace RBX_Alt_Manager
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>

        public static AccountManager MainForm;
        private static Mutex mutex = new Mutex(true, "{93b3858f-3dac-4dc0-99cb-0476efc5adce}");

        [STAThread]
        static void Main()
        {
            if (mutex.WaitOne(TimeSpan.Zero, true))
            {
                try
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    MainForm = new AccountManager();
                    Application.Run(MainForm);
                }
                finally
                {
                    mutex.ReleaseMutex();
                }
            }
            else
                MessageBox.Show("Roblox Account Manager is already running!", "Roblox Account Manager", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static T Next<T>(this T src) where T : struct
        {
            if (!typeof(T).IsEnum) throw new ArgumentException(String.Format("Argument {0} is not an Enum", typeof(T).FullName));

            T[] Arr = (T[])Enum.GetValues(src.GetType());
            int j = Array.IndexOf<T>(Arr, src) + 1;
            return (Arr.Length == j) ? Arr[0] : Arr[j];
        }
    }
}
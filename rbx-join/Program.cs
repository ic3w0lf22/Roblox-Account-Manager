using System;
using System.Threading;
using System.Windows.Forms;

namespace rbx_join
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 
        private static Mutex mutex = new Mutex(true, "{683c0f96-797b-4055-ad30-4ec76dd68a3d}");

        [STAThread]
        static void Main(string[] args)
        {
            if (mutex.WaitOne(TimeSpan.Zero, true))
            {
                try
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new rbxjoin());
                }
                finally
                {
                    mutex.ReleaseMutex();
                }
            }
        }
    }
}
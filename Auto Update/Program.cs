using System;
using System.IO;
using System.Windows.Forms;

namespace Auto_Update
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new AutoUpdater());
        }

        public static void RecursiveDelete(this DirectoryInfo baseDir)
        {
            if (!baseDir.Exists)
                return;

            baseDir.Attributes = FileAttributes.Normal;

            foreach (var dir in baseDir.EnumerateDirectories())
                RecursiveDelete(dir);

            foreach (var file in baseDir.GetFiles())
            {
                file.IsReadOnly = false;
                file.Delete();
            }

            baseDir.Delete(true);
        }
    }
}
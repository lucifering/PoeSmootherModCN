using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace UntxPrinter.Untx.Helper
{
    class Dirs
    {

        public static void  isExitOrCreate(string path) {

            if (Directory.Exists(path))
            {
            }
            else
            {
                DirectoryInfo directoryInfo = new DirectoryInfo(path);
                directoryInfo.Create();
            }
        }

        public static string currentpath {

            get {

                return Application.StartupPath;
            }
        
        }

        public static bool isExitFile(string path)
        {

            if (path == null || path.Length < 1) {
                return false;
            }
            return File.Exists(path);
        }

    }
}

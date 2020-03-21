using System.IO;
using System.Reflection;

namespace DesktopServer.Service
{
    public static class Constants
    {
        // Local .exe folder location
        private static string localExePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        public static string LocalExePath
        {
            get
            {
                return localExePath;
            }
        }

        // Local database file location
        private static string localDBPath = LocalExePath + "\\Contents\\Database\\MobileMovieManagerDB.db";
        public static string LocalDBPath
        {
            get
            {
                if (!Directory.Exists(LocalExePath + "\\Contents\\Database\\"))
                {
                    if (!Directory.Exists(LocalExePath + "\\Contents\\"))
                    {
                        Directory.CreateDirectory(LocalExePath + "\\Contents\\");
                        Directory.CreateDirectory(LocalExePath + "\\Contents\\Database\\");
                    }
                    else
                    {
                        Directory.CreateDirectory(LocalExePath + "\\Contents\\Database\\");
                    }
                }

                return localDBPath;
            }
        }
    }
}

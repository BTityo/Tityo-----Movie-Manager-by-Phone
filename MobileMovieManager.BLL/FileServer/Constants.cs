using System.IO;
using System.Reflection;

namespace MobileMovieManager.BLL.FileServer
{
    public static class Constants
    {
        #region PC
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

        // Local images location
        private static string localImagesPath = LocalExePath + "\\Contents\\Images\\";
        public static string LocalImagesPath
        {
            get
            {
                if (!Directory.Exists(LocalExePath + "\\Contents\\Images\\"))
                {
                    if (!Directory.Exists(LocalExePath + "\\Contents\\"))
                    {
                        Directory.CreateDirectory(LocalExePath + "\\Contents\\");
                        Directory.CreateDirectory(LocalExePath + "\\Contents\\Images\\");
                    }
                    else
                    {
                        Directory.CreateDirectory(LocalExePath + "\\Contents\\Images\\");
                    }
                }

                return localImagesPath;
            }
        }
        #endregion
    }
}

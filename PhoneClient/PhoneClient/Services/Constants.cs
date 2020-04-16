using System.IO;
using Xamarin.Forms;

namespace PhoneClient.Services
{
    public static class Constants
    {
        private static string externalStoragePath = DependencyService.Get<IFileSystem>().GetExternalStorage();
        private static string dbPath = externalStoragePath + "/Database/MobileMovieManagerDB.db";

        public static string DbPath
        {
            get
            {
                if (!Directory.Exists(externalStoragePath + "/Database/"))
                {
                    Directory.CreateDirectory(externalStoragePath + "/Database/");
                }

                return dbPath;
            }
        }

    }
}

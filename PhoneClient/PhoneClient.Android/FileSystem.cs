using Android.Content;
using PhoneClient.Droid;
using PhoneClient.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileSystem))]
namespace PhoneClient.Droid
{
    public class FileSystem : IFileSystem
    {
        public string GetExternalStorage()
        {
            Context context = Android.App.Application.Context;
            var filePath = context.GetExternalFilesDir("");

            return filePath.Path;
        }
    }
}
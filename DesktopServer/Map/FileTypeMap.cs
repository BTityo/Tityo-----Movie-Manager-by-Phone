using DesktopServer.ViewModels;
using MobileMovieManager.DAL.Models;

namespace DesktopServer.Map
{
    public static class FileTypeMap
    {
        public static FileType MapToMovie(FileTypeViewModel fileTypeViewModel)
        {
            if (fileTypeViewModel != null)
            {
                return new FileType()
                {
                    TypeName = fileTypeViewModel.TypeName,
                    IsChecked = fileTypeViewModel.IsChecked
                };
            }
            else
            {
                return null;
            }
        }

        public static FileTypeViewModel MapToMovieViewModel(FileType fileType)
        {
            if (fileType != null)
            {
                return new FileTypeViewModel()
                {
                    IsChecked = fileType.IsChecked,
                    TypeName = fileType.TypeName
                };
            }
            else
            {
                return null;
            }
        }
    }
}

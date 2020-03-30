using DesktopServer.ViewModels;
using MobileMovieManager.DAL.Models;
using System.Collections.Generic;

namespace DesktopServer.Map
{
    public static class FileTypeMap
    {
        public static FileType MapToFileType(FileTypeViewModel fileTypeViewModel)
        {
            if (fileTypeViewModel != null)
            {
                return new FileType()
                {
                    Id = fileTypeViewModel.Id,
                    TypeName = fileTypeViewModel.TypeName,
                    IsChecked = fileTypeViewModel.IsChecked
                };
            }
            else
            {
                return null;
            }
        }

        public static ICollection<FileType> MapToFileTypeList(ICollection<FileTypeViewModel> fileTypeViewModels)
        {
            if (fileTypeViewModels != null && fileTypeViewModels.Count > 0)
            {
                ICollection<FileType> fileTypes = new List<FileType>();
                foreach (FileTypeViewModel fileTypeViewModel in fileTypeViewModels)
                {
                    fileTypes.Add(new FileType { Id = fileTypeViewModel.Id, IsChecked = fileTypeViewModel.IsChecked, TypeName = fileTypeViewModel.TypeName });
                }

                return fileTypes;
            }
            else
            {
                return null;
            }
        }

        public static FileTypeViewModel MapToFileTypeViewModel(FileType fileType)
        {
            if (fileType != null)
            {
                return new FileTypeViewModel()
                {
                    Id = fileType.Id,
                    IsChecked = fileType.IsChecked,
                    TypeName = fileType.TypeName
                };
            }
            else
            {
                return null;
            }
        }

        public static List<FileTypeViewModel> MapToFileTypeViewModelList(List<FileType> fileTypes)
        {
            if (fileTypes != null && fileTypes.Count > 0)
            {
                List<FileTypeViewModel> fileTypeViewModels = new List<FileTypeViewModel>();
                foreach (FileType fileType in fileTypes)
                {
                    fileTypeViewModels.Add(new FileTypeViewModel { Id = fileType.Id, IsChecked = fileType.IsChecked, TypeName = fileType.TypeName });
                }

                return fileTypeViewModels;
            }
            else
            {
                return null;
            }
        }
    }
}

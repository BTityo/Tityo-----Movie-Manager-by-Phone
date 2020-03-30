using DesktopServer.ViewModels;
using MobileMovieManager.DAL.Models;
using System.Collections.Generic;

namespace DesktopServer.Map
{
    public static class FileSizeMap
    {
        public static FileSize MapFileSizeViewModelToFileSize(FileSizeViewModel fileSizeViewModel)
        {
            if (fileSizeViewModel != null)
            {
                return new FileSize()
                {
                    Id = fileSizeViewModel.Id,
                    SizeName = fileSizeViewModel.SizeName
                };
            }
            else
            {
                return null;
            }
        }

        public static FileSizeViewModel MapToFileSizeViewModel(FileSize fileSize)
        {
            if (fileSize != null)
            {
                return new FileSizeViewModel()
                {
                    Id = fileSize.Id,
                    SizeName = fileSize.SizeName
                };
            }
            else
            {
                return new FileSizeViewModel();
            }
        }

        public static List<FileSizeViewModel> MapToFileSizeViewModelList(List<FileSize> fileSizes)
        {
            if (fileSizes != null && fileSizes.Count > 0)
            {
                List<FileSizeViewModel> fileSizeViewModels = new List<FileSizeViewModel>();
                foreach (FileSize fileSize in fileSizes)
                {
                    fileSizeViewModels.Add(new FileSizeViewModel
                    {
                        Id = fileSize.Id,
                        SizeName = fileSize.SizeName
                    });
                }

                return fileSizeViewModels;
            }
            else
            {
                return null;
            }
        }
    }
}

using MobileMovieManager.DAL.Models;
using PhoneClient.ViewModels;

namespace PhoneClient.Services.Map
{
    public static class FilterMap
    {
        public static Filter MapFilterViewModelToFilter(FilterViewModel filterViewModel)
        {
            if (filterViewModel != null)
            {
                return new Filter()
                {
                    Id = filterViewModel.Id,
                    FileSizeId = filterViewModel.SelectedSize.Id,
                    FileSize = FileSizeMap.MapFileSizeViewModelToFileSize(filterViewModel.SelectedSize),
                    FileTypeId = filterViewModel.SelectedType.Id,
                    FileType = FileTypeMap.MapToFileType(filterViewModel.SelectedType),
                    IsSeries = filterViewModel.IsSeries,
                    IsAllMovies = filterViewModel.IsAllMovies,
                    IsFavorite = filterViewModel.IsFavorite,
                    IsMoreCD = filterViewModel.IsMoreCD,
                    DateFrom = filterViewModel.DateFrom,
                    DateTo = filterViewModel.DateTo
                };
            }
            else
            {
                return null;
            }
        }

        public static FilterViewModel MapToFilterViewModel(Filter filter)
        {
            if (filter != null)
            {
                return new FilterViewModel()
                {
                    Id = filter.Id,
                    SelectedSize = FileSizeMap.MapToFileSizeViewModel(filter.FileSize),
                    SelectedType = FileTypeMap.MapToFileTypeViewModel(filter.FileType),
                    IsSeries = filter.IsSeries,
                    IsAllMovies = filter.IsAllMovies,
                    IsFavorite = filter.IsFavorite,
                    IsMoreCD = filter.IsMoreCD,
                    DateFrom = filter.DateFrom,
                    DateTo = filter.DateTo
                };
            }
            else
            {
                return new FilterViewModel();
            }
        }
    }
}

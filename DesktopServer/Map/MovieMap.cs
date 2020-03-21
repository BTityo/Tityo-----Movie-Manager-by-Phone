using DesktopServer.ViewModels;
using MobileMovieManager.DAL.Models;

namespace DesktopServer.Map
{
    public static class MovieMap
    {
        public static Movie MapToMovie(MovieViewModel movieViewModel)
        {
            if (movieViewModel != null)
            {
                return new Movie()
                {
                    Id = movieViewModel.Id,
                    Title = movieViewModel.Title,
                    FolderTitle = movieViewModel.FolderTitle,
                    FullPath = movieViewModel.FullPath,
                    FileType = movieViewModel.FileType,
                    FileTypeId = movieViewModel.FileType.Id,
                    Size = movieViewModel.Size,
                    CreationTime = movieViewModel.CreationTime,
                    IsFavorite = movieViewModel.IsFavorite
                };
            }
            else
            {
                return null;
            }
        }

        public static MovieViewModel MapToMovieViewModel(Movie movie)
        {
            if (movie != null)
            {
                return new MovieViewModel()
                {
                    Id = movie.Id,
                    Title = movie.Title,
                    FolderTitle = movie.FolderTitle,
                    FullPath = movie.FullPath,
                    FileType = movie.FileType,
                    Size = movie.Size,
                    CreationTime = movie.CreationTime,
                    IsFavorite = movie.IsFavorite
                };
            }
            else
            {
                return null;
            }
        }
    }
}

using DesktopServer.ViewModels;
using MobileMovieManager.DAL.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;

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
                    IsFavorite = movieViewModel.IsFavorite,
                    ImdbId = movieViewModel.ImdbId,
                    IsMoreCD = movieViewModel.IsMoreCD,
                    IsSeries = movieViewModel.IsSeries
                };
            }
            else
            {
                return null;
            }
        }

        public static List<Movie> MapToMovieList(ObservableCollection<MovieViewModel> movieViewModels)
        {
            List<Movie> movies = new List<Movie>();
            if (movieViewModels != null && movieViewModels.Count > 0)
            {
                foreach (MovieViewModel movieViewModel in movieViewModels)
                {
                    movies.Add(new Movie
                    {
                        Id = movieViewModel.Id,
                        Title = movieViewModel.Title,
                        FolderTitle = movieViewModel.FolderTitle,
                        FullPath = movieViewModel.FullPath,
                        FileType = movieViewModel.FileType,
                        FileTypeId = movieViewModel.FileType.Id,
                        Size = movieViewModel.Size,
                        CreationTime = movieViewModel.CreationTime,
                        IsFavorite = movieViewModel.IsFavorite,
                        ImdbId = movieViewModel.ImdbId,
                        IsMoreCD = movieViewModel.IsMoreCD,
                        IsSeries = movieViewModel.IsSeries
                    });
                }

                return movies;
            }
            else
            {
                return movies;
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
                    IsFavorite = movie.IsFavorite,
                    ImdbId = movie.ImdbId,
                    IsMoreCD = movie.IsMoreCD,
                    IsSeries = movie.IsSeries
                };
            }
            else
            {
                return null;
            }
        }

        public static ObservableCollection<MovieViewModel> MapToMovieViewModelList(List<Movie> movies)
        {
            ObservableCollection<MovieViewModel> movieViewModels = new ObservableCollection<MovieViewModel>();
            if (movies != null && movies.Count > 0)
            {
                foreach (Movie movie in movies)
                {
                    movieViewModels.Add(new MovieViewModel
                    {
                        Id = movie.Id,
                        Title = movie.Title,
                        FolderTitle = movie.FolderTitle,
                        FullPath = movie.FullPath,
                        FileType = movie.FileType,
                        Size = movie.Size,
                        CreationTime = movie.CreationTime,
                        IsFavorite = movie.IsFavorite,
                        ImdbId = movie.ImdbId,
                        IsMoreCD = movie.IsMoreCD,
                        IsSeries = movie.IsSeries
                    });
                }

                return movieViewModels;
            }
            else
            {
                return movieViewModels;
            }
        }
    }
}

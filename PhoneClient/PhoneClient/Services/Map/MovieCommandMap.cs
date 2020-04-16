using MobileMovieManager.DAL.Models;
using PhoneClient.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace PhoneClient.Services.Map
{
    public static class MovieCommandMap
    {
        public static ObservableCollection<MovieCommandViewModel> MapToMovieCommandViewModelList(List<MovieCommand> movieCommands)
        {
            if (movieCommands != null && movieCommands.Count > 0)
            {
                ObservableCollection<MovieCommandViewModel> movieCommandViewModels = new ObservableCollection<MovieCommandViewModel>();
                foreach (MovieCommand movieCommand in movieCommands)
                {
                    movieCommandViewModels.Add(new MovieCommandViewModel
                    {
                        Id = movieCommand.Id,
                        Command = movieCommand.Command,
                        CommandDescription = movieCommand.CommandDescription
                    });
                }

                return movieCommandViewModels;
            }
            else
            {
                return null;
            }
        }

    }
}

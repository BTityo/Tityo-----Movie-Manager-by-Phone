using MobileMovieManager.DAL.Models;
using MobileMovieManager.DAL.Models.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace MobileMovieManager.BLL.Service
{
    public class MovieCommandService
    {
        public List<MovieCommand> MovieCommands { get; set; }
        public List<MovieCommand> MovieCommandsNow { get; set; }

        public MovieCommandService()
        {
            MovieCommands = new List<MovieCommand>();
            MovieCommandsNow = new List<MovieCommand>();
            MovieCommands.Add(new MovieCommand { Id = 1, Command = MovieCommandEnum.MovieCommand.StartPauseMovie, CommandDescription = getDescription(MovieCommandEnum.MovieCommand.StartPauseMovie) });
            MovieCommands.Add(new MovieCommand { Id = 2, Command = MovieCommandEnum.MovieCommand.StopMovie, CommandDescription = getDescription(MovieCommandEnum.MovieCommand.StopMovie) });
            MovieCommands.Add(new MovieCommand { Id = 3, Command = MovieCommandEnum.MovieCommand.RewindMovie, CommandDescription = getDescription(MovieCommandEnum.MovieCommand.RewindMovie) });
            MovieCommands.Add(new MovieCommand { Id = 4, Command = MovieCommandEnum.MovieCommand.ForwardMovie, CommandDescription = getDescription(MovieCommandEnum.MovieCommand.ForwardMovie) });
            MovieCommands.Add(new MovieCommand { Id = 5, Command = MovieCommandEnum.MovieCommand.CloseMovie, CommandDescription = getDescription(MovieCommandEnum.MovieCommand.CloseMovie) });
            MovieCommands.Add(new MovieCommand { Id = 6, Command = MovieCommandEnum.MovieCommand.FullScreen, CommandDescription = getDescription(MovieCommandEnum.MovieCommand.FullScreen) });
            MovieCommands.Add(new MovieCommand { Id = 7, Command = MovieCommandEnum.MovieCommand.SoundPlus, CommandDescription = getDescription(MovieCommandEnum.MovieCommand.SoundPlus) });
            MovieCommands.Add(new MovieCommand { Id = 8, Command = MovieCommandEnum.MovieCommand.SoundMinus, CommandDescription = getDescription(MovieCommandEnum.MovieCommand.SoundMinus) });
            MovieCommands.Add(new MovieCommand { Id = 9, Command = MovieCommandEnum.MovieCommand.CloseClient, CommandDescription = getDescription(MovieCommandEnum.MovieCommand.CloseClient) });
            MovieCommands.Add(new MovieCommand { Id = 10, Command = MovieCommandEnum.MovieCommand.ShutdownPc, CommandDescription = getDescription(MovieCommandEnum.MovieCommand.ShutdownPc) });
            MovieCommands.Add(new MovieCommand { Id = 11, Command = MovieCommandEnum.MovieCommand.Restart, CommandDescription = getDescription(MovieCommandEnum.MovieCommand.Restart) });
            MovieCommands.Add(new MovieCommand { Id = 12, Command = MovieCommandEnum.MovieCommand.Hibernate, CommandDescription = getDescription(MovieCommandEnum.MovieCommand.Hibernate) });
            MovieCommands.Add(new MovieCommand { Id = 13, Command = MovieCommandEnum.MovieCommand.LogOff, CommandDescription = getDescription(MovieCommandEnum.MovieCommand.LogOff) });
            MovieCommands.Add(new MovieCommand { Id = 14, Command = MovieCommandEnum.MovieCommand.Sleep, CommandDescription = getDescription(MovieCommandEnum.MovieCommand.Sleep) });
            MovieCommands.Add(new MovieCommand { Id = 15, Command = MovieCommandEnum.MovieCommand.Lock, CommandDescription = getDescription(MovieCommandEnum.MovieCommand.Lock) });
            MovieCommands.Add(new MovieCommand { Id = 16, Command = MovieCommandEnum.MovieCommand.Mute, CommandDescription = getDescription(MovieCommandEnum.MovieCommand.Mute) });
            // NOW
            MovieCommandsNow.Add(new MovieCommand { Id = 1, Command = MovieCommandEnum.MovieCommand.ShutdownPcNow, CommandDescription = getDescription(MovieCommandEnum.MovieCommand.ShutdownPcNow) });
            MovieCommandsNow.Add(new MovieCommand { Id = 2, Command = MovieCommandEnum.MovieCommand.RestartNow, CommandDescription = getDescription(MovieCommandEnum.MovieCommand.RestartNow) });
            MovieCommandsNow.Add(new MovieCommand { Id = 3, Command = MovieCommandEnum.MovieCommand.HibernateNow, CommandDescription = getDescription(MovieCommandEnum.MovieCommand.HibernateNow) });
            MovieCommandsNow.Add(new MovieCommand { Id = 4, Command = MovieCommandEnum.MovieCommand.LogOffNow, CommandDescription = getDescription(MovieCommandEnum.MovieCommand.LogOffNow) });
            MovieCommandsNow.Add(new MovieCommand { Id = 5, Command = MovieCommandEnum.MovieCommand.SleepNow, CommandDescription = getDescription(MovieCommandEnum.MovieCommand.SleepNow) });
            MovieCommandsNow.Add(new MovieCommand { Id = 6, Command = MovieCommandEnum.MovieCommand.LockNow, CommandDescription = getDescription(MovieCommandEnum.MovieCommand.LockNow) });
        }

        private string getDescription(MovieCommandEnum.MovieCommand movieCommand)
        {
            FieldInfo fieldInfo = movieCommand.GetType().GetField(movieCommand.ToString());
            if (fieldInfo != null)
            {
                var attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
                return ((attributes.Length > 0) && (!String.IsNullOrEmpty(attributes[0].Description))) ? attributes[0].Description : movieCommand.ToString();
            }
            else
            {
                return movieCommand.ToString();
            }
        }
    }
}

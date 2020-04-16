using MobileMovieManager.BLL.Service;
using MobileMovieManager.DAL.Models;
using MobileMovieManager.DAL.Models.Enums;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using static MobileMovieManager.DAL.Models.Enums.LoggerEnum;

namespace MobileMovieManager.BLL.SocketServer
{
    public static class BinaryConverter
    {
        #region PC
        // Convert command binary to command enum
        public static MovieCommandEnum.MovieCommand BinaryToCommandEnum(byte[] commandBinary)
        {
            MovieCommandEnum.MovieCommand command = MovieCommandEnum.MovieCommand.None;
            if (commandBinary == null) return MovieCommandEnum.MovieCommand.None;

            BinaryFormatter binaryFormatter = new BinaryFormatter();
            using (MemoryStream memoryStream = new MemoryStream(commandBinary))
            {
                try
                {
                    memoryStream.Position = 0;
                    command = (MovieCommandEnum.MovieCommand)binaryFormatter.Deserialize(memoryStream);

                    return command;
                }
                catch (Exception ex)
                {
                    LoggerService.Instance.Loggers.Add(new Logger { Id = LoggerService.Instance.LastId + 1, Message = "Hiba a kiválasztott parancs fogadása közben:" + Environment.NewLine + ex.Message, Sender = Sender.SocketServer, LogDate = DateTime.Now });
                    return command;
                }
            }

            //int commandInt = BitConverter.ToInt32(commandBinary, 0);
            //MovieCommandEnum.MovieCommand commandEnum = (MovieCommandEnum.MovieCommand)Enum.ToObject(typeof(MovieCommandEnum.MovieCommand), commandInt);
        }

        // Convert movies list object to byte array
        //public static async Task<byte[]> MoviesObjectToBinary()
        //{
        //    byte[] moviesBinary;
        //    // get movies
        //    movieService = new MovieService(Constants.LocalDBPath);
        //    movies = await movieService.GetMoviesAsync();
        //    var movies2 = movies.Where(m => m.Id < 200).ToList();
        //    if (movies == null) return null;

        //    BinaryFormatter binaryFormatter = new BinaryFormatter();
        //    using (MemoryStream memoryStream = new MemoryStream())
        //    {
        //        memoryStream.Position = 0;
        //        binaryFormatter.Serialize(memoryStream, movies2);
        //        moviesBinary = memoryStream.ToArray();
        //    }

        //    return moviesBinary;
        //}

        // Convert byte array to one movie object
        public static Movie BinaryToMovieObject(byte[] binaryMovie)
        {
            Movie movie = null;
            if (binaryMovie == null) return null;

            BinaryFormatter binaryFormatter = new BinaryFormatter();
            using (MemoryStream memoryStream = new MemoryStream(binaryMovie))
            {
                try
                {
                    memoryStream.Position = 0;
                    movie = (Movie)binaryFormatter.Deserialize(memoryStream);

                    return movie;
                }
                catch (Exception ex)
                {
                    LoggerService.Instance.Loggers.Add(new Logger { Id = LoggerService.Instance.LastId + 1, Message = "Hiba a kiválasztott Film fogadása közben:" + Environment.NewLine + ex.Message, Sender = Sender.SocketServer, LogDate = DateTime.Now });
                    return movie;
                }
            }
        }
        #endregion

        #region ANDROID
        // Convert byte array to movies list object
        //public static List<Movie> BinaryToMoviesObject(byte[] binaryMovies)
        //{
        //    List<Movie> movies = null;
        //    if (binaryMovies == null) return null;

        //    BinaryFormatter binaryFormatter = new BinaryFormatter();
        //    using (MemoryStream memoryStream = new MemoryStream(binaryMovies))
        //    {
        //        try
        //        {
        //            memoryStream.Position = 0;
        //            movies = (List<Movie>)binaryFormatter.Deserialize(memoryStream);

        //            movies = movies.OrderBy(o => o.Title).ToList();
        //        }
        //        catch (Exception ex)
        //        {
        //            LoggerService.Instance.Loggers.Add(new Logger { Id = LoggerService.Instance.LastId + 1, Message = "Hiba a Filmek fogadás közben:" + Environment.NewLine + ex.Message, Sender = Sender.SocketClient, LogDate = DateTime.Now });
        //        }

        //        return movies;
        //    }
        //}

        // Convert movies list object or command to byte array
        public static byte[] ObjectToBinary(object clientObject)
        {
            byte[] movieBinary;
            if (clientObject == null) return null;

            BinaryFormatter binaryFormatter = new BinaryFormatter();
            using (MemoryStream memoryStream = new MemoryStream())
            {
                memoryStream.Position = 0;
                binaryFormatter.Serialize(memoryStream, clientObject);
                movieBinary = memoryStream.ToArray();
            }

            return movieBinary;
        }
        #endregion
    }
}

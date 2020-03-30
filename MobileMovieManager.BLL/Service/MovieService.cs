using MobileMovieManager.DAL.Context;
using MobileMovieManager.DAL.Models;
using MobileMovieManager.DAL.Repository;
using MobileMovieManager.DAL.Repository.IRepository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MobileMovieManager.BLL.Service
{
    public class MovieService
    {
        private IMovieRepo<Movie> movieRepo;
        private IFileTypeRepo<FileType> fileTypeRepo;

        public MovieService(string connectionString)
        {
            this.movieRepo = new MovieRepo(new MobileMovieManagerDbContext(connectionString));
            this.fileTypeRepo = new FileTypeRepo(new MobileMovieManagerDbContext(connectionString));
        }

        public MovieService(IMovieRepo<Movie> movieRepo, IFileTypeRepo<FileType> fileTypeRepo)
        {
            this.movieRepo = movieRepo;
            this.fileTypeRepo = fileTypeRepo;
        }

        /// <summary>
        /// Get all movies async
        /// </summary>
        /// <returns>Task<List<Movie>></returns>
        public async Task<List<Movie>> GetMoviesAsync()
        {
            List<Movie> movies = await movieRepo.GetAllAsync();
            movies.ForEach(async m => m.FileType = await fileTypeRepo.GetByIdAsync(m.FileTypeId));
            return await movieRepo.GetAllAsync();
        }

        /// <summary>
        /// Get movie by Id async
        /// </summary>
        /// <param name="movieId"></param>
        /// <returns>Task<Movie></returns>
        public async Task<Movie> GetMovieByIdAsync(int movieId)
        {
            return await movieRepo.GetByIdAsync(movieId);
        }

        /// <summary>
        /// Insert movie to database async
        /// </summary>
        /// <param name="movie"></param>
        /// <returns>Task<Movie></returns>
        public async Task<Movie> InsertMovieAsync(Movie movie)
        {
            return await movieRepo.InsertAsync(movie);
        }

        /// <summary>
        /// Insert all movies to database async
        /// </summary>
        /// <param name="movies"></param>
        /// <returns>Task<List<Movie>></returns>
        public async Task<List<Movie>> InsertAllMovieAsync(List<Movie> movies)
        {
            return await movieRepo.InsertAllMovieAsync(movies);
        }

        /// <summary>
        /// Update movie in database async
        /// </summary>
        /// <param name="movie"></param>
        /// <returns>Task<Movie></returns>
        public async Task<Movie> UpdateMovieAsync(Movie movie)
        {
            return await movieRepo.UpdateAsync(movie);
        }

        /// <summary>
        /// Update all movies in database async
        /// </summary>
        /// <param name="movies"></param>
        /// <returns>Task<List<Movie>></returns>
        public async Task<List<Movie>> UpdateAllMovieAsync(List<Movie> movies)
        {
            return await movieRepo.UpdateAllMovieAsync(movies);
        }

        /// <summary>
        /// Delete movie by id async
        /// </summary>
        /// <param name="movieId"></param>
        /// <returns>Task<bool></returns>
        public async Task<bool> DeleteMovieByIdAsync(int movieId)
        {
            return await movieRepo.DeleteByIdAsync(movieId);
        }

        /// <summary>
        /// Delete movies by List<int> /MovieId/ async
        /// </summary>
        /// <param name="movieIds"></param>
        /// <returns>Task<bool></returns>
        public async Task<bool> DeleteMoviesByListIdAsync(List<int> movieIds)
        {
            return await movieRepo.DeleteMoviesByListIdAsync(movieIds);
        }
    }
}

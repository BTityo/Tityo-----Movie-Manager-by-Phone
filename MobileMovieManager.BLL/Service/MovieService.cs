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

        public MovieService(string connectionString)
        {
            this.movieRepo = new MovieRepo(new MobileMovieManagerDbContext(connectionString));
        }

        public MovieService(IMovieRepo<Movie> movieRepo)
        {
            this.movieRepo = movieRepo;
        }

        /// <summary>
        /// Get all movies async
        /// </summary>
        /// <returns>Task<List<Movie>></returns>
        public async Task<List<Movie>> GetMoviesAsync()
        {
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
        /// Update movie in database async
        /// </summary>
        /// <param name="movie"></param>
        /// <returns>Task<Movie></returns>
        public async Task<Movie> UpdateMovieAsync(Movie movie)
        {
            return await movieRepo.UpdateAsync(movie);
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
    }
}

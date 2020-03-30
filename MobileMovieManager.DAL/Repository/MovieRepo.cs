using Microsoft.EntityFrameworkCore;
using MobileMovieManager.DAL.Context;
using MobileMovieManager.DAL.Models;
using MobileMovieManager.DAL.Repository.IRepository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileMovieManager.DAL.Repository
{
    public class MovieRepo : BaseRepo<Movie>, IMovieRepo<Movie>
    {
        private MobileMovieManagerDbContext context;

        public MovieRepo(MobileMovieManagerDbContext context) : base(context)
        {
            this.context = context;
        }

        public async Task<bool> DeleteMoviesByListIdAsync(List<int> movieIds)
        {
            bool isSuccessAllDelete = true;

            // Check delete is successful
            foreach (int movieId in movieIds)
            {
                var model = await context.Set<Movie>().FindAsync(movieId);

                if (model != null)
                {
                    context.Set<Movie>().Remove(model);
                    isSuccessAllDelete = true;
                }
                else
                {
                    isSuccessAllDelete = false;
                }
            }

            await SaveAsync();

            return isSuccessAllDelete;
        }

        public async Task<List<Movie>> InsertAllMovieAsync(List<Movie> movies)
        {
            List<Movie> insertedMovies = new List<Movie>();
            foreach (Movie movie in movies)
            {
                context.Set<Movie>().Local.Add(movie);
                insertedMovies.Add(movie);
            }

            await SaveAsync();

            return insertedMovies;
        }

        public async Task<List<Movie>> UpdateAllMovieAsync(List<Movie> movies)
        {
            foreach (Movie movie in movies)
            {
                Movie localMovie = context.Set<Movie>().Local.FirstOrDefault(m => m.Id == movie.Id);
                if (localMovie != null)
                {
                    context.Entry(localMovie).State = EntityState.Detached;
                }

                context.Entry(movie).State = EntityState.Modified;
            }

            await SaveAsync();

            return movies;
        }
    }
}

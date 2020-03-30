using MobileMovieManager.DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MobileMovieManager.DAL.Repository.IRepository
{
    public interface IMovieRepo<T> : IBaseRepo<T> where T : class
    {
        Task<List<Movie>> UpdateAllMovieAsync(List<Movie> movies);

        Task<List<Movie>> InsertAllMovieAsync(List<Movie> movies);

        Task<bool> DeleteMoviesByListIdAsync(List<int> movieIds);
    }
}

using MobileMovieManager.BLL.FileServer;
using MobileMovieManager.DAL.Context;
using MobileMovieManager.DAL.Models;
using MobileMovieManager.DAL.Repository;
using MobileMovieManager.DAL.Repository.IRepository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MobileMovieManager.BLL.Service
{
    public class FilterService
    {
        private IFilterRepo<Filter> filterRepo;

        public FilterService(string connectionString)
        {
            this.filterRepo = new FilterRepo(new MobileMovieManagerDbContext(connectionString));
        }

        public FilterService(IFilterRepo<Filter> filterRepo)
        {
            this.filterRepo = filterRepo;
        }

        /// <summary>
        /// Get all Filter-s async
        /// </summary>
        /// <returns>Task<List<Filter>></returns>
        public async Task<List<Filter>> GetFiltersAsync()
        {
            return await filterRepo.GetAllAsync();
        }

        /// <summary>
        /// Get Filter by Id async
        /// </summary>
        /// <param name="filterId"></param>
        /// <returns>Task<Filter></returns>
        public async Task<Filter> GetFilterByIdAsync(int filterId)
        {
            return await filterRepo.GetByIdAsync(filterId);
        }

        /// <summary>
        /// Insert Filter to database async
        /// </summary>
        /// <param name="filter"></param>
        /// <returns>Task<Filter></returns>
        public async Task<Filter> InsertFilterAsync(Filter filter)
        {
            return await filterRepo.InsertAsync(filter);
        }

        /// <summary>
        /// Update Filter in database async
        /// </summary>
        /// <param name="filter"></param>
        /// <returns>Task<Filter></returns>
        public async Task<Filter> UpdateFilterAsync(Filter filter)
        {
            var updatedFilter = await filterRepo.UpdateAsync(filter);
            await FTPServer.UploadDB();

            return updatedFilter;
        }

        /// <summary>
        /// Delete Filter by id async
        /// </summary>
        /// <param name="filterId"></param>
        /// <returns>Task<bool></returns>
        public async Task<bool> DeleteFilterByIdAsync(int filterId)
        {
            return await filterRepo.DeleteByIdAsync(filterId);
        }
    }
}

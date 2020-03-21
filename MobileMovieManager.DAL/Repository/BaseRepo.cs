using Microsoft.EntityFrameworkCore;
using MobileMovieManager.DAL.Context;
using MobileMovieManager.DAL.Repository.IRepository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MobileMovieManager.DAL.Repository
{
    public class BaseRepo<T> : IBaseRepo<T> where T : class
    {
        private readonly MobileMovieManagerDbContext context;

        public BaseRepo(MobileMovieManagerDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Delete model by id async
        /// </summary>
        /// <param name="id"></param>
        /// <returns>bool</returns>
        public async Task<bool> DeleteByIdAsync(int id)
        {
            var model = await context.Set<T>().FindAsync(id);

            // Check delete is successful
            if (model != null)
            {
                context.Set<T>().Remove(model);

                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Get all entity async
        /// </summary>
        /// <returns>IEnumerable</returns>
        public async Task<List<T>> GetAllAsync()
        {
            return await context.Set<T>().ToListAsync();
        }

        /// <summary>
        /// Get entity by Id async
        /// </summary>
        /// <param name="id"></param>
        /// <returns>T</returns>
        public async Task<T> GetByIdAsync(int id)
        {
            return await context.Set<T>().FindAsync(id);
        }

        /// <summary>
        /// Insert entity to database async
        /// </summary>
        /// <param name="model"></param>
        /// <returns>T</returns>
        public async Task<T> InsertAsync(T model)
        {
            var insertedModel = context.Set<T>().Add(model);
            await SaveAsync();

            return insertedModel.Entity;
        }

        /// <summary>
        /// Update entity in database async
        /// </summary>
        /// <param name="model"></param>
        /// <returns>T</returns>
        public async Task<T> UpdateAsync(T model)
        {
            context.Entry(model).State = EntityState.Modified;
            await SaveAsync();

            return model;
        }

        /// <summary>
        /// Save changes in database async
        /// </summary>
        /// <returns></returns>
        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// Dispose database context async
        /// </summary>
        public async void Dispose()
        {
            await context.DisposeAsync();
        }
    }
}

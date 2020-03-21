using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MobileMovieManager.DAL.Repository.IRepository
{
    public interface IBaseRepo<T> : IDisposable where T : class
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<T> InsertAsync(T model);
        Task<bool> DeleteByIdAsync(int id);
        Task<T> UpdateAsync(T model);
        Task SaveAsync();
    }
}

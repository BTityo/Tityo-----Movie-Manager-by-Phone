using MobileMovieManager.DAL.Context;
using MobileMovieManager.DAL.Models;
using MobileMovieManager.DAL.Repository;
using MobileMovieManager.DAL.Repository.IRepository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MobileMovieManager.BLL.Service
{
    public class FileSizeService
    {
        private IFileSizeRepo<FileSize> fileSizeRepo;

        public FileSizeService(string connectionString)
        {
            this.fileSizeRepo = new FileSizeRepo(new MobileMovieManagerDbContext(connectionString));
        }

        public FileSizeService(IFileSizeRepo<FileSize> fileSizeRepo)
        {
            this.fileSizeRepo = fileSizeRepo;
        }

        /// <summary>
        /// Get all FileSize-s async
        /// </summary>
        /// <returns>Task<List<FileSize>></returns>
        public async Task<List<FileSize>> GetFileSizesAsync()
        {
            return await fileSizeRepo.GetAllAsync();
        }

        /// <summary>
        /// Get FileSize by Id async
        /// </summary>
        /// <param name="fileSizeId"></param>
        /// <returns>Task<FileSize></returns>
        public async Task<FileSize> GetFileSizeByIdAsync(int fileSizeId)
        {
            return await fileSizeRepo.GetByIdAsync(fileSizeId);
        }

        /// <summary>
        /// Insert FileSize to database async
        /// </summary>
        /// <param name="fileSize"></param>
        /// <returns>Task<FileSize></returns>
        public async Task<FileSize> InsertFileSizeAsync(FileSize fileSize)
        {
            return await fileSizeRepo.InsertAsync(fileSize);
        }

        /// <summary>
        /// Update FileSize in database async
        /// </summary>
        /// <param name="fileSize"></param>
        /// <returns>Task<FileSize></returns>
        public async Task<FileSize> UpdateFileSizeAsync(FileSize fileSize)
        {
            return await fileSizeRepo.UpdateAsync(fileSize);
        }

        /// <summary>
        /// Delete FileSize by id async
        /// </summary>
        /// <param name="fileSizeId"></param>
        /// <returns>Task<bool></returns>
        public async Task<bool> DeleteFileSizeByIdAsync(int fileSizeId)
        {
            return await fileSizeRepo.DeleteByIdAsync(fileSizeId);
        }
    }
}

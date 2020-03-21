using MobileMovieManager.DAL.Context;
using MobileMovieManager.DAL.Models;
using MobileMovieManager.DAL.Repository;
using MobileMovieManager.DAL.Repository.IRepository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MobileMovieManager.BLL.Service
{
    public class FileTypeService
    {
        private IFileTypeRepo<FileType> fileTypeRepo;

        public FileTypeService(string connectionString)
        {
            this.fileTypeRepo = new FileTypeRepo(new MobileMovieManagerDbContext(connectionString));
        }

        public FileTypeService(IFileTypeRepo<FileType> fileTypeRepo)
        {
            this.fileTypeRepo = fileTypeRepo;
        }

        /// <summary>
        /// Get all FileType-s async
        /// </summary>
        /// <returns>Task<List<FileType>></returns>
        public async Task<List<FileType>> GetFileTypesAsync()
        {
            return await fileTypeRepo.GetAllAsync();
        }

        /// <summary>
        /// Get FileType by Id async
        /// </summary>
        /// <param name="fileTypeId"></param>
        /// <returns>Task<FileType></returns>
        public async Task<FileType> GetFileTypeByIdAsync(int fileTypeId)
        {
            return await fileTypeRepo.GetByIdAsync(fileTypeId);
        }

        /// <summary>
        /// Insert FileType to database async
        /// </summary>
        /// <param name="fileType"></param>
        /// <returns>Task<FileType></returns>
        public async Task<FileType> InsertFileTypeAsync(FileType fileType)
        {
            return await fileTypeRepo.InsertAsync(fileType);
        }

        /// <summary>
        /// Update FileType in database async
        /// </summary>
        /// <param name="fileType"></param>
        /// <returns>Task<FileType></returns>
        public async Task<FileType> UpdateFileTypeAsync(FileType fileType)
        {
            return await fileTypeRepo.UpdateAsync(fileType);
        }

        /// <summary>
        /// Delete FileType by id async
        /// </summary>
        /// <param name="fileTypeId"></param>
        /// <returns>Task<bool></returns>
        public async Task<bool> DeleteFileTypeByIdAsync(int fileTypeId)
        {
            return await fileTypeRepo.DeleteByIdAsync(fileTypeId);
        }
    }
}

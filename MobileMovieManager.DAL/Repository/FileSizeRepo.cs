using Microsoft.EntityFrameworkCore;
using MobileMovieManager.DAL.Context;
using MobileMovieManager.DAL.Models;
using MobileMovieManager.DAL.Repository.IRepository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileMovieManager.DAL.Repository
{
    public class FileSizeRepo : BaseRepo<FileSize>, IFileSizeRepo<FileSize>
    {
        private MobileMovieManagerDbContext context;

        public FileSizeRepo(MobileMovieManagerDbContext context) : base(context)
        {
            this.context = context;
        }

        public override async Task<List<FileSize>> GetAllAsync()
        {
            var fileSizes = await context.FileSizes.ToListAsync();

            return fileSizes;
        }

        public override async Task<FileSize> GetByIdAsync(int fileSizeId)
        {
            var fileSize = await context.FileSizes.FindAsync(fileSizeId);

            return fileSize;
        }

        public override async Task<FileSize> UpdateAsync(FileSize fileSize)
        {
            // Update FileSize
            var localFileSize = context.Set<FileSize>().Local.FirstOrDefault(f => f.Id == fileSize.Id);
            if (localFileSize != null)
            {
                context.Entry(localFileSize).State = EntityState.Detached;
            }

            context.Entry(fileSize).State = EntityState.Modified;

            await SaveAsync();

            return fileSize;
        }
    }
}

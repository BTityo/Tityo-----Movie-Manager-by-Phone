using MobileMovieManager.DAL.Context;
using MobileMovieManager.DAL.Models;
using MobileMovieManager.DAL.Repository.IRepository;

namespace MobileMovieManager.DAL.Repository
{
    public class FileTypeRepo : BaseRepo<FileType>, IFileTypeRepo<FileType>
    {
        private MobileMovieManagerDbContext context;

        public FileTypeRepo(MobileMovieManagerDbContext context) : base(context)
        {
            this.context = context;
        }
    }
}

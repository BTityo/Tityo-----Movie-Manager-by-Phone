using MobileMovieManager.DAL.Context;
using MobileMovieManager.DAL.Models;
using MobileMovieManager.DAL.Repository.IRepository;

namespace MobileMovieManager.DAL.Repository
{
    public class SettingRepo : BaseRepo<Setting>, ISettingRepo<Setting>
    {
        private MobileMovieManagerDbContext context;

        public SettingRepo(MobileMovieManagerDbContext context) : base(context)
        {
            this.context = context;
        }
    }
}

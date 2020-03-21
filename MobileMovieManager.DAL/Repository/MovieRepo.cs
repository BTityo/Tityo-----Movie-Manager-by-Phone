using MobileMovieManager.DAL.Context;
using MobileMovieManager.DAL.Models;
using MobileMovieManager.DAL.Repository.IRepository;

namespace MobileMovieManager.DAL.Repository
{
    public class MovieRepo : BaseRepo<Movie>, IMovieRepo<Movie>
    {
        private MobileMovieManagerDbContext context;

        public MovieRepo(MobileMovieManagerDbContext context) : base(context)
        {
            this.context = context;
        }
    }
}

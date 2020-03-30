using Microsoft.EntityFrameworkCore;
using MobileMovieManager.DAL.Context;
using MobileMovieManager.DAL.Models;
using MobileMovieManager.DAL.Repository.IRepository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileMovieManager.DAL.Repository
{
    public class FilterRepo : BaseRepo<Filter>, IFilterRepo<Filter>
    {
        private MobileMovieManagerDbContext context;

        public FilterRepo(MobileMovieManagerDbContext context) : base(context)
        {
            this.context = context;
        }

        public override async Task<List<Filter>> GetAllAsync()
        {
            var filters = await context.Filters.ToListAsync();
            filters.ForEach(async f => f.FileSize = await context.FileSizes.FindAsync(f.FileSizeId));
            filters.ForEach(async f => f.FileType = await context.FileTypes.FindAsync(f.FileTypeId));

            return filters;
        }

        public override async Task<Filter> GetByIdAsync(int filterId)
        {
            var filter = await context.Filters.FindAsync(filterId);
            filter.FileSize = await context.FileSizes.FindAsync(filter.FileSizeId);
            filter.FileType = await context.FileTypes.FindAsync(filter.FileTypeId);

            return filter;
        }

        public override async Task<Filter> UpdateAsync(Filter filter)
        {
            // Update setting
            var localFilter = context.Set<Filter>().Local.FirstOrDefault(f => f.Id == filter.Id);
            if (localFilter != null)
            {
                context.Entry(localFilter).State = EntityState.Detached;
            }

            context.Entry(filter).State = EntityState.Modified;

            await SaveAsync();

            return filter;
        }
    }
}

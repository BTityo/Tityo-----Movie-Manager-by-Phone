using Microsoft.EntityFrameworkCore;
using MobileMovieManager.DAL.Context;
using MobileMovieManager.DAL.Models;
using MobileMovieManager.DAL.Repository.IRepository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MobileMovieManager.DAL.Repository
{
    public class SettingRepo : BaseRepo<Setting>, ISettingRepo<Setting>
    {
        private MobileMovieManagerDbContext context;

        public SettingRepo(MobileMovieManagerDbContext context) : base(context)
        {
            this.context = context;
        }

        public override async Task<List<Setting>> GetAllAsync()
        {
            var settings = await context.Settings.ToListAsync();
            settings.ForEach(async f => f.FileTypes = await context.FileTypes.ToListAsync());

            return settings;
        }

        public override async Task<Setting> GetByIdAsync(int settingId)
        {
            var setting = await context.Settings.FindAsync(settingId);
            setting.FileTypes = await context.FileTypes.ToListAsync();

            return setting;
        }

        public override async Task<Setting> UpdateAsync(Setting setting)
        {
            // Update setting
            var localSetting = context.Set<Setting>().Local.FirstOrDefault(s => s.Id == setting.Id);
            if (localSetting != null)
            {
                context.Entry(localSetting).State = EntityState.Detached;
            }

            context.Entry(setting).State = EntityState.Modified;

            // Update filetype
            foreach (FileType fileType in setting.FileTypes)
            {
                var localFileType = context.Set<FileType>().Local.FirstOrDefault(f => f.Id == fileType.Id);
                if (localFileType != null)
                {
                    context.Entry(localFileType).State = EntityState.Detached;
                }

                context.Entry(fileType).State = EntityState.Modified;
            }

            await SaveAsync();

            return setting;
        }
    }
}

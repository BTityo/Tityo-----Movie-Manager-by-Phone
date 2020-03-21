using MobileMovieManager.DAL.Context;
using MobileMovieManager.DAL.Models;
using MobileMovieManager.DAL.Repository;
using MobileMovieManager.DAL.Repository.IRepository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MobileMovieManager.BLL.Service
{
    public class SettingService
    {
        private ISettingRepo<Setting> settingRepo;

        public SettingService(string connectionString)
        {
            this.settingRepo = new SettingRepo(new MobileMovieManagerDbContext(connectionString));
        }

        public SettingService(ISettingRepo<Setting> settingRepo)
        {
            this.settingRepo = settingRepo;
        }

        /// <summary>
        /// Get all Setting-s async
        /// </summary>
        /// <returns>Task<List<Setting>></returns>
        public async Task<List<Setting>> GetSettingsAsync()
        {
            return await settingRepo.GetAllAsync();
        }

        /// <summary>
        /// Get Setting by Id async
        /// </summary>
        /// <param name="settingId"></param>
        /// <returns>Task<Setting></returns>
        public async Task<Setting> GetSettingByIdAsync(int settingId)
        {
            return await settingRepo.GetByIdAsync(settingId);
        }

        /// <summary>
        /// Insert Setting to database async
        /// </summary>
        /// <param name="setting"></param>
        /// <returns>Task<Setting></returns>
        public async Task<Setting> InsertSettingAsync(Setting setting)
        {
            return await settingRepo.InsertAsync(setting);
        }

        /// <summary>
        /// Update Setting in database async
        /// </summary>
        /// <param name="setting"></param>
        /// <returns>Task<Setting></returns>
        public async Task<Setting> UpdateSettingAsync(Setting setting)
        {
            return await settingRepo.UpdateAsync(setting);
        }

        /// <summary>
        /// Delete Setting by id async
        /// </summary>
        /// <param name="settingId"></param>
        /// <returns>Task<bool></returns>
        public async Task<bool> DeleteSettingByIdAsync(int settingId)
        {
            return await settingRepo.DeleteByIdAsync(settingId);
        }
    }
}

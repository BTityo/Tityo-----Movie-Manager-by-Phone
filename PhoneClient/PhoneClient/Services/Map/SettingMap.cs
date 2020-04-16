using MobileMovieManager.DAL.Models;
using PhoneClient.ViewModels;
using System;
using System.Drawing;
using System.Linq;

namespace PhoneClient.Services.Map
{
    public static class SettingMap
    {
        public static Setting MapSettingViewModelToSetting(SettingViewModel settingViewModel)
        {
            if (settingViewModel != null)
            {
                return new Setting()
                {
                    Id = settingViewModel.Id,
                    ServerIP = settingViewModel.ServerIP,
                    Port = settingViewModel.Port,
                    UserName = settingViewModel.UserName,
                    IsSearchForSubtitle = settingViewModel.IsSearchForSubtitle,
                    ConnectedPhone = settingViewModel.ConnectedPhone,
                    MoviesPath = settingViewModel.MoviesPath,
                    FileTypes = FileTypeMap.MapToFileTypeList(settingViewModel.FileTypeViewModels.Where(t => t.Id != 1).ToList()),
                    SelectedAccentColor = convertColorToByteArray(settingViewModel.SelectedAccentColor),
                    SelectedTheme = settingViewModel.SelectedTheme
                };
            }
            else
            {
                return null;
            }
        }

        public static SettingViewModel MapToSettingViewModel(Setting setting)
        {
            if (setting != null)
            {
                return new SettingViewModel()
                {
                    Id = setting.Id,
                    ServerIP = setting.ServerIP,
                    Port = setting.Port,
                    UserName = Environment.UserDomainName + "/" + Environment.UserName,
                    IsSearchForSubtitle = setting.IsSearchForSubtitle,
                    ConnectedPhone = setting.ConnectedPhone,
                    MoviesPath = setting.MoviesPath,
                    FileTypeViewModels = FileTypeMap.MapToFileTypeViewModelList(setting.FileTypes.Where(t => t.Id != 1).ToList()),
                    SelectedAccentColor = convertByteArrayToColor(setting.SelectedAccentColor),
                    SelectedTheme = setting.SelectedTheme
                };
            }
            else
            {
                return new SettingViewModel();
            }
        }

        private static byte[] convertColorToByteArray(Color accentColor)
        {
            return new byte[] { accentColor.R, accentColor.G, accentColor.B };
        }

        private static Color convertByteArrayToColor(byte[] accentColor)
        {
            return Color.FromArgb(accentColor[0], accentColor[1], accentColor[2]);
        }
    }
}

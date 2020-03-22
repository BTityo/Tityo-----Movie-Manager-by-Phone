using DesktopServer.ViewModels;
using FirstFloor.ModernUI.Presentation;
using MobileMovieManager.DAL.Models;
using System;
using System.Linq;
using System.Windows.Media;

namespace DesktopServer.Map
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
                    IsStartWithWindows = settingViewModel.IsStartWithWindows,
                    ConnectedPhone = settingViewModel.ConnectedPhone,
                    MoviesPath = settingViewModel.MoviesPath,
                    FileTypes = FileTypeMap.MapToFileTypeList(settingViewModel.FileTypeViewModels),
                    SelectedAccentColor = convertColorToByteArray(settingViewModel.SelectedAccentColor),
                    SelectedPalette = settingViewModel.SelectedPalette,
                    SelectedTheme = settingViewModel.SelectedTheme.DisplayName
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
                    IsStartWithWindows = setting.IsStartWithWindows,
                    ConnectedPhone = setting.ConnectedPhone,
                    MoviesPath = setting.MoviesPath,
                    FileTypeViewModels = FileTypeMap.MapToFileTypeViewModelList(setting.FileTypes.ToList()),
                    SelectedAccentColor = convertByteArrayToColor(setting.SelectedAccentColor),
                    SelectedPalette = setting.SelectedPalette,
                    SelectedTheme = setting.SelectedTheme == "Sötét" ? new Link { DisplayName = "Sötét", Source = AppearanceManager.DarkThemeSource } : new Link { DisplayName = "Világos", Source = AppearanceManager.LightThemeSource }
                };
            }
            else
            {
                return null;
            }
        }

        private static byte[] convertColorToByteArray(Color accentColor)
        {
            return new byte[] { accentColor.R, accentColor.G, accentColor.B };
        }

        private static Color convertByteArrayToColor(byte[] accentColor)
        {
            return Color.FromRgb(accentColor[0], accentColor[1], accentColor[2]);
        }
    }
}

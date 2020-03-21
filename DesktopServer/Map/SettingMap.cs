using DesktopServer.ViewModels;
using FirstFloor.ModernUI.Presentation;
using MobileMovieManager.DAL.Models;
using System.Windows.Media;

namespace DesktopServer.Map
{
    public static class SettingMap
    {
        public static Setting MapToSetting(SettingMainViewModel settingMainViewModel, SettingDisplayViewModel settingDisplayViewModel)
        {
            if (settingMainViewModel != null && settingDisplayViewModel != null)
            {
                return new Setting()
                {
                    Id = settingMainViewModel.Id,
                    ServerIP = settingMainViewModel.ServerIP,
                    Port = settingMainViewModel.Port,
                    UserName = settingMainViewModel.UserName,
                    IsSearchForSubtitle = settingMainViewModel.IsSearchForSubtitle,
                    IsStartWithWindows = settingMainViewModel.IsStartWithWindows,
                    ConnectedPhone = settingMainViewModel.ConnectedPhone,
                    MoviesPath = settingMainViewModel.MoviesPath,
                    SelectedAccentColor = convertColorToByteArray(settingDisplayViewModel.SelectedAccentColor),
                    SelectedFontSize = settingDisplayViewModel.SelectedFontSize,
                    SelectedPalette = settingDisplayViewModel.SelectedPalette,
                    SelectedTheme = settingDisplayViewModel.SelectedTheme.DisplayName
                };
            }
            else
            {
                return null;
            }
        }

        public static SettingMainViewModel MapToSettingMainViewModel(Setting setting)
        {
            if (setting != null)
            {
                return new SettingMainViewModel()
                {
                    Id = setting.Id,
                    ServerIP = setting.ServerIP,
                    Port = setting.Port,
                    UserName = setting.UserName,
                    IsSearchForSubtitle = setting.IsSearchForSubtitle,
                    IsStartWithWindows = setting.IsStartWithWindows,
                    ConnectedPhone = setting.ConnectedPhone,
                    MoviesPath = setting.MoviesPath
                };
            }
            else
            {
                return null;
            }
        }

        public static SettingDisplayViewModel MapToSettingDisplayViewModel(Setting setting)
        {
            if (setting != null)
            {
                return new SettingDisplayViewModel()
                {
                    Id = setting.Id,
                    SelectedAccentColor = convertByteArrayToColor(setting.SelectedAccentColor),
                    SelectedFontSize = setting.SelectedFontSize,
                    SelectedPalette = setting.SelectedPalette,
                    SelectedTheme = setting.SelectedTheme == "dark" ? new Link { DisplayName = "dark", Source = AppearanceManager.DarkThemeSource } : new Link { DisplayName = "light", Source = AppearanceManager.LightThemeSource }
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

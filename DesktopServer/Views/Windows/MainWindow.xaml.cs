using DesktopServer.Map;
using DesktopServer.Service;
using DesktopServer.ViewModels;
using FirstFloor.ModernUI.Windows.Controls;
using MobileMovieManager.BLL.Service;
using System;
using System.IO;
using System.Windows;

namespace DesktopServer.Views.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : ModernWindow
    {
        private readonly SettingService settingService;
        private SettingViewModel settingViewModel;

        public MainWindow()
        {
            settingViewModel = new SettingViewModel();
            settingService = new SettingService(Constants.LocalDBPath);

            InitializeComponent();
            setViewModel();
        }

        private async void setViewModel()
        {
            // We use just one Setting (Get and Update this Setting)
            await settingService.GetSettingByIdAsync(1).ContinueWith(setting =>
            {
                settingViewModel = SettingMap.MapToSettingViewModel(setting.Result);

                // Check setted Movies folder is exists
                if (!Directory.Exists(settingViewModel.MoviesPath))
                {
                    this.Dispatcher.Invoke(() =>
                    {
                        MessageBoxResult result = ModernDialog.ShowMessage("A(z) '" + settingViewModel.MoviesPath + "' mappa nem létezik! Szeretnéd megváltoztatni?", "'Filmek' mappa nem létezik", MessageBoxButton.YesNo);
                        if (result == MessageBoxResult.Yes)
                        {
                            ContentSource = new Uri("/DesktopServer;component/Views/Pages/Setting.xaml", UriKind.Relative);
                        }
                        else
                        {
                            ContentSource = new Uri("/DesktopServer;component/Views/Pages/Movie.xaml", UriKind.Relative);
                        }
                    });
                }
                else
                {
                    this.Dispatcher.Invoke(() =>
                        ContentSource = new Uri("/DesktopServer;component/Views/Pages/Movie.xaml", UriKind.Relative)
                    );
                }
            });
        }
    }
}

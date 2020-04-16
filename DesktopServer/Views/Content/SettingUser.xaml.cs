using DesktopServer.Map;
using DesktopServer.Service;
using DesktopServer.ViewModels;
using FirstFloor.ModernUI.Windows;
using FirstFloor.ModernUI.Windows.Controls;
using FirstFloor.ModernUI.Windows.Navigation;
using MobileMovieManager.BLL.FileServer;
using MobileMovieManager.BLL.Service;
using System;
using System.Windows;
using System.Windows.Controls;

namespace DesktopServer.Views.Content
{
    /// <summary>
    /// Interaction logic for SettingUser.xaml
    /// </summary>
    public partial class SettingUser : UserControl, IContent
    {
        private readonly SettingService settingService;
        private SettingViewModel settingViewModel;

        public SettingUser()
        {
            settingViewModel = new SettingViewModel();
            settingService = new SettingService(Constants.LocalDBPath);

            InitializeComponent();
            setViewModel();
        }

        private async void setViewModel()
        {
            // We use just one Setting (Get and Update this Setting)
            await settingService.GetSettingByIdAsync(1).ContinueWith(async setting =>
            {
                settingViewModel = SettingMap.MapToSettingViewModel(await setting);
                this.DataContext = settingViewModel;
            });
        }

        public void OnFragmentNavigation(FragmentNavigationEventArgs e)
        {
        }

        public void OnNavigatedFrom(NavigationEventArgs e)
        {
        }

        public void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        public async void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(textBoxUserName.Text) || String.IsNullOrWhiteSpace(textBoxUserName.Text))
                {
                    textBoxUserName.Text = "-";
                    settingViewModel.UserName = "-";
                }

                await settingService.UpdateSettingAsync(SettingMap.MapSettingViewModelToSetting(settingViewModel));
            }
            catch (Exception ex)
            {
                ModernDialog.ShowMessage(ex.Message, "Hiba mentés közben", MessageBoxButton.OK);
                e.Cancel = true;
            }
        }
    }
}

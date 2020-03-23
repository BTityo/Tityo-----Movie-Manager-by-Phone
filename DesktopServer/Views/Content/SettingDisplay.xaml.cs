using DesktopServer.Map;
using DesktopServer.Service;
using DesktopServer.ViewModels;
using FirstFloor.ModernUI.Windows;
using FirstFloor.ModernUI.Windows.Controls;
using FirstFloor.ModernUI.Windows.Navigation;
using MobileMovieManager.BLL.Service;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace DesktopServer.Views.Content
{
    /// <summary>
    /// Interaction logic for SettingDisplay.xaml
    /// </summary>
    public partial class SettingDisplay : UserControl, IContent
    {
        private readonly SettingService settingService;
        private SettingViewModel settingViewModel;

        public SettingDisplay()
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
                this.DataContext = settingViewModel;
            });
        }

        //private async void buttonSave_Click(object sender, RoutedEventArgs e)
        //{
        //    try
        //    {
        //        await settingService.UpdateSettingAsync(SettingMap.MapSettingViewModelToSetting(settingViewModel));
        //    }
        //    catch (Exception ex)
        //    {
        //        ModernDialog.ShowMessage(ex.Message, "Hiba mentés közben", MessageBoxButton.OK);
        //    }
        //}

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            settingViewModel.SelectedPalette = ((ComboBox)sender).SelectedItem.ToString();
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            // Select first control on the form
            Keyboard.Focus(this.comboBoxPalette);
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
            // Save Setting when navigating from page
            try
            {
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

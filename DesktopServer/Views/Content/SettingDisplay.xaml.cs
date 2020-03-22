using DesktopServer.Map;
using DesktopServer.Service;
using DesktopServer.ViewModels;
using MobileMovieManager.BLL.Service;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DesktopServer.Views.Content
{
    /// <summary>
    /// Interaction logic for SettingDisplay.xaml
    /// </summary>
    public partial class SettingDisplay : UserControl
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

        private async void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            var test = await settingService.UpdateSettingAsync(SettingMap.MapSettingViewModelToSetting(settingViewModel));
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            settingViewModel.SelectedPalette = ((ComboBox)sender).SelectedItem.ToString();
        }
    }
}

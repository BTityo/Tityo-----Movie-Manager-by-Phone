using DesktopServer.Map;
using DesktopServer.Service;
using DesktopServer.ViewModels;
using FirstFloor.ModernUI.Windows.Controls;
using MobileMovieManager.BLL.Service;

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
            await settingService.GetSettingByIdAsync(1).ContinueWith(async setting =>
            {
                settingViewModel = SettingMap.MapToSettingViewModel(await setting);
            });
        }
    }
}

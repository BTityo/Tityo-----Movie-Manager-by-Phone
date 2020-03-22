using DesktopServer.Map;
using DesktopServer.Service;
using DesktopServer.ViewModels;
using FirstFloor.ModernUI.Windows.Controls;
using MobileMovieManager.BLL.Service;
using MobileMovieManager.DAL.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
            });
        }
    }
}

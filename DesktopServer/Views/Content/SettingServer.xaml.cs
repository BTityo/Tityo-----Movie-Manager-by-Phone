using DesktopServer.Map;
using DesktopServer.Service;
using DesktopServer.ViewModels;
using MobileMovieManager.BLL.Service;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace DesktopServer.Views.Content
{
    /// <summary>
    /// Interaction logic for SettingServer.xaml
    /// </summary>
    public partial class SettingServer : UserControl
    {
        private readonly SettingService settingService;
        private SettingViewModel settingViewModel;

        public SettingServer()
        {
            settingViewModel = new SettingViewModel();
            settingService = new SettingService(Constants.LocalDBPath);

            InitializeComponent();
            setViewModel();
            setTypes();
        }

        private async void setViewModel()
        {
            // We use just one Setting (Get and Update this Setting)
            var setting = await settingService.GetSettingByIdAsync(1);
            settingViewModel = SettingMap.MapToSettingViewModel(setting);
            this.DataContext = settingViewModel;
        }

        private void setTypes()
        {
            Binding myBinding;
            CheckBox checkBox;
            foreach (FileTypeViewModel fileTypeViewModel in settingViewModel.FileTypeViewModels)
            {
                checkBox = new CheckBox();
                myBinding = new Binding();

                // Set binding
                myBinding.Source = fileTypeViewModel;
                myBinding.Path = new PropertyPath("IsChecked");
                myBinding.Mode = BindingMode.TwoWay;
                myBinding.UpdateSourceTrigger = UpdateSourceTrigger.PropertyChanged;
                // Set CheckBox
                checkBox.Content = fileTypeViewModel.TypeName;
                checkBox.SetBinding(CheckBox.IsCheckedProperty, myBinding);

                this.Dispatcher.Invoke(() => stackPanelTypes.Children.Add(checkBox));
            }
        }

        private async void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            var test = await settingService.UpdateSettingAsync(SettingMap.MapSettingViewModelToSetting(settingViewModel));
        }
    }
}

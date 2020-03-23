using DesktopServer.Map;
using DesktopServer.Service;
using DesktopServer.ViewModels;
using FirstFloor.ModernUI.Windows;
using FirstFloor.ModernUI.Windows.Controls;
using FirstFloor.ModernUI.Windows.Navigation;
using MobileMovieManager.BLL.Service;
using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace DesktopServer.Views.Content
{
    /// <summary>
    /// Interaction logic for SettingServer.xaml
    /// </summary>
    public partial class SettingServer : UserControl, IContent
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

        //private async void buttonSave_Click(object sender, RoutedEventArgs e)
        //{

        //}

        void OnLoaded(object sender, RoutedEventArgs e)
        {
            // Select first control on the form
            Keyboard.Focus(this.textBoxServerIP);
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
                if (!String.IsNullOrEmpty(textBoxServerIP.Text) && textBoxServerIP.Text.Contains("192.168.1.") && !String.IsNullOrEmpty(textBoxPort.Text) && Convert.ToInt32(textBoxPort.Text) > 1000 && Convert.ToInt32(textBoxPort.Text) < 10000 && !String.IsNullOrEmpty(textBoxMoviesPath.Text))
                {
                    await settingService.UpdateSettingAsync(SettingMap.MapSettingViewModelToSetting(settingViewModel));
                }
                else
                {
                    ModernDialog.ShowMessage("Javítsd a kötelező mezőket", "Hiba mentés közben", MessageBoxButton.OK);
                    e.Cancel = true;
                }
            }
            catch (Exception ex)
            {
                ModernDialog.ShowMessage(ex.Message, "Hiba mentés közben", MessageBoxButton.OK);
                e.Cancel = true;
            }
        }

        // Only numbers in port input
        private void textBoxPort_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}

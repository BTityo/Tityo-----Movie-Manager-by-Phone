using DesktopServer.Map;
using DesktopServer.ViewModels;
using FirstFloor.ModernUI.Windows;
using FirstFloor.ModernUI.Windows.Controls;
using FirstFloor.ModernUI.Windows.Navigation;
using MobileMovieManager.BLL.FileServer;
using MobileMovieManager.BLL.Service;
using Ookii.Dialogs.Wpf;
using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
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
            // Set IP automatically
            settingViewModel.ServerIP = getMyIP();
            this.DataContext = settingViewModel;
        }

        // Get my IP Address
        private string getMyIP()
        {
            string ipAddress = "192.168.1.106";
            // Get my local IP Address
            IPAddress[] localIp = Dns.GetHostAddresses(Dns.GetHostName());
            foreach (IPAddress address in localIp)
            {
                if (address.AddressFamily == AddressFamily.InterNetwork)
                {
                    ipAddress = address.ToString();
                }
            }

            return ipAddress;
        }

        private void setTypes()
        {
            Binding myBinding;
            CheckBox checkBox;
            foreach (FileTypeViewModel fileTypeViewModel in settingViewModel.FileTypeViewModels.Where(f => f.Id > 0))
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

        // Open 'movie' folder
        private void buttonOpenMovieFolder_Click(object sender, RoutedEventArgs e)
        {
            VistaFolderBrowserDialog openFileDialog = new VistaFolderBrowserDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                settingViewModel.MoviesPath = openFileDialog.SelectedPath;
            }
        }
    }
}

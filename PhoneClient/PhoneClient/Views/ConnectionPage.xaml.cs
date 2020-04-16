using MobileMovieManager.BLL.FileServer;
using MobileMovieManager.BLL.Service;
using PhoneClient.Services;
using PhoneClient.Services.Map;
using PhoneClient.ViewModels;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net.Sockets;
using Xamarin.Forms;
using Constants = PhoneClient.Services.Constants;

namespace PhoneClient.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class ConnectionPage : ContentPage
    {
        private readonly SettingService settingService;
        private readonly MovieService movieService;
        private SettingViewModel settingViewModel;
        private ObservableCollection<MovieViewModel> movieViewModels;

        private Socket clientSocket;

        public ConnectionPage()
        {
            settingViewModel = new SettingViewModel();
            movieViewModels = new ObservableCollection<MovieViewModel>();
            downloadSQLiteDB();
            settingService = new SettingService(Constants.DbPath);
            movieService = new MovieService(Constants.DbPath);

            // Init socket
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            InitializeComponent();

            setViewModel();
        }

        // Download SQLite database from FTP server
        private async void downloadSQLiteDB()
        {
            await FTPServer.DownloadDB(Constants.DbPath);
        }

        private async void setViewModel()
        {
            // We use just one Setting (Get and Update this Setting)
            await settingService.GetSettingByIdAsync(1).ContinueWith(async settingResult =>
                settingViewModel = SettingMap.MapToSettingViewModel(await settingResult)
            );
            // Set MovieViewModels
            await movieService.GetMoviesAsync().ContinueWith(async moviesResult =>
                movieViewModels = MovieMap.MapToMovieViewModelList(await moviesResult)
            );

            BindingContext = settingViewModel;
        }

        private async void buttonConnect_Clicked(object sender, EventArgs e)
        {
            // Loading screen
            await Navigation.PushModalAsync(new Loading());
            if (SocketClientService.Instance.StartClientService(clientSocket, settingViewModel.ServerIP, settingViewModel.Port))
            {
                buttonConnect.IsEnabled = false;
                buttonConnectClose.IsEnabled = true;

                await Navigation.PopModalAsync();
                await Navigation.PushModalAsync(new MoviesListPage(movieViewModels));
            }
            else
            {
                await Navigation.PopModalAsync();
                await DisplayAlert("Nincs kapcsolat", "Nem sikerült kapcsolódni a szerverhez...\nBiztosan fut a szerver?", "Ok");
            }
        }

        private void buttonConnectClose_Clicked(object sender, EventArgs e)
        {
            SocketClientService.Instance.CloseClientService();

            buttonConnect.IsEnabled = true;
            buttonConnectClose.IsEnabled = false;
        }
    }
}
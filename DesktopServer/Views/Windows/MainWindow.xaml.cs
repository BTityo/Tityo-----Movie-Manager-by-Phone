using DesktopServer.Map;
using DesktopServer.Service;
using DesktopServer.ViewModels;
using FirstFloor.ModernUI.Windows.Controls;
using MobileMovieManager.BLL.FileServer;
using MobileMovieManager.BLL.Service;
using MobileMovieManager.DAL.Models;
using System.Collections.Generic;

namespace DesktopServer.Views.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : ModernWindow
    {
        private readonly SettingService settingService;
        private readonly MovieService movieService;
        private SettingViewModel settingViewModel;
        private List<Movie> movies;

        public MainWindow()
        {
            settingService = new SettingService(Constants.LocalDBPath);
            movieService = new MovieService(Constants.LocalDBPath);
            settingViewModel = new SettingViewModel();
            movies = new List<Movie>();

            InitializeComponent();
            setViewModel();
        }

        private async void setViewModel()
        {
            // Upload SQLite database through FTP
            await FTPServer.UploadDB();

            // We use just one Setting (Get and Update this Setting)
            settingViewModel = SettingMap.MapToSettingViewModel(await settingService.GetSettingByIdAsync(1));

            // Get movies and Start Socket Server if movies count more than 0 and not null
            await movieService.GetMoviesAsync().ContinueWith(async moviesResult =>
            {
                movies = await moviesResult;

                if (movies != null && movies.Count > 0)
                {
                    SocketServerService socketServer = new SocketServerService();
                    // Start Socket Server listen                    
                    socketServer.StartServer(settingViewModel);
                }
            });
        }
    }
}

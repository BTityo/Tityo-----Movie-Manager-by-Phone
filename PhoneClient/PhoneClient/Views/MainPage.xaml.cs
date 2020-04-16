using MobileMovieManager.BLL.Service;
using PhoneClient.Models;
using PhoneClient.Services;
using PhoneClient.Services.Map;
using PhoneClient.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PhoneClient.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : MasterDetailPage
    {
        private Dictionary<int, NavigationPage> MenuPages = new Dictionary<int, NavigationPage>();
        private readonly MovieService movieService;
        private ObservableCollection<MovieViewModel> movieViewModels;

        public MainPage()
        {
            movieService = new MovieService(Constants.DbPath);
            movieViewModels = new ObservableCollection<MovieViewModel>();

            InitializeComponent();

            MasterBehavior = MasterBehavior.Popover;

            //MenuPages.Add((int)MenuItemType.MoviesList, (NavigationPage)Detail);
        }

        public async Task NavigateFromMenu(int id)
        {
            if (!MenuPages.ContainsKey(id))
            {
                // Set MovieViewModels
                await movieService.GetMoviesAsync().ContinueWith(async moviesResult =>
                    movieViewModels = MovieMap.MapToMovieViewModelList(await moviesResult)
                );

                switch (id)
                {
                    case (int)MenuItemType.Connection:
                        MenuPages.Add(id, new NavigationPage(new ConnectionPage()));
                        break;
                    case (int)MenuItemType.MoviesList:
                        MenuPages.Add(id, new NavigationPage(new MoviesListPage(movieViewModels)));
                        break;
                    case (int)MenuItemType.Command:
                        MenuPages.Add(id, new NavigationPage(new CommandPage()));
                        break;
                    case (int)MenuItemType.Log:
                        MenuPages.Add(id, new NavigationPage(new LogPage()));
                        break;
                }
            }

            var newPage = MenuPages[id];

            if (newPage != null && Detail != newPage)
            {
                Detail = newPage;

                if (Device.RuntimePlatform == Device.Android)
                    await Task.Delay(100);

                IsPresented = false;
            }
        }
    }
}
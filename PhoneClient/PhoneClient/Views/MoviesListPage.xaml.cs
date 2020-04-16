using MobileMovieManager.DAL.Models;
using PhoneClient.Services;
using PhoneClient.Services.Map;
using PhoneClient.ViewModels;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PhoneClient.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [DesignTimeVisible(false)]
    public partial class MoviesListPage : ContentPage
    {
        private ObservableCollection<MovieViewModel> movieViewModels;

        public MoviesListPage(ObservableCollection<MovieViewModel> movieViewModels)
        {
            this.movieViewModels = movieViewModels;
            InitializeComponent();

            listViewMovies.ItemsSource = this.movieViewModels;
        }

        private async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            var item = args.SelectedItem as MovieViewModel;
            if (item == null)
                return;

            if (Connection.Instance.ClientSocket != null && Connection.Instance.ClientSocket.ClientSocket != null && Connection.Instance.ClientSocket.ClientSocket.Connected && Connection.Instance.ClientSocket.IsPlayMovie)
            {
                SocketClientService.Instance.SendMovie(MovieMap.MapToMovie(item));
            }
            else
            {
                await DisplayAlert("Nincs kapcsolat", "Film küldése sikertelen...\nNincs kapcsolat a szerverrel", "Ok");
            }

            // Manually deselect item.
            listViewMovies.SelectedItem = null;
        }
    }
}
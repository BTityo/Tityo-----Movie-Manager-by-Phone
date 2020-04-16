using MobileMovieManager.BLL.Service;
using MobileMovieManager.DAL.Models;
using PhoneClient.Services;
using System.Collections.Generic;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PhoneClient.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [DesignTimeVisible(false)]
    public partial class CommandPage : ContentPage
    {
        private MovieCommandService movieCommandService;

        public CommandPage()
        {
            movieCommandService = new MovieCommandService();
            InitializeComponent();

            listViewCommandsNow.ItemsSource = movieCommandService.MovieCommandsNow;
            listViewCommands.ItemsSource = movieCommandService.MovieCommands;
        }

        // Click on a command
        private async void Button_Clicked(object sender, System.EventArgs e)
        {
            string clickedButtonText = ((Button)sender).Text;

            if (Connection.Instance.ClientSocket != null && Connection.Instance.ClientSocket.ClientSocket != null && Connection.Instance.ClientSocket.ClientSocket.Connected && Connection.Instance.ClientSocket.IsPlayMovie)
            {
                foreach (MovieCommand movieCommand in movieCommandService.MovieCommands)
                {
                    if (movieCommand.CommandDescription.ToString() == clickedButtonText)
                    {
                        SocketClientService.Instance.SendCommand(movieCommand.Command);
                    }
                }
            }
            else
            {
                await DisplayAlert("Nincs kapcsolat", "Parancs küldése sikertelen...\nNincs kapcsolat a szerverrel", "Ok");
            }
        }
    }
}
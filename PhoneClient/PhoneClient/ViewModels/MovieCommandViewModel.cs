using static MobileMovieManager.DAL.Models.Enums.MovieCommandEnum;

namespace PhoneClient.ViewModels
{
    public class MovieCommandViewModel : BaseViewModel
    {

        private MovieCommand command;
        public MovieCommand Command
        {
            get { return command; }
            set
            {
                if (command != value)
                {
                    command = value;
                    OnPropertyChanged("Command");
                }
            }
        }

        private string commandDescription;
        public string CommandDescription
        {
            get { return commandDescription; }
            set
            {
                if (commandDescription != value)
                {
                    commandDescription = value;
                    OnPropertyChanged("CommandDescription");
                }
            }
        }
    }
}

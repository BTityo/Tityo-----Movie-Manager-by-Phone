using System.Collections.Generic;

namespace DesktopServer.ViewModels
{
    public class SettingMainViewModel : BaseViewModel
    {
        // Server settings
        private string connectedPhone;
        public string ConnectedPhone
        {
            get { return connectedPhone; }
            set
            {
                if (connectedPhone != value)
                {
                    connectedPhone = value;
                    OnPropertyChanged("ConnectedPhone");
                }
            }
        }

        private string serverIP;
        public string ServerIP
        {
            get { return serverIP; }
            set
            {
                if (serverIP != value)
                {
                    serverIP = value;
                    OnPropertyChanged("ServerIP");
                }
            }
        }

        private int port;
        public int Port
        {
            get { return port; }
            set
            {
                if (port != value)
                {
                    port = value;
                    OnPropertyChanged("Port");
                }
            }
        }

        // User settings
        private bool isStartWithWindows;
        public bool IsStartWithWindows
        {
            get { return isStartWithWindows; }
            set
            {
                if (isStartWithWindows != value)
                {
                    isStartWithWindows = value;
                    OnPropertyChanged("IsStartWithWindows");
                }
            }
        }

        private bool isSearchForSubtitle;
        public bool IsSearchForSubtitle
        {
            get { return isSearchForSubtitle; }
            set
            {
                if (isSearchForSubtitle != value)
                {
                    isSearchForSubtitle = value;
                    OnPropertyChanged("IsSearchForSubtitle");
                }
            }
        }

        private string userName;
        public string UserName
        {
            get { return userName; }
            set
            {
                if (userName != value)
                {
                    userName = value;
                    OnPropertyChanged("UserName");
                }
            }
        }

        private string moviesPath;
        public string MoviesPath
        {
            get { return moviesPath; }
            set
            {
                if (moviesPath != value)
                {
                    moviesPath = value;
                    OnPropertyChanged("MoviesPath");
                }
            }
        }

        public List<FileTypeViewModel> FileTypeViewModels { get; set; }
    }
}

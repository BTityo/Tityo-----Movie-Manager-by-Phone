using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;

namespace PhoneClient.ViewModels
{
    public class SettingViewModel : BaseViewModel, IDataErrorInfo
    {
        private Color selectedAccentColor;

        private string selectedTheme;
        public string SelectedTheme
        {
            get { return this.selectedTheme; }
            set { SetProperty(ref selectedTheme, value); }
        }

        public Color SelectedAccentColor
        {
            get { return this.selectedAccentColor; }
            set { SetProperty(ref selectedAccentColor, value); }
        }

        // Server setting
        private string connectedPhone;
        public string ConnectedPhone
        {
            get { return connectedPhone; }
            set { SetProperty(ref connectedPhone, value); }
        }

        private string serverIP;
        public string ServerIP
        {
            get { return serverIP; }
            set { SetProperty(ref serverIP, value); }
        }

        private int port;
        public int Port
        {
            get { return port; }
            set { SetProperty(ref port, value); }
        }

        // User settings

        private bool isSearchForSubtitle;
        public bool IsSearchForSubtitle
        {
            get { return isSearchForSubtitle; }
            set { SetProperty(ref isSearchForSubtitle, value); }
        }

        private string userName;
        public string UserName
        {
            get { return userName; }
            set { SetProperty(ref userName, value); }
        }

        private string moviesPath;
        public string MoviesPath
        {
            get { return moviesPath; }
            set { SetProperty(ref moviesPath, value); }
        }

        private ICollection<FileTypeViewModel> fileTypeViewModels;
        public ICollection<FileTypeViewModel> FileTypeViewModels
        {
            get { return fileTypeViewModels; }
            set { SetProperty(ref fileTypeViewModels, value); }
        }

        public string Error
        {
            get { return null; }
        }

        public string this[string columnName]
        {
            get
            {
                switch (columnName)
                {
                    case "ServerIP":
                        return string.IsNullOrEmpty(this.serverIP) && !this.serverIP.Contains("192.168.1.") ? "Kötelező (192.168.1.XXX)" : null;
                    case "Port":
                        return this.port < 1001 || this.port > 9999 ? "Kötelező (1001 és 9999 között)" : null;
                    case "MoviesPath":
                        return string.IsNullOrEmpty(this.moviesPath) ? "Kötelező" : null;
                    default:
                        return null;
                }
            }
        }
    }
}

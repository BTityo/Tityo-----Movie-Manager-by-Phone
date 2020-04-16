using System;
using static MobileMovieManager.DAL.Models.Enums.LoggerEnum;

namespace DesktopServer.ViewModels
{
    public class LoggerViewModel : BaseViewModel
    {
        private string message;
        public string Message
        {
            get { return message; }
            set
            {
                if (message != value)
                {
                    message = value;
                    OnPropertyChanged("Message");
                }
            }
        }

        private Sender sender;
        public Sender Sender
        {
            get { return sender; }
            set
            {
                if (sender != value)
                {
                    sender = value;
                    OnPropertyChanged("Sender");
                }
            }
        }

        private DateTime logDate;
        public DateTime LogDate
        {
            get { return logDate; }
            set
            {
                if (logDate != value)
                {
                    logDate = value;
                    OnPropertyChanged("LogDate");
                }
            }
        }
    }
}

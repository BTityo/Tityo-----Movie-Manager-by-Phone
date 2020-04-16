using System;
using static MobileMovieManager.DAL.Models.Enums.LoggerEnum;

namespace PhoneClient.ViewModels
{
    public class LoggerViewModel : BaseViewModel
    {
        private string message;
        public string Message
        {
            get { return message; }
            set { SetProperty(ref message, value); }
        }

        private Sender sender;
        public Sender Sender
        {
            get { return sender; }
            set { SetProperty(ref sender, value); }
        }

        private DateTime logDate;
        public DateTime LogDate
        {
            get { return logDate; }
            set { SetProperty(ref logDate, value); }
        }
    }
}

namespace DesktopServer.ViewModels
{
    public class FileSizeViewModel : BaseViewModel
    {
        private string sizeName;
        public string SizeName
        {
            get { return sizeName; }
            set
            {
                if (sizeName != value)
                {
                    sizeName = value;
                    OnPropertyChanged("SizeName");
                }
            }
        }
    }
}

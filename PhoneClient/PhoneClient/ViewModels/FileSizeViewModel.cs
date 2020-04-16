namespace PhoneClient.ViewModels
{
    public class FileSizeViewModel : BaseViewModel
    {
        private string sizeName;
        public string SizeName
        {
            get { return sizeName; }
            set { SetProperty(ref sizeName, value); }
        }
    }
}

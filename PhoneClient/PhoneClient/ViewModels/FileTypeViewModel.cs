namespace PhoneClient.ViewModels
{
    public class FileTypeViewModel : BaseViewModel
    {
        private string typeName;
        public string TypeName
        {
            get { return typeName; }
            set { SetProperty(ref typeName, value); }

        }

        private bool isChecked;
        public bool IsChecked
        {
            get { return isChecked; }
            set { SetProperty(ref isChecked, value); }

        }
    }
}

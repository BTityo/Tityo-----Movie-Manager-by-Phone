namespace DesktopServer.ViewModels
{
    public class FileTypeViewModel : BaseViewModel
    {
        private string typeName;
        public string TypeName
        {
            get { return typeName; }
            set
            {
                if (typeName != value)
                {
                    typeName = value;
                    OnPropertyChanged("TypeName");
                }
            }
        }

        private bool isChecked;
        public bool IsChecked
        {
            get { return isChecked; }
            set
            {
                if (isChecked != value)
                {
                    isChecked = value;
                    OnPropertyChanged("IsChecked");
                }
            }
        }
    }
}

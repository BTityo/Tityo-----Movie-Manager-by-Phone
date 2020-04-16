using DesktopServer.ViewModels;
using FirstFloor.ModernUI.Windows;
using FirstFloor.ModernUI.Windows.Navigation;
using System;
using System.Windows.Controls;

namespace DesktopServer.Views.Content
{
    /// <summary>
    /// Interaction logic for MovieProfile.xaml
    /// </summary>
    public partial class MovieProfile : UserControl, IContent
    {
        private int movieViewModelId;

        public MovieProfile()
        {
            InitializeComponent();
        }

        public MovieProfile(int movieViewModelId) : this()
        {
        }

        public void OnFragmentNavigation(FragmentNavigationEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.Fragment))
            {
                this.movieViewModelId = Convert.ToInt32(e.Fragment);
            }
        }

        public void OnNavigatedFrom(NavigationEventArgs e)
        {
        }

        public void OnNavigatedTo(NavigationEventArgs e)
        {
        }

        public void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
        }
    }
}

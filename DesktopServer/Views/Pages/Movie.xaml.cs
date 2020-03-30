using DesktopServer.Map;
using DesktopServer.Service;
using DesktopServer.ViewModels;
using DesktopServer.Views.Content;
using FirstFloor.ModernUI.Windows.Controls;
using FirstFloor.ModernUI.Windows.Navigation;
using MobileMovieManager.BLL.FileServer;
using MobileMovieManager.BLL.Service;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using DALModel = MobileMovieManager.DAL.Models;

namespace DesktopServer.Views.Pages
{
    /// <summary>
    /// Interaction logic for Movie.xaml
    /// </summary>
    public partial class Movie : UserControl
    {
        // Services
        private readonly SettingService settingService;
        private readonly MovieService movieService;
        private readonly FilterService filterService;
        private readonly FileSizeService fileSizeService;
        private readonly FileTypeService fileTypeService;
        // ViewModels
        private SettingViewModel settingViewModel;
        private ObservableCollection<MovieViewModel> movieViewModels;
        private FilterViewModel filterViewModel;
        // Models
        private List<DALModel.Movie> moviesFromDb;
        private List<DALModel.Movie> moviesFromFileServer;

        public Movie()
        {
            filterViewModel = new FilterViewModel();
            settingViewModel = new SettingViewModel();
            movieViewModels = new ObservableCollection<MovieViewModel>();
            settingService = new SettingService(Constants.LocalDBPath);
            movieService = new MovieService(Constants.LocalDBPath);
            filterService = new FilterService(Constants.LocalDBPath);
            fileSizeService = new FileSizeService(Constants.LocalDBPath);
            fileTypeService = new FileTypeService(Constants.LocalDBPath);

            InitializeComponent();
            // Set fullscreen
            startTaskSetMovieViewModel();
        }

        private async Task<ObservableCollection<MovieViewModel>> setViewModel()
        {
            // We just update Setting and Filter models, no delete - no insert
            var setting = await settingService.GetSettingByIdAsync(1);
            var filter = await filterService.GetFilterByIdAsync(1);
            // FileSizes - FileTypes
            var fileSizes = await fileSizeService.GetFileSizesAsync();
            var fileTypes = await fileTypeService.GetFileTypesAsync();

            // Get viewmodels
            this.Dispatcher.Invoke(() =>
                {
                    settingViewModel = SettingMap.MapToSettingViewModel(setting);
                    filterViewModel = FilterMap.MapToFilterViewModel(filter);
                    filterViewModel.FileTypeViewModels = FileTypeMap.MapToFileTypeViewModelList(fileTypes);
                    filterViewModel.FileSizeViewModels = FileSizeMap.MapToFileSizeViewModelList(fileSizes);

                    dockPanelFilter.DataContext = filterViewModel;
                }
            );

            // Show message if MoviesPath is not set
            if (!Directory.Exists(settingViewModel.MoviesPath))
            {
                this.Dispatcher.Invoke(() =>
                {
                    MessageBoxResult result = ModernDialog.ShowMessage("A(z) '" + settingViewModel.MoviesPath + "' mappa nem létezik, így nem lehet filmeket keresni! Szeretnéd megváltoztatni most?", "'Filmek' mappa nem létezik", MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.Yes)
                    {
                        IInputElement target = NavigationHelper.FindFrame("_top", this);
                        NavigationCommands.GoToPage.Execute("/Views/Content/SettingServer.xaml", target);
                    }
                    else
                    {
                        IInputElement target = NavigationHelper.FindFrame("_top", this);
                        LinkCommands.NavigateLink.Execute("/Views/Pages/Introduction.xaml", target);
                    }
                });
            }
            // Get movies and set viewmodel
            else
            {
                // Get movies from database
                moviesFromDb = await movieService.GetMoviesAsync();
                movieViewModels = MovieMap.MapToMovieViewModelList(moviesFromDb);

                await Task.Factory.StartNew(async () =>
                {
                    moviesFromFileServer = await MovieServer.GetMovieTitles(settingViewModel.MoviesPath, FileTypeMap.MapToFileTypeList(settingViewModel.FileTypeViewModels));
                });

                // We have no movies in database
                if (movieViewModels.Count <= 0)
                {
                    var insertedMovies = await movieService.InsertAllMovieAsync(moviesFromFileServer);
                    movieViewModels = MovieMap.MapToMovieViewModelList(insertedMovies);
                }
                else
                {
                    // Delete and insert changes
                    var deleted = moviesFromDb.Where(m => !moviesFromFileServer.Select(c => c.Title).Contains(m.Title)).ToList();
                    var inserted = moviesFromFileServer.Where(c => !moviesFromDb.Select(m2 => m2.Title).Contains(c.Title)).ToList();

                    if (deleted != null && deleted.Count > 0)
                    {
                        // Delete my movies
                        await movieService.DeleteMoviesByListIdAsync(deleted.Select(d => d.Id).ToList());
                    }
                    if (inserted != null && inserted.Count > 0)
                    {
                        // Insert my movies
                        await movieService.InsertAllMovieAsync(inserted);
                    }

                    // Correct current movies what can search with TMDB
                    moviesFromDb = await movieService.GetMoviesAsync();
                    movieViewModels = MovieMap.MapToMovieViewModelList(moviesFromDb);
                }
            };


            return await Task.FromResult(movieViewModels);
        }

        private async void startTaskSetMovieViewModel()
        {
            await showLoading();

            await Task.Factory.StartNew(async () =>
            {
                await setViewModel();

                await hideLoading();

                await this.Dispatcher.InvokeAsync(() =>
                {
                    listViewMovies.ItemsSource = movieViewModels;
                });
            });
        }

        // Save movie model changes and filter changes
        private async void buttonSave_Click(object sender, RoutedEventArgs e)
        {
            await showLoading();

            await Task.Factory.StartNew(async () =>
            {
                var movies = MovieMap.MapToMovieList(movieViewModels);
                await movieService.UpdateAllMovieAsync(movies);

                var filter = FilterMap.MapFilterViewModelToFilter(filterViewModel);
                await filterService.UpdateFilterAsync(filter);

                await hideLoading();
            });
        }

        // Hide Loading screen
        private async Task hideLoading()
        {
            await Task.Run(async () =>
                await this.Dispatcher.InvokeAsync(() =>
                {
                    dockPanelLoading.Children.Clear();
                    dockPanelLoading.Visibility = Visibility.Collapsed;
                    stackPanelSearch.Visibility = Visibility.Visible;
                    dockPanelFilter.Visibility = Visibility.Visible;
                    borderMovies.Visibility = Visibility.Visible;
                    stackPanelButtons.Visibility = Visibility.Visible;
                })
            );
        }

        // Show Loading screen
        private async Task showLoading()
        {
            await Task.Run(async () =>
                await this.Dispatcher.InvokeAsync(() =>
                {
                    stackPanelSearch.Visibility = Visibility.Collapsed;
                    dockPanelFilter.Visibility = Visibility.Collapsed;
                    borderMovies.Visibility = Visibility.Collapsed;
                    stackPanelButtons.Visibility = Visibility.Collapsed;
                    dockPanelLoading.Visibility = Visibility.Visible;
                    dockPanelLoading.Children.Add(new Loading());
                })
            );
        }

        private void buttonRefresh_Click(object sender, RoutedEventArgs e)
        {
            startTaskSetMovieViewModel();
        }

        // Search event
        private void textBoxSearch_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            string searched = (((TextBox)sender).Text + e.Key.ToString()).ToUpper().Trim();

            if (searched.Length > 3)
            {
                this.Dispatcher.Invoke(() =>
                {
                    listViewMovies.ItemsSource = movieViewModels.Where(m => m.Title.ToUpper().Contains(searched) || m.FullPath.ToUpper().Contains(searched) || m.FolderTitle.ToUpper().Contains(searched) || m.FileType.TypeName.ToUpper().Contains(searched));
                });
            }
        }

        // Filter event
        private void buttonFilter_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

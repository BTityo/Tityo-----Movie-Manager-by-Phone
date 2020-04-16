using DesktopServer.ViewModels;
using FirstFloor.ModernUI.Windows.Controls;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace DesktopServer.Views.Pages
{
    /// <summary>
    /// Filter movies functionality
    /// </summary>
    public partial class Movie
    {
        private ObservableCollection<MovieViewModel> filteredMovieViewModels = new ObservableCollection<MovieViewModel>();

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

        // Set filters to default
        private void buttonIsAllMovies_Click(object sender, RoutedEventArgs e)
        {
            setFilterControlsToDefault();

            filteredMovieViewModels = movieViewModels;
            listViewMovies.ItemsSource = filteredMovieViewModels;
        }

        // Filter event
        private async void buttonFilter_Click(object sender, RoutedEventArgs e)
        {
            await showLoading();

            // Check file size as double in GigaByte
            double minSize = 0;
            double maxSize = 10000;
            switch (filterViewModel.SelectedSize.Id)
            {
                case 2:
                    maxSize = 1.00;
                    minSize = 0;
                    break;
                case 3:
                    maxSize = 2.00;
                    minSize = 1.00;
                    break;
                case 4:
                    maxSize = 5.00;
                    minSize = 2.00;
                    break;
                case 5:
                    maxSize = 10000.00;
                    minSize = 5.00;
                    break;
                default:
                    maxSize = 10000;
                    minSize = 0;
                    break;
            }

            // Check SelectedTypeId
            bool isAllType = (filterViewModel.SelectedType.Id > 1) ? false : true;

            this.Dispatcher.Invoke(() =>
            {
                if (isAllType)
                {
                    filteredMovieViewModels = new ObservableCollection<MovieViewModel>(movieViewModels.Where(m => m.IsSeries == filterViewModel.IsSeries && m.IsMoreCD == filterViewModel.IsMoreCD && m.IsFavorite == filterViewModel.IsFavorite && Convert.ToDouble(m.Size.Split(" ")[0]) >= minSize && Convert.ToDouble(m.Size.Split(" ")[0]) < maxSize && m.CreationTime >= filterViewModel.DateFrom && m.CreationTime <= filterViewModel.DateTo));
                }
                else
                {
                    filteredMovieViewModels = new ObservableCollection<MovieViewModel>(movieViewModels.Where(m => m.IsSeries == filterViewModel.IsSeries && m.IsMoreCD == filterViewModel.IsMoreCD && m.IsFavorite == filterViewModel.IsFavorite && m.FileType.Id == filterViewModel.SelectedType.Id && Convert.ToDouble(m.Size.Split(" ")[0]) >= minSize && Convert.ToDouble(m.Size.Split(" ")[0]) < maxSize && m.CreationTime >= filterViewModel.DateFrom && m.CreationTime <= filterViewModel.DateTo));
                }

                listViewMovies.ItemsSource = filteredMovieViewModels;
            });

            await hideLoading();
        }

        // Set all filter controls to default, except IsAllMovies
        private void setFilterControlsToDefault()
        {
            // Set comboboxes to default '0' index
            foreach (ComboBox comboBox in FindVisualChildren<ComboBox>(dockPanelFilter))
            {
                switch (comboBox.Tag.ToString())
                {
                    case "SelectedType":
                        filterViewModel.SelectedType = filterViewModel.FileTypeViewModels[0];
                        break;
                    case "SelectedSize":
                        filterViewModel.SelectedSize = filterViewModel.FileSizeViewModels[0];
                        break;
                    default:
                        break;
                }
            }

            // Set checkboxes to default false
            foreach (CheckBox checkBox in FindVisualChildren<CheckBox>(dockPanelFilter))
            {
                filterViewModel.IsSeries = false;
                filterViewModel.IsFavorite = false;
                filterViewModel.IsMoreCD = false;
            }

            // Set calendars to default value
            foreach (Calendar calendar in FindVisualChildren<Calendar>(dockPanelFilter))
            {
                switch (calendar.Tag.ToString())
                {
                    case "CalendarFrom":
                        filterViewModel.DateFrom = new DateTime(2010, 01, 01);
                        break;
                    case "CalendarTo":
                        filterViewModel.DateTo = DateTime.Now;
                        break;
                    default:
                        break;
                }
            }
        }

        // Get controls
        public static IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }

        #region Order
        private async void buttonOrder_Click(object sender, RoutedEventArgs e)
        {
            await showLoading();

            // Tag of the button -> Order type and direction
            string tag = ((ModernButton)sender).Tag.ToString();

            if (filteredMovieViewModels == null || filteredMovieViewModels.Count <= 0)
            {
                filteredMovieViewModels = movieViewModels;
            }

            switch (tag)
            {
                case "TitleUp":
                    filteredMovieViewModels = new ObservableCollection<MovieViewModel>(filteredMovieViewModels.OrderBy(o => o.Title));
                    break;
                case "TitleDown":
                    filteredMovieViewModels = new ObservableCollection<MovieViewModel>(filteredMovieViewModels.OrderByDescending(o => o.Title));
                    break;
                case "DateUp":
                    filteredMovieViewModels = new ObservableCollection<MovieViewModel>(filteredMovieViewModels.OrderBy(o => o.CreationTime));
                    break;
                case "DateDown":
                    filteredMovieViewModels = new ObservableCollection<MovieViewModel>(filteredMovieViewModels.OrderByDescending(o => o.CreationTime));
                    break;
                case "SizeUp":
                    filteredMovieViewModels = new ObservableCollection<MovieViewModel>(filteredMovieViewModels.OrderBy(o => Convert.ToDouble(o.Size.Split(' ')[0])));
                    break;
                case "SizeDown":
                    filteredMovieViewModels = new ObservableCollection<MovieViewModel>(filteredMovieViewModels.OrderByDescending(o => Convert.ToDouble(o.Size.Split(' ')[0])));
                    break;
                default:
                    break;
            }

            listViewMovies.ItemsSource = filteredMovieViewModels;

            await hideLoading();
        }
        #endregion
    }
}

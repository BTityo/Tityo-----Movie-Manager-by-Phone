using System;
using System.Collections.Generic;

namespace DesktopServer.ViewModels
{
    public class FilterViewModel : BaseViewModel
    {
        private int selectedTypeId;
        public int SelectedTypeId
        {
            get { return selectedType.Id - 1; }
            set
            {
                if (selectedTypeId != value)
                {
                    selectedTypeId = value;
                    OnPropertyChanged("SelectedTypeId");
                }
            }
        }

        private int selectedSizeId;
        public int SelectedSizeId
        {
            get { return selectedSize.Id - 1; }
            set
            {
                if (selectedSizeId != value)
                {
                    selectedSizeId = value;
                    OnPropertyChanged("SelectedSizeId");
                }
            }
        }

        private FileTypeViewModel selectedType;
        public FileTypeViewModel SelectedType
        {
            get { return selectedType; }
            set
            {
                if (selectedType != value)
                {
                    selectedType = value;
                    OnPropertyChanged("SelectedType");
                }
            }
        }

        private List<FileTypeViewModel> fileTypeViewModels;
        public List<FileTypeViewModel> FileTypeViewModels
        {
            get { return fileTypeViewModels; }
            set
            {
                if (fileTypeViewModels != value)
                {
                    fileTypeViewModels = value;
                    OnPropertyChanged("FileTypeViewModels");
                }
            }
        }

        private FileSizeViewModel selectedSize;
        public FileSizeViewModel SelectedSize
        {
            get { return selectedSize; }
            set
            {
                if (selectedSize != value)
                {
                    selectedSize = value;
                    OnPropertyChanged("SelectedSize");
                }
            }
        }

        private List<FileSizeViewModel> fileSizeViewModels;
        public List<FileSizeViewModel> FileSizeViewModels
        {
            get { return fileSizeViewModels; }
            set
            {
                if (fileSizeViewModels != value)
                {
                    fileSizeViewModels = value;
                    OnPropertyChanged("FileSizeViewModels");
                }
            }
        }

        private bool isSeries;
        public bool IsSeries
        {
            get { return isSeries; }
            set
            {
                if (isSeries != value)
                {
                    isSeries = value;
                    OnPropertyChanged("IsSeries");
                }
            }
        }

        private bool isFavorite;
        public bool IsFavorite
        {
            get { return isFavorite; }
            set
            {
                if (isFavorite != value)
                {
                    isFavorite = value;
                    OnPropertyChanged("IsFavorite");
                }
            }
        }

        private bool isMoreCD;
        public bool IsMoreCD
        {
            get { return isMoreCD; }
            set
            {
                if (isMoreCD != value)
                {
                    isMoreCD = value;
                    OnPropertyChanged("IsMoreCD");
                }
            }
        }

        private bool isAllMovies;
        public bool IsAllMovies
        {
            get { return isAllMovies; }
            set
            {
                if (isAllMovies != value)
                {
                    isAllMovies = value;
                    OnPropertyChanged("IsAllMovies");
                }
            }
        }

        private DateTime dateFrom;
        public DateTime DateFrom
        {
            get { return dateFrom; }
            set
            {
                if (dateFrom != value)
                {
                    dateFrom = value;
                    OnPropertyChanged("DateFrom");
                }
            }
        }

        private DateTime dateTo;
        public DateTime DateTo
        {
            get { return dateTo; }
            set
            {
                if (dateTo != value)
                {
                    dateTo = value;
                    OnPropertyChanged("DateTo");
                }
            }
        }
    }
}

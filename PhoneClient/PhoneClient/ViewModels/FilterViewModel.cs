using System;
using System.Collections.Generic;

namespace PhoneClient.ViewModels
{
    public class FilterViewModel : BaseViewModel
    {
        private int selectedTypeId;
        public int SelectedTypeId
        {
            get { return selectedType.Id - 1; }
            set { SetProperty(ref selectedTypeId, value); }
        }

        private int selectedSizeId;
        public int SelectedSizeId
        {
            get { return selectedSize.Id - 1; }
            set { SetProperty(ref selectedSizeId, value); }
        }

        private FileTypeViewModel selectedType;
        public FileTypeViewModel SelectedType
        {
            get { return selectedType; }
            set { SetProperty(ref selectedType, value); }
        }

        private List<FileTypeViewModel> fileTypeViewModels;
        public List<FileTypeViewModel> FileTypeViewModels
        {
            get { return fileTypeViewModels; }
            set { SetProperty(ref fileTypeViewModels, value); }
        }

        private FileSizeViewModel selectedSize;
        public FileSizeViewModel SelectedSize
        {
            get { return selectedSize; }
            set { SetProperty(ref selectedSize, value); }
        }

        private List<FileSizeViewModel> fileSizeViewModels;
        public List<FileSizeViewModel> FileSizeViewModels
        {
            get { return fileSizeViewModels; }
            set { SetProperty(ref fileSizeViewModels, value); }
        }

        private bool isSeries;
        public bool IsSeries
        {
            get { return isSeries; }
            set { SetProperty(ref isSeries, value); }
        }

        private bool isFavorite;
        public bool IsFavorite
        {
            get { return isFavorite; }
            set { SetProperty(ref isFavorite, value); }
        }

        private bool isMoreCD;
        public bool IsMoreCD
        {
            get { return isMoreCD; }
            set { SetProperty(ref isMoreCD, value); }
        }

        private bool isAllMovies;
        public bool IsAllMovies
        {
            get { return isAllMovies; }
            set { SetProperty(ref isAllMovies, value); }
        }

        private DateTime dateFrom;
        public DateTime DateFrom
        {
            get { return dateFrom; }
            set { SetProperty(ref dateFrom, value); }
        }

        private DateTime dateTo;
        public DateTime DateTo
        {
            get { return dateTo; }
            set { SetProperty(ref dateTo, value); }
        }
    }
}

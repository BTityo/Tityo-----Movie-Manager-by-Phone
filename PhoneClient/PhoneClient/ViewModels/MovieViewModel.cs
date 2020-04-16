using MobileMovieManager.DAL.Models;
using System;

namespace PhoneClient.ViewModels
{
    public class MovieViewModel : BaseViewModel
    {
        private string folderTitle;
        public string FolderTitle
        {
            get { return folderTitle; }
            set { SetProperty(ref folderTitle, value); }
        }

        private string fullPath;
        public string FullPath
        {
            get { return fullPath; }
            set { SetProperty(ref fullPath, value); }
        }

        private string size;
        public string Size
        {
            get { return size; }
            set { SetProperty(ref size, value); }
        }

        private DateTime creationTime;
        public DateTime CreationTime
        {
            get { return creationTime; }
            set { SetProperty(ref creationTime, value); }
        }

        private bool isFavorite;
        public bool IsFavorite
        {
            get { return isFavorite; }
            set { SetProperty(ref isFavorite, value); }
        }

        private string imdbId;
        public string ImdbId
        {
            get { return imdbId; }
            set { SetProperty(ref imdbId, value); }
        }

        private bool isMoreCD;
        public bool IsMoreCD
        {
            get { return isMoreCD; }
            set { SetProperty(ref isMoreCD, value); }
        }

        private bool isSeries;
        public bool IsSeries
        {
            get { return isSeries; }
            set { SetProperty(ref isSeries, value); }
        }

        private FileType fileType;
        public virtual FileType FileType
        {
            get { return fileType; }
            set { SetProperty(ref fileType, value); }
        }
    }
}

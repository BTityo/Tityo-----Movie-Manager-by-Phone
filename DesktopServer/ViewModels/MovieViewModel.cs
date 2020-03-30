using MobileMovieManager.DAL.Models;
using System;

namespace DesktopServer.ViewModels
{
    public class MovieViewModel : BaseViewModel
    {
        private string title;
        public string Title
        {
            get { return title; }
            set
            {
                if (title != value)
                {
                    title = value;
                    OnPropertyChanged("Title");
                }
            }
        }

        private string folderTitle;
        public string FolderTitle
        {
            get { return folderTitle; }
            set
            {
                if (folderTitle != value)
                {
                    folderTitle = value;
                    OnPropertyChanged("FolderTitle");
                }
            }
        }

        private string fullPath;
        public string FullPath
        {
            get { return fullPath; }
            set
            {
                if (fullPath != value)
                {
                    fullPath = value;
                    OnPropertyChanged("FullPath");
                }
            }
        }

        private string size;
        public string Size
        {
            get { return size; }
            set
            {
                if (size != value)
                {
                    size = value;
                    OnPropertyChanged("Size");
                }
            }
        }

        private DateTime creationTime;
        public DateTime CreationTime
        {
            get { return creationTime; }
            set
            {
                if (creationTime != value)
                {
                    creationTime = value;
                    OnPropertyChanged("CreationTime");
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

        private string imdbId;
        public string ImdbId
        {
            get { return imdbId; }
            set
            {
                if (imdbId != value)
                {
                    imdbId = value;
                    OnPropertyChanged("ImdbId");
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

        private FileType fileType;
        public virtual FileType FileType
        {
            get { return fileType; }
            set
            {
                if (fileType != value)
                {
                    fileType = value;
                    OnPropertyChanged("FileType");
                }
            }
        }
    }
}

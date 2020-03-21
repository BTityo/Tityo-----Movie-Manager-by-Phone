using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace MobileMovieManager.DAL.Models
{
    [Serializable]
    [Table("Movie", Schema = "Movie")]
    public class Movie : ISerializable
    {
        public Movie()
        {
        }

        [Key]
        private int id;
        public int Id { get { return id; } set { id = value; } }

        private string title;
        [Required]
        public string Title { get { return title; } set { title = value; } }

        private string folderTitle;
        [Required]
        public string FolderTitle { get { return folderTitle; } set { folderTitle = value; } }

        private string fullPath;
        [Required]
        public string FullPath { get { return fullPath; } set { fullPath = value; } }

        private string size;
        [MaxLength(50)]
        public string Size { get { return size; } set { size = value; } }

        private DateTime creationTime;
        [Required]
        public DateTime CreationTime { get { return creationTime; } set { creationTime = value; } }

        private bool isFavorite;
        public bool IsFavorite { get { return isFavorite; } set { isFavorite = value; } }


        private int fileTypeId;
        [Required]
        public int FileTypeId { get { return fileTypeId; } set { fileTypeId = value; } }

        private FileType fileType;
        public virtual FileType FileType { get { return fileType; } set { fileType = value; } }

        public IntPtr Handle => throw new NotImplementedException();

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            // Use the AddValue method to specify serialized values
            info.SetType(typeof(Movie));

            info.AddValue("id", id, typeof(int));
            info.AddValue("title", title, typeof(string));
            info.AddValue("folderTitle", folderTitle, typeof(string));
            info.AddValue("fullPath", fullPath, typeof(string));
            info.AddValue("fileTypeId", fileTypeId, typeof(int));
            info.AddValue("fileType", fileType, typeof(FileType));
            info.AddValue("size", size, typeof(string));
            info.AddValue("creationTime", creationTime, typeof(DateTime));
            info.AddValue("isFavorite", isFavorite, typeof(bool));
        }

        public Movie(SerializationInfo info, StreamingContext context)
        {
            // Reset the property value using the GetValue method.
            info.GetType();

            id = (int)info.GetValue("id", typeof(int));
            title = (string)info.GetValue("title", typeof(string));
            folderTitle = (string)info.GetValue("folderTitle", typeof(string));
            fullPath = (string)info.GetValue("fullPath", typeof(string));
            fileTypeId = (int)info.GetValue("fileTypeId", typeof(int));
            fileType = (FileType)info.GetValue("fileType", typeof(FileType));
            size = (string)info.GetValue("size", typeof(string));
            creationTime = (DateTime)info.GetValue("creationTime", typeof(DateTime));
            isFavorite = (bool)info.GetValue("isFavorite", typeof(bool));
        }
    }
}

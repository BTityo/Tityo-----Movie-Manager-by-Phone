using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace MobileMovieManager.DAL.Models
{
    [Serializable]
    [Table("FileType", Schema = "Setting")]
    public class FileType : ISerializable
    {
        public FileType()
        {
        }

        [Key]
        private int id;
        public int Id { get { return id; } set { id = value; } }
        [Required]
        private string typeName;
        public string TypeName { get { return typeName; } set { typeName = value; } }
        [Required]
        private bool isChecked;
        public bool IsChecked { get { return isChecked; } set { isChecked = value; } }


        public virtual ICollection<Filter> Filters { get; set; }
        public virtual ICollection<Movie> Movies { get; set; }


        public IntPtr Handle => throw new NotImplementedException();

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            // Use the AddValue method to specify serialized values
            info.SetType(typeof(FileType));

            info.AddValue("id", id, typeof(int));
            info.AddValue("typeName", typeName, typeof(string));
            info.AddValue("isChecked", isChecked, typeof(bool));
        }

        public FileType(SerializationInfo info, StreamingContext context)
        {
            // Reset the property value using the GetValue method.
            info.GetType();

            id = (int)info.GetValue("id", typeof(int));
            typeName = (string)info.GetValue("typeName", typeof(string));
            isChecked = (bool)info.GetValue("isChecked", typeof(bool));
        }
    }
}

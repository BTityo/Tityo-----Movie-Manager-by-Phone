using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MobileMovieManager.DAL.Models
{
    [Table("Filter", Schema = "Filter")]
    public class Filter
    {
        [Key]
        public int Id { get; set; }

        public bool IsSeries { get; set; }

        public bool IsFavorite { get; set; }

        public bool IsMoreCD { get; set; }

        public bool IsAllMovies { get; set; }

        public DateTime DateFrom { get; set; }

        public DateTime DateTo { get; set; }


        [Required]
        public int FileTypeId { get; set; }
        public virtual FileType FileType { get; set; }

        [Required]
        public int FileSizeId { get; set; }
        public virtual FileSize FileSize { get; set; }
    }
}

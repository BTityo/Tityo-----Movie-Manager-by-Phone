using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MobileMovieManager.DAL.Models
{
    [Table("FileSize", Schema = "Filter")]
    public class FileSize
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string SizeName { get; set; }
    }
}

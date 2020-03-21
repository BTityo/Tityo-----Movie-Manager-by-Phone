using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MobileMovieManager.DAL.Models
{
    [Table("FileType", Schema = "Setting")]
    public class FileType
    {
        [Key]
        public int Id { get; set; }
        [Required, MaxLength(40)]
        public string TypeName { get; set; }
        [Required]
        public bool IsChecked { get; set; }

        public virtual ICollection<Movie> Movies { get; set; }
    }
}

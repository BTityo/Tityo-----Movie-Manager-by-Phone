using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MobileMovieManager.DAL.Models
{
    [Table("Setting", Schema = "Setting")]
    public class Setting
    {
        [Key]
        public int Id { get; set; }

        // Server settings
        public string ConnectedPhone { get; set; }
        [Required, MaxLength(50)]
        public string ServerIP { get; set; }
        [Required, Range(1000, 10000)]
        public int Port { get; set; }

        // User settings
        [Required]
        public bool IsStartWithWindows { get; set; }
        [Required]
        public bool IsSearchForSubtitle { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string MoviesPath { get; set; }

        // Display settings
        public string SelectedPalette { get; set; }
        public string SelectedTheme { get; set; }
        public byte[] SelectedAccentColor { get; set; }


        public virtual ICollection<FileType> FileTypes { get; set; }
    }
}

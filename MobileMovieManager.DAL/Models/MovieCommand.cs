using MobileMovieManager.DAL.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace MobileMovieManager.DAL.Models
{
    public class MovieCommand
    {
        [Key]
        public int Id { get; set; }
        public MovieCommandEnum.MovieCommand Command { get; set; }
        public string CommandDescription { get; set; }
    }
}

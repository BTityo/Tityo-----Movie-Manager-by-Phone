using MobileMovieManager.DAL.Models.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MobileMovieManager.DAL.Models
{
    [Table("Logger", Schema = "Log")]
    public class Logger
    {
        [Key]
        public int Id { get; set; }
        public string Message { get; set; }
        public LoggerEnum.Sender Sender { get; set; }
        public DateTime LogDate { get; set; }
    }
}

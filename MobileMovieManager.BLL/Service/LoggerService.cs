using MobileMovieManager.DAL.Models;
using System.Collections.Generic;

namespace MobileMovieManager.BLL.Service
{
    public class LoggerService
    {
        public int LastId { get; set; }
        public List<Logger> Loggers { get; set; }

        public LoggerService()
        {
            Loggers = new List<Logger>();
            LastId = 0;
        }

        // Singleton
        private static LoggerService _instance;
        public static LoggerService Instance
        {
            get
            {
                if (_instance == null) _instance = new LoggerService();
                return _instance;
            }
        }
    }
}

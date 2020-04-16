using DesktopServer.ViewModels;
using MobileMovieManager.DAL.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace DesktopServer.Map
{
    public static class LoggerMap
    {
        public static Logger MapLoggerViewModelToLogger(LoggerViewModel loggerViewModel)
        {
            if (loggerViewModel != null)
            {
                return new Logger()
                {
                    Id = loggerViewModel.Id,
                    Message = loggerViewModel.Message,
                    Sender = loggerViewModel.Sender,
                    LogDate = loggerViewModel.LogDate
                };
            }
            else
            {
                return null;
            }
        }

        public static LoggerViewModel MapToLoggerViewModel(Logger logger)
        {
            if (logger != null)
            {
                return new LoggerViewModel()
                {
                    Id = logger.Id,
                    Message = logger.Message,
                    Sender = logger.Sender,
                    LogDate = logger.LogDate
                };
            }
            else
            {
                return new LoggerViewModel();
            }
        }

        public static ObservableCollection<LoggerViewModel> MapToLoggerViewModelList(List<Logger> loggers)
        {
            if (loggers != null && loggers.Count > 0)
            {
                ObservableCollection<LoggerViewModel> loggerViewModels = new ObservableCollection<LoggerViewModel>();
                foreach (Logger logger in loggers)
                {
                    loggerViewModels.Add(new LoggerViewModel
                    {
                        Id = logger.Id,
                        Message = logger.Message,
                        Sender = logger.Sender,
                        LogDate = logger.LogDate
                    });
                }

                return loggerViewModels;
            }
            else
            {
                return null;
            }
        }

        public static List<Logger> MapToLoggerList(List<LoggerViewModel> loggerViewModels)
        {
            if (loggerViewModels != null && loggerViewModels.Count > 0)
            {
                List<Logger> loggers = new List<Logger>();
                foreach (LoggerViewModel loggerViewModel in loggerViewModels)
                {
                    loggers.Add(new Logger
                    {
                        Id = loggerViewModel.Id,
                        Message = loggerViewModel.Message,
                        Sender = loggerViewModel.Sender,
                        LogDate = loggerViewModel.LogDate
                    });
                }

                return loggers;
            }
            else
            {
                return null;
            }
        }
    }
}

using MobileMovieManager.BLL.Service;
using MobileMovieManager.DAL.Models;
using System;
using System.Net;
using System.Threading.Tasks;
using static MobileMovieManager.DAL.Models.Enums.LoggerEnum;

namespace MobileMovieManager.BLL.FileServer
{
    public static class FTPServer
    {
        //FTP jelszó ->%W7$q2S6
        public static async Task<bool> UploadDB()
        {
            bool isSuccess = false;

            try
            {
                isSuccess = true;

                using (var client = new WebClient())
                {
                    client.Credentials = new NetworkCredential("jelenleti", "%W7$q2S6");
                    await client.UploadFileTaskAsync("ftp://bergman.dima.hu/SQLite/MobileMovieManagerDB.db", WebRequestMethods.Ftp.UploadFile, Constants.LocalDBPath);
                }

                return isSuccess;
            }
            catch (Exception ex)
            {
                isSuccess = false;

                LoggerService.Instance.Loggers.Add(new Logger { Id = LoggerService.Instance.LastId + 1, Message = "Hiba az adatbázis feltöltése közben, FTP-n keresztül!" + Environment.NewLine + ex.Message, Sender = Sender.FileServer, LogDate = DateTime.Now });

                return isSuccess;
            }
        }

        public static async Task<bool> DownloadDB(string fileDownloadPath)
        {
            bool isSuccess = false;

            try
            {
                isSuccess = true;

                using (var client = new WebClient())
                {
                    client.Credentials = new NetworkCredential("jelenleti", "%W7$q2S6");
                    await client.DownloadFileTaskAsync("ftp://bergman.dima.hu/SQLite/MobileMovieManagerDB.db", fileDownloadPath);
                }

                return isSuccess;
            }
            catch (Exception ex)
            {
                isSuccess = false;

                LoggerService.Instance.Loggers.Add(new Logger { Id = LoggerService.Instance.LastId + 1, Message = "Hiba az adatbázis letöltése közben, FTP-n keresztül!" + Environment.NewLine + ex.Message, Sender = Sender.FileServer, LogDate = DateTime.Now });

                return isSuccess;
            }
        }
    }
}

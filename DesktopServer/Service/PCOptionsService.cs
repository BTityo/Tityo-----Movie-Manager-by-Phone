using System.Diagnostics;
using System.Runtime.InteropServices;
using static MobileMovieManager.DAL.Models.Enums.PcOptionEnum;

namespace DesktopServer.Service
{
    public static class PCOptionsService
    {
        // Lock PC
        [DllImport("user32")]
        public static extern void LockWorkStation();

        // Log off from PC
        [DllImport("user32")]
        public static extern bool ExitWindowsEx(uint uFlags, uint dwReason);

        // Hibernate or Sleep PC
        [DllImport("PowrProf.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern bool SetSuspendState(bool hibernate, bool forceCritical, bool disableWakeEvent);

        /// <summary>
        /// Set an option after movie is played - Return True if No Option
        /// </summary>
        /// <param name="pcOption"></param>
        /// <returns></returns>
        public static bool SetOption(PCOption pcOption)
        {
            bool isNoOption = false;

            // Set PCOption
            switch (pcOption)
            {
                case PCOption.Shutdown:
                    Process.Start("shutdown.exe", "/s /t 0");
                    break;
                case PCOption.Restart:
                    Process.Start("shutdown.exe", "/r /t 0");
                    break;
                case PCOption.Sleep:
                    SetSuspendState(false, true, true);
                    break;
                case PCOption.Hibernate:
                    SetSuspendState(true, true, true);
                    break;
                case PCOption.LogOff:
                    ExitWindowsEx(0, 0);
                    break;
                case PCOption.Lock:
                    LockWorkStation();
                    break;
                default:
                    isNoOption = true;
                    break;
            }

            return isNoOption;
        }
    }
}

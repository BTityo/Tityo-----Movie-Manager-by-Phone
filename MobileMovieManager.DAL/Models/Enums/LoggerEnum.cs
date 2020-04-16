using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace MobileMovieManager.DAL.Models.Enums
{
    public class LoggerEnum
    {
        [TypeConverter(typeof(EnumDescriptionTypeConverter))]
        public enum Sender
        {
            [Description("Socket Szerver")]
            SocketServer = 0,
            [Description("Socket Kliens")]
            SocketClient = 1,
            [Description("Fájl Szerver")]
            FileServer = 2,
            [Description("Média lejátszó")]
            MoviePlayer = 3,
            [Description("Beállítások")]
            Setting = 4
        }


        public IList<Sender> Senders
        {
            get
            {
                // Will result in a list like {"Tester", "Engineer"}
                return Enum.GetValues(typeof(Sender)).Cast<Sender>().ToList<Sender>();
            }
        }

        public Sender SenderProp
        {
            get;
            set;
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace MobileMovieManager.DAL.Models.Enums
{
    public class MovieCommandEnum
    {
        [TypeConverter(typeof(EnumDescriptionTypeConverter))]
        public enum MovieCommand
        {
            [Description("Lejátszás/Állj")]
            StartPauseMovie = 0,
            [Description("Stop")]
            StopMovie = 1,
            [Description("Hátra")]
            RewindMovie = 2,
            [Description("Előre")]
            ForwardMovie = 3,
            [Description("Hang+")]
            SoundPlus = 4,
            [Description("Hang-")]
            SoundMinus = 5,
            [Description("Némít")]
            Mute = 6,
            [Description("Teljes képernyő")]
            FullScreen = 7,
            [Description("PC Leállítás")]
            ShutdownPc = 8,
            [Description("PC Újraindítás")]
            Restart = 9,
            [Description("PC Hibernálás")]
            Hibernate = 10,
            [Description("PC Alvás")]
            Sleep = 11,
            [Description("PC Kijelentkezés")]
            LogOff = 12,
            [Description("PC Zárolás")]
            Lock = 13,
            [Description("Film bezárása")]
            CloseMovie = 14,
            [Description("Kapcsolat bezárása")]
            CloseClient = 15,
            [Description("Üres parancs")]
            None = 16,
            // PC Commands NOW
            [Description("PC Leállítás most")]
            ShutdownPcNow = 17,
            [Description("PC Újraindítás most")]
            RestartNow = 18,
            [Description("PC Hibernálás most")]
            HibernateNow = 19,
            [Description("PC Alvás most")]
            SleepNow = 20,
            [Description("PC Kijelentkezés most")]
            LogOffNow = 21,
            [Description("PC Zárolás most")]
            LockNow = 22
        }

        public IList<MovieCommand> MovieCommands
        {
            get
            {
                return Enum.GetValues(typeof(MovieCommand)).Cast<MovieCommand>().ToList<MovieCommand>();
            }
        }

        public MovieCommand MovieCommandProp
        {
            get;
            set;
        }
    }
}

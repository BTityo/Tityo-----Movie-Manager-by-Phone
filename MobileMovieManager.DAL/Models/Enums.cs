using System;
using System.Collections.Generic;
using System.Text;

namespace MobileMovieManager.DAL.Models
{
    public enum MovieCommands
    {
        StartPauseMovie = 0,
        StopMovie = 1,
        RewindMovie = 2,
        ForwardMovie = 3,
        SoundPlus = 4,
        SoundMinus = 5,
        Mute = 6,
        FullScreen = 7,
        ShutdownPc = 8,
        CloseMovie = 9,
        CloseClient = 10
    }
}

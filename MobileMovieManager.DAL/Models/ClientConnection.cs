using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace MobileMovieManager.DAL.Models
{
    public class ClientConnection
    {
        public Socket ClientSocket { get; set; }
        public bool IsConnected { get; set; }
        public bool IsPlayMovie { get; set; }
        public DateTime ConnectionStartDate { get; set; }
    }
}

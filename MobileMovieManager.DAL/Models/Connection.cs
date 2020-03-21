using System.Collections.Generic;
using System.Net.Sockets;

namespace MobileMovieManager.DAL.Models
{
    public class Connection
    {
        public List<Socket> ClientSockets { get; set; }
        public Socket ClientSocket { get; set; }
        public Socket ServerSocket { get; set; }

        public Connection()
        {
            ClientSockets = new List<Socket>();
        }

        private static Connection _instance;
        public static Connection Instance
        {
            get
            {
                if (_instance == null) _instance = new Connection();
                return _instance;
            }
        }
    }
}

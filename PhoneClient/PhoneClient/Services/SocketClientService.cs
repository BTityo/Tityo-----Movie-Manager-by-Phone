using MobileMovieManager.BLL.Service;
using MobileMovieManager.BLL.SocketServer;
using MobileMovieManager.DAL.Models;
using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using static MobileMovieManager.DAL.Models.Enums.LoggerEnum;

namespace PhoneClient.Services
{
    /// <summary>
    /// TCP/IP Client
    /// </summary>
    public class SocketClientService
    {
        public Connection Connection;
        private byte[] receivedBuffer;
        private string ipAdress;
        private int port;
        private byte attempts;

        public SocketClientService()
        {
            receivedBuffer = new byte[200000];
            ipAdress = "";
            port = 1234;
            attempts = 0;
        }

        // SocketClientService is 'Singleton'
        private static SocketClientService _instance;
        public static SocketClientService Instance
        {
            get
            {
                if (_instance == null) _instance = new SocketClientService();
                return _instance;
            }
        }

        // Open client connection
        public bool StartClientService(Socket clientSocket, string ipAdress, int port)
        {
            Connection.Instance.ClientSocket = new ClientConnection { ClientSocket = clientSocket, ConnectionStartDate = DateTime.Now, IsConnected = false, IsPlayMovie = false };
            this.ipAdress = ipAdress;
            this.port = port;

            // Connect to Server Socket in loop
            if (loopConnect(3))
            {
                // Begin Receive data
                Connection.Instance.ClientSocket.ClientSocket.BeginReceive(receivedBuffer, 0, receivedBuffer.Length, SocketFlags.None, new AsyncCallback(ReceiveData), Connection.Instance.ClientSocket.ClientSocket);
            }

            return Connection.Instance.ClientSocket.IsConnected;
        }

        // Close connection
        public void CloseClientService()
        {
            byte[] closeCommandBinary = BinaryConverter.ObjectToBinary(MobileMovieManager.DAL.Models.Enums.MovieCommandEnum.MovieCommand.CloseClient);
            Connection.Instance.ClientSocket.ClientSocket.BeginSend(closeCommandBinary, 0, closeCommandBinary.Length, SocketFlags.None, new AsyncCallback(SendCallback), Connection.Instance.ClientSocket.ClientSocket);

            if (Connection.Instance.ClientSocket.ClientSocket.Connected) Connection.Instance.ClientSocket.ClientSocket.Disconnect(true);

            LoggerService.Instance.Loggers.Add(new Logger { Id = LoggerService.Instance.LastId + 1, Message = "Kapcsolat bontva", Sender = Sender.SocketClient, LogDate = DateTime.Now });
        }

        // Connect again in loop (3 attempts)
        private bool loopConnect(byte maxAttempts)
        {
            Connection.Instance.ClientSocket.IsConnected = false;

            while (!Connection.Instance.ClientSocket.ClientSocket.Connected && attempts != maxAttempts)
            {
                try
                {
                    attempts++;
                    Connection.Instance.ClientSocket.ClientSocket.Connect(IPAddress.Parse(ipAdress), port);
                    LoggerService.Instance.Loggers.Add(new Logger { Id = LoggerService.Instance.LastId + 1, Message = "Kapcsolódva a szerverhez!\nPróbálkozások száma: " + attempts.ToString(), Sender = Sender.SocketClient, LogDate = DateTime.Now });

                    // Set client socket
                    Connection.Instance.ClientSocket.IsConnected = true;
                }
                catch (SocketException ex)
                {
                    Connection.Instance.ClientSocket.IsConnected = false;
                    LoggerService.Instance.Loggers.Add(new Logger { Id = LoggerService.Instance.LastId + 1, Message = "Hiba a szerverhez való kapcsolódáskor, próbálkozások száma: " + attempts.ToString() + "\n" + ex.Message, Sender = Sender.SocketClient, LogDate = DateTime.Now });

                    // Close connection
                    //ClientSocket.Close();
                    //ClientSocket.Dispose();
                }
            }

            if (!Connection.Instance.ClientSocket.IsConnected)
            {
                LoggerService.Instance.Loggers.Add(new Logger { Id = LoggerService.Instance.LastId + 1, Message = "Nem sikerült kapcsolódni a szerverhez!\nBiztosan fut a szerver?\nPróbálkozások száma: " + attempts.ToString(), Sender = Sender.SocketClient, LogDate = DateTime.Now });

                // Close connection
                Connection.Instance.ClientSocket.ClientSocket.Close();
                Connection.Instance.ClientSocket.ClientSocket.Dispose();
            }

            return Connection.Instance.ClientSocket.IsConnected;
        }

        // Receive data from Socket Server
        private async void ReceiveData(IAsyncResult ar)
        {
            Socket socket = (Socket)ar.AsyncState;
            if (socket.Connected)
            {
                int received = socket.EndReceive(ar);
                // Set server socket
                Connection.Instance.ServerSocket = socket;
                byte[] dataBuf = new byte[received];
                Array.Copy(receivedBuffer, dataBuf, received);
                await Task.Delay(50);
                // MovieCommand from Server
                MobileMovieManager.DAL.Models.Enums.MovieCommandEnum.MovieCommand movieCommand = BinaryConverter.BinaryToCommandEnum(dataBuf);

                // Listen again
                Connection.Instance.ClientSocket.ClientSocket.BeginReceive(receivedBuffer, 0, receivedBuffer.Length, SocketFlags.None, new AsyncCallback(ReceiveData), Connection.Instance.ClientSocket.ClientSocket);

                // Close Client
                if (movieCommand == MobileMovieManager.DAL.Models.Enums.MovieCommandEnum.MovieCommand.CloseClient)
                {

                    //Device.BeginInvokeOnMainThread(async () =>
                    //    await App.Current.MainPage.Navigation.PushModalAsync(new MoviesListPage(movies))
                    //);
                }
                else
                {
                    LoggerService.Instance.Loggers.Add(new Logger { Id = LoggerService.Instance.LastId + 1, Message = "Adat érkezett a szervertől", Sender = Sender.SocketClient, LogDate = DateTime.Now });
                }
            }
            else
            {
                LoggerService.Instance.Loggers.Add(new Logger { Id = LoggerService.Instance.LastId + 1, Message = "Nem sikerült kapcsolódni a szerverhez!", Sender = Sender.SocketClient, LogDate = DateTime.Now });
            }
        }

        // Send a movie to Server and Play on Server
        public void SendMovie(Movie selectedMovie)
        {
            byte[] selectedMovieBinary = BinaryConverter.ObjectToBinary(selectedMovie);
            Connection.Instance.ClientSocket.ClientSocket.BeginSend(selectedMovieBinary, 0, selectedMovieBinary.Length, SocketFlags.None, new AsyncCallback(SendCallback), Connection.Instance.ClientSocket.ClientSocket);
            Connection.Instance.ClientSocket.IsPlayMovie = true;
        }

        // Send a command to Server and Server will process the command
        public void SendCommand(MobileMovieManager.DAL.Models.Enums.MovieCommandEnum.MovieCommand movieCommand)
        {
            byte[] movieCommandBinary = BinaryConverter.ObjectToBinary(movieCommand);
            Connection.Instance.ClientSocket.ClientSocket.BeginSend(movieCommandBinary, 0, movieCommandBinary.Length, SocketFlags.None, new AsyncCallback(SendCallback), Connection.Instance.ClientSocket.ClientSocket);
        }

        // Send object
        private void SendCallback(IAsyncResult ar)
        {
            Socket socket = (Socket)ar.AsyncState;
            socket.EndSend(ar);
        }
    }
}

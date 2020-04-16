using DesktopServer.Map;
using DesktopServer.ViewModels;
using DesktopServer.Views.Windows;
using FirstFloor.ModernUI.Windows.Controls;
using MobileMovieManager.BLL.FileServer;
using MobileMovieManager.BLL.Service;
using MobileMovieManager.BLL.SocketServer;
using MobileMovieManager.DAL.Models;
using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Windows;
using static MobileMovieManager.DAL.Models.Enums.LoggerEnum;
using static MobileMovieManager.DAL.Models.Enums.PcOptionEnum;
using MovieCommand = MobileMovieManager.DAL.Models.Enums.MovieCommandEnum.MovieCommand;

namespace DesktopServer.Service
{
    /// <summary>
    /// TCP/IP Server
    /// </summary>
    public class SocketServerService
    {
        private byte[] buffer;
        private Socket serverSocket;

        private Connection connection;
        private SettingViewModel settingViewModel;
        private MovieService movieService;

        public SocketServerService()
        {
            connection = new Connection();
            movieService = new MovieService(Constants.LocalDBPath);
            buffer = new byte[200000];
        }

        // SocketServerService is 'Singleton'
        private static SocketServerService _instance;
        public static SocketServerService Instance
        {
            get
            {
                if (_instance == null) _instance = new SocketServerService();
                return _instance;
            }
        }

        // Start TCP/IP Server
        public void StartServer(SettingViewModel settingViewModel)
        {
            this.settingViewModel = settingViewModel;

            // Init clients
            connection = new Connection();

            // Init socket - Communicating with socket(TCP)
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            serverSocket.Bind(new IPEndPoint(IPAddress.Any, settingViewModel.Port));
            // Maximum 10 client can connect to Socket Server
            serverSocket.Listen(10);
            connection.ServerSocket = serverSocket;
            serverSocket.BeginAccept(new AsyncCallback(AcceptCallbackAsync), null);

            LoggerService.Instance.Loggers.Add(new Logger { Id = LoggerService.Instance.LastId + 1, Message = "A szerver elindult --- Bejövő kapcsolatok figyelése..." + Environment.NewLine + "\tPort: " + settingViewModel.Port + Environment.NewLine + "\tSzerver IP: " + settingViewModel.ServerIP, Sender = Sender.SocketServer, LogDate = DateTime.Now });
        }

        // Accept new connection
        private void AcceptCallbackAsync(IAsyncResult ar)
        {
            // Save new connection
            Socket socket = serverSocket.EndAccept(ar);
            if (connection.ClientSockets.FirstOrDefault(c => c.ClientSocket == socket) == null)
            {
                connection.ClientSockets.Add(new ClientConnection { ClientSocket = socket, ConnectionStartDate = DateTime.Now, IsConnected = true, IsPlayMovie = false });
            }

            LoggerService.Instance.Loggers.Add(new Logger { Id = LoggerService.Instance.LastId + 1, Message = "Kliens kapcsolódva --- " + Environment.NewLine + "\t" + socket.RemoteEndPoint.ToString(), Sender = Sender.SocketServer, LogDate = DateTime.Now });

            // Receive
            socket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), socket);
            serverSocket.BeginAccept(new AsyncCallback(AcceptCallbackAsync), null);
        }

        // End of Send object
        private void SendCallback(IAsyncResult ar)
        {
            Socket socket = (Socket)ar.AsyncState;
            socket.EndSend(ar);
        }

        // Receive data
        private void ReceiveCallback(IAsyncResult ar)
        {
            Socket socket = ar.AsyncState as Socket;
            if (socket.Connected)
            {
                int received;
                try
                {
                    received = socket.EndReceive(ar);
                }
                catch (Exception ex)
                {
                    // Close connection
                    CloseClientConnection("Probléma az adatok fogadása közben..." + Environment.NewLine + ex.Message, socket.RemoteEndPoint);

                    return;
                }

                // Get received movie object or movie command
                if (received != 0)
                {
                    byte[] dataBuf = new byte[received];
                    Array.Copy(buffer, dataBuf, received);

                    // Client sent a movie
                    if (received != 194)
                    {
                        MovieViewModel movieViewModel = MovieMap.MapToMovieViewModel(BinaryConverter.BinaryToMovieObject(dataBuf));

                        LoggerService.Instance.Loggers.Add(new Logger { Id = LoggerService.Instance.LastId + 1, Message = "Kliens kiválasztott egy filmet ---\n\t" + movieViewModel.Title, Sender = Sender.SocketServer, LogDate = DateTime.Now });

                        connection.ClientSockets.FirstOrDefault(c => c.ClientSocket == socket).IsPlayMovie = true;

                        // Start playing movie
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            ((ModernWindow)Application.Current.MainWindow).Hide();
                            new PlayerWindow(movieViewModel).Show();
                        });


                        // Listen again (Receive data)
                        socket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), socket);
                    }
                    // Client sent a movie command
                    else
                    {
                        MovieCommand commandEnum = BinaryConverter.BinaryToCommandEnum(dataBuf);

                        if (commandEnum != MovieCommand.CloseClient)
                        {
                            // Process movie command
                            Application.Current.Dispatcher.Invoke(() =>
                            {
                                PlayerWindow playerWindow = Application.Current.Windows.OfType<PlayerWindow>().FirstOrDefault();
                                switch (commandEnum)
                                {
                                    case MovieCommand.StartPauseMovie:
                                        playerWindow.PlayPause();
                                        break;
                                    case MovieCommand.StopMovie:
                                        playerWindow.Stop();
                                        break;
                                    case MovieCommand.RewindMovie:
                                        playerWindow.Rewind();
                                        break;
                                    case MovieCommand.ForwardMovie:
                                        playerWindow.Forward();
                                        break;
                                    case MovieCommand.SoundPlus:
                                        playerWindow.SoundPlus();
                                        break;
                                    case MovieCommand.SoundMinus:
                                        playerWindow.SoundMinus();
                                        break;
                                    case MovieCommand.Mute:
                                        playerWindow.MuteOnOff();
                                        break;
                                    case MovieCommand.FullScreen:
                                        playerWindow.SetFullScreen();
                                        break;
                                    case MovieCommand.ShutdownPc:
                                        playerWindow.SetPcOption(PCOption.Shutdown);
                                        break;
                                    case MovieCommand.Restart:
                                        playerWindow.SetPcOption(PCOption.Restart);
                                        break;
                                    case MovieCommand.Hibernate:
                                        playerWindow.SetPcOption(PCOption.Hibernate);
                                        break;
                                    case MovieCommand.LogOff:
                                        playerWindow.SetPcOption(PCOption.LogOff);
                                        break;
                                    case MovieCommand.Sleep:
                                        playerWindow.SetPcOption(PCOption.Sleep);
                                        break;
                                    case MovieCommand.Lock:
                                        playerWindow.SetPcOption(PCOption.Lock);
                                        break;
                                    case MovieCommand.CloseMovie:
                                        playerWindow.CloseWindow();
                                        break;
                                }
                            });

                            // Listen again (Receive data)
                            socket.BeginReceive(buffer, 0, buffer.Length, SocketFlags.None, new AsyncCallback(ReceiveCallback), socket);
                        }
                        else
                        {
                            CloseClientConnection("A kliens bezárta a kapcsolatot", socket.RemoteEndPoint);
                        }
                    }
                }
                else
                {
                    CloseClientConnection("Nem érhető el a kliens", socket.RemoteEndPoint);
                }
            }
            else
            {
                CloseClientConnection("Nem érhető el a kliens", socket.RemoteEndPoint);
            }
        }

        // Close current client connection
        public void CloseClientConnection(string message, EndPoint remoteEndPoint = null)
        {
            LoggerService.Instance.Loggers.Add(new Logger { Id = LoggerService.Instance.LastId + 1, Message = message + " --- " + remoteEndPoint.ToString(), Sender = Sender.SocketServer, LogDate = DateTime.Now });

            // Convert CloseClient command to binary --- Notify Client about Server Closing
            //byte[] closeClientSocketCommand = BinaryConverter.ObjectToBinary(MovieCommands.CloseClient);
            // Send command
            //connection.ServerSocket.BeginSend(closeClientSocketCommand, 0, closeClientSocketCommand.Length, SocketFlags.None, new AsyncCallback(SendCallback), connection.ServerSocket);

            // Close client connection
            //Socket currentClient = connection.ClientSockets.FirstOrDefault(c => c.RemoteEndPoint.ToString() == connection.ServerSocket.RemoteEndPoint.ToString());
            //currentClient.Dispose();
            //currentClient.Close();
            //connection.ClientSockets.Remove(currentClient);
        }
    }
}

using DesktopServer.Service;
using DesktopServer.ViewModels;
using FirstFloor.ModernUI.Windows.Controls;
using MobileMovieManager.DAL.Models.Enums;
using NAudio.CoreAudioApi;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Threading;
using static MobileMovieManager.DAL.Models.Enums.PcOptionEnum;

namespace DesktopServer.Views.Windows
{
    /// <summary>
    /// Interaction logic for PlayerWindow.xaml
    /// </summary>
    public partial class PlayerWindow : Window
    {
        // Selected MovieViewModel
        private MovieViewModel movieViewModel;
        private PcOptionEnum pcOptionViewModel;

        // Timer for panels show/hide animation
        private DispatcherTimer dispatcherTimer;
        private bool isBack = false;

        // Player
        private DispatcherTimer videoTimer;
        private TimeSpan totalDuration;
        private bool isPlaying = false;
        private bool isFullScreen = true;
        private PCOption selectedPcOption;
        // Audio
        MMDeviceEnumerator deviceEnumerator = new MMDeviceEnumerator();
        MMDevice device;

        public PlayerWindow(MovieViewModel movieViewModel)
        {
            this.movieViewModel = movieViewModel;
            this.pcOptionViewModel = new PcOptionEnum();
            dispatcherTimer = new DispatcherTimer() { Interval = TimeSpan.FromSeconds(5) };
            dispatcherTimer.Tick += DispatcherTimer_Tick;

            InitializeComponent();

            this.DataContext = movieViewModel;
            gridTopMenu.DataContext = pcOptionViewModel;
            dispatcherTimer.Start();

            // Get default sound device
            device = deviceEnumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);
        }

        // If you move your screen from one display device to another, the Window will be black (bug) --- Solved with this
        private void Window_LocationChanged(object sender, EventArgs e)
        {
            if (Screen.AllScreens.Length > 1)
            {
                this.LocationChanged -= Window_LocationChanged;
                HwndSource hwndSource = PresentationSource.FromVisual(this) as HwndSource;
                HwndTarget hwndTarget = hwndSource.CompositionTarget;
                hwndTarget.RenderMode = RenderMode.SoftwareOnly;
            }
        }

        // Options after playing the movie
        private void comboBoxPcFunctions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            selectedPcOption = (PCOption)e.AddedItems[0];
        }

        // Start/Pause movie
        private void mediaElementMovie_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            mouseMoveAnimation();

            if (isPlaying)
            {
                mediaElementMovie.Pause();
                isPlaying = false;
            }
            else
            {
                mediaElementMovie.Play();
                isPlaying = true;
            }
        }

        // Control MediaElement with keyboard
        private void Window_PreviewKeyDown(object sender, System.Windows.Input.KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.None:
                    break;
                case Key.Tab:
                    break;
                case Key.Enter:
                    SetFullScreen();
                    break;
                case Key.Pause:
                    mediaElementMovie.Pause();
                    isPlaying = false;
                    break;
                case Key.Escape:
                    CloseWindow();
                    break;
                case Key.Back:
                    mediaElementMovie.Stop();
                    isPlaying = false;
                    break;
                case Key.Space:
                    PlayPause();
                    break;
                case Key.PageUp:
                    break;
                case Key.PageDown:
                    break;
                case Key.End:
                    break;
                case Key.Home:
                    break;
                case Key.Left:
                    Rewind();
                    break;
                case Key.Up:
                    if (sliderVolume.Value <= 95) sliderVolume.Value += 5;
                    break;
                case Key.Right:
                    Forward();
                    break;
                case Key.Down:
                    if (sliderVolume.Value >= 5) sliderVolume.Value -= 5;
                    break;
                case Key.Insert:
                    break;
                case Key.Delete:
                    break;
                case Key.A:
                    break;
                case Key.B:
                    break;
                case Key.C:
                    break;
                case Key.D:
                    break;
                case Key.E:
                    break;
                case Key.F:
                    break;
                case Key.G:
                    break;
                case Key.H:
                    break;
                case Key.I:
                    break;
                case Key.J:
                    break;
                case Key.K:
                    break;
                case Key.L:
                    break;
                case Key.M:
                    break;
                case Key.N:
                    break;
                case Key.O:
                    break;
                case Key.P:
                    break;
                case Key.Q:
                    break;
                case Key.R:
                    break;
                case Key.S:
                    break;
                case Key.T:
                    break;
                case Key.U:
                    break;
                case Key.V:
                    break;
                case Key.W:
                    break;
                case Key.X:
                    break;
                case Key.Y:
                    break;
                case Key.Z:
                    break;
                case Key.LWin:
                    break;
                case Key.RWin:
                    break;
                case Key.Sleep:
                    break;
                case Key.Multiply:
                    break;
                case Key.Add:
                    break;
                case Key.Subtract:
                    break;
                case Key.Decimal:
                    break;
                case Key.Divide:
                    break;
                case Key.LeftShift:
                    break;
                case Key.RightShift:
                    break;
                case Key.LeftCtrl:
                    break;
                case Key.RightCtrl:
                    break;
                case Key.LeftAlt:
                    break;
                case Key.RightAlt:
                    break;
                default:
                    break;
            }
        }

        // Close App
        public void CloseWindow()
        {
            SystemCommands.CloseWindow(this);
            ((ModernWindow)System.Windows.Application.Current.MainWindow).Show();
        }

        // Set Fullscreen
        public void SetFullScreen()
        {
            if (isFullScreen)
            {
                SystemCommands.RestoreWindow(this);
                isFullScreen = false;
            }
            else
            {
                SystemCommands.MaximizeWindow(this);
                isFullScreen = true;
            }
        }

        // Media is ended
        private void mediaElementMovie_MediaEnded(object sender, RoutedEventArgs e)
        {
            SetPcOption(selectedPcOption);
        }

        // Set PcOptions after movie
        public void SetPcOption(PCOption pCOption)
        {
            // Control PC(Shutdown, Restart...) after playing the movie. If No Option - Replay movie
            if (PCOptionsService.SetOption(pCOption)) mediaElementMovie.Stop();
            mediaElementMovie.Play();
        }

        // Double Click go to Fullscreen
        private void Window_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            SetFullScreen();
        }
    }
}

using DesktopServer.Map;
using DesktopServer.ViewModels;
using MobileMovieManager.BLL.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace DesktopServer.Views.Pages
{
    /// <summary>
    /// Interaction logic for Log.xaml
    /// </summary>
    public partial class Log : UserControl
    {
        // Timer for log --- Refresh items in every 30 second
        private DispatcherTimer dispatcherTimer;

        public Log()
        {
            // Refresh Log every 10 second
            dispatcherTimer = new DispatcherTimer() { Interval = TimeSpan.FromSeconds(10) };
            dispatcherTimer.Tick += DispatcherTimer_Tick;
            dispatcherTimer.Start();

            InitializeComponent();

            dataGridLogger.ItemsSource = LoggerMap.MapToLoggerViewModelList(LoggerService.Instance.Loggers);
        }

        // Refresh Log
        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            dataGridLogger.ItemsSource = null;
            dataGridLogger.ItemsSource = LoggerMap.MapToLoggerViewModelList(LoggerService.Instance.Loggers);

            dataGridLogger.Items.Refresh();
        }
    }
}

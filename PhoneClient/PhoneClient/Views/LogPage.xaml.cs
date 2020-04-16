using MobileMovieManager.BLL.Service;
using PhoneClient.Services.Map;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PhoneClient.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [DesignTimeVisible(false)]
    public partial class LogPage : ContentPage
    {
        public LogPage()
        {
            InitializeComponent();

            listViewLog.ItemsSource = LoggerMap.MapToLoggerViewModelList(LoggerService.Instance.Loggers);

            // Timer for log --- Refresh items in every 10 second
            Device.StartTimer(TimeSpan.FromSeconds(10), () => {
                listViewLog.ItemsSource = null;
                listViewLog.ItemsSource = LoggerMap.MapToLoggerViewModelList(LoggerService.Instance.Loggers);

                return true;
            });
        }
    }
}
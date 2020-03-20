using DesktopServer.ViewModels;
using System;
using System.Collections.Generic;
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

namespace DesktopServer.Views.Content
{
    /// <summary>
    /// Interaction logic for SettingDisplay.xaml
    /// </summary>
    public partial class SettingDisplay : UserControl
    {
        public SettingDisplay()
        {
            InitializeComponent();

            // Simple view model for display settings
            this.DataContext = new SettingDisplayViewModel();
        }
    }
}

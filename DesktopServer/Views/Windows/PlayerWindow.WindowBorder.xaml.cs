using FirstFloor.ModernUI.Windows.Controls;
using System;
using System.Windows;
using System.Windows.Input;

namespace DesktopServer.Views.Windows
{
    public partial class PlayerWindow
    {
        // Can execute
        private void CommandBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        // Minimize
        private void CommandBinding_Executed_Minimize(object sender, ExecutedRoutedEventArgs e)
        {
            SystemCommands.MinimizeWindow(this);
            isFullScreen = false;
        }

        // Maximize
        private void CommandBinding_Executed_Maximize(object sender, ExecutedRoutedEventArgs e)
        {
            SystemCommands.MaximizeWindow(this);
            isFullScreen = true;
        }

        // Restore
        private void CommandBinding_Executed_Restore(object sender, ExecutedRoutedEventArgs e)
        {
            SystemCommands.RestoreWindow(this);
            isFullScreen = false;
        }

        // Close
        private void CommandBinding_Executed_Close(object sender, ExecutedRoutedEventArgs e)
        {
            SystemCommands.CloseWindow(this);
            ((ModernWindow)Application.Current.MainWindow).Show();
        }

        private void Window_StateChanged(object sender, EventArgs e)
        {
            if (WindowState == WindowState.Maximized)
            {
                MainWindowBorder.BorderThickness = new Thickness(8);
                RestoreButton.Visibility = Visibility.Visible;
                MaximizeButton.Visibility = Visibility.Collapsed;
                ResizeGrip.Visibility = Visibility.Collapsed;
            }
            else
            {
                MainWindowBorder.BorderThickness = new Thickness(0);
                RestoreButton.Visibility = Visibility.Collapsed;
                MaximizeButton.Visibility = Visibility.Visible;
                ResizeGrip.Visibility = Visibility.Visible;
            }
        }
    }
}

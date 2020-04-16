using FirstFloor.ModernUI.Windows.Controls;
using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace DesktopServer.Views.Windows
{
    public partial class PlayerWindow
    {
        // Store mouse last position (because MouseMove event always triggered on MediaElement)
        private Point lastCursorPosition;

        // Close menu animation
        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            (FindResource("TopMenuClose") as Storyboard).Begin(gridTopMenu);
            (FindResource("TopMenuClose") as Storyboard).Begin(gridBottomMenu);
            (FindResource("TopMenuClose") as Storyboard).Begin(gridWindowsBar);
            dispatcherTimer.Stop();
        }

        // Back to the MainWindow
        private void buttonBack_Click(object sender, RoutedEventArgs e)
        {
            isBack = true;
            this.Close();
            ((ModernWindow)Application.Current.MainWindow).Show();
        }
        // Application exit
        private void ModernWindow_Closed(object sender, EventArgs e)
        {
            // Exit from application or just back to the MainWindow
            if (!isBack)
            {
                Environment.Exit(0);
            }
        }

        // Show or Hide panels and set window to fullscreen or no
        private void AppArea_MouseMove(object sender, MouseEventArgs e)
        {
            mouseMoveAnimation();
        }
        private void mouseMoveAnimation()
        {
            if (lastCursorPosition != Mouse.GetPosition(this))
            {
                this.Cursor = Cursors.Arrow;
                (FindResource("TopMenuOpen") as Storyboard).Begin(gridTopMenu);
                (FindResource("TopMenuOpen") as Storyboard).Begin(gridBottomMenu);
                (FindResource("TopMenuOpen") as Storyboard).Begin(gridWindowsBar);
                dispatcherTimer.Start();
            }

            lastCursorPosition = Mouse.GetPosition(this);
        }

        // Show or Hide panels animation completed
        private void Storyboard_Completed(object sender, EventArgs e)
        {
            this.Cursor = Cursors.None;
        }
    }
}

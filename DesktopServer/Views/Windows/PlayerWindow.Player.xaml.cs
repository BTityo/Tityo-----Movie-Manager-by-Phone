using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace DesktopServer.Views.Windows
{
    public partial class PlayerWindow
    {
        private bool isMouseWheel = false;

        // MediaElement is loaded then play movie
        private void mediaElementMovie_Loaded(object sender, RoutedEventArgs e)
        {
            mediaElementMovie.Play();
            mediaElementMovie.Volume = (double)(sliderVolume.Value / 100);
            isPlaying = true;
        }

        // Start timer if media opened
        private void mediaElementMovie_MediaOpened(object sender, RoutedEventArgs e)
        {
            totalDuration = mediaElementMovie.NaturalDuration.TimeSpan;
            sliderTimeLine.Maximum = totalDuration.TotalSeconds;

            // Create a timer that will update the counters and the time slider
            videoTimer = new DispatcherTimer();
            videoTimer.Interval = TimeSpan.FromSeconds(1);
            videoTimer.Tick += DispatcherTimerVideoTimer_Tick;
            videoTimer.Start();
        }

        // Set slider movement with 1 sec
        private void DispatcherTimerVideoTimer_Tick(object sender, EventArgs e)
        {
            // Calculate position of TimeLine slider
            if (mediaElementMovie.NaturalDuration.HasTimeSpan && mediaElementMovie.NaturalDuration.TimeSpan.TotalSeconds > 0)
            {
                if (totalDuration.TotalSeconds > 0)
                {
                    // Updating TimeLine slider
                    sliderTimeLine.Value = mediaElementMovie.Position.TotalSeconds;
                    labelRemaining.Content = TimeSpan.FromSeconds(totalDuration.TotalSeconds - sliderTimeLine.Value).ToString(@"hh\:mm\:ss");
                    sliderTimeLine.ToolTip = TimeSpan.FromSeconds(sliderTimeLine.Value).ToString(@"hh\:mm\:ss");
                }
            }
            // Stop VideoTimer if movie finished
            else
            {
                videoTimer.Stop();
            }
        }

        #region SliderTimeLine
        // Rewind/Forward movie with Mouse Wheel by SliderTimeLine
        private void sliderTimeLine_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            isMouseWheel = true;
            if (e.Delta > 0 && totalDuration.TotalSeconds > 0)
                sliderTimeLine.Value += 30;
            else if (e.Delta < 0 && totalDuration.TotalSeconds > 0)
                sliderTimeLine.Value -= 30;
        }

        // Set MediaElement position by SliderTimeLine value
        private void sliderTimeLine_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (totalDuration.TotalSeconds > 0 && sliderTimeLine.Value > 0 && isMouseWheel)
            {
                mediaElementMovie.Position = TimeSpan.FromSeconds(sliderTimeLine.Value);
            }
        }

        // Pause movie while seek
        private void sliderTimeLine_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            mediaElementMovie.Pause();
            isPlaying = false;
            isMouseWheel = false;
        }

        // Rewind/Forward movie with Mouse by SliderTimeLine
        private void sliderTimeLine_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (totalDuration.TotalSeconds > 0)
            {
                mediaElementMovie.Position = TimeSpan.FromSeconds(sliderTimeLine.Value);
            }
            mediaElementMovie.Play();
            isPlaying = true;
        }
        #endregion

        #region Volume
        // Mute/Add volume
        private void buttonVolume_Click(object sender, RoutedEventArgs e)
        {
            MuteOnOff();
        }

        // Set volume with mouse wheel
        private void sliderVolume_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            if (e.Delta > 0 && sliderVolume.Value <= 90)
                sliderVolume.Value += 10;
            else if (e.Delta < 0 && sliderVolume.Value >= 10)
                sliderVolume.Value -= 10;
        }

        // Set volume
        private void sliderVolume_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (sliderVolume.Value == 0)
            {
                mediaElementMovie.IsMuted = true;
                buttonVolume.IconData = gridBottomMenu.FindResource("MuteIconData") as Geometry;
                mediaElementMovie.Volume = 0;
                // Set sound device volume
                device.AudioEndpointVolume.MasterVolumeLevelScalar = 0;
                device.AudioEndpointVolume.Mute = true;
            }
            else
            {
                mediaElementMovie.IsMuted = false;
                mediaElementMovie.Volume = (double)(sliderVolume.Value / 100);
                if (device != null) device.AudioEndpointVolume.Mute = false;
                // Set sound device volume
                if (device != null) device.AudioEndpointVolume.MasterVolumeLevelScalar = (float)(sliderVolume.Value / 100);
                if (buttonVolume != null) buttonVolume.IconData = gridBottomMenu.FindResource("VolumeIconData") as Geometry;
            }
        }
        #endregion

        #region Control Events
        // Sound control
        public void SoundPlus()
        {
            sliderVolume.Value += 10;
        }
        public void SoundMinus()
        {
            sliderVolume.Value -= 10;
        }

        // Mute On/Off
        public void MuteOnOff()
        {
            if (mediaElementMovie.IsMuted)
            {
                mediaElementMovie.IsMuted = false;
                sliderVolume.Value = 100;
                buttonVolume.IconData = gridBottomMenu.FindResource("VolumeIconData") as Geometry;
            }
            else
            {
                mediaElementMovie.IsMuted = true;
                sliderVolume.Value = 0;
                buttonVolume.IconData = gridBottomMenu.FindResource("MuteIconData") as Geometry;
            }
        }

        // Play/Pause movie
        private void buttonPlayPause_Click(object sender, RoutedEventArgs e)
        {
            PlayPause();
        }
        public void PlayPause()
        {
            if (isPlaying)
            {
                mediaElementMovie.Pause();
                isPlaying = false;
                buttonPlayPause.IconData = gridBottomMenu.FindResource("PlayIconData") as Geometry;
                buttonPlayPause.ToolTip = "Lejátszás";
            }
            else
            {
                mediaElementMovie.Play();
                isPlaying = true;
                buttonPlayPause.IconData = gridBottomMenu.FindResource("PauseIconData") as Geometry;
                buttonPlayPause.ToolTip = "Pillanat állj";
            }
        }

        // Stop movie
        private void buttonStop_Click(object sender, RoutedEventArgs e)
        {
            Stop();
        }
        public void Stop()
        {
            mediaElementMovie.Stop();
            isPlaying = false;
            buttonPlayPause.IconData = gridBottomMenu.FindResource("PlayIconData") as Geometry;
            buttonPlayPause.ToolTip = "Lejátszás";
        }

        // Rewind movie
        private void buttonRewind_Click(object sender, RoutedEventArgs e)
        {
            Rewind();
        }
        public void Rewind()
        {
            isMouseWheel = true;
            sliderTimeLine.Value -= new TimeSpan(0, 0, 10).TotalSeconds;
        }

        // Forward movie
        private void buttonForward_Click(object sender, RoutedEventArgs e)
        {
            Forward();
        }
        public void Forward()
        {
            isMouseWheel = true;
            sliderTimeLine.Value += new TimeSpan(0, 0, 10).TotalSeconds;
        }
        #endregion
    }
}

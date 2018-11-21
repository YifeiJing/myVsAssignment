using System;
using System.Windows;
using System.Windows.Controls;

namespace Example1
{
    public class VideoHelper 
    {
        #region Private members

        static MediaElement Helper = null;

        static BasePage VideoPage = null;

        static string VidoeName { get; } = "eyes.mp4";

        static string CurrentDirectory { set; get; }

        static string LeftSee { get; } = "eyesleft.mp4";

        static string RightSee { get; } = "eyesright.mp4";

        #endregion

        #region Constructor
        /// <summary>
        /// Get the mediaelement of the window
        /// </summary>
        public VideoHelper()
        {
            VideoPage = (Application.Current.MainWindow.FindName("frame") as Frame).Content as BasePage;
            Helper = (MediaElement)(((Application.Current.MainWindow.FindName("frame") as Frame).Content as BasePage).FindName("meetingdis"));
            CurrentDirectory = AppDomain.CurrentDomain.BaseDirectory + "Videos\\";
            Helper.MediaEnded += Helper_MediaEnded;
        }


        #endregion

        #region Public helpers

        public void PlayVideoMid()
        {
            var uri = CurrentDirectory + VidoeName;
            Helper.Source = new Uri(uri, UriKind.Absolute);
            while (!Helper.IsLoaded);
            Helper.Play();
        }

        public void PlayVideoLeft()
        {
            var uri = CurrentDirectory + LeftSee;
            Helper.Source = new Uri(uri, UriKind.Absolute);
            while (!Helper.IsLoaded) ;
            Helper.Play();
        }

        public void PlayVideoRight()
        {
            var uri = CurrentDirectory + RightSee;
            Helper.Source = new Uri(uri, UriKind.Absolute);
            while (!Helper.IsLoaded) ;
            Helper.Play();
        }

        public void PlayVideo()
        {
            Helper.Play();
        }

        public void PauseVideo()
        {
            Helper.Pause();
        }

        public void StopVideo()
        {
            Helper.Stop();
        }

        #endregion

        #region private helpers

        private void Helper_MediaEnded(object sender, RoutedEventArgs e)
        {
            Helper.Position = TimeSpan.FromMilliseconds(0.01);
            Helper.Play();
        }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Example1
{
    public class AudioHelper 
    {
        #region Public members

        static MediaElement Helper = null;

        static string OnVerifying { get; } = "Verifying.mp3";

        static string CurrentDirectory { set; get; }

        #endregion

        #region Constructor
        /// <summary>
        /// Get the mediaelement of the window
        /// </summary>
        public AudioHelper()
        {
            Helper = (MediaElement)Application.Current.MainWindow.FindName("audiohelper");
            CurrentDirectory = AppDomain.CurrentDomain.BaseDirectory + "Audios\\";

        }

        #endregion

        #region Public helpers

        public static void PleaseInsertCard()
        {
            var uri = CurrentDirectory + OnVerifying;
            Helper.Source = new Uri(uri,UriKind.Absolute);
            while (!Helper.IsLoaded);
            Helper.Play();
        }
        #endregion
    }
}

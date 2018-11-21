using System;
using System.Windows;
using System.Windows.Controls;

namespace Example1
{
    public class AudioHelper 
    {
        #region Private members

        static MediaElement Helper = null;

        static string OnVerifying { get; } = "Verifying.mp3";

        static string CurrentDirectory { set; get; }

        static string mLang = "EN";

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

        public static void ChangeLanguage()
        {
            if(mLang == "EN")
            {
                mLang = "ZH";
            }
            else
            {
                mLang = "EN";
            }
        }

        public static void PleaseInsertCard()
        {
            if (mLang == "EN")
            {
                var uri = CurrentDirectory + OnVerifying;
                Helper.Source = new Uri(uri, UriKind.Absolute);
            }
            else if(mLang == "ZH")
            {
                var uri = CurrentDirectory + OnVerifying;
                Helper.Source = new Uri(uri, UriKind.Absolute);
            }
            while (!Helper.IsLoaded);
            Helper.Play();
        }
        #endregion
    }
}

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

        public static void Hello()
        {
            if (mLang == "EN")
            {
                var uri = CurrentDirectory + "hello.mp3";
                Helper.Source = new Uri(uri, UriKind.Absolute);
            }
            else if (mLang == "ZH")
            {
                var uri = CurrentDirectory + "你好.mp3";
                Helper.Source = new Uri(uri, UriKind.Absolute);
            }
            while (!Helper.IsLoaded) ;
            Helper.Play();
        }

        public static void Insert()
        {
            if (mLang == "EN")
            {
                var uri = CurrentDirectory + "insert.mp3";
                Helper.Source = new Uri(uri, UriKind.Absolute);
            }
            else if (mLang == "ZH")
            {
                var uri = CurrentDirectory + "请插入.mp3";
                Helper.Source = new Uri(uri, UriKind.Absolute);
            }
            while (!Helper.IsLoaded) ;
            Helper.Play();
        }

        public static void Out()
        {
            if (mLang == "EN")
            {
                var uri = CurrentDirectory + "out.mp3";
                Helper.Source = new Uri(uri, UriKind.Absolute);
            }
            else if (mLang == "ZH")
            {
                var uri = CurrentDirectory + "超出.mp3";
                Helper.Source = new Uri(uri, UriKind.Absolute);
            }
            while (!Helper.IsLoaded) ;
            Helper.Play();
        }

        public static void Play()
        {
            if (mLang == "EN")
            {
                var uri = CurrentDirectory + "play.mp3";
                Helper.Source = new Uri(uri, UriKind.Absolute);
            }
            else if (mLang == "ZH")
            {
                var uri = CurrentDirectory + "游戏.mp3";
                Helper.Source = new Uri(uri, UriKind.Absolute);
            }
            while (!Helper.IsLoaded) ;
            Helper.Play();
        }

        public static void Thanks()
        {
            if (mLang == "EN")
            {
                var uri = CurrentDirectory + "thank.mp3";
                Helper.Source = new Uri(uri, UriKind.Absolute);
            }
            else if (mLang == "ZH")
            {
                var uri = CurrentDirectory + "谢谢.mp3";
                Helper.Source = new Uri(uri, UriKind.Absolute);
            }
            while (!Helper.IsLoaded) ;
            Helper.Play();
        }
        #endregion
    }
}

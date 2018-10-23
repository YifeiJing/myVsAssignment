using System.Windows;
using System.Windows.Input;
using System.IO.Ports;
using System;

namespace Example1
{
    class WindowViewModel : BaseViewModel
    {
        #region Private members

        /// <summary>
        /// The object for the serial port
        /// </summary>
        private SerialPort sp = null;

        /// <summary>
        /// The variable of current language version of the display
        /// </summary>
        private string _CurrentLang = "EN";

        #endregion

        #region Public members

        /// <summary>
        /// The number of the port
        /// </summary>
        public string PortNumber { get; set; } = "COM1";

        /// <summary>
        /// The value of the baud
        /// </summary>
        public int Baud { get; set; } = 9600;

        /// <summary>
        /// The height of the header
        /// </summary>
        public int HeaderHeight { get; } = 46;
        public GridLength HeaderHeightSize { get { return new GridLength(HeaderHeight); } }

        /// <summary>
        /// The margin between the header and content of the window
        /// </summary>
        public int MarginSize { get; } = 6;
        public Thickness MarginSizeThickness { get { return new Thickness(MarginSize); } }

        /// <summary>
        /// Initialize the inner content padding
        /// </summary>
        public Thickness InnerContentPadding { get { return new Thickness(0); } }

        /// <summary>
        /// The current page
        /// </summary>
        public PageTypes CurrentPage { set; get; } = PageTypes.LoginPage;

        /// <summary>
        /// The message between mbed
        /// </summary>
        public string Message { set; get; }

        /// <summary>
        /// The message from the port
        /// </summary>
        public string ReceMessage { set; get; } = "Ready";

        /// <summary>
        /// The message to display on the textbox
        /// </summary>
        public string DisplayMessage { set; get; } = string.Empty;

        #endregion

        #region Commands

        /// <summary>
        /// The command which minimize the window
        /// </summary>
        public ICommand MinimizeCommand { set; get; }

        /// <summary>
        /// The command which maximize the window
        /// </summary>
        public ICommand MaximizeCommand { set; get; }

        /// <summary>
        /// The command which closes the window
        /// </summary>
        public ICommand CloseCommand { set; get; }

        /// <summary>
        /// The command which send data to the port
        /// </summary>
        public ICommand SendDataCommand { set; get; }

        /// <summary>
        /// The command which change the current window to Developer window
        /// </summary>
        public ICommand ChangeToDeveloperCommand { set; get; }

        /// <summary>
        /// The command which change the current language
        /// </summary>
        public ICommand ChangeLanguageCommand { set; get; }

        #endregion
        #region Constructor

        public WindowViewModel()
        {
            sp = new SerialPort(PortNumber,Baud);
            MinimizeCommand = new RelayCommand(() => Application.Current.MainWindow.WindowState = WindowState.Minimized);
            MaximizeCommand = new RelayCommand(() => Application.Current.MainWindow.WindowState ^= WindowState.Maximized);
            CloseCommand = new RelayCommand(() => Application.Current.MainWindow.Close());
            ChangeToDeveloperCommand = new RelayCommand(() =>
            {
                if (this.CurrentPage == PageTypes.DeveloperPage)
                {
                    return;
                }
                else
                {

                    this.CurrentPage = PageTypes.DeveloperPage;
                }

            });
            sp.DataReceived += (s, e) =>
            {
                ReceMessage = ReceiveData((s as SerialPort).ReadBufferSize);
                DisplayMessage += ReceMessage;
            };
            ChangeLanguageCommand = new RelayCommand(ChangeLanguage);
        }
        #endregion

        #region Private helpers

        /// <summary>
        /// Send data to the port
        /// </summary>
        private void SendData()
        {
            if(sp.IsOpen)
            {
                sp.Write(Message);
            }
            else
            {
                try
                {
                    sp.Open();
                    sp.Write(Message);
                }
                catch { }
            }
        }

        /// <summary>
        /// Convert the receiving bytes to a string
        /// </summary>
        /// <param name="bufsize"> The size of the read buffer</param>
        /// <returns></returns>
        private string ReceiveData(int bufsize)
        {
            char[] buffer = new char[bufsize];
            sp.Read(buffer, 0, bufsize);

            return new string(buffer);
        }

        /// <summary>
        /// Change the current language
        /// </summary>
        private void ChangeLanguage()
        {
            ResourceDictionary dict = new ResourceDictionary();

            if (_CurrentLang == "ZH")
            {
                dict.Source = new Uri($"pack://application:,,,/Language/EN.xaml");

                _CurrentLang = "EN";
            }
            else
            {
                dict.Source = new Uri($"pack://application:,,,/Language/CH.xaml");

                _CurrentLang = "ZH";
            }

            Application.Current.Resources.MergedDictionaries[0] = dict;
        }
        #endregion
    }
}

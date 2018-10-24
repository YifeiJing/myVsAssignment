using System.Windows;
using System.Windows.Input;
using System.IO.Ports;
using System;
using System.Threading.Tasks;

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

        /// <summary>
        /// Indicates whether the ticker is count in increment mode or decrement mode
        /// </summary>
        public bool IsDecrement { get; set; } = false;

        /// <summary>
        /// Indicates whether the game is on
        /// </summary>
        public bool IsPlaying { get; set; } = false;

        /// <summary>
        /// The varible to count every second since the game begins
        /// </summary>
        public int Ticker { get; set; } = 0;

        /// <summary>
        /// Indicates whether the port is open
        /// </summary>
        public bool IsPortOpen { get; set; } = false;

        /// <summary>
        /// The method to change the value of the ticker every second.
        /// </summary>
        public void TimeTicker()
        {
            if(!IsDecrement)
            {
                Task.Run(async () =>
                {
                    do
                    {
                        await Task.Delay(200);
                        ++Ticker;
                    } while (IsPlaying);
                });
            }
            else
            {
                Task.Run(async () =>
                {
                    do
                    {
                        await Task.Delay(200);
                        --Ticker;
                        if (Ticker == 0)
                            IsPlaying = false;
                    } while (IsPlaying);
                });
            }
        }

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
        /// The command which change the current window to Login Window
        /// </summary>
        public ICommand ChangeToLoginCommand { set; get; }

        /// <summary>
        /// The command which change the current window to Login Window
        /// </summary>
        public ICommand ChangeToGameCommand { set; get; }

        /// <summary>
        /// The command which change the current language
        /// </summary>
        public ICommand ChangeLanguageCommand { set; get; }

        /// <summary>
        /// The command to start the game
        /// </summary>
        public ICommand GameStartCommand { set; get; }

        /// <summary>
        /// The command to start the game
        /// </summary>
        public ICommand GameEndCommand { set; get; }

        /// <summary>
        /// The command to open the port
        /// </summary>
        public ICommand PortOpenCommand { set; get; }

        /// <summary>
        /// The command to close the port
        /// </summary>
        public ICommand PortCloseCommand { set; get; }

        /// <summary>
        /// The command to clear the content text in the developer window
        /// </summary>
        public ICommand DisplayMessageClearCommand { get; set; }

        #endregion
        #region Constructor

        public WindowViewModel()
        {
            sp = new SerialPort(PortNumber,Baud);
            MinimizeCommand = new RelayCommand(() => Application.Current.MainWindow.WindowState = WindowState.Minimized);
            MaximizeCommand = new RelayCommand(() => Application.Current.MainWindow.WindowState ^= WindowState.Maximized);
            CloseCommand = new RelayCommand(() => Application.Current.MainWindow.Close());

            #region Page Load command implement

            ///
            ///Change the CurrentPage to what the user want
            ///
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

            ChangeToLoginCommand = new RelayCommand(() =>
            {
                if (this.CurrentPage == PageTypes.LoginPage)
                {
                    return;
                }
                else
                {

                    this.CurrentPage = PageTypes.LoginPage;
                }

            });

            ChangeToGameCommand = new RelayCommand( () =>
            {
                if (this.CurrentPage == PageTypes.GamePage)
                {
                    return;
                }
                else
                {
                    this.CurrentPage = PageTypes.GamePage;
                }

            });

            #endregion

            #region Serial port Datareceived Event handler
            ///
            //Deal with the coming data and send it to <para ReceMessage>
            //<para DisplayMessage> show the accumulate message
            ///
            sp.DataReceived += (s, e) =>
            {
                ReceMessage = ReceiveData((s as SerialPort).BytesToRead);
                DisplayMessage += ReceMessage;
            };

            #endregion

            //Change current language
            ChangeLanguageCommand = new RelayCommand(ChangeLanguage);

            #region Game panel commands
            GameStartCommand = new RelayCommand(GameStart);
            GameEndCommand = new RelayCommand(GameEnd);

            #endregion

            #region Developer panel commands

            PortOpenCommand = new RelayCommand(() =>
            {
                try
                { sp.Open();
                    IsPortOpen = true;
                }
                catch
                { }
                });
            PortCloseCommand = new RelayCommand(() => {
                try
                { sp.Close();
                    IsPortOpen = false;
                }
                catch
                { }
            });
            DisplayMessageClearCommand = new RelayCommand(() => DisplayMessage = string.Empty);
            SendDataCommand = new RelayCommand(() => SendData());

            #endregion
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

        /// <summary>
        /// The the user press the start game button
        /// The command runs this method
        /// </summary>
        private void GameStart()
        {
            Ticker = 0;
            IsPlaying = true;
            TimeTicker();
        }

        /// <summary>
        /// The game end command method
        /// </summary>
        private void GameEnd()
        {
            IsPlaying = false;
        }

        #endregion
    }
}

using System.Windows;
using System.Windows.Input;
using System.IO.Ports;
using System;
using System.Threading.Tasks;
using System.Windows.Controls;

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

        private AudioHelper mAudioHelper = new AudioHelper();

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
        /// If the player is ot of the sensor range
        /// </summary>
        public bool IsOutOfRange { get; set; } = false;

        /// <summary>
        /// Indicates whether the game is end
        /// </summary>
        public bool IsGameEnd { get; set; } = false;

        /// <summary>
        /// If the machine is verifying the card
        /// </summary>
        public bool IsVerifying { get; set; } = false;

        /// <summary>
        /// holds the value of the ticker in pause screen
        /// </summary>
        public int OutOfRangeTick { get; set; } = 50;

        /// <summary>
        /// The score of the current player
        /// </summary>
        public int PlayerScore { get; set; } = 0;

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

        /// <summary>
        /// The message of var1 in debug panel
        /// </summary>
        public string CommandVar1 { set; get; } = string.Empty;

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

        public ICommand PlayAudioHelperCommand { get; set; }

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
            PlayAudioHelperCommand = new RelayCommand(() =>
            {
                AudioHelper.PleaseInsertCard();
            });

            ChangeToDeveloperCommand = new RelayCommand(async () =>
            {
                if (this.CurrentPage == PageTypes.DeveloperPage)
                {
                    return;
                }
                else
                {
                    var mFrame = Application.Current.MainWindow.FindName("frame");
                    BasePage page = (mFrame as Frame).Content as BasePage;
                    await page.AnimatOut();
                    this.CurrentPage = PageTypes.DeveloperPage;
                }

            });

            ChangeToLoginCommand = new RelayCommand(async() =>
            {
                if (this.CurrentPage == PageTypes.LoginPage)
                {
                    return;
                }
                else
                {
                    var mFrame = Application.Current.MainWindow.FindName("frame");
                    BasePage page = (mFrame as Frame).Content as BasePage;
                    await page.AnimatOut();
                    this.CurrentPage = PageTypes.LoginPage;
                }

            });

            ChangeToGameCommand = new RelayCommand( async() =>
            {
                if (this.CurrentPage == PageTypes.GamePage)
                {
                    return;
                }
                else
                {
                    var mFrame = Application.Current.MainWindow.FindName("frame");
                    BasePage page = (mFrame as Frame).Content as BasePage;
                    await page.AnimatOut();
                    this.CurrentPage = PageTypes.GamePage;
                }

            });

            #endregion

            #region Serial port Datareceived Event handler
            ///
            //Deal with the coming data and send it to <para ReceMessage>
            //<para DisplayMessage> show the accumulate message
            //Take charge of the current message from the port
            //Go to different pages
            ///
            sp.DataReceived += (s, e) =>
            {
                ReceMessage = ReceiveData((s as SerialPort).BytesToRead);
                DisplayMessage += ReceMessage;

                if(ReceMessage == LoginCommands.Login)
                {
                    if (this.CurrentPage == PageTypes.GamePage)
                    {
                        return;
                    }
                    else
                    {
                        Application.Current.Dispatcher.Invoke(async () =>
                        {
                            var mFrame = Application.Current.MainWindow.FindName("frame");
                            BasePage page = (mFrame as Frame).Content as BasePage;
                            await page.AnimatOut();
                            this.CurrentPage = PageTypes.GamePage;
                        }).Wait();
                    }
                }
                else if(ReceMessage == LoginCommands.IDVerify)
                {
                    IsVerifying = true;
                }
                else if(ReceMessage == LoginCommands.Root)
                {
                    if (this.CurrentPage == PageTypes.DeveloperPage)
                    {
                        return;
                    }
                    else
                    {
                        Application.Current.Dispatcher.Invoke(async () =>
                        {
                            var mFrame = Application.Current.MainWindow.FindName("frame");
                            BasePage page = (mFrame as Frame).Content as BasePage;
                            await page.AnimatOut();
                            this.CurrentPage = PageTypes.DeveloperPage;
                        }).Wait();
                    }
                }
                else if(ReceMessage == GameComands.Back || ReceMessage == DebugCommands.Back)
                {
                    if (this.CurrentPage == PageTypes.LoginPage)
                    {
                        return;
                    }
                    else
                    {
                        Application.Current.Dispatcher.Invoke(async () =>
                        {
                            var mFrame = Application.Current.MainWindow.FindName("frame");
                            BasePage page = (mFrame as Frame).Content as BasePage;
                            await page.AnimatOut();
                            this.CurrentPage = PageTypes.LoginPage;
                        }).Wait();
                    }
                }
                else if(ReceMessage == GameComands.GameStart)
                {
                    GameStart();
                }
                else if(ReceMessage == GameComands.GameEnd)
                {
                    GameEnd();
                }
                else if(ReceMessage == GameComands.GamePause)
                {
                    GamePause();
                }
                else if(ReceMessage == GameComands.OutRange)
                {
                    OutOfRange();
                }
                else if(ReceMessage == GameComands.GetBack)
                {
                    GetBack();
                }
                else
                {
                    return;
                }
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
                if (sp.IsOpen)
                { }
                else
                {
                    try
                    {
                        sp.Open();
                        IsPortOpen = true;
                    }
                    catch { }
                }
                if(CommandVar1 == "S")
                {
                    MoveServo();
                }
                else if(CommandVar1 == "R")
                {
                    ReadRGBSensor();
                }
                else if(CommandVar1 == "D")
                {
                    ReadDistanceSensor();
                }
                else
                {
                    return;
                }
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
            IsGameEnd = true;
        }

        /// <summary>
        /// The game pause method
        /// </summary>
        private void GamePause()
        {
            IsPlaying = false;
        }

        /// <summary>
        /// The game resume method
        /// </summary>
        private void GameResume()
        {
            IsPlaying = true;
            TimeTicker();
        }

        /// <summary>
        /// The method to run when the player out of the sensor range
        /// </summary>
        private void OutOfRange()
        {
            GamePause();
            IsOutOfRange = true;
        }

        /// <summary>
        /// When the player get back from outer place
        /// </summary>
        private void GetBack()
        {
            IsOutOfRange = false;
            GameResume();
            OutOfRangeTick = 50;
        }

        /// <summary>
        /// Move servo in the debug mode
        /// </summary>
        private void MoveServo()
        {
            if (sp.IsOpen)
            {
                sp.Write(DebugCommands.MoveServo);
            }
            else
                return;
        }

        /// <summary>
        /// Read distance sensor
        /// </summary>
        private void ReadDistanceSensor()
        {
            if (sp.IsOpen)
            {
                sp.Write(DebugCommands.ReadDistanceSensor);
            }
            else
                return;
        }

        /// <summary>
        /// Read RGB sensor
        /// </summary>
        private void ReadRGBSensor()
        {
            if (sp.IsOpen)
            {
                sp.Write(DebugCommands.ReadRGBSensor);
            }
            else
                return;
        }

        /// <summary>
        /// Begin debug mode
        /// </summary>
        private void BeginDebug()
        {
            if (sp.IsOpen)
            {
                sp.Write(DebugCommands.Debug);
            }
            else
                return;
        }

        /// <summary>
        /// The ticker begin when the player get out of the sensor range
        /// </summary>
        private void OutOfRangeTicker()
        {
            Task.Run(async () =>
            {
                while (IsOutOfRange)
                {
                    await Task.Delay(200);
                    OutOfRangeTick--;
                    if (OutOfRangeTick < 0)
                    {
                        IsOutOfRange = false;
                        IsGameEnd = true;
                        break;
                    }
                }
            });
        }
        #endregion
    }
}

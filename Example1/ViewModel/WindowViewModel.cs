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

        /// <summary>
        /// Initialize the static audio helper object
        /// </summary>
        private AudioHelper mAudioHelper = new AudioHelper();

        private VideoHelper mVideoHelper = null;

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
        public PageTypes CurrentPage { set; get; } = PageTypes.FirstPage;

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

        /// <summary>
        /// The message of var2 in debug panel
        /// </summary>
        public string CommandVar2 { set; get; } = string.Empty;

        /// <summary>
        /// The message of var3 in debug panel
        /// </summary>
        public string CommandVar3 { set; get; } = string.Empty;

        /// <summary>
        /// Holds the state of the side menu, whether it is visible
        /// </summary>
        public bool SideMenuVisible { get; set; } = false;

        /// <summary>
        /// The states of the video in the first page
        /// </summary>
        public bool IsVideoPlaying { get; set; } = false;

        /// <summary>
        /// The number the map to display
        /// </summary>
        public int MapNumber { get; set; } = 0;

        /// <summary>
        /// The ID of the user
        /// </summary>
        public int UserID { get; set; } = 0;

        /// <summary>
        /// Game end status
        /// </summary>
        public bool IsGameSucceed { get; set; } = false;
        public bool IsGameFailed { get; set; } = false;

        /// <summary>
        /// The status of the start button
        /// </summary>
        public bool IsStarted { get; set; } = false;

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
        /// The command which change the current window to First Window
        /// </summary>
        public ICommand ChangeToFirstCommand { set; get; }

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

        /// <summary>
        /// The command to player the audio helper
        /// </summary>
        public ICommand PlayAudioHelperCommand { get; set; }

        /// <summary>
        /// The command on the icon, which change the visibility of the side menu
        /// </summary>
        public ICommand SideMenuCommand { get; set; }

        /// <summary>
        /// Display helper message in the debug mode
        /// </summary>
        public ICommand HelperCommand { get; set; }

        /// <summary>
        /// After the user insert the card, touch the button to verify the card
        /// </summary>
        public ICommand VerifyCommand { get; set; }

        /// <summary>
        /// When the user finishes fetching the gift
        /// </summary>
        public ICommand FetchedCommand { get; set; }

        #endregion

        #region Constructor

        public WindowViewModel()
        {
            sp = new SerialPort(PortNumber,Baud);
            var frame = Application.Current.MainWindow.FindName("frame") as Frame;
            frame.Navigated += (s, ee) => ((Frame)s).NavigationService.RemoveBackEntry();

            SideMenuCommand = new RelayCommand(() =>
            {
                if (IoC.Get<ApplicationViewModel>().SideMenuVisible)
                {
                    IoC.Get<ApplicationViewModel>().SideMenuVisible = false;
                }
                else
                {
                    IoC.Get<ApplicationViewModel>().SideMenuVisible = true;
                    var SideMenu = Application.Current.MainWindow.FindName("sidemenu") as SideMenuControl;
                    Application.Current.Dispatcher.Invoke(() => SideMenu.DataContext = new ChatListViewModel());
                }
                });

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
                    sp.Write("begin");
                    var mFrame = Application.Current.MainWindow.FindName("frame");
                    BasePage page = (mFrame as Frame).Content as BasePage;
                    await page.AnimatOut();
                    this.CurrentPage = PageTypes.LoginPage;
                    AudioHelper.Insert();
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
            ChangeToFirstCommand = new RelayCommand(async () =>
            {
                if (this.CurrentPage == PageTypes.FirstPage)
                {
                    return;
                }
                else
                {
                    var mFrame = Application.Current.MainWindow.FindName("frame");
                    BasePage page = (mFrame as Frame).Content as BasePage;
                    await page.AnimatOut();
                    this.CurrentPage = PageTypes.FirstPage;
                }
                //mVideoHelper = new VideoHelper();
            });

            #endregion

            #region Serial port Datareceived Event handler
            ///
            //Deal with the coming data and send it to <@para ReceMessage>
            //<@para DisplayMessage> show the accumulate message
            //Take charge of the current message from the port
            //Go to different pages
            ///
            sp.DataReceived += (s, e) =>
            {
                ReceMessage = ReceiveData((s as SerialPort).BytesToRead);
                DisplayMessage += ReceMessage;

                if (ReceMessage == LoginCommands.Login)
                {
                    IsVerifying = false;

                    if (this.CurrentPage == PageTypes.GamePage)
                    {
                        return;
                    }
                    else
                    {
                        IsGameEnd = false;
                        IsStarted = false;
                        Ticker = 0;
                        Application.Current.Dispatcher.Invoke(async () =>
                        {
                            var mFrame = Application.Current.MainWindow.FindName("frame");
                            BasePage page = (mFrame as Frame).Content as BasePage;
                            await page.AnimatOut();
                            this.CurrentPage = PageTypes.GamePage;
                            AudioHelper.Play();
                        }).Wait();
                    }
                }
                else if (ReceMessage == LoginCommands.IDVerify)
                {
                    IsVerifying = true;
                }
                else if (ReceMessage == LoginCommands.Root)
                {
                    IsVerifying = false;

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
                        DisplayMessage = String.Empty;
                    }
                }
                else if (ReceMessage == LoginCommands.ToFirstPage)
                {
                    IsVerifying = false;

                    if (this.CurrentPage == PageTypes.FirstPage)
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
                            this.CurrentPage = PageTypes.FirstPage;
                        }).Wait();
                        Task.Run(async () =>
                        {
                            await Task.Delay(1000);
                            //Application.Current.Dispatcher.Invoke(() => AudioHelper.Hello());
                            Application.Current.Dispatcher.Invoke(() => mVideoHelper = new VideoHelper());
                            
                        });
                    }
                    IsGameSucceed = false;
                    IsGameFailed = false;
                    
                }
                else if (ReceMessage == LoginCommands.PlayMidAnime)
                {
                    if (CurrentPage == PageTypes.FirstPage)
                    {
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            MidEyePlay();
                           
                            AudioHelper.Hello();
                            });
                    }
                }
                else if (ReceMessage == LoginCommands.PlayLeftAnime)
                {
                    if (CurrentPage == PageTypes.FirstPage)
                    {
                        Application.Current.Dispatcher.Invoke(() =>
                        LeftEyePlay());
                    }
                }
                else if (ReceMessage == LoginCommands.PlayRightAnime)
                {
                    if (CurrentPage == PageTypes.FirstPage)
                    {
                        Application.Current.Dispatcher.Invoke(() =>
                        RightEyePlay());
                    }
                }
                else if (ReceMessage == LoginCommands.PlayAnime)
                {
                    if (CurrentPage == PageTypes.FirstPage)
                    {
                        Application.Current.Dispatcher.Invoke(() =>
                        PlayVideo());
                    }
                }
                else if (ReceMessage == LoginCommands.PauseAnime)
                {
                    if (CurrentPage == PageTypes.FirstPage)
                    {
                        Application.Current.Dispatcher.Invoke(() =>
                        PauseVideo());
                    }
                }
                else if (ReceMessage == LoginCommands.StopAnime)
                {
                    if (CurrentPage == PageTypes.FirstPage)
                    {
                        Application.Current.Dispatcher.Invoke(() =>
                        StopVideo());
                    }
                }
                else if (ReceMessage == GameComands.Back || ReceMessage == DebugCommands.Back)
                {
                    IsVerifying = false;

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
                else if (ReceMessage == GameComands.GameStart)
                {
                    GameStart();
                }
                else if (ReceMessage == GameComands.GameEnd)
                {
                    GameEnd();
                }
                else if (ReceMessage == GameComands.GamePause)
                {
                    GamePause();
                }
                else if (ReceMessage == GameComands.OutRange)
                {
                    OutOfRange();
                }
                else if (ReceMessage == GameComands.GetBack)
                {
                    GetBack();
                }
                else if (ReceMessage == GameComands.GameSucceed)
                {
                    IsGameSucceed = true;
                }
                else if (ReceMessage == GameComands.GameFailed)
                {
                    IsGameFailed = true;
                }
                else if (ReceMessage == CommunicationCommands.Map00)
                {
                    MapNumber = 0;
                }
                else if (ReceMessage == CommunicationCommands.Map01)
                {
                    MapNumber = 0;
                }
                else if (ReceMessage == CommunicationCommands.Map02)
                {
                    MapNumber = 0;
                }
                else if (ReceMessage == CommunicationCommands.Map03)
                {
                    MapNumber = 0;
                }
                else if (ReceMessage == CommunicationCommands.Map04)
                {
                    MapNumber = 0;
                }
                else if (ReceMessage == CommunicationCommands.UserID01)
                {
                    UserID = 1;
                }
                else if (ReceMessage == CommunicationCommands.UserID02)
                {
                    UserID = 2;
                }
                else if (ReceMessage == CommunicationCommands.UserID03)
                {
                    UserID = 3;
                }
                else if (ReceMessage == CommunicationCommands.UserID04)
                {
                    UserID = 4;
                }
                else if (ReceMessage == CommunicationCommands.UserID05)
                {
                    UserID = 5;
                }
                else if (ReceMessage == CommunicationCommands.UserID06)
                {
                    UserID = 6;
                }
                else if (ReceMessage == CommunicationCommands.UserID07)
                {
                    UserID = 7;
                }
                else if (ReceMessage == CommunicationCommands.UserID08)
                {
                    UserID = 8;
                }
                else if (ReceMessage == CommunicationCommands.UserID09)
                {
                    UserID = 9;
                }
                else if (ReceMessage == CommunicationCommands.UserID10)
                {
                    UserID = 10;
                }
                else if (ReceMessage == CommunicationCommands.UserID11)
                {
                    UserID = 11;
                }
                else if (ReceMessage == CommunicationCommands.UserID12)
                {
                    UserID = 12;
                }
                else if (ReceMessage == CommunicationCommands.UserID13)
                {
                    UserID = 13;
                }
                else if (ReceMessage == CommunicationCommands.UserID14)
                {
                    UserID = 14;
                }
                else if (ReceMessage == CommunicationCommands.UserID15)
                {
                    UserID = 15;
                }
                else if (ReceMessage == GameComands.GameFailed)
                {
                    IsGameFailed = true;
                    IsPlaying = false;
                }
                else if (ReceMessage == GameComands.GameSucceed)
                {
                    IsGameSucceed = true;
                    IsPlaying = false;
                    Application.Current.Dispatcher.Invoke(() => AudioHelper.Thanks());
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
            HelperCommand = new RelayCommand(() =>
            {
                if (sp.IsOpen)
                {
                    sp.Write("?");
                }
                else
                {
                    try
                    {
                        sp.Open();
                    }
                    catch { }
                    sp.Write("?");
                }
            });

            VerifyCommand = new RelayCommand(() => sp.Write(LoginCommands.IDVerify));
            FetchedCommand = new RelayCommand(() => sp.Write(GameComands.GiftFetched));


        #endregion
            sp.Open();
            IsPortOpen = true;

        Application.Current.MainWindow.KeyDown += MainWindow_KeyDown;
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Space)
            {
                mVideoHelper = new VideoHelper();
                mVideoHelper.PlayVideoMid();
                IsVideoPlaying = true;
            }
            if(e.Key == Key.Q)
            {
                if(IsVideoPlaying)
                {
                    mVideoHelper.PlayVideoLeft();
                    IsVideoPlaying = false;
                }
                else
                {
                    mVideoHelper.PlayVideoRight();
                    IsVideoPlaying = true;
                }
            }
            if (e.Key == Key.W)
            {
                if (IsVideoPlaying)
                {
                    mVideoHelper.PauseVideo();
                    IsVideoPlaying = false;
                }
                else
                {
                    mVideoHelper.PlayVideo();
                    IsVideoPlaying = true;
                }
            }
            if (e.Key == Key.E)
            {
                CurrentPage = PageTypes.LoginPage;
            }
            if (e.Key == Key.R)
            {
                mVideoHelper = new VideoHelper();
                sp.Write("a");
                Application.Current.MainWindow.KeyDown -= MainWindow_KeyDown;
            }
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
            AudioHelper.ChangeLanguage();
        }

        #region GamePage functions

        /// <summary>
        /// The the user press the start game button
        /// The command runs this method
        /// </summary>
        private void GameStart()
        {
            
            sp.Write("a");
            Ticker = 0;
            IsPlaying = true;
            IsOutOfRange = false;
            IsStarted = true;
            TimeTicker();
            
        }

        /// <summary>
        /// The game end command method
        /// </summary>
        private void GameEnd()
        {
            IsPlaying = false;
            IsGameEnd = true;
            //AudioHelper.Thanks();
            var connection = new DataBaseControl();
            connection.access.Change_sheet1_time(Ticker/5, UserID.ToString());
            connection.access.rank();
            
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
            if (IsPlaying)
                return;

            IsPlaying = true;
            TimeTicker();
        }

        /// <summary>
        /// The method to run when the player out of the sensor range
        /// </summary>
        private void OutOfRange()
        {
            if (IsOutOfRange)
                return;

            GamePause();
            OutOfRangeTicker();
            IsOutOfRange = true;
            Application.Current.Dispatcher.Invoke(() => AudioHelper.Out());
        }

        /// <summary>
        /// When the player get back from outer place
        /// </summary>
        private void GetBack()
        {
            if (!IsOutOfRange)
                return;
            IsOutOfRange = false;
            GameResume();
            //TimeTicker();
            OutOfRangeTick = 50;
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
                        IsPlaying = false;
                        IsGameEnd = true;
                        sp.Write("end__");
                        OutOfRangeTick = 50;
                        break;
                    }
                }
            });
        }

        #endregion

        #region OperationPage function

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

        #endregion

        
        #region FirstPgae commands
        /// <summary>
        /// The anime in the first page, three animes are used
        /// </summary>
        private void MidEyePlay()
        {
            mVideoHelper.PlayVideoMid();
            IsVideoPlaying = true;
        }

        private void LeftEyePlay()
        {
            mVideoHelper.PlayVideoLeft();
            IsVideoPlaying = true;
        }

        private void RightEyePlay()
        {
            mVideoHelper.PlayVideoRight();
            IsVideoPlaying = true;
        }

        private void PlayVideo()
        {
            mVideoHelper.PlayVideo();
            IsVideoPlaying = true;
        }

        private void PauseVideo()
        {
            mVideoHelper.PauseVideo();
            IsVideoPlaying = false;
        }

        private void StopVideo()
        {
            mVideoHelper.StopVideo();
            IsVideoPlaying = false;
        }
        #endregion

        #endregion
    }
}

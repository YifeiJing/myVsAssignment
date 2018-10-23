using System.Windows;
using System.Windows.Input;

namespace Example1
{
    class WindowViewModel : BaseViewModel
    {
        #region Private members


        #endregion

        #region Public members

        /// <summary>
        /// The height of the header
        /// </summary>
        public int HeaderHeight { get; } = 46;
        public GridLength HeaderHeightSize { get { return new GridLength(HeaderHeight); } }

        /// <summary>
        /// The margin between the header and content of the window
        /// </summary>
        public int MarginSize { get; } = 2;
        public Thickness MarginSizeThickness { get { return new Thickness(MarginSize); } }

        /// <summary>
        /// Initialize the inner content padding
        /// </summary>
        public Thickness InnerContentPadding { get { return new Thickness(0); } }

        /// <summary>
        /// The current page
        /// </summary>
        public PageTypes CurrentPage { set; get; } = PageTypes.LoginPage;

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

        #endregion
        #region Constructor

        public WindowViewModel()
        {
            MinimizeCommand = new RelayCommand(() => Application.Current.MainWindow.WindowState = WindowState.Minimized);
            MaximizeCommand = new RelayCommand(() => Application.Current.MainWindow.WindowState ^= WindowState.Maximized);
            CloseCommand = new RelayCommand(() => Application.Current.MainWindow.Close());
        }
        #endregion
    }
}

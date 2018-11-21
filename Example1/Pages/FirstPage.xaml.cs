using System.Windows;
using System.Windows.Input;

namespace Example1
{
    /// <summary>
    /// Interaction logic for FirstPage.xaml
    /// </summary>
    public partial class FirstPage : BasePage
    {
        private VideoHelper mhelper = null;

        public FirstPage()
        {

            InitializeComponent();
            this.DataContext = Application.Current.MainWindow.DataContext;

        }

        private void BasePage_Loaded(object sender, RoutedEventArgs e)
        {
            mhelper = new VideoHelper();
        }

    }
}

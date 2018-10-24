using System.Windows;
using System.Windows.Controls;

namespace Example1
{
    /// <summary>
    /// Interaction logic for GamePage.xaml
    /// </summary>
    public partial class GamePage : BasePage
    {
        public GamePage()
        {
            InitializeComponent();
            this.DataContext = Application.Current.MainWindow.DataContext;
        }
    }
}

using System.Windows;
using System.Windows.Controls;

namespace Example1
{
    /// <summary>
    /// Interaction logic for DeveloperPage.xaml
    /// </summary>
    public partial class DeveloperPage : BasePage
    {
        public DeveloperPage()
        {
            InitializeComponent();
            this.DataContext = Application.Current.MainWindow.DataContext;
        }
    }
}

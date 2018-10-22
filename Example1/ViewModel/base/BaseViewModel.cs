using System.ComponentModel;

namespace Example1
{
    class BaseViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Basic ViewModel fires property changed events as needed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged = (Sender, s) => { };

        public void OnPropertyChanged(string name)
        {
            PropertyChanged(this, new PropertyChangedEventArgs(name));
        }
    }
}

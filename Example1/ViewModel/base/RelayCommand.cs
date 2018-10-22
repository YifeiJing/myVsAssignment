using System;
using System.Windows.Input;

namespace Example1
{
    class RelayCommand : ICommand
    {
        private Action mAction;

        public event EventHandler CanExecuteChanged = (sender, s) => { };

        public RelayCommand(Action action)
        {
            this.mAction = action;
        }
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            mAction();
        }
    }
}

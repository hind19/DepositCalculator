using System.Windows.Input;

namespace WPFClient.Helpers
{
    public class RelayCommand : ICommand
    {
        private readonly Predicate<object> _canExecuteDelegate;
        private readonly Action<object> _executeDelegate;

        public RelayCommand(Action<object> action, Predicate<object> canExecute = null)
        {
            _executeDelegate = action;
            _canExecuteDelegate = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        public bool CanExecute(object parameter)
        {
            if (_canExecuteDelegate is not null)
            {
                return _canExecuteDelegate(parameter);
            }
            return true;
        }

        public void Execute(object parameter)
        {
            if (_executeDelegate is not null)
            {
                _executeDelegate(parameter);
            }
        }
    }
}

using System;
using System.Windows.Input;

namespace StripeCreator.WPF
{
    public class RelayCommand : ICommand
    {
        #region Private fields

        private Action<object?> _executeAction;
        private Func<object?, bool>? _canExecute;

        #endregion

        #region Constructors

        public RelayCommand(Action<object?> executeAction, Func<object?, bool>? canExecute = null)
        {
            _executeAction = executeAction;
            _canExecute = canExecute;
        }

        #endregion

        #region Public events

        public event EventHandler? CanExecuteChanged;

        #endregion

        #region Command methods

        public bool CanExecute(object? parameter)
        {
            return _canExecute is null ? true : _canExecute(parameter);
        }

        public void Execute(object? parameter)
        {
            _executeAction(parameter);
        }

        #endregion
    }
}
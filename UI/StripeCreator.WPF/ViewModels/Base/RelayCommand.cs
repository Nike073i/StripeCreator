using System;
using System.Windows.Input;

namespace StripeCreator.WPF
{
    /// <summary>
    /// Команда с ретранслированным действием
    /// </summary>
    public class RelayCommand : ICommand
    {
        #region Private fields

        /// <summary>
        /// Ретранслированное действие
        /// </summary>
        private readonly Action<object?> _executeAction;

        /// <summary>
        /// Делегат проверки готовности выполнения команды
        /// </summary>
        private readonly Func<object?, bool>? _canExecute;

        #endregion

        #region Constructors

        /// <summary>
        /// Конструктор с полной инициализацией
        /// </summary>
        /// <param name="executeAction">Действие для выполнения</param>
        /// <param name="canExecute">Проверка готовности команды</param>
        public RelayCommand(Action<object?> executeAction, Func<object?, bool>? canExecute = null)
        {
            _executeAction = executeAction;
            _canExecute = canExecute;
        }

        #endregion

        #region Interface implementations

        public event EventHandler? CanExecuteChanged = (sender, e) => { };

        public bool CanExecute(object? parameter) => _canExecute is null || _canExecute(parameter);

        public void Execute(object? parameter) => _executeAction(parameter);

        #endregion
    }
}
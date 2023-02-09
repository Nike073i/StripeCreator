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

        #endregion

        #region Public properties

        /// <summary>
        /// Предикат проверки готовности выполнения команды
        /// </summary>
        public Predicate<object?>? CanExecutePredicate { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Конструктор с полной инициализацией
        /// </summary>
        /// <param name="executeAction">Действие для выполнения</param>
        /// <param name="canExecute">Проверка готовности команды</param>
        public RelayCommand(Action<object?> executeAction)
        {
            _executeAction = executeAction;
        }

        #endregion

        #region Interface implementations

        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object? parameter) => CanExecutePredicate is null || CanExecutePredicate(parameter);

        public void Execute(object? parameter)
        {
            _executeAction(parameter);
        }

        #endregion
    }
}
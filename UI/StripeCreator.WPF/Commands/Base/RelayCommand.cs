using System;

namespace StripeCreator.WPF
{

    /// <summary>
    /// Команда с ретранслированным действием
    /// </summary>
    public class RelayCommand : BaseCommand
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
        public RelayCommand(Action<object?> executeAction)
        {
            _executeAction = executeAction;
        }

        #endregion

        #region Override methods

        public override bool CanExecute(object? parameter) => CanExecutePredicate is null || CanExecutePredicate(parameter);

        public override void Execute(object? parameter)
        {
            _executeAction(parameter);
        }

        #endregion
    }
}
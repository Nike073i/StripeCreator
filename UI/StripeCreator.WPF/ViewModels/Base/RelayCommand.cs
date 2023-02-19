using System;
using System.Windows.Input;

namespace StripeCreator.WPF
{
    /// <summary>
    /// ������� � ����������������� ���������
    /// </summary>
    public class RelayCommand : ICommand
    {
        #region Private fields

        /// <summary>
        /// ����������������� ��������
        /// </summary>
        private readonly Action<object?> _executeAction;

        #endregion

        #region Public properties

        /// <summary>
        /// �������� �������� ���������� ���������� �������
        /// </summary>
        public Predicate<object?>? CanExecutePredicate { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// ����������� � ������ ��������������
        /// </summary>
        /// <param name="executeAction">�������� ��� ����������</param>
        /// <param name="canExecute">�������� ���������� �������</param>
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
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

        /// <summary>
        /// ������� �������� ���������� ���������� �������
        /// </summary>
        private readonly Func<object?, bool>? _canExecute;

        #endregion

        #region Constructors

        /// <summary>
        /// ����������� � ������ ��������������
        /// </summary>
        /// <param name="executeAction">�������� ��� ����������</param>
        /// <param name="canExecute">�������� ���������� �������</param>
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
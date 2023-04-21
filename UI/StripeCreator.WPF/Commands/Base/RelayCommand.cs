using System;

namespace StripeCreator.WPF
{

    /// <summary>
    /// ������� � ����������������� ���������
    /// </summary>
    public class RelayCommand : BaseCommand
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
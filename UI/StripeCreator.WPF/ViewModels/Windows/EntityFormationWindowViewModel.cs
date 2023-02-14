using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace StripeCreator.WPF
{
    /// <summary>
    /// ViewModel окна формирования (создания или изменения) сущности
    /// </summary>
    public class EntityFormationWindowViewModel : BaseViewModel
    {
        #region Public properties

        /// <summary>
        /// Заголовок окна
        /// </summary>
        public string Title { get; }

        /// <summary>
        /// Отображаемое представление формирования сущности
        /// </summary>
        public Control View { get; }

        #region Commands

        /// <summary>
        /// Команда при подтверждении формирования
        /// </summary>
        public ICommand OkCommand { get; }

        /// <summary>
        /// Команда при отмене формирования
        /// </summary>
        public ICommand CancelCommand { get; }

        #endregion

        #endregion

        #region Constructors

        /// <summary>
        /// Конструктор с полной инициализацией
        /// </summary>
        /// <param name="view">Представление формирования</param>
        /// <param name="title">Заголовок окна</param>
        /// <param name="okAction">Действие при подтверждении</param>
        /// <param name="cancelAction">Действие при отмене</param>
        public EntityFormationWindowViewModel(Control view, string title, Action<object?> okAction, Action<object?> cancelAction)
        {
            View = view;
            Title = title;
            OkCommand = new RelayCommand(okAction);
            CancelCommand = new RelayCommand(cancelAction);
        }

        #endregion
    }
}

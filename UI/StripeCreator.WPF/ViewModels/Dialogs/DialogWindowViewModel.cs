using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace StripeCreator.WPF
{
    /// <summary>
    /// ViewModel диалогового окна
    /// </summary>
    public class DialogWindowViewModel : BaseViewModel
    {
        #region Public properties

        /// <summary>
        /// Заголовок окна
        /// </summary>
        public string Title { get; }

        /// <summary>
        /// Заголовок представления
        /// </summary>
        public string Caption { get; }

        /// <summary>
        /// Отображаемое представление
        /// </summary>
        public Control View { get; }

        #region Commands

        /// <summary>
        /// Команда при подтверждении
        /// </summary>
        public ICommand OkCommand { get; }

        /// <summary>
        /// Текст кнопки подтверждения
        /// </summary>
        public string OkText { get; }

        /// <summary>
        /// Команда при отмене
        /// </summary>
        public ICommand CancelCommand { get; }

        /// <summary>
        /// Текст кнопки отмены
        /// </summary>
        public string CancelText { get; }

        #endregion

        #endregion

        #region Constructors

        /// <summary>
        /// Конструктор с полной инициализацией
        /// </summary>
        /// <param name="view">Отображаемое представление</param>
        /// <param name="title">Заголовок окна</param>
        /// <param name="caption">Заголовок представления</param>
        /// <param name="okAction">Действие при подтверждении</param>
        /// <param name="cancelAction">Действие при отмене</param>
        public DialogWindowViewModel(Control view, string title, string caption, Action<object?> okAction, Action<object?> cancelAction, string okText = "Ок", string cancelText = "Отмена")
        {
            View = view;
            Title = title;
            Caption = caption;
            OkCommand = new RelayCommand(okAction);
            OkText = okText;
            CancelCommand = new RelayCommand(cancelAction);
            CancelText = cancelText;
        }

        #endregion
    }
}

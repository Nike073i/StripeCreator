using FontAwesome5;
using System;
using System.Windows.Input;

namespace StripeCreator.WPF
{
    /// <summary>
    /// Класс view-model для элемента меню действий
    /// </summary>
    public class ActionMenuItemViewModel : BaseViewModel
    {
        #region Public properties

        /// <summary>
        /// Иконка действия
        /// </summary>
        public EFontAwesomeIcon Icon { get; set; }

        /// <summary>
        /// Текст действия
        /// </summary>
        public string ActionText { get; set; }

        /// <summary>
        /// Команда при нажатии
        /// </summary>
        public ICommand ActionCommand { get; }

        #endregion

        #region Constructors 

        /// <summary>
        /// Конструктор с полной инициализацией через делегат
        /// </summary>
        /// <param name="icon">Иконка действия</param>
        /// <param name="actionText">Текст действия</param>
        /// <param name="action">Действие</param>
        public ActionMenuItemViewModel(EFontAwesomeIcon icon, string actionText, Action<object?> action)
        {
            Icon = icon;
            ActionText = actionText;
            ActionCommand = new RelayCommand(action);
        }

        /// <summary>
        /// Конструктор с полной инициализацией через команду
        /// </summary>
        /// <param name="icon">Иконка действия</param>
        /// <param name="actionText">Текст действия</param>
        /// <param name="command">Команда</param>
        public ActionMenuItemViewModel(EFontAwesomeIcon icon, string actionText, ICommand command)
        {
            Icon = icon;
            ActionText = actionText;
            ActionCommand = command;
        }

        #endregion
    }
}

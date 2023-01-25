using FontAwesome5;
using System;
using System.Data;
using System.Windows.Input;

namespace StripeCreator.WPF
{
    /// <summary>
    /// Класс view-model для элемента меню действий
    /// </summary>
    public class ActionMenuItemViewModel : BaseViewModel
    {
        #region Public properties

        public EFontAwesomeIcon Icon { get; set; }
        public string ActionText { get; set; }
        public ICommand ActionCommand { get; }

        #endregion

        #region Constructors 

        public ActionMenuItemViewModel(EFontAwesomeIcon icon, string actionText, Action<object?> action)
        {
            Icon = icon;
            ActionText = actionText;
            ActionCommand = new RelayCommand(action);
        }

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public ActionMenuItemViewModel()
        {
            Icon = EFontAwesomeIcon.Regular_Image;
            ActionText = string.Empty;
            ActionCommand = new RelayCommand(_ => { });
        }

        #endregion
    }
}

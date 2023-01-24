using FontAwesome5;

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

        #endregion

        #region Constructors 

        public ActionMenuItemViewModel(EFontAwesomeIcon icon, string actionText)
        {
            Icon = icon;
            ActionText = actionText;
        }

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public ActionMenuItemViewModel()
        {
            Icon = EFontAwesomeIcon.Regular_Image;
            ActionText = string.Empty;
        }

        #endregion
    }
}

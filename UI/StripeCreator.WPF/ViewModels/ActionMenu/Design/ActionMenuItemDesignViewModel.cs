using FontAwesome5;

namespace StripeCreator.WPF
{
    /// <summary>
    /// Класс view-model элемента меню действий для design-mode
    /// </summary>
    public class ActionMenuItemDesignViewModel : ActionMenuItemViewModel
    {
        #region Singleton

        /// <summary>
        /// Экземпляр для показа в Design-mode
        /// </summary>
        public static ActionMenuItemDesignViewModel Instance => new();

        #endregion

        #region Constructors 

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public ActionMenuItemDesignViewModel()
        {
            Icon = EFontAwesomeIcon.Solid_Image;
            ActionText = "Загрузить изображение";
        }

        #endregion
    }
}

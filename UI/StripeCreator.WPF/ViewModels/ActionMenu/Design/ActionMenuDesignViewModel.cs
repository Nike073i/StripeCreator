using FontAwesome5;
using System.Collections.Generic;

namespace StripeCreator.WPF
{
    /// <summary>
    /// Класс view-model меню действий для design-mode
    /// </summary>
    public class ActionMenuDesignViewModel : ActionMenuViewModel
    {
        #region Singleton

        /// <summary>
        /// Экземпляр для показа в Design-mode
        /// </summary>
        public static ActionMenuDesignViewModel Instance => new();

        #endregion

        #region Constructors 

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public ActionMenuDesignViewModel()
        {
            Header = "Добро пожаловать!";
            Items = new List<ActionMenuItemViewModel> {
                new() {Icon = EFontAwesomeIcon.Solid_Image, ActionText = "Загрузить изображение" },
                new() {Icon = EFontAwesomeIcon.Solid_Ruler, ActionText = "Материалы" },
                new() {Icon = EFontAwesomeIcon.Solid_BusinessTime, ActionText = "Сообщество" },
                new() {Icon = EFontAwesomeIcon.Solid_Hashtag, ActionText = "Загрузить схему" },
            };
        }

        #endregion
    }
}

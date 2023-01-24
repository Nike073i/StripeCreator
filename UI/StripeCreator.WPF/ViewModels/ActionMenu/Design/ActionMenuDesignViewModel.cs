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
                new(EFontAwesomeIcon.Solid_Image, "Загрузить изображение"),
                new(EFontAwesomeIcon.Solid_Ruler, "Материалы"),
                new(EFontAwesomeIcon.Solid_BusinessTime, "Сообщество"),
                new(EFontAwesomeIcon.Solid_Hashtag, "Загрузить схему"),
            };
        }

        #endregion
    }
}

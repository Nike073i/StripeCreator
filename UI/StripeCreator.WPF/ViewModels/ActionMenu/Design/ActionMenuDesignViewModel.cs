using FontAwesome5;
using System;
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

        #region Design Data

        private static readonly string _header = "Добро пожаловать!";
        private static readonly Action<object?> emptyAction = new(_ => { });
        private static readonly List<ActionMenuItemViewModel> _items = new()
        {
             new (EFontAwesomeIcon.Solid_Image, "Загрузить изображение", emptyAction),
             new (EFontAwesomeIcon.Solid_Ruler, "Материалы", emptyAction),
             new (EFontAwesomeIcon.Solid_BusinessTime, "Сообщество", emptyAction),
             new (EFontAwesomeIcon.Solid_Hashtag, "Загрузить схему", emptyAction),
        };

        #endregion

        #region Constructors 

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public ActionMenuDesignViewModel() : base(_header, _items) { }

        #endregion
    }
}

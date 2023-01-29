using FontAwesome5;
using System;

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

        #region Design data

        private readonly static EFontAwesomeIcon _icon = EFontAwesomeIcon.Solid_Image;
        private readonly static string _actionText = "Загрузить изображение";
        private readonly static Action<object?> _emptyCommand = new(_ => { });

        #endregion

        #region Constructors 

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public ActionMenuItemDesignViewModel() : base(_icon, _actionText, _emptyCommand) { }

        #endregion
    }
}

using System.Collections.Generic;

namespace StripeCreator.WPF
{
    /// <summary>
    /// Класс view-model для меню действий
    /// </summary>
    public class ActionMenuViewModel : BaseViewModel
    {
        #region Public properties 

        public string Header { get; set; }
        public List<ActionMenuItemViewModel> Items { get; set; }

        #endregion

        #region Constructors 

        public ActionMenuViewModel(string header, List<ActionMenuItemViewModel> items)
        {
            Header = header;
            Items = items;
        }

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public ActionMenuViewModel()
        {
            Header = string.Empty;
            Items = new();
        }

        #endregion
    }
}

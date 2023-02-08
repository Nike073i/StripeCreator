using System.Collections.Generic;

namespace StripeCreator.WPF
{
    /// <summary>
    /// Класс view-model для меню действий
    /// </summary>
    public class ActionMenuViewModel : BaseViewModel
    {
        #region Private fields

        /// <summary>
        /// Список элементов меню
        /// </summary>
        private List<ActionMenuItemViewModel> _items;

        #endregion

        #region Public properties 

        /// <summary>
        /// Заголовок меню
        /// </summary>
        public string Header { get; set; }

        /// <summary>
        /// Список элементов меню
        /// </summary>
        public IEnumerable<ActionMenuItemViewModel> Items => _items;

        #endregion

        #region Constructors 

        /// <summary>
        /// Конструктор с полной инициализацией
        /// </summary>
        /// <param name="header">Заголовок меню</param>
        /// <param name="items">Элементы меню</param>
        public ActionMenuViewModel(string header, List<ActionMenuItemViewModel> items)
        {
            Header = header;
            _items = items;
        }

        #endregion
    }
}

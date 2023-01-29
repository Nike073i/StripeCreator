using System.Collections.Generic;

namespace StripeCreator.WPF
{
    /// <summary>
    /// Класс view-model для меню действий
    /// </summary>
    public class ActionMenuViewModel : BaseViewModel
    {
        #region Private fields

        private List<ActionMenuItemViewModel> _items;

        #endregion

        #region Public properties 

        public string Header { get; set; }
        public IEnumerable<ActionMenuItemViewModel> Items => _items;

        #endregion

        #region Constructors 

        public ActionMenuViewModel(string header, List<ActionMenuItemViewModel> items)
        {
            Header = header;
            _items = items;
        }

        #endregion
    }
}

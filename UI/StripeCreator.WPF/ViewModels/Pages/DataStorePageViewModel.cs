using FontAwesome5;
using System.Collections.Generic;

namespace StripeCreator.WPF
{
    /// <summary>
    /// Класс view-model для страницы работы со справочниками
    /// </summary>
    public class DataStorePageViewModel : BaseViewModel
    {
        #region Constants

        private readonly string _header = "Справочники";

        #endregion

        #region Public Properties 

        public ActionMenuViewModel ActionMenuViewModel { get; protected set; }

        #endregion

        #region Constructors

        public DataStorePageViewModel()
        {
            ActionMenuViewModel = new()
            {
                Header = _header,
                Items = GetSideMenuItems()
            };
        }

        #endregion

        #region Private helper methods

        private List<ActionMenuItemViewModel> GetSideMenuItems()
        {
            return new List<ActionMenuItemViewModel>
            {
                new(EFontAwesomeIcon.Solid_Bars, "Нитки", ShowThreadStore),
                new(EFontAwesomeIcon.Solid_CropAlt, "Ткани",ShowClothStore),
            };
        }

        private void ShowThreadStore(object? parameter) { }
        private void ShowClothStore(object? parameter) { }

        #endregion
    }
}

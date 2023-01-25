using FontAwesome5;
using System.Collections.Generic;

namespace StripeCreator.WPF
{
    /// <summary>
    /// Класс view-model для страницы приветствия
    /// </summary>
    public class WelcomePageViewModel : BaseViewModel
    {
        #region Constants

        private readonly string _header = "Добро пожаловать!";

        #endregion

        #region Public Properties 

        public ActionMenuViewModel ActionMenuViewModel { get; protected set; }

        #endregion

        #region Constructors

        public WelcomePageViewModel()
        {
            ActionMenuViewModel = new()
            {
                Header = _header,
                Items = GetActionMenuItems()
            };
        }

        #endregion

        #region Private helper methods

        private List<ActionMenuItemViewModel> GetActionMenuItems()
        {
            return new List<ActionMenuItemViewModel>
            {
                new(EFontAwesomeIcon.Solid_Image, "Загрузить изображение", LoadImagePage),
                new(EFontAwesomeIcon.Solid_Ruler, "Материалы",LoadMaterialPage),
                new(EFontAwesomeIcon.Solid_BusinessTime, "Сообщество", LoadCommunityPage),
                new(EFontAwesomeIcon.Solid_Hashtag, "Загрузить схему", LoadSchemePage),
            };
        }

        private void LoadImagePage(object? parameter) { }
        private void LoadMaterialPage(object? parameter) { }
        private void LoadCommunityPage(object? parameter) { }
        private void LoadSchemePage(object? parameter) { }

        #endregion
    }
}

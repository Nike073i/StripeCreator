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

        /// <summary>
        /// Заголовок меню действий
        /// </summary>
        private readonly string _header = "Добро пожаловать!";

        /// <summary>
        /// ViewModel приложения
        /// </summary>
        private readonly ApplicationViewModel _applicationViewModel;

        #endregion

        #region Public Properties 

        /// <summary>
        /// ViewModel меню действий
        /// </summary>
        public ActionMenuViewModel ActionMenuViewModel { get; protected set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
#nullable disable
        public WelcomePageViewModel() { }
#nullable enable

        /// <summary>
        /// Конструктор с полной инициализацией
        /// </summary>
        /// <param name="applicationViewModel">ViewModel приложения</param>
        public WelcomePageViewModel(ApplicationViewModel applicationViewModel)
        {
            _applicationViewModel = applicationViewModel;
            ActionMenuViewModel = new(_header, GetActionMenuItems());
        }

        #endregion

        #region Private helper methods

        /// <summary>
        /// Получить список элементов меню действий
        /// </summary>
        /// <returns>Список элементов меню действий</returns>
        private List<ActionMenuItemViewModel> GetActionMenuItems() =>
            new()
            {
                new(EFontAwesomeIcon.Solid_Image, "Обработать изображение", LoadImagePage),
                new(EFontAwesomeIcon.Solid_Database, "Справочники", LoadDataPage),
                new(EFontAwesomeIcon.Solid_BusinessTime, "Заказы", LoadOrderPage),
                new(EFontAwesomeIcon.Solid_ChartPie, "Отчеты", LoadReportPage),
                new(EFontAwesomeIcon.Solid_Globe, "Сообщество", LoadCommunityPage),
                new(EFontAwesomeIcon.Solid_Hashtag, "Загрузить схему", LoadSchemePage),
            };

        /// <summary>
        /// Загрузка страницы работы с изображением
        /// </summary>
        /// <param name="parameter">Параметр для команды</param>
        private void LoadImagePage(object? parameter) => _applicationViewModel.GoToPage(ApplicationPage.ImageProcessing);

        /// <summary>
        /// Загрузка страницы работы со справочниками
        /// </summary>
        /// <param name="parameter">Параметр для команды</param>
        private void LoadDataPage(object? parameter) => _applicationViewModel.GoToPage(ApplicationPage.DataStore);

        /// <summary>
        /// Загрузка страницы взаимодействия с заказами
        /// </summary>
        /// <param name="parameter">Параметр для команды</param>
        private void LoadOrderPage(object? parameter) => _applicationViewModel.GoToPage(ApplicationPage.Order);

        /// <summary>
        /// Загрузка страницы формирования отчетов
        /// </summary>
        /// <param name="parameter">Параметр для команды</param>
        private void LoadReportPage(object? parameter) => _applicationViewModel.GoToPage(ApplicationPage.Report);

        /// <summary>
        /// Загрузка страницы работы с сообществом
        /// </summary>
        /// <param name="parameter">Параметр для команды</param>
        private void LoadCommunityPage(object? parameter) { }

        /// <summary>
        /// Загрузка страницы работы со схемами
        /// </summary>
        /// <param name="parameter">Параметр для команды</param>
        private void LoadSchemePage(object? parameter) { }

        #endregion
    }
}

using FontAwesome5;
using Microsoft.Win32;
using StripeCreator.Stripe.Interfaces;
using StripeCreator.Stripe.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        #endregion

        #region Private fields

        /// <summary>
        /// ViewModel приложения
        /// </summary>
        private readonly ApplicationViewModel _applicationViewModel;

        /// <summary>
        /// Хранитель схем
        /// </summary>
        private readonly IDataKeeper<Scheme> _schemeKeeper;

        /// <summary>
        /// Менеджер интерактивного взаимодействия
        /// </summary>
        private readonly IUiManager _uiManager;

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
        /// <param name="schemeKeeper">Хранитель схем</param>
        /// <param name="uiManager">Менеджер интерактивного взаимодействия</param>
        public WelcomePageViewModel(ApplicationViewModel applicationViewModel, IDataKeeper<Scheme> schemeKeeper, IUiManager uiManager)
        {
            _applicationViewModel = applicationViewModel;
            _schemeKeeper = schemeKeeper;
            _uiManager = uiManager;
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
                new(EFontAwesomeIcon.Solid_Database, "Справочники", async param => await LoadDataPage(param)),
                new(EFontAwesomeIcon.Solid_BusinessTime, "Заказы", async param => await LoadOrderPage(param)),
                new(EFontAwesomeIcon.Solid_ChartPie, "Отчеты", async param => await LoadReportPage(param)),
                new(EFontAwesomeIcon.Solid_Globe, "Сообщество", async param => await LoadCommunityPage(param)),
                new(EFontAwesomeIcon.Solid_Hashtag, "Загрузить схему", async (param) => await LoadSchemePage(param)),
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
        private async Task LoadDataPage(object? parameter)
        {
            try
            {
                _applicationViewModel.GoToPage(ApplicationPage.DataStore);
            }
            catch (Exception ex)
            {
                await _uiManager.ShowError(new("Ошибка запуска страницы работы со справочниками", ex.Message));
            }
        }

        /// <summary>
        /// Загрузка страницы взаимодействия с заказами
        /// </summary>
        /// <param name="parameter">Параметр для команды</param>
        private async Task LoadOrderPage(object? parameter)
        {
            try
            {
                _applicationViewModel.GoToPage(ApplicationPage.Order);
            }
            catch (Exception ex)
            {
                await _uiManager.ShowError(new("Ошибка запуска страницы работы с заказами", ex.Message));
            }
        }

        /// <summary>
        /// Загрузка страницы формирования отчетов
        /// </summary>
        /// <param name="parameter">Параметр для команды</param>
        private async Task LoadReportPage(object? parameter)
        {
            try
            {
                _applicationViewModel.GoToPage(ApplicationPage.Report);
            }
            catch (Exception ex)
            {
                await _uiManager.ShowError(new("Ошибка запуска страницы работы с отчетами", ex.Message));
            }
        }

        /// <summary>
        /// Загрузка страницы работы с сообществом
        /// </summary>
        /// <param name="parameter">Параметр для команды</param>
        private async Task LoadCommunityPage(object? parameter)
        {
            if (!InternetChecker.IsConnectedToInternet())
            {
                await _uiManager.ShowError(new MessageBoxDialogViewModel("Ошибка", "Отсутствует подключение к интернету"));
                return;
            }
            try
            {
                _applicationViewModel.GoToPage(ApplicationPage.Community);
            }
            catch (Exception ex)
            {
                await _uiManager.ShowError(new("Ошибка запуска страницы работы с сообществом", ex.Message));
            }
        }

        /// <summary>
        /// Загрузка страницы работы со схемами
        /// </summary>
        /// <param name="parameter">Параметр для команды</param>
        private async Task LoadSchemePage(object? parameter)
        {
            var dialog = new OpenFileDialog
            {
                Filter = "Схема StripeCreator|*.sch"
            };
            if (dialog.ShowDialog() == false) return;
            try
            {
                var scheme = await _schemeKeeper.LoadAsync(dialog.FileName);
                _applicationViewModel.GoToPage(ApplicationPage.Scheme, scheme);
            }
            catch (Exception ex)
            {
                await _uiManager.ShowError(new("Ошибка загрузки схемы", ex.Message));
            }
        }

        #endregion
    }
}

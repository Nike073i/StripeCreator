using FontAwesome5;
using Microsoft.Win32;
using StripeCreator.Statistic.Models;
using StripeCreator.Statistic.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace StripeCreator.WPF
{
    /// <summary>
    /// ViewModel для страницы формирования отчетов
    /// </summary>
    public class ReportPageViewModel : BaseViewModel
    {
        #region Private fields

        /// <summary>
        /// Заголовок меню действий
        /// </summary>
        private readonly string _header = "Доступные действия";

        /// <summary>
        /// Сервис получения статистики
        /// </summary>
        private readonly StatisticService _statisticService;

        /// <summary>
        /// Сервис формирования отчетов
        /// </summary>
        private readonly ReportService _reportService;

        /// <summary>
        /// ViewModel приложения
        /// </summary>
        private readonly ApplicationViewModel _applicationViewModel;

        /// <summary>
        /// Менеджер интерактивного взаимодействия
        /// </summary>
        private readonly IUiManager _uiManager;

        #endregion

        #region Public properties

        /// <summary>
        /// ViewModel меню действий
        /// </summary>
        public ActionMenuViewModel ActionMenuViewModel { get; protected set; }

        /// <summary>
        /// Флаг установки значения у даты начала периода
        /// </summary>
        public bool IsDateStartSet { get; set; }

        /// <summary>
        /// Дата начала периода
        /// </summary>
        public DateTime DateStart { get; set; } = DateTime.Now.AddYears(-1);

        /// <summary>
        /// Дата окончания периода
        /// </summary>
        public DateTime DateEnd { get; set; } = DateTime.Now;

        /// <summary>
        /// Флаг установки значения у даты окончания периода
        /// </summary>
        public bool IsDateEndSet { get; set; }

        /// <summary>
        /// Данные для графика
        /// </summary>
        public IEnumerable<OrderIncomeDataModel> PlotData { get; set; } = Enumerable.Empty<OrderIncomeDataModel>();

        #region Commands

        /// <summary>
        /// Команда создания отчета
        /// </summary>
        public ICommand ReportCommand { get; }

        /// <summary>
        /// Команда отрисовки графика
        /// </summary>
        public ICommand ShowPlotCommand { get; }

        /// <summary>
        /// Команда выхода в главное меню
        /// </summary>
        public ICommand MenuCommand { get; }

        #endregion

        #endregion

        #region Constructors

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
#nullable disable
        public ReportPageViewModel() { }
#nullable enable

        /// <summary>
        /// Конструктор с полной инициализацией
        /// </summary>
        /// <param name="applicationViewModel">ViewModel приложения</param>
        /// <param name="statisticService">Сервис статистики</param>
        /// <param name="reportService">Сервис формирования отчетов</param>
        public ReportPageViewModel(ApplicationViewModel applicationViewModel, IUiManager uiManager, StatisticService statisticService, ReportService reportService)
        {
            _applicationViewModel = applicationViewModel;
            _uiManager = uiManager;
            _statisticService = statisticService;
            _reportService = reportService;

            // Инициализация команд
            ReportCommand = new RelayCommand(async param => await OnExecutedReportCommand(param));
            ShowPlotCommand = new RelayCommand(async param => await OnExecutedShowPlotCommand(param));
            MenuCommand = new RelayCommand(OnExecutedMenuCommand);

            ActionMenuViewModel = new(_header, GetMenuItems());
        }

        #endregion

        #region Private helper methods

        /// <summary>
        /// Получить список элементов меню действий
        /// </summary>
        /// <returns>Список элементов меню действий</returns>
        private List<ActionMenuItemViewModel> GetMenuItems()
        {
            return new List<ActionMenuItemViewModel>
            {
                new(EFontAwesomeIcon.Solid_FilePdf, "Создать отчет", ReportCommand),
                new(EFontAwesomeIcon.Solid_ChartLine, "Отрисовать график", ShowPlotCommand),
                new(EFontAwesomeIcon.Solid_ArrowLeft, "В меню", MenuCommand),
            };
        }

        #region Commands action and predicate

        /// <summary>
        /// Действие при команде выхода в главное меню
        /// </summary>
        /// <param name="parameter">Параметр команды</param>
        private void OnExecutedMenuCommand(object? parameter) => _applicationViewModel.GoToPage(ApplicationPage.Welcome);

        /// <summary>
        /// Действие при команде создания отчета
        /// </summary>
        /// <param name="parameter">Параметр команды</param>
        private async Task OnExecutedReportCommand(object? parameter)
        {
            DateTime? dateStart = IsDateStartSet ? DateStart : null;
            DateTime? dateEnd = IsDateEndSet ? DateEnd : null;
            var dialog = new SaveFileDialog
            {
                Filter = "pdf|*.pdf"
            };
            if (dialog.ShowDialog() == false) return;
            var IsCreated = await _reportService.CreateReport(dialog.FileName, dateStart, dateEnd);
            if (IsCreated)
                await _uiManager.ShowInfo(new("Отчет сохранен", "Успех"));
            else
                await _uiManager.ShowError(new("Ошибка сохранения отчета", "Неудача"));
        }

        /// <summary>
        /// Действие при команде отрисовки графика
        /// </summary>
        /// <param name="parameter">Параметр команды</param>
        private async Task OnExecutedShowPlotCommand(object? parameter)
        {
            DateTime? dateStart = IsDateStartSet ? DateStart : null;
            DateTime? dateEnd = IsDateEndSet ? DateEnd : null;
            PlotData = await _statisticService.GetOrderIncomeData(dateStart, dateEnd);
        }

        #endregion

        #endregion
    }
}

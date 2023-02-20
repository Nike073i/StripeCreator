using StripeCreator.Statistic.Reports;

namespace StripeCreator.Statistic.Services
{
    /// <summary>
    /// Сервис для формирования отчета
    /// </summary>
    public class ReportService
    {
        #region Private fields

        /// <summary>
        /// Сервис получения статистики
        /// </summary>
        private readonly StatisticService _statisticService;

        #endregion

        #region Constructors

        /// <summary>
        /// Конструктор с полной инциализацией
        /// </summary>
        /// <param name="statisticService">Сервис статистики</param>
        /// <param name="reportBuilder">Создатель отчета</param>
        public ReportService(StatisticService statisticService)
        {
            _statisticService = statisticService;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Создать отчет за период
        /// </summary>
        /// <param name="path">Путь сохранения отчета</param>
        /// <param name="dateStart">Дата начала периода</param>
        /// <param name="dateEnd">Дата окончания периода</param>
        /// <returns></returns>
        public async Task<bool> CreateReport(string path, DateTime? dateStart, DateTime? dateEnd)
        {
            var incomedata = await _statisticService.GetOrderIncomeData(dateStart, dateEnd);
            var statusData = await _statisticService.GetOrderStatusData(dateStart, dateEnd);
            var reportBuilder = new PdfReportBuilder("Отчет по заказам", dateStart, dateEnd);
            reportBuilder.CreateDocument();
            reportBuilder.InsertData(incomedata);
            reportBuilder.InsertData(statusData);
            return reportBuilder.SaveDocument(path);
        }

        #endregion
    }
}

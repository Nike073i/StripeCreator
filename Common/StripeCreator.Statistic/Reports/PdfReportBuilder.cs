using MigraDoc.DocumentObjectModel;
using MigraDoc.DocumentObjectModel.Shapes.Charts;
using MigraDoc.DocumentObjectModel.Tables;
using MigraDoc.Rendering;
using StripeCreator.Statistic.Extensions;
using StripeCreator.Statistic.Models;
using System.Text;

namespace StripeCreator.Statistic.Reports
{
    /// <summary>
    /// Строитель отчета в pdf-формате
    /// </summary>
    public class PdfReportBuilder : IReportBuilder
    {
        #region Constants

        /// <summary>
        /// Минимально допустимая дата
        /// </summary>
        private static readonly DateTime DateTimeMinValue = new DateTime(2000, 1, 1);

        /// <summary>
        /// Максимально допустимая дата
        /// </summary>
        private static readonly DateTime DateTimeMaxValue = DateTime.Now;

        /// <summary>
        /// Заполнитель пустой ячейки таблицы
        /// </summary>
        private static readonly string EmptyCell = string.Empty;

        #endregion

        #region Private fields

        /// <summary>
        /// Формируемый документ
        /// </summary>
        private Document _document;

        /// <summary>
        /// Заголовок документа
        /// </summary>
        private readonly string _title;

        /// <summary>
        /// Начало периода
        /// </summary>
        private readonly DateTime _dateStart;

        /// <summary>
        /// Окончание периода
        /// </summary>
        private readonly DateTime _dateEnd;

        #endregion

        #region Private properties

        /// <summary>
        /// Секция документа для вставки данных
        /// </summary>
        private Section Section => _document.LastSection;

        #endregion

        #region Public properties

        /// <summary>
        /// Формат вывода периода отчета
        /// </summary>
        public string DataInfoFormat { get; set; } = "с {0} по {1} число";

        /// <summary>
        /// Отступ после абзаца заголовка отчета в см
        /// </summary>
        public double SpaceAfterCm { get; set; } = 1;

        /// <summary>
        /// Размер шрифта абзаца заголовка отчета
        /// </summary>
        public double HeaderInfoFontSize { get; set; } = 12;

        #endregion

        #region Constructors

        /// <summary>
        /// Конструктор с полной инициализацией
        /// </summary>
        /// <param name="title">Заголовок отчета</param>
        /// <param name="dateStart">Дата начала периода</param>
        /// <param name="dateEnd">Дата окончания периода</param>
        public PdfReportBuilder(string title, DateTime? dateStart = null, DateTime? dateEnd = null)
        {
            _title = title;
            _dateStart = dateStart ?? DateTimeMinValue;
            _dateEnd = dateEnd ?? DateTimeMaxValue;
            _document = new Document();

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

        #endregion

        #region Interface implementations 

        public void CreateDocument()
        {
            _document.AddSection();
            InsertHeaderInfo(_title, _dateStart, _dateEnd);
        }

        public void InsertData(IEnumerable<OrderIncomeDataModel> data) => InsertOrderIncome(data);

        public void InsertData(IEnumerable<OrderStatusDataModel> data) => InsertOrderStatus(data);

        public bool SaveDocument(string path)
        {
            if (string.IsNullOrWhiteSpace(path)) return false;
            if (!path.EndsWith(".pdf")) path += ".pdf";
            var renderer = new PdfDocumentRenderer(true)
            {
                Document = _document
            };
            renderer.RenderDocument();
            renderer.Save(path);
            return true;
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Вставка заголовка отчета
        /// </summary>
        /// <param name="title">Текст заголовка</param>
        /// <param name="dateStart">Дата начала периода</param>
        /// <param name="dateEnd">Дата окончания периода</param>
        private void InsertHeaderInfo(string title, DateTime dateStart, DateTime dateEnd)
        {
            var headerInfoParagraph = new Paragraph();
            headerInfoParagraph.AddText(title);
            headerInfoParagraph.AddLineBreak();
            headerInfoParagraph.AddText(string.Format(DataInfoFormat, dateStart.ToShortDateString(), dateEnd.ToShortDateString()));
            headerInfoParagraph.Format.Alignment = ParagraphAlignment.Center;
            headerInfoParagraph.Format.Font.Bold = true;
            headerInfoParagraph.Format.Font.Size = HeaderInfoFontSize;
            Section.Add(headerInfoParagraph);
            Section.AddIndent(SpaceAfterCm);
        }

        /// <summary>
        /// Вставка отчета по ежедневной выручке
        /// </summary>
        /// <param name="data">Данные по ежедневной выручке</param>
        private void InsertOrderIncome(IEnumerable<OrderIncomeDataModel> data)
        {
            var table = new Table
            {
                Borders = new Borders() { Width = 1 },
            };
            table.Format.Alignment = ParagraphAlignment.Center;

            table.AddColumns(new[] { 2.5, 4, 4, 3, 2 });

            table.CreateRow(new List<string> { "Дата", "Номер клиента", "Почта клиента", "Сумма", "Статус" }, true);

            foreach (var item in data)
            {
                table.CreateRow(new List<string> { item.Date.ToShortDateString(), EmptyCell, EmptyCell, EmptyCell, EmptyCell }, true);

                foreach (var order in item.Orders)
                {
                    var cells = new[]
                    {
                        EmptyCell,
                        order.ContactData.ContactNumber,
                        order.ContactData.Email,
                        order.Price.ToString(),
                        order.Status.ToString(),
                    };
                    table.CreateRow(cells);
                }
                var incomeRow = new List<string> { EmptyCell, EmptyCell, EmptyCell, "Выручка", item.Income.ToString() };
                table.CreateRow(incomeRow, true);
            }

            var totalIncome = new List<string> { EmptyCell, EmptyCell, EmptyCell, "Итог", data.Sum(data => data.Income).ToString() };
            table.CreateRow(totalIncome, true);
            Section.Add(table);
            Section.AddIndent(SpaceAfterCm);
        }

        /// <summary>
        /// Вставка отчета по статусам заказов
        /// </summary>
        /// <param name="data">Данные по статусам заказов</param>
        private void InsertOrderStatus(IEnumerable<OrderStatusDataModel> data)
        {
            var chart = new Chart
            {
                Type = ChartType.Pie2D,
                Width = Unit.FromCentimeter(15.5),
                Height = Unit.FromCentimeter(10),
                PivotChart = true,
                HasDataLabel = true
            };

            chart.HeaderArea.AddParagraph("Статистика заказов по статусам");
            chart.HeaderArea.Height = Unit.FromCentimeter(1);
            chart.HeaderArea.Format.Font.Bold = true;
            chart.HeaderArea.Format.Font.Size = HeaderInfoFontSize;
            chart.Format.Alignment = ParagraphAlignment.Center;
            chart.RightArea.AddLegend();

            var dataSeries = chart.SeriesCollection.AddSeries();
            dataSeries.Add(data.Select(i => (double)i.Count).ToArray());
            dataSeries.DataLabel.Type = DataLabelType.Value;
            dataSeries.DataLabel.Position = DataLabelPosition.OutsideEnd;

            var labelSeries = chart.XValues.AddXSeries();
            labelSeries.Add(data.Select(i => i.Status.ToString()).ToArray());

            Section.Add(chart);
            Section.AddIndent(SpaceAfterCm);
        }

        #endregion
    }
}

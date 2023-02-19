using StripeCreator.Statistic.Models;

namespace StripeCreator.Statistic.Reports
{
    /// <summary>
    /// Интерфейс создателя отчетов
    /// </summary>
    public interface IReportBuilder
    {
        /// <summary>
        /// Создание документа отчета
        /// </summary>
        void CreateDocument();

        /// <summary>
        /// Вставка данных по выручке
        /// </summary>
        /// <param name="data">Данные по выручке</param>
        void InsertData(IEnumerable<OrderIncomeDataModel> data);

        /// <summary>
        /// Вставка данных по заказам в статусах
        /// </summary>
        /// <param name="data">Данные по заказам в статусах</param>
        void InsertData(IEnumerable<OrderStatusDataModel> data);

        /// <summary>
        /// Сохранение отчета по пути <paramref name="path"/>
        /// </summary>
        /// <param name="path">Абсолютный путь сохранения отчета</param>
        /// <returns>Результат сохранения</returns>
        bool SaveDocument(string path);
    }
}

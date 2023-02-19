using StripeCreator.Business.Models;

namespace StripeCreator.Statistic.Models
{
    /// <summary>
    /// Модель данных статистики по выручке за день
    /// </summary>
    public class OrderIncomeDataModel
    {
        #region Public properties

        /// <summary>
        /// Дата выручки
        /// </summary>
        public DateTime Date { get; }

        /// <summary>
        /// Заказы за день
        /// </summary>
        public IEnumerable<Order> Orders { get; }

        /// <summary>
        /// Выручка за день
        /// </summary>
        public decimal Income { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Конструктор с полной инциализацией
        /// </summary>
        /// <param name="date">Дата выручки</param>
        /// <param name="orders">Заказы за день</param>
        /// <param name="income">Выручка за день</param>
        public OrderIncomeDataModel(DateTime date, IEnumerable<Order> orders, decimal income)
        {
            Date = date;
            Orders = orders;
            Income = income;
        }

        #endregion
    }
}

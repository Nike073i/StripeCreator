using StripeCreator.Business.Enums;
using StripeCreator.Business.Models;

namespace StripeCreator.Statistic.Models
{
    /// <summary>
    /// Модель данных статистики по статусам заказов
    /// </summary>
    public class OrderStatusDataModel
    {
        #region Public properties

        /// <summary>
        /// Статус заказов
        /// </summary>
        public OrderStatus Status { get; }

        /// <summary>
        /// Заказы 
        /// </summary>
        public IEnumerable<Order> Orders { get; }

        /// <summary>
        /// Количество заказов
        /// </summary>
        public int Count => Orders.Count();

        #endregion

        #region Constructors

        /// <summary>
        /// Конструктор с полной инициализацией
        /// </summary>
        /// <param name="status">Статус заказов</param>
        /// <param name="orders">Заказы</param>
        public OrderStatusDataModel(OrderStatus status, IEnumerable<Order> orders)
        {
            Status = status;
            Orders = orders;
        }

        #endregion
    }

}

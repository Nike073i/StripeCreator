using StripeCreator.Business.Enums;
using StripeCreator.Business.Models;
using StripeCreator.Business.Repositories;
using StripeCreator.Statistic.Models;

namespace StripeCreator.Statistic.Services
{
    /// <summary>
    /// Сервис для получения статистических данных
    /// </summary>
    public class StatisticService
    {
        #region Private fields

        /// <summary>
        /// Репозиторий заказов
        /// </summary>
        private readonly IOrderRepository _orderRepository;

        #endregion

        #region Constructors

        /// <summary>
        /// Конструктор с полной инциализацией
        /// </summary>
        /// <param name="orderRepository">Репозиторий заказов</param>
        public StatisticService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Получить данные по ежедневной выручке
        /// </summary>
        /// <param name="dateStart">
        /// Дата начала периода. 
        /// Если null - то не учитывается
        /// </param>
        /// <param name="dateEnd">
        /// Дата окончания периода. 
        /// Если null - то не учитывается
        /// </param>
        /// <returns>Список данных по ежедневной выручке</returns>
        public async Task<IEnumerable<OrderIncomeDataModel>> GetOrderIncomeData(DateTime? dateStart, DateTime? dateEnd)
        {
            var orders = await _orderRepository.GetAllAsync();
            var ordersForPeriod = orders.Where(order => OrderInRange(order, dateStart, dateEnd) && OrderPayed(order));
            if (!ordersForPeriod.Any()) return Enumerable.Empty<OrderIncomeDataModel>();
            return GetOrderIncomeData(ordersForPeriod);
        }

        /// <summary>
        /// Получить данные по заказам в разрезе статусов
        /// </summary>
        /// <param name="dateStart">
        /// Дата начала периода. 
        /// Если null - то не учитывается
        /// </param>
        /// <param name="dateEnd">
        /// Дата окончания периода. 
        /// Если null - то не учитывается
        /// </param>
        /// <returns>Список данных по заказам в разрезе статусов</returns>
        public async Task<IEnumerable<OrderStatusDataModel>> GetOrderStatusData(DateTime? dateStart, DateTime? dateEnd)
        {
            var orders = await _orderRepository.GetAllAsync();
            var ordersForPeriod = orders.Where(order => OrderInRange(order, dateStart, dateEnd));
            if (!ordersForPeriod.Any()) return Enumerable.Empty<OrderStatusDataModel>();
            return GetOrderStatusData(ordersForPeriod);
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Определение вхождения заказа в указанный диапазон
        /// </summary>
        /// <param name="order">Заказ</param>
        /// <param name="dateStart">
        /// Дата начала периода. 
        /// Если null - то не учитывается
        /// </param>
        /// <param name="dateEnd">
        /// Дата окончания периода. 
        /// Если null - то не учитывается
        /// </param>
        /// <returns>Вхождение заказа в указанный диапазон</returns>
        private bool OrderInRange(Order order, DateTime? dateStart, DateTime? dateEnd) =>
            (!dateStart.HasValue || order.DateCreated.Date >= dateStart.Value.Date) && (!dateEnd.HasValue || order.DateCreated.Date <= dateEnd.Value.Date);

        /// <summary>
        /// Определение оплаченности заказа
        /// Если заказ отменен или только создан, то он не учитывается в выручке
        /// </summary>
        /// <param name="order">Заказ</param>
        /// <returns>Оплаченность заказа</returns>
        private bool OrderPayed(Order order) =>
            order.Status != OrderStatus.Created && order.Status != OrderStatus.Canceled;

        /// <summary>
        /// Получить ежедневную выручку по заказам
        /// </summary>
        /// <param name="ordersForPeriod">Список заказов</param>
        /// <returns>Данные по выручке в разрезе дат</returns>
        private IEnumerable<OrderIncomeDataModel> GetOrderIncomeData(IEnumerable<Order> ordersForPeriod)
        {
            var data = new List<OrderIncomeDataModel>();

            // Группируем заказы по датам
            var groups = ordersForPeriod.OrderBy(order => order.DateCreated)
                .GroupBy(sell => sell.DateCreated.Date)
                .Select(group => new
                {
                    Date = group.Key,
                    Orders = group.ToList(),
                }).ToList();

            // Высчитываем ежедневную выручку
            groups.ForEach(group =>
            {
                var income = group.Orders.Sum(order => order.Price);
                data.Add(new(group.Date, group.Orders, income));
            });

            return data;
        }

        /// <summary>
        /// Получить список групп заказов по статусу
        /// </summary>
        /// <param name="ordersForPeriod">Список заказов</param>
        /// <returns>Группы заказов по статусу</returns>
        private IEnumerable<OrderStatusDataModel> GetOrderStatusData(IEnumerable<Order> ordersForPeriod)
        {
            // Группируем заказы по статусу
            return ordersForPeriod.OrderBy(order => order.Status)
                .GroupBy(sell => sell.Status)
                .Select(group => new OrderStatusDataModel(group.Key, group));
        }

        #endregion
    }
}

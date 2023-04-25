using StripeCreator.Business.Enums;
using StripeCreator.Business.Models;
using StripeCreator.Business.Models.OperationModels;
using StripeCreator.Business.Repositories;

namespace StripeCreator.Business.Services
{
    /// <summary>
    /// Сервис работы с заказами
    /// </summary>
    public class OrderService
    {
        #region Private fields

        /// <summary>
        /// Калькулятор стоимости заказа
        /// </summary>
        private readonly OrderPriceCalculator _orderPriceCalculator;

        /// <summary>
        /// Репозиторий клиентов
        /// </summary>
        private readonly IClientRepository _clientRepository;

        /// <summary>
        /// Репозиторий заказов
        /// </summary>
        private readonly IOrderRepository _orderRepository;

        #endregion

        #region Constructors

        /// <summary>
        /// Конструктор с полной инициализацией
        /// </summary>
        /// <param name="orderPriceCalculator">Калькулятор стоимости заказа</param>
        /// <param name="clientRepository">Репозиторий клиентов</param>
        /// <param name="orderRepository">Репозиторий заказов</param>
        public OrderService(OrderPriceCalculator orderPriceCalculator, IClientRepository clientRepository, IOrderRepository orderRepository)
        {
            _orderPriceCalculator = orderPriceCalculator;
            _clientRepository = clientRepository;
            _orderRepository = orderRepository;
        }

        #endregion

        #region Public methods

        public async Task<IEnumerable<Order>> GetAllAsync() => await _orderRepository.GetAllAsync();

        public async Task<Order?> GetByIdAsync(Guid id) => await _orderRepository.GetByIdAsync(id);

        public async Task<IEnumerable<Order>> GetByClientIdAsync(Guid clientId) => await _orderRepository.GetByClientIdAsync(clientId);

        public async Task<IEnumerable<Order>> GetByStatusAsync(OrderStatus status) => await _orderRepository.GetByStatusAsync(status);

        /// <summary>
        /// Создание нового заказа
        /// </summary>
        /// <returns>Новый заказ</returns>
        /// <exception cref="ArgumentException">Возникает, если клиент с указанным <paramref name="orderCreateModel.ClientId"/> не найден</exception>
        public async Task<Order> CreateOrderAsync(OrderCreateModel orderCreateModel)
        {
            var client = await _clientRepository.GetByIdAsync(orderCreateModel.ClientId);
            if (client == null)
                throw new ArgumentException($"Клиент с указанным Id - {orderCreateModel.ClientId} не найден");
            var totalPrice = orderCreateModel.OrderPrice ?? await _orderPriceCalculator.CalculatePriceAsync(orderCreateModel.OrderProducts);
            var order = new Order(orderCreateModel.ClientId, totalPrice, orderCreateModel.OrderProducts, orderCreateModel.ContactData);
            return await _orderRepository.SaveAsync(order);
        }

        /// <summary>
        /// Отмена заказа
        /// </summary>
        /// <param name="id">Идентификатор заказа</param>
        /// <returns>Измененный заказ</returns>
        public Task<Order> CancelOrderAsync(Guid id) => ChangeOrderStatusAsync(id, OrderStatus.Canceled);

        /// <summary>
        /// Оплата заказа
        /// </summary>
        /// <param name="id">Идентификатор заказа</param>
        /// <returns>Измененный заказ</returns>
        public Task<Order> PayOrderAsync(Guid id) => ChangeOrderStatusAsync(id, OrderStatus.Paid);

        /// <summary>
        /// Обработка заказа
        /// </summary>
        /// <param name="id">Идентификатор заказа</param>
        /// <returns>Измененный заказ</returns>
        public Task<Order> ProcessOrderAsync(Guid id) => ChangeOrderStatusAsync(id, OrderStatus.Processed);

        /// <summary>
        /// Отправка заказа
        /// </summary>
        /// <param name="id">Идентификатор заказа</param>
        /// <returns>Измененный заказ</returns>
        public Task<Order> SendOrderAsync(Guid id) => ChangeOrderStatusAsync(id, OrderStatus.Sent);

        #endregion

        #region Private helper methods

        /// <summary>
        /// Изменение статуса заказа
        /// </summary>
        /// <param name="id">Идентификатор заказа</param>
        /// <param name="newStatus">Новый статус</param>
        /// <returns>Измененный заказ</returns>
        /// <exception cref="ArgumentException">Возникает, если заказ с указанным <paramref name="id"/> не найден</exception>
        private async Task<Order> ChangeOrderStatusAsync(Guid id, OrderStatus newStatus)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            if (order == null)
                throw new ArgumentException($"Заказ с указанным Id - {id} не найден");
            order.ChangeStatus(newStatus);
            return await _orderRepository.SaveAsync(order);
        }

        #endregion
    }
}
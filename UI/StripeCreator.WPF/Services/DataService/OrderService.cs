using StripeCreator.Business.Enums;
using StripeCreator.Business.Models;
using StripeCreator.Business.Models.OperationModels;
using StripeCreator.Business.Repositories;
using StripeCreator.Business.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StripeCreator.WPF
{
    /// <summary>
    /// Сервис для работы с ViewModel сущности <see cref="Order"/>
    /// </summary>
    public class OrderService
    {
        #region Private fields

        /// <summary>
        /// Сервис работы с продажами
        /// </summary>
        private readonly SaleService _saleService;

        /// <summary>
        /// Репозиторий заказов
        /// </summary>
        private readonly IOrderRepository _repository;

        #endregion

        #region Constructors

        /// <summary>
        /// Конструктор с полной инициализацией
        /// </summary>
        /// <param name="saleService">Сервис работы с продажами</param>
        public OrderService(SaleService saleService, IOrderRepository repository)
        {
            _saleService = saleService;
            _repository = repository;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Запрос на получение всех заказов в виде ViewModel
        /// </summary>
        /// <returns>Список ViewModel заказов</returns>
        /// <exception cref="Exception">Возникает, если при запросе получения всех заказов возникла ошибка</exception>
        public async Task<IEnumerable<OrderViewModel>> GetAllAsync()
        {
            var storedEntities = await _repository.GetAllAsync();
            return storedEntities.Select(CreateViewModel);
        }

        /// <summary>
        /// Запрос на создание заказа
        /// </summary>
        /// <param name="orderModel">Модель данных нового заказа</param>
        /// <returns>Созданный заказ</returns>
        public async Task<OrderViewModel> CreateAsync(OrderCreateModel orderModel)
        {
            var newOrder = await _saleService.CreateOrderAsync(orderModel);
            return CreateViewModel(newOrder);
        }

        /// <summary>
        /// Запрос на продвижение статуса заказа
        /// </summary>
        /// <param name="viewModel">ViewModel заказа</param>
        /// <returns>Измененный заказ</returns>
        /// <exception cref="ArgumentNullException">Возникает, если идентфикатор заказа не установлен</exception>
        /// <exception cref="InvalidOperationException">Возникает, если текущий статус не имеет продвижения</exception>
        public async Task<OrderViewModel> UpdateStatus(OrderViewModel viewModel)
        {
            var orderId = viewModel.GetEntityId() ?? throw new ArgumentNullException(nameof(viewModel), "Получен заказ без идентификатора");
            var changedOrder = viewModel.Status switch
            {
                OrderStatus.Created => await _saleService.PayOrderAsync(orderId),
                OrderStatus.Canceled => throw new InvalidOperationException("Нельзя изменить состояние у отмененного объекта"),
                OrderStatus.Paid => await _saleService.ProcessOrderAsync(orderId),
                OrderStatus.Processed => await _saleService.SendOrderAsync(orderId),
                OrderStatus.Sent => throw new InvalidOperationException("Нельзя изменить состояние у отправленного объекта"),
                _ => throw new InvalidOperationException("Для данного состояния не существует продвижения"),
            };
            return CreateViewModel(changedOrder);
        }

        /// <summary>
        /// Запрос на отмену заказа
        /// </summary>
        /// <param name="viewModel">ViewModel заказа</param>
        /// <returns>Измененный заказ</returns>
        /// <exception cref="ArgumentNullException">Возникает, если идентфикатор заказа не установлен</exception>
        public async Task<OrderViewModel> CancelOrder(OrderViewModel viewModel)
        {
            var orderId = viewModel.GetEntityId() ?? throw new ArgumentNullException(nameof(viewModel), "Получен заказ без идентификатора");
            var cancelOrder = await _saleService.CancelOrderAsync(orderId);
            return CreateViewModel(cancelOrder);
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Создание ViewModel для доменной сущности
        /// </summary>
        /// <param name="order">доменная сущность</param>
        /// <returns>ViewModel сущности</returns>
        private OrderViewModel CreateViewModel(Order order) => new(order);

        #endregion
    }
}

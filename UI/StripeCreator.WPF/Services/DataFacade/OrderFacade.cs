using StripeCreator.Business.Enums;
using StripeCreator.Business.Models;
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
    public class OrderFacade
    {
        #region Private fields

        /// <summary>
        /// Сервис работы с заказами
        /// </summary>
        private readonly OrderService _orderService;

        /// <summary>
        /// Сервис работы с продукцией
        /// </summary>
        private readonly ProductService _productService;

        /// <summary>
        /// Сервис работы с клиентами
        /// </summary>
        private readonly ClientService _clientService;

        /// <summary>
        /// Менеджер интерактивного взаимодействия
        /// </summary>
        private readonly IUiManager _uiManager;

        #endregion

        #region Constructors

        /// <summary>
        /// Конструктор с полной инициализацией
        /// </summary>
        /// <param name="saleService">Сервис работы с заказами</param>
        public OrderFacade(OrderService orderService, ProductService productService, ClientService clientService, IUiManager uiManager)
        {
            _orderService = orderService;
            _productService = productService;
            _clientService = clientService;
            _uiManager = uiManager;
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
            try
            {
                var storedEntities = await _orderService.GetAllAsync();
                return storedEntities.Select(CreateViewModel);
            }
            catch (Exception ex)
            {
                throw new Exception($"При запросе всех записей возникла ошибка - {ex.Message}", ex.InnerException);
            }
        }

        public async Task GetOrderInfo(OrderViewModel orderViewModel)
        {
            try
            {
                var order = orderViewModel.Entity;
                var client = await _clientService.GetByIdAsync(order.ClientId) ??
                    throw new InvalidOperationException("Клиент с указанным Id не найден");
                var orderProducts = new List<OrderProductViewModel>();
                foreach (var orderProduct in order.Products)
                {
                    var product = await _productService.GetByIdAsync(orderProduct.ProductId);
                    if (product == null) continue;
                    orderProducts.Add(new OrderProductViewModel(product, orderProduct.Quantity));
                }
                var orderDetailViewModel = new OrderDetailViewModel(order, client, orderProducts);
                await _uiManager.ShowOrderDetail(orderDetailViewModel);
            }
            catch (Exception ex)
            {
                throw new Exception($"При попытке получения данных заказа возникла ошибка - {ex.Message}", ex.InnerException);
            }
        }

        /// <summary>
        /// Запрос на создание заказа
        /// </summary>
        /// <returns>Созданный заказ</returns>
        public async Task<OrderViewModel?> CreateAsync()
        {
            try
            {
                var products = await _productService.GetAllAsync();
                var clients = await _clientService.GetAllAsync();
                var orderCreateModel = await _uiManager.CreateOrder(products, clients);
                if (orderCreateModel == null)
                    return null;
                var newOrder = await _orderService.CreateOrderAsync(orderCreateModel);
                return CreateViewModel(newOrder);
            }
            catch (Exception ex)
            {
                throw new Exception($"При запросе сохранения записи возникла ошибка - {ex.Message}", ex.InnerException);
            }
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
            try
            {
                var orderId = viewModel.GetEntityId() ?? throw new ArgumentNullException(nameof(viewModel), "Получен заказ без идентификатора");
                var changedOrder = viewModel.Status switch
                {
                    OrderStatus.Created => await _orderService.PayOrderAsync(orderId),
                    OrderStatus.Canceled => throw new InvalidOperationException("Нельзя изменить состояние у отмененного объекта"),
                    OrderStatus.Paid => await _orderService.ProcessOrderAsync(orderId),
                    OrderStatus.Processed => await _orderService.SendOrderAsync(orderId),
                    OrderStatus.Sent => throw new InvalidOperationException("Нельзя изменить состояние у отправленного объекта"),
                    _ => throw new InvalidOperationException("Для данного состояния не существует продвижения"),
                };
                return CreateViewModel(changedOrder);
            }
            catch (Exception ex)
            {
                throw new Exception($"При запросе обновления записи возникла ошибка - {ex.Message}", ex.InnerException);
            }
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
            var cancelOrder = await _orderService.CancelOrderAsync(orderId);
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

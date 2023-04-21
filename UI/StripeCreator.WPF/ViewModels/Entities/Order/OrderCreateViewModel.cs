using StripeCreator.Business.Models;
using StripeCreator.Business.Models.OperationModels;
using StripeCreator.Business.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace StripeCreator.WPF
{
    /// <summary>
    /// ViewModel представления для создания заказа
    /// </summary>
    public class OrderCreateViewModel : BaseViewModel
    {
        #region Private fields

        /// <summary>
        /// Выбранный клиент
        /// </summary>
        private ClientViewModel? _selectedClient;

        /// <summary>
        /// Менеджер интерактивного взаимодействия
        /// </summary>
        private readonly IUiManager _uiManager;

        /// <summary>
        /// Сервис работы с клеинтами
        /// </summary>
        private readonly ClientService _clientService;

        /// <summary>
        /// Сервис подсчета стоимости заказа
        /// </summary>
        private readonly OrderPriceCalculator _orderPriceCalculator;

        #endregion

        #region Public properties

        /// <summary>
        /// Сообщение с ошибкой заполнения данных
        /// </summary>
        public string ErrorString { get; private set; } = string.Empty;

        /// <summary>
        /// Список продуктов в заказе
        /// </summary>
        public ObservableCollection<OrderProductViewModel> OrderLines { get; private set; }

        /// <summary>
        /// Список доступных клиентов
        /// </summary>
        public ObservableCollection<ClientViewModel> Clients { get; set; }

        /// <summary>
        /// Выбранный клиент
        /// </summary>
        public ClientViewModel? SelectedClient
        {
            get => _selectedClient;
            set
            {
                _selectedClient = value;
                var client = _selectedClient?.Entity;
                if (client == null) return;
                var contactData = client.ContactData;
                ContactNumber = contactData.ContactNumber;
                Email = contactData.Email;
                Other = contactData.Other;
            }
        }


        /// <summary>
        /// Список доступных продуктов для добавления
        /// </summary>
        public IEnumerable<Product> Products { get; set; }

        /// <summary>
        /// Выбранный продукт для добавления
        /// </summary>
        public Product? SelectedProduct { get; set; }

        /// <summary>
        /// Количество добавляемого продукта
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Стоимость заказа
        /// </summary>
        public decimal? OrderPrice { get; set; }

        #region ContactData

        /// <summary>
        /// Контактные данные клиента. Номер телефона
        /// </summary>
        public string? ContactNumber { get; set; }

        /// <summary>
        ///  Контактные данные клиента. Электронный почтовый адрес
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        ///  Контактные данные клиента. Иные контактные сведения. Например адрес страницы в соц. сетях
        /// </summary>
        public string? Other { get; set; }

        #endregion

        #region Commands

        /// <summary>
        /// Команда удаления продукта из заказа
        /// </summary>
        public ICommand RemoveOrderProductCommand { get; }

        /// <summary>
        /// Команда добавления продукта в заказ
        /// </summary>
        public ICommand AddOrderProductCommand { get; }

        /// <summary>
        /// Команда создания клиента
        /// </summary>
        public ICommand CreateClientCommand { get; }

        /// <summary>
        /// Команда подсчета стоимости заказа
        /// </summary>
        public ICommand CalculatePriceCommand { get; }

        #endregion

        #endregion

        #region Constructors

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
#nullable disable
        public OrderCreateViewModel() { }
#nullable enable

        /// <summary>
        /// Конструктор с полной инициализацией
        /// </summary>
        public OrderCreateViewModel(IUiManager uiManager, ClientService clientService, OrderPriceCalculator orderPriceCalculator,
            IEnumerable<Client> clients, IEnumerable<Product> products)
        {
            _uiManager = uiManager;
            _clientService = clientService;
            _orderPriceCalculator = orderPriceCalculator;
            Clients = new ObservableCollection<ClientViewModel>(clients.Select(client => new ClientViewModel(client)));
            Products = products;
            OrderLines = new ObservableCollection<OrderProductViewModel>();

            // Инициализация комманд
            AddOrderProductCommand = new RelayCommand(OnExecutedAddOrderProductCommand) { CanExecutePredicate = CanExecuteAddOrderProductCommand };
            RemoveOrderProductCommand = new RelayCommand(OnExecutedRemoveOrderProductCommand) { CanExecutePredicate = CanExecuteRemoveOrderProductCommand };
            CreateClientCommand = new RelayCommand(async param => await OnExecutedCreateClientCommand(param));
            CalculatePriceCommand = new RelayCommand(async param => await OnExecutedCalculatePriceCommand(param)) { CanExecutePredicate = CanExecuteCalculatePriceCommand };
        }

        #endregion

        #region Public methods

        public OrderCreateModel? GetData() => ValidateData() ? TryCreateOrderModel() : null;

        #endregion

        #region Private methods

        /// <summary>
        /// Валидация введенных данных
        /// </summary>
        /// <returns></returns>
        private bool ValidateData()
        {
            if (SelectedClient == null
                 || !OrderLines.Any()
                 || string.IsNullOrWhiteSpace(ContactNumber)
                 || string.IsNullOrWhiteSpace(Email)
                 || !OrderPrice.HasValue)
            {
                ErrorString = "Заполните обязательные поля";
                return false;
            }
            return true;
        }

        /// <summary>
        /// Попытка создания модели по заполненным данным
        /// </summary>
        /// <returns>OrderCreateModel - если модель создана успешно
        /// null - если указанны некорректные данные</returns>
        private OrderCreateModel? TryCreateOrderModel()
        {
            try
            {
                // Проверка введенных контактных данных
                var contactData = new ContactData(ContactNumber!, Email!, Other);
                return new OrderCreateModel(SelectedClient!.GetEntityId()!.Value, contactData,
                    OrderLines.Select(line => line.OrderProduct), OrderPrice);
            }
            catch (Exception ex)
            {
                ErrorString = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// Действие при команде добавления продукта в заказ
        /// </summary>
        /// <param name="obj"></param>
        private void OnExecutedAddOrderProductCommand(object? obj)
        {
            var productId = SelectedProduct!.Id!.Value;
            var dict = OrderLines.ToDictionary(op => op.OrderProduct.ProductId);
            int newQuantity = 0;
            if (dict.ContainsKey(productId))
                newQuantity = dict[productId].Quantity + Quantity;
            else
                newQuantity = Quantity;
            dict[productId] = new(SelectedProduct!, newQuantity);
            OrderLines = new ObservableCollection<OrderProductViewModel>(dict.Values);
        }

        /// <summary>
        /// Проверка вызова команды добавления продукта в заказ
        /// </summary>
        /// <param name="parameter">Параметр команды</param>
        private bool CanExecuteAddOrderProductCommand(object? obj) => SelectedProduct != null && Quantity > 0 && Quantity < 250;

        /// <summary>
        /// Действие при команде удаления продукта из заказ
        /// </summary>
        /// <param name="obj"></param>
        private void OnExecutedRemoveOrderProductCommand(object? obj)
        {
            if (obj is not OrderProductViewModel orderProduct) return;
            OrderLines.Remove(orderProduct);
        }

        /// <summary>
        /// Проверка вызова команды удаления продукта из заказ
        /// </summary>
        /// <param name="parameter">Параметр команды</param>
        private bool CanExecuteRemoveOrderProductCommand(object? obj) => obj is OrderProductViewModel;

        /// <summary>
        /// Действие при команде создания клиента
        /// </summary>
        /// <param name="obj"></param>
        private async Task OnExecutedCreateClientCommand(object? obj)
        {
            var formationViewModel = new ClientFormationViewModel();
            var formationData = await _uiManager.FormationEntity(formationViewModel);
            if (formationData == null)
            {
                await _uiManager.ShowInfo(new("Отмена", "Создание клиента отменено"));
                return;
            }
            try
            {
                var newClient = await _clientService.SaveAsync(formationData);
                if (newClient is not ClientViewModel clientViewModel)
                    throw new InvalidOperationException();
                Clients.Add(clientViewModel);
                SelectedClient = clientViewModel;
            }
            catch (Exception ex)
            {
                await _uiManager.ShowError(new("Ошибка создания клиента", ex.Message));
            }
        }

        /// <summary>
        /// Действие при команде подсчета стоимости заказа
        /// </summary>
        /// <param name="parameter">Параметр команды</param>
        private async Task OnExecutedCalculatePriceCommand(object? parameter)
        {
            var orderProdutcs = OrderLines.Select(line => line.OrderProduct);
            OrderPrice = await _orderPriceCalculator.CalculatePriceAsync(orderProdutcs);
        }

        /// <summary>
        /// Проверка вызова команды удаления продукта из заказ
        /// </summary>
        /// <param name="parameter">Параметр команды</param>
        private bool CanExecuteCalculatePriceCommand(object? parameter) => OrderLines.Any();

        #endregion
    }
}

using StripeCreator.Business.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
        private Client? _selectedClient;

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
        public IEnumerable<Client> Clients { get; set; }

        /// <summary>
        /// Выбранный клиент
        /// </summary>
        public Client? SelectedClient
        {
            get => _selectedClient;
            set
            {
                _selectedClient = value;
                var contactData = _selectedClient?.ContactData;
                ContactNumber = contactData?.ContactNumber;
                Email = contactData?.Email;
                Other = contactData?.Other;
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
        public OrderCreateViewModel(IEnumerable<Client> clients, IEnumerable<Product> products)
        {
            Clients = clients;
            Products = products;
            OrderLines = new ObservableCollection<OrderProductViewModel>();

            // Инициализация комманд
            AddOrderProductCommand = new RelayCommand(OnExecutedAddOrderProductCommand) { CanExecutePredicate = CanExecuteAddOrderProductCommand };
            RemoveOrderProductCommand = new RelayCommand(OnExecutedRemoveOrderProductCommand) { CanExecutePredicate = CanExecuteRemoveOrderProductCommand };
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
            if (_selectedClient == null || !OrderLines.Any()
                 || string.IsNullOrWhiteSpace(ContactNumber) || string.IsNullOrWhiteSpace(Email))
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
                return new OrderCreateModel(_selectedClient!.Id!.Value, contactData,
                    OrderLines.Select(line => line.OrderProduct));
            }
            catch (Exception ex)
            {
                ErrorString = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// Добавить продукт в заказ
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
        /// Удалить продукт из заказ
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

        #endregion
    }
}

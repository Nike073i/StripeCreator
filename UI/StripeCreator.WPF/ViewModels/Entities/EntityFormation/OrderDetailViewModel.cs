using StripeCreator.Business.Enums;
using StripeCreator.Business.Models;
using System;
using System.Collections.Generic;

namespace StripeCreator.WPF
{
    /// <summary>
    /// ViewModel сущности <see cref="Order"/> для показа подробной информации
    /// </summary>
    public class OrderDetailViewModel : BaseViewModel
    {
        #region Private fields

        /// <summary>
        /// Доменная сущность заказа
        /// </summary>
        private readonly Order _order;

        /// <summary>
        /// Доменная сущность клиента, указанного в заказе
        /// </summary>
        private readonly Client _client;

        #endregion

        #region Public properties

        #region OrderData

        /// <summary>
        /// Дата создания заказа
        /// </summary>
        public DateTime DateCreated => _order.DateCreated;

        /// <summary>
        /// Стоимость заказа
        /// </summary>
        public decimal Price => _order.Price;

        /// <summary>
        /// Статус заказа
        /// </summary>
        public OrderStatus Status => _order.Status;

        #endregion

        #region ContactData

        /// <summary>
        /// Полное имя клиента
        /// </summary>
        public string ClientName => _client.PersonData.SecondName + " " + _client.PersonData.FirstName;

        /// <summary>
        /// Контактные данные заказа. Номер телефона
        /// </summary>
        public string? ContactNumber => _order.ContactData.ContactNumber;

        /// <summary>
        ///  Контактные данные заказа. Электронный почтовый адрес
        /// </summary>
        public string? Email => _order.ContactData.Email;

        /// <summary>
        ///  Контактные данные заказа. Иные контактные сведения.
        /// </summary>
        public string? Other => _order.ContactData.Other;

        #endregion

        /// <summary>
        /// Список продукции в заказе
        /// </summary>
        public IEnumerable<OrderProductViewModel> OrderProducts { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
#nullable disable
        public OrderDetailViewModel() { }
#nullable enable

        /// <summary>
        /// Конструктор с полной инициализацией
        /// </summary>
        /// <param name="order">Доменная модель заказа</param>
        /// <param name="client">Доменная модель клиента, указанного в заказе</param>
        public OrderDetailViewModel(Order order, Client client, IEnumerable<OrderProductViewModel> orderProducts)
        {
            _order = order;
            _client = client;
            OrderProducts = orderProducts;
        }

        #endregion
    }
}

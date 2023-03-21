using StripeCreator.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StripeCreator.WPF
{
    /// <summary>
    /// Модель для создания заказа
    /// </summary>
    public class OrderCreateModel
    {
        #region Public properties

        /// <summary>
        /// Список продуктов в заказе
        /// </summary>
        public IEnumerable<OrderProduct> OrderProducts { get; }

        /// <summary>
        /// Контактные данные заказа
        /// </summary>
        public ContactData ContactData { get; }

        /// <summary>
        /// Идентификатор клиента
        /// </summary>
        public Guid ClientId { get; }

        public decimal? OrderPrice { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Конструктор с полной инициализацией
        /// </summary>
        /// <param name="clientId">Идентификатор клиента</param>
        /// <param name="contactData">Контактные данные заказа</param>
        /// <param name="orderProducts">Список продуктов в заказе</param>
        /// <exception cref="ArgumentNullException">Возникает, если получен пустой список продукции в заказе</exception>
        public OrderCreateModel(Guid clientId, ContactData contactData, IEnumerable<OrderProduct> orderProducts, decimal? orderPrice = null)
        {
            ClientId = clientId;
            ContactData = contactData;
            if (!orderProducts.Any())
                throw new ArgumentNullException(nameof(orderProducts), "Список продукции в заказе не может быть пустым");
            if (orderPrice.HasValue && orderPrice <= 0)
                throw new ArgumentOutOfRangeException(nameof(orderPrice), "Стоимость заказа не может быть <= 0");
            OrderProducts = orderProducts;
            OrderPrice = orderPrice;
        }

        #endregion
    }
}

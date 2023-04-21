using StripeCreator.Business.Enums;
using StripeCreator.Business.Models;
using System;
using System.Collections.Generic;

namespace StripeCreator.WPF
{
    /// <summary>
    /// ViewModel доменной сущности <see cref="Order"/>
    /// </summary>
    public class OrderViewModel : EntityViewModel<Order>
    {
        #region Public properties

        /// <summary>
        /// Дата создания заказа
        /// </summary>
        public DateTime DateCreated => Entity.DateCreated;

        /// <summary>
        /// Стоимость заказа
        /// </summary>
        public decimal Price => Entity.Price;

        /// <summary>
        /// Статус заказа
        /// </summary>
        public OrderStatus Status => Entity.Status;

        /// <summary>
        /// Телефон клиента
        /// </summary>
        public string ContactNumber => Entity.ContactData.ContactNumber;

        #endregion

        #region Constructors

        /// <summary>
        /// Конструктор с полной инициализацией
        /// </summary>
        /// <param name="order">Доменная модель заказа</param>
        public OrderViewModel(Order order) : base(order) { }

        #endregion

        #region Interface implementations 

        public override EntityInfoViewModel GetData
        {
            get
            {
                var data = new List<EntityInfoValueViewModel>
                {
                    new("Дата заказа", DateCreated.ToString("dd.MM.yyyy")),
                    new("Статус заказа", Price.ToString()),
                    new("Стоимость", Status.ToString()),
                    new("Телефон клиента", ContactNumber),
                };

                return new EntityInfoViewModel(data);
            }
        }

        #endregion
    }
}

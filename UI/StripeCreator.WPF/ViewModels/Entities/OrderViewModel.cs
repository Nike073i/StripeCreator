using StripeCreator.Business.Models;
using System.Collections.Generic;

namespace StripeCreator.WPF
{
    /// <summary>
    /// ViewModel доменной сущности <see cref="Order"/>
    /// </summary>
    public class OrderViewModel : EntityViewModel<Order>
    {
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
                    new("Дата заказа", Entity.DateCreated.ToString("dd.MM.yyyy")),
                    new("Статус заказа", Entity.Price.ToString()),
                    new("Стоимость", Entity.Status.ToString()),
                    new("Телефон клиента", Entity.ContactData.ContactNumber),
                };

                return new EntityInfoViewModel(data);
            }
        }

        #endregion
    }
}

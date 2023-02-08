using StripeCreator.Business.Enums;
using StripeCreator.Core.Models;

namespace StripeCreator.Business.Models
{
    /// <summary>
    /// Класс сущности заказа
    /// </summary>
    public class Order : Entity
    {
        #region Private fields

        /// <summary>
        /// Идентификатор клиента
        /// </summary>
        private Guid _clientId;

        /// <summary>
        /// Стоимость заказа
        /// </summary>
        private decimal _price;

        /// <summary>
        /// Продукция заказа
        /// </summary>
        private IEnumerable<OrderProduct> _products = Enumerable.Empty<OrderProduct>();

        #endregion

        #region Public properties

        /// <summary>
        /// Идентификатор клиента
        /// </summary>
        public Guid ClientId
        {
            get => _clientId;
            set
            {
                if (value == Guid.Empty)
                    throw new ArgumentNullException(nameof(ClientId), "Идентификатор клиента не может быть null");
                _clientId = value;
            }
        }

        /// <summary>
        /// Стоимость заказа
        /// </summary>
        public decimal Price
        {
            get => _price;
            private set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(Price), "Стоимость заказа не может быть отрицательной");
                _price = value;
            }
        }

        /// <summary>
        /// Продукция заказа
        /// </summary>
        public IEnumerable<OrderProduct> Products
        {
            get => _products;
            private set
            {
                if (!value.Any())
                    throw new ArgumentNullException(nameof(Products), "Список продукции заказа не может быть пустым");
                _products = value;
            }
        }

        /// <summary>
        /// Контактные данные для получения заказа
        /// </summary>
        public ContactData ContactData { get; }

        /// <summary>
        /// Статус заказа
        /// </summary>
        public OrderStatus Status { get; private set; }

        /// <summary>
        /// Дата создания заказа
        /// </summary>
        public DateTime DateCreated { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Конструктор создания нового заказа
        /// </summary>
        /// <param name="clientId">Идентификатор клиента</param>
        /// <param name="price">Стоимость заказа</param>
        /// <param name="products">Продукция в заказе</param>
        /// <param name="contactData">Контактная информация получателя</param>
        public Order(Guid clientId, decimal price, IEnumerable<OrderProduct> products, ContactData contactData)
        {
            ClientId = clientId;
            Price = price;
            Products = products;
            ContactData = contactData;
            Status = OrderStatus.Created;
            DateCreated = DateTime.UtcNow;
        }

        /// <summary>
        /// Конструктор создания существующего заказа
        /// </summary>
        /// <param name="clientId">Идентификатор клиента</param>
        /// <param name="price">Стоимость заказа</param>
        /// <param name="products">Продукция в заказе</param>
        /// <param name="contactData">Контактная информация получателя</param>
        /// <param name="status">Текущий статус заказа</param>
        /// <param name="dateCreated">Дата создания заказа</param>
        /// <param name="id">Идентификатор заказа</param>
        public Order(Guid clientId, decimal price, IEnumerable<OrderProduct> products,
                    ContactData contactData, OrderStatus status, DateTime dateCreated, Guid? id = null) : base(id)
        {
            ClientId = clientId;
            Price = price;
            Products = products;
            ContactData = contactData;
            Status = status;
            DateCreated = dateCreated;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Добавление санкций к стоимости заказа
        /// </summary>
        /// <param name="cost">Значение санкции</param>
        /// <returns>Текущий заказ</returns>
        public Order AddSanctions(decimal cost)
        {
            Price -= cost;
            return this;
        }

        /// <summary>
        /// Смена статуса текущего заказа
        /// </summary>
        /// <param name="newStatus">Новый статус заказа</param>
        /// <returns>Текущий заказ</returns>
        /// <exception cref="InvalidOperationException">Возникает, если смена статуса заказа на новый является некорректным</exception>
        public Order ChangeStatus(OrderStatus newStatus)
        {
            if (Status == OrderStatus.Canceled || Status == OrderStatus.Sent)
                throw new InvalidOperationException("Заказ уже не может изменить свой статус");
            if (Status == newStatus) return this;

            // Проверка корректности перехода состояний заказа. Допускается только постепенный переход
            if (newStatus == OrderStatus.Canceled ||
                (Status == OrderStatus.Created && newStatus == OrderStatus.Paid) ||
                (Status == OrderStatus.Paid && newStatus == OrderStatus.Processed) ||
                (Status == OrderStatus.Processed && newStatus == OrderStatus.Sent))
                Status = newStatus;
            else throw new InvalidOperationException($"Заказ не может изменить текущий статус {Status} на {newStatus}");
            return this;
        }

        #endregion
    }
}
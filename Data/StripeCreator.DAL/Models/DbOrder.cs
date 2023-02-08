using StripeCreator.Business.Enums;
using System.ComponentModel.DataAnnotations;

namespace StripeCreator.DAL.Models
{
    /// <summary>
    /// Хранимая сущность заказа
    /// </summary>
    public class DbOrder : DbEntity
    {
        #region Public properties

        /// <summary>
        /// Идентификатор клиента
        ///</summary>
        [Required]
        public Guid ClientId { get; protected set; }

        /// <summary>
        /// Стоимость заказа
        /// </summary>
        [Required]
        public decimal Price { get; protected set; }

        /// <summary>
        /// Продукция заказа
        /// </summary>
        [Required]
        public IEnumerable<DbOrderProduct> Products { get; protected set; } = Enumerable.Empty<DbOrderProduct>();

        #region ContactData

        /// <summary>
        /// Контактные данные для получения заказа. Номер телефона
        /// </summary>
        [Required]
        public string ContactNumber { get; protected set; } = "+79020067089";

        /// <summary>
        ///  Контактные данные для получения заказа. Электронный почтовый адрес
        /// </summary>
        [Required]
        public string Email { get; protected set; } = "ulstu@mail.ru";

        /// <summary>
        ///  Контактные данные для получения заказа. Иные контактные сведения. Например адрес страницы в соц. сетях
        /// </summary>
        public string? Other { get; protected set; }

        #endregion

        /// <summary>
        /// Статус заказа
        /// </summary>
        [Required]
        public OrderStatus Status { get; set; }

        /// <summary>
        /// Дата создания заказа
        /// </summary>
        [Required]
        public DateTime DateCreated { get; protected set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Конструктор по умолчанию для EFC
        /// </summary>
        protected DbOrder() { }

        /// <summary>
        /// Конструктор c полной инициализацией
        /// </summary>
        /// <param name="clientId">Идентификатор клиента</param>
        /// <param name="price">Стоимость заказа</param>
        /// <param name="products">Продукция в заказе</param>
        /// <param name="contactNumber">Контактная информация получателя. Номер телефона</param>
        /// <param name="email">Контактная информация получателя. Электронная почта</param>
        /// <param name="other">Контактная информация получателя. Иные сведения</param>
        /// <param name="contactData">Контактная информация получателя</param>
        /// <param name="status">Текущий статус заказа</param>
        /// <param name="dateCreated">Дата создания заказа</param>
        /// <param name="id">Идентификатор заказа</param>
        public DbOrder(Guid clientId, decimal price, IEnumerable<DbOrderProduct> products,
                    string contactNumber, string email, string? other, OrderStatus status, DateTime dateCreated, Guid? id = null) : base(id)
        {
            ClientId = clientId;
            Price = price;
            Products = products;
            ContactNumber = contactNumber;
            Email = email;
            Other = other;
            Status = status;
            DateCreated = dateCreated;
        }

        #endregion
    }
}
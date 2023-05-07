using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StripeCreator.DAL.Models
{
    /// <summary>
    /// Хранимая сущность-связка для отношения M:М Заказов и продуктов
    /// </summary>
    [Table("OrderProducts")]
    public class DbOrderProduct
    {
        #region Public properties

        /// <summary>
        /// Идентификатор продукта. Составной ключ
        /// </summary>
        [Required]
        public Guid ProductId { get; protected set; }

        /// <summary>
        /// Идентификатор заказа. Составной ключ
        /// </summary>
        [Required]
        public Guid OrderId { get; protected set; }

        /// <summary>
        /// Количество продукции в заказе
        /// </summary>
        [Required]
        public int Quantity { get; protected set; }

        /// <summary>
        /// Навигационное свойство заказа
        /// </summary>
        public DbOrder DbOrder { get; protected set; }

        /// <summary>
        /// Навигационное свойство продукции
        /// </summary>
        public DbProduct DbProduct { get; protected set; }

        #endregion

        #region Constructors 
#nullable disable
        /// <summary>
        /// Конструктор по умолчанию для EFC
        /// </summary>
        protected DbOrderProduct() { }

        /// <summary>
        /// Конструктор для создания связки
        /// </summary>
        /// <param name="productId">Идентификатор продукта</param>
        /// <param name="quantity">Количество продукции в заказе</param>
        public DbOrderProduct(Guid productId, int quantity)
        {
            ProductId = productId;
            Quantity = quantity;
        }

        /// <summary>
        /// Конструктор с полной инициализацией
        /// </summary>
        /// <param name="productId">Идентификатор продукта</param>
        /// <param name="quantity">Количество продукции в заказе</param>
        /// <param name="orderId">Идентификатор заказа</param>
        public DbOrderProduct(Guid productId, int quantity, Guid orderId) : this(productId, quantity)
        {
            OrderId = orderId;
        }
#nullable enable

        #endregion
    }
}
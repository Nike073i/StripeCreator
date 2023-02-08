using System.ComponentModel.DataAnnotations;

namespace StripeCreator.DAL.Models
{
    /// <summary>
    /// Хранимая сущность-связка для отношения M:М Заказов и продуктов
    /// </summary>
    public class DbOrderProduct : DbEntity
    {
        #region Public properties

        /// <summary>
        /// Идентификатор продукта
        /// </summary>
        [Required]
        public Guid ProductId { get; protected set; }

        /// <summary>
        /// Количество продукции в заказе
        /// </summary>
        [Required]
        public int Quantity { get; protected set; }

        #endregion

        #region Constructors 

        /// <summary>
        /// Конструктор по умолчанию для EFC
        /// </summary>
        protected DbOrderProduct() { }

        /// <summary>
        /// Конструктор с полной инициализацией
        /// </summary>
        /// <param name="productId">Идентификатор продукта</param>
        /// <param name="quantity">Количество продукции в заказе</param>
        public DbOrderProduct(Guid productId, int quantity)
        {
            ProductId = productId;
            Quantity = quantity;
        }

        #endregion
    }
}
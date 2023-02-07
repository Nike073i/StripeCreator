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
        public Guid ProductId { get; set; }

        /// <summary>
        /// Количество продукции в заказе
        /// </summary>
        public int Quantity { get; set; }

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
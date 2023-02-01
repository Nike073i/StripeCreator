namespace StripeCreator.Business.Models
{
    public class OrderProduct
    {
        #region Public properties

        /// <summary>
        /// Идентификатор продукта
        /// </summary>
        public Guid ProductId { get; }

        /// <summary>
        /// Количество продукции в заказе
        /// </summary>
        public int Quantity { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Конструктор с полной инициализацией
        /// </summary>
        /// <param name="productId">Идентификатор продукта</param>
        /// <param name="quantity">Количество продукции в заказе</param>
        /// <exception cref="ArgumentNullException">Возникает, если <paramref name="productId"/> указан значением по умолчанию</exception>
        /// <exception cref="ArgumentOutOfRangeException">Возникает, если <paramref name="quantity"/> Имеет значение < 1</exception>
        public OrderProduct(Guid productId, int quantity)
        {
            if (productId == Guid.Empty) throw new ArgumentNullException(nameof(productId));
            if (quantity < 1) throw new ArgumentOutOfRangeException(nameof(quantity));

            ProductId = productId;
            Quantity = quantity;
        }

        #endregion

        #region Override object methods

        public override bool Equals(object? obj) => (obj is OrderProduct other) && Equals(other);

        public bool Equals(OrderProduct other) => other != null &&
                                                ProductId == other.ProductId &&
                                                Quantity == other.Quantity;

        public override int GetHashCode() => HashCode.Combine(ProductId, Quantity);

        public override string ToString() => $"Продукт - {ProductId}, {Quantity} шт.";

        #endregion
    }
}
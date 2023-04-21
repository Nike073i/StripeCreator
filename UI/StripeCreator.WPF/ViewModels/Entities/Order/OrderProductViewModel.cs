using StripeCreator.Business.Models;

namespace StripeCreator.WPF
{
    /// <summary>
    /// ViewModel доменной модели <see cref="OrderProduct"/>
    /// </summary>
    public class OrderProductViewModel
    {
        #region Private fields

        /// <summary>
        /// Продукт в заказе
        /// </summary>
        private Product _product;

        #endregion

        #region Public properties 

        /// <summary>
        /// Доменная запись продукта в заказе
        /// </summary>
        public OrderProduct OrderProduct => new(_product.Id!.Value, Quantity);

        /// <summary>
        /// Количества продукта в заказе
        /// </summary>
        public int Quantity { get; }

        /// <summary>
        /// Название продукта
        /// </summary>
        public string ProductName => _product.Name;

        #endregion

        #region Constructors

        public OrderProductViewModel(Product product, int quantity)
        {
            _product = product;
            Quantity = quantity;
        }

        #endregion
    }
}

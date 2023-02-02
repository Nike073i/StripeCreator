using StripeCreator.Business.Models;
using StripeCreator.Business.Repositories;

namespace StripeCreator.Business.Services
{
    /// <summary>
    /// Калькулятор стоимости заказа
    /// </summary>
    public class OrderPriceCalculator
    {
        #region Private fields

        /// <summary>
        /// Репозиторий продукции
        /// </summary>
        private readonly IProductRepository _productRepository;

        #endregion

        #region Constructors

        /// <summary>
        /// Конструктор с полной инициализацией
        /// </summary>
        /// <param name="productRepository">Репозиторий продукции</param>
        public OrderPriceCalculator(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Подсчет суммы стоимости по заказанной продукции
        /// </summary>
        /// <param name="orderProducts">Список заказанной продукции</param>
        /// <returns>Сумма стоимости заказа</returns>
        /// <exception cref="ArgumentException">Возникает, если в списке продукции <paramref name="orderProducts"/> найден несуществующий продукт</exception>
        public async Task<decimal> CalculatePriceAsync(IEnumerable<OrderProduct> orderProducts)
        {
            var price = 0m;
            foreach (var orderProduct in orderProducts)
            {
                var storedProduct = await _productRepository.GetByIdAsync(orderProduct.ProductId);
                if (storedProduct == null)
                    throw new ArgumentException($"Продукт с Id = {orderProduct.ProductId} не найден в хранилище.");
                price += storedProduct.Price * orderProduct.Quantity;
            }
            return price;
        }

        #endregion
    }
}
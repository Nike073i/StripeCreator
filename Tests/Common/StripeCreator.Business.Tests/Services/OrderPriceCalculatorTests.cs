using Moq;
using NUnit.Framework;
using StripeCreator.Business.Models;
using StripeCreator.Business.Repositories;
using StripeCreator.Business.Services;
using StripeCreator.Business.Tests.Infrastructure.Helpers.Models;

namespace StripeCreator.Business.Tests.Services
{
    internal class OrderPriceCalculatorTests
    {
        private static readonly List<Product> TestProducts = new()
        {
            ProductHelper.CreateProduct(price: 250m, id : new Guid("96001426-966e-4c7d-89e4-21867ef09787")),
            ProductHelper.CreateProduct(price: 550m, id : new Guid("5a69ae03-ff72-493d-a548-0b663f4bcb2d")),
            ProductHelper.CreateProduct(price: 1525m, id : new Guid("91fcf117-69a3-40d9-8c35-ee7e7f9c1120")),
        };

        private List<OrderProduct> TestOrderProducts = new List<OrderProduct>()
        {
            OrderProductHelper.CreateOrderProduct(productId: TestProducts[0].Id, quantity: 10),
            OrderProductHelper.CreateOrderProduct(productId: TestProducts[1].Id, quantity: 20),
            OrderProductHelper.CreateOrderProduct(productId: TestProducts[2].Id, quantity: 30),
        };

        private const decimal TestOrderPrice = 250m * 10 + 550m * 20 + 1525m * 30;

        [Test]
        public async Task CalculatePrice_EmptyLines_ReturnZero()
        {
            var productRepository = new Mock<IProductRepository>();
            var calculator = new OrderPriceCalculator(productRepository.Object);
            var price = await calculator.CalculatePriceAsync(Enumerable.Empty<OrderProduct>());
            Assert.That(price, Is.EqualTo(0));
        }

        [Test]
        public async Task Calculate_Price_ReturnSum()
        {
            var productRepository = new Mock<IProductRepository>();
            foreach (var product in TestProducts)
            {
                productRepository.Setup(repos => repos.GetByIdAsync(product.Id!.Value)).ReturnsAsync(product);
            }
            var calculator = new OrderPriceCalculator(productRepository.Object);
            decimal price = await calculator.CalculatePriceAsync(TestOrderProducts);
            Assert.That(price, Is.EqualTo(TestOrderPrice));
        }

        [Test]
        public void CalculatePrice_ProductNotFound_ThrowException()
        {
            var productRepository = new Mock<IProductRepository>();
            var calculator = new OrderPriceCalculator(productRepository.Object);
            async Task<decimal> calculateAsync() => await calculator!.CalculatePriceAsync(TestOrderProducts);
            Assert.ThrowsAsync<ArgumentException>(calculateAsync);
        }
    }
}

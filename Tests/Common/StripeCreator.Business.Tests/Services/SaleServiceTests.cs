using Moq;
using NUnit.Framework;
using StripeCreator.Business.Models;
using StripeCreator.Business.Repositories;
using StripeCreator.Business.Services;
using StripeCreator.Business.Tests.Infrastructure.Helpers.Models;

namespace StripeCreator.Business.Tests.Services
{
    internal class SaleServiceTests
    {
        [Test]
        public void Create_Order_ClientNotFound()
        {
            var saleService = GetSaleService();
            var clientId = ClientHelper.TestId;
            var orderProducts = new List<OrderProduct> { OrderProductHelper.CreateOrderProduct() };
            var contactData = ContactDataHelper.CreateContactData();
            async Task<Order> createAsync() => await saleService!.CreateOrderAsync(clientId, orderProducts, contactData);
            Assert.ThrowsAsync<ArgumentException>(createAsync);
        }

        [Test]
        public async Task Create_Order_ReturnNewOrder()
        {
            var client = ClientHelper.CreateClient();
            var clientRepository = new Mock<IClientRepository>();
            var product = ProductHelper.CreateProduct();
            var productRepository = new Mock<IProductRepository>();
            productRepository.Setup(repos => repos.GetByIdAsync(product.Id!.Value))
                             .ReturnsAsync(product);
            clientRepository.Setup(repos => repos.GetByIdAsync(client.Id!.Value))
                            .ReturnsAsync(client);
            var orderRepository = new Mock<IOrderRepository>();
            orderRepository.Setup(repos => repos.SaveAsync(It.IsAny<Order>())).ReturnsAsync((Order order) => order);

            var saleService = GetSaleService(productRepository.Object, clientRepository.Object, orderRepository.Object);
            var orderProducts = new List<OrderProduct> { new OrderProduct(product.Id!.Value, 15) };
            var contactData = ContactDataHelper.CreateContactData();

            var newOrder = await saleService!.CreateOrderAsync(client.Id!.Value, orderProducts, contactData);

            Assert.Multiple(() =>
            {
                Assert.That(newOrder, Is.Not.Null);
                Assert.That(newOrder.Status, Is.EqualTo(Enums.OrderStatus.Created));
                Assert.That(newOrder.ClientId, Is.EqualTo(client.Id));
                Assert.That(newOrder.ContactData, Is.EqualTo(contactData));
                Assert.That(newOrder.Products.SequenceEqual(orderProducts));
            });
        }

        [Test]
        public void Change_OrderStatus_OrderNotFound()
        {
            var saleService = GetSaleService();
            var orderId = OrderHelper.TestId;
            async Task<Order> changeStatus() => await saleService!.SendOrderAsync(orderId);
            Assert.ThrowsAsync<ArgumentException>(changeStatus);
        }

        private static SaleService GetSaleService(IProductRepository? productRepository = null,
                                                  IClientRepository? clientRepository = null,
                                                  IOrderRepository? orderRepository = null)
        {
            productRepository ??= new Mock<IProductRepository>().Object;
            var orderCalculator = new OrderPriceCalculator(productRepository);
            clientRepository ??= new Mock<IClientRepository>().Object;
            orderRepository ??= new Mock<IOrderRepository>().Object;
            return new SaleService(orderCalculator, clientRepository, orderRepository);
        }
    }
}

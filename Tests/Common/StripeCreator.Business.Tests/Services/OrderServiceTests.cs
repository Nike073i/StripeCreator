using Moq;
using NUnit.Framework;
using StripeCreator.Business.Models;
using StripeCreator.Business.Models.OperationModels;
using StripeCreator.Business.Repositories;
using StripeCreator.Business.Services;
using StripeCreator.Business.Tests.Infrastructure.Helpers.Models;

namespace StripeCreator.Business.Tests.Services
{
    internal class OrderServiceTests
    {
        [Test]
        public void Create_Order_ClientNotFound()
        {
            var orderService = GetOrderService();
            var clientId = ClientHelper.TestId;
            var orderProducts = new List<OrderProduct> { OrderProductHelper.CreateOrderProduct() };
            var contactData = ContactDataHelper.CreateContactData();
            var orderModel = new OrderCreateModel(clientId, contactData, orderProducts);
            async Task<Order> createAsync() => await orderService!.CreateOrderAsync(orderModel);
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

            var orderService = GetOrderService(productRepository.Object, clientRepository.Object, orderRepository.Object);
            var orderProducts = new List<OrderProduct> { new OrderProduct(product.Id!.Value, 15) };
            var contactData = ContactDataHelper.CreateContactData();

            var orderCreateModel = new OrderCreateModel(client.Id!.Value, contactData, orderProducts);
            var newOrder = await orderService!.CreateOrderAsync(orderCreateModel);

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
            var orderService = GetOrderService();
            var orderId = OrderHelper.TestId;
            async Task<Order> changeStatus() => await orderService!.SendOrderAsync(orderId);
            Assert.ThrowsAsync<ArgumentException>(changeStatus);
        }

        private static OrderService GetOrderService(IProductRepository? productRepository = null,
                                                  IClientRepository? clientRepository = null,
                                                  IOrderRepository? orderRepository = null)
        {
            productRepository ??= new Mock<IProductRepository>().Object;
            var orderCalculator = new OrderPriceCalculator(productRepository);
            clientRepository ??= new Mock<IClientRepository>().Object;
            orderRepository ??= new Mock<IOrderRepository>().Object;
            return new OrderService(orderCalculator, clientRepository, orderRepository);
        }
    }
}

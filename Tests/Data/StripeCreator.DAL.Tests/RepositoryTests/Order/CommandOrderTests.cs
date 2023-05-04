using Shouldly;
using StripeCreator.Business.Enums;
using StripeCreator.Business.Models;
using StripeCreator.DAL.Exceptions;
using StripeCreator.DAL.Models;
using StripeCreator.DAL.Repositories;
using StripeCreator.DAL.Tests.Base;
using StripeCreator.DAL.Tests.TestData;
using Xunit;
using OrderModel = StripeCreator.Business.Models.Order;

namespace StripeCreator.DAL.Tests.RepositoryTests.Order
{
    public class CommandOrderTests : BaseTest
    {
        private readonly DbOrderRepository _repository;

        public CommandOrderTests()
        {
            _repository = new DbOrderRepository(Context);
        }

        [Fact]
        public async Task CreateOrder_Success()
        {
            Context.AddRange(new[] { TestProducts.Product3 });
            Context.Add(TestOrders.Order3Client);
            Context.SaveChanges();

            var newOrderModel = MapFrom(TestOrders.Order3);
            var savedOrder = await _repository.SaveAsync(newOrderModel);

            savedOrder.ShouldNotBeNull();
            savedOrder.Id.ShouldNotBeNull();
            savedOrder.Id.ShouldNotBe(Guid.Empty);
        }


        [Fact]
        public void CreateOrderClientNotFound_ThrowException()
        {
            var newOrderModel = MapFrom(TestOrders.Order1);
            Should.ThrowAsync<EntityNotFoundException>(_repository.SaveAsync(newOrderModel));
        }

        [Fact]
        public void CreateOrderProductNotFound_ThrowException()
        {
            Context.Add(TestOrders.Order1Client);
            Context.SaveChanges();

            var newOrderModel = MapFrom(TestOrders.Order1);

            Should.ThrowAsync<EntityNotFoundException>(_repository.SaveAsync(newOrderModel));
        }

        [Fact]
        public async Task UpdateOrderStatus_Success()
        {
            Context.AddRange(new[] { TestProducts.Product2 });
            Context.Add(TestOrders.Order2Client);
            Context.Add(TestOrders.Order2);
            Context.SaveChanges();

            var storedOrder = MapFrom(TestOrders.Order2, true);
            var newStatus = OrderStatus.Processed;

            storedOrder.ChangeStatus(newStatus);
            var changedOrder = await _repository.SaveAsync(storedOrder);

            changedOrder.ShouldNotBeNull();
            changedOrder.Id.ShouldBe(storedOrder.Id);
            changedOrder.ClientId.ShouldBe(storedOrder.ClientId);
            changedOrder.Price.ShouldBe(storedOrder.Price);
            changedOrder.Products.Count().ShouldBe(storedOrder.Products.Count());
            changedOrder.ContactData.ShouldBe(storedOrder.ContactData);
            changedOrder.DateCreated.ShouldBe(storedOrder.DateCreated);
            changedOrder.Status.ShouldBe(newStatus);
        }

        [Fact]
        public void RemoveOrder_Success()
        {
            var testDelegate = async () => await _repository.RemoveAsync(Guid.NewGuid());
            Assert.ThrowsAsync<NotSupportedException>(testDelegate);
        }

        private static OrderModel MapFrom(DbOrder order, bool mapId = false)
        {
            var contactData = new ContactData(
                email: order.Email,
                other: order.Other,
                number: order.ContactNumber
            );
            var products = order.Products.Select(line => new OrderProduct(line.ProductId, line.Quantity));
            return mapId
                ? (new(
                    contactData: contactData,
                    clientId: order.ClientId,
                    products: products,
                    price: order.Price,
                    dateCreated: order.DateCreated,
                    status: order.Status,
                    id: order.Id))
                : (new(
                    contactData: contactData,
                    clientId: order.ClientId,
                    products: products,
                    price: order.Price));
        }
    }
}

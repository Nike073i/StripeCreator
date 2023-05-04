using Shouldly;
using StripeCreator.Business.Enums;
using StripeCreator.DAL.Repositories;
using StripeCreator.DAL.Tests.Fixtures;
using StripeCreator.DAL.Tests.TestData;
using Xunit;

namespace StripeCreator.DAL.Tests.RepositoryTests.Order
{
    [Collection("OrderCollection")]
    public class QueryOrderTests
    {
        private readonly DbOrderRepository _orderRepository;

        public QueryOrderTests(OrderQueryTextFixture fixture)
        {
            _orderRepository = fixture.OrderRepository;
        }

        [Fact]
        public async Task GetAllOrders_Success()
        {
            var orders = await _orderRepository.GetAllAsync();

            orders.ShouldNotBeNull();
            orders.Count().ShouldBe(OrderQueryTextFixture.Orders.Length);
        }

        [Fact]
        public async Task GetOrderById_Success()
        {
            var storedOrder = OrderQueryTextFixture.Orders[0];
            var receivedOrder = await _orderRepository.GetByIdAsync(storedOrder.Id!.Value);

            receivedOrder.ShouldNotBeNull();
            receivedOrder.Id.ShouldBe(storedOrder.Id);
            receivedOrder.Price.ShouldBe(storedOrder.Price);
            receivedOrder.ClientId.ShouldBe(storedOrder.ClientId);
            receivedOrder.ContactData.ContactNumber.ShouldBe(storedOrder.ContactNumber);
            receivedOrder.ContactData.Email.ShouldBe(storedOrder.Email);
            receivedOrder.ContactData.Other.ShouldBe(storedOrder.Other);
            receivedOrder.DateCreated.ShouldBe(storedOrder.DateCreated);
            receivedOrder.Status.ShouldBe(storedOrder.Status);
            receivedOrder.Products.Count().ShouldBe(TestOrderProducts.Order1Products.Length);
        }

        [Fact]
        public async Task GetOrderById_NotFound()
        {
            var receivedOrder = await _orderRepository.GetByIdAsync(Guid.NewGuid());
            receivedOrder.ShouldBeNull();
        }


        [Fact]
        public void GetOrdersSkipNegative_Fail()
        {
            int skipCount = -1;
            int takeCount = 5;

            Should.ThrowAsync<ArgumentOutOfRangeException>(_orderRepository.GetAsync(skipCount, takeCount));
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(0)]
        public void GetOrdersTakeIncorrect_Fail(int takeCount)
        {
            int skipCount = 5;

            Should.ThrowAsync<ArgumentOutOfRangeException>(_orderRepository.GetAsync(skipCount, takeCount));
        }

        [Fact]
        public async Task GetOrders_Success()
        {
            var skip = 1;
            var take = 2;
            var orders = await _orderRepository.GetAsync(skip, take);

            orders.ShouldNotBeNull();
            orders.Count().ShouldBeLessThanOrEqualTo(take);
        }

        [Fact]
        public async Task GetByClientId_Success()
        {
            var client = OrderQueryTextFixture.Clients[0];
            var receivedOrder = await _orderRepository.GetByClientIdAsync(client.Id!.Value);

            receivedOrder.ShouldNotBeNull();
            receivedOrder.ShouldNotBeEmpty();
        }

        [Fact]
        public async Task GetByClientId_NotFound()
        {
            var receivedOrder = await _orderRepository.GetByClientIdAsync(Guid.NewGuid());
            receivedOrder.ShouldNotBeNull();
            receivedOrder.ShouldBeEmpty();
        }

        [Fact]
        public async Task GetByStatus_Success()
        {
            var orderStatus = OrderStatus.Paid;
            var receivedOrder = await _orderRepository.GetByStatusAsync(orderStatus);

            receivedOrder.ShouldNotBeNull();
            receivedOrder.ShouldNotBeEmpty();
        }

        [Fact]
        public async Task GetByStatus_NotFound()
        {
            var orderStatus = OrderStatus.Created;
            var receivedOrder = await _orderRepository.GetByStatusAsync(orderStatus);
            receivedOrder.ShouldNotBeNull();
            receivedOrder.ShouldBeEmpty();
        }
    }
}

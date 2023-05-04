using StripeCreator.DAL.Models;
using StripeCreator.DAL.Repositories;
using StripeCreator.DAL.Tests.Extensions;
using StripeCreator.DAL.Tests.TestData;
using Xunit;

namespace StripeCreator.DAL.Tests.Fixtures
{
    public class OrderQueryTextFixture : QueryTextFixture
    {
        public DbOrderRepository OrderRepository => new(Context);

        public static readonly DbClient[] Clients =
        {
            TestClients.Client1,
            TestClients.Client2,
            TestClients.Client3,
            TestClients.Client4
        };

        public static readonly DbProduct[] Products =
        {
            TestProducts.Product1,
            TestProducts.Product2,
            TestProducts.Product3,
        };

        public static readonly IEnumerable<DbOrderProduct> OrderProducts = new List<DbOrderProduct>().AddRangeFromCollections(
            TestOrderProducts.Order1Products,
            TestOrderProducts.Order2Products,
            TestOrderProducts.Order3Products,
            TestOrderProducts.Order4Products,
            TestOrderProducts.Order5Products,
            TestOrderProducts.Order6Products
        );

        public static readonly DbOrder[] Orders =
        {
            TestOrders.Order1,
            TestOrders.Order2,
            TestOrders.Order3,
            TestOrders.Order4,
            TestOrders.Order5,
            TestOrders.Order6
        };

        protected override void InitializeData(StripeCreatorDb context)
        {
            base.InitializeData(context);
            context.AddRange(Clients);
            context.AddRange(Products);
            context.SaveChanges();
            context.AddRange(Orders);
            context.SaveChanges();
        }
    }

    [CollectionDefinition("OrderCollection")]
    public class OrderCollection : ICollectionFixture<OrderQueryTextFixture> { }
}

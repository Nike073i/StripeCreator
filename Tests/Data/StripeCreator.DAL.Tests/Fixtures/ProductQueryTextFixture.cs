using StripeCreator.DAL.Models;
using StripeCreator.DAL.Repositories;
using StripeCreator.DAL.Tests.TestData;
using Xunit;

namespace StripeCreator.DAL.Tests.Fixtures
{
    public class ProductQueryTextFixture : QueryTextFixture
    {
        public DbProductRepository ProductRepository => new(Context);

        public static readonly DbProduct[] Products =
        {
            TestProducts.Product1,
            TestProducts.Product2,
            TestProducts.Product3,
        };

        protected override void InitializeData(StripeCreatorDb context)
        {
            base.InitializeData(context);
            context.AddRange(Products);
            context.SaveChanges();
        }
    }

    [CollectionDefinition("ProductCollection")]
    public class ProductCollection : ICollectionFixture<ProductQueryTextFixture> { }
}

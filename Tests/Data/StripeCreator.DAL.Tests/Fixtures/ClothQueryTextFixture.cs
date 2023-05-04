using StripeCreator.DAL.Models;
using StripeCreator.DAL.Repositories;
using StripeCreator.DAL.Tests.TestData;
using Xunit;

namespace StripeCreator.DAL.Tests.Fixtures
{
    public class ClothQueryTextFixture : QueryTextFixture
    {
        public DbClothRepository ClothRepository => new(Context);

        public static readonly DbCloth[] Cloths =
        {
            TestCloths.Cloth1,
            TestCloths.Cloth2,
            TestCloths.Cloth3,
            TestCloths.Cloth4,
        };

        protected override void InitializeData(StripeCreatorDb context)
        {
            base.InitializeData(context);
            context.AddRange(Cloths);
            context.SaveChanges();
        }
    }

    [CollectionDefinition("ClothCollection")]
    public class ClothCollection : ICollectionFixture<ClothQueryTextFixture> { }
}

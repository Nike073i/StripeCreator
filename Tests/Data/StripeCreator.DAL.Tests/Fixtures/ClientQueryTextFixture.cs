using StripeCreator.DAL.Models;
using StripeCreator.DAL.Repositories;
using StripeCreator.DAL.Tests.TestData;
using Xunit;

namespace StripeCreator.DAL.Tests.Fixtures
{
    public class ClientQueryTextFixture : QueryTextFixture
    {
        public DbClientRepository ClientRepository => new(Context);

        public static readonly DbClient[] Clients =
        {
            TestClients.Client1,
            TestClients.Client2,
            TestClients.Client3,
            TestClients.Client4
        };

        protected override void InitializeData(StripeCreatorDb context)
        {
            base.InitializeData(context);
            context.AddRange(Clients);
            context.SaveChanges();
        }
    }

    [CollectionDefinition("ClientCollection")]
    public class ClientCollection : ICollectionFixture<ClientQueryTextFixture> { }
}

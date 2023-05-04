using StripeCreator.DAL.Models;
using StripeCreator.DAL.Repositories;
using StripeCreator.DAL.Tests.TestData;
using Xunit;

namespace StripeCreator.DAL.Tests.Fixtures
{
    public class ThreadQueryTextFixture : QueryTextFixture
    {
        public DbThreadRepository ThreadRepository => new(Context);

        public static readonly DbThread[] Threads =
        {
            TestThreads.Thread1,
            TestThreads.Thread2,
            TestThreads.Thread3,
            TestThreads.Thread4,
        };

        protected override void InitializeData(StripeCreatorDb context)
        {
            base.InitializeData(context);
            context.AddRange(Threads);
            context.SaveChanges();
        }
    }

    [CollectionDefinition("ThreadCollection")]
    public class ThreadCollection : ICollectionFixture<ThreadQueryTextFixture> { }
}

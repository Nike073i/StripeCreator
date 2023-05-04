using StripeCreator.DAL.Tests.Infrastructure;

namespace StripeCreator.DAL.Tests.Fixtures
{
    public abstract class QueryTextFixture : IDisposable
    {
        protected StripeCreatorDb Context { get; private set; }

        protected QueryTextFixture()
        {
            Context = AppContextFactory.Create();
            InitializeData(Context);
        }

        public virtual void Dispose()
        {
            AppContextFactory.Destroy(Context);
        }

        protected virtual void InitializeData(StripeCreatorDb context) { }
    }
}

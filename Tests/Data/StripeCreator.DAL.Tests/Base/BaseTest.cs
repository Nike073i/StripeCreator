using StripeCreator.DAL.Tests.Infrastructure;

namespace StripeCreator.DAL.Tests.Base
{
    public abstract class BaseTest : IDisposable
    {
        protected readonly StripeCreatorDb Context;

        protected BaseTest()
        {
            Context = AppContextFactory.Create();
        }

        public virtual void Dispose()
        {
            AppContextFactory.Destroy(Context);
        }
    }
}

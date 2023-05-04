using Microsoft.EntityFrameworkCore;

namespace StripeCreator.DAL.Tests.Infrastructure
{
    internal class AppContextFactory
    {
        public static StripeCreatorDb Create()
        {
            var options = new DbContextOptionsBuilder<StripeCreatorDb>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            var context = new StripeCreatorDb(options);
            context.Database.EnsureCreated();
            return context;
        }

        public static void Destroy(StripeCreatorDb db)
        {
            db.Database.EnsureDeleted();
            db.Dispose();
        }
    }
}

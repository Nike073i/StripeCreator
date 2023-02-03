using StripeCreator.Core.Repositories;
using Thread = StripeCreator.Stripe.Models.Thread;

namespace StripeCreator.Stripe.Repositories
{
    public interface IThreadRepository : IRepository<Thread> { }
}

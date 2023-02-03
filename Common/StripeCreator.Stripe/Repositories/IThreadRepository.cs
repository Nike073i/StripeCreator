using StripeCreator.Core.Repositories;
using Thread = StripeCreator.Stripe.Models.Thread;

namespace StripeCreator.Stripe.Repositories
{
    /// <summary>
    /// Интерфейс репозитория для сущности <see cref="Thread"/>
    /// </summary>
    public interface IThreadRepository : IRepository<Thread> { }
}

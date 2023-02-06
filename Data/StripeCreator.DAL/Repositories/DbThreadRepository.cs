using StripeCreator.DAL.Mappers;
using StripeCreator.DAL.Models;
using StripeCreator.Stripe.Repositories;
using Thread = StripeCreator.Stripe.Models.Thread;

namespace StripeCreator.DAL.Repositories
{
    /// <summary>
    /// Реализация интерфейса сущности <see cref="Thread"/>
    /// </summary>
    public class DbThreadRepository : DbRepository<DbThread, Thread>, IThreadRepository
    {
        #region Constructors 

        /// <summary>
        /// Конструктор с полной инициаилизацией
        /// </summary>
        /// <param name="context">Контекст БД</param>
        public DbThreadRepository(StripeCreatorDb context) : base(context, new ThreadMapper()) { }

        #endregion
    }
}

using StripeCreator.Business.Models;
using StripeCreator.Business.Repositories;
using StripeCreator.DAL.Mappers;
using StripeCreator.DAL.Models;

namespace StripeCreator.DAL.Repositories
{
    /// <summary>
    /// Реализация интерфейса сущности <see cref="Client"/>
    /// </summary>
    public class DbClientRepository : DbRepository<DbClient, Client>, IClientRepository
    {
        #region Constructors 

        /// <summary>
        /// Конструктор с полной инициаилизацией
        /// </summary>
        /// <param name="context">Контекст БД</param>
        public DbClientRepository(StripeCreatorDb context) : base(context, new ClientMapper()) { }

        #endregion
    }
}
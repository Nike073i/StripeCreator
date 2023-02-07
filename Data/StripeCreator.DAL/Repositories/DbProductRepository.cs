using StripeCreator.Business.Models;
using StripeCreator.Business.Repositories;
using StripeCreator.DAL.Mappers;
using StripeCreator.DAL.Models;

namespace StripeCreator.DAL.Repositories
{
    /// <summary>
    /// Реализация интерфейса сущности <see cref="Product"/>
    /// </summary>
    public class DbProductRepository : DbRepository<DbProduct, Product>, IProductRepository
    {
        #region Constructors 

        /// <summary>
        /// Конструктор с полной инициаилизацией
        /// </summary>
        /// <param name="context">Контекст БД</param>
        public DbProductRepository(StripeCreatorDb context) : base(context, new ProductMapper()) { }

        #endregion
    }
}
using StripeCreator.DAL.Mappers;
using StripeCreator.DAL.Models;
using StripeCreator.Stripe.Models;
using StripeCreator.Stripe.Repositories;

namespace StripeCreator.DAL.Repositories
{
    /// <summary>
    /// Реализация интерфейса сущности <see cref="Cloth"/>
    /// </summary>
    public class DbClothRepository : DbRepository<DbCloth, Cloth>, IClothRepository
    {
        #region Constructors 

        /// <summary>
        /// Конструктор с полной инициаилизацией
        /// </summary>
        /// <param name="context">Контекст БД</param>
        public DbClothRepository(StripeCreatorDb context) : base(context, new ClothMapper()) { }

        #endregion
    }
}

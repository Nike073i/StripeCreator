using Microsoft.EntityFrameworkCore;
using StripeCreator.Business.Enums;
using StripeCreator.Business.Models;
using StripeCreator.Business.Repositories;
using StripeCreator.DAL.Mappers;
using StripeCreator.DAL.Models;

namespace StripeCreator.DAL.Repositories
{
    /// <summary>
    /// Реализация интерфейса сущности <see cref="Order"/>
    /// </summary>
    public class DbOrderRepository : DbRepository<DbOrder, Order>, IOrderRepository
    {
        #region Constructors 

        /// <summary>
        /// Конструктор с полной инициаилизацией
        /// </summary>
        /// <param name="context">Контекст БД</param>
        public DbOrderRepository(StripeCreatorDb context) : base(context, new OrderMapper()) { }

        public async Task<IEnumerable<Order>> GetByClientIdAsync(Guid clientId)
        {
            return await Items.Where(order => order.ClientId == clientId)
                .Select(order => Mapper.MapToDomainModel(order))
                .ToListAsync();
        }

        public async Task<IEnumerable<Order>> GetByStatusAsync(OrderStatus status)
        {
            return await Items.Where(order => order.Status == status)
                .Select(order => Mapper.MapToDomainModel(order))
                .ToListAsync();
        }

        #endregion
    }
}
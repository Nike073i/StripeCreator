using Microsoft.EntityFrameworkCore;
using StripeCreator.Business.Enums;
using StripeCreator.Business.Models;
using StripeCreator.Business.Repositories;
using StripeCreator.DAL.Exceptions;
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

        #region Overrides

        public override async Task<Order> SaveAsync(Order order)
        {
            var storedOrder = await Items.SingleOrDefaultAsync(o => o.Id == order.Id);
            if (storedOrder == null)
            {
                var clientId = order.ClientId;
                var client = await DbContext.Clients.SingleOrDefaultAsync(entry => entry.Id == clientId);
                if (client == null)
                    throw new EntityNotFoundException($"Клиент с Id {clientId} не найден");
                var productIds = order.Products.Select(line => line.ProductId);
                foreach (var id in productIds)
                {
                    var isProductExist = await DbContext.Products.AnyAsync(product => product.Id == id);
                    if (!isProductExist)
                        throw new EntityNotFoundException($"Продукт с Id {id} не найден");
                }
                storedOrder = Mapper.CreateDbModel(order);
                await Set.AddAsync(storedOrder);
            }
            else
            {
                Mapper.UpdateDbModel(order, ref storedOrder);
                Set.Update(storedOrder);
            }
            await DbContext.SaveChangesAsync();
            return Mapper.MapToDomainModel(storedOrder);
        }

        public override Task<Guid> RemoveAsync(Guid id)
        {
            throw new NotSupportedException("Удаление заказа не поддерживается");
        }

        protected override IQueryable<DbOrder> Items => Set.Include(o => o.Products).Include(o => o.Client);

        #endregion
    }
}
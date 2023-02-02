using StripeCreator.Business.Enums;
using StripeCreator.Business.Models;
using StripeCreator.Core.Repositories;

namespace StripeCreator.Business.Repositories
{
    /// <summary>
    /// Интерфейс репозитория для сущности <see cref="Order"/>
    /// </summary>
    public interface IOrderRepository : IRepository<Order>
    {
        /// <summary>
        /// Получение списка заказов клиента
        /// </summary>
        /// <param name="clientId">Идентификатор клиента</param>
        /// <returns>Перечисление заказов клиента</returns>
        public Task<IEnumerable<Order>> GetByClientIdAsync(Guid clientId);

        /// <summary>
        /// Получение списка заказов по статусу
        /// </summary>
        /// <param name="status">Статус заказа</param>
        /// <returns>Перечисление заказов в статусе <paramref name="status"/></returns>
        public Task<IEnumerable<Order>> GetByStatusAsync(OrderStatus status);
    }
}
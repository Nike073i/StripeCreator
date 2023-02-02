using StripeCreator.Business.Models;
using StripeCreator.Core.Repositories;

namespace StripeCreator.Business.Repositories
{
    /// <summary>
    /// Интерфейс репозитория для сущности <see cref="Client"/>
    /// </summary>
    public interface IClientRepository : IRepository<Client> { }
}
using StripeCreator.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StripeCreator.WPF
{
    /// <summary>
    /// Сервис для работы с ViewModel сущности <see cref="Client"/>
    /// </summary>
    public class ClientService : IDataService
    {
        #region Private fields 

        /// <summary>
        /// Репозиторий сущности <see cref="Client"/>
        /// </summary>
        private readonly IClientRepository _clientRepository;

        #endregion

        #region Constructors 

        /// <summary>
        /// Конструктор с полной инициализацией
        /// </summary>
        /// <param name="clientRepository">Репозиторий сущности</param>
        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        #endregion

        #region Interface implementations

        public Task<IEnumerable<IEntityViewModel>> GetAllAsync()
        {
            //TODO РЕАЛИЗОВАТЬ МЕТОД
            throw new NotImplementedException();
        }

        public Task<Guid> RemoveAsync(IEntityViewModel entity)
        {
            //TODO РЕАЛИЗОВАТЬ МЕТОД
            throw new NotImplementedException();
        }

        public Task<IEntityViewModel> SaveAsync(IEntityViewModel entity)
        {
            //TODO РЕАЛИЗОВАТЬ МЕТОД
            throw new NotImplementedException();
        }

        #endregion
    }
}

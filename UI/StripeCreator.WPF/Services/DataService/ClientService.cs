using StripeCreator.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<IEnumerable<IEntityViewModel>> GetAllAsync()
        {
            try
            {
                var storedClients = await _clientRepository.GetAllAsync();
                return storedClients.Select(client => new ClientViewModel(client));
            }
            catch (Exception ex)
            {
                throw new Exception($"При запросе всех клиентов возникла ошибка - {ex.Message}", ex.InnerException);
            }
        }

        public async Task<Guid> RemoveAsync(IEntityViewModel entity)
        {
            var entityId = entity.GetEntityId();
            if (!entityId.HasValue)
                throw new InvalidOperationException("Удаление хранимой сущности без идентификатора");
            try
            {
                var removeId = await _clientRepository.RemoveAsync(entityId.Value);
                return removeId;
            }
            catch (Exception ex)
            {
                throw new Exception($"При запросе удаления клиента возникла ошибка - {ex.Message}", ex.InnerException);
            }
        }

        public async Task<IEntityViewModel> SaveAsync(IEntityViewModel entity)
        {
            if (entity is not ClientViewModel clientViewModel)
                throw new ArgumentException($"Указанная ViewModel не является ViewModel сущности клиента");
            try
            {
                var savedEntity = await _clientRepository.SaveAsync(clientViewModel.Entity);
                return new ClientViewModel(savedEntity);
            }
            catch (Exception ex)
            {
                throw new Exception($"При запросе сохранения клиента возникла ошибка - {ex.Message}", ex.InnerException);
            }
        }

        #endregion
    }
}

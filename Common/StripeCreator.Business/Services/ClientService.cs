using StripeCreator.Business.Models;
using StripeCreator.Business.Repositories;

namespace StripeCreator.Business.Services
{
    public class ClientService
    {
        private readonly IClientRepository _clientRepository;

        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public Task<Client> SaveAsync(Client entity) => _clientRepository.SaveAsync(entity);

        public async Task<Guid> RemoveAsync(Guid id)
        {
            try
            {
                return await _clientRepository.RemoveAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception(
                    "Возможно вы пытаетесь удалить данные клиента, который уже привязан к заказу",
                    ex.InnerException);
            }
        }

        public Task<IEnumerable<Client>> GetAllAsync() => _clientRepository.GetAllAsync();

        public Task<Client?> GetByIdAsync(Guid id) => _clientRepository.GetByIdAsync(id);
    }
}

using StripeCreator.Business.Enums;
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

        public async Task<Client> SaveAsync(Client entity) => await _clientRepository.SaveAsync(entity);

        public async Task<Guid> RemoveAsync(Guid id) => await _clientRepository.RemoveAsync(id);

        public async Task<IEnumerable<Client>> GetAllAsync() => await _clientRepository.GetAllAsync();

        public async Task<Client?> GetByIdAsync(Guid id) => await _clientRepository.GetByIdAsync(id);
    }
}

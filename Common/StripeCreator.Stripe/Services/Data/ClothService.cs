using StripeCreator.Stripe.Models;
using StripeCreator.Stripe.Repositories;

namespace StripeCreator.Stripe.Services
{
    public class ClothService
    {
        private readonly IClothRepository _clothRepository;

        public ClothService(IClothRepository clothRepository)
        {
            _clothRepository = clothRepository;
        }

        public async Task<Cloth> SaveAsync(Cloth entity) => await _clothRepository.SaveAsync(entity);

        public async Task<Guid> RemoveAsync(Guid id) => await _clothRepository.RemoveAsync(id);

        public async Task<IEnumerable<Cloth>> GetAllAsync() => await _clothRepository.GetAllAsync();

        public async Task<Cloth?> GetByIdAsync(Guid id) => await _clothRepository.GetByIdAsync(id);
    }
}

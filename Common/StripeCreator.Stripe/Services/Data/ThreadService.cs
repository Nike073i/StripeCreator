using StripeCreator.Stripe.Repositories;
using Thread = StripeCreator.Stripe.Models.Thread;

namespace StripeCreator.Stripe.Services
{
    public class ThreadService
    {
        private readonly IThreadRepository _threadRepository;

        public ThreadService(IThreadRepository threadRepository)
        {
            _threadRepository = threadRepository;
        }

        public async Task<Thread> SaveAsync(Thread entity) => await _threadRepository.SaveAsync(entity);

        public async Task<Guid> RemoveAsync(Guid id) => await _threadRepository.RemoveAsync(id);

        public async Task<IEnumerable<Thread>> GetAllAsync() => await _threadRepository.GetAllAsync();

        public async Task<Thread?> GetByIdAsync(Guid id) => await _threadRepository.GetByIdAsync(id);
    }
}

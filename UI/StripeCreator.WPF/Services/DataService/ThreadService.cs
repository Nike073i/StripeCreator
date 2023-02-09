using StripeCreator.Stripe.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Thread = StripeCreator.Stripe.Models.Thread;

namespace StripeCreator.WPF
{
    /// <summary>
    /// Сервис для работы с ViewModel сущности <see cref="Thread"/>
    /// </summary>
    public class ThreadService : IDataService
    {
        #region Private fields 

        /// <summary>
        /// Репозиторий сущности <see cref="Thread"/>
        /// </summary>
        private readonly IThreadRepository _threadRepository;

        #endregion

        #region Constructors 

        /// <summary>
        /// Конструктор с полной инициализацией
        /// </summary>
        /// <param name="threadRepository">Репозиторий сущности</param>
        public ThreadService(IThreadRepository threadRepository)
        {
            _threadRepository = threadRepository;
        }

        #endregion

        #region Interface implementations

        public async Task<IEnumerable<IEntityViewModel>> GetAllAsync()
        {
            try
            {
                var storedThreads = await _threadRepository.GetAllAsync();
                return storedThreads.Select(thread => new ThreadViewModel(thread));
            }
            catch (Exception ex)
            {
                throw new Exception($"При запросе всех нитей возникла ошибка - {ex.Message}", ex.InnerException);
            }
        }

        public async Task<Guid> RemoveAsync(IEntityViewModel entity)
        {
            var entityId = entity.GetEntityId();
            if (!entityId.HasValue)
                throw new InvalidOperationException("Удаление хранимой сущности без идентификатора");
            try
            {
                var removeId = await _threadRepository.RemoveAsync(entityId.Value);
                return removeId;
            }
            catch (Exception ex)
            {
                throw new Exception($"При запросе удаления нити возникла ошибка - {ex.Message}", ex.InnerException);
            }
        }

        public async Task<IEntityViewModel> SaveAsync(IEntityViewModel entity)
        {
            if (entity is not ThreadViewModel threadViewModel)
                throw new ArgumentException($"Указанная ViewModel не является ViewModel сущности нити");
            try
            {
                var savedEntity = await _threadRepository.SaveAsync(threadViewModel.Entity);
                return new ThreadViewModel(savedEntity);
            }
            catch (Exception ex)
            {
                throw new Exception($"При запросе сохранения нити возникла ошибка - {ex.Message}", ex.InnerException);
            }
        }

        #endregion
    }
}

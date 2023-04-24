using StripeCreator.Core.Models;
using StripeCreator.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StripeCreator.WPF
{
    /// <summary>
    /// Базовый класс сервиса для работы с ViewModel сущности <see cref="Entity"/>
    /// </summary>
    public abstract class EntityService<TEntity> : IDataService
        where TEntity : Entity
    {
        #region Private fields 

        /// <summary>
        /// Репозиторий сущности <see cref="Entity"/>
        /// </summary>
        private readonly IRepository<TEntity> _repository;

        #endregion

        #region Constructors 

        /// <summary>
        /// Конструктор с полной инициализацией
        /// </summary>
        /// <param name="repository">Репозиторий сущности</param>
        public EntityService(IRepository<TEntity> repository)
        {
            _repository = repository;
        }

        #endregion

        #region Interface implementations

        public async Task<Guid> RemoveAsync(IEntityViewModel entity)
        {
            var entityId = entity.GetEntityId();
            if (!entityId.HasValue)
                throw new InvalidOperationException("Удаление хранимой сущности без идентификатора");
            try
            {
                var removeId = await _repository.RemoveAsync(entityId.Value);
                return removeId;
            }
            catch (Exception ex)
            {
                throw new Exception($"При запросе удаления записи возникла ошибка - {ex.Message}", ex.InnerException);
            }
        }

        public async Task<IEntityViewModel> SaveAsync(IEntityViewModel viewModel)
        {
            var entity = GetEntityFromViewModel(viewModel);
            try
            {
                var savedEntity = await _repository.SaveAsync(entity);
                return CreateViewModel(savedEntity);
            }
            catch (Exception ex)
            {
                throw new Exception($"При запросе сохранения записи возникла ошибка - {ex.Message}", ex.InnerException);
            }
        }

        public abstract EntityFormationViewModel CreateFormationViewModel(IEntityViewModel? entity = null);

        #endregion

        #region Abstract methods

        /// <summary>
        /// Создание ViewModel для доменной сущности
        /// </summary>
        /// <param name="entity">доменная сущность</param>
        /// <returns>ViewModel сущности</returns>
        protected abstract IEntityViewModel CreateViewModel(TEntity entity);

        /// <summary>
        /// Получить доменную модель сущности из ViewModel
        /// </summary>
        /// <param name="viewModel">ViewModel сущности</param>
        /// <returns>Доменная модель</returns>
        protected abstract TEntity GetEntityFromViewModel(IEntityViewModel viewModel);

        #endregion
    }
}

using StripeCreator.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StripeCreator.WPF
{
    /// <summary>
    /// Базовый класс сервиса для работы с ViewModel сущности <see cref="Entity"/>
    /// </summary>
    public abstract class EntityFacade<TEntity> : IDataFacade
    {
        #region Interface implementations

        public async Task<IEnumerable<IEntityViewModel>> GetAllAsync()
        {
            try
            {
                var entities = await GetAllEntities();
                return entities.Select(CreateViewModel);
            }
            catch (Exception ex)
            {
                throw new Exception($"При запросе всех записей возникла ошибка - {ex.Message}", ex.InnerException);
            }
        }

        public async Task<Guid> RemoveAsync(IEntityViewModel entity)
        {
            var entityId = entity.GetEntityId();
            if (!entityId.HasValue)
                throw new InvalidOperationException("Удаление хранимой сущности без идентификатора");
            try
            {
                var removeId = await RemoveAsync(entityId.Value);
                return removeId;
            }
            catch (Exception ex)
            {
                throw new Exception($"При запросе удаления записи возникла ошибка - {ex.Message}", ex.InnerException);
            }
        }

        public async Task<IEntityViewModel?> CreateAsync()
        {
            var formationData = await FormData();
            if (formationData == null)
                return null;
            try
            {
                var savedEntity = await CreateEntityAsync(formationData);
                return CreateViewModel(savedEntity);
            }
            catch (Exception ex)
            {
                throw new Exception($"При запросе сохранения записи возникла ошибка - {ex.Message}", ex.InnerException);
            }
        }

        public async Task<IEntityViewModel?> EditAsync(IEntityViewModel entityViewModel)
        {
            var changedEntity = await FormData(entityViewModel);
            if (changedEntity == null)
                return null;
            try
            {
                var savedEntity = await EditEntityAsync(changedEntity);
                return CreateViewModel(savedEntity);
            }
            catch (Exception ex)
            {
                throw new Exception($"При запросе редактирования записи возникла ошибка - {ex.Message}", ex.InnerException);
            }
        }



        #endregion

        #region Abstract methods

        protected abstract IEntityViewModel CreateViewModel(TEntity entity);
        protected abstract Task<IEnumerable<TEntity>> GetAllEntities();
        protected abstract Task<Guid> RemoveAsync(Guid entityId);
        protected abstract Task<TEntity> CreateEntityAsync(IEntityViewModel entityViewModel);
        protected abstract Task<TEntity> EditEntityAsync(IEntityViewModel entityViewModel);
        protected abstract Task<IEntityViewModel?> FormData(IEntityViewModel? entityViewModel = null);

        #endregion
    }
}

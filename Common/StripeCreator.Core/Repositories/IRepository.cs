using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StripeCreator.Core.Models;

namespace StripeCreator.Core.Repositories
{
    /// <summary>
    /// Базовый интерфейс репозитория для сущности <see cref="Entity"/>
    /// </summary>
    /// <typeparam name="TEntity">Дочерний класс от <see cref="Entity"/></typeparam>
    public interface IRepository<TEntity> where TEntity : Entity
    {
        /// <summary>
        /// Сохранение сущности. 
        /// Если сущность новая, то в хранилище добавляется новая запись
        /// Иначе сущность с таким же идентификатором обновляется новыми значениями
        /// </summary>
        /// <param name="entity">Сохраняемая сущность</param>
        /// <returns>Сохраненная сущность</returns>
        public Task<TEntity> SaveAsync(TEntity entity);

        /// <summary>
        /// Удаление сущности по <paramref name="id"/>
        /// </summary>
        /// <param name="id">Идентификатор удаляемой сущности</param>
        /// <returns>Идентификатор удаленной сущности</returns>
        public Task<Guid> RemoveAsync(Guid id);

        /// <summary>
        /// Получение перечисления всех хранимых сущностей
        /// </summary>
        /// <returns>Перечисление хранимых сущностей</returns>
        public Task<IEnumerable<TEntity>> GetAllAsync();

        /// <summary>
        /// Получение хранимой сущности по <paramref name="id"/>
        /// Если сущность не найдена, возвращается null-значение
        /// </summary>
        /// <param name="id">Идентификатор искомой сущности</param>
        /// <returns>Искомая сущность</returns>
        public Task<TEntity?> GetByIdAsync(Guid id);

        /// <summary>
        /// Получение перечисления хранимых сущностей в количестве <paramref name="take"/> пропустив <paramref name="skip"/> 
        /// </summary>
        /// <param name="skip">Число пропускаемых сущностей</param>
        /// <param name="take">Число получаемых сущностей</param>
        /// <returns>Перечисление хранимых сущностей</returns>
        public Task<IEnumerable<TEntity>> GetAsync(int skip, int take);
    }
}
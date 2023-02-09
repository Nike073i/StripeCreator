using StripeCreator.Core.Models;

namespace StripeCreator.WPF
{
    /// <summary>
    /// Базовый класс ViewModel доменной сущности
    /// </summary>
    /// <typeparam name="TEntity">Тип доменной сущности</typeparam>
    public abstract class EntityViewModel<TEntity> : IEntityViewModel where TEntity : Entity
    {
        #region Protected fields

        /// <summary>
        /// Доменная модель сущности
        /// </summary>
        protected TEntity Entity { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Конструктор с полной инициализацией
        /// </summary>
        /// <param name="entity"></param>
        public EntityViewModel(TEntity entity)
        {
            Entity = entity;
        }

        #endregion

        #region Interface implementations 

        public abstract EntityInfoViewModel GetData { get; }

        #endregion
    }
}

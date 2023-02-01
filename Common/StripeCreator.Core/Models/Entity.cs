namespace StripeCreator.Core.Models
{
    /// <summary>
    /// Базовый класс сущности
    /// </summary>
    public abstract class Entity
    {
        #region Public properties 

        /// <summary>
        /// Уникальный идентификатор сущности. Если сущность новая, то <paramref name="Id"/> равен null
        /// </summary>
        public Guid? Id { get; protected set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        /// <param name="id">Идентификатор сущности</param>
        public Entity(Guid? id = null) => Id = id;

        #endregion
    }
}
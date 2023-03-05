namespace StripeCreator.VK.Models
{
    /// <summary>Информация о категории</summary>
    public class Category
    {
        #region Public properties

        /// <summary>Идентификатор категории</summary>
        public long Id { get; }

        /// <summary>Название категории</summary>
        public string Name { get; }

        #endregion

        #region Constructors

        /// <summary>Конструктор с полной инициализацией</summary>
        /// <param name="id">Идентификатор категории</param>
        /// <param name="name">Название категории</param>
        public Category(long id, string name)
        {
            Id = id;
            Name = name;
        }

        #endregion
    }
}
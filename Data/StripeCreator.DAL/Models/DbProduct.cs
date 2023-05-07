using System.ComponentModel.DataAnnotations;

namespace StripeCreator.DAL.Models
{
    /// <summary>
    /// Хранимая сущность продукта
    /// </summary>
    public class DbProduct : DbEntity
    {
        #region Public properties

        /// <summary>
        /// Название продукта
        /// </summary>
        [Required]
        public string Name { get; set; } = "Название";

        /// <summary>
        /// Цена продукта
        /// </summary>
        [Required]
        public decimal Price { get; set; }

        /// <summary>
        /// Описание продукта
        /// </summary>
        [Required]
        public string Description { get; set; } = "Описание";

        #endregion

        #region Constructors

#nullable disable
        /// <summary>
        /// Конструктор по умолчанию для EFC
        /// </summary>
        protected DbProduct() { }

        /// <summary>
        /// Конструктор с полной инициализацией
        /// </summary>
        /// <param name="name">Название</param>
        /// <param name="price">Цена</param>
        /// <param name="description">Описание</param>
        /// <param name="id">Идентификатор сущности</param>
        public DbProduct(string name, decimal price, string description, Guid? id = null) : base(id)
        {
            Name = name;
            Price = price;
            Description = description;
        }
#nullable enable

        #endregion
    }
}
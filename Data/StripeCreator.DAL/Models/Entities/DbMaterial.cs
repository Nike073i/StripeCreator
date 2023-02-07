using System.ComponentModel.DataAnnotations;
using StripeCreator.Core.Models;

namespace StripeCreator.DAL.Models
{
    /// <summary>
    /// Базовый класс хранимого материала
    /// </summary>
    public abstract class DbMaterial : DbEntity
    {
        #region Public properties

        /// <summary>
        /// Название материала
        /// </summary>
        [Required]
        public string Name { get; set; } = "Материал";

        /// <summary>
        /// Стоимость материала
        /// </summary>
        [Required]
        public decimal Price { get; set; }

        /// <summary>
        /// Название прозводителя
        /// </summary>
        [Required]
        public string Manufacturer { get; set; } = "Производитель";

        /// <summary>
        /// Код цвета материала
        /// </summary>
        [Required]
        public string ColorHex { get; set; } = Color.DefaultColor;

        #endregion

        #region Constructors

        /// <summary>
        /// Конструктор по умолчанию для EFC
        /// </summary>
        protected DbMaterial() { }

        /// <summary>
        /// Конструктор с полной инициализацией
        /// </summary>
        /// <param name="name">Название материала</param>
        /// <param name="price">Стоимость материала</param>
        /// <param name="manufacturer">Производитель материала</param>
        /// <param name="colorHex">Код цвета материала</param>
        public DbMaterial(string name, decimal price, string manufacturer, string colorHex, Guid? id = null) : base(id)
        {
            Name = name;
            Price = price;
            Manufacturer = manufacturer;
            ColorHex = colorHex;
        }

        #endregion
    }
}
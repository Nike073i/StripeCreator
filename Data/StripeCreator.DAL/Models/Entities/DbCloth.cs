using System.ComponentModel.DataAnnotations;
using StripeCreator.Stripe.Models;

namespace StripeCreator.DAL.Models
{
    /// <summary>
    /// Хранимая сущность ткани
    /// </summary>
    public class DbCloth : DbMaterial
    {
        #region Public properties

        /// <summary>
        /// "Каунт" ткани. Количество клеток в 10 см.
        /// </summary>
        [Required]
        public int Count { get; set; }

        /// <summary>
        /// Тип ткани
        /// </summary>
        [Required]
        public ClothType Type { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Конструктор по умолчанию для EFC
        /// </summary>
        protected DbCloth() { }

        /// <summary>
        /// Конструктор с полной инициализацией
        /// </summary>
        /// <param name="name">Название ткани</param>
        /// <param name="price">Стоимость ткани</param>
        /// <param name="manufacturer">Производитель ткани</param>
        /// <param name="colorHex">Код цвета ткани</param>
        /// <param name="type">Тип ткани</param>
        /// <param name="count">"Каунт" ткани</param>
        /// <param name="id">Идентификатор ткани</param>
        public DbCloth(string name, decimal price, string manufacturer, string colorHex, ClothType type, int count, Guid? id = null)
            : base(name, price, manufacturer, colorHex, id)
        {
            Type = type;
            Count = count;
        }

        #endregion
    }
}
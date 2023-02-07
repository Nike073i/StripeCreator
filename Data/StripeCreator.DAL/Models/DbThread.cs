using System.ComponentModel.DataAnnotations;
using StripeCreator.Stripe.Models;

namespace StripeCreator.DAL.Models
{
    /// <summary>
    /// Хранимая сущность нити
    /// </summary>
    public class DbThread : DbMaterial
    {
        #region Public properties

        /// <summary>
        /// Вес нити
        /// </summary>
        [Required]
        public int Weight { get; set; }

        /// <summary>
        /// Тип нити
        /// </summary>
        [Required]
        public ThreadType Type { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Конструктор по умолчанию для EFC
        /// </summary>
        protected DbThread() { }

        /// <summary>
        /// Конструктор с полной инициализацией
        /// </summary>
        /// <param name="name">Название нити</param>
        /// <param name="price">Стоимость нити</param>
        /// <param name="manufacturer">Производитель нити</param>
        /// <param name="colorHex">Код цвета нити</param>
        /// <param name="type">Тип нити</param>
        /// <param name="weight">Вес нити</param>
        /// <param name="id">Идентификатор нити</param>
        public DbThread(string name, decimal price, string manufacturer, string colorHex, ThreadType type, int weight, Guid? id = null)
            : base(name, price, manufacturer, colorHex, id)
        {
            Type = type;
            Weight = weight;
        }

        #endregion
    }
}
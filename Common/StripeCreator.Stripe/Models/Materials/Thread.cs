using StripeCreator.Core.Models;

namespace StripeCreator.Stripe.Models
{
    /// <summary>
    /// Класс сущности нити
    /// </summary>
    public class Thread : Material
    {
        #region Private fields

        /// <summary>
        /// Вес нити
        /// </summary>
        private int _weight;

        #endregion
        #region Public properties

        /// <summary>
        /// Тип нити
        /// </summary>
        public ThreadType Type { get; set; }

        /// <summary>
        /// Вес нити
        /// </summary>
        public int Weight
        {
            get => _weight;
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException(nameof(Weight), "Вес нити не может быть <= 0");
                _weight = value;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Конструктор с полной инициализацией
        /// </summary>
        /// <param name="name">Название нити</param>
        /// <param name="price">Стоимость нити</param>
        /// <param name="manufacturer">Производитель нити</param>
        /// <param name="color">Цвет нити</param>
        /// <param name="type">Тип нити</param>
        /// <param name="weight">Вес нити</param>
        /// <param name="id">Идентификатор нити</param>
        public Thread(string name, decimal price, string manufacturer, Color color, ThreadType type, int weight, Guid? id = null)
            : base(name, price, manufacturer, color, id)
        {
            Type = type;
            Weight = weight;
        }

        #endregion
    }
}
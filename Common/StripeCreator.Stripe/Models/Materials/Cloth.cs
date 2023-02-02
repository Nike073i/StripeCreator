using StripeCreator.Core.Models;

namespace StripeCreator.Stripe.Models
{
    /// <summary>
    /// Класс сущности ткани
    /// </summary>
    public class Cloth : Material
    {
        #region Private fields

        /// <summary>
        /// "Каунт" ткани. Количество клеток в 10 см.
        /// </summary>
        private int _count;

        #endregion
        #region Public properties

        /// <summary>
        /// Тип ткани
        /// </summary>
        public ClothType Type { get; set; }

        /// <summary>
        /// "Каунт" ткани. Количество клеток в 10 см.
        /// </summary>
        public int Count
        {
            get => _count;
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException(nameof(Count), "Каунт ткани не может быть <= 0");
                _count = value;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Конструктор с полной инициализацией
        /// </summary>
        /// <param name="name">Название ткани</param>
        /// <param name="price">Стоимость ткани</param>
        /// <param name="manufacturer">Производитель ткани</param>
        /// <param name="color">Цвет ткани</param>
        /// <param name="type">Тип ткани</param>
        /// <param name="count">"Каунт" ткани</param>
        /// <param name="id">Идентификатор ткани</param>
        public Cloth(string name, decimal price, string manufacturer, Color color, ClothType type, int count, Guid? id = null)
            : base(name, price, manufacturer, color, id)
        {
            Type = type;
            Count = count;
        }

        #endregion
    }
}
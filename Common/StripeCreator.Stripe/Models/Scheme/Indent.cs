using StripeCreator.Core.Models;

namespace StripeCreator.Stripe.Models
{
    /// <summary>
    /// Отступ схемы
    /// </summary>
    public class Indent
    {
        #region Private fields

        /// <summary>
        /// Размер отступа в клетках
        /// </summary>
        private int _size;

        #endregion

        #region Public properties

        /// <summary>
        /// Цвет отступа
        /// </summary>
        public Color Color { get; set; }

        /// <summary>
        /// Размер отступа в клетках
        /// </summary>
        public int Size
        {
            get => _size;
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException(nameof(Size), "Размер отступа в клетках не может быть <= 0");
                _size = value;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Конструктор с полной инициализацией
        /// </summary>
        /// <param name="size">Размер отступа в клетках</param>
        /// <param name="color">Цвет отступа</param>
        public Indent(int size, Color? color = null)
        {
            Size = size;
            Color = color ?? new Color();
        }

        #endregion
    }
}

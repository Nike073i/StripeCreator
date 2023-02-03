using StripeCreator.Core.Models;

namespace StripeCreator.Stripe.Models
{
    /// <summary>
    /// Сетка схемы
    /// </summary>
    public class Grid
    {
        #region Private fields

        /// <summary>
        /// Размер сетки в px
        /// </summary>
        private int _size;

        #endregion

        #region Public properties

        /// <summary>
        /// Цвет сетки
        /// </summary>
        public Color Color { get; set; }

        /// <summary>
        /// Размер сетки в px
        /// </summary>
        public int Size
        {
            get => _size;
            set
            {
                if (value <= 0) 
                    throw new ArgumentOutOfRangeException(nameof(Size), "Размер сетки не может быть <= 0");
                _size = value;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Конструктор с полной инициализацией
        /// </summary>
        /// <param name="size">Размер сетки в px</param>
        /// <param name="color">Цвет сетки</param>
        public Grid(int size, Color? color = null)
        {
            Size = size;
            Color = color ?? new Color();
        }

        #endregion
    }
}

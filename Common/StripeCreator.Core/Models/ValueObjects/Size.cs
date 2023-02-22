namespace StripeCreator.Core.Models
{
    /// <summary>
    /// Данные по размеру
    /// </summary>
    public class Size
    {
        #region Public properties

        /// <summary>
        /// Ширина
        /// </summary>
        public int Width { get; }

        /// <summary>
        /// Высота
        /// </summary>
        public int Height { get; }

        #endregion

        #region Contstuctors

        /// <summary>
        /// Конструктор с полной инициализацией
        /// </summary>
        /// <param name="width">Ширина</param>
        /// <param name="height">Высота</param>
        public Size(int width, int height)
        {
            if (width <= 0)
                throw new ArgumentOutOfRangeException(nameof(width), "Ширина не может быть <= 0");
            if (width <= 0)
                throw new ArgumentOutOfRangeException(nameof(height), "Высота не может быть <= 0");

            Width = width;
            Height = height;
        }

        #endregion

        #region Overrides

        public override string ToString() => $"{Width}x{Height}";

        #endregion
    }
}

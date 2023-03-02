using StripeCreator.Core.Models;

namespace StripeCreator.Stripe.Models
{
    /// <summary>
    /// Смета материалов для изготовления вышивки
    /// </summary>
    public class SchemeMaterialEstimate
    {
        #region Public properties

        /// <summary>
        /// Ширина ткани в метрах
        /// </summary>
        public double ClothWidth { get; }

        /// <summary>
        /// Высота ткани в метрах
        /// </summary>
        public double ClothHeight { get; }

        /// <summary>
        /// Перечень длин нитей по цветам
        /// </summary>
        public Dictionary<Color, double> ColorLengths { get; }

        #endregion

        #region Constructors

        /// <summary>
        /// Конструктор с полной инициализацией
        /// </summary>
        /// <param name="clothWidth">Ширина ткани</param>
        /// <param name="clothHeight">Высота ткани</param>
        /// <param name="colorLengths">Перечень длин нитей</param>
        public SchemeMaterialEstimate(double clothWidth, double clothHeight, Dictionary<Color, double> colorLengths)
        {
            ClothWidth = clothWidth;
            ClothHeight = clothHeight;
            ColorLengths = colorLengths;
        }

        #endregion
    }
}

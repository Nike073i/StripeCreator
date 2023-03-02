using StripeCreator.Core.Models;
using StripeCreator.Stripe.Models;

namespace StripeCreator.Stripe.Services
{
    /// <summary>
    /// Сервис расчета количества материалов
    /// </summary>
    public class SchemeMaterialCalculator
    {
        #region Constants 

        /// <summary>
        /// Длина стежка в метрах
        /// </summary>
        public static readonly double StitchLength = 0.005;

        #endregion

        #region Public methods

        /// <summary>
        /// Расчет материалов для вышивки
        /// </summary>
        /// <param name="method">Метод вышивки</param>
        /// <param name="type">Тип вышивки</param>
        /// <param name="scheme">Схема вышивки</param>
        /// <returns>Смета материалов</returns>
        /// <exception cref="ArgumentException">Возникает, если указанный тип вышивки не поддерживается</exception>
        public SchemeMaterialEstimate Calculate(EmbroideryMethod method, EmbroideryType type, Scheme scheme)
        {
            var cellsInCm = (double)scheme.TargetClothCount / 10;
            double clothWidth = scheme.Width / cellsInCm / 100; // в метрах
            double clothHeight = scheme.Height / cellsInCm / 100; // в метрах
            var colorStatistic = scheme.GetColorsStatistic();
            double stichFactor = type switch
            {
                EmbroideryType.Cross => 3,
                EmbroideryType.SmoothDiagonal => 2,
                EmbroideryType.SmoothHorizontal => 2,
                EmbroideryType.SmoothVertical => 2,
                _ => throw new ArgumentException("Указанный тип вышивки не поддерживается"),
            };
            stichFactor = method == EmbroideryMethod.In1Thread ? stichFactor : stichFactor * 2;

            var threadLengths = new Dictionary<Color, double>();
            foreach (var color in colorStatistic)
                threadLengths.Add(color.Key, color.Value * stichFactor * StitchLength);

            return new SchemeMaterialEstimate(clothWidth, clothHeight, threadLengths);
        }

        #endregion
    }
}

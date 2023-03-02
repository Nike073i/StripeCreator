using StripeCreator.Core.Models;
using StripeCreator.Stripe.Models;

namespace StripeCreator.Stripe.Services
{
    /// <summary>
    /// Сервис расчета стоимости вышивкима
    /// </summary>
    public class SchemePriceCalculator
    {
        #region Private fields

        /// <summary>
        /// Сервис расчета количества материалов
        /// </summary>
        private readonly SchemeMaterialCalculator _materialCalculator;

        #endregion

        #region Constructors

        /// <summary>
        /// Конструктор с полной инициализацией
        /// </summary>
        /// <param name="materialCalculator">Сервис расчета материалов</param>
        public SchemePriceCalculator(SchemeMaterialCalculator materialCalculator)
        {
            _materialCalculator = materialCalculator;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Расчет стоимости вышивки
        /// </summary>
        /// <param name="method">Метод вышивки</param>
        /// <param name="type">Тип вышивки</param>
        /// <param name="scheme">Схема вышивки</param>
        /// <param name="threads">Используемые нити</param>
        /// <param name="clothCost">Стоимость ткани</param>
        /// <returns>Стоимость вышивки</returns>
        /// <exception cref="ArgumentException">Возникает, если в <paramref name="threads"/> не найдена нить с необходимым цветом</exception>
        public decimal Calculate(EmbroideryMethod method, EmbroideryType type, Scheme scheme, Dictionary<Color, decimal> threads, decimal clothCost)
        {
            var estimate = _materialCalculator.Calculate(method, type, scheme);
            double clothSquare = estimate.ClothWidth * estimate.ClothHeight;
            decimal clothPrice = clothCost * Convert.ToDecimal(clothSquare);

            var colorLengths = estimate.ColorLengths;
            var threadPrice = colorLengths.Sum(colorLength =>
            {
                if (!threads.TryGetValue(colorLength.Key, out var threadCost))
                    throw new ArgumentException($"Не найдена нить с цветом {colorLength.Key}");
                var price = threadCost * Convert.ToDecimal(colorLength.Value);
                return price;
            });
            return clothPrice + threadPrice;
        }

        #endregion
    }
}

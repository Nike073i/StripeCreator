namespace StripeCreator.Core.Models
{
    /// <summary>
    /// Класс представления стоимости
    /// </summary>
    public class Price
    {
        #region Private fields

        /// <summary>
        /// Минимально допустимое значение стоимости
        /// </summary>
        private decimal _minValue = 0m;

        /// <summary>
        /// Максимально допустимое значение стоимости
        /// </summary>
        private decimal _maxValue = 999_999_999m;

        /// <summary>
        /// Стоимость
        /// </summary>
        private decimal _value = 0m;

        #endregion

        #region Public properties

        /// <summary>
        /// Стоимость
        /// </summary>
        public decimal Value
        {
            get => _value;
            set
            {
                if (value < MinValue || value > MaxValue)
                    throw new ArgumentOutOfRangeException(nameof(Value), $"Стоимость не может быть < {MinValue} и > {MaxValue}");
                _value = value;
            }
        }

        /// <summary>
        /// Минимально допустимое значение стоимости
        /// </summary>
        public decimal MinValue
        {
            get => _minValue;
            set
            {
                if (value > MaxValue || value > Value)
                    throw new ArgumentOutOfRangeException(nameof(MinValue),
                    $"Минимальное значение стоимости не может быть больше максимального значения {MaxValue} и текущего значения {Value}");
                _minValue = value;
            }
        }

        /// <summary>
        /// Максимально допустимое значение стоимости
        /// </summary>
        public decimal MaxValue
        {
            get => _maxValue;
            set
            {
                if (value < MinValue || value < Value)
                    throw new ArgumentOutOfRangeException(nameof(MaxValue),
                    $"Минимальное значение стоимости не может быть меньше минимального значения {MinValue} и текущего значения {Value}");
                _maxValue = value;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public Price() { }

        /// <summary>
        /// Конструктор с инициализацией значения стоимости
        /// </summary>
        /// <param name="value">Значение стоимости</param>
        public Price(decimal value)
        {
            Value = value;
        }

        /// <summary>
        /// Конструктор с полной инициализацией
        /// </summary>
        /// <param name="minValue">Минимально допустимое значение стоимости</param>
        /// <param name="maxValue">Максимально допустимое значение стоимости</param>
        /// <param name="value">Значение стоимости</param>
        public Price(decimal minValue, decimal maxValue, decimal value)
        {
            MinValue = minValue;
            MaxValue = maxValue;
            Value = value;
        }

        #endregion

        #region Operator overloads

        /// <summary>
        /// Оператор сложения двух стоимостей
        /// </summary>
        /// <param name="a">1-ое слагаемое</param>
        /// <param name="b">2-ое слагаемое</param>
        /// <returns>Новая стоимость</returns>
        public static Price operator +(Price a, Price b) => new Price(a.MinValue, a.MaxValue, a.Value + b.Value);

        /// <summary>
        /// Оператор вычитания двух стоимостей
        /// </summary>
        /// <param name="a">Уменьшаемое</param>
        /// <param name="b">Вычитаемое</param>
        /// <returns>Новая стоимость</returns>
        public static Price operator -(Price a, Price b) => new Price(a.MinValue, a.MaxValue, a.Value - b.Value);

        #endregion
    }
}
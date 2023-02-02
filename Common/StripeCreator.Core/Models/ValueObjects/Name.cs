namespace StripeCreator.Core.Models
{
    /// <summary>
    /// Класс представления названия
    /// </summary>
    public class Name
    {
        #region Private fields

        /// <summary>
        /// Минимально допустимая длина
        /// </summary>
        private int _minLength = 1;

        /// <summary>
        /// Максимально допустимая длина
        /// </summary>
        private int _maxLength = 255;

        /// <summary>
        /// Название
        /// </summary>
        private string _value = "Название";

        #endregion

        #region Public properties

        /// <summary>
        /// Название
        /// </summary>
        public string Value
        {
            get => _value;
            set
            {
                if (value.Length < MinLength || value.Length > MaxLength)
                    throw new ArgumentOutOfRangeException(nameof(Value), $"Длина не может быть < {MinLength} и > {MaxLength}");
                _value = value;
            }
        }

        /// <summary>
        /// Минимально допустимая длина
        /// </summary>
        public int MinLength
        {
            get => _minLength;
            set
            {
                if (value > MaxLength || value > Value.Length)
                    throw new ArgumentOutOfRangeException(nameof(MinLength),
                    $"Минимальная длина не может быть больше максимальной {MaxLength} и текущей {Value.Length}");
                _minLength = value;
            }
        }

        /// <summary>
        /// Максимально допустимая длина
        /// </summary>
        public int MaxLength
        {
            get => _maxLength;
            set
            {
                if (value < MinLength || value < Value.Length)
                    throw new ArgumentOutOfRangeException(nameof(MaxLength),
                    $"Минимальная длина не может быть меньше минимальной {MinLength} и текущей {Value.Length}");
                _maxLength = value;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public Name() { }

        /// <summary>
        /// Конструктор с инициализацией названия
        /// </summary>
        /// <param name="value">Название</param>
        public Name(string value)
        {
            Value = value;
        }

        /// <summary>
        /// Конструктор с полной инициализацией
        /// </summary>
        /// <param name="minLength">Минимально допустимая длина</param>
        /// <param name="maxLength">Максимально допустимая длина</param>
        /// <param name="value">Название</param>
        public Name(int minLength, int maxLength, string value)
        {
            MinLength = minLength;
            MaxLength = maxLength;
            Value = value;
        }

        #endregion
    }
}
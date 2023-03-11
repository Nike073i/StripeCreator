using System.Text.RegularExpressions;

namespace StripeCreator.Core.Models
{
    /// <summary>
    /// Класс представления цвета
    /// </summary>
    public class Color
    {
        #region Constants

        /// <summary>
        /// Паттерн кода цвета. 
        /// Допускает значения RGB и ARGB в Hex-формате
        /// </summary>
        private static readonly string ColorPattern = @"^#([A-Fa-f0-9]{6}|[A-Fa-f0-9]{8})$";

        /// <summary>
        /// Цвет по умолчанию
        /// </summary>
        public static readonly string DefaultColor = "#FFFFFF";

        #endregion

        #region Private fields

        /// <summary>
        /// Представление цвета в hex-формате
        /// </summary>
        private string _colorHex = DefaultColor;

        #endregion

        #region Public properties

        /// <summary>
        /// Представление цвета в hex-формате
        /// </summary>
        public string HexValue
        {
            get => _colorHex;
            set => _colorHex = Regex.IsMatch(value, ColorPattern) ? value : DefaultColor;
        }

        /// <summary>
        /// Красный канал в битах
        /// </summary>
        public byte Red => _colorHex.Length == 9 ?
            Convert.ToByte(HexValue.Substring(3, 2), 16) :
            Convert.ToByte(HexValue.Substring(1, 2), 16);

        /// <summary>
        /// Зеленый канал в битах
        /// </summary>
        public byte Green => _colorHex.Length == 9 ?
            Convert.ToByte(HexValue.Substring(5, 2), 16) :
            Convert.ToByte(HexValue.Substring(3, 2), 16);

        /// <summary>
        /// Синий канал в битах
        /// </summary>
        public byte Blue => _colorHex.Length == 9 ?
            Convert.ToByte(HexValue.Substring(7, 2), 16) :
            Convert.ToByte(HexValue.Substring(5, 2), 16);

        /// <summary>
        /// Альфа канал в битах
        /// </summary>
        public byte Alpha => _colorHex.Length == 9 ? Convert.ToByte(HexValue.Substring(1, 2), 16) : (byte)255;

        #endregion

        #region Constructors

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public Color() { }

        /// <summary>
        /// Конструктор с инициализацией цвета
        /// </summary>
        /// <param name="colorHex">Цвет в hex-формате</param>
        public Color(string colorHex)
        {
            HexValue = colorHex;
        }

        #endregion

        #region Override object methods

        public override bool Equals(object? obj) => (obj is Color other) && Equals(other);

        public bool Equals(Color other) =>
            other != null && _colorHex.Equals(other.HexValue, StringComparison.OrdinalIgnoreCase);

        public override int GetHashCode() => _colorHex.GetHashCode();

        public override string ToString() => HexValue;

        #endregion
    }
}
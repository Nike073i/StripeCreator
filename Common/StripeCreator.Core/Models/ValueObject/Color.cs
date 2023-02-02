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
    }
}
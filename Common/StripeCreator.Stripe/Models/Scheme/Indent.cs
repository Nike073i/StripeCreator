using StripeCreator.Core.Models;
using System.Runtime.Serialization;

namespace StripeCreator.Stripe.Models
{
    /// <summary>
    /// Отступ схемы
    /// </summary>
    [Serializable]
    public class Indent : ISerializable
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

        /// <summary>
        /// Приватный конструктор для десериализации объекта
        /// </summary>
        /// <param name="info">Данные сериализованного объекта</param>
        /// <param name="context">Источник потока сериализованного объекта</param>
        /// <exception cref="SerializationException">Возникает, если нет возможности десериализовать цвет отступа</exception>
        private Indent(SerializationInfo info, StreamingContext context)
        {
            var size = info.GetInt32(nameof(Size));
            var colorHex = info.GetString(nameof(Color)) ?? throw new SerializationException("Ошибка сериализации цвета отступа");
            Size = size;
            Color = new Color(colorHex);
        }

        #endregion

        #region Interface implemetations

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(Size), Size);
            info.AddValue(nameof(Color), Color.HexValue);
        }

        #endregion
    }
}

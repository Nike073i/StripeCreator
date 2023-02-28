using ImageMagick;
using StripeCreator.Core.Models;
using StripeCreator.Stripe.Extensions;
using System.Runtime.Serialization;

namespace StripeCreator.Stripe.Models
{
    /// <summary>
    /// Cхема вышивки
    /// </summary>
    [Serializable]
    public class Scheme : ISerializable, IDisposable
    {
        #region Private fields

        /// <summary>
        /// Обработчик заготовки схемы
        /// </summary>
        private readonly MagickImage _magickImage;

        #endregion

        #region Public properties 

        /// <summary>
        /// Ширина схемы в клетках
        /// </summary>
        public int Width => _magickImage.Width;

        /// <summary>
        /// Высота схемы в клетках
        /// </summary>
        public int Height => _magickImage.Height;

        /// <summary>
        /// Заготовка схемы вышивки
        /// </summary>
        public Image SchemeTemplate => _magickImage.CreateImage();

        /// <summary>
        /// Сетка для схемы. 
        /// Null-значение означает, что сетка не применяется
        /// </summary>
        public Grid? Grid { get; set; }

        /// <summary>
        /// Отступ для схемы. 
        /// Null-значение означает, что отступ не применяется
        /// </summary>
        public Indent? Indent { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Конструктор с полной инициализацией
        /// </summary>
        /// <param name="schemeTemplate">Заготовка схемы вышивки</param>
        public Scheme(Image schemeTemplate)
        {
            _magickImage = new MagickImage(schemeTemplate.Data);
        }

        /// <summary>
        /// Приватный конструктор для десериализации объекта
        /// </summary>
        /// <param name="info">Данные сериализованного объекта</param>
        /// <param name="context">Источник потока сериализованного объекта</param>
        /// <exception cref="SerializationException">Возникает, если нет возможности десериализовать массив клеток</exception>
        private Scheme(SerializationInfo info, StreamingContext context)
        {
            var dataInfo = info.GetValue("data", typeof(byte[]));
            if (dataInfo is not byte[] data)
                throw new SerializationException("Ошибка сериализации данных схемы");
            _magickImage = new MagickImage(data);
            var gridInfo = info.GetValue(nameof(Grid), typeof(Grid));
            Grid = gridInfo is Grid grid ? grid : null;
            var indentInfo = info.GetValue(nameof(Indent), typeof(Indent));
            Indent = indentInfo is Indent indent ? indent : null;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Установка нового цвета по позиции
        /// </summary>
        /// <param name="color">Цвет клетки</param>
        /// <param name="position">Позиция клетки в схеме</param>
        /// <exception cref="ArgumentOutOfRangeException">Возникает, если указанная позиция выходит за границы схемы</exception>
        public void SetColor(Color color, PointPosition position)
        {
            if (position.X >= Width || position.Y >= Height)
                throw new ArgumentOutOfRangeException(nameof(position));
            var drawables = new Drawables();
            drawables.FilledPoint(position.X, position.Y, MagickImageExtensions.CreateColor(color));
            _magickImage.Draw(drawables);
        }

        /// <summary>
        /// Замена цвета на новый
        /// </summary>
        /// <param name="currentColor">Текущий цвет</param>
        /// <param name="newColor">Новый цвет</param>
        public void ChangeColor(Color currentColor, Color newColor)
        {
            if (currentColor.Equals(newColor)) return;
            _magickImage.Opaque(MagickImageExtensions.CreateColor(currentColor), MagickImageExtensions.CreateColor(newColor));
        }

        /// <summary>
        /// Получение цвета клетки по позиции
        /// </summary>
        /// <param name="position">Позиция клетки</param>
        /// <returns>Цвет клетки</returns>
        /// <exception cref="ArgumentOutOfRangeException">Возникает, если указанная позиция выходит за границы схемы</exception>
        public Color GetColor(PointPosition position)
        {
            if (position.X >= Width || position.Y >= Height)
                throw new ArgumentOutOfRangeException(nameof(position));
            using var pixel = _magickImage.Clone(position.X, position.Y, 1, 1);
            var magickColor = pixel.Histogram().First().Key;
            return new Color(magickColor.ToHexString());
        }

        /// <summary>
        /// Последовательность всех использованных цветов в схеме
        /// </summary>
        public IEnumerable<Color> GetColors() =>
            _magickImage.Histogram().Select(x => new Color(x.Key.ToHexString()));

        #endregion

        #region Interface implementations

        /// <summary>
        /// Получение данных объекта для сериализации. Реализация <see cref="ISerializable"/>
        /// </summary>
        /// <param name="info">Данные сериализованного объекта</param>
        /// <param name="context">Источник потока сериализованного объекта</param>
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("data", SchemeTemplate.Data);
            info.AddValue(nameof(Grid), Grid);
            info.AddValue(nameof(Indent), Indent);
        }

        public void Dispose() => _magickImage.Dispose();

        #endregion
    }
}

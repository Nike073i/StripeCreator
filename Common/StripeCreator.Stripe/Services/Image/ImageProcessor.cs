using ImageMagick;
using StripeCreator.Core.Models;
using StripeCreator.Stripe.Extensions;
using StripeCreator.Stripe.Models;

namespace StripeCreator.Stripe.Services
{
    /// <summary>
    /// Обработчик изображений
    /// </summary>
    public class ImageProccesor : IDisposable
    {
        #region Private fields

        private readonly MagickImage _imageMagick;

        #endregion

        #region Public properties

        /// <summary>
        /// Данные изображения
        /// </summary>
        public Image Image => _imageMagick.CreateImage();

        #endregion

        #region Constructors

        /// <summary>
        /// Конструктор с полной инициализацией
        /// </summary>
        /// <param name="image">Исходные данные изображения</param>
        public ImageProccesor(Image image)
        {
            _imageMagick = new MagickImage(image.Data);
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Обрезка шума изображения по краям
        /// </summary>
        public void Trim() => _imageMagick.Trim();

        /// <summary>
        /// Масштабирование изображения путем репликации
        /// </summary>
        /// <param name="newSize">Новая размер изображения</param>
        public void Sample(Size newSize) => _imageMagick.Sample(newSize.Width, newSize.Height);

        /// <summary>
        /// Масштабирование изображения методом "Резьба по шву"
        /// </summary>
        /// <param name="newSize">Новая размер изображения</param>
        public void LiquidRescale(Size newSize) => _imageMagick.LiquidRescale(newSize.Width, newSize.Height);

        /// <summary>
        /// Масштабирование изображение с усреднением пикселя
        /// </summary>
        /// <param name="newSize">Новая размер изображения</param>
        public void Scale(Size newSize) => _imageMagick.Scale(newSize.Width, newSize.Height);

        /// <summary>
        /// Адаптивное изменение размера изображения с данными триангуляции.
        /// </summary>
        /// <param name="newSize">Новая размер изображения</param>
        public void AdaptiveResize(Size newSize) => _imageMagick.AdaptiveResize(newSize.Width, newSize.Height);

        /// <summary>
        /// Квантование цветов
        /// </summary>
        /// <param name="colors">Новое количество цветов</param>
        /// <exception cref="ArgumentOutOfRangeException">Возникает, если указано некорректное количество цветов</exception>
        public void Quantize(int colors)
        {
            if (colors < 1)
                throw new ArgumentOutOfRangeException(nameof(colors), "Количество цветов в изображении не может быть < 1");
            var settings = new QuantizeSettings
            {
                Colors = colors,
            };
            _imageMagick.Quantize(settings);
        }

        /// <summary>
        /// Уменьшение количества уровней в каждом цветовом канале
        /// </summary>
        /// <param name="levels">Новое количество уровне в каждом канале</param>
        /// <exception cref="ArgumentOutOfRangeException">Возникает, если указано некорреткное значение количества уровней</exception>
        public void Posterize(int levels)
        {
            if (levels < 1 || levels > 256)
                throw new ArgumentOutOfRangeException(nameof(levels), "Количество уровней в каждом канале не может быть < 1 и > 256");
            _imageMagick.Posterize(levels);
        }

        /// <summary>
        /// Нормальное распределение цветовой гаммы
        /// </summary>
        public void Normalize() => _imageMagick.Normalize();

        #endregion

        #region Interface implementations 

        /// <summary>
        /// Реализация интерфейса <see cref="IDisposable"/>
        /// </summary>
        public void Dispose() => _imageMagick.Dispose();

        #endregion
    }
}
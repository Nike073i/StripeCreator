using ImageMagick;
using StripeCreator.Stripe.Models;

namespace StripeCreator.Stripe.Services
{
    /// <summary>
    /// Обработчик изображений
    /// </summary>
    public class ImageProccesor : IDisposable
    {
        #region Private fields

        private MagickImage imageMagick;

        #endregion

        #region Public properties

        /// <summary>
        /// Данные изображения
        /// </summary>
        public Image Image => new Image(data: imageMagick.ToByteArray(), imageMagick.Width, imageMagick.Height);

        #endregion

        #region Constructors

        /// <summary>
        /// Конструктор с полной инициализацией
        /// </summary>
        /// <param name="image">Исходные данные изображения</param>
        public ImageProccesor(Image image)
        {
            imageMagick = new MagickImage(image.Data);
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Обрезка шума изображения по краям
        /// </summary>
        public void Trim() => imageMagick.Trim();

        /// <summary>
        /// Масштабирование изображения без сглаживаний
        /// </summary>
        /// <param name="newWidth">Новая ширина</param>
        /// <param name="newHeight">Новая высота</param>
        /// <exception cref="ArgumentOutOfRangeException">Возникает, если новые размеры изображения указаны неверно</exception>
        public void Scale(int newWidth, int newHeight)
        {
            if (newWidth < 1)
                throw new ArgumentOutOfRangeException(nameof(newWidth), "Новая ширина изображения не может быть < 1");
            if (newHeight < 1)
                throw new ArgumentOutOfRangeException(nameof(newHeight), "Новая высота изображения не может быть < 1");
            imageMagick.Sample(newWidth, newHeight);
        }

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
            imageMagick.Quantize(settings);
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
            imageMagick.Posterize(levels);
        }

        /// <summary>
        /// Нормальное распределение цветовой гаммы
        /// </summary>
        public void Normalize() => imageMagick.Normalize();

        #endregion

        #region Interface implementations 

        /// <summary>
        /// Реализация интерфейса <see cref="IDisposable"/>
        /// </summary>
        public void Dispose()
        {
            imageMagick.Dispose();
        }

        #endregion
    }
}
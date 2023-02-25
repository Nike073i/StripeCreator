using StripeCreator.Core.Models;
using StripeCreator.Stripe.Models;
using StripeCreator.Stripe.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace StripeCreator.WPF.Services
{
    /// <summary>
    /// Сервис работы с изображением и схематизацией
    /// </summary>
    public class ImageService
    {
        #region Constants

        /// <summary>
        /// Доступный максимальный размер стороны вышивки
        /// </summary>
        public static readonly int[] StripeSizes = new[] { 5, 6, 7, 8, 9, 10, 12, 15, 18, 20, 22, 25, 30, 35, 40, 45, 50, 60, 65, 70, 80, 90, 100, 110, 120, 130 };

        #endregion

        #region Private fields

        /// <summary>
        /// Хранитель изображения
        /// </summary>
        private readonly ImageKeeper _imageKeeper;

        #endregion

        #region Constructors

        public ImageService(ImageKeeper imageKeeper)
        {
            _imageKeeper = imageKeeper;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Создание заготовки схемы по исходному изображению
        /// </summary>
        /// <param name="imagePath">Абсолютный путь к исходному изображению</param>
        /// <param name="clothCount">Каунт ткани</param>
        /// <param name="stripeMaxSize">Максимальная длина вышивки в см</param>
        /// <param name="resizeMethod">Метод дискретизации</param>
        /// <param name="reductiveMethod">Метод уменьшения цветов</param>
        /// <param name="reductiveCount">Аргумент метода уменьшения</param>
        /// <param name="colorNormalize">Флаг применения нормализации цветов</param>
        /// <returns>Заготовка схемы вышивки</returns>
        public async Task<Image> CreateSchemaTemplate(string imagePath, int clothCount, int stripeMaxSize,
            ResizeMethod resizeMethod, ReductiveMethod reductiveMethod, int reductiveCount,
            bool colorNormalize = false)
        {
            var sourceImage = await _imageKeeper.LoadAsync(imagePath);
            using var imageProcessor = new ImageProccesor(sourceImage);
            var schemeSize = GetSchemeSize(sourceImage.Width, sourceImage.Height, clothCount, stripeMaxSize);
            ResizeImage(resizeMethod, imageProcessor, schemeSize);
            ReduceColors(reductiveMethod, reductiveCount, imageProcessor);
            if (colorNormalize) imageProcessor.Normalize();
            return imageProcessor.Image;
        }

        /// <summary>
        /// Метод получения доступных размеров вышивки
        /// </summary>
        /// <param name="imageWidth">Ширина исходного изображения</param>
        /// <param name="imageHeight">Высота исходного изображения</param>
        /// <returns>Список доступных размеров вышивки</returns>
        public static IEnumerable<Size> GetAvailableStripeSizes(double imageWidth, double imageHeight)
        {
            Func<int, Size> createSizeFunc = imageWidth >= imageHeight ?
                (int maxSize) => new Size(maxSize, GetMinStripeSize(imageWidth, imageHeight, maxSize)) :
                (int maxSize) => new Size(GetMinStripeSize(imageHeight, imageWidth, maxSize), maxSize);
            return StripeSizes.Select(createSizeFunc);
        }

        /// <summary>
        /// Получить размер изображения в пикселях
        /// </summary>
        /// <param name="imagePath">Абсолютный путь изображения</param>
        /// <returns>Размер изображения</returns>
        public static Size GetImageSize(string imagePath)
        {
            var decoder = BitmapDecoder.Create(new Uri(imagePath), BitmapCreateOptions.None, BitmapCacheOption.None);
            var frame = decoder.Frames[0];
            return new Size(frame.PixelWidth, frame.PixelHeight);
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Вызов соответствующего метода изменения размера изображения
        /// </summary>
        /// <param name="resizeMethod">Метод изменения</param>
        /// <param name="imageProcessor">Обработчик изображения</param>
        /// <param name="newSize">Новый размер изображения</param>
        private void ResizeImage(ResizeMethod resizeMethod, ImageProccesor imageProcessor, Size newSize)
        {
            switch (resizeMethod)
            {
                case ResizeMethod.Scale:
                    imageProcessor.Scale(newSize);
                    break;
                case ResizeMethod.Liquid:
                    imageProcessor.LiquidRescale(newSize);
                    break;
                case ResizeMethod.Adaptive:
                    imageProcessor.AdaptiveResize(newSize);
                    break;
                default:
                case ResizeMethod.Sample:
                    imageProcessor.Sample(newSize);
                    break;
            }
        }

        /// <summary>
        /// Вызов соответствующего метода уменьшения количества цветов
        /// </summary>
        /// <param name="reductiveMethod">Метод уменьшения</param>
        /// <param name="reductiveCount">Аргумент метода уменьшения</param>
        /// <param name="imageProcessor">Обработчик изображения</param>
        private void ReduceColors(ReductiveMethod reductiveMethod, int reductiveCount, ImageProccesor imageProcessor)
        {
            switch (reductiveMethod)
            {
                case ReductiveMethod.Posterization:
                    imageProcessor.Posterize(reductiveCount);
                    break;
                default:
                case ReductiveMethod.Quantization:
                    imageProcessor.Quantize(reductiveCount);
                    break;
            }
        }

        /// <summary>
        /// Получить размер схемы вышивки в пикселях
        /// </summary>
        /// <param name="imageWidth">Ширина исходного изображения</param>
        /// <param name="imageHeight">Высота исходного изображения</param>
        /// <param name="clothCount">Каунт ткани</param>
        /// <param name="stripeMaxSize">Максимальный размер стороны вышивки</param>
        /// <returns></returns>
        private Size GetSchemeSize(int imageWidth, int imageHeight, int clothCount, int stripeMaxSize)
        {
            double imageMaxSize = Math.Max(imageWidth, imageHeight);
            double imageMinSize = Math.Min(imageWidth, imageHeight);
            int schemeMaxSize = Convert.ToInt32(Math.Round(stripeMaxSize * 0.1d * clothCount));
            int schemeMinSize = Convert.ToInt32(Math.Round(imageMinSize * schemeMaxSize / imageMaxSize));
            return imageWidth == imageMaxSize ? new(schemeMaxSize, schemeMaxSize) : new(schemeMinSize, schemeMaxSize);
        }

        /// <summary>
        /// Получить минимальный размер вышивки по пропорции
        /// </summary>
        /// <param name="imageMaxSize">Максимальный размер изображения</param>
        /// <param name="imageMinSize">Минимальный размер изображения</param>
        /// <param name="stripeMaxSize">Максимальный размер вышивки</param>
        /// <returns></returns>
        private static int GetMinStripeSize(double imageMaxSize, double imageMinSize, double stripeMaxSize) =>
            Convert.ToInt32(Math.Round(imageMinSize * stripeMaxSize / imageMaxSize));


        #endregion
    }
}

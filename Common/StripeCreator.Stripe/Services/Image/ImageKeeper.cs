using ImageMagick;
using StripeCreator.Stripe.Extensions;
using StripeCreator.Stripe.Interfaces;
using StripeCreator.Stripe.Models;

namespace StripeCreator.Stripe.Services
{
    /// <summary>
    /// Хранитель данных <see cref="Image"/>
    /// </summary>
    public class ImageKeeper : IDataKeeper<Image>
    {
        #region Interface implementations

        /// <summary>
        /// Загрузка изображений из файла
        /// </summary>
        /// <param name="path">Абсолютный путь к изображению</param>
        /// <returns><see cref="Image"/> - загруженные данные изображения</returns>
        /// <exception cref="FileNotFoundException">Возникает, если файл с изображением по указанному пути отсутствует</exception>
        public async Task<Image> LoadAsync(string path)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException("Изображение по указаному пути не найдено");
            var data = await File.ReadAllBytesAsync(path);
            return new MagickImage(data).CreateImage();
        }

        /// <summary>
        /// Сохранение изображения в файл
        /// </summary>
        /// <param name="path">Абсолютный путь сохранения изображения</param>
        /// <param name="image">Данные изображения для сохранения</param>
        public async Task SaveAsync(string path, Image image)
        {
            using var magickImage = new MagickImage(image.Data);
            await magickImage.WriteAsync(path);
        }

        #endregion
    }
}
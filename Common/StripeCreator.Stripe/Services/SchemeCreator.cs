using ImageMagick;
using StripeCreator.Core.Models;
using StripeCreator.Stripe.Models;

namespace StripeCreator.Stripe.Services
{
    /// <summary>
    /// Создатель схемы из изображения
    /// </summary>
    public class SchemeCreator
    {
        #region Public methods

        /// <summary>
        /// Создать схему по данным изображения <see cref="Image"/>
        /// </summary>
        /// <param name="image">Данные изображения</param>
        /// <returns>Схема по изображению</returns>
        public Scheme CreateScheme(Image image)
        {
            var scheme = new Scheme(image.Width, image.Height);
            using var magickImage = new MagickImage(image.Data);
            using var pixels = magickImage.GetPixels();
            foreach (var pixel in pixels)
            {
                var magickColor = pixel.ToColor();
                // Если цвет распознать не удалось, то устанавливаем цвет по умолчанию
                var color = magickColor != null ? new Color(magickColor.ToHexString()) : new Color();
                scheme.SetCell(color, new PointPosition(pixel.X, pixel.Y));
            }

            return scheme;
        }

        #endregion
    }
}
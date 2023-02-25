using StripeCreator.Core.Models;

namespace StripeCreator.Stripe.Models
{
    /// <summary>
    /// Класс с данными изображения
    /// </summary>
    public class Image
    {
        #region Private fields

        /// <summary>
        /// Бинарные данные изображения  
        /// </summary>
        private readonly byte[] _data;

        #endregion

        #region Public properties

        /// <summary>
        /// Бинарные данные изображения
        /// </summary>
        public byte[] Data => (byte[])_data.Clone();

        /// <summary>
        /// Размер изображения
        /// </summary>
        public Size Size { get; }

        /// <summary>
        /// Ширина изображения
        /// </summary>
        public int Width => Size.Width;

        /// <summary>
        /// Высота изображения
        /// </summary>
        public int Height => Size.Height;

        #endregion

        #region Constructors

        /// <summary>
        /// Инициализация объекта изображения по ширине и высоте
        /// </summary>
        /// <param name="data">Бинарные данные изображения</param>
        /// <param name="width">Ширина изображения в пикселях</param>
        /// <param name="height">Высота изображения в пикселях</param>
        public Image(byte[] data, int width, int height) : this(data, new Size(width, height)) { }

        /// <summary>
        /// Инициализация объекта изображения по размеру
        /// </summary>
        /// <param name="data">Бинарные данные изображения</param>
        /// <param name="size">Размер изображения</param>
        public Image(byte[] data, Size size)
        {
            if (data.Length == 0)
                throw new ArgumentException("Бинарные данные изображения не могут быть пустыми");
            _data = data;
            Size = size;
        }

        #endregion
    }
}
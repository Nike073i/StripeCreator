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
        private byte[] _data = { };

        /// <summary>
        /// Ширина изображения
        /// </summary>
        public int _width;

        /// <summary>
        /// Высота изображения
        /// </summary>
        public int _height;

        #endregion

        #region Public properties

        /// <summary>
        /// Бинарные данные изображения
        /// </summary>
        public byte[] Data
        {
            get => (byte[])_data.Clone();
            private set
            {
                if (value.Length == 0)
                    throw new ArgumentException("Бинарные данные изображения не могут быть пустыми");
                _data = value;
            }
        }

        /// <summary>
        /// Ширина изображения
        /// </summary>
        public int Width
        {
            get => _width;
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException(nameof(Width), "Ширина изображения не может быть <= 0");
                _width = value;
            }
        }

        /// <summary>
        /// Высота изображения
        /// </summary>
        public int Height
        {
            get => _height;
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException(nameof(Height), "Высота изображения не может быть <= 0");
                _height = value;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Инициализация объекта изображения
        /// </summary>
        /// <param name="data">Бинарные данные изображения</param>
        /// <param name="width">Ширина изображения в пикселях</param>
        /// <param name="height">Высота изображения в пикселях</param>
        /// <exception cref="ImageException">Возникает при некорреткном указании данных изображения</exception>
        public Image(byte[] data, int width, int height)
        {
            Data = data;
            Width = width;
            Height = height;
        }

        #endregion
    }
}
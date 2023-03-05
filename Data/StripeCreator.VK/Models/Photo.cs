namespace StripeCreator.VK.Models
{
    /// <summary>Информация о фотографии</summary>
    public class Photo
    {
        #region Constants

        /// <summary>Минимально допустимая ширина изображения</summary>
        public static readonly int ImageMinWidth = 400;

        /// <summary>Максимально допустимая ширина изображения</summary>
        public static readonly int ImageMaxWidth = 1500;

        /// <summary>Минимально допустимая высота изображения</summary>
        public static readonly int ImageMinHeight = 400;

        /// <summary>Максимально допустимая высота изображения</summary>
        public static readonly int ImageMaxHeight = 1500;

        #endregion

        #region Public properties

        /// <summary>Идентификатор изображения</summary>
        public long Id { get; }

        /// <summary>Адрес изображения</summary>
        public Uri Uri { get; }

        #endregion

        #region Constructors

        /// <summary>Конструктор с полной инициализацией</summary>
        /// <param name="id">Идентификатор фотографии</param>
        /// <param name="uri">Адрес фотографии на сервере</param>
        public Photo(long id, Uri uri)
        {
            Id = id;
            Uri = uri;
        }

        #endregion
    }
}
namespace StripeCreator.WPF
{
    /// <summary>Модель для публикации записи</summary>
    public class PublishMessageModel
    {
        #region Public properties

        /// <summary>Сообщение</summary>
        public string Message { get; }

        /// <summary>Абсолютный путь к изображению</summary>
        public string? PhotoPath { get; }

        #endregion

        #region Constructors

        /// <summary>Конструктор с полной инициализацией</summary>
        /// <param name="message">Сообщение</param>
        /// <param name="photoPath">Абсолютный путь к изображению</param>
        public PublishMessageModel(string message, string? photoPath = null)
        {
            Message = message;
            PhotoPath = photoPath;
        }

        #endregion
    }
}

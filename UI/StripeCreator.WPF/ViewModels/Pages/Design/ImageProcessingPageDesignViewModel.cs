namespace StripeCreator.WPF
{
    /// <summary>
    /// Класс view-model для страницы обработки изображения в design-mode
    /// </summary>
    public class ImageProcessingPageDesignViewModel : ImageProcessingPageViewModel
    {
        #region Singleton

        /// <summary>
        /// Экземпляр для показа в Design-mode
        /// </summary>
        public static ImageProcessingPageDesignViewModel Instance => new();

        #endregion

        #region Constructors

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public ImageProcessingPageDesignViewModel() { }

        #endregion
    }
}

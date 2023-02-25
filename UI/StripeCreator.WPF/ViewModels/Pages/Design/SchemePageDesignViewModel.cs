namespace StripeCreator.WPF
{
    /// <summary>
    /// Класс view-model для страницы работы со схемой в design-mode
    /// </summary>
    public class SchemePageDesignViewModel : SchemePageViewModel
    {
        #region Singleton

        /// <summary>
        /// Экземпляр для показа в Design-mode
        /// </summary>
        public static SchemePageDesignViewModel Instance => new();

        #endregion

        #region Constructors

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public SchemePageDesignViewModel() { }

        #endregion
    }
}

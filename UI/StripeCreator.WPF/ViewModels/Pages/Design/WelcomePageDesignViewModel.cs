namespace StripeCreator.WPF
{
    /// <summary>
    /// Класс view-model для страницы приветствия в design-mode
    /// </summary>
    public class WelcomePageDesignViewModel : WelcomePageViewModel
    {
        #region Singleton

        /// <summary>
        /// Экземпляр для показа в Design-mode
        /// </summary>
        public static WelcomePageDesignViewModel Instance => new();

        #endregion

        #region Constructors

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public WelcomePageDesignViewModel() {  }

        #endregion
    }
}

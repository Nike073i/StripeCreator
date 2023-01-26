namespace StripeCreator.WPF
{
    /// <summary>
    /// Класс view-model для страницы приветствия в design-mode
    /// </summary>
    public class DataStorePageDesignViewModel : DataStorePageViewModel
    {
        #region Singleton

        /// <summary>
        /// Экземпляр для показа в Design-mode
        /// </summary>
        public static DataStorePageDesignViewModel Instance => new();

        #endregion

        #region Constructors

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public DataStorePageDesignViewModel()
        {
        }

        #endregion
    }
}

namespace StripeCreator.WPF
{
    /// <summary>
    /// Класс view-model для страницы формирования отчетов в design-mode
    /// </summary>
    public class ReportPageDesignViewModel : ReportPageViewModel
    {
        #region Singleton

        /// <summary>
        /// Экземпляр для показа в Design-mode
        /// </summary>
        public static ReportPageDesignViewModel Instance => new();

        #endregion

        #region Constructors

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public ReportPageDesignViewModel() { }

        #endregion
    }
}

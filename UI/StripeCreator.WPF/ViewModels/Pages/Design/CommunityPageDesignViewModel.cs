namespace StripeCreator.WPF
{
    /// <summary>
    /// Класс view-model для страницы работы с сообществом в design-mode
    /// </summary>
    public class CommunityPageDesignViewModel : CommunityPageViewModel
    {
        #region Singleton

        /// <summary>
        /// Экземпляр для показа в Design-mode
        /// </summary>
        public static CommunityPageDesignViewModel Instance => new();

        #endregion

        #region Constructors

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public CommunityPageDesignViewModel() { }

        #endregion
    }
}

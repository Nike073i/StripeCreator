namespace StripeCreator.WPF
{
    /// <summary>
    /// ViewModel основонго приложения
    /// </summary>
    public class ApplicationViewModel : BaseViewModel
    {
        #region Public properties

        /// <summary>
        /// Текущая страница приложения
        /// </summary>
        public ApplicationPage CurrentPage { get; private set; } = ApplicationPage.Welcome;

        #endregion

        #region Public methods

        /// <summary>
        /// Смена страницы на <paramref name="page"/>
        /// </summary>
        /// <param name="page">Новая страница</param>
        public void GoToPage(ApplicationPage page)
        {
            // Если текущая страница соответствует новой, то ничего не делаем
            if (CurrentPage == page) return;

            CurrentPage = page;
        }

        #endregion
    }
}

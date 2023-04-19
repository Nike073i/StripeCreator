namespace StripeCreator.WPF
{
    /// <summary>
    /// ViewModel основонго приложения
    /// </summary>
    public class ApplicationViewModel : BaseViewModel
    {
        #region Public properties

        /// <summary>
        /// Заголовок окна приложения
        /// </summary>
        public string WindowTitle { get; set; } = "StripeCreator";

        /// <summary>
        /// Текущая страница приложения
        /// </summary>
        public ApplicationPage CurrentPage { get; private set; } = ApplicationPage.Welcome;

        /// <summary>
        /// Аргумент страницы приложения
        /// </summary>
        public object? CurrentPageArg { get; set; }

        #endregion

        #region Public methods

        /// <summary>
        /// Смена страницы на <paramref name="page"/>
        /// </summary>
        /// <param name="page">Новая страница</param>
        /// <param name="pageArg">Аргумент страницы</param>
        public void GoToPage(ApplicationPage page, object? pageArg = null)
        {
            // Если текущая страница соответствует новой, то ничего не делаем
            if (CurrentPage == page) return;
            var tmpPage = CurrentPage;
            var tmpArgs = CurrentPageArg;
            try
            {
                CurrentPageArg = pageArg;
                CurrentPage = page;
            }
            catch
            {
                CurrentPage = tmpPage;
                CurrentPageArg = tmpArgs;
                throw;
            }
        }

        #endregion
    }
}

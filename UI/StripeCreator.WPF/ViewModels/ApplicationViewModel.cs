namespace StripeCreator.WPF
{
    public class ApplicationViewModel : BaseViewModel
    {
        public ApplicationPage CurrentPage { get; private set; } = ApplicationPage.Welcome;

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
    }
}

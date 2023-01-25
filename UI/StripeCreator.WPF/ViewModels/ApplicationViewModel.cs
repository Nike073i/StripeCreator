namespace StripeCreator.WPF
{
    public class ApplicationViewModel : BaseViewModel
    {
        public ApplicationPage CurrentPage { get; private set; }

        /// <summary>
        /// Смена страницы на <paramref name="page"/>
        /// </summary>
        /// <param name="page">Новая страница</param>
        public void GoToPage(ApplicationPage page)
        {
            // Возможна дополнительная логика проверки перехода
            CurrentPage = page;
        }
    }
}

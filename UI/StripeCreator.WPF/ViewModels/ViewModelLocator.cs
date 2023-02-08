namespace StripeCreator.WPF
{
    /// <summary>
    /// Вспомогательный класс для получения ViewModels
    /// </summary>
    public class ViewModelLocator
    {
        #region Public properties

        /// <summary>
        /// ViewModel основного приложения
        /// </summary>
        public static ApplicationViewModel ApplicationViewModel => IoC.GetRequiredService<ApplicationViewModel>();

        #endregion
    }
}

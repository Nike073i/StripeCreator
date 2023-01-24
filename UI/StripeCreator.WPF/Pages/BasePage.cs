using System.Windows.Controls;

namespace StripeCreator.WPF
{
    /// <summary>
    /// Базовый класс для страниц
    /// </summary>
    public class BasePage : UserControl { }

    /// <summary>
    /// Базовый класс для страниц с view-model
    /// </summary>
    /// <typeparam name="TViewModel">view-model страницы</typeparam>
    public class BasePage<TViewModel> : BasePage
        where TViewModel : BaseViewModel, new()
    {
        #region Public properties

        public TViewModel ViewModel { get; set; }

        #endregion

        #region Constructors

        public BasePage(TViewModel viewModel)
        {
            ViewModel = viewModel;
        }

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public BasePage()
        {
            ViewModel = new TViewModel();
        }

        #endregion
    }
}
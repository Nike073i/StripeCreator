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
        #region Private fields

#nullable disable
        private TViewModel _viewModel;

        #endregion

        #region Public properties

        public TViewModel ViewModel
        {
            get => _viewModel;
            set
            {
                // Если ничего не изменилось
                if (_viewModel == value)
                    return;

                _viewModel = value;
                DataContext = _viewModel;
            }
        }

        #endregion

        #region Constructors

        public BasePage(TViewModel viewModel)
        {
            ViewModel = viewModel;
        }

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public BasePage() : this(new TViewModel()) { }

        #endregion
    }
}
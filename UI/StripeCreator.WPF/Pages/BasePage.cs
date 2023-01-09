using System.Windows.Controls;

namespace StripeCreator.WPF
{
    public abstract class BasePage : UserControl { }

    public abstract class BasePage<TViewModel> : BasePage
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

        #endregion
    }
}
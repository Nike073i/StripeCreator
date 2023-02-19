using System.Windows.Controls;

namespace StripeCreator.WPF
{
    /// <summary>
    /// ������� ����� ��� �������
    /// </summary>
    public class BasePage : UserControl { }

    /// <summary>
    /// ������� ����� ��� ������� � view-model
    /// </summary>
    /// <typeparam name="TViewModel">view-model ��������</typeparam>
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
        /// ����������� �� ���������
        /// </summary>
        public BasePage()
        {
            ViewModel = new TViewModel();
        }

        #endregion
    }
}
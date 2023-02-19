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
                // ���� ������ �� ����������
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
        /// ����������� �� ���������
        /// </summary>
        public BasePage() : this(new TViewModel()) { }

        #endregion
    }
}
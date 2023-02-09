using System.Threading.Tasks;
using System.Windows;

namespace StripeCreator.WPF
{
    /// <summary>
    /// Диалоговая реаализация взаимодействия с пользователем
    /// </summary>
    public class DialogUiManager : IUiManager
    {
        #region Interface implementations

        public Task ShowInfo(MessageBoxDialogViewModel viewModel)
        {
            return Task.FromResult(MessageBox.Show(viewModel.Message, viewModel.Title, MessageBoxButton.OK, MessageBoxImage.Information));
        }

        public Task ShowError(MessageBoxDialogViewModel viewModel)
        {
            return Task.FromResult(MessageBox.Show(viewModel.Message, viewModel.Title, MessageBoxButton.OK, MessageBoxImage.Error));
        }

        public Task<bool> ShowConfirm(MessageBoxDialogViewModel viewModel)
        {
            return Task.FromResult(MessageBox.Show(viewModel.Message, viewModel.Title, MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes);
        }

        #endregion
    }
}

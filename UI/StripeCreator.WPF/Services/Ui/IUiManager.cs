using System.Threading.Tasks;

namespace StripeCreator.WPF
{
    /// <summary>
    /// Менеджер для интерактивного взаимодействия с пользователем
    /// </summary>
    public interface IUiManager
    {
        /// <summary>
        /// Отобразить информацию для пользователя
        /// </summary>
        /// <param name="viewModel">ViewModel сообщения</param>
        Task ShowInfo(MessageBoxDialogViewModel viewModel);

        /// <summary>
        /// Отобразить ошибку для пользователя
        /// </summary>
        /// <param name="viewModel">ViewModel сообщения</param>
        Task ShowError(MessageBoxDialogViewModel viewModel);

        /// <summary>
        /// Отобразить окно подтверждения для пользователя
        /// </summary>
        /// <param name="viewModel">ViewModel сообщения</param>
        /// <returns>Выбор пользователя</returns>
        Task<bool> ShowConfirm(MessageBoxDialogViewModel viewModel);
    }
}

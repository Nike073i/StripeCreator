using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

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

        public Task<IEntityViewModel?> FormationEntity(EntityFormationViewModel formationViewModel)
        {
            EntityFormationWindow window = new();
            IEntityViewModel? formedEntity = null;

            async void okAction(object? param)
            {
                var newViewModel = formationViewModel!.GetData();
                if (newViewModel == null)
                {
                    await ShowError(new("Ошибка заполнения полей", formationViewModel.ErrorString));
                    return;
                }
                formedEntity = newViewModel;
                window.DialogResult = true;
                window.Close();
            }

            void cancelAction(object? param)
            {
                window.DialogResult = false;
                window.Close();
            }

            (Control formationView, string title) = GetViewInfo(formationViewModel);

            var windowViewModel = new EntityFormationWindowViewModel(formationView, title, okAction, cancelAction);
            window.DataContext = windowViewModel;
            window.ShowDialog();

            return Task.FromResult(formedEntity);
        }

        #endregion

        #region Private helper methods

        /// <summary>
        /// Получить данные представления для формирования сущности
        /// </summary>
        /// <param name="formationViewModel">Модель формирования сущности</param>
        /// <returns>Представление формирования и заголовок окна</returns>
        /// <exception cref="ArgumentException">Возникает, если для <paramref name="formationViewModel"/> не поддерживается формирование</exception>
        private (Control, string) GetViewInfo(EntityFormationViewModel formationViewModel)
        {
            Control? formationView;
            string title;

            switch (formationViewModel)
            {
                case ClientFormationViewModel client:
                    formationView = new ClientFormationView(client);
                    title = "Формирование клиента";
                    break;
                case ClothFormationViewModel cloth:
                    formationView = new ClothFormationView(cloth);
                    title = "Формирование ткани";
                    break;
                case ThreadFormationViewModel thread:
                    formationView = new ThreadFormationView(thread);
                    title = "Формирование нити";
                    break;
                default:
                    throw new ArgumentException("Формирование данной сущности не поддерживается");
            }

            return (formationView, title);
        }

        #endregion
    }
}

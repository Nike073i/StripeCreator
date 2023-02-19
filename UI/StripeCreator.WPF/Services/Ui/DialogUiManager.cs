using StripeCreator.Business.Models;
using System;
using System.Collections.Generic;
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
            DialogWindow window = new();
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
            string caption = "Формирование";

            var windowViewModel = new DialogWindowViewModel(formationView, title, caption, okAction, cancelAction, okText: "Сохранить");
            window.DataContext = windowViewModel;
            window.ShowDialog();

            return Task.FromResult(formedEntity);
        }

        public Task<OrderCreateModel?> CreateOrder(IEnumerable<Product> products, IEnumerable<Client> clients)
        {
            DialogWindow window = new();
            var createOrderViewModel = new OrderCreateViewModel(clients, products);
            OrderCreateModel? createOrderModel = null;
            async void okAction(object? param)
            {
                var newViewModel = createOrderViewModel.GetData();
                if (newViewModel == null)
                {
                    await ShowError(new("Ошибка заполнения полей", createOrderViewModel.ErrorString));
                    return;
                }
                createOrderModel = newViewModel;
                window.DialogResult = true;
                window.Close();
            }

            void cancelAction(object? param)
            {
                window.DialogResult = false;
                window.Close();
            }

            var createOrderView = new OrderCreateView(createOrderViewModel);
            string title = "Создание заказа";
            string caption = "Новый заказ";

            var windowViewModel = new DialogWindowViewModel(createOrderView, title, caption, okAction, cancelAction, okText: "Сохранить");

            window.DataContext = windowViewModel;
            window.ShowDialog();

            return Task.FromResult(createOrderModel);
        }

        public Task ShowOrderDetail(OrderDetailViewModel orderDetailViewModel)
        {
            DialogWindow window = new();
            void closeAction(object? param) { window.Close(); }

            var createOrderView = new OrderDetailView(orderDetailViewModel);
            string title = "Информация";
            string caption = "Информация по заказу";

            var windowViewModel = new DialogWindowViewModel(createOrderView, title, caption, closeAction, closeAction, okText: "Ок");

            window.DataContext = windowViewModel;
            window.ShowDialog();
            return Task.CompletedTask;
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
                case ProductFormationViewModel product:
                    formationView = new ProductFormationView(product);
                    title = "Формирование продукта";
                    break;
                default:
                    throw new ArgumentException("Формирование данной сущности не поддерживается");
            }

            return (formationView, title);
        }

        #endregion
    }
}

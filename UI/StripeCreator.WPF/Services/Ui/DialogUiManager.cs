using StripeCreator.Business.Models;
using StripeCreator.Business.Services;
using StripeCreator.Stripe.Models;
using StripeCreator.Stripe.Services;
using StripeCreator.VK.Models;
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
        #region Private fields

        /// <summary>
        /// Сервис работы с клиентами
        /// </summary>
        private readonly ClientService _clientService;

        /// <summary>
        /// Сервис расчета стоимости заказа
        /// </summary>
        private readonly OrderPriceCalculator _orderPriceCalculator;

        #endregion

        #region Constructors

        /// <summary>
        /// Конструктор с полной инициализацией
        /// </summary>
        /// <param name="clientService">Сервис клиентов</param>
        public DialogUiManager(ClientService clientService, OrderPriceCalculator orderPriceCalculator)
        {
            _clientService = clientService;
            _orderPriceCalculator = orderPriceCalculator;
        }

        #endregion

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
            var createOrderViewModel = new OrderCreateViewModel(this, _clientService, _orderPriceCalculator, clients, products);
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

        public Task CalculateMaterial(Scheme scheme)
        {
            DialogWindow window = new();
            var materialCalculator = new SchemeMaterialCalculator();
            var schemeMaterialCalculateViewModel = new SchemeMaterialCalculateViewModel(this, materialCalculator, scheme);
            var schemeMaterialCalculateView = new SchemeMaterialCalculateView(schemeMaterialCalculateViewModel);

            void closeAction(object? param) => window.Close();
            void okAction(object? param) => schemeMaterialCalculateViewModel?.CalculateMaterial();

            var windowViewModel = new DialogWindowViewModel(schemeMaterialCalculateView, "Расчет", "Материалы", okAction, closeAction, okText: "Рассчитать");

            window.DataContext = windowViewModel;
            window.ShowDialog();
            return Task.CompletedTask;
        }

        public Task CalculatePrice(Scheme scheme, IEnumerable<Thread> threads, IEnumerable<Cloth> cloths)
        {
            DialogWindow window = new();
            var materialCalculator = new SchemeMaterialCalculator();
            var priceCalculator = new SchemePriceCalculator(materialCalculator);
            var schemePriceCalculateViewModel = new SchemePriceCalculateViewModel(this, priceCalculator, scheme, cloths, threads);
            var schemePriceCalculateView = new SchemePriceCalculateView(schemePriceCalculateViewModel);

            void closeAction(object? param) => window.Close();
            void okAction(object? param) => schemePriceCalculateViewModel?.CalculatePrice();

            var windowViewModel = new DialogWindowViewModel(schemePriceCalculateView, "Расчет", "Стоимость", okAction, closeAction, okText: "Рассчитать");

            window.DataContext = windowViewModel;
            window.ShowDialog();
            return Task.CompletedTask;
        }

        public Task<MarketCreateModel?> CreateMarket()
        {
            DialogWindow window = new();
            var createMarketViewModel = new MarketCreateViewModel();
            MarketCreateModel? createMarketModel = null;
            async void okAction(object? param)
            {
                var newViewModel = createMarketViewModel.GetData();
                if (newViewModel == null)
                {
                    await ShowError(new("Ошибка заполнения полей", createMarketViewModel.ErrorString));
                    return;
                }
                createMarketModel = newViewModel;
                window.DialogResult = true;
                window.Close();
            }

            void cancelAction(object? param)
            {
                window.DialogResult = false;
                window.Close();
            }

            var createMarketView = new MarketCreateView(createMarketViewModel);
            string title = "Создание товара";
            string caption = "Новый товар";

            var windowViewModel = new DialogWindowViewModel(createMarketView, title, caption, okAction, cancelAction, okText: "Сохранить");

            window.DataContext = windowViewModel;
            window.ShowDialog();

            return Task.FromResult(createMarketModel);
        }

        public Task<Market?> EditMarket(MarketViewModel viewModel)
        {
            DialogWindow window = new();
            var editMarketViewModel = new MarketEditViewModel(viewModel.Market);
            Market? editMarket = null;
            async void okAction(object? param)
            {
                var newMarket = editMarketViewModel.GetData();
                if (newMarket == null)
                {
                    await ShowError(new("Ошибка заполнения полей", editMarketViewModel.ErrorString));
                    return;
                }
                editMarket = newMarket;
                window.DialogResult = true;
                window.Close();
            }

            void cancelAction(object? param)
            {
                window.DialogResult = false;
                window.Close();
            }

            var editMarketView = new MarketEditView(editMarketViewModel);
            string title = "Редактирование товара";
            string caption = $"Товар - {viewModel.Market.Id}";

            var windowViewModel = new DialogWindowViewModel(editMarketView, title, caption, okAction, cancelAction, okText: "Сохранить");

            window.DataContext = windowViewModel;
            window.ShowDialog();

            return Task.FromResult(editMarket);
        }

        public Task<PublishMessageModel?> PublishMessage(PublishMessageViewModel? publishMessageViewModel = null)
        {
            DialogWindow window = new();
            publishMessageViewModel ??= new PublishMessageViewModel();
            PublishMessageModel? publishMessageModel = null;
            async void okAction(object? param)
            {
                var newViewModel = publishMessageViewModel.GetData();
                if (newViewModel == null)
                {
                    await ShowError(new("Ошибка заполнения полей", publishMessageViewModel.ErrorString));
                    return;
                }
                publishMessageModel = newViewModel;
                window.DialogResult = true;
                window.Close();
            }

            void cancelAction(object? param)
            {
                window.DialogResult = false;
                window.Close();
            }

            var publishMessageView = new PublishMessageView(publishMessageViewModel);
            string title = "Публикация записи";
            string caption = "Новая запись";

            var windowViewModel = new DialogWindowViewModel(publishMessageView, title, caption, okAction, cancelAction, okText: "Сохранить");

            window.DataContext = windowViewModel;
            window.ShowDialog();

            return Task.FromResult(publishMessageModel);
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

using FontAwesome5;
using StripeCreator.Business.Repositories;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace StripeCreator.WPF
{
    /// <summary>
    /// ViewModel для страницы взаимодействия с заказами
    /// </summary>
    public class OrderPageViewModel : BaseViewModel
    {
        #region Private fields

        /// <summary>
        /// Заголовок меню действий
        /// </summary>
        private readonly string _header = "Доступные действия";

        /// <summary>
        /// Менеджер интерактивного взаимодействия
        /// </summary>
        private readonly IUiManager _uiManager;

        /// <summary>
        /// Сервис работы с заказами
        /// </summary>
        private readonly OrderService _orderService;

        /// <summary>
        /// Репозиторий продукции
        /// </summary>
        private readonly IProductRepository _productRepository;

        /// <summary>
        /// Репозиторий клиентов
        /// </summary>
        private readonly IClientRepository _clientRepository;

        /// <summary>
        /// ViewModel приложения
        /// </summary>
        private readonly ApplicationViewModel _applicationViewModel;

        #endregion

        #region Public properties

        /// <summary>
        /// ViewModel меню действий
        /// </summary>
        public ActionMenuViewModel ActionMenuViewModel { get; protected set; }

        /// <summary>
        /// Список хранимых заказов
        /// </summary>
        public ObservableCollection<OrderViewModel> Orders { get; protected set; }

        /// <summary>
        /// Выбранный заказ
        /// </summary>
        public OrderViewModel? SelectedOrder { get; set; }

        #region Commands

        /// <summary>
        /// Команда создания нового заказа
        /// </summary>
        public ICommand CreateCommand { get; }

        /// <summary>
        /// Команда просмотра деталей заказа
        /// </summary>
        public ICommand InfoCommand { get; }

        /// <summary>
        /// Команда продвижения статуса заказа
        /// </summary>
        public ICommand UpdateCommand { get; }

        /// <summary>
        /// Команда отмена заказа
        /// </summary>
        public ICommand CancelCommand { get; }

        /// <summary>
        /// Команда обновления списка хранимых заказов
        /// </summary>
        public ICommand RefreshCommand { get; }


        /// <summary>
        /// Команда выхода в главное меню
        /// </summary>
        public ICommand MenuCommand { get; }

        /// <summary>
        /// Предикат для команды взаимодействия с выбранным заказом
        /// </summary>
        public bool ActionEnabled => SelectedOrder != null;

        #endregion

        #endregion

        #region Constructors

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
#nullable disable
        public OrderPageViewModel() { }
#nullable enable

        /// <summary>
        /// Конструктор с полной инициализацией
        /// </summary>
        /// <param name="applicationViewModel">ViewModel приложения</param>
        /// <param name="uiManager">Менеджер интерактивного взаимодействия</param>
        public OrderPageViewModel(ApplicationViewModel applicationViewModel, IUiManager uiManager, OrderService orderService, IProductRepository productRepository, IClientRepository clientRepository)
        {
            _applicationViewModel = applicationViewModel;
            _uiManager = uiManager;
            _orderService = orderService;
            _productRepository = productRepository;
            _clientRepository = clientRepository;

            Orders = new ObservableCollection<OrderViewModel>();

            // Инициализация команд
            CreateCommand = new RelayCommand(async param => await OnExecutedCreateCommand(param));
            InfoCommand = new RelayCommand(async param => await OnExecutedInfoCommand(param)) { CanExecutePredicate = CanExecuteInfoCommand };
            UpdateCommand = new RelayCommand(async param => await OnExecutedUpdateCommand(param)) { CanExecutePredicate = CanExecuteUpdateCommand };
            CancelCommand = new RelayCommand(async param => await OnExecutedCancelCommand(param)) { CanExecutePredicate = CanExecuteCancelCommand };
            RefreshCommand = new RelayCommand(async param => await OnExecutedRefreshCommand(param));
            MenuCommand = new RelayCommand(param => OnExecutedMenuCommand(param));

            ActionMenuViewModel = new(_header, GetMenuItems());
        }

        #endregion

        #region Private helper methods

        /// <summary>
        /// Получить список элементов меню действий
        /// </summary>
        /// <returns>Список элементов меню действий</returns>
        private List<ActionMenuItemViewModel> GetMenuItems()
        {
            return new List<ActionMenuItemViewModel>
            {
                new(EFontAwesomeIcon.Solid_Plus, "Создать заказ", CreateCommand),
                new(EFontAwesomeIcon.Solid_Info, "Детали заказа", InfoCommand),
                new(EFontAwesomeIcon.Solid_AngleDoubleUp, "Продвинуть статус", UpdateCommand),
                new(EFontAwesomeIcon.Solid_Ban, "Отменить заказ", CancelCommand),
                new(EFontAwesomeIcon.Solid_SyncAlt, "Обновить список", RefreshCommand),
                new(EFontAwesomeIcon.Solid_ArrowLeft, "В меню", MenuCommand),
            };
        }

        #region Commands action and predicate

        /// <summary>
        /// Создать новый заказ
        /// </summary>
        /// <param name="parameter">Параметр команды</param>
        private async Task OnExecutedCreateCommand(object? parameter)
        {
            try
            {
                var products = await _productRepository.GetAllAsync();
                var clients = await _clientRepository.GetAllAsync();
                var orderCreateModel = await _uiManager.CreateOrder(products, clients);
                if (orderCreateModel == null)
                {
                    await _uiManager.ShowInfo(new("Отмена", "Создание заказа отменено"));
                    return;
                }
                var newOrder = await _orderService.CreateAsync(orderCreateModel);
                Orders.Add(newOrder);
            }
            catch (Exception ex)
            {
                await _uiManager.ShowError(new("Ошибка создания заказа", ex.Message));
            }
        }

        /// <summary>
        /// Получить информацию по заказку
        /// </summary>
        /// <param name="parameter">Параметр команды</param>
        private async Task OnExecutedInfoCommand(object? parameter)
        {
            try
            {
                var orders = await _orderService.GetAllAsync();
                Orders = new ObservableCollection<OrderViewModel>(orders);

                var order = SelectedOrder!.Entity;
                var client = await _clientRepository.GetByIdAsync(order.ClientId) ??
                    throw new InvalidOperationException("Клиент с указанным Id не найден");
                var orderProducts = new List<OrderProductViewModel>();
                foreach (var orderProduct in order.Products)
                {
                    var product = await _productRepository.GetByIdAsync(orderProduct.ProductId);
                    if (product == null) continue;
                    orderProducts.Add(new OrderProductViewModel(product, orderProduct.Quantity));
                }
                var orderDetailViewModel = new OrderDetailViewModel(order, client, orderProducts);
                await _uiManager.ShowOrderDetail(orderDetailViewModel);
            }
            catch (Exception ex)
            {
                await _uiManager.ShowError(new("Ошибка получения данных по заказу", ex.Message));
            }
        }

        /// <summary>
        /// Проверка вызова команды получения информацию по заказу
        /// </summary>
        /// <param name="parameter">Параметр команды</param>
        private bool CanExecuteInfoCommand(object? parameter) => ActionEnabled;

        /// <summary>
        /// Продвинуть статус заказа
        /// </summary>
        /// <param name="parameter">Параметр команды</param>
        private async Task OnExecutedUpdateCommand(object? parameter)
        {
            try
            {
                var changedOrder = await _orderService.UpdateStatus(SelectedOrder!);
                Orders[Orders.IndexOf(SelectedOrder!)] = changedOrder;
            }
            catch (Exception ex)
            {
                await _uiManager.ShowError(new("Ошибка обновления статуса", ex.Message));
            }
        }

        /// <summary>
        /// Проверка вызова команды продвижения статуса заказа
        /// </summary>
        /// <param name="parameter">Параметр команды</param>
        private bool CanExecuteUpdateCommand(object? parameter) => ActionEnabled;

        /// <summary>
        /// Отмена заказа
        /// </summary>
        /// <param name="parameter">Параметр команды</param>
        private async Task OnExecutedCancelCommand(object? parameter)
        {
            try
            {
                var changedOrder = await _orderService.CancelOrder(SelectedOrder!);
                Orders[Orders.IndexOf(SelectedOrder!)] = changedOrder;
            }
            catch (Exception ex)
            {
                await _uiManager.ShowError(new("Ошибка отмены заказа", ex.Message));
            }
        }

        /// <summary>
        /// Проверка вызова команды отмены заказа
        /// </summary>
        /// <param name="parameter">Параметр команды</param>
        private bool CanExecuteCancelCommand(object? parameter) => ActionEnabled;

        /// <summary>
        /// Обновить список хранимых заказов
        /// </summary>
        /// <param name="parameter">Параметр команды</param>
        private async Task OnExecutedRefreshCommand(object? parameter)
        {
            try
            {
                var orders = await _orderService.GetAllAsync();
                Orders = new ObservableCollection<OrderViewModel>(orders);
            }
            catch (Exception ex)
            {
                await _uiManager.ShowError(new("Ошибка загрузки данных", ex.Message));
            }
        }

        /// <summary>
        /// Действие при команде выхода в главное меню
        /// </summary>
        /// <param name="parameter">Параметр команды</param>
        private void OnExecutedMenuCommand(object? parameter) => _applicationViewModel.GoToPage(ApplicationPage.Welcome);

        #endregion

        #endregion
    }
}

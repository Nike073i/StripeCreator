using FontAwesome5;
using StripeCreator.Business.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Navigation;

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
        public ObservableCollection<OrderViewModel>? Orders { get; protected set; }

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
        /// Предикат для команд работы с выбранным заказов
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
        public OrderPageViewModel(ApplicationViewModel applicationViewModel, IUiManager uiManager)
        {
            _applicationViewModel = applicationViewModel;
            _uiManager = uiManager;

            ActionMenuViewModel = new(_header, GetMenuItems());

            // Инициализация команд
            CreateCommand = new RelayCommand(async param => await OnExecutedCreateCommand(param)) { CanExecutePredicate = CanExecuteCreateCommand };
            InfoCommand = new RelayCommand(async param => await OnExecutedInfoCommand(param)) { CanExecutePredicate = CanExecuteInfoCommand };
            UpdateCommand = new RelayCommand(async param => await OnExecutedUpdateCommand(param)) { CanExecutePredicate = CanExecuteUpdateCommand };
            CancelCommand = new RelayCommand(async param => await OnExecutedCancelCommand(param)) { CanExecutePredicate = CanExecuteCancelCommand };
            RefreshCommand = new RelayCommand(async param => await OnExecutedRefreshCommand(param)) { CanExecutePredicate = CanExecuteRefreshCommand };
            MenuCommand = new RelayCommand(param => OnExecutedMenuCommand(param));
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
        private async Task OnExecutedCreateCommand(object? parameter) { }

        /// <summary>
        /// Проверка вызова команды создания заказа
        /// </summary>
        /// <param name="parameter">Параметр команды</param>
        /// <returns></returns>
        private bool CanExecuteCreateCommand(object? parameter) => true;

        /// <summary>
        /// Получить информацию по заказку
        /// </summary>
        /// <param name="parameter">Параметр команды</param>
        private async Task OnExecutedInfoCommand(object? parameter) { }

        /// <summary>
        /// Проверка вызова команды получения информацию по заказу
        /// </summary>
        /// <param name="parameter">Параметр команды</param>
        /// <returns></returns>
        private bool CanExecuteInfoCommand(object? parameter) => true;

        /// <summary>
        /// Продвинуть статус заказа
        /// </summary>
        /// <param name="parameter">Параметр команды</param>
        private async Task OnExecutedUpdateCommand(object? parameter) { }

        /// <summary>
        /// Проверка вызова команды продвижения статуса заказа
        /// </summary>
        /// <param name="parameter">Параметр команды</param>
        /// <returns></returns>
        private bool CanExecuteUpdateCommand(object? parameter) => true;

        /// <summary>
        /// Отмена заказа
        /// </summary>
        /// <param name="parameter">Параметр команды</param>
        private async Task OnExecutedCancelCommand(object? parameter) { }

        /// <summary>
        /// Проверка вызова команды отмены заказа
        /// </summary>
        /// <param name="parameter">Параметр команды</param>
        /// <returns></returns>
        private bool CanExecuteCancelCommand(object? parameter) => true;

        /// <summary>
        /// Обновить список хранимых заказов
        /// </summary>
        /// <param name="parameter">Параметр команды</param>
        private Task OnExecutedRefreshCommand(object? parameter)
        {
            var orderLines = new List<OrderProduct>
            {
                new(Guid.NewGuid(), 15),
            };
            var list = new List<OrderViewModel>
            {
                new(new(Guid.NewGuid(), 150m, orderLines, new("+79176306258", "nike073i@mail.ru"))),
                new(new(Guid.NewGuid(), 150m, orderLines, new("+79176306258", "nike073i@mail.ru"))),
                new(new(Guid.NewGuid(), 150m, orderLines, new("+79176306258", "nike073i@mail.ru"))),
                new(new(Guid.NewGuid(), 150m, orderLines, new("+79176306258", "nike073i@mail.ru"))),
            };
            Orders = new ObservableCollection<OrderViewModel>(list);
            return Task.CompletedTask;
        }

        /// <summary>
        /// Проверка вызова команды обновления данных
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        private bool CanExecuteRefreshCommand(object? parameter) => true;

        /// <summary>
        /// Действие при команде выхода в главное меню
        /// </summary>
        /// <param name="parameter">Параметр команды</param>
        private void OnExecutedMenuCommand(object? parameter) => _applicationViewModel.GoToPage(ApplicationPage.Welcome);

        #endregion

        #endregion
    }
}

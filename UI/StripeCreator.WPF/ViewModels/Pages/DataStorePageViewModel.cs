using FontAwesome5;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace StripeCreator.WPF
{
    /// <summary>
    /// Класс view-model для страницы работы со справочниками
    /// </summary>
    public class DataStorePageViewModel : BaseViewModel
    {
        #region Constants

        private const string NonSelectedEntityError = "Не выбранна сущность для взаимодействия";

        #endregion

        #region Private fields

        /// <summary>
        /// Заголовок меню действий
        /// </summary>
        private readonly string _header = "Справочники";

        /// <summary>
        /// ViewModel приложения
        /// </summary>
        private readonly ApplicationViewModel _applicationViewModel;

        #region Services

        /// <summary>
        /// Сервис работы с ViewModel доменных сущностей
        /// </summary>
        private IDataFacade? _dataFacade;

        /// <summary>
        /// Сервис работы с клиентами
        /// </summary>
        private readonly ClientFacade _clientFacade;

        /// <summary>
        /// Сервис работы с нитями
        /// </summary>
        private readonly ThreadFacade _threadFacade;

        /// <summary>
        /// Сервис работы с тканями
        /// </summary>
        private readonly ClothFacade _clothFacade;

        /// <summary>
        /// Сервис работы с продуктами
        /// </summary>
        private readonly ProductFacade _productFacade;

        #endregion

        /// <summary>
        /// Менеджер интерактивного взаимодействия
        /// </summary>
        private readonly IUiManager _uiManager;

        #endregion

        #region Public Properties 

        /// <summary>
        /// ViewModel меню действий
        /// </summary>
        public ActionMenuViewModel ActionMenuViewModel { get; private set; }

        /// <summary>
        /// Список хранимых сущностей
        /// </summary>
        public ObservableCollection<IEntityViewModel>? Entities { get; protected set; }

        /// <summary>
        /// Выбранная в списке сущность
        /// </summary>
        public IEntityViewModel? SelectedEntity { get; set; }

        /// <summary>
        /// Сервис работы с сущностью
        /// </summary>
        public IDataFacade? DataFacade
        {
            get => _dataFacade;
            set
            {
                _dataFacade = value;
                RefreshCommand.Execute(null);
            }
        }

        /// <summary>
        /// Название сущности
        /// </summary>
        public string DataHeader { get; protected set; } = "Сущности";

        #region Commands

        /// <summary>
        /// Показать список хранимых нитей
        /// </summary>
        public ICommand ShowThreadStoreCommand { get; }

        /// <summary>
        /// Показать список хранимых тканей
        /// </summary>
        public ICommand ShowClothStoreCommand { get; }

        /// <summary>
        /// Показать список хранимых клиентов
        /// </summary>
        public ICommand ShowClientStoreCommand { get; }

        /// <summary>
        /// Показать список хранимой продукции
        /// </summary>
        public ICommand ShowProductStoreCommand { get; }

        /// <summary>
        /// Команда добавления новой сущности
        /// </summary>
        public ICommand AddCommand { get; }

        /// <summary>
        /// Команда редактирования сущности
        /// </summary>
        public ICommand EditCommand { get; }

        /// <summary>
        /// Команда удаления сущности
        /// </summary>
        public ICommand RemoveCommand { get; }

        /// <summary>
        /// Команда обновления списка хранимых сущностей
        /// </summary>
        public ICommand RefreshCommand { get; }

        /// <summary>
        /// Предикат для команд обращения к сервису
        /// </summary>
        public bool ServiceAvailable => _dataFacade != null;

        /// <summary>
        /// Предикат для команд изменений сущности
        /// </summary>
        public bool EditingEnabled => ServiceAvailable && SelectedEntity != null;

        #endregion

        #endregion

        #region Constructors

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
#nullable disable
        public DataStorePageViewModel() { }
#nullable enable

        /// <summary>
        /// Конструктор с полной инициализацией
        /// </summary>
        /// <param name="applicationViewModel">ViewModel приложения</param>
        /// <param name="uiManager">Менеджер интерактивного взаимодействия</param>
        public DataStorePageViewModel(ApplicationViewModel applicationViewModel, IUiManager uiManager,
            ClientFacade clientService, ThreadFacade threadService, ClothFacade clothService, ProductFacade productService)
        {
            _applicationViewModel = applicationViewModel;
            _uiManager = uiManager;
            _clientFacade = clientService;
            _threadFacade = threadService;
            _clothFacade = clothService;
            _productFacade = productService;


            // Инициализация команд
            ShowThreadStoreCommand = new RelayCommand(async param => await ShowThreadStore(param));
            ShowClothStoreCommand = new RelayCommand(async param => await ShowClothStore(param));
            ShowClientStoreCommand = new RelayCommand(async param => await ShowClientStore(param));
            ShowProductStoreCommand = new RelayCommand(async param => await ShowProductStore(param));

            AddCommand = new RelayCommand(async param => await OnExecutedAddCommand(param)) { CanExecutePredicate = CanExecuteAddCommand };
            EditCommand = new RelayCommand(async param => await OnExecutedEditCommand(param)) { CanExecutePredicate = CanExecuteEditCommand };
            RemoveCommand = new RelayCommand(async param => await OnExecutedRemoveCommand(param)) { CanExecutePredicate = CanExecuteRemoveCommand };
            RefreshCommand = new RelayCommand(async param => await OnExecutedRefreshCommand(param)) { CanExecutePredicate = CanExecuteRefreshCommand };

            ActionMenuViewModel = new(_header, GetSideMenuItems());
        }

        #endregion

        #region Private helper methods

        /// <summary>
        /// Получить список элементов меню действий
        /// </summary>
        /// <returns>Список элементов меню действий</returns>
        private List<ActionMenuItemViewModel> GetSideMenuItems()
        {
            return new List<ActionMenuItemViewModel>
            {
                new(EFontAwesomeIcon.Solid_Bars, "Нитки", ShowThreadStoreCommand),
                new(EFontAwesomeIcon.Solid_CropAlt, "Ткани", ShowClothStoreCommand),
                new(EFontAwesomeIcon.Solid_Users, "Клиенты", ShowClientStoreCommand),
                new(EFontAwesomeIcon.Brands_Slack, "Продукты", ShowProductStoreCommand),
                new(EFontAwesomeIcon.Solid_ArrowLeft, "В меню", ShowMenu)
            };
        }

        #region Commands action and predicate

        /// <summary>
        /// Показать главное меню
        /// </summary>
        /// <param name="parameter">Параметр команды</param>
        private void ShowMenu(object? parameter) => _applicationViewModel.GoToPage(ApplicationPage.Welcome);

        /// <summary>
        /// Показать список хранимых нитей
        /// </summary>
        /// <param name="parameter">Параметр команды</param>
        private async Task ShowThreadStore(object? parameter)
        {
            try
            {
                DataFacade = _threadFacade;
                DataHeader = "Нити";
            }
            catch (Exception ex)
            {
                await _uiManager.ShowError(new("Ошибка загрузки нитей", ex.Message));
            }
        }

        /// <summary>
        /// Показать список хранимых тканей
        /// </summary>
        /// <param name="parameter">Параметр команды</param>
        private async Task ShowClothStore(object? parameter)
        {
            try
            {
                DataFacade = _clothFacade;
                DataHeader = "Ткани";
            }
            catch (Exception ex)
            {
                await _uiManager.ShowError(new("Ошибка загрузки тканей", ex.Message));
            }
        }

        /// <summary>
        /// Показать список хранимых нитей
        /// </summary>
        /// <param name="parameter">Параметр команды</param>
        private async Task ShowClientStore(object? parameter)
        {
            try
            {
                DataFacade = _clientFacade;
                DataHeader = "Клиенты";
            }
            catch (Exception ex)
            {
                await _uiManager.ShowError(new("Ошибка загрузки клиентов", ex.Message));
            }
        }

        /// <summary>
        /// Показать список хранимых продуктов
        /// </summary>
        /// <param name="parameter">Параметр команды</param>
        private async Task ShowProductStore(object? parameter)
        {
            try
            {
                DataFacade = _productFacade;
                DataHeader = "Продукция";
            }
            catch (Exception ex)
            {
                await _uiManager.ShowError(new("Ошибка загрузки продукции", ex.Message));
            }
        }

        /// <summary>
        /// Добавить новую хранимую сущность
        /// </summary>
        /// <param name="parameter">Параметр команды</param>
        private async Task OnExecutedAddCommand(object? parameter)
        {
            try
            {
                var newEntity = await DataFacade!.CreateAsync();
                if (newEntity == null)
                {
                    await _uiManager.ShowInfo(new("Отмена", "Создание записи отменено"));
                    return;
                }
                Entities?.Add(newEntity);
            }
            catch (Exception ex)
            {
                await _uiManager.ShowError(new("Ошибка добавления сущности", ex.Message));
            }
        }

        /// <summary>
        /// Проверка вызова команды добавления данных
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        private bool CanExecuteAddCommand(object? parameter) => ServiceAvailable;

        /// <summary>
        /// Редактировать выбранную хранимую сущность
        /// </summary>
        /// <param name="parameter">Параметр команды</param>
        private async Task OnExecutedEditCommand(object? parameter)
        {
            if (Entities is null || SelectedEntity is null)
                throw new InvalidOperationException(NonSelectedEntityError);
            try
            {
                var changedEntity = await DataFacade!.EditAsync(SelectedEntity);
                if (changedEntity == null)
                {
                    _ = _uiManager.ShowInfo(new("Отмена", "Редактирование записи отменено"));
                    return;
                }
                Entities[Entities.IndexOf(SelectedEntity)] = changedEntity;
            }
            catch (Exception ex)
            {
                await _uiManager.ShowError(new("Ошибка редактирования сущности", ex.Message));
            }
        }

        /// <summary>
        /// Проверка вызова команды редактирования
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        private bool CanExecuteEditCommand(object? parameter) => EditingEnabled;

        /// <summary>
        /// Удалить выбранную хранимую сущность
        /// </summary>
        /// <param name="parameter">Параметр команды</param>
        private async Task OnExecutedRemoveCommand(object? parameter)
        {
            if (Entities is null || SelectedEntity is null)
                throw new InvalidOperationException(NonSelectedEntityError);
            try
            {
                var removeId = await DataFacade!.RemoveAsync(SelectedEntity);
                await _uiManager.ShowInfo(new MessageBoxDialogViewModel("Удалено успешно", $"Удалена сущность с Id {removeId}"));
                Entities?.Remove(SelectedEntity);
            }
            catch (Exception ex)
            {
                await _uiManager.ShowError(new("Ошибка удаления сущности", ex.Message));
            }
        }

        /// <summary>
        /// Проверка вызова команды удаления
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        private bool CanExecuteRemoveCommand(object? parameter) => EditingEnabled;

        /// <summary>
        /// Обновить список хранимых сущностей
        /// </summary>
        /// <param name="parameter">Параметр команды</param>
        private async Task OnExecutedRefreshCommand(object? parameter)
        {
            try
            {
                var data = await DataFacade!.GetAllAsync();
                Entities = new ObservableCollection<IEntityViewModel>(data);
            }
            catch (Exception ex)
            {
                await _uiManager.ShowError(new("Ошибка загрузки данных", ex.Message));
            }
        }

        /// <summary>
        /// Проверка вызова команды обновления данных
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        private bool CanExecuteRefreshCommand(object? parameter) => ServiceAvailable;

        #endregion

        #endregion
    }
}

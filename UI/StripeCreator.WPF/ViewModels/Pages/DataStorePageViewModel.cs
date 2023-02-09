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
        private IDataService? _dataService;

        /// <summary>
        /// Сервис работы с клиентами
        /// </summary>
        private readonly ClientService _clientService;

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
        /// Предикат для команд управления сущностью
        /// </summary>
        public bool ManagementEnabled => _dataService != null && SelectedEntity != null;

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
        public DataStorePageViewModel(ApplicationViewModel applicationViewModel, IUiManager uiManager, ClientService clientService)
        {
            _applicationViewModel = applicationViewModel;
            _uiManager = uiManager;
            _clientService = clientService;
            ActionMenuViewModel = new(_header, GetSideMenuItems());

            // Инициализация команд
            ShowThreadStoreCommand = new RelayCommand(ShowThreadStore);
            ShowClothStoreCommand = new RelayCommand(ShowClothStore);
            ShowClientStoreCommand = new RelayCommand(ShowClientStore);

            AddCommand = new RelayCommand(async param => await OnExecutedAddCommand(param)) { CanExecutePredicate = CanExecuteAddCommand };
            EditCommand = new RelayCommand(async param => await OnExecutedEditCommand(param)) { CanExecutePredicate = CanExecuteEditCommand };
            RemoveCommand = new RelayCommand(async param => await OnExecutedRemoveCommand(param)) { CanExecutePredicate = CanExecuteRemoveCommand };
            RefreshCommand = new RelayCommand(async param => await OnExecutedRefreshCommand(param)) { CanExecutePredicate = CanExecuteRefreshCommand };
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
                new(EFontAwesomeIcon.Solid_Bars, "Нитки", ShowThreadStore),
                new(EFontAwesomeIcon.Solid_CropAlt, "Ткани", ShowClothStore),
                new(EFontAwesomeIcon.Solid_Users, "Клиенты", ShowClientStore),
            };
        }

        #region Commands action and predicate

        /// <summary>
        /// Показать список хранимых нитей
        /// </summary>
        /// <param name="parameter">Параметр команды</param>
        private void ShowThreadStore(object? parameter) => _applicationViewModel.GoToPage(ApplicationPage.Welcome);

        /// <summary>
        /// Показать список хранимых тканей
        /// </summary>
        /// <param name="parameter">Параметр команды</param>
        private void ShowClothStore(object? parameter) { }

        /// <summary>
        /// Показать список хранимых нитей
        /// </summary>
        /// <param name="parameter">Параметр команды</param>
        private void ShowClientStore(object? parameter)
        {
            _dataService = _clientService;
            RefreshCommand.Execute(parameter);
        }

        /// <summary>
        /// Добавить новую хранимую сущность
        /// </summary>
        /// <param name="parameter">Параметр команды</param>
        private async Task OnExecutedAddCommand(object? parameter)
        {
            // TODO: Логика создания новой сущности
        }

        /// <summary>
        /// Проверка вызова команды добавления данных
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        private bool CanExecuteAddCommand(object? parameter) => _dataService != null;

        /// <summary>
        /// Редактировать выбранную хранимую сущность
        /// </summary>
        /// <param name="parameter">Параметр команды</param>
        private async Task OnExecutedEditCommand(object? parameter)
        {
            // TODO: Логика редактирования...
        }

        /// <summary>
        /// Проверка вызова команды редактирования
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        private bool CanExecuteEditCommand(object? parameter) => ManagementEnabled;

        /// <summary>
        /// Удалить выбранную хранимую сущность
        /// </summary>
        /// <param name="parameter">Параметр команды</param>
        private async Task OnExecutedRemoveCommand(object? parameter)
        {
            if (SelectedEntity is null)
                throw new InvalidOperationException(NonSelectedEntityError);
            var removeId = await _dataService!.RemoveAsync(SelectedEntity);
            await _uiManager.ShowInfo(new MessageBoxDialogViewModel("Удалено успешно", $"Удалена сущность с Id {removeId}"));
            Entities?.Remove(SelectedEntity);
        }

        /// <summary>
        /// Проверка вызова команды удаления
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        private bool CanExecuteRemoveCommand(object? parameter) => ManagementEnabled;

        /// <summary>
        /// Обновить список хранимых сущностей
        /// </summary>
        /// <param name="parameter">Параметр команды</param>
        private async Task OnExecutedRefreshCommand(object? parameter)
        {
            var data = await _dataService!.GetAllAsync();
            Entities = new ObservableCollection<IEntityViewModel>(data);
        }

        /// <summary>
        /// Проверка вызова команды обновления данных
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        private bool CanExecuteRefreshCommand(object? parameter) => _dataService != null;

        #endregion

        #endregion
    }
}

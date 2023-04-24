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
        public IDataService? DataService
        {
            get => _dataService;
            set
            {
                _dataService = value;
                RefreshCommand?.Execute(null);
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
        public ICommand? AddCommand { get; private set; }

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
        public ICommand? RefreshCommand { get; private set; }

        /// <summary>
        /// Предикат для команд обращения к сервису
        /// </summary>
        public bool ServiceAvailable => true;

        /// <summary>
        /// Предикат для команд изменений сущности
        /// </summary>
        public bool EditingEnabled => true;

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
        public DataStorePageViewModel(ApplicationViewModel applicationViewModel, IUiManager uiManager)
        {
            _applicationViewModel = applicationViewModel;
            _uiManager = uiManager;

            // Инициализация команд
            ShowThreadStoreCommand = new RelayCommand(ShowThreadStore);
            ShowClothStoreCommand = new RelayCommand(ShowClothStore);
            ShowClientStoreCommand = new RelayCommand(ShowClientStore);
            ShowProductStoreCommand = new RelayCommand(ShowProductStore);

            EditCommand = new RelayCommand(async param => await OnExecutedEditCommand(param)) { CanExecutePredicate = CanExecuteEditCommand };
            RemoveCommand = new RelayCommand(async param => await OnExecutedRemoveCommand(param)) { CanExecutePredicate = CanExecuteRemoveCommand };

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
        private void ShowThreadStore(object? parameter)
        {
            if (RefreshCommand is GetAllThreadsCommand) return;

            var getAllThreadCommand = CommandLocator.GetAllThreadsCommand;
            getAllThreadCommand.DataLoaded = data =>
            {
                Entities = new ObservableCollection<IEntityViewModel>(data);
                DataHeader = "Нити";
            };
            getAllThreadCommand.DataLoadingError = message =>
            {
                Task.Run(async () => await _uiManager.ShowError(new("Ошибка загрузки нитей", message)));
            };
            RefreshCommand = getAllThreadCommand;
            RefreshCommand.Execute(parameter);
        }

        /// <summary>
        /// Показать список хранимых тканей
        /// </summary>
        /// <param name="parameter">Параметр команды</param>
        private void ShowClothStore(object? parameter)
        {
            if (RefreshCommand is GetAllClothsCommand) return;
            var getAllCommand = CommandLocator.GetAllClothsCommand;
            getAllCommand.DataLoaded = data =>
            {
                Entities = new ObservableCollection<IEntityViewModel>(data);
                DataHeader = "Ткани";
            };
            getAllCommand.DataLoadingError = message =>
            {
                Task.Run(async () =>
                await _uiManager.ShowError(new("Ошибка загрузки тканей", message)));
            };
            RefreshCommand = getAllCommand;
            RefreshCommand.Execute(parameter);
        }

        /// <summary>
        /// Показать список хранимых клиентов
        /// </summary>
        /// <param name="parameter">Параметр команды</param>
        private void ShowClientStore(object? parameter)
        {
            if (RefreshCommand is GetAllClientsCommand) return;
            var getAllCommand = CommandLocator.GetAllClientsCommand;
            getAllCommand.DataLoaded = data =>
            {
                Entities = new ObservableCollection<IEntityViewModel>(data);
                DataHeader = "Клиенты";
            };
            getAllCommand.DataLoadingError = message =>
            {
                Task.Run(async () =>
                await _uiManager.ShowError(new("Ошибка загрузки клиентов", message)));
            };
            RefreshCommand = getAllCommand;
            RefreshCommand.Execute(parameter);

            var saveCommand = CommandLocator.SaveClientCommand;

            addCommand.DataSaved = data =>
            {
                Entities?.Add(data);
            };
            addCommand.DataSavingError = message =>
            {
                Task.Run(async () => await _uiManager.ShowError(new("Ошибка добавления клиента", message)));
            };

            AddCommand = addCommand;
        }

        /// <summary>
        /// Показать список хранимых продуктов
        /// </summary>
        /// <param name="parameter">Параметр команды</param>
        private void ShowProductStore(object? parameter)
        {
            if (RefreshCommand is GetAllProductsCommand) return;
            var getAllCommand = CommandLocator.GetAllProductsCommand;
            getAllCommand.DataLoaded = data =>
            {
                Entities = new ObservableCollection<IEntityViewModel>(data);
                DataHeader = "Продукция";
            };
            getAllCommand.DataLoadingError = message =>
            {
                Task.Run(async () =>
                await _uiManager.ShowError(new("Ошибка загрузки продукции", message)));
            };
            RefreshCommand = getAllCommand;
            RefreshCommand.Execute(parameter);
        }

        /// <summary>
        /// Редактировать выбранную хранимую сущность
        /// </summary>
        /// <param name="parameter">Параметр команды</param>
        private async Task OnExecutedEditCommand(object? parameter)
        {
            if (Entities is null || SelectedEntity is null)
                throw new InvalidOperationException(NonSelectedEntityError);
            var formationViewModel = DataService!.CreateFormationViewModel(SelectedEntity);
            var changedEntity = await _uiManager.FormationEntity(formationViewModel);
            if (changedEntity == null)
            {
                await _uiManager.ShowInfo(new("Отмена", "Редактирование записи отменено"));
                return;
            }
            try
            {
                var newEntity = await DataService.SaveAsync(changedEntity);
                Entities[Entities.IndexOf(SelectedEntity)] = newEntity;
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
                var removeId = await _dataService!.RemoveAsync(SelectedEntity);
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

        #endregion

        #endregion
    }
}

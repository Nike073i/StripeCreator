using FontAwesome5;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace StripeCreator.WPF
{
    /// <summary>
    /// Класс view-model для страницы работы со справочниками
    /// </summary>
    public class DataStorePageViewModel : BaseViewModel
    {
        #region Private fields

        /// <summary>
        /// Заголовок меню действий
        /// </summary>
        private readonly string _header = "Справочники";

        /// <summary>
        /// ViewModel приложения
        /// </summary>
        private readonly ApplicationViewModel _applicationViewModel;

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
        public DataStorePageViewModel(ApplicationViewModel applicationViewModel)
        {
            _applicationViewModel = applicationViewModel;
            ActionMenuViewModel = new(_header, GetSideMenuItems());

            // Инициализация команд
            AddCommand = new RelayCommand(OnExecutedAddCommand);
            EditCommand = new RelayCommand(OnExecutedEditCommand);
            RemoveCommand = new RelayCommand(OnExecutedRemoveCommand);
            RefreshCommand = new RelayCommand(OnExecutedRefreshCommand);
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
                new(EFontAwesomeIcon.Solid_CropAlt, "Ткани",ShowClothStore),
            };
        }

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
        /// Добавить новую хранимую сущность
        /// </summary>
        /// <param name="parameter">Параметр команды</param>
        private void OnExecutedAddCommand(object? parameter) { }

        /// <summary>
        /// Редактировать выбранную хранимую сущность
        /// </summary>
        /// <param name="parameter">Параметр команды</param>
        private void OnExecutedEditCommand(object? parameter) { }

        /// <summary>
        /// Удалить выбранную хранимую сущность
        /// </summary>
        /// <param name="parameter">Параметр команды</param>
        private void OnExecutedRemoveCommand(object? parameter) { }

        /// <summary>
        /// Обновить список хранимых сущностей
        /// </summary>
        /// <param name="parameter">Параметр команды</param>
        private void OnExecutedRefreshCommand(object? parameter) { }

        #endregion
    }
}

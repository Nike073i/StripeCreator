using FontAwesome5;
using StripeCreator.WPF.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;

namespace StripeCreator.WPF
{
    /// <summary>
    /// Класс view-model для страницы работы сообществом
    /// </summary>
    public class CommunityPageViewModel : BaseViewModel
    {
        #region Private fields

        /// <summary>
        /// Заголовок меню действий
        /// </summary>
        private readonly string _header = "Доступные действия";

        /// <summary>
        /// ViewModel приложения
        /// </summary>
        private readonly ApplicationViewModel _applicationViewModel;

        /// <summary>
        /// Менеджер интерактивного взаимодействия
        /// </summary>
        private readonly IUiManager _uiManager;

        /// <summary>
        /// Сервис взаимодействия с сообществом
        /// </summary>
        private readonly CommunityService _communityService;

        #endregion

        #region Public properties

        /// <summary>
        /// ViewModel меню действий
        /// </summary>
        public ActionMenuViewModel ActionMenuViewModel { get; protected set; }

        /// <summary>
        /// Список товаров сообщества
        /// </summary>
        public ObservableCollection<MarketViewModel>? Markets { get; protected set; }

        /// <summary>
        /// Выбранный товар
        /// </summary>
        public MarketViewModel? SelectedMarket { get; set; }

        #region Commands

        /// <summary>
        /// Команда выхода в главное меню
        /// </summary>
        public ICommand MenuCommand { get; }

        /// <summary>
        /// Команда добавления нового товара
        /// </summary>
        public ICommand AddCommand { get; }

        /// <summary>
        /// Команда редактирования товара
        /// </summary>
        public ICommand EditCommand { get; }

        /// <summary>
        /// Команда удаления товара
        /// </summary>
        public ICommand RemoveCommand { get; }

        /// <summary>
        /// Команда обновления списка товаров
        /// </summary>
        public ICommand RefreshCommand { get; }

        /// <summary>
        /// Команда публикации новой записи
        /// </summary>
        public ICommand PostCommand { get; }

        /// <summary>
        /// Предикат для команд изменений сущности
        /// </summary>
        public bool EditingEnabled => SelectedMarket != null;

        #endregion

        #endregion

        #region Constructors

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
#nullable disable
        public CommunityPageViewModel() { }
#nullable enable

        /// <summary>
        /// Конструктор с полной инициализацией
        /// </summary>
        /// <param name="applicationViewModel">ViewModel приложения</param>
        public CommunityPageViewModel(ApplicationViewModel applicationViewModel, IUiManager uiManager, CommunityService communityService)
        {
            _applicationViewModel = applicationViewModel;
            _uiManager = uiManager;
            _communityService = communityService;

            // Инициализация команд
            MenuCommand = new RelayCommand(OnExecutedMenuCommand);
            AddCommand = new RelayCommand(async param => await OnExecutedAddCommand(param));
            EditCommand = new RelayCommand(async param => await OnExecutedEditCommand(param)) { CanExecutePredicate = CanExecuteEditCommand };
            RemoveCommand = new RelayCommand(async param => await OnExecutedRemoveCommand(param)) { CanExecutePredicate = CanExecuteRemoveCommand };
            RefreshCommand = new RelayCommand(async param => await OnExecutedRefreshCommand(param));
            PostCommand = new RelayCommand(async param => await OnExecutedPostCommand(param));

            ActionMenuViewModel = new(_header, GetMenuItems());
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Получить список элементов меню действий
        /// </summary>
        /// <returns>Список элементов меню действий</returns>
        private List<ActionMenuItemViewModel> GetMenuItems() =>
            new()
            {
                new(EFontAwesomeIcon.Solid_Plus, "Добавить", AddCommand),
                new(EFontAwesomeIcon.Solid_Pen, "Изменить", EditCommand),
                new(EFontAwesomeIcon.Solid_Trash, "Удалить", RemoveCommand),
                new(EFontAwesomeIcon.Solid_SyncAlt, "Обновить", RefreshCommand),
                new(EFontAwesomeIcon.Brands_Vk, "Новая запись", PostCommand),
                new(EFontAwesomeIcon.Solid_ArrowLeft, "В меню", MenuCommand),
            };

        #region Commands actions

        /// <summary>
        /// Действие при команде выхода в главное меню
        /// </summary>
        /// <param name="parameter">Параметр команды</param>
        private void OnExecutedMenuCommand(object? parameter) => _applicationViewModel.GoToPage(ApplicationPage.Welcome);

        /// <summary>
        /// Действие при команде добавления товара
        /// </summary>
        /// <param name="parameter">Параметр команды</param>
        private async Task OnExecutedAddCommand(object? parameter)
        {
            try
            {
                var marketCreateModel = await _uiManager.CreateMarket();
                if (marketCreateModel == null)
                {
                    await _uiManager.ShowInfo(new("Отмена", "Создание товара отменено"));
                    return;
                }
                await _communityService.AddAsync(marketCreateModel);
                RefreshCommand.Execute(null);
                if (await _uiManager.ShowConfirm(new MessageBoxDialogViewModel("Новая запись", "Создать новую запись о товаре?")))
                {
                    PostCommand.Execute(new PublishMessageViewModel
                    {
                        Message = "В продаже появился новый товар!" + Environment.NewLine + marketCreateModel.Market.Title,
                        PhotoPath = marketCreateModel.PhotoPath
                    });
                }
            }
            catch (Exception ex)
            {
                await _uiManager.ShowError(new("Ошибка создания товара", ex.Message));
            }
        }

        /// <summary>
        /// Действие при команде редактирования товара
        /// </summary>
        /// <param name="parameter">Параметр команды</param>
        private async Task OnExecutedEditCommand(object? parameter)
        {
            try
            {
                var newMarket = await _uiManager.EditMarket(SelectedMarket!);
                if (newMarket == null)
                {
                    await _uiManager.ShowInfo(new("Отмена", "Редактирование товара отменено"));
                    return;
                }
                await _communityService.EditAsync(newMarket);
                RefreshCommand.Execute(null);
            }
            catch (Exception ex)
            {
                await _uiManager.ShowError(new("Ошибка редактирования товара", ex.Message));
            }
        }

        /// <summary>
        /// Проверка вызова команды редактирования товара
        /// </summary>
        /// <param name="parameter">Параметр команды</param>
        private bool CanExecuteEditCommand(object? parameter) => EditingEnabled;

        /// <summary>
        /// Действие при команде удаления товара
        /// </summary>
        /// <param name="parameter">Параметр команды</param>
        private async Task OnExecutedRemoveCommand(object? parameter)
        {
            var selectedMarket = SelectedMarket!.Market;
            var isSuccessful = await _communityService.RemoveAsync(selectedMarket);
            if (isSuccessful)
            {
                await _uiManager.ShowInfo(new MessageBoxDialogViewModel("Удалено успешно", $"Удален товар {selectedMarket.Title}"));
                Markets?.Remove(SelectedMarket);
            }
            else
                await _uiManager.ShowInfo(new MessageBoxDialogViewModel("Отказ при удалении", $"Товар {selectedMarket.Title} не был удален"));
        }

        /// <summary>
        /// Проверка вызова команды удаления товара
        /// </summary>
        /// <param name="parameter">Параметр команды</param>
        /// <returns></returns>
        private bool CanExecuteRemoveCommand(object? parameter) => EditingEnabled;

        /// <summary>
        /// Действие при команде обновления списка товаров
        /// </summary>
        /// <param name="parameter">Параметр команды</param>
        private async Task OnExecutedRefreshCommand(object? parameter)
        {
            var data = await _communityService.GetAllAsync();
            Markets = new ObservableCollection<MarketViewModel>(data);
        }

        /// <summary>
        /// Действие при команде публикации новой записи
        /// </summary>
        /// <param name="parameter">Параметр команды</param>
        private async Task OnExecutedPostCommand(object? parameter)
        {
            try
            {
                var publishViewModel = parameter as PublishMessageViewModel;
                var publishMessageModel = await _uiManager.PublishMessage(publishViewModel);
                if (publishMessageModel == null)
                {
                    await _uiManager.ShowInfo(new("Отмена", "Публикация записи отменена"));
                    return;
                }
                await _communityService.PostMessageAsync(publishMessageModel);
                await _uiManager.ShowInfo(new("Успех", "Новая запись опубликована"));
            }
            catch
            {
                await _uiManager.ShowError(new("Ошибка публикации записи", "Возникла ошибка публикации записи"));
            }
        }

        #endregion

        #endregion
    }
}

using Microsoft.Win32;
using StripeCreator.Core.Models;
using StripeCreator.Stripe.Models;
using StripeCreator.Stripe.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace StripeCreator.WPF
{
    /// <summary>
    /// Класс view-model для страницы обработки изображения
    /// </summary>
    public class ImageProcessingPageViewModel : BaseViewModel
    {
        #region Private fields

        /// <summary>
        /// Адрес ресурсов
        /// </summary>
        private readonly Uri _resourceUri = new("Resources/Localizations/ImageProcessing.xaml", UriKind.Relative);

        /// <summary> 
        /// Ресурсы приложения
        /// </summary>
        private readonly System.Windows.ResourceDictionary _resources;

        /// <summary>
        /// ViewModel приложения
        /// </summary>
        private readonly ApplicationViewModel _applicationViewModel;

        /// <summary>
        /// Сервис работы с изображением
        /// </summary>
        private readonly ImageService _imageFacade;

        /// <summary>
        /// Сервис тканей
        /// </summary>
        private readonly ClothService _clothService;

        /// <summary>
        /// Менеджер интерактивного взаимодействия
        /// </summary>
        private readonly IUiManager _uiManager;

        #endregion

        #region Public Properties

        /// <summary>
        /// Адрес изображения для обработки
        /// </summary>
        public string? ImagePath { get; set; }

        /// <summary>
        /// Выбранный метод дискретизации
        /// </summary>
        public ResizeMethodViewModel? SelectedResizeMethod { get; set; }

        /// <summary>
        /// Распространенные каунты тканей
        /// </summary>
        public int[] ClothCounts => new[] { 31, 39, 47, 55, 63, 71 };

        /// <summary>
        /// Список доступных методов уменьшения цветов
        /// </summary>
        public ObservableCollection<ReductiveMethodViewModel> ReductiveMethods { get; }

        /// <summary>
        /// Список доступных методов дискретизации
        /// </summary>
        public ObservableCollection<ResizeMethodViewModel> ResizeMethods { get; }

        /// <summary>
        /// Выбранный метод уменьшения цветов
        /// </summary>
        public ReductiveMethodViewModel? SelectedReductiveMethod { get; set; }

        /// <summary>
        /// Аргумент для метода уменьшения цветов.
        /// </summary>
        public int ReductiveCount { get; set; } = 8;

        /// <summary>
        /// Флаг установки значения у распределения цветов
        /// </summary>
        public bool IsColorNormalizeSet { get; set; }

        /// <summary>
        /// Выбранный каунт из распространненых
        /// </summary>
        public int SelectedClothCount { get; set; }

        /// <summary>
        /// Список хранимых тканей
        /// </summary>
        public IEnumerable<Cloth>? Cloths { get; protected set; }

        /// <summary>
        /// Выбранная ткань
        /// </summary>
        public Cloth? SelectedCloth { get; set; }

        /// <summary>
        /// Флаг использования каунта
        /// Если true, то из выбранной ткани
        /// Если false, то из списка распространненых 
        /// </summary>
        public bool IsClothData { get; set; } = true;

        /// <summary>
        /// Список доступных размеров вышивки
        /// </summary>
        public IEnumerable<Size>? AvailableStripeSizes { get; protected set; }

        /// <summary>
        /// Выбранный размер вышивки
        /// </summary>
        public Size? SelectedStripeSize { get; set; }

        #region Commands

        /// <summary>
        /// Команда выбора изображения
        /// </summary>
        public ICommand ChooseCommand { get; }

        /// <summary>
        /// Команда обработки изображения
        /// </summary>
        public ICommand HandleCommand { get; }

        /// <summary>
        /// Команда выхода в главное меню
        /// </summary>
        public ICommand MenuCommand { get; }

        /// <summary>
        /// Команда загрузки данных по тканям
        /// </summary>
        public ICommand LoadClothsCommand { get; }

        #endregion

        #endregion

        #region Constructors

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
#nullable disable
        public ImageProcessingPageViewModel() { }
#nullable enable

        /// <summary>
        /// Конструктор с полной инициализацией
        /// </summary>
        /// <param name="applicationViewModel">ViewModel приложения</param>
        /// <param name="imageService">Сервис работы с изображением</param>
        /// <param name="clothService">Репозиторий тканей</param>
        /// <param name="uiManager">Менеджер интерактивного взаимодействия</param>
        public ImageProcessingPageViewModel(ApplicationViewModel applicationViewModel, ImageService imageService, ClothService clothService, IUiManager uiManager)
        {
            _applicationViewModel = applicationViewModel;
            _imageFacade = imageService;
            _clothService = clothService;
            _uiManager = uiManager;
            SelectedClothCount = ClothCounts.First();

            _resources = new System.Windows.ResourceDictionary
            {
                Source = _resourceUri
            };

            ResizeMethods = new ObservableCollection<ResizeMethodViewModel>
            {
                new ResizeMethodViewModel(ResizeMethod.Adaptive,
                    _resources["ResizeMethod_Adaptive_Label"]?.ToString() ?? ResizeMethod.Adaptive.ToString(),
                    _resources["ResizeMethod_Adaptive_Description"]?.ToString() ?? string.Empty),
                new ResizeMethodViewModel(ResizeMethod.Sample,
                    _resources["ResizeMethod_Sample_Label"]?.ToString() ?? ResizeMethod.Sample.ToString(),
                    _resources["ResizeMethod_Sample_Description"]?.ToString() ?? string.Empty),
                new ResizeMethodViewModel(ResizeMethod.Scale,
                    _resources["ResizeMethod_Scale_Label"]?.ToString() ?? ResizeMethod.Scale.ToString(),
                    _resources["ResizeMethod_Scale_Description"]?.ToString() ?? string.Empty),
                new ResizeMethodViewModel(ResizeMethod.Liquid,
                    _resources["ResizeMethod_Liquid_Label"] ?.ToString() ?? ResizeMethod.Liquid.ToString(),
                    _resources["ResizeMethod_Liquid_Description"] ?.ToString() ?? string.Empty)
            };

            ReductiveMethods = new ObservableCollection<ReductiveMethodViewModel>
            {
                new ReductiveMethodViewModel(ReductiveMethod.Quantization,
                    _resources["ReductiveMethod_Quantization_Label"]?.ToString() ?? ReductiveMethod.Quantization.ToString(),
                    _resources["ReductiveMethod_Quantization_Description"]?.ToString() ?? string.Empty),
                new ReductiveMethodViewModel(ReductiveMethod.Posterization,
                    _resources["ReductiveMethod_Posterization_Label"]?.ToString() ?? ReductiveMethod.Posterization.ToString(),
                    _resources["ReductiveMethod_Posterization_Description"]?.ToString() ?? string.Empty)
            };

            //Инициализация комманд
            MenuCommand = new RelayCommand(OnExecutedMenuCommand);
            ChooseCommand = new RelayCommand(OnExecutedChooseCommand);
            HandleCommand = new RelayCommand(async (param) => await OnExecutedHandleCommand(param)) { CanExecutePredicate = CanExecuteHandleCommand };
            LoadClothsCommand = new RelayCommand(async (param) => await OnExecutedLoadClothsCommand(param));
        }

        #endregion

        #region Command actions

        /// <summary>
        /// Действие при команде выхода в главное меню
        /// </summary>
        /// <param name="parameter">Параметр команды</param>
        private void OnExecutedMenuCommand(object? parameter) => _applicationViewModel.GoToPage(ApplicationPage.Welcome);

        /// <summary>
        /// Действие при команде выбора изображения
        /// </summary>
        /// <param name="parameter">Параметр команды</param>
        private void OnExecutedChooseCommand(object? parameter)
        {
            var dialog = new OpenFileDialog
            {
                Filter = "Файлы рисунков (*.bmp, *.jpg, *.jpeg, *.png)|*.bmp;*.jpg;*.jpeg;*.png"
            };
            if (dialog.ShowDialog() == false) return;
            ImagePath = dialog.FileName;
            var imageSize = ImageService.GetImageSize(ImagePath);
            AvailableStripeSizes = ImageService.GetAvailableStripeSizes(imageSize.Width, imageSize.Height);
        }

        /// <summary>
        /// Действие при команде обработки изображения
        /// </summary>
        /// <param name="parameter">Параметр команды</param>
        private async Task OnExecutedHandleCommand(object? parameter)
        {
            var count = IsClothData ? SelectedCloth!.Count : SelectedClothCount;
            var stripeSize = SelectedStripeSize!;
            var maxSize = Math.Max(stripeSize.Width, stripeSize.Height);
            try
            {
                await Task.Run(async () =>
                {
                    var schemeTemplate = await _imageFacade.CreateSchemaTemplate(ImagePath!, count, maxSize,
                        SelectedResizeMethod!.Method,
                        SelectedReductiveMethod!.Method, ReductiveCount,
                        IsColorNormalizeSet);
                    _applicationViewModel.GoToPage(ApplicationPage.Scheme, (schemeTemplate, count));
                });
            }
            catch (Exception ex)
            {
                await _uiManager.ShowError(new("Ошибка обработки изображения", ex.Message));
            }
        }

        /// <summary>
        /// Проверка вызова команды обработки изображения
        /// </summary>
        /// <param name="parameter">Параметр команды</param>
        private bool CanExecuteHandleCommand(object? parameter)
             => !string.IsNullOrWhiteSpace(ImagePath)
            && SelectedReductiveMethod != null
            && (!IsClothData || SelectedCloth != null)
            && ReductiveCount > 0
            && ReductiveCount <= 255
            && SelectedResizeMethod != null
            && SelectedStripeSize != null;

        /// <summary>
        /// Действие при команде обработки изображения
        /// </summary>
        /// <param name="parameter">Параметр команды</param>
        private async Task OnExecutedLoadClothsCommand(object? parameter)
        {
            try
            {
                Cloths = await _clothService.GetAllAsync();
            }
            catch (Exception ex)
            {
                await _uiManager.ShowError(new("Ошибка загрузки тканей", ex.Message));
            }
        }

        #endregion
    }
}

using FontAwesome5;
using Microsoft.Win32;
using StripeCreator.Core.Models;
using StripeCreator.Stripe.Interfaces;
using StripeCreator.Stripe.Models;
using StripeCreator.Stripe.Repositories;
using StripeCreator.Stripe.Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace StripeCreator.WPF
{
    /// <summary>
    /// Класс view-model для страницы работы со схемой
    /// </summary>
    public class SchemePageViewModel : BaseViewModel
    {
        #region Constants 

        private static readonly string DefaultColorHex = Color.DefaultColor;

        #endregion

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
        /// Репозиторий тканей
        /// </summary>
        private readonly IClothRepository _clothRepository;

        /// <summary>
        /// Репозиторий нитей
        /// </summary>
        private readonly IThreadRepository _threadRepository;

        /// <summary>
        /// Визуализатор схемы
        /// </summary>
        private readonly SchemeVisualizer _schemeVisualizer;

        /// <summary>
        /// Хранитель изображений
        /// </summary>
        private readonly IDataKeeper<Image> _imageKeeper;

        /// <summary>
        /// Хранитель схем
        /// </summary>
        private readonly IDataKeeper<Scheme> _schemeKeeper;

        /// <summary>
        /// Создатель описания схемы
        /// </summary>
        private readonly ISchemeDescriptor _schemeDescriptor;

        /// <summary>
        /// Схема вышивки
        /// </summary>
        private Scheme? _scheme;

        #endregion

        #region Public Properties

        /// <summary>
        /// ViewModel меню действий
        /// </summary>
        public ActionMenuViewModel ActionMenuViewModel { get; protected set; }

        /// <summary>
        /// Бинарные данные визуализации схемы вышивки
        /// </summary>
        public byte[]? SchemeData { get; set; }

        /// <summary>
        /// Вид отображения схемы
        /// </summary>
        public bool IsPixelView { get; set; } = true;

        /// <summary>
        /// Типы вышивки
        /// </summary>
        public static IEnumerable ЕmbroideryTypes => Enum.GetValues(typeof(EmbroideryType));

        /// <summary>
        /// Выбранный тип вышивки
        /// </summary>
        public EmbroideryType? SelectedЕmbroideryType { get; set; }

        /// <summary>
        /// Методы вышивки
        /// </summary>
        public static IEnumerable ЕmbroideryMethods => Enum.GetValues(typeof(EmbroideryMethod));

        /// <summary>
        /// Выбранный метод вышивки
        /// </summary>
        public EmbroideryMethod? SelectedЕmbroideryMethod { get; set; }

        /// <summary>
        /// Выбранный цвет ткани
        /// </summary>
        public string SelectedClothColor { get; set; } = DefaultColorHex;

        /// <summary>
        /// Схема вышивки
        /// </summary>
        public Scheme Scheme
        {
            get => _scheme ?? throw new InvalidOperationException("Использование схемы до инициализации");
            set
            {
                _scheme = value;
                InitializeSchemeData();
                RefreshColors();
            }
        }

        /// <summary>
        /// Цвета, используемые в схеме
        /// </summary>
        public IEnumerable<Color>? SchemeColors { get; set; }

        /// <summary>
        /// Количество цветов, используемых в схеме
        /// </summary>
        public int? SchemeColorsCount => SchemeColors?.Count();

        /// <summary>
        /// Ширина схемы
        /// </summary>
        public int? SchemeWidth { get; set; }

        /// <summary>
        /// Высота схемы
        /// </summary>
        public int? SchemeHeight { get; set; }

        /// <summary>
        /// Флаг использования сетки в схеме
        /// </summary>
        public bool IsGridActivated { get; set; }

        /// <summary>
        /// Выбранный цвет сетки
        /// </summary>
        public string GridColorHex { get; set; } = DefaultColorHex;

        /// <summary>
        /// Флаг использования отступа в схеме
        /// </summary>
        public bool IsIndentActivated { get; set; }

        /// <summary>
        /// Выбранный цвет отступа
        /// </summary>
        public string IndentColorHex { get; set; } = DefaultColorHex;

        /// <summary>
        /// Выбранный размер отступа в клетках
        /// </summary>
        public int IndentSize { get; set; } = 4;

        /// <summary>
        /// X Координата клетки
        /// </summary>
        public int CellCoordinateX { get; set; } = 0;

        /// <summary>
        /// Y Координата клетки
        /// </summary>
        public int CellCoordinateY { get; set; } = 0;

        /// <summary>
        /// Текущий цвет клетки с координатами <see cref="CellCoordinateX"/> <see cref="CellCoordinateY"/> 
        /// </summary>
        public string CurrentCellColorHex { get; set; } = DefaultColorHex;

        /// <summary>
        /// Новый цвет клетки с координатами <see cref="CellCoordinateX"/> <see cref="CellCoordinateY"/> 
        /// </summary>
        public string NewCellColorHex { get; set; } = DefaultColorHex;

        /// <summary>
        /// Текущий цвет схемы
        /// </summary>
        public string CurrentColorHex { get; set; } = DefaultColorHex;

        /// <summary>
        /// Новый цвет схемы
        /// </summary>
        public string NewColorHex { get; set; } = DefaultColorHex;

        #region Commands

        /// <summary>
        /// Команда отображения схемы
        /// </summary>
        public ICommand ShowSchemeCommand { get; }

        /// <summary>
        /// Команда сохранения схемы
        /// </summary>
        public ICommand SaveCommand { get; }

        /// <summary>
        /// Команда сохранения экспорта схемы как изображения
        /// </summary>
        public ICommand SaveImageCommand { get; }

        /// <summary>
        /// Команда расчета материалов
        /// </summary>
        public ICommand MaterialCalculateCommand { get; }

        /// <summary>
        /// Команда расчета стоимости
        /// </summary>
        public ICommand PriceCalculateCommand { get; }

        /// <summary>
        /// Команда выхода в главное меню
        /// </summary>
        public ICommand MenuCommand { get; }

        /// <summary>
        /// Команда получения цвета клетки
        /// </summary>
        public ICommand GetCellColorCommand { get; }

        /// <summary>
        /// Команда изменения цвета клетки
        /// </summary>
        public ICommand ChangeCellColorCommand { get; }

        /// <summary>
        /// Команда изменения цвета
        /// </summary>
        public ICommand ChangeColorCommand { get; }

        /// <summary>
        /// Команда создания описания схемы
        /// </summary>
        public ICommand CreateSchemeDecriptionCommand { get; }

        #endregion

        #endregion

        #region Constructors

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
#nullable disable
        public SchemePageViewModel() { }
#nullable enable

        /// <summary>
        /// Конструктор с полной инициализацией
        /// </summary>
        /// <param name="applicationViewModel">ViewModel приложения</param>
        public SchemePageViewModel(
            ApplicationViewModel applicationViewModel,
            SchemeVisualizer schemeVisualizer,
            IDataKeeper<Image> imageKeeper,
            IDataKeeper<Scheme> schemeKeeper,
            IUiManager uiManager,
            IClothRepository clothRepository,
            IThreadRepository threadRepository,
            ISchemeDescriptor schemeDescriptor)
        {
            _applicationViewModel = applicationViewModel;
            _schemeVisualizer = schemeVisualizer;
            _imageKeeper = imageKeeper;
            _schemeKeeper = schemeKeeper;
            _uiManager = uiManager;
            _clothRepository = clothRepository;
            _threadRepository = threadRepository;
            _schemeDescriptor = schemeDescriptor;

            //Инициализация комманд
            ShowSchemeCommand = new RelayCommand(OnExecutedShowSchemeCommand) { CanExecutePredicate = CanExecuteShowSchemeCommand };
            SaveCommand = new RelayCommand(async (param) => await OnExecutedSaveCommand(param)) { CanExecutePredicate = CanExecuteSaveCommand };
            SaveImageCommand = new RelayCommand(async (param) => await OnExecutedSaveImageCommand(param)) { CanExecutePredicate = CanExecuteSaveImageCommand };
            MaterialCalculateCommand = new RelayCommand(OnExecutedMaterialCalculateCommand);
            PriceCalculateCommand = new RelayCommand(async (param) => await OnExecutedPriceCalculateCommand(param));
            MenuCommand = new RelayCommand(OnExecutedMenuCommand);

            GetCellColorCommand = new RelayCommand(OnExecutedGetCellColorCommand) { CanExecutePredicate = CanExecuteGetCellColorCommand };
            ChangeCellColorCommand = new RelayCommand(OnExecutedChangeCellColorCommand) { CanExecutePredicate = CanExecuteChangeCellColorCommand };
            ChangeColorCommand = new RelayCommand(OnExecutedChangeColorCommand);

            CreateSchemeDecriptionCommand = new RelayCommand(async (param) => await OnExecutedCreateSchemeDescriptionCommand(param))
            { CanExecutePredicate = CanExecuteCreateSchemeDescriptionCommand };

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
                new(EFontAwesomeIcon.Solid_Eye, "Отобразить схему", ShowSchemeCommand),
                new(EFontAwesomeIcon.Solid_Save, "Сохранить схему", SaveCommand),
                new(EFontAwesomeIcon.Solid_Image, "Сохранить как изображение", SaveImageCommand),
                new(EFontAwesomeIcon.Solid_FilePrescription, "Сохранить описание схемы", CreateSchemeDecriptionCommand),
                new(EFontAwesomeIcon.Solid_Ruler, "Расчет материалов", MaterialCalculateCommand),
                new(EFontAwesomeIcon.Solid_RubleSign, "Расчет стоимости", PriceCalculateCommand),
                new(EFontAwesomeIcon.Solid_ArrowLeft, "В меню", MenuCommand),
            };

        /// <summary>
        /// Инициализация полей по данным схемы
        /// </summary>
        private void InitializeSchemeData()
        {
            SchemeWidth = Scheme.Width;
            SchemeHeight = Scheme.Height;
            if (Scheme.Grid != null)
            {
                GridColorHex = Scheme.Grid.Color.HexValue;
                IsGridActivated = true;
            }
            if (Scheme.Indent != null)
            {
                IndentColorHex = Scheme.Indent.Color.HexValue;
                IsIndentActivated = true;
                IndentSize = Scheme.Indent.Size;
            }
            CellCoordinateX = 0;
            CellCoordinateY = 0;
        }

        /// <summary>
        /// Обновление списка цветов схемы
        /// </summary>
        private void RefreshColors()
        {
            SchemeColors = Scheme.GetColors();
            GetCellColorCommand.Execute(null);
        }

        #region Command actions

        /// <summary>
        /// Проверка вызова команды отображения схемы
        /// </summary>
        /// <param name="parameter">Параметр команды</param>
        private bool CanExecuteShowSchemeCommand(object? parameter) =>
            (IsPixelView || SelectedЕmbroideryType != null && SelectedЕmbroideryMethod != null) &&
            (!IsIndentActivated || IndentSize > 0 && IndentSize <= 10);

        /// <summary>
        /// Действие при команде отображения схемы
        /// </summary>
        /// <param name="parameter">Параметр команды</param>
        private void OnExecutedShowSchemeCommand(object? parameter)
        {
            var grid = IsGridActivated ? new Grid(1, new Color(GridColorHex)) : null;
            Scheme.Grid = grid;
            var indent = IsIndentActivated ? new Indent(IndentSize, new Color(IndentColorHex)) : null;
            Scheme.Indent = indent;
            var clothColor = new Color(SelectedClothColor);
            Task.Run(() =>
            {
                var image = GetSchemeVisualization(clothColor);
                SchemeData = image.Data;
            });
        }

        /// <summary>
        /// Получить визуализацию схемы
        /// </summary>
        /// <param name="clothColor">Цвет ткани</param>
        /// <returns>Визуализация схемы</returns>
        private Image GetSchemeVisualization(Color clothColor, bool isPageScheme = true) =>
            IsPixelView ?
            _schemeVisualizer.CreateCellScheme(Scheme, isPageScheme) :
            _schemeVisualizer.CreatePrototypeScheme(Scheme, SelectedЕmbroideryType!.Value, SelectedЕmbroideryMethod!.Value, clothColor);

        /// <summary>
        /// Действие при команде сохранения схемы
        /// </summary>
        /// <param name="parameter">Параметр команды</param>
        private async Task OnExecutedSaveCommand(object? parameter)
        {
            var dialog = new SaveFileDialog
            {
                Filter = "Схема StripeCreator|*.sch"
            };
            if (dialog.ShowDialog() == false) return;
            try
            {
                await _schemeKeeper.SaveAsync(dialog.FileName, Scheme);
            }
            catch (Exception ex)
            {
                await _uiManager.ShowError(new("Ошибка сохранения схемы", ex.Message));
            }
        }

        /// <summary>
        /// Проверка вызова команды сохранения схемы
        /// </summary>
        /// <param name="parameter">Параметр команды</param>
        private bool CanExecuteSaveCommand(object? parameter) { return true; }

        /// <summary>
        /// Действие при команде экспорта схемы как изображения
        /// </summary>
        /// <param name="parameter">Параметр команды</param>
        private async Task OnExecutedSaveImageCommand(object? parameter)
        {
            var dialog = new SaveFileDialog
            {
                Filter = "Файлы рисунков (*.bmp, *.jpg, *.jpeg, *.png)|*.bmp;*.jpg;*.jpeg;*.png"
            };
            if (dialog.ShowDialog() == false) return;
            var grid = IsGridActivated ? new Grid(1, new Color(GridColorHex)) : null;
            Scheme.Grid = grid;
            var indent = IsIndentActivated ? new Indent(IndentSize, new Color(IndentColorHex)) : null;
            Scheme.Indent = indent;
            var clothColor = new Color(SelectedClothColor);
            try
            {
                await Task.Run(async () =>
                {
                    var fileName = dialog.FileName;
                    var image = GetSchemeVisualization(clothColor, false);
                    await _imageKeeper.SaveAsync(fileName, image);
                    if (await _uiManager.ShowConfirm(new("Схема", "Создать текстовое описание схемы?")))
                    {
                        CreateSchemeDecriptionCommand.Execute($"{fileName}-desc.txt");
                    }
                });
            }
            catch (Exception ex)
            {
                await _uiManager.ShowError(new("Ошибка визуализации схемы", ex.Message));
            }
        }

        /// <summary>
        /// Проверка вызова команды экспорта схемы как изображения
        /// </summary>
        /// <param name="parameter">Параметр команды</param>
        private bool CanExecuteSaveImageCommand(object? parameter) =>
            (IsPixelView || SelectedЕmbroideryType != null && SelectedЕmbroideryMethod != null) &&
            (!IsIndentActivated || IndentSize > 0 && IndentSize <= 10);

        /// <summary>
        /// Действие при команде расчета материлов
        /// </summary>
        /// <param name="parameter">Параметр команды</param>
        private void OnExecutedMaterialCalculateCommand(object? parameter) => _uiManager.CalculateMaterial(Scheme);

        /// <summary>
        /// Действие при команде расчета стоимости
        /// </summary>
        /// <param name="parameter">Параметр команды</param>
        private async Task OnExecutedPriceCalculateCommand(object? parameter)
        {
            try
            {
                var threads = await _threadRepository.GetAllAsync();
                if (!threads.Any())
                {
                    await _uiManager.ShowError(new("Ошибка расчета", "В хранилище отсутствуют нити"));
                    return;
                }
                var cloths = await _clothRepository.GetAllAsync();
                if (!cloths.Any())
                {
                    await _uiManager.ShowError(new("Ошибка расчета", "В хранилище отсутствуют ткани"));
                    return;
                }
                await _uiManager.CalculatePrice(Scheme, threads, cloths);
            }
            catch (Exception ex)
            {
                await _uiManager.ShowError(new("Ошибка расчета стоимости схемы", ex.Message));
            }
        }

        /// <summary>
        /// Действие при команде выхода в главное меню
        /// </summary>
        /// <param name="parameter">Параметр команды</param>
        private void OnExecutedMenuCommand(object? parameter) => _applicationViewModel.GoToPage(ApplicationPage.Welcome);

        /// <summary>
        /// Проверка вызова команды получения цвета клетки
        /// </summary>
        /// <param name="parameter">Параметр команды</param>
        private bool CanExecuteGetCellColorCommand(object? parameter) =>
            CellCoordinateX >= 0 && CellCoordinateX < SchemeWidth!.Value &&
            CellCoordinateY >= 0 && CellCoordinateY < SchemeHeight!.Value;

        /// <summary>
        /// Действие при команде изменения цвета клетки
        /// </summary>
        /// <param name="parameter">Параметр команды</param>
        private void OnExecutedGetCellColorCommand(object? parameter) =>
            CurrentCellColorHex = Scheme.GetColor(new PointPosition(CellCoordinateX, CellCoordinateY)).HexValue;

        /// <summary>
        /// Проверка вызова команды изменения цвета клетки
        /// </summary>
        /// <param name="parameter">Параметр команды</param>
        private bool CanExecuteChangeCellColorCommand(object? parameter) =>
            CellCoordinateX >= 0 && CellCoordinateX < SchemeWidth!.Value &&
            CellCoordinateY >= 0 && CellCoordinateY < SchemeHeight!.Value;

        /// <summary>
        /// Действие при команде изменения цвета клетки
        /// </summary>
        /// <param name="parameter">Параметр команды</param>
        private void OnExecutedChangeCellColorCommand(object? parameter)
        {
            Scheme.SetColor(new Color(NewCellColorHex), new PointPosition(CellCoordinateX, CellCoordinateY));
            RefreshColors();
        }

        /// <summary>
        /// Действие при команде изменения цвета
        /// </summary>
        /// <param name="parameter">Параметр команды</param>
        private void OnExecutedChangeColorCommand(object? parameter)
        {
            Scheme.ChangeColor(new Color(CurrentColorHex), new Color(NewColorHex));
            RefreshColors();
        }

        /// <summary>
        /// Действие при команде создания описания схемы
        /// </summary>
        /// <param name="parameter">Путь к месту сохранения описания</param>
        private async Task OnExecutedCreateSchemeDescriptionCommand(object? parameter)
        {
            if (parameter is not string path)
            {
                var dialog = new SaveFileDialog
                {
                    Filter = "Текстовые файлы (*.txt)|*.txt"
                };
                if (dialog.ShowDialog() == false) return;
                path = dialog.FileName;
            }
            var indent = IsIndentActivated ? new Indent(IndentSize, new Color(IndentColorHex)) : null;
            Scheme.Indent = indent;
            try
            {
                await Task.Run(() => _schemeDescriptor.SaveDescription(path, Scheme));
                await _uiManager.ShowInfo(new("Сохранено", "Описание сохранено успешно"));
            }
            catch (Exception ex)
            {
                await _uiManager.ShowError(new("Ошибка создания описания схемы", ex.Message));
            }
        }

        /// <summary>
        /// Проверка вызова команды создания описания схемы
        /// </summary>
        /// <param name="parameter">Путь к месту сохранения описания</param>
        private bool CanExecuteCreateSchemeDescriptionCommand(object? parameter) =>
            (!IsIndentActivated || IndentSize > 0 && IndentSize <= 10);

        #endregion

        #endregion
    }
}

using ImageMagick;
using Newtonsoft.Json;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;

namespace StripeCreator.TestConsole
{
    public class Program
    {
        private static string TestImageInputPath = @"Images/test.bmp";
        //private static string TestImageInputPath = @"Images/ДДТ.jpg";
        private static string TestImageOutputPath = @"Images/OutputImage.png";

        public static async Task Main(string[] args)
        {
            // Тест отрисовки схемы
            var imageKeeper = new ImageKeeper();
            var image = await imageKeeper.LoadImageAsync(TestImageInputPath);
            var converter = new SchemeConverter();
            var scheme = converter.CreateScheme(image);

            var viz = new SchemeVisualizer(scheme);
            scheme.Grid = new Grid(1, new Color("#AAAAAA"));
            //var newImage = viz.CreateCellScheme(5);
            var newImage = viz.CreatePrototypeScheme(ЕmbroideryType.SmoothVertical, EmbroideryMethod.In2Thread, 5);

            await imageKeeper.SaveImageAsync(TestImageOutputPath, newImage);
            //var cells = scheme.Cells;

            //int count = 0;
            //foreach (var cell in cells)
            //{
            //    if (count == 5)
            //        break;
            //    mi.Draw(viz.DrawableFilledCell(cell));
            //    //mi.Draw(viz.DrawableSymbolCell(cell, 'М'));
            //    //mi.Draw(viz.DrawableTypeCell(cell,ЕmbroideryType.Cross, cellSize:10));

            //    count++;
            //}

            //await mi.WriteAsync(TestImageOutputPath);

            //var newImage = converter.CreateImageFromScheme(scheme);
            //await imageKeeper.SaveImageAsync(TestImageOutputPath, newImage);

            // Тест работы с изображением
            //var imageKeeper = new ImageKeeper();
            //var image = await imageKeeper.LoadImageAsync(TestImageInputPath);
            //var converter = new SchemeConverter();
            //var scheme = converter.CreateScheme(image);
            //var newImage = converter.CreateImageFromScheme(scheme);
            //await imageKeeper.SaveImageAsync(TestImageOutputPath, newImage);
            //var schemeKeeper = new SchemeKeeper();
            //await schemeKeeper.SaveSchemeAsync("scheme1.json", scheme);
            //var schemeRestore = await schemeKeeper.LoadSchemeFromJsonAsync("scheme1.json");
            //var imageProccesor = new ImageProccesor(image);
            //imageProccesor.Trim();

            //Task.Run(() => imageKeeper.SaveImageAsync(TestImageOutputPath, imageProccesor.Image)).Wait();

            // Тест работы схемы
            //var scheme = new Scheme(10, 10);
            //var position = new CellPosition(9, 9);
            //var color = new CellColor("#000000");
            //scheme.SetCell(color, position);
            //var cells = scheme.Cells;
        }
    }

    /// <summary>
    /// Класс для визуализации схем
    /// </summary>
    public class SchemeVisualizer
    {
        #region Constants

        private readonly static Color DefaultColor = new("#000000");

        #endregion

        #region Private fields 

        private readonly Scheme _scheme;

        #endregion

        #region Constructors

        public SchemeVisualizer(Scheme scheme)
        {
            _scheme = scheme;
        }

        #endregion 

        #region Public methods

        /// <summary>
        /// Создание пиксельного изображения по схеме с размерами клетки <paramref name="cellSize"/>
        /// </summary>
        /// <param name="cellSize">Размер клетки в пикселях</param>
        /// <returns>Данные по изображению</returns>
        /// <exception cref="ArgumentOutOfRangeException">Возникает, если указан некорректный размер клетки для изображения</exception>
        public Image CreateCellScheme(int cellSize)
        {
            if (cellSize <= 0)
                throw new ArgumentOutOfRangeException(nameof(cellSize), "Размер клетки не может быть <= 0");

            var image = DrawFilledCells(cellSize, _scheme);

            var grid = _scheme.Grid;
            if (grid != null)
                image = DrawGrid(image, cellSize, grid.Size, grid.Color);

            return image;
        }

        /// <summary>
        /// Создание прототипного изображения по схеме с размерами клетки <paramref name="cellSize"/> 
        /// </summary>
        /// <param name="type">Вид вышивки</param>
        /// <param name="method">Способ вышивки</param>
        /// <param name="cellSize">Размер клетки в пикселях</param>
        /// <param name="backgroundColor">Фоновый цвет</param>
        /// <returns>Данные по изображению</returns>
        /// <exception cref="ArgumentOutOfRangeException">Возникает, если указан некорректный размер клетки для изображения</exception>
        public Image CreatePrototypeScheme(ЕmbroideryType type, EmbroideryMethod method, int cellSize, Color? backgroundColor = null)
        {
            if (cellSize <= 0)
                throw new ArgumentOutOfRangeException(nameof(cellSize), "Размер клетки не может быть <= 0");

            var image = DrawEmbroideryView(type, method, cellSize, _scheme, backgroundColor);

            var grid = _scheme.Grid;
            if (grid != null)
                image = DrawGrid(image, cellSize, grid.Size, grid.Color);

            return image;
        }

        #endregion

        #region Private helper methods

        /// <summary>
        /// Отрисовка закрашенных клеток по <paramref name="scheme"/> размером <paramref name="cellSize"/>
        /// </summary>
        /// <param name="cellSize">Размер клетки в сетке</param>
        /// <param name="scheme">Схема для отрисовки</param>
        /// <returns></returns>
        private Image DrawFilledCells(int cellSize, Scheme scheme)
        {
            // Отрисовываем клетки в виде пикселей
            var drawables = new Drawables();
            foreach (var cell in scheme.Cells)
            {
                var cellPosition = cell.Position;
                drawables.DrawableFilledPoint(cellPosition.X, cellPosition.Y, new MagickColor(cell.Color.HexValue));
            }
            var width = scheme.Width;
            var height = scheme.Height;
            var magickImage = new MagickImage(MagickColors.Transparent, width, height)
            {
                Format = MagickFormat.Png
            };

            magickImage.Draw(drawables);

            // Масштабируем пиксели до нужного размера клетки
            var newWidth = width * cellSize;
            var newHeigth = height * cellSize;
            magickImage.Sample(newWidth, newHeigth);

            return new Image(magickImage.ToByteArray(), newWidth, newHeigth);
        }

        /// <summary>
        /// Отрисовка сетки на изображении путем наращивания
        /// </summary>
        /// <param name="sourceImage">Исходное изображение для нанесения сетки</param>
        /// <param name="cellSize">Размер необходимой клетки в пикселях</param>
        /// <param name="gridSize">Размер сетки в пикселях</param>
        /// <param name="gridColor">Цвет сетки</param>
        /// <returns></returns>
        private Image DrawGrid(Image sourceImage, int cellSize, int gridSize, Color gridColor)
        {
            var magickImage = new MagickImage(sourceImage.Data);
            magickImage.DrawGrid(sourceImage.Width, sourceImage.Height, cellSize, gridSize, new MagickColor(gridColor.HexValue));
            return new Image(magickImage.ToByteArray(), magickImage.Width, magickImage.Height);
        }

        /// <summary>
        /// Отрисовка клеток в виде вышивки
        /// </summary>
        /// <param name="type">Вид вышивки</param>
        /// <param name="method">Способ вышивки</param>
        /// <param name="cellSize">Размер клетки</param>
        /// <param name="scheme">Данные схемы</param>
        /// <param name="backgroundColor">Фоновый цвет</param>
        /// <returns></returns>
        private Image DrawEmbroideryView(ЕmbroideryType type, EmbroideryMethod method, int cellSize, Scheme scheme, Color? backgroundColor = null)
        {
            backgroundColor ??= DefaultColor;

            var width = _scheme.Width * cellSize;
            var height = _scheme.Height * cellSize;

            // Создаем пустое изображение размером width x height с фоновым цветом
            using var magickImage = new MagickImage(new MagickColor(backgroundColor.HexValue), width, height)
            {
                Format = MagickFormat.Png
            };

            var drawables = new Drawables();

            // Устанавливаем свойства для способа вышивки
            if (method == EmbroideryMethod.In1Thread)
                drawables.DisableStrokeAntialias();
            else
                drawables.EnableStrokeAntialias();

            foreach (var cell in _scheme.Cells)
            {
                drawables.DrawEmbroideredCell(type, cellSize, cell);
            }

            // Отрисовываем клетки в виде вышивки на пустом изображении
            magickImage.Draw(drawables);

            return new Image(magickImage.ToByteArray(), width, height);
        }

        #endregion
    }

    /// <summary>
    /// Класс расширений для <see cref="MagickImage"/>
    /// </summary>
    public static class MagickImageExtensions
    {
        /// <summary>
        /// Отрисовка сетки с помощью наращивания
        /// К схеме добавляются линии сетки размером <paramref name="gridSize"/> и цветом <paramref name="gridColor"/>
        /// </summary>
        /// <param name="image">объект <see cref="MagickImage"/> для расширения</param>
        /// <param name="width">ширина изображения</param>
        /// <param name="height">высота изображения</param>
        /// <param name="cellSize">размер клетки в пикселях</param>
        /// <param name="gridSize">размер сетки в пикселях</param>
        /// <param name="gridColor">цвет сетки</param>
        /// <exception cref="ArgumentOutOfRangeException">Возникает, когда <paramref name="cellSize"/> или <paramref name="gridSize"/> имеют некорректные значения</exception>
        /// <exception cref="ArgumentException">Возникает, когда размеры изображения: <paramref name="width"/> и <paramref name="height"/> не кратны размеру сетки <paramref name="cellSize"/></exception>
        public static void DrawGrid(this MagickImage image, int width, int height, int cellSize, int gridSize, MagickColor gridColor)
        {
            if (cellSize <= 0)
                throw new ArgumentOutOfRangeException(nameof(cellSize), "Размер клетки не может быть <= 0");
            if (gridSize <= 0)
                throw new ArgumentOutOfRangeException(nameof(gridSize), "Размер сетки не может быть <= 0");
            if (width % cellSize != 0 || height % cellSize != 0)
                throw new ArgumentException("Указан некорректный размер клетки. Изображение должно быть кратно размеру клетки");

            image.BackgroundColor = gridColor;

            // Вызываем цикличную отрисовку пересечений с правого нижнего угла
            DrawCross(image, xoffset: width, yoffset: height, cellSize, gridSize);

            // Очистка лишнего пространства, полученного в результате наращивания
            image.Trim();
            // Добавление граничной рамки
            image.BorderColor = gridColor;
            image.Border(gridSize);
        }

        #region private helper methods

        /// <summary>
        /// Отрисовка пересечений от отступа xoffset,yoffset до 0,0
        /// </summary>
        /// <param name="image">Изображение для отрисовки</param>
        /// <param name="xoffset">Отступ по оси X</param>
        /// <param name="yoffset">Отступ по оси Y</param>
        /// <param name="cellSize">Размер клетки</param>
        /// <param name="gridSize">Размер секти</param>
        private static void DrawCross(in MagickImage image, int xoffset, int yoffset, int cellSize, int gridSize)
        {
            // Если отступы отрицательные, завершаем работу
            while (xoffset >= 0 && yoffset >= 0)
            {
                image.Splice(new MagickGeometry($"{gridSize}x{gridSize}+{xoffset}+{yoffset}"));

                // Высчитываем отступы для следующих отрисовок.
                // Если отступы равны, то идем по диагонали
                if (xoffset == yoffset)
                    xoffset = yoffset -= cellSize;
                // Если отступ по оси Х больше, то идем влево
                else if (xoffset > yoffset)
                    xoffset -= cellSize;
                // Иначе вверх
                else
                    yoffset -= cellSize;
            }
        }

        #endregion
    }

    /// <summary>
    /// Класс расширений для элементов отрисовки <see cref="IDrawable{byte}"/>
    /// </summary>
    public static class IDrawablesExtensions
    {
        #region Extensions

        /// <summary>
        /// Отрисовка закрашенной точки
        /// </summary>
        /// <param name="drawables">объект <see cref="IDrawable{byte}"/> для расширения</param>
        /// <param name="x">X координата</param>
        /// <param name="y">Y координата</param>
        /// <param name="color">Цвет заливки</param>
        /// <returns>объект <see cref="IDrawable{byte}"/> для расширения</returns>
        public static IDrawables<byte> DrawableFilledPoint(this IDrawables<byte> drawables, int x, int y, MagickColor color)
        {
            drawables.FillColor(color)
                     .Point(x, y);

            return drawables;
        }

        /// <summary>
        /// Отрисовка линии по точкам <see cref="PointPosition"/>
        /// </summary>
        /// <param name="drawables">объект <see cref="IDrawable{byte}"/> для расширения</param>
        /// <param name="startPoint">Точка начала отрисовки</param>
        /// <param name="endPoint">Точка окончания отрисовки</param>
        /// <returns>объект <see cref="IDrawable{byte}"/> для расширения</returns>
        public static IDrawables<byte> LineByPoints(this IDrawables<byte> drawables, PointPosition startPoint, PointPosition endPoint)
        {
            var startX = startPoint.X;
            var startY = startPoint.Y;
            var endX = endPoint.X;
            var endY = endPoint.Y;
            return drawables.Line(startX, startY, endX, endY);
        }

        /// <summary>
        /// Отрисовка вышитой клетки
        /// </summary>
        /// <param name="drawables">объект <see cref="IDrawable{byte}"/> для расширения</param>
        /// <param name="type">Вид вышивки</param>
        /// <param name="cellSize">Размер клетки</param>
        /// <param name="cell">Данные клетки</param>
        /// <returns>объект <see cref="IDrawable{byte}"/> для расширения</returns>
        public static IDrawables<byte> DrawEmbroideredCell(this IDrawables<byte> drawables, ЕmbroideryType type, int cellSize, Cell cell)
        {
            drawables.StrokeColor(new MagickColor(cell.Color.HexValue));
            var cellCoordinate = new PointPosition(cell.Position.X * cellSize, cell.Position.Y * cellSize);
            var cellCoordinates = new CellCoordinates(cellCoordinate, cellSize);

            // Отрисовка клетки в зависимости от вида вышивки
            switch (type)
            {
                case ЕmbroideryType.Cross:
                    drawables.LineByPoints(cellCoordinates.BottomLeftPosition, cellCoordinates.TopRightPosition);
                    drawables.LineByPoints(cellCoordinates.TopLeftPosition, cellCoordinates.BottomRightPosition);
                    break;
                case ЕmbroideryType.SmoothHorizontal:
                    drawables.LineByPoints(cellCoordinates.CenterLeftPosition, cellCoordinates.CenterRightPosition);
                    break;
                case ЕmbroideryType.SmoothVertical:
                    drawables.LineByPoints(cellCoordinates.BottomCenterPosition, cellCoordinates.TopCenterPosition);
                    break;
                case ЕmbroideryType.SmoothDiagonal:
                    drawables.LineByPoints(cellCoordinates.BottomLeftPosition, cellCoordinates.TopRightPosition);
                    break;
            }

            return drawables;
        }

        #endregion
    }

    /// <summary>
    /// Методы вышивки
    /// </summary>
    public enum EmbroideryMethod
    {
        /// <summary>
        /// Вышивка в 1 нить
        /// </summary>
        In1Thread = 1,

        /// <summary>
        /// Вышивка в 2 нити
        /// </summary>
        In2Thread = 2
    }

    /// <summary>
    /// Данные по координатам клетки <see cref="Cell"/>
    /// </summary>
    public struct CellCoordinates
    {
        private readonly int _cellLength;

        /// <summary>
        /// Верхняя левая точка клетки
        /// </summary>
        public PointPosition TopLeftPosition { get; }

        /// <summary>
        /// Верхняя правая точка клетки
        /// </summary>
        public PointPosition TopRightPosition => new(TopLeftPosition.X + _cellLength, TopLeftPosition.Y);

        /// <summary>
        /// Нижняя левая точка клетки
        /// </summary>
        public PointPosition BottomLeftPosition => new(TopLeftPosition.X, TopLeftPosition.Y + _cellLength);

        /// <summary>
        /// Нижняя правая точка клетки
        /// </summary>
        public PointPosition BottomRightPosition => new(TopLeftPosition.X + _cellLength, TopLeftPosition.Y + _cellLength);

        /// <summary>
        /// Центральная точка клетки
        /// </summary>
        public PointPosition CenterPosition => new(TopLeftPosition.X + _cellLength / 2, TopLeftPosition.Y + _cellLength / 2);

        /// <summary>
        /// Центральная левая точка клетки
        /// </summary>
        public PointPosition CenterLeftPosition => new(TopLeftPosition.X, TopLeftPosition.Y + _cellLength / 2);

        /// <summary>
        /// Центральная правая точка клетки
        /// </summary>
        public PointPosition CenterRightPosition => new(TopLeftPosition.X + _cellLength, TopLeftPosition.Y + _cellLength / 2);

        /// <summary>
        /// Верхняя центральная точка клетки
        /// </summary>
        public PointPosition TopCenterPosition => new(TopLeftPosition.X + _cellLength / 2, TopLeftPosition.Y);

        /// <summary>
        /// Нижняя центральная точка клетки
        /// </summary>
        public PointPosition BottomCenterPosition => new(TopLeftPosition.X + _cellLength / 2, TopLeftPosition.Y + _cellLength);

        /// <summary>
        /// Инициализация координат клетки по верхней левой координате и размеру клетки
        /// </summary>
        /// <param name="topLeftPoint">Верхняя левая координата</param>
        /// <param name="cellSize">Размер клетки</param>
        public CellCoordinates(PointPosition topLeftPoint, int cellSize)
        {
            if (cellSize <= 1) throw new ArgumentOutOfRangeException(nameof(cellSize), "Размер клетки не может быть <= 1");
            _cellLength = cellSize - 1;
            TopLeftPosition = topLeftPoint;
        }
    }

    /// <summary>
    /// Виды вышивки
    /// </summary>
    public enum ЕmbroideryType
    {
        /// <summary>
        /// Крестиком
        /// </summary>
        Cross,

        /// <summary>
        /// Гладью вбок
        /// </summary>
        SmoothHorizontal,

        /// <summary>
        /// Гладью вперед
        /// </summary>
        SmoothVertical,

        /// <summary>
        /// Гладью по диагонали
        /// </summary>
        SmoothDiagonal
    }

    /// <summary>
    /// Класс для загрузки и сохранения <see cref="Scheme"/>
    /// </summary>
    public class SchemeKeeper
    {
        /// <summary>
        /// Загрузка схемы из json файла
        /// </summary>
        /// <param name="path">Абсолютный путь к файлу сохранения в json формате</param>
        /// <returns><see cref="Scheme"/> - загруженные данные схемы</returns>
        /// <exception cref="FileNotFoundException">Возникает, если файл со схемой по указанному пути отсутствует</exception>
        /// <exception cref="ArgumentException">Возникает, если указанный файл не имеет расширение "json"</exception>
        /// <exception cref="SerializationException">Возникает, если произошла ошибка при десериализации схемы из файла</exception>
        public async Task<Scheme> LoadSchemeFromJsonAsync(string path)
        {
            var fileInfo = new FileInfo(path);
            if (!fileInfo.Exists) throw new FileNotFoundException($"Схема по пути {path} не найдена");
            if (fileInfo.Extension != ".json") throw new ArgumentException($"Указан файл с неподходящим расширением - {fileInfo.Extension}");

            var data = await File.ReadAllTextAsync(path);
            var scheme = JsonConvert.DeserializeObject<Scheme>(data);

            return scheme ?? throw new SerializationException($"Ошибка десериализации схемы по пути {path}");
        }

        /// 
        /// <summary>
        /// Сохранения схемы в файл
        /// </summary>
        /// <param name="path">Абсолютный путь сохранения схемы</param>
        /// <param name="scheme">Данные схемы для сохранения</param>
        /// <param name="writeIndented">Запись данных с отступами и переносами строк. По умолчанию = false/></param>
        /// <returns></returns>
        public async Task SaveSchemeAsync(string path, Scheme scheme, bool writeIndented = false)
        {
            var formating = writeIndented ? Formatting.Indented : Formatting.None;
            var json = JsonConvert.SerializeObject(scheme, formating);
            await File.WriteAllTextAsync(path, json);
        }
    }

    /// <summary>
    /// Класс преобразователь схем
    /// </summary>
    public class SchemeConverter
    {
        #region Public methods

        /// <summary>
        /// Метод создания схемы по данным изображения <see cref="Image"/>
        /// </summary>
        /// <param name="image">Данные изображения</param>
        /// <returns>Схема по изображению</returns>
        public Scheme CreateScheme(Image image)
        {
            var scheme = new Scheme(image.Width, image.Height);
            using var magickImage = new MagickImage(image.Data);
            using var pixels = magickImage.GetPixels();
            foreach (var pixel in pixels)
            {
                var color = pixel.ToColor();
                // Если цвет распознать не удалось, то устанавливаем цвет по умолчанию
                var colorHex = color?.ToHexString() ?? Color.DefaultColor;
                scheme.SetCell(new Color(colorHex), new PointPosition(pixel.X, pixel.Y));
            }

            return scheme;
        }

        #endregion
    }

    /// <summary>
    /// Класс расширений для массивов <see cref="Array"/>
    /// </summary>
    public static class ArrayExtensions
    {
        /// <summary>
        /// Метод преобразования многомерного массива в <see cref="IEnumerable{T}"/> 
        /// </summary>
        /// <typeparam name="T">Тип элементов многомерного массива</typeparam>
        /// <param name="target">Многомерный массив</param>
        /// <returns>Последовательность элементов многомерного массива</returns>
        public static IEnumerable<T> ToEnumerable<T>(this T[,] target)
        {
            foreach (var item in target)
                yield return item;
        }
    }

    /// <summary>
    /// Класс схемы вышивки
    /// Реализует <see cref="ISerializable"/> для сериализации объекта схемы
    /// </summary>
    [Serializable]
    public class Scheme : ISerializable
    {
        #region Private fields

        /// <summary>
        /// Ширина схемы в клетках
        /// </summary>
        public int _width;

        /// <summary>
        /// Высота схемы в клетках
        /// </summary>
        public int _height;

        /// <summary>
        /// Клетки схемы. Многомерный массив размером <see cref="Width"/> на <see cref="Height"/>
        /// </summary>
        private Cell[,] _cells;

        #endregion

        #region Public properties 

        /// <summary>
        /// Ширина схемы в клетках
        /// </summary>
        public int Width
        {
            get => _width;
            set
            {
                if (value <= 0) throw new ArgumentOutOfRangeException();
                _width = value;
            }
        }

        /// <summary>
        /// Высота схемы в клетках
        /// </summary>
        public int Height
        {
            get => _height;
            set
            {
                if (value <= 0) throw new ArgumentOutOfRangeException();
                _height = value;
            }
        }

        /// <summary>
        /// Сетка для схемы. 
        /// Если значение не установлено, то сетка не используется
        /// </summary>
        public Grid? Grid { get; set; }

        /// <summary>
        /// Последовательность всех клеток схемы
        /// </summary>
        public IEnumerable<Cell> Cells => _cells.ToEnumerable();

        #endregion

        #region Constructors

        /// <summary>
        /// Инициализация объекта схемы
        /// </summary>
        /// <param name="width">Ширина схемы в клетках</param>
        /// <param name="height">Высота схемы в клетках</param>
        public Scheme(int width, int height)
        {
            Width = width;
            Height = height;
            _cells = new Cell[width, height];
        }

        /// <summary>
        /// Приватный конструктор для десериализации объекта
        /// </summary>
        /// <param name="info">Данные сериализованного объекта</param>
        /// <param name="context">Источник потока сериализованного объекта</param>
        /// <exception cref="SerializationException">Возникает, если нет возможности десериализовать массив клеток</exception>
        private Scheme(SerializationInfo info, StreamingContext context)
        {
            Width = info.GetInt32(nameof(Width));
            Height = info.GetInt32(nameof(Height));
            var cellsObject = info.GetValue(nameof(_cells), typeof(Cell[,]));
            if (cellsObject is not Cell[,] cells)
                throw new SerializationException("Ошибка сериализации клеток схемы");
            _cells = cells;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Установка новой клетки по позиции
        /// </summary>
        /// <param name="color">Цвет клетки</param>
        /// <param name="position">Позиция клетки в схеме</param>
        /// <exception cref="ArgumentOutOfRangeException">Возникает, если указанная позиция выходит за границы схемы</exception>
        public void SetCell(Color color, PointPosition position)
        {
            if (position.X >= Width || position.Y >= Height)
                throw new ArgumentOutOfRangeException(nameof(position));
            _cells[position.X, position.Y] = new Cell(color, position);
        }

        /// <summary>
        /// Последовательность всех использованных цветов в схеме
        /// </summary>
        public IEnumerable<string> GetColors() => Cells.Select(cell => cell.Color.HexValue).Distinct();

        /// <summary>
        /// Получение данных объекта для сериализации. Реализация <see cref="ISerializable"/>
        /// </summary>
        /// <param name="info">Данные сериализованного объекта</param>
        /// <param name="context">Источник потока сериализованного объекта</param>
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue(nameof(Width), Width);
            info.AddValue(nameof(Height), Height);
            info.AddValue(nameof(_cells), _cells);
        }

        #endregion
    }

    /// <summary>
    /// Класс сетки схемы
    /// </summary>
    public class Grid
    {
        #region Private fields

        /// <summary>
        /// Размер сетки в px
        /// </summary>
        private int _size;

        #endregion

        #region Constructors

        public Grid(int size, Color? color = null)
        {
            Size = size;
            Color = color ?? new Color();
        }

        #endregion

        #region Public properties

        /// <summary>
        /// Цвет сетки
        /// </summary>
        public Color Color { get; set; }

        /// <summary>
        /// Размер сетки в px
        /// </summary>
        public int Size
        {
            get => _size;
            set
            {
                if (value < 0) throw new ArgumentOutOfRangeException(nameof(Size), "Размер сетки не может быть < 0");
                _size = value;
            }
        }

        #endregion
    }

    /// <summary>
    /// Класс клетки схемы
    /// </summary>
    public class Cell : IComparable<Cell>
    {
        #region Public properties 

        /// <summary>
        /// Цвет клетки
        /// </summary>
        public Color Color { get; set; }

        /// <summary>
        /// Позиция клетки
        /// </summary>
        public PointPosition Position { get; }

        #endregion

        #region Constructors

        public Cell(Color color, PointPosition position)
        {
            Color = color;
            Position = position;
        }

        #endregion

        #region Public methods

        /// <summary>
        /// Сравнение клеток по позициям. Реализация интерфейса <see cref="IComparable{Cell}"/>\
        /// Первичная сортировка по Y координатам
        /// Вторичная по X координатам
        /// </summary>
        /// <param name="other">Данные другой клетки</param>
        /// <returns>Результат сравнения позиций</returns>
        /// <exception cref="ArgumentNullException">Возникает, если в качестве объекта клетки получен null</exception>
        public int CompareTo(Cell? other)
        {
            if (other is null)
                throw new ArgumentNullException(nameof(other), "Значение клетки не может быть null");
            var position = other.Position;
            if (Position.Y != position.Y)
                return Position.Y - position.Y;
            else
                return Position.X - position.X;
        }

        #endregion
    }

    /// <summary>
    /// Класс цвета клетки
    /// </summary>
    public class Color
    {
        #region Constants

        /// <summary>
        /// Паттерн кода цвета
        /// </summary>
        private static readonly string ColorPattern = @"^#[0-9A-F]{6}$";

        /// <summary>
        /// Базовый цвет в 16 с.с
        /// </summary>
        public static readonly string DefaultColor = "#FFFFFF";

        #endregion

        #region Private fields

        /// <summary>
        /// 16-ое представление цвета RGB
        /// </summary>
        private string _colorHex = DefaultColor;

        #endregion

        #region Public properties

        /// <summary>
        /// Цвет в 16 с.с представлении
        /// </summary>
        public string HexValue
        {
            get => _colorHex;
            set => _colorHex = Regex.IsMatch(value, ColorPattern) ? value : DefaultColor;
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Дефолтный конструктор
        /// </summary>
        public Color() { }

        /// <summary>
        /// Инициализация объекта цвета
        /// </summary>
        /// <param name="colorHex">цвет в 16 с.с</param>
        public Color(string colorHex)
        {
            HexValue = colorHex;
        }

        #endregion
    }

    /// <summary>
    /// Структура с координатами точки
    /// </summary>
    public struct PointPosition
    {
        /// <summary>
        /// Х координата
        /// </summary>
        public int X { get; }
        /// <summary>
        /// Y координата
        /// </summary>
        public int Y { get; }

        /// <summary>
        /// Инициализация координат
        /// </summary>
        /// <param name="x">x координата</param>
        /// <param name="y">y координата</param>
        public PointPosition(int x, int y)
        {
            X = x;
            Y = y;
        }
    }

    /// <summary>
    /// Класс для загрузки и сохранения <see cref="Image"/>
    /// </summary>
    public class ImageKeeper
    {
        /// <summary>
        /// Загрузка изображений из файла
        /// </summary>
        /// <param name="path">Абсолютный путь к изображению</param>
        /// <returns><see cref="Image"/> - загруженные данные изображения</returns>
        /// <exception cref="FileNotFoundException">Возникает, если файл с изображением по указанному пути отсутствует</exception>
        public async Task<Image> LoadImageAsync(string path)
        {
            if (!File.Exists(path)) throw new FileNotFoundException("Изображение по указаному пути не найдено");
            var data = await File.ReadAllBytesAsync(path);
            using var image = new MagickImage(data);
            return new Image(data, image.Width, image.Height);
        }

        /// <summary>
        /// Сохранение изображения в файл
        /// </summary>
        /// <param name="path">Абсолютный путь сохранения изображения</param>
        /// <param name="image">Данные изображения для сохранения</param>
        public async Task SaveImageAsync(string path, Image image)
        {
            using var magickImage = new MagickImage(image.Data);
            await magickImage.WriteAsync(path);
        }
    }

    /// <summary>
    /// Класс обработчик изображений
    /// </summary>
    public class ImageProccesor
    {
        #region Public properties

        public Image Image { get; private set; }

        #endregion

        #region Constructors

        public ImageProccesor(Image image)
        {
            Image = image;
        }

        #endregion

        #region Public methods

        public void Trim()
        {
            using var imageMagick = new MagickImage(Image.Data);
            imageMagick.Trim();
            Image = new Image(data: imageMagick.ToByteArray(), imageMagick.Width, imageMagick.Height);
        }

        #endregion
    }

    /// <summary>
    /// Класс с данными изображения
    /// </summary>
    public class Image
    {
        #region Private fields

        /// <summary>
        /// Бинарные данные изображения  
        /// </summary>
        private byte[] _data;

        /// <summary>
        /// Ширина изображения
        /// </summary>
        private int _width;

        /// <summary>
        /// Высота изображения
        /// </summary>
        private int _height;

        #endregion

        #region Public properties

        /// <summary>
        /// Копия бинарных данных изображения
        /// </summary>
        public byte[] Data
        {
            get => (byte[])_data.Clone();
            private set
            {
                if (value.Length == 0) throw new ImageException("Бинарные данные изображения не могут быть пустыми");
                _data = value;
            }
        }

        /// <summary>
        /// Ширина изображения
        /// </summary>
        public int Width
        {
            get => _width;
            private set
            {
                if (value < 0) throw new ImageException("Значение ширины изображения не может быть <= 0");
                _width = value;
            }
        }

        /// <summary>
        /// Высота изображения
        /// </summary>
        public int Height
        {
            get => _height;
            private set
            {
                if (value < 0) throw new ImageException("Значение высоты изображения не может быть <= 0");
                _height = value;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Инициализация объекта изображения
        /// </summary>
        /// <param name="data">Бинарные данные изображения</param>
        /// <param name="width">Ширина изображения в пикселях</param>
        /// <param name="height">Высота изображения в пикселях</param>
        /// <exception cref="ImageException">Возникает при некорреткном указании данных изображения</exception>
        public Image(byte[] data, int width, int height)
        {
            _data = data;
            Width = width;
            Height = height;
        }

        #endregion
    }

    /// <summary>
    /// Класс исключений при взаимодействии с <see cref="Image"/>
    /// </summary>
    public class ImageException : Exception
    {
        #region Constructors

        public ImageException(string message) : base(message) { }

        #endregion
    }
}
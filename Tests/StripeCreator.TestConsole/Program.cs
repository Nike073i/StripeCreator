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
    /// Класс исключений при взаимодействии с <see cref="Image"/>
    /// </summary>
    public class ImageException : Exception
    {
        #region Constructors

        public ImageException(string message) : base(message) { }

        #endregion
    }
}
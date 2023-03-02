using ImageMagick;
using StripeCreator.Core.Models;
using StripeCreator.Stripe.Extensions;
using StripeCreator.Stripe.Models;

namespace StripeCreator.Stripe.Services
{
    /// <summary>
    /// Создатель схематичного изображения
    /// </summary>
    public class SchemeVisualizer
    {
        #region Constants

        /// <summary>
        /// Размер клетки для отображения на странице
        /// </summary>
        public static readonly int SchemeCellSizeForPage = 2;

        /// <summary>
        /// Размер клетки для отображения <see cref="EmbroideryMethod.In2Thread"/>
        /// </summary>
        public static readonly int SchemeCellSizeFor2Thread = 5;

        /// <summary>
        /// Размер клетки для отображения <see cref="EmbroideryMethod.In1Thread"/>
        /// </summary>
        public static readonly int SchemeCellSizeFor1Thread = 3;

        /// <summary>
        /// Размер клетки для отображения схемы в виде клеток
        /// </summary>
        public static readonly int SchemeCellSizePixel = 5;

        /// <summary>
        /// Размер сетки
        /// </summary>
        public static readonly int GridSize = 1;

        #endregion

        #region Public methods

        /// <summary>
        /// Создание пиксельного изображения по схеме
        /// </summary>
        /// <param name="scheme">Схема вышивки</param>
        /// <returns>Данные по изображению</returns>
        public Image CreateCellScheme(Scheme scheme, bool isPageScheme = false)
        {
            var cellSize = isPageScheme ? SchemeCellSizeForPage : SchemeCellSizePixel;
            return CreateScheme(scheme, cellSize, () => DrawFilledCells(cellSize, scheme.SchemeTemplate));
        }

        /// <summary>
        /// Создание прототипного изображения по схеме с размерами клетки <paramref name="cellSize"/> 
        /// </summary>
        /// <param name="scheme">Схема вышивки</param>
        /// <param name="type">Вид вышивки</param>
        /// <param name="method">Способ вышивки</param>
        /// <param name="backgroundColor">Фоновый цвет</param>
        /// <returns>Данные по изображению</returns>

        public Image CreatePrototypeScheme(Scheme scheme, EmbroideryType type, EmbroideryMethod method, Color? backgroundColor = null)
        {
            var cellSize = GetCellSize(method);
            return CreateScheme(scheme, cellSize, () => DrawEmbroideryView(scheme.SchemeTemplate, type, method, backgroundColor ?? new Color()));
        }
        #endregion

        #region Private helper methods

        /// <summary>
        /// Создание изображения по схеме способом <paramref name="drawContentFunc"/> 
        /// </summary>
        /// <param name="scheme">Схема вышивки</param>
        /// <param name="cellSize">Размер клетки в пикселях</param>
        /// <param name="drawContentFunc">Способ отрисовки схемы</param>
        /// <returns>Данные по изображению</returns>
        /// <exception cref="ArgumentOutOfRangeException">Возникает, если указан некорректный размер клетки для изображения</exception>
        private Image CreateScheme(Scheme scheme, int cellSize, Func<Image> drawContentFunc)
        {
            if (cellSize <= 0)
                throw new ArgumentOutOfRangeException(nameof(cellSize), "Размер клетки не может быть <= 0");

            var image = drawContentFunc();

            var indent = scheme.Indent;
            if (indent != null)
                image = AddIndent(image, cellSize, indent.Size, indent.Color);

            var grid = scheme.Grid;
            if (grid != null)
                image = DrawGrid(image, cellSize, GridSize, grid.Color);

            return image;
        }

        /// <summary>
        /// Отрисовка закрашенных клеток по <paramref name="schemeTemplate"/> размером <paramref name="cellSize"/>
        /// </summary>
        /// <param name="cellSize">Размер клетки в сетке</param>
        /// <param name="schemeTemplate">Заготовка схемы вышивки</param>
        /// <returns>Визуализация схемы</returns>
        private Image DrawFilledCells(int cellSize, Image schemeTemplate)
        {
            using var magickImage = MagickImageExtensions.CreateMagickImage(schemeTemplate);

            // Масштабируем пиксели до нужного размера клетки
            var newWidth = schemeTemplate.Width * cellSize;
            var newHeigth = schemeTemplate.Height * cellSize;
            magickImage.Sample(newWidth, newHeigth);

            return magickImage.CreateImage();
        }

        /// <summary>
        /// Отрисовка сетки на изображении путем наращивания
        /// </summary>
        /// <param name="sourceImage">Исходное изображение для нанесения сетки</param>
        /// <param name="cellSize">Размер необходимой клетки в пикселях</param>
        /// <param name="gridSize">Размер сетки в пикселях</param>
        /// <param name="gridColor">Цвет сетки</param>
        /// <returns>Данные изображения</returns>
        private Image DrawGrid(Image sourceImage, int cellSize, int gridSize, Color gridColor)
        {
            using var magickImage = MagickImageExtensions.CreateMagickImage(sourceImage);

            var parts = magickImage.SplitImage();
            parts.AsParallel().ForAll(part => part.DrawGrid(cellSize, gridSize, MagickImageExtensions.CreateColor(gridColor)));
            using var collection = new MagickImageCollection(parts);
            var image = collection.Montage(new MontageSettings()
            {
                TileGeometry = new MagickGeometry("2x2"),
                Geometry = new MagickGeometry("+1+1")
            });

            return image.CreateImage();
        }

        /// <summary>
        /// Отрисовка отступа для схемы
        /// </summary>
        /// <param name="sourceImage">Исходное изображение</param>
        /// <param name="cellSize">Размер клетки в пикселях</param>
        /// <param name="count">Количество клеток</param>
        /// <param name="color">Цвет фона отступа</param>
        /// <returns>Данные изображения</returns>
        private Image AddIndent(Image sourceImage, int cellSize, int count, Color color)
        {
            if (count <= 0 || count > 10) return sourceImage;
            using var magickImage = MagickImageExtensions.CreateMagickImage(sourceImage);
            magickImage.BorderColor = MagickImageExtensions.CreateColor(color);
            magickImage.Border(cellSize * count);
            return magickImage.CreateImage();
        }

        /// <summary>
        /// Отрисовка клеток в виде вышивки
        /// </summary>
        /// <param name="type">Вид вышивки</param>
        /// <param name="method">Способ вышивки</param>
        /// <param name="schemeTemplate">Заготовка схемы вышивки</param>
        /// <param name="backgroundColor">Фоновый цвет</param>
        /// <returns>Визуализация схемы</returns>
        private Image DrawEmbroideryView(Image schemeTemplate, EmbroideryType type, EmbroideryMethod method, Color backgroundColor)
        {
            var cellSize = GetCellSize(method);
            var imageCells = DrawFilledCells(cellSize, schemeTemplate);
            using var image = MagickImageExtensions.CreateMagickImage(imageCells);
            using var tile = GetEmbroideryMethodPatternImage(type, method);
            using var mask = MagickImageExtensions.CreateMagickImage(new Size(imageCells.Width, imageCells.Height));
            mask.Texture(tile);
            image.Composite(mask, CompositeOperator.CopyAlpha);
            image.BackgroundColor = MagickImageExtensions.CreateColor(backgroundColor);
            image.Alpha(AlphaOption.Remove);
            return image.CreateImage();
        }

        /// <summary>
        /// Получить паттерн вышивки в виде тайла
        /// </summary>
        /// <param name="type"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        private static MagickImage GetEmbroideryMethodPatternImage(EmbroideryType type, EmbroideryMethod method)
        {
            var cellSize = GetCellSize(method);
            var patternImage = MagickImageExtensions.CreateMagickImage(new Size(cellSize, cellSize));
            var drawables = method == EmbroideryMethod.In1Thread ? Get1ThreadPattern(type) : Get2ThreadPattern(type);
            patternImage.Draw(drawables);
            return patternImage;
        }

        /// <summary>
        /// Получить паттерн для вышивки способом <see cref="EmbroideryMethod.In1Thread"/>
        /// </summary>
        /// <param name="type">Тип вышивки</param>
        /// <returns>Паттерн</returns>
        private static Drawables Get1ThreadPattern(EmbroideryType type)
        {
            var drawables = new Drawables();
            drawables.DisableStrokeAntialias();
            switch (type)
            {
                case EmbroideryType.Cross:
                    drawables.Line(0, 0, 2, 2);
                    drawables.Line(0, 2, 2, 0);
                    break;
                case EmbroideryType.SmoothHorizontal:
                    drawables.Line(0, 1, 2, 1);
                    break;
                case EmbroideryType.SmoothVertical:
                    drawables.Line(1, 0, 1, 2);
                    break;
                case EmbroideryType.SmoothDiagonal:
                    drawables.Line(0, 2, 2, 0);
                    break;
            }
            return drawables;
        }

        /// <summary>
        /// Получить паттерн для вышивки способом <see cref="EmbroideryMethod.In2Thread"/>
        /// </summary>
        /// <param name="type">Тип вышивки</param>
        /// <returns>Паттерн</returns>
        private static Drawables Get2ThreadPattern(EmbroideryType type)
        {
            var drawables = new Drawables();
            drawables.DisableStrokeAntialias();
            switch (type)
            {
                case EmbroideryType.Cross:
                    // Диагональ левый верх - правый низ
                    drawables.Line(1, 0, 4, 3);
                    drawables.Line(0, 0, 4, 4);
                    drawables.Line(0, 1, 3, 4);
                    // Диагональ левый низ - правый верх
                    drawables.Line(0, 3, 3, 0);
                    drawables.Line(0, 4, 4, 0);
                    drawables.Line(1, 4, 4, 1);
                    break;
                case EmbroideryType.SmoothHorizontal:
                    drawables.Line(0, 1, 4, 1);
                    drawables.Line(0, 2, 4, 2);
                    drawables.Line(0, 3, 4, 3);
                    break;
                case EmbroideryType.SmoothVertical:
                    drawables.Line(1, 0, 1, 4);
                    drawables.Line(2, 0, 2, 4);
                    drawables.Line(3, 0, 3, 4);
                    break;
                case EmbroideryType.SmoothDiagonal:
                    drawables.Line(0, 3, 3, 0);
                    drawables.Line(0, 4, 4, 0);
                    drawables.Line(1, 4, 4, 1);
                    break;
            }
            return drawables;
        }

        /// <summary>
        /// Получить размер клетки по методу вышивки <see cref="EmbroideryMethod"/>
        /// </summary>
        /// <param name="method">Метод вышивки</param>
        /// <returns></returns>
        private static int GetCellSize(EmbroideryMethod method) =>
            method == EmbroideryMethod.In2Thread ? SchemeCellSizeFor2Thread : SchemeCellSizeFor1Thread;

        #endregion
    }
}

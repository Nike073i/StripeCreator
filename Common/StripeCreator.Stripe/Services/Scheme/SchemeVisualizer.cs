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
        #region Public methods

        /// <summary>
        /// Создание пиксельного изображения по схеме с размерами клетки <paramref name="cellSize"/>
        /// </summary>
        /// <param name="scheme">Схема вышивки</param>
        /// <param name="cellSize">Размер клетки в пикселях</param>
        /// <returns>Данные по изображению</returns>
        public Image CreateCellScheme(Scheme scheme, int cellSize) =>
            CreateScheme(scheme, cellSize, () => DrawFilledCells(cellSize, scheme));

        /// <summary>
        /// Создание прототипного изображения по схеме с размерами клетки <paramref name="cellSize"/> 
        /// </summary>
        /// <param name="scheme">Схема вышивки</param>
        /// <param name="type">Вид вышивки</param>
        /// <param name="method">Способ вышивки</param>
        /// <param name="cellSize">Размер клетки в пикселях</param>
        /// <param name="backgroundColor">Фоновый цвет</param>
        /// <returns>Данные по изображению</returns>

        public Image CreatePrototypeScheme(Scheme scheme, int cellSize, ЕmbroideryType type, EmbroideryMethod method, Color? backgroundColor = null) =>
            CreateScheme(scheme, cellSize, () => DrawEmbroideryView(scheme, cellSize, type, method, backgroundColor ?? new Color()));

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

            var grid = scheme.Grid;
            if (grid != null)
                image = DrawGrid(image, cellSize, grid.Size, grid.Color);

            return image;
        }

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
                drawables.FilledPoint(cellPosition.X, cellPosition.Y, new MagickColor(cell.Color.HexValue));
            }
            var width = scheme.Width;
            var height = scheme.Height;
            using var magickImage = new MagickImage(MagickColors.Transparent, width, height)
            {
                Format = MagickFormat.Png
            };

            magickImage.Draw(drawables);

            // Масштабируем пиксели до нужного размера клетки
            var newWidth = width * cellSize;
            var newHeigth = height * cellSize;
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
        /// <returns></returns>
        private Image DrawGrid(Image sourceImage, int cellSize, int gridSize, Color gridColor)
        {
            using var magickImage = new MagickImage(sourceImage.Data);
            magickImage.DrawGrid(cellSize, gridSize, new MagickColor(gridColor.HexValue));
            return magickImage.CreateImage();
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
        private Image DrawEmbroideryView(Scheme scheme, int cellSize, ЕmbroideryType type, EmbroideryMethod method, Color backgroundColor)
        {
            var width = scheme.Width * cellSize;
            var height = scheme.Height * cellSize;

            // Создаем пустое изображение размером width x height с фоновым цветом
            using var magickImage = new MagickImage(new MagickColor(backgroundColor.HexValue), width, height)
            {
                Format = MagickFormat.Png
            };

            var drawables = new Drawables();
            // Устанавливаем свойства для соответствия способу вышивки
            if (method == EmbroideryMethod.In1Thread)
                drawables.DisableStrokeAntialias();
            else
                drawables.EnableStrokeAntialias();

            foreach (var cell in scheme.Cells)
            {
                drawables.StrokeColor(new MagickColor(cell.Color.HexValue));
                var topLeftPosition = new PointPosition(cell.Position.X * cellSize, cell.Position.Y * cellSize);
                var cellCoordinates = new CellCoordinatesHelper(topLeftPosition, cellSize);
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
            }

            // Отрисовываем клетки в виде вышивки на пустом изображении
            magickImage.Draw(drawables);
            return magickImage.CreateImage();
        }

        #endregion
    }
}

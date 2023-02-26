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
            CreateScheme(scheme, cellSize, () => DrawFilledCells(cellSize, scheme.SchemeTemplate));

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
            CreateScheme(scheme, cellSize, () => DrawEmbroideryView(scheme.SchemeTemplate, cellSize, type, method, backgroundColor ?? new Color()));

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
        /// <returns>Визуализация схемы</returns>
        private Image DrawGrid(Image sourceImage, int cellSize, int gridSize, Color gridColor)
        {
            using var magickImage = MagickImageExtensions.CreateMagickImage(sourceImage);
            magickImage.DrawGrid(cellSize, gridSize, new MagickColor(gridColor.HexValue));
            return magickImage.CreateImage();
        }

        /// <summary>
        /// Отрисовка клеток в виде вышивки
        /// </summary>
        /// <param name="type">Вид вышивки</param>
        /// <param name="method">Способ вышивки</param>
        /// <param name="cellSize">Размер клетки</param>
        /// <param name="schemeTemplate">Заготовка схемы вышивки</param>
        /// <param name="backgroundColor">Фоновый цвет</param>
        /// <returns></returns>
        private Image DrawEmbroideryView(Image schemeTemplate, int cellSize, ЕmbroideryType type, EmbroideryMethod method, Color backgroundColor)
        {
            var imageCells = DrawFilledCells(cellSize, schemeTemplate);
            using var image = MagickImageExtensions.CreateMagickImage(imageCells);
            using var tile = GetEmbroideryMethodPattern(type, method, cellSize);
            using var mask = MagickImageExtensions.CreateMagickImage(new Size(imageCells.Width, imageCells.Height));
            mask.Tile(tile, CompositeOperator.Copy);
            image.Composite(mask, CompositeOperator.CopyAlpha);
            image.Opaque(MagickColors.Transparent, new MagickColor(backgroundColor.HexValue));

            return image.CreateImage();
        }

        private MagickImage GetEmbroideryMethodPattern(ЕmbroideryType type, EmbroideryMethod method, int cellSize)
        {
            var patternImage = MagickImageExtensions.CreateMagickImage(new Size(cellSize, cellSize));
            var drawables = new Drawables();
            if (method == EmbroideryMethod.In1Thread)
                drawables.DisableStrokeAntialias();
            else
                drawables.EnableStrokeAntialias();
            var center = (cellSize - 1) / 2;
            drawables.StrokeWidth(2);
            switch (type)
            {
                case ЕmbroideryType.Cross:
                    drawables.Line(0, 0, cellSize - 1, cellSize - 1);
                    drawables.Line(0, cellSize - 1, cellSize - 1, 0);
                    break;
                case ЕmbroideryType.SmoothHorizontal:
                    drawables.Line(0, center, cellSize, center);
                    break;
                case ЕmbroideryType.SmoothVertical:
                    drawables.Line(center, 0, center, cellSize);
                    break;
                case ЕmbroideryType.SmoothDiagonal:
                    drawables.Line(0, cellSize - 1, cellSize - 1, 0);
                    break;
            }
            patternImage.Draw(drawables);
            return patternImage;
        }

        #endregion
    }
}

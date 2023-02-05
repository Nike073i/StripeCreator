using ImageMagick;
using StripeCreator.Stripe.Models;

namespace StripeCreator.Stripe.Extensions
{
    /// <summary>
    /// Расширения для изображения <see cref="MagickImage"/>
    /// </summary>
    public static class MagickImageExtensions
    {
        #region Public methods

        /// <summary>
        /// Отрисовка сетки с помощью наращивания
        /// К схеме добавляются линии сетки размером <paramref name="gridSize"/> и цветом <paramref name="gridColor"/>
        /// </summary>
        /// <param name="image">объект <see cref="MagickImage"/> для расширения</param>
        /// <param name="cellSize">размер клетки в пикселях</param>
        /// <param name="gridSize">размер сетки в пикселях</param>
        /// <param name="gridColor">цвет сетки</param>
        /// <exception cref="ArgumentOutOfRangeException">Возникает, когда <paramref name="cellSize"/> или <paramref name="gridSize"/> имеют некорректные значения</exception>
        /// <exception cref="ArgumentException">Возникает, когда размеры изображения не кратны размеру сетки <paramref name="cellSize"/></exception>
        public static void DrawGrid(this MagickImage image, int cellSize, int gridSize, MagickColor gridColor)
        {
            if (cellSize <= 0)
                throw new ArgumentOutOfRangeException(nameof(cellSize), "Размер клетки не может быть <= 0");
            if (gridSize <= 0)
                throw new ArgumentOutOfRangeException(nameof(gridSize), "Размер сетки не может быть <= 0");

            int width = image.Width;
            int height = image.Height;
            if (width % cellSize != 0 || height % cellSize != 0)
                throw new ArgumentException("Указан некорректный размер клетки. Изображение должно быть кратно размеру клетки");

            image.BackgroundColor = gridColor;

            // Вызываем отрисовку пересечений с правого нижнего угла
            DrawCross(image, xoffset: width, yoffset: height, cellSize, gridSize);

            // Очистка лишнего пространства, полученного в результате наращивания
            image.Trim();
            // Добавление граничной рамки
            image.BorderColor = gridColor;
            image.Border(gridSize);
        }

        /// <summary>
        /// Создание объекта данных изображения <see cref="Image"/>
        /// </summary>
        /// <param name="imageMagick">объект <see cref="MagickImage"/> для расширения</param>
        /// <returns>Данные изображения</returns>
        public static Image CreateImage(this MagickImage imageMagick) => new Image(imageMagick.ToByteArray(), imageMagick.Width, imageMagick.Height);

        #endregion

        #region private helper methods

        /// <summary>
        /// Отрисовка пересечений от точки (<paramref name="xoffset"/>,<paramref name="yoffset"/>) до (0,0)
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
}
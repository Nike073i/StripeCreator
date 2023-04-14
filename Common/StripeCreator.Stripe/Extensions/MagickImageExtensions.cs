using ImageMagick;
using StripeCreator.Core.Models;
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
        /// Разбивка изображения на 4 части
        /// </summary>
        /// <param name="sourceImage">объект <see cref="MagickImage"/> для расширения</param>
        /// <returns>Список частей изображения</returns>
        public static IEnumerable<MagickImage> SplitImage(this MagickImage sourceImage, int cellSize)
        {
            var list = new List<MagickImage>();
            int width = sourceImage.Width;
            int height = sourceImage.Height;
            int widthHalf = width / 2;
            int heightHalf = height / 2;
            widthHalf -= widthHalf % cellSize;
            heightHalf -= heightHalf % cellSize;
            list.Add((MagickImage)sourceImage.Clone(0, 0, widthHalf, heightHalf));
            list.Add((MagickImage)sourceImage.Clone(widthHalf, 0, width - widthHalf, heightHalf));
            list.Add((MagickImage)sourceImage.Clone(0, heightHalf, widthHalf, height - heightHalf));
            list.Add((MagickImage)sourceImage.Clone(widthHalf, heightHalf, width - widthHalf, height - heightHalf));
            return list;
        }

        /// <summary>
        /// Создание обработчика изображения по бинарным данным
        /// </summary>
        /// <param name="data">бинарные данные изображения</param>
        /// <returns>Обработчик изображения</returns>
        public static MagickImage CreateMagickImage(byte[] data) => new(data) { Format = MagickFormat.Png };

        /// <summary>
        /// Создание обработчика изображения по данным изображения
        /// </summary>
        /// <param name="image">Данные изображения</param>
        /// <returns>Обработчик изображения</returns>
        public static MagickImage CreateMagickImage(Image image) => new(image.Data) { Format = MagickFormat.Png };

        /// <summary>
        /// Создание обработчика изображения по размерам
        /// </summary>
        /// <param name="size">Размеры изображения</param>
        /// <returns>Обработчик изображения</returns>
        public static MagickImage CreateMagickImage(Size size, Color? color = null)
        {
            var imageBackground = color != null ? CreateColor(color) : MagickColors.Transparent;
            return new(imageBackground, size.Width, size.Height) { Format = MagickFormat.Png };
        }

        /// <summary>
        /// Создание цвета из <see cref="Color"/>
        /// </summary>
        /// <param name="color">Цвет</param>
        /// <returns></returns>
        public static MagickColor CreateColor(Color color) => new(color.Red, color.Green, color.Blue, color.Alpha);

        /// <summary>
        /// Создание объекта данных изображения <see cref="Image"/>
        /// </summary>
        /// <param name="imageMagick">объект <see cref="MagickImage"/> для расширения</param>
        /// <returns>Данные изображения</returns>
        public static Image CreateImage(this IMagickImage imageMagick) => new(imageMagick.ToByteArray(), imageMagick.Width, imageMagick.Height);

        #endregion

        #region Private helper methods

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
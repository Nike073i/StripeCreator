using ImageMagick;
using System.Text.RegularExpressions;

namespace StripeCreator.TestConsole
{
    public class Program
    {
        private static string TestImageInputPath = @"Images/Вольные.bmp";
        private static string TestImageOutputPath = @"Images/OutputImage.png";

        public static void Main(string[] args)
        {
            var imageKeeper = new ImageKeeper();
            var image = Task.Run(() => imageKeeper.LoadImageAsync(TestImageInputPath)).Result;
            var imageProccesor = new ImageProccesor(image);
            imageProccesor.Trim();
            Task.Run(() => imageKeeper.SaveImageAsync(TestImageOutputPath, imageProccesor.Image)).Wait();
        }
    }

    /// <summary>
    /// Класс клетки схемы
    /// </summary>
    public class Cell
    {
        #region Public properties 

        /// <summary>
        /// Цвет клетки
        /// </summary>
        public CellColor Color { get; set; }

        /// <summary>
        /// Позиция клетки
        /// </summary>
        public CellPosition Position { get; }

        #endregion

        #region Constructors
        
        public Cell(CellColor color, CellPosition position)
        {
            Color = color;
            Position = position;
        }

        #endregion
    }

    /// <summary>
    /// Класс цвета клетки
    /// </summary>
    public class CellColor
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
        /// Инициализация объекта цвета клетки
        /// </summary>
        /// <param name="colorHex">цвет в 16 с.с</param>
        public CellColor(string colorHex)
        {
            HexValue = colorHex;
        }

        #endregion
    }

    /// <summary>
    /// Структура с координатами клетки
    /// </summary>
    public struct CellPosition
    {
        /// <summary>
        /// Х координата от левого края
        /// </summary>
        public int X { get; }
        /// <summary>
        /// Y координата от верхнего края
        /// </summary>
        public int Y { get; }

        /// <summary>
        /// Инициализация координат
        /// </summary>
        /// <param name="x">x координата</param>
        /// <param name="y">y координата</param>
        public CellPosition(int x, int y)
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
        /// Метод загрузки изображений
        /// </summary>
        /// <param name="path">Абсолютный путь к изображению</param>
        /// <returns><see cref="Image"/> - загруженные данные изображения</returns>
        /// <exception cref="FileNotFoundException">Ошибка загрузки файла с изображением</exception>
        public async Task<Image> LoadImageAsync(string path)
        {
            if (!File.Exists(path)) throw new FileNotFoundException("Изображение по указаному пути не найдено");
            var data = await File.ReadAllBytesAsync(path);
            using var image = new MagickImage(data);
            return new Image(data, image.Width, image.Height);
        }

        /// <summary>
        /// Метод сохранения изображения
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
                _width = value;
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
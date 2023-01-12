using ImageMagick;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Drawing;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;

namespace StripeCreator.TestConsole
{
    public class Program
    {
        private static string TestImageInputPath = @"Images/test.bmp";
        private static string TestImageOutputPath = @"Images/OutputImage.png";

        public static void Main(string[] args)
        {
            // Тест работы с изображением
            var imageKeeper = new ImageKeeper();
            var image = Task.Run(() => imageKeeper.LoadImageAsync(TestImageInputPath)).Result;
            var converter = new SchemeConverter();
            var scheme = converter.CreateScheme(image);
            var colors = scheme.GetColors();
            var jsonSerializer = new JsonSerializer { Formatting = Formatting.Indented };
            var json = JObject.FromObject(scheme).ToString();
            Task.Run(() => File.WriteAllTextAsync("content.json", json)).Wait();
            var newScheme = JObject.Parse(json).ToObject<Scheme>();
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
                var colorHex = color?.ToHexString() ?? CellColor.DefaultColor;
                scheme.SetCell(new CellColor(colorHex), new CellPosition(pixel.X, pixel.Y));
            }

            return scheme;
        }

        #endregion
    }

    /// <summary>
    /// Класс для расширений массивов <see cref="Array"/>
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
        public void SetCell(CellColor color, CellPosition position)
        {
            if (position.X >= Width || position.Y >= Height)
                throw new ArgumentOutOfRangeException(nameof(position));
            _cells[position.X,position.Y] = new Cell(color, position);
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
        /// Дефолтный конструктор
        /// </summary>
        public CellColor() { }

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
using ImageMagick;

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
            set
            {
                if (value.Length == 0) throw new ImageException("Бинарные данные изображения не могут быть пустыми");
                _data = value;
            }
        }

        /// <summary>
        /// Методы доступа к ширине изображения
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
        /// Методы доступа к высоте изображения
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
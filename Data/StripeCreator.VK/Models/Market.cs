namespace StripeCreator.VK.Models
{
    /// <summary>Информация о продукте</summary>
    public class Market
    {
        #region Constants

        /// <summary>Минимальная длина названия</summary>
        private const int TitleMinLength = 4;

        /// <summary>Максимальная длина названия</summary>
        private const int TitleMaxLength = 100;

        /// <summary>Минимальная длина описания</summary>
        private const int DescriptionMinLength = 10;

        /// <summary>Максимальная длина описания</summary>
        private const int DescriptionMaxLength = 5000;

        /// <summary>Минимальное значение цены</summary>
        private const decimal PriceMinValue = 0.01m;

        #endregion

        #region Private fields

        /// <summary>Название товара</summary>
        private string _title = string.Empty;

        /// <summary>Текст описания товара</summary>
        private string _description = string.Empty;

        /// <summary>Цена</summary>
        private decimal _price = decimal.Zero;

        #endregion

        #region Public properties

        /// <summary>Идентификатор товара</summary>
        public long? Id { get; }

        /// <summary>Название товара</summary>
        public string Title
        {
            get => _title;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException(nameof(Title), "Получена нулевая строка");
                if (value.Length is < TitleMinLength or > TitleMaxLength)
                    throw new ArgumentOutOfRangeException(nameof(Title),
                        $"Длина строки должна быть от {TitleMinLength} до {TitleMaxLength} символов");
                _title = value;
            }
        }

        /// <summary>Текст описания товара</summary>
        public string Description
        {
            get => _description;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException(nameof(Description), "Получена нулевая строка");
                if (value.Length is < DescriptionMinLength or > DescriptionMaxLength)
                    throw new ArgumentOutOfRangeException(nameof(Description),
                        $"Длина строки должна быть от {DescriptionMinLength} до {DescriptionMaxLength} символов");
                _description = value;
            }
        }

        /// <summary>Цена</summary>
        public decimal Price
        {
            get => _price;
            set
            {
                if (value < PriceMinValue)
                    throw new ArgumentOutOfRangeException(nameof(Price),
                        $"Цена товара не может быть < {PriceMinValue}");
                _price = value;
            }
        }

        /// <summary>Категория товара</summary>
        public Category Category { get; }

        /// <summary>Дата создания товара</summary>
        public DateTime? Date { get; }

        /// <summary>Изображение товара</summary>
        public Photo? MainPhoto { get; }

        #endregion

        #region Constructors

        /// <summary>Конструктор с полной инициализацией</summary>
        /// <param name="title">Название товара</param>
        /// <param name="description">Текст описания товара</param>
        /// <param name="price">Цена</param>
        /// <param name="category">Категория товара</param>
        /// <param name="id">Идентификатор товара</param>
        /// <param name="mainPhoto">Изображение товара</param>
        /// <param name="date">Дата создания товара</param>
        public Market(string title, string description, decimal price, Category category,
            long? id = null, Photo? mainPhoto = null, DateTime? date = null)
        {
            Title = title;
            Description = description;
            Price = price;
            Category = category;
            Id = id;
            Date = date;
            MainPhoto = mainPhoto;
        }

        #endregion
    }
}
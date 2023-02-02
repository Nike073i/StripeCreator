using StripeCreator.Core.Models;

namespace StripeCreator.Stripe.Models
{
    /// <summary>
    /// Абстрактный класс сущности материала
    /// </summary>
    public abstract class Material : Entity
    {
        #region Private fields

        /// <summary>
        /// Название материала
        /// </summary>
        private string _name = "Материал";

        /// <summary>
        /// Стоимость материала
        /// </summary>
        private decimal _price;

        /// <summary>
        /// Название прозводителя
        /// </summary>
        private string _manufacturer = "Производитель";

        #endregion

        #region Public properties

        /// <summary>
        /// Название материала
        /// </summary>
        public string Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException(nameof(Name), "Указано пустое значение названия материала");
                _name = value;
            }
        }

        /// <summary>
        /// Стоимость материала
        /// </summary>
        public decimal Price
        {
            get => _price;
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException(nameof(Price), "Стоимость материала не может быть <= 0");
                _price = value;
            }
        }

        /// <summary>
        /// Название прозводителя
        /// </summary>
        public string Manufacturer
        {
            get => _manufacturer;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException(nameof(Manufacturer), "Указано пустое значение названия производителя");
                _manufacturer = value;
            }
        }

        /// <summary>
        /// Цвет материала
        /// </summary>
        public Color Color { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Конструктор с полной инициализацией
        /// </summary>
        /// <param name="name">Название материала</param>
        /// <param name="price">Стоимость материала</param>
        /// <param name="manufacturer">Производитель материала</param>
        /// <param name="color">Цвет материала</param>
        protected Material(string name, decimal price, string manufacturer, Color color, Guid? id = null) : base(id)
        {
            Name = name;
            Price = price;
            Manufacturer = manufacturer;
            Color = color;
        }

        #endregion
    }
}
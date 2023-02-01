using StripeCreator.Core.Models;

namespace StripeCreator.Business.Models
{
    /// <summary>
    /// Класс сущности продукта
    /// </summary>
    public class Product : Entity
    {
        #region Private fields

        /// <summary>
        /// Название продукта
        /// </summary>
        private string _name = "Продукт";

        /// <summary>
        /// Цена продукта
        /// </summary>
        private decimal _price;

        /// <summary>
        /// Описание продукта
        /// </summary>
        private string _description = "Описание";

        #endregion

        #region Public properties

        /// <summary>
        /// Название продукта
        /// </summary>
        public string Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException(nameof(Name), "Указано пустое значение названия продукта");
                _name = value;
            }
        }

        /// <summary>
        /// Цена продукта
        /// </summary>
        public decimal Price
        {
            get => _price;
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException(nameof(Price), "Цена продукта не может быть <= 0");
                _price = value;
            }
        }


        /// <summary>
        /// Описание продукта
        /// </summary>
        public string Description
        {
            get => _description;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentNullException(nameof(Description), "Указано пустое значение описания продукта");
                _description = value;
            }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Конструктор с полной инициализацией
        /// </summary>
        /// <param name="name">Название</param>
        /// <param name="price">Цена</param>
        /// <param name="description">Описание</param>
        /// <param name="id">Идентификатор сущности</param>
        public Product(string name, decimal price, string description, Guid? id = null) : base(id)
        {
            Name = name;
            Price = price;
            Description = description;
        }

        #endregion
    }
}
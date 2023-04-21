using StripeCreator.Business.Models;
using System;

namespace StripeCreator.WPF
{
    /// <summary>
    /// ViewModel формирования сущности продукта
    /// </summary>
    public class ProductFormationViewModel : EntityFormationViewModel
    {
        #region Public properties

        /// <summary>
        /// Название продукта
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Цена продукта
        /// </summary>
        public decimal? Price { get; set; }

        /// <summary>
        /// Описание продукта
        /// </summary>
        public string? Description { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public ProductFormationViewModel() { }

        /// <summary>
        /// Конструктор с инициализацией полей по ViewModel сущности ткани
        /// </summary>
        /// <param name="viewModel">ViewModel сущности ткани</param>
        public ProductFormationViewModel(Product entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Price = entity.Price;
            Description = entity.Description;
        }

        #endregion

        #region Overrides

        protected override bool ValidateData()
        {
            if (string.IsNullOrWhiteSpace(Name) || !Price.HasValue
                || string.IsNullOrWhiteSpace(Description))
            {
                ErrorString = "Заполните обязательные поля";
                return false;
            }
            return true;
        }

        protected override IEntityViewModel? TryCreateEntity()
        {
            try
            {
                var product = new Product(Name!, Price!.Value, Description!, Id);
                return new ProductViewModel(product);
            }
            catch (Exception ex)
            {
                ErrorString = ex.Message;
                return null;
            }
        }

        #endregion
    }
}

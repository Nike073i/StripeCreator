using StripeCreator.Core.Models;
using StripeCreator.Stripe.Models;
using System;
using System.Collections;

namespace StripeCreator.WPF
{
    /// <summary>
    /// ViewModel формирования сущности ткани
    /// </summary>
    public class ClothFormationViewModel : EntityFormationViewModel
    {
        #region Public properties

        /// <summary>
        /// Название ткани
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Стоимость ткани
        /// </summary>
        public decimal? Price { get; set; }

        /// <summary>
        /// Название прозводителя
        /// </summary>
        public string? Manufacturer { get; set; }

        /// <summary>
        /// Код цвета материала
        /// </summary>
        public string? ColorHex { get; set; }

        /// <summary>
        /// "Каунт" ткани. Количество клеток в 10 см.
        /// </summary>
        public int? Count { get; set; }

        /// <summary>
        /// Тип ткани
        /// </summary>
        public ClothType? Type { get; set; }

        /// <summary>
        /// Список всех доступных типов ткани
        /// </summary>
        public static IEnumerable Types { get; } = Enum.GetValues(typeof(ClothType));

        #endregion

        #region Constructors

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public ClothFormationViewModel()
        {
            ColorHex = Color.DefaultColor;
        }

        /// <summary>
        /// Конструктор с инициализацией полей по ViewModel сущности ткани
        /// </summary>
        /// <param name="entity">Cущность ткани</param>
        public ClothFormationViewModel(Cloth entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Price = entity.Price;
            Manufacturer = entity.Manufacturer;
            ColorHex = entity.Color.HexValue;
            Count = entity.Count;
            Type = entity.Type;
        }

        #endregion

        #region Overrides

        protected override bool ValidateData()
        {
            if (string.IsNullOrWhiteSpace(Name) || !Price.HasValue
                || string.IsNullOrWhiteSpace(Manufacturer) || string.IsNullOrWhiteSpace(ColorHex)
                || !Count.HasValue || !Type.HasValue)
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
                var color = new Color(ColorHex!);
                var cloth = new Cloth(Name!, Price!.Value, Manufacturer!, color, Type!.Value, Count!.Value, Id);
                return new ClothViewModel(cloth);
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

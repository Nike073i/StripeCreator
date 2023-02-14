using StripeCreator.Core.Models;
using StripeCreator.Stripe.Models;
using System;
using System.Collections;

namespace StripeCreator.WPF
{
    /// <summary>
    /// ViewModel формирования сущности нити
    /// </summary>
    public class ThreadFormationViewModel : EntityFormationViewModel
    {
        #region Public properties

        /// <summary>
        /// Название нити
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Стоимость нити
        /// </summary>
        public decimal? Price { get; set; }

        /// <summary>
        /// Название прозводителя
        /// </summary>
        public string? Manufacturer { get; set; }

        /// <summary>
        /// Код цвета нити
        /// </summary>
        public string? ColorHex { get; set; }

        /// <summary>
        /// Вес нити
        /// </summary>
        public int? Weight { get; set; }

        /// <summary>
        /// Тип нити
        /// </summary>
        public ThreadType? Type { get; set; }

        /// <summary>
        /// Список всех доступных типов нити
        /// </summary>
        public static IEnumerable Types { get; } = Enum.GetValues(typeof(ThreadType));

        #endregion

        #region Constructors

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public ThreadFormationViewModel() { }

        /// <summary>
        /// Конструктор с инициализацией полей по ViewModel сущности нити
        /// </summary>
        /// <param name="viewModel">ViewModel сущности нити</param>
        public ThreadFormationViewModel(Thread entity)
        {
            Id = entity.Id;
            Name = entity.Name;
            Price = entity.Price;
            Manufacturer = entity.Manufacturer;
            ColorHex = entity.Color.HexValue;
            Weight = entity.Weight;
            Type = entity.Type;
        }

        #endregion

        #region Overrides

        protected override bool ValidateData()
        {
            if (string.IsNullOrWhiteSpace(Name) || !Price.HasValue
                || string.IsNullOrWhiteSpace(Manufacturer) || string.IsNullOrWhiteSpace(ColorHex)
                || !Weight.HasValue || !Type.HasValue)
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
                var thread = new Thread(Name!, Price!.Value, Manufacturer!, color, Type!.Value, Weight!.Value, Id);
                return new ThreadViewModel(thread);
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

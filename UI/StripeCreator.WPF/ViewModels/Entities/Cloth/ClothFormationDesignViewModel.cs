using StripeCreator.Stripe.Models;

namespace StripeCreator.WPF
{
    /// <summary>
    /// Класс view-model представления формирования сущности ткани для design-mode
    /// </summary>
    public class ClothFormationDesignViewModel : ClothFormationViewModel
    {
        #region Singleton

        /// <summary>
        /// Экземпляр для показа в Design-mode
        /// </summary>
        public static ClothFormationDesignViewModel Instance => new();

        #endregion

        #region Design Data 

        private static string _designName = "Канва K18";
        private static decimal _designPrice = 300m;
        private static string _designManufacturer = "Gamma";
        private static string _designColorHex = "#3F534B";
        private static int _designCount = 71;
        private static ClothType _designType = ClothType.Aida;

        #endregion

        #region Constructors 

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public ClothFormationDesignViewModel()
        {
            Name = _designName;
            Price = _designPrice;
            Manufacturer = _designManufacturer;
            ColorHex = _designColorHex;
            Count = _designCount;
            Type = _designType;
        }

        #endregion
    }
}
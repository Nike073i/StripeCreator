using StripeCreator.Stripe.Models;

namespace StripeCreator.WPF
{
    /// <summary>
    /// Класс view-model представления формирования сущности нити для design-mode
    /// </summary>
    public class ThreadFormationDesignViewModel : ThreadFormationViewModel
    {
        #region Singleton

        /// <summary>
        /// Экземпляр для показа в Design-mode
        /// </summary>
        public static ThreadFormationDesignViewModel Instance => new();

        #endregion

        #region Design Data 

        private static string _designName = "Шелковые JST2 50";
        private static decimal _designPrice = 290m;
        private static string _designManufacturer = "SumikoThread";
        private static string _designColorHex = "#3F534B";
        private static int _designWeight = 215;
        private static ThreadType _designType = ThreadType.Silk;

        #endregion

        #region Constructors 

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public ThreadFormationDesignViewModel()
        {
            Name = _designName;
            Price = _designPrice;
            Manufacturer = _designManufacturer;
            ColorHex = _designColorHex;
            Weight = _designWeight;
            Type = _designType;
        }

        #endregion
    }
}
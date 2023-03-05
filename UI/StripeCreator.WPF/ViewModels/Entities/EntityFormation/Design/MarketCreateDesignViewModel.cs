using System;

namespace StripeCreator.WPF
{
    /// <summary>
    /// Класс view-model представления создания товара для design-mode
    /// </summary>
    public class MarketCreateDesignViewModel : MarketCreateViewModel
    {
        #region Singleton

        /// <summary>
        /// Экземпляр для показа в Design-mode
        /// </summary>
        public static MarketCreateDesignViewModel Instance => new();

        #endregion

        #region Design Data 

        private static string _designName = "Вышивка - озеро у дома";
        private static decimal _designPrice = 1500m;
        private static string _designDescription = "Вышивка по картине известного художника Васильева И.Н." + Environment.NewLine +
                                                   "Размер схемы в сантиметрах - 15х15" + Environment.NewLine +
                                                   "Выполнена нитями Мулине от производителя DMC на канве AIDA 18" + Environment.NewLine +
                                                   "Отлично подходит для подарка своим знакомым и родственникам";

        #endregion

        #region Constructors 

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public MarketCreateDesignViewModel()
        {
            Name = _designName;
            Price = _designPrice;
            Description = _designDescription;
        }

        #endregion
    }
}
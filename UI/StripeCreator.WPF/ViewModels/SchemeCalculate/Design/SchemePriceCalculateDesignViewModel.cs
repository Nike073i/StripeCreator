namespace StripeCreator.WPF
{
    /// <summary>
    /// Класс view-model для представления расчета стоимости в design-mode
    /// </summary>
    public class SchemePriceCalculateDesignViewModel : SchemePriceCalculateViewModel
    {
        #region Singleton

        /// <summary>
        /// Экземпляр для показа в Design-mode
        /// </summary>
        public static SchemePriceCalculateDesignViewModel Instance => new();

        #endregion

        #region Constructors

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public SchemePriceCalculateDesignViewModel() { }

        #endregion
    }
}

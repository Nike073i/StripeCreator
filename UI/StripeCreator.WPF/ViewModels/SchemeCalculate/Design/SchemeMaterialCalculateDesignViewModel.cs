namespace StripeCreator.WPF
{
    /// <summary>
    /// Класс view-model для представления расчета материалов в design-mode
    /// </summary>
    public class SchemeMaterialCalculateDesignViewModel : SchemeMaterialCalculateViewModel
    {
        #region Singleton

        /// <summary>
        /// Экземпляр для показа в Design-mode
        /// </summary>
        public static SchemeMaterialCalculateDesignViewModel Instance => new();

        #endregion

        #region Constructors

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public SchemeMaterialCalculateDesignViewModel() { }

        #endregion
    }
}

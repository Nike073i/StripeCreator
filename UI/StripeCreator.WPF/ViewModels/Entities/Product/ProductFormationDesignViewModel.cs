namespace StripeCreator.WPF
{
    /// <summary>
    /// Класс view-model представления формирования сущности продукта для design-mode
    /// </summary>
    public class ProductFormationDesignViewModel : ProductFormationViewModel
    {
        #region Singleton

        /// <summary>
        /// Экземпляр для показа в Design-mode
        /// </summary>
        public static ProductFormationDesignViewModel Instance => new();

        #endregion

        #region Design Data 

        private static string _designName = "Нашивка The Offspring";
        private static decimal _designPrice = 500m;
        private static string _designDescription = "Очень хороший, красивый товар. \n Создан по кайфу \n Качество - высший сорт \n Оценка - Огонь, пожар, бомба! \n";

        #endregion

        #region Constructors 

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public ProductFormationDesignViewModel()
        {
            Name = _designName;
            Price = _designPrice;
            Description = _designDescription;
        }

        #endregion
    }
}
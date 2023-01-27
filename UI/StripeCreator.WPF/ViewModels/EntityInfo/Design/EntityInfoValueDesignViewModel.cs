namespace StripeCreator.WPF
{
    /// <summary>
    /// Класс view-model для поля из данных сущности
    /// </summary>
    public class EntityInfoValueDesignViewModel : EntityInfoValueViewModel
    {
        #region Singleton

        /// <summary>
        /// Экземпляр для показа в Design-mode
        /// </summary>
        public static EntityInfoValueDesignViewModel Instance => new();

        #endregion

        #region Constructors 

        public EntityInfoValueDesignViewModel() : base("Поле", "Значение") { }

        #endregion
    }
}

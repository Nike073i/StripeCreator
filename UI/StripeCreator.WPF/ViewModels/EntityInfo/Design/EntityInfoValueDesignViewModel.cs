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

        #region Design Data

        private readonly static string DesignName = "Поле";
        private readonly static string DesignValue = "Значение";

        #endregion

        #region Constructors 

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public EntityInfoValueDesignViewModel() : base(DesignName, DesignValue) { }

        #endregion
    }
}

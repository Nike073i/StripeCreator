namespace StripeCreator.WPF
{
    /// <summary>
    /// Поле данных сущности
    /// </summary>
    public class EntityInfoValueViewModel : BaseViewModel
    {
        #region Public properties

        /// <summary>
        /// Название поля данных
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Значение поля данных
        /// </summary>
        public string Value { get; set; }

        #endregion

        #region Constructors 

        /// <summary>
        /// Конструктор с полной инициализацией
        /// </summary>
        /// <param name="name">Название поля</param>
        /// <param name="value">Значение поля</param>
        public EntityInfoValueViewModel(string name, string value)
        {
            Name = name;
            Value = value;
        }

        #endregion
    }
}

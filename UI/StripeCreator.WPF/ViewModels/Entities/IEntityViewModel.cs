namespace StripeCreator.WPF
{
    /// <summary>
    /// Интерфейс ViewModel сущности
    /// </summary>
    public interface IEntityViewModel
    {
        /// <summary>
        /// Получение информации сущности
        /// </summary>
        EntityInfoViewModel GetData { get; }
    }
}

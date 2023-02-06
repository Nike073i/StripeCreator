namespace StripeCreator.DAL.Exceptions
{
    /// <summary>
    /// Ошибка запроса сущности. Сущность не найдена
    /// </summary>
    public class EntityNotFoundException : Exception
    {
        #region Constructors 

        /// <summary>
        /// Конструктор с инициализацией сообщения ошибки
        /// </summary>
        /// <param name="message">Сообщение ошибки</param>
        public EntityNotFoundException(string message) : base(message) { }

        #endregion
    }
}
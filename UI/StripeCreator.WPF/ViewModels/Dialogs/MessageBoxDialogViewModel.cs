namespace StripeCreator.WPF
{
    public class MessageBoxDialogViewModel : BaseViewModel
    {
        #region Public properties

        /// <summary>
        /// Заголовок окна
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Сообщение для отображения
        /// </summary>
        public string Message { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Конструктор с полной инициализацией
        /// </summary>
        /// <param name="title">Заголовок окна</param>
        /// <param name="message">Сообщение для отображения</param>
        public MessageBoxDialogViewModel(string title, string message)
        {
            Title = title;
            Message = message;
        }

        #endregion
    }
}

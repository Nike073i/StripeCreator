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

        /// <summary>
        /// Текст кнопки подтверждения
        /// </summary>
        public string OkText { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Конструктор с полной инициализацией
        /// </summary>
        /// <param name="title">Заголовок окна</param>
        /// <param name="message">Сообщение для отображения</param>
        /// <param name="okText">Текст кнопки подтверждения</param>
        public MessageBoxDialogViewModel(string title, string message, string okText = "Да")
        {
            Title = title;
            Message = message;
            OkText = okText;
        }

        #endregion
    }
}

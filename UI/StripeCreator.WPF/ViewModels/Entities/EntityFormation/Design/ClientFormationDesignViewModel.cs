namespace StripeCreator.WPF
{
    /// <summary>
    /// Класс view-model представления формирования сущности клиента для design-mode
    /// </summary>
    public class ClientFormationDesignViewModel : ClientFormationViewModel
    {
        #region Singleton

        /// <summary>
        /// Экземпляр для показа в Design-mode
        /// </summary>
        public static ClientFormationDesignViewModel Instance => new();

        #endregion

        #region Design Data 

        private static string _designFirstName = "Николай";
        private static string _designSecondName = "Иванов";
        private static string _designContactNumber = "+79156300123";
        private static string _designEmail = "example@mail.ru";
        private static string _designOther = "Вконтакте - vk.com/id536525252";

        #endregion

        #region Constructors 

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public ClientFormationDesignViewModel()
        {
            FirstName = _designFirstName;
            SecondName = _designSecondName;
            ContactNumber = _designContactNumber;
            Email = _designEmail;
            Other = _designOther;
        }

        #endregion
    }
}
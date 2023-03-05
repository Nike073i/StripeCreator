using System;

namespace StripeCreator.WPF
{
    /// <summary>
    /// Класс view-model представления создания товара для design-mode
    /// </summary>
    public class PublishMessageDesignViewModel : PublishMessageViewModel
    {
        #region Singleton

        /// <summary>
        /// Экземпляр для показа в Design-mode
        /// </summary>
        public static PublishMessageDesignViewModel Instance => new();

        #endregion

        #region Design Data 

        private static string _designMessage = "Новая вышивка по картине известного художника Васильева И.Н." + Environment.NewLine +
                                                   "Размер схемы в сантиметрах - 15х15" + Environment.NewLine +
                                                   "Выполнена нитями Мулине от производителя DMC на канве AIDA 18" + Environment.NewLine +
                                                   "Отлично подходит для подарка своим знакомым и родственникам";

        #endregion

        #region Constructors 

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
        public PublishMessageDesignViewModel()
        {
            Message = _designMessage;
        }

        #endregion
    }
}
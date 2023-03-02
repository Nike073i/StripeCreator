using StripeCreator.Core.Models;
using StripeCreator.Stripe.Models;

namespace StripeCreator.WPF
{
    /// <summary>
    /// Класс view-model для связки цвета и нити
    /// </summary>
    public class ColorThreadViewModel : BaseViewModel
    {
        #region Public properties

        /// <summary>
        /// Цвет клетки
        /// </summary>
        public Color Color { get; set; }

        /// <summary>
        /// Нить
        /// </summary>
        public Thread Thread { get; set; }

        #endregion

        #region Constructors

        /// <summary>
        /// Конструктор по умолчанию
        /// </summary>
#nullable disable
        public ColorThreadViewModel() { }
#nullable enable

        /// <summary>
        /// Конструктор с полной инициализацией
        /// </summary>
        /// <param name="color">Цвет</param>
        /// <param name="thread">Нить</param>
        public ColorThreadViewModel(Color color, Thread thread)
        {
            Color = color;
            Thread = thread;
        }

        #endregion
    }
}

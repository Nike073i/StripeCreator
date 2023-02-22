namespace StripeCreator.Stripe.Models
{
    /// <summary>
    /// Методы дискретизации изображения
    /// </summary>
    public enum ResizeMethod
    {
        /// <summary>
        /// Изменение без размытия
        /// </summary>
        Adaptive = 0,
        /// <summary>
        /// Изменение путем репликации
        /// </summary>
        Sample = 1,
        /// <summary>
        /// Изменение с усреднением пикселей
        /// </summary>
        Scale = 2,
        /// <summary>
        /// Изменение методом "Резьба по шву"
        /// </summary>
        Liquid = 3,
    }
}

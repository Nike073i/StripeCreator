namespace StripeCreator.Business.Enums
{
    /// <summary>
    /// Статусы заказа
    /// </summary>
    public enum OrderStatus
    {
        /// <summary>
        /// Заказ создан
        /// </summary>
        Created,

        /// <summary>
        /// Заказ отменен
        /// </summary>
        Canceled,

        /// <summary>
        /// Заказ оплачен
        /// </summary>
        Paid,

        /// <summary>
        /// Заказ обработан
        /// </summary>
        Processed,

        /// <summary>
        /// Заказ отправлен
        /// </summary>
        Sent
    }
}
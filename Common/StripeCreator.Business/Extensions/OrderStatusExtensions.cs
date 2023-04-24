using StripeCreator.Business.Enums;

namespace StripeCreator.Business.Extensions
{
    public static class OrderStatusExtensions
    {
        public static string ConvertToString(this OrderStatus orderStatus)
        {
            var stringValue = orderStatus switch
            {
                OrderStatus.Created => "Создан",
                OrderStatus.Canceled => "Отменен",
                OrderStatus.Paid => "Оплачен",
                OrderStatus.Processed => "В процессе",
                OrderStatus.Sent => "Отправлен",
                _ => throw new NotImplementedException(),
            };
            return stringValue;
        }
    }
}

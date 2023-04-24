using StripeCreator.Business.Enums;
using StripeCreator.Business.Extensions;
using System;
using System.Globalization;

namespace StripeCreator.WPF
{
    class OrderStatusValueConverter : BaseValueConverter<OrderStatusValueConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not OrderStatus status)
                throw new ArgumentNullException(nameof(value), "Не указан статус для конвертации");
            return status.ConvertToString();
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

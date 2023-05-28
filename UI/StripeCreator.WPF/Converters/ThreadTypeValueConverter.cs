using StripeCreator.Stripe.Extensions;
using StripeCreator.Stripe.Models;
using System;
using System.Globalization;

namespace StripeCreator.WPF
{
    class ThreadTypeValueConverter : BaseValueConverter<ThreadTypeValueConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not ThreadType type)
                throw new ArgumentNullException(nameof(value), "Не указан тип для конвертации");
            return type.ConverToString();
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

using StripeCreator.Stripe.Models;
using System;
using System.Globalization;

namespace StripeCreator.WPF
{
    class EmbroideryMethodValueConverter : BaseValueConverter<EmbroideryMethodValueConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not EmbroideryMethod method)
                throw new ArgumentNullException(nameof(value), "Не указан метод для конвертации");
            return method switch
            {
                EmbroideryMethod.In1Thread => "В 1 нить",
                EmbroideryMethod.In2Thread => "В 2 нити",
                _ => throw new NotImplementedException(),
            };
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

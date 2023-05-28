using StripeCreator.Stripe.Models;
using System;
using System.Globalization;

namespace StripeCreator.WPF
{
    class EmbroideryTypeValueConverter : BaseValueConverter<EmbroideryTypeValueConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not EmbroideryType type)
                throw new ArgumentNullException(nameof(value), "Не указан метод для конвертации");
            return type switch
            {
                EmbroideryType.Cross => "Крестиком",
                EmbroideryType.SmoothHorizontal => "Горизонтальной гладью",
                EmbroideryType.SmoothVertical => "Вертикальной гладью",
                EmbroideryType.SmoothDiagonal => "Диагональной гладью",
                _ => throw new NotImplementedException(),
            };
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

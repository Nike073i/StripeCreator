using System;
using System.Globalization;

namespace StripeCreator.WPF
{
    class ApplicationPageValueConverter : BaseValueConverter<ApplicationPageValueConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (ApplicationPage)value switch
            {
                ApplicationPage.Welcome => new WelcomePage(IoC.GetRequiredService<WelcomePageViewModel>()),
                ApplicationPage.DataStore => new DataStorePage(IoC.GetRequiredService<DataStorePageViewModel>()),
                _ => throw new ArgumentException("Получена несуществующая страница для конвертации"),
            };
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

using System;
using System.Globalization;

namespace StripeCreator.WPF
{
    class ApplicationPageValueConverter : BaseValueConverter<ApplicationPageValueConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is not ApplicationPage page)
                throw new ArgumentNullException("Не указана страница для конвертации");

            switch (page)
            {
                case ApplicationPage.Welcome:
                    return new WelcomePage(IoC.GetRequiredService<WelcomePageViewModel>());
                case ApplicationPage.DataStore:
                    return new DataStorePage(IoC.GetRequiredService<DataStorePageViewModel>());
                case ApplicationPage.Order:
                    return new OrderPage(IoC.GetRequiredService<OrderPageViewModel>());
                case ApplicationPage.Report:
                    return new ReportPage(IoC.GetRequiredService<ReportPageViewModel>());
                case ApplicationPage.ImageProcessing:
                    return new ImageProcessingPage(IoC.GetRequiredService<ImageProcessingPageViewModel>());
                default:
                    throw new ArgumentException("Получена несуществующая страница для конвертации");
            }
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

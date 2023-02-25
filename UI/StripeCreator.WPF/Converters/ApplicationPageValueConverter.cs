using StripeCreator.Stripe.Models;
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
            var applicationViewModel = IoC.GetRequiredService<ApplicationViewModel>();
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
                case ApplicationPage.Scheme:
                    var viewModel = IoC.GetRequiredService<SchemePageViewModel>();
                    if (applicationViewModel.CurrentPageArg is Image schemeTemplate)
                        viewModel.Scheme = new Scheme(schemeTemplate);
                    return new SchemePage(viewModel);
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

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
                    applicationViewModel.WindowTitle = "Добро пожаловать";
                    return new WelcomePage(IoC.GetRequiredService<WelcomePageViewModel>());
                case ApplicationPage.DataStore:
                    applicationViewModel.WindowTitle = "Справочники";
                    return new DataStorePage(IoC.GetRequiredService<DataStorePageViewModel>());
                case ApplicationPage.Order:
                    applicationViewModel.WindowTitle = "Заказы";
                    return new OrderPage(IoC.GetRequiredService<OrderPageViewModel>());
                case ApplicationPage.Report:
                    applicationViewModel.WindowTitle = "Отчеты";
                    return new ReportPage(IoC.GetRequiredService<ReportPageViewModel>());
                case ApplicationPage.ImageProcessing:
                    applicationViewModel.WindowTitle = "Обработка изображения";
                    return new ImageProcessingPage(IoC.GetRequiredService<ImageProcessingPageViewModel>());
                case ApplicationPage.Scheme:
                    applicationViewModel.WindowTitle = "Схема";
                    var viewModel = IoC.GetRequiredService<SchemePageViewModel>();
                    if (applicationViewModel.CurrentPageArg is (Image schemeTemplate, int targetClothCount))
                        viewModel.Scheme = new Scheme(schemeTemplate, targetClothCount);
                    if (applicationViewModel.CurrentPageArg is Scheme scheme)
                        viewModel.Scheme = scheme;
                    return new SchemePage(viewModel);
                case ApplicationPage.Community:
                    applicationViewModel.WindowTitle = "Сообщество";
                    return new CommunityPage(IoC.GetRequiredService<CommunityPageViewModel>());
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

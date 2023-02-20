﻿using System;
using System.Globalization;

namespace StripeCreator.WPF
{
    class ApplicationPageValueConverter : BaseValueConverter<ApplicationPageValueConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => (ApplicationPage)value switch
            {
                ApplicationPage.Welcome => new WelcomePage(IoC.GetRequiredService<WelcomePageViewModel>()),
                ApplicationPage.DataStore => new DataStorePage(IoC.GetRequiredService<DataStorePageViewModel>()),
                ApplicationPage.Order => new OrderPage(IoC.GetRequiredService<OrderPageViewModel>()),
                ApplicationPage.Report => new ReportPage(IoC.GetRequiredService<ReportPageViewModel>()),
                _ => throw new ArgumentException("Получена несуществующая страница для конвертации"),
            };

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}

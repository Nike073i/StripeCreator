using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace StripeCreator.WPF
{
    /// <summary>
    /// Базовый класс для конвертеров в xaml
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseValueConverter<T> : MarkupExtension, IValueConverter
        where T : class, new()
    {
        #region Private fields 

        private static T? _converter = null;

        #endregion

        #region Markup Extension Methods

        /// <summary>
        /// Снабжает статическим экземпляром конвертера
        /// </summary>
        /// <param name="serviceProvider">Поставщик сервисов</param>
        /// <returns></returns>
        public override object ProvideValue(IServiceProvider serviceProvider) => _converter ??= new();

        #endregion

        #region Value Converter Methods

        /// <summary>
        /// Метод преобразования туда
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public abstract object Convert(object value, Type targetType, object parameter, CultureInfo culture);

        /// <summary>
        /// Метод преобразования обратно
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public abstract object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture);

        #endregion
    }
}

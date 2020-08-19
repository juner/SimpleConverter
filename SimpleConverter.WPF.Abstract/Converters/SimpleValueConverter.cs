using System;
using System.Globalization;
using System.Windows.Data;

namespace SimpleConverter.Converters
{
    public abstract class SimpleValueConverter<TSource, TTarget> : SimpleValueConverter<TSource, object?, TTarget> { }
    public abstract class SimpleValueConverter<TSource, TParameter, TTarget> : IValueConverter
    {
        public virtual object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => Convert((TSource)value, (TParameter)parameter, culture);

        public abstract TTarget Convert(TSource value, TParameter parameter, CultureInfo culture);

        public virtual object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => ConvertBack((TTarget)value, (TParameter)parameter, culture);
        public abstract TSource ConvertBack(TTarget value, TParameter parameter, CultureInfo culture);
    }
}

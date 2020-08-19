using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;
using System.Windows.Markup;

namespace SimpleConverter.MarkupExtensions
{
    public abstract class SimpleValueConverterMarkupExtension<TSource, TTarget> : SimpleValueConverterMarkupExtension<TSource, object?, TTarget> { }
    public abstract class SimpleValueConverterMarkupExtension<TSource, TParameter, TTarget> : MarkupExtension, IValueConverter
    {
        public virtual object? Convert(object value, Type targetType, object parameter, CultureInfo culture)
            => Convert((TSource)value, (TParameter)parameter, culture);

        public abstract TTarget Convert(TSource value, TParameter parameter, CultureInfo culture);

        public virtual object? ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            => ConvertBack((TTarget)value, (TParameter)parameter, culture);
        public abstract TSource ConvertBack(TTarget value, TParameter parameter, CultureInfo culture);
        public override object? ProvideValue(IServiceProvider serviceProvider) => this;
    }
}

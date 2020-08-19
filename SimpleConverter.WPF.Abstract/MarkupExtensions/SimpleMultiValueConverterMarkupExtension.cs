using System;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows.Data;
using System.Windows.Markup;

namespace SimpleConverter.MarkupExtensions
{
    public abstract class SimpleMultiValueConverterMarkupExtension<TSource, TTarget> : SimpleMultiValueConverterMarkupExtension<TSource, object?, TTarget>
        where TSource : ITuple
    { }
    public abstract class SimpleMultiValueConverterMarkupExtension<TSource, TParameter, TTarget> : MarkupExtension, IMultiValueConverter
        where TSource : ITuple
    {
        public virtual object? Convert(object?[] values, Type targetType, object parameter, CultureInfo culture)
            => Convert(ToValueTuple(values), (TParameter)parameter, culture);
        public abstract TTarget Convert(TSource values, TParameter parameter, CultureInfo culture);
        protected TSource ToValueTuple(object?[] values)
        {
            var Type = typeof(TSource); var GenericArguments = Type.GetGenericArguments();

            if (values.Length != GenericArguments.Length)
                throw new ArgumentException($"{nameof(values)} length:{values.Length} GenericArguments length:{GenericArguments.Length} no match.", nameof(values));
            var Constructor = Type.GetConstructor(GenericArguments);
            if (!(Constructor is ConstructorInfo))
                throw new ArgumentException($"not have consructor {nameof(GenericArguments)}");
            return (TSource)Constructor.Invoke(values)!;

        }
        public virtual object?[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
            => FromValueTuple(ConvertBack((TTarget)value, (TParameter)parameter, culture));
        public abstract TSource ConvertBack(TTarget value, TParameter parameter, CultureInfo culture);
        protected object?[] FromValueTuple(TSource source)
        {
            var objectArray = new object?[source.Length];
            for (var i = 0; i < source.Length; i++)
                objectArray[i] = source[i];
            return objectArray;
        }
        public override object? ProvideValue(IServiceProvider serviceProvider) => this;
    }
}

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SimpleConverter.Converters.Tests
{
    [TestClass()]
    public class StringToIntConverterTests
    {
        static IEnumerable<object?[]> ConvertTestData
        {
            get
            {
                yield return ConvertTest(new StringToIntConverter(), (int?)null, "test", typeof(int?), null, CultureInfo.InvariantCulture);
                static object?[] ConvertTest(IValueConverter Converter, object? Expected, object? Value, Type Target, object? Parameter, CultureInfo Culture)
                    => new object?[] { Converter, Expected, Value, Target, Parameter, Culture };
            }
        }
        [TestMethod, DynamicData(nameof(ConvertTestData))]
        public void ConvertTest(IValueConverter Converter, object? Expected, object? Value, Type Target, object? Parameter, CultureInfo Culture)
            => Assert.AreEqual(Expected, Converter.Convert(Value, Target, Parameter, Culture));
        static IEnumerable<object?[]> ConvertBackTestData
        {
            get
            {
                yield return ConvertBackTest(new StringToIntConverter(), "", null, typeof(int), null, CultureInfo.InvariantCulture);
                yield return ConvertBackTest(new StringToIntConverter(), "1E", 30, typeof(int), NumberStyles.HexNumber, CultureInfo.InvariantCulture);
                static object?[] ConvertBackTest(IValueConverter Converter, object? Expected, object? Value, Type Target, object? Parameter, CultureInfo Culture)
                    => new object?[] { Converter, Expected, Value, Target, Parameter, Culture };
            }
        }
        [TestMethod, DynamicData(nameof(ConvertBackTestData))]
        public void ConvertBackTest(IValueConverter Converter, object? Expected, object? Value, Type Target, object? Parameter, CultureInfo Culture)
            => Assert.AreEqual(Expected, Converter.ConvertBack(Value, Target, Parameter, Culture));
    }
}

using System.Globalization;

namespace GenericConverter
{
    public class StringToIntConverter : SimpleValueConverter<string, NumberStyles, int?>
    {
        public override int? Convert(string value, NumberStyles parameter, CultureInfo culture)
            => int.TryParse(value, parameter, culture, out var result) ? result : (int?)null;

        public override string ConvertBack(int? value, NumberStyles parameter, CultureInfo culture)
            => value?.ToString(parameterToFormatString(parameter), culture) ?? string.Empty;
        private string parameterToFormatString(NumberStyles parameter)
            => (parameter | NumberStyles.AllowCurrencySymbol) == NumberStyles.AllowCurrencySymbol ? "C"
            : (parameter | NumberStyles.AllowHexSpecifier) == NumberStyles.AllowHexSpecifier ? "X"
            : "D";
    }
}

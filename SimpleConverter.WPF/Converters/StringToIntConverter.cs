using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace SimpleConverter.Converters
{
    public class StringToIntConverter : SimpleValueConverter<string, NumberStyles?, int?>
    {
        public override int? Convert(string value, NumberStyles? parameter, CultureInfo culture)
            => int.TryParse(value, parameter ?? 0, culture, out var result) ? result : (int?)null;

        public override string ConvertBack(int? value, NumberStyles? parameter, CultureInfo culture)
            => value?.ToString(ParameterToFormatString(parameter), culture) ?? string.Empty;
        private string ParameterToFormatString(NumberStyles? parameter)
            => FormatList.FirstOrDefault(v => HasFlags(parameter, v.Style)).Format is string Format
            && !string.IsNullOrEmpty(Format)
            ? Format : DefaultFormatStyle;
        private static readonly IEnumerable<(NumberStyles Style, string Format)> FormatList = new List<(NumberStyles, string)>{
            (NumberStyles.AllowCurrencySymbol, "C"),
            (NumberStyles.AllowHexSpecifier, "X"),
        }.AsReadOnly();
        private static readonly string DefaultFormatStyle = "D";
        private bool HasFlags(NumberStyles? parameter, NumberStyles flags)
            => (parameter & flags) == flags;
    }
}

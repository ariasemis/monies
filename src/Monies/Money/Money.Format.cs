using System;
using System.Globalization;

namespace Monies
{
    public sealed partial class Money<TCurrency> : IFormattable
    {
        public override string ToString()
            => ToString("G", CultureInfo.CurrentCulture);

        public string ToString(string format)
            => ToString(format, CultureInfo.CurrentCulture);

        public string ToString(IFormatProvider provider)
            => ToString("G", provider);

        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (string.IsNullOrEmpty(format) || format.Equals("G", StringComparison.InvariantCultureIgnoreCase))
                format = "C";

            if (!format.StartsWith("C", StringComparison.InvariantCultureIgnoreCase))
                throw new FormatException($"The '{format}' format string is not supported.");

            var numberFormatInfo = NumberFormatInfo.GetInstance(formatProvider);
            numberFormatInfo = (NumberFormatInfo)numberFormatInfo.Clone();

            numberFormatInfo.CurrencySymbol = (Currency is IFormattable currencyFormat) ?
                currencyFormat.ToString("G", formatProvider) : Currency.ToString();

            return Amount.ToString(format, numberFormatInfo);
        }
    }
}

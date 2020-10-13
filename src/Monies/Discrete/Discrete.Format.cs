using System.Globalization;

namespace Monies
{
    public sealed partial class Discrete<TCurrency>
    {
        // TODO: implement IFormattable

        public override string ToString()
        {
            var numberFormatInfo = NumberFormatInfo.GetInstance(CultureInfo.CurrentCulture);
            numberFormatInfo = (NumberFormatInfo)numberFormatInfo.Clone();

            numberFormatInfo.CurrencySymbol = Currency.ToString() ?? string.Empty;

            var normalized = (decimal)Amount / Unit.Scale;

            return normalized.ToString("C", numberFormatInfo);
        }
    }
}

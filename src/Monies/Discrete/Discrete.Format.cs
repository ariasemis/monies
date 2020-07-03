using System.Globalization;

namespace Monies
{
    public sealed partial class Discrete<TCurrency, TUnit>
    {
        // TODO: implement IFormattable

        public override string ToString()
        {
            var numberFormatInfo = NumberFormatInfo.GetInstance(CultureInfo.CurrentCulture);
            numberFormatInfo = (NumberFormatInfo)numberFormatInfo.Clone();

            numberFormatInfo.CurrencySymbol = Currency.ToString();

            return Amount.ToString("C", numberFormatInfo);
        }
    }
}

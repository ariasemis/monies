using FsCheck.Xunit;
using Monies.Tests.Attributes;
using System;
using System.Globalization;
using Xunit;

namespace Monies.Tests
{
    [MoneyProperties(QuietOnSuccess = true)]
    public class DenseFormatTests
    {
        [Property]
        public void Default_format_is_equal_to_G_format(Dense<string> x)
        {
            var expected = x.ToString("G");

            Assert.Equal(expected, x.ToString());
            Assert.Equal(expected, x.ToString((string)null));
            Assert.Equal(expected, x.ToString(string.Empty));
            Assert.Equal(expected, x.ToString((IFormatProvider)null));
            Assert.Equal(expected, x.ToString(null, null));
            Assert.Equal(expected, x.ToString(string.Empty, null));
            Assert.Equal(expected, x.ToString(null, CultureInfo.CurrentCulture));
        }

        [Property]
        public void Format_string_is_case_insensitive(Dense<string> x)
        {
            var formats = new[] { "G", "C" };

            foreach (var format in formats)
            {
                Assert.Equal(x.ToString(format), x.ToString(format.ToUpperInvariant()));
            }
        }

        [Fact]
        public void Cannot_convert_if_unknown_format()
        {
            var money = Money.Dense(100, "$");

            Assert.Throws<FormatException>(() => money.ToString("?"));
        }

        [Theory]
        [MemberData(nameof(FormatStringData))]
        public void Converting_to_string_uses_format_string(Dense<string> money, string format, string expected)
        {
            Assert.Equal(expected, money.ToString(format, CultureInfo.InvariantCulture));
        }

        [Theory]
        [MemberData(nameof(FormatProviderData))]
        public void Converting_to_string_uses_format_provider(Dense<string> money, IFormatProvider provider, string expected)
        {
            Assert.Equal(expected, money.ToString(provider));
            Assert.Equal(expected, money.ToString(null, provider));
        }

        [Theory]
        [MemberData(nameof(FormatCurrencyData))]
        public void Converting_to_string_uses_currency_formatting(Dense<CurrencyISO> money, IFormatProvider provider, string expected)
        {
            Assert.Equal(expected, money.ToString(null, provider));
        }

        public static TheoryData<Dense<string>, string, string> FormatStringData
            => new TheoryData<Dense<string>, string, string>
            {
                { Money.Dense(123, "XTS"), "G", "XTS123.00" },
                { Money.Dense(123, "XTS"), "C", "XTS123.00" },
                { Money.Dense(123.4567m, "XTS"), "C4", "XTS123.4567" },
                { Money.Dense(123.4567m, "XTS"), "C3", "XTS123.457" },
            };

        public static TheoryData<Dense<string>, IFormatProvider, string> FormatProviderData
            => new TheoryData<Dense<string>, IFormatProvider, string>
            {
                { Money.Dense(1043.17m, "$"), CultureInfo.CreateSpecificCulture("en-US"), "$1,043.17" },
                { Money.Dense(1043.17m, "€"), CultureInfo.CreateSpecificCulture("fr-FR"), "1 043,17 €" },
                { Money.Dense(1043.17m, "$"), CultureInfo.CreateSpecificCulture("es-AR").NumberFormat, "$ 1.043,17" },
                { Money.Dense(1043.17m, "€"), CultureInfo.CreateSpecificCulture("de-DE").NumberFormat, "1.043,17 €" },
                { Money.Dense(1043.17m, "¤"), CultureInfo.InvariantCulture, "¤1,043.17" },
                { Money.Dense(1043.17m, "USD"), CultureInfo.CreateSpecificCulture("en-US"), "USD1,043.17" },
            };

        public static TheoryData<Dense<CurrencyISO>, IFormatProvider, string> FormatCurrencyData
            => new TheoryData<Dense<CurrencyISO>, IFormatProvider, string>
            {
                { Money.Dense(1043.17m, CurrencyISO.Instance), CultureInfo.CreateSpecificCulture("en-US"), "USD1,043.17" },
                { Money.Dense(1043.17m, CurrencyISO.Instance), CultureInfo.CreateSpecificCulture("fr-FR"), "1 043,17 EUR" },
                { Money.Dense(1043.17m, CurrencyISO.Instance), CultureInfo.CreateSpecificCulture("es-AR"), "ARS 1.043,17" },
            };

        public class CurrencyISO : IFormattable, IEquatable<CurrencyISO>
        {
            private CurrencyISO() { }

            public static CurrencyISO Instance { get; } = new CurrencyISO();

            public bool Equals(CurrencyISO other)
                => true;

            public override bool Equals(object obj)
                => Equals(obj as CurrencyISO);

            public override int GetHashCode() => 1;

            public string ToString(string format, IFormatProvider formatProvider)
            {
                var ci = formatProvider as CultureInfo ?? CultureInfo.CurrentCulture;
                var ri = new RegionInfo(ci.Name);

                return ri.ISOCurrencySymbol;
            }

            public override string ToString()
                => ToString(null, null);
        }
    }
}

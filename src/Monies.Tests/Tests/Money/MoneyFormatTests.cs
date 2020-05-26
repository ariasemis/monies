using FsCheck.Xunit;
using Monies.Tests.Attributes;
using System;
using System.Globalization;
using Xunit;

namespace Monies.Tests
{
    [MoneyProperties(QuietOnSuccess = true)]
    public class MoneyFormatTests
    {
        [Property]
        public void Default_format_is_equal_to_G_format(Money<string> x)
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
        public void Format_string_is_case_insensitive(Money<string> x)
        {
            var formats = new[] { "G", "C" };

            foreach (var format in formats)
            {
                Assert.Equal(x.ToString(format), x.ToString(format.ToLowerInvariant()));
            }
        }

        [Fact]
        public void Cannot_convert_if_unknown_format()
        {
            var money = Money.Create(100, "$");

            Assert.Throws<FormatException>(() => money.ToString("?"));
        }

        [Theory]
        [MemberData(nameof(FormatStringData))]
        public void Converting_to_string_uses_format_string(Money<string> money, string format, string expected)
        {
            Assert.Equal(expected, money.ToString(format, CultureInfo.InvariantCulture));
        }

        [Theory]
        [MemberData(nameof(FormatProviderData))]
        public void Converting_to_string_uses_format_provider(Money<string> money, IFormatProvider provider, string expected)
        {
            Assert.Equal(expected, money.ToString(provider));
            Assert.Equal(expected, money.ToString(null, provider));
        }

        public static TheoryData<Money<string>, string, string> FormatStringData
            => new TheoryData<Money<string>, string, string>
            {
                { Money.Create(123, "XTS"), "G", "XTS123.00" },
                { Money.Create(123, "XTS"), "C", "XTS123.00" },
                { Money.Create(123.4567m, "XTS"), "C4", "XTS123.4567" },
                { Money.Create(123.4567m, "XTS"), "C3", "XTS123.457" },
            };

        public static TheoryData<Money<string>, IFormatProvider, string> FormatProviderData
            => new TheoryData<Money<string>, IFormatProvider, string>
            {
                { Money.Create(1043.17m, "$"), CultureInfo.CreateSpecificCulture("en-US"), "$1,043.17" },
                { Money.Create(1043.17m, "€"), CultureInfo.CreateSpecificCulture("fr-FR"), "1 043,17 €" },
                { Money.Create(1043.17m, "$"), CultureInfo.CreateSpecificCulture("es-AR").NumberFormat, "$ 1.043,17" },
                { Money.Create(1043.17m, "€"), CultureInfo.CreateSpecificCulture("de-DE").NumberFormat, "1.043,17 €" },
                { Money.Create(1043.17m, "¤"), CultureInfo.InvariantCulture, "¤1,043.17" },
                { Money.Create(1043.17m, "USD"), CultureInfo.CreateSpecificCulture("en-US"), "USD1,043.17" },
            };
    }
}

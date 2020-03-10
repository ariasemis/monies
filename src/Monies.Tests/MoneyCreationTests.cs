using System;
using System.Collections.Generic;
using Xunit;

namespace Monies.Tests
{
    public class MoneyCreationTests
    {
        [Fact]
        public void Try_to_create_money_without_currency_fails()
        {
            Assert.Throws<ArgumentNullException>(() => new Money<object>(100, null));
        }

        [Theory]
        [MemberData(nameof(ValidCases))]
        public void Created_money_has_expected_values(decimal amount, string currency)
        {
            var actual = new Money<string>(amount, currency);

            Assert.NotNull(actual);
            Assert.Equal(amount, actual.Amount);
            Assert.Equal(currency, actual.Currency);
        }

        public static IEnumerable<object[]> ValidCases => new[]
        {
            new object[] { 100, "$" },
            new object[] { 3.1415m, "USD" },
            new object[] { decimal.MaxValue, string.Empty },
            new object[] { decimal.Zero, "EUR" },
            new object[] { -42, "€" },
            new object[] { -765.43m, "XAU" },
            new object[] { decimal.MinValue, "cent" },
        };
    }
}

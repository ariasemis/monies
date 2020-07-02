using System;
using Xunit;

namespace Monies.Tests
{
    public class DenseCreationTests
    {
        [Fact]
        public void Cannot_create_money_without_currency()
        {
            Assert.Throws<ArgumentNullException>(() => Money.Dense<string>(100, null));
        }

        [Theory]
        [MemberData(nameof(ValidCases))]
        public void Created_money_has_expected_values(decimal amount, string currency)
        {
            var actual = Money.Dense(amount, currency);

            Assert.NotNull(actual);
            Assert.Equal(amount, actual.Amount);
            Assert.Equal(currency, actual.Currency);
        }

        public static TheoryData<decimal, string> ValidCases => new TheoryData<decimal, string>
        {
            { 100, "$" },
            { 3.1415m, "USD" },
            { decimal.MaxValue, string.Empty },
            { decimal.Zero, "EUR" },
            { -42, "€" },
            { -765.43m, "XAU" },
            { decimal.MinValue, "cent" },
        };
    }
}

using System;
using Xunit;

namespace Monies.Tests
{
    public class DiscreteCreationTests
    {
        [Fact]
        public void Cannot_create_money_without_currency()
        {
            Assert.Throws<ArgumentNullException>(() => Money.Discrete<string, string>(100, null, "euros"));
        }

        [Fact]
        public void Cannot_create_money_without_unit()
        {
            Assert.Throws<ArgumentNullException>(() => Money.Discrete<string, string>(100, "EUR", null));
        }

        [Theory]
        [MemberData(nameof(ValidCases))]
        public void Created_money_has_expected_values(long amount, string currency, string unit)
        {
            var actual = Money.Discrete(amount, currency, unit);

            Assert.NotNull(actual);
            Assert.Equal(amount, actual.Amount);
            Assert.Equal(currency, actual.Currency);
            Assert.Equal(unit, actual.Unit);
        }

        public static TheoryData<long, string, string> ValidCases => new TheoryData<long, string, string>
        {
            { 100, "EUR", "euros" },
            { long.MaxValue, "EUR", "cents" },
            { 0, string.Empty, string.Empty },
            { -100, "USD", "dollars" },
            { long.MinValue, "ARS", "pesos" },
        };
    }
}

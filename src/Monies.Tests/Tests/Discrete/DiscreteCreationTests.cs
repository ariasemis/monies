using System;
using Xunit;

namespace Monies.Tests
{
    public class DiscreteCreationTests
    {
        [Fact]
        public void Cannot_create_money_without_currency()
        {
            Assert.Throws<ArgumentNullException>(() => Money.Discrete(100, null, Money.Unit(1, "USD")));
        }

        [Fact]
        public void Cannot_create_money_without_unit()
        {
            Assert.Throws<ArgumentNullException>(() => Money.Discrete(100, "EUR", null));
        }

        [Fact]
        public void Cannot_create_money_when_currency_mismatch()
        {
            Assert.Throws<ArgumentException>(() => Money.Discrete(100, "EUR", Money.Unit(1, "USD")));
        }

        [Theory]
        [MemberData(nameof(ValidCases))]
        public void Created_money_has_expected_values(long amount, string currency, Unit<string> unit)
        {
            var actual = Money.Discrete(amount, currency, unit);

            Assert.NotNull(actual);
            Assert.Equal(amount, actual.Amount);
            Assert.Equal(currency, actual.Currency);
            Assert.Equal(unit, actual.Unit);
        }

        [Theory]
        [MemberData(nameof(ValidCases))]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "xUnit1026:Theory methods should use all of their parameters", Justification = "Reusing the set of test cases")]
        public void Created_money_with_default_unit_has_expected_values(long amount, string currency, Unit<string> _)
        {
            var unit = Money.Unit(1, currency);
            var actual = Money.Discrete(amount, currency);

            Assert.NotNull(actual);
            Assert.Equal(amount, actual.Amount);
            Assert.Equal(currency, actual.Currency);
            Assert.Equal(unit.Scale, actual.Unit.Scale);
        }

        [Theory]
        [MemberData(nameof(ValidCases))]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Usage", "xUnit1026:Theory methods should use all of their parameters", Justification = "Reusing the set of test cases")]
        public void Created_money_with_default_currency_has_expected_values(long amount, string _, Unit<string> unit)
        {
            var expected = unit.Currency;
            var actual = Money.Discrete(amount, unit);

            Assert.NotNull(actual);
            Assert.Equal(amount, actual.Amount);
            Assert.Equal(expected, actual.Currency);
            Assert.Equal(unit.Scale, actual.Unit.Scale);
        }

        public static TheoryData<long, string, Unit<string>> ValidCases => new TheoryData<long, string, Unit<string>>
        {
            { 100, "EUR", Money.Unit(1, "EUR") },
            { long.MaxValue, "EUR", Money.Unit(100, "EUR") },
            { 0, string.Empty, Money.Unit(1, string.Empty) },
            { -100, "USD", Money.Unit(1, "USD") },
            { long.MinValue, "ARS", Money.Unit(1, "ARS") },
        };
    }
}

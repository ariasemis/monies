using FsCheck;
using FsCheck.Xunit;
using Monies.Tests.Attributes;
using Monies.Tests.Generators;
using System;
using Xunit;

namespace Monies.Tests
{
    [MoneyProperties(QuietOnSuccess = true)]
    public abstract class DiscreteComparisonTests<T> where T : IEquatable<T>
    {
        [Property(Arbitrary = new[] { typeof(NullDiscreteArbitrary) })]
        public void Money_is_equal_to_itself(Discrete<T> x)
        {
            var y = x;

            Assert.True(Equals(x, y));
            Assert.True(x == y);
            Assert.False(x != y);

            if (x != null)
            {
                Assert.True(x.Equals(y));
                Assert.True(x.Equals((object)y));
            }
        }

        [Property]
        public void If_x_equals_y_then_y_equals_x(Discrete<T> x, Discrete<T> y)
        {
            Assert.True(x.Equals(y) == y.Equals(x));
            Assert.True(x.Equals((object)y) == y.Equals((object)x));
            Assert.True((x == y) == (y == x));
            Assert.True((x != y) == (y != x));
        }

        [Property]
        public void If_x_equals_y_and_y_equals_z_then_x_equals_z(Discrete<T> x, Discrete<T> y, Discrete<T> z)
        {
            if (x.Equals(y) && y.Equals(z))
            {
                Assert.True(x.Equals(z));
                Assert.True(x.Equals((object)z));
                Assert.True(x == z);
                Assert.False(x != z);
            }
        }

        [Property]
        public void Monies_with_same_amount_currency_and_unit_are_equal(Discrete<T> x)
        {
            var y = Money.Discrete(x.Amount, x.Currency, x.Unit);

            Assert.True(x.Equals(y));
            Assert.True(x.Equals((object)y));
            Assert.True(x == y);
            Assert.False(x != y);

            Assert.Equal(x.GetHashCode(), y.GetHashCode());
        }

        [Property]
        public void Money_is_not_equal_to_null(Discrete<T> x)
        {
            Assert.False(x.Equals(null));
            Assert.False(x.Equals((object)null));
            Assert.False(x == null);
            Assert.True(x != null);
        }

        [Property]
        public void Money_is_not_equal_to_another_object(Discrete<T> x)
        {
            var y = new { x.Amount, x.Currency, x.Unit };

            Assert.False(x.Equals(y));
        }
    }

    public sealed class DiscreteIntCurrencyComparisonTests : DiscreteComparisonTests<int> { }
    public sealed class DiscreteGuidCurrencyComparisonTests : DiscreteComparisonTests<Guid> { }

    public sealed class DiscreteStringCurrencyComparisonTests : DiscreteComparisonTests<string>
    {
        public static TheoryData<Discrete<string>, Discrete<string>> DifferentCurrencies => new TheoryData<Discrete<string>, Discrete<string>>
        {
            { Money.Discrete(100, "$"), Money.Discrete(100, "€") },
            { Money.Discrete(40, "USD"), Money.Discrete(40, "EUR") },
            { Money.Discrete(0, "840"), Money.Discrete(0, "978") },
            { Money.Discrete(-1, ""), Money.Discrete(-1, "XXX") },
        };

        public static TheoryData<Discrete<string>, Discrete<string>> GreaterAmount => new TheoryData<Discrete<string>, Discrete<string>>
        {
            { Money.Discrete(2, "$"), Money.Discrete(1, "$") },
            { Money.Discrete(100, ""), Money.Discrete(-100, "") },
            { Money.Discrete(0, "USD"), Money.Discrete(-1, "USD") },
            { Money.Discrete(-40, "978"), Money.Discrete(-41, "978") },
        };

        public static TheoryData<Discrete<string>, Discrete<string>> SmallerAmount => new TheoryData<Discrete<string>, Discrete<string>>
        {
            { Money.Discrete(33, "XBT"), Money.Discrete(31, "XBT") },
            { Money.Discrete(100, ""), Money.Discrete(200, "") },
            { Money.Discrete(-5, "$"), Money.Discrete(5, "$") },
            { Money.Discrete(-10, "840"), Money.Discrete(-9, "840")  },
        };

        public static TheoryData<Discrete<string>, Discrete<string>> EquivalentAmount => new TheoryData<Discrete<string>, Discrete<string>>
        {
            { Money.Discrete(1, Money.Unit(1, "XBT")), Money.Discrete(100, Money.Unit(100, "XBT")) },
            { Money.Discrete(200, Money.Unit(4, "XBT")), Money.Discrete(400, Money.Unit(8, "XBT")) },
            { Money.Discrete(0, Money.Unit(2, string.Empty)), Money.Discrete(0, Money.Unit(3, string.Empty)) },
            { Money.Discrete(-50, Money.Unit(1, "$")), Money.Discrete(-5000, Money.Unit(100, "$")) },
        };

        public static TheoryData<Discrete<string>, Discrete<string>> DifferentUnits => new TheoryData<Discrete<string>, Discrete<string>>
        {
            { Money.Discrete(100, Money.Unit(1, "$")), Money.Discrete(100, Money.Unit(100, "$")) },
            { Money.Discrete(40, Money.Unit(2, "USD")), Money.Discrete(40, Money.Unit(3, "USD")) },
            { Money.Discrete(-1, Money.Unit(1001, "")), Money.Discrete(-1, Money.Unit(1000, "")) },
        };

        [Theory]
        [MemberData(nameof(DifferentCurrencies))]
        public void Monies_with_same_amount_but_different_currency_are_not_equal(Discrete<string> x, Discrete<string> y)
        {
            Assert.False(x.Equals(y));
            Assert.False(x.Equals((object)y));
            Assert.False(x == y);
            Assert.True(x != y);
        }

        [Theory]
        [MemberData(nameof(GreaterAmount))]
        [MemberData(nameof(SmallerAmount))]
        public void Monies_with_different_amount_but_same_currency_are_not_equal(Discrete<string> x, Discrete<string> y)
        {
            Assert.False(x.Equals(y));
            Assert.False(x.Equals((object)y));
            Assert.False(x == y);
            Assert.True(x != y);
        }

        [Theory]
        [MemberData(nameof(EquivalentAmount))]
        public void Monies_with_equivalent_amounts_and_same_currency_are_equal(Discrete<string> x, Discrete<string> y)
        {
            Assert.True(x.Equals(y));
            Assert.True(x.Equals((object)y));
            Assert.True(x == y);
            Assert.False(x != y);
        }

        [Theory]
        [MemberData(nameof(DifferentUnits))]
        public void Monies_with_different_units_are_not_equal(Discrete<string> x, Discrete<string> y)
        {
            Assert.False(x.Equals(y));
            Assert.False(x.Equals((object)y));
            Assert.False(x == y);
            Assert.True(x != y);
        }

    }
}

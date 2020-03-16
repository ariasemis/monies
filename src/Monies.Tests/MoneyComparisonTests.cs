using FsCheck;
using FsCheck.Xunit;
using Monies.Tests.Generators;
using System;
using Xunit;

namespace Monies.Tests
{
    [Properties(Arbitrary = new[] { typeof(MoneyArbitrary), typeof(SameCurrencyArbitrary) }, QuietOnSuccess = true)]
    public abstract class MoneyComparisonTests<T> where T : IEquatable<T>
    {
        [Property(Arbitrary = new[] { typeof(NullMoneyArbitrary) })]
        public void Money_is_equal_to_itself(Money<T> x)
        {
            var y = x;

            Assert.True(Equals(x, y));
            Assert.True(x == y);
            Assert.False(x != y);
            Assert.True(x >= y);
            Assert.True(x <= y);

            if (x != null)
            {
                Assert.True(x.Equals(y));
                Assert.True(x.Equals((object)y));
                Assert.Equal(0, x.CompareTo(y));
                Assert.Equal(0, x.CompareTo((object)y));
            }
        }

        [Property]
        public void If_x_equals_y_then_y_equals_x(Money<T> x, Money<T> y)
        {
            Assert.True(x.Equals(y) == y.Equals(x));
            Assert.True(x.Equals((object)y) == y.Equals((object)x));
            Assert.True((x == y) == (y == x));
            Assert.True((x != y) == (y != x));
        }

        [Property]
        public void If_x_equals_y_and_y_equals_z_then_x_equals_z(Money<T> x, Money<T> y, Money<T> z)
        {
            if (x.Equals(y) && y.Equals(z))
            {
                Assert.True(x.Equals(z));
                Assert.True(x.Equals((object)z));
                Assert.True(x == z);
                Assert.False(x != z);
                Assert.Equal(0, x.CompareTo(z));
                Assert.Equal(0, x.CompareTo((object)z));
                Assert.True(x <= z);
                Assert.True(x >= z);
            }
        }

        [Property]
        public void Monies_with_same_amount_and_same_currency_are_equal(Money<T> x)
        {
            var y = Money.Create(x.Amount, x.Currency);

            Assert.True(x.Equals(y));
            Assert.True(x.Equals((object)y));
            Assert.True(x == y);
            Assert.False(x != y);
            Assert.Equal(0, x.CompareTo(y));
            Assert.Equal(0, x.CompareTo((object)y));
            Assert.True(x >= y);
            Assert.True(x <= y);

            Assert.Equal(x.GetHashCode(), y.GetHashCode());
        }

        [Property]
        public void Money_is_not_equal_to_null(Money<T> x)
        {
            Assert.False(x.Equals(null));
            Assert.False(x.Equals((object)null));
            Assert.False(x == null);
            Assert.True(x != null);
            Assert.NotEqual(0, x.CompareTo(null));
            Assert.NotEqual(0, x.CompareTo((object)null));
        }

        [Property]
        public void Money_is_not_equal_to_another_object(Money<T> x)
        {
            var y = new { x.Amount, x.Currency };

            Assert.False(x.Equals(y));
        }

        [Property]
        public void At_least_one_money_must_be_lt_or_eq_the_other(SameCurrency<T> monies)
        {
            var (x, y) = monies;

            Assert.True(x.CompareTo(y) <= 0 || y.CompareTo(x) <= 0);
            Assert.True(x.CompareTo((object)y) <= 0 || y.CompareTo((object)x) <= 0);
            Assert.True(x <= y || y <= x);
            Assert.True(x >= y || y >= x);
        }

        [Property]
        public void If_both_are_lte_then_they_are_equal(SameCurrency<T> monies)
        {
            var (x, y) = monies;

            if (x <= y && y <= x)
            {
                Assert.True(x.Equals(y));
                Assert.True(x.Equals((object)y));
                Assert.True(x == y);
                Assert.False(x != y);
                Assert.Equal(0, x.CompareTo(y));
                Assert.Equal(0, x.CompareTo((object)y));
            }
        }

        [Property]
        public void If_not_equal_then_x_to_y_is_opposite_than_y_to_x(SameCurrency<T> monies)
        {
            var (x, y) = monies;

            var r = x.CompareTo(y);
            if (r != 0)
            {
                var sign = Math.Sign(r) * -1;

                Assert.Equal(sign, Math.Sign(y.CompareTo(x)));
                Assert.Equal(sign, Math.Sign(y.CompareTo((object)x)));
                Assert.True(x < y != y < x);
                Assert.True(x <= y != y <= x);
                Assert.True(x > y != y > x);
                Assert.True(x >= y != y >= x);
            }
        }

        [Property]
        public void If_x_lte_y_and_y_lte_z_then_x_lte_z(SameCurrency<T> monies)
        {
            var (x, y, z) = monies;

            if (x <= y && y <= z)
            {
                Assert.True(x <= z);
                Assert.True(x.CompareTo(z) <= 0);
                Assert.True(x.CompareTo((object)z) <= 0);
            }
        }

        [Property]
        public void Money_is_greater_than_null(Money<T> x)
        {
            Assert.True(x.CompareTo(null) > 0);
            Assert.True(x.CompareTo((object)null) > 0);
            Assert.True(x > null);
            Assert.True(x >= null);
            Assert.False(x < null);
            Assert.False(x <= null);
        }

        [Property]
        public void Cannot_compare_money_with_another_object(Money<T> x)
        {
            var y = new { Amount = 1, Currency = "$" };

            Assert.Throws<ArgumentException>(() => x.CompareTo(y));
        }
    }

    public sealed class MoneyIntCurrencyComparisonTests : MoneyComparisonTests<int> { }
    public sealed class MoneyGuidCurrencyComparisonTests : MoneyComparisonTests<Guid> { }
    public sealed class MoneyFakeCurrencyComparisonTests : MoneyComparisonTests<FakeCurrency> { }

    public sealed class MoneyStringCurrencyComparisonTests : MoneyComparisonTests<string>
    {
        public static TheoryData<Money<string>, Money<string>> DifferentCurrencies => new TheoryData<Money<string>, Money<string>>
        {
            { Money.Create(100, "$"), Money.Create(100, "€") },
            { Money.Create(40.2m, "USD"), Money.Create(40.2m, "EUR") },
            { Money.Create(0, "840"), Money.Create(0, "978") },
            { Money.Create(-1, ""), Money.Create(-1, "XXX") },
        };

        public static TheoryData<Money<string>, Money<string>> GreaterAmount => new TheoryData<Money<string>, Money<string>>
        {
            { Money.Create(1m, "$"), Money.Create(0.99m, "$") },
            { Money.Create(100, ""), Money.Create(-100, "") },
            { Money.Create(0, "USD"), Money.Create(-1, "USD") },
            { Money.Create(-40, "978"), Money.Create(-40.01m, "978") },
        };

        public static TheoryData<Money<string>, Money<string>> SmallerAmount => new TheoryData<Money<string>, Money<string>>
        {
            { Money.Create(0.00000033m, "XBT"), Money.Create(0.000000331m, "XBT") },
            { Money.Create(100, ""), Money.Create(200, "") },
            { Money.Create(-0.5m, "$"), Money.Create(0.5m, "$") },
            { Money.Create(-10m, "840"), Money.Create(-9m, "840")  },
        };

        [Theory]
        [MemberData(nameof(DifferentCurrencies))]
        public void Monies_with_same_amount_but_different_currency_are_not_equal(Money<string> x, Money<string> y)
        {
            Assert.False(x.Equals(y));
            Assert.False(x.Equals((object)y));
            Assert.False(x == y);
            Assert.True(x != y);
        }

        [Theory]
        [MemberData(nameof(GreaterAmount))]
        [MemberData(nameof(SmallerAmount))]
        public void Monies_with_different_amount_but_same_currency_are_not_equal(Money<string> x, Money<string> y)
        {
            Assert.False(x.Equals(y));
            Assert.False(x.Equals((object)y));
            Assert.False(x == y);
            Assert.True(x != y);
            Assert.NotEqual(0, x.CompareTo(y));
            Assert.NotEqual(0, x.CompareTo((object)y));
        }

        [Theory]
        [MemberData(nameof(GreaterAmount))]
        public void Money_is_greater_if_amount_is_greater(Money<string> x, Money<string> y)
        {
            Assert.True(x.CompareTo(y) > 0);
            Assert.True(x.CompareTo((object)y) > 0);
            Assert.True(x > y);
            Assert.True(x >= y);
            Assert.False(x < y);
            Assert.False(x <= y);
        }

        [Theory]
        [MemberData(nameof(SmallerAmount))]
        public void Money_is_smaller_if_amount_is_smaller(Money<string> x, Money<string> y)
        {
            Assert.True(x.CompareTo(y) < 0);
            Assert.True(x.CompareTo((object)y) < 0);
            Assert.True(x < y);
            Assert.True(x <= y);
            Assert.False(x > y);
            Assert.False(x >= y);
        }

        [Theory]
        [MemberData(nameof(DifferentCurrencies))]
        public void Cannot_compare_money_with_different_currencies<T>(Money<T> m1, Money<T> m2) where T : IEquatable<T>
        {
            Assert.Throws<InvalidOperationException>(() => m1.CompareTo(m2));
            Assert.Throws<InvalidOperationException>(() => m1.CompareTo((object)m2));
            Assert.Throws<InvalidOperationException>(() => m1 < m2);
            Assert.Throws<InvalidOperationException>(() => m1 > m2);
            Assert.Throws<InvalidOperationException>(() => m1 <= m2);
            Assert.Throws<InvalidOperationException>(() => m1 >= m2);
        }
    }

    public class FakeCurrency : IEquatable<FakeCurrency>
    {
        public bool Equals(FakeCurrency other) => true;
    }
}

using System;
using System.Collections.Generic;
using Xunit;

namespace Monies.Tests
{
    public class MoneyComparisonTests
    {
        public static IEnumerable<object[]> OneMoney => new[]
        {
             new object[] { Money.Create(10, "$") },
             new object[] { Money.Create(0, "") },
             new object[] { Money.Create(-10, new FakeCurrency()) },
        };

        public static IEnumerable<object[]> SameMoney => new[]
        {
            new object[] { Money.Create(0.25m, "$"), Money.Create(0.25m, "$") },
            new object[] { Money.Create(100, new FakeCurrency()), Money.Create(100, new FakeCurrency()) },
            new object[] { Money.Create(-15, 840), Money.Create(-15, 840) },
        };

        public static IEnumerable<object[]> DifferentCurrency => new[]
        {
            new object[] { Money.Create(100, "$"), Money.Create(100, "€") },
            new object[] { Money.Create(40.2m, "USD"), Money.Create(40.2m, "EUR") },
            new object[] { Money.Create(0, 840), Money.Create(0, 978) },
            new object[] { Money.Create(-1, ""), Money.Create(-1, "XXX") },
        };

        public static IEnumerable<object[]> GreaterAmount => new[]
        {
            new object[] { Money.Create(1m, "$"), Money.Create(0.99m, "$") },
            new object[] { Money.Create(100, new FakeCurrency()), Money.Create(-100, new FakeCurrency()) },
            new object[] { Money.Create(0, "USD"), Money.Create(-1, "USD") },
            new object[] { Money.Create(-40, 978), Money.Create(-40.01m, 978) },
        };

        public static IEnumerable<object[]> SmallerAmount => new[]
        {
            new object[] { Money.Create(0.00000033m, "XBT"), Money.Create(0.000000331m, "XBT") },
            new object[] { Money.Create(100, new FakeCurrency()), Money.Create(200, new FakeCurrency()) },
            new object[] { Money.Create(-0.5m, "$"), Money.Create(0.5m, "$") },
            new object[] { Money.Create(-10m, 840), Money.Create(-9m, 840) },
        };

        [Theory]
        [MemberData(nameof(OneMoney))]
        public void Money_is_equal_to_itself<T>(Money<T> m1) where T : IEquatable<T>
        {
            var m2 = m1;

            Assert.Same(m1, m2);
            Assert.True(m1.Equals(m2));
            Assert.True(m1.Equals((object)m2));
            Assert.True(m1 == m2);
            Assert.False(m1 != m2);
            Assert.Equal(0, m1.CompareTo(m2));
            Assert.Equal(0, m1.CompareTo((object)m2));
            Assert.True(m1 >= m2);
            Assert.True(m1 <= m2);
        }

        [Theory]
        [MemberData(nameof(SameMoney))]
        public void Monies_with_same_amount_and_same_currency_are_equal<T>(Money<T> m1, Money<T> m2) where T : IEquatable<T>
        {
            Assert.True(m1.Equals(m2));
            Assert.True(m1.Equals((object)m2));
            Assert.True(m1 == m2);
            Assert.False(m1 != m2);
            Assert.Equal(0, m1.CompareTo(m2));
            Assert.Equal(0, m1.CompareTo((object)m2));
            Assert.True(m1 >= m2);
            Assert.True(m1 <= m2);

            // because GetHashCode() depends on it being implemented on the currency type,
            // it will only be equal if it is also equal for the currencies' hashcodes
            var sameHash = m1.Currency.GetHashCode() == m2.Currency.GetHashCode();
            if (sameHash)
            {
                Assert.Equal(m1.GetHashCode(), m2.GetHashCode());
            }
        }

        [Theory]
        [MemberData(nameof(DifferentCurrency))]
        public void Monies_with_same_amount_but_different_currency_are_not_equal<T>(Money<T> m1, Money<T> m2) where T : IEquatable<T>
        {
            Assert.False(m1.Equals(m2));
            Assert.False(m1.Equals((object)m2));
            Assert.False(m1 == m2);
            Assert.True(m1 != m2);
        }

        [Theory]
        [MemberData(nameof(GreaterAmount))]
        [MemberData(nameof(SmallerAmount))]
        public void Monies_with_different_amount_but_same_currency_are_not_equal<T>(Money<T> m1, Money<T> m2) where T : IEquatable<T>
        {
            Assert.False(m1.Equals(m2));
            Assert.False(m1.Equals((object)m2));
            Assert.False(m1 == m2);
            Assert.True(m1 != m2);
            Assert.NotEqual(0, m1.CompareTo(m2));
            Assert.NotEqual(0, m1.CompareTo((object)m2));
        }

        [Theory]
        [MemberData(nameof(OneMoney))]
        public void Money_is_not_equal_to_null<T>(Money<T> m) where T : IEquatable<T>
        {
            Assert.False(m.Equals(null));
            Assert.False(m.Equals((object)null));
            Assert.False(m == null);
            Assert.True(m != null);
            Assert.NotEqual(0, m.CompareTo(null));
            Assert.NotEqual(0, m.CompareTo((object)null));
        }

        [Theory]
        [MemberData(nameof(OneMoney))]
        public void Money_is_not_equal_to_another_object<T>(Money<T> m) where T : IEquatable<T>
        {
            var o = new { Amount = 1, Currency = "$" };

            Assert.False(m.Equals(o));
        }

        [Fact]
        public void Null_monies_are_equal()
        {
            Money<string> m1 = null;
            Money<string> m2 = null;

            Assert.True(Equals(m1, m2));
            Assert.True(m1 == m2);
            Assert.False(m1 != m2);
            Assert.True(m1 >= m2);
            Assert.True(m1 <= m2);
        }

        [Theory]
        [MemberData(nameof(OneMoney))]
        public void Money_is_greater_than_null<T>(Money<T> m) where T : IEquatable<T>
        {
            Assert.True(m.CompareTo(null) > 0);
            Assert.True(m.CompareTo((object)null) > 0);
            Assert.True(m > null);
            Assert.True(m >= null);
            Assert.False(m < null);
            Assert.False(m <= null);
        }

        [Theory]
        [MemberData(nameof(GreaterAmount))]
        public void Money_is_greater_if_amount_is_greater<T>(Money<T> m1, Money<T> m2) where T : IEquatable<T>
        {
            Assert.True(m1.CompareTo(m2) > 0);
            Assert.True(m1.CompareTo((object)m2) > 0);
            Assert.True(m1 > m2);
            Assert.True(m1 >= m2);
            Assert.False(m1 < m2);
            Assert.False(m1 <= m2);
        }

        [Theory]
        [MemberData(nameof(SmallerAmount))]
        public void Money_is_smaller_if_amount_is_smaller<T>(Money<T> m1, Money<T> m2) where T : IEquatable<T>
        {
            Assert.True(m1.CompareTo(m2) < 0);
            Assert.True(m1.CompareTo((object)m2) < 0);
            Assert.True(m1 < m2);
            Assert.True(m1 <= m2);
            Assert.False(m1 > m2);
            Assert.False(m1 >= m2);
        }

        [Theory]
        [MemberData(nameof(OneMoney))]
        public void Cannot_compare_money_with_another_object<T>(Money<T> m) where T : IEquatable<T>
        {
            var o = new { Amount = 1, Currency = "$" };

            Assert.Throws<ArgumentException>(() => m.CompareTo(o));
        }

        [Theory]
        [MemberData(nameof(DifferentCurrency))]
        public void Cannot_compare_money_with_different_currencies<T>(Money<T> m1, Money<T> m2) where T : IEquatable<T>
        {
            Assert.Throws<InvalidOperationException>(() => m1.CompareTo(m2));
            Assert.Throws<InvalidOperationException>(() => m1.CompareTo((object)m2));
            Assert.Throws<InvalidOperationException>(() => m1 < m2);
            Assert.Throws<InvalidOperationException>(() => m1 > m2);
            Assert.Throws<InvalidOperationException>(() => m1 <= m2);
            Assert.Throws<InvalidOperationException>(() => m1 >= m2);
        }

        class FakeCurrency : IEquatable<FakeCurrency>
        {
            public bool Equals(FakeCurrency other) => true;
        }
    }
}

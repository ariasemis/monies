using FsCheck.Xunit;
using Monies.Tests.Attributes;
using Monies.Tests.Generators;
using System;
using Xunit;

namespace Monies.Tests
{
    [MoneyProperties(QuietOnSuccess = true)]
    public class DenseAdditionTests
    {
        [Property]
        public void Adding_x_to_y_is_the_same_as_adding_y_to_x(SameCurrencyDense<string> monies)
        {
            var (x, y) = monies;

            Assert.Equal(x + y, y + x);
            Assert.Equal(x.Add(y), y.Add(x));
        }

        [Property]
        public void Adding_3_monies_in_any_order_returns_the_same_result(SameCurrencyDense<string> monies)
        {
            var (x, y, z) = monies;

            Assert.Equal(x + y + z, x + (y + z));
            Assert.Equal(x.Add(y).Add(z), x.Add(y.Add(z)));
        }

        [Property]
        public void Adding_zero_to_a_money_returns_same_money(Dense<string> x)
        {
            var zero = Money.Dense(0, x.Currency);

            Assert.Equal(x, x + zero);
            Assert.Equal(x, zero + x);
            Assert.Equal(x, x.Add(zero));
            Assert.Equal(x, zero.Add(x));
        }

        [Property]
        public void Adding_the_opposite_returns_zero_money(Dense<string> x)
        {
            var zero = Money.Dense(0, x.Currency);

            Assert.Equal(zero, x + (-x));
            Assert.Equal(zero, x.Add(x.Negate()));
        }

        [Property]
        public void Adding_null_to_a_money_returns_null(Dense<string> x)
        {
            Assert.Null(x.Add(null));
            Assert.Null(x + null);
            Assert.Null(null + x);
        }

        [Theory]
        [InlineData(100, "$", 200, "€")]
        [InlineData(0, "USD", 50, "EUR")]
        [InlineData(-15, "", 13, "XXX")]
        public void Cannot_add_monies_with_different_currencies(decimal a1, string c1, decimal a2, string c2)
        {
            var x = Money.Dense(a1, c1);
            var y = Money.Dense(a2, c2);

            Assert.Throws<InvalidOperationException>(() => x + y);
            Assert.Throws<InvalidOperationException>(() => x.Add(y));
        }

        [Theory]
        [MemberData(nameof(TestData))]
        public void Adding_x_to_y_returns_z(Dense<string> x, Dense<string> y, Dense<string> z)
        {
            Assert.Equal(z, x + y);
            Assert.Equal(z, x.Add(y));
        }

        public static TheoryData<Dense<string>, Dense<string>, Dense<string>> TestData
            => new()
            {
                { Money.Dense(101, "$"), Money.Dense(99, "$"), Money.Dense(200, "$") },
                { Money.Dense(0.05m, "USD"), Money.Dense(0.025m, "USD"), Money.Dense(0.075m, "USD") },
                { Money.Dense(1.999m, "EUR"), Money.Dense(0.9m, "EUR"), Money.Dense(2.899m, "EUR") },
                { Money.Dense(2, "XBT"), Money.Dense(-1, "XBT"), Money.Dense(1, "XBT") },
                { Money.Dense(-100.5m, "€"), Money.Dense(-0.9m, "€"), Money.Dense(-101.4m, "€") },
            };
    }
}

using FsCheck.Xunit;
using Monies.Tests.Attributes;
using Monies.Tests.Generators;
using System;
using Xunit;

namespace Monies.Tests
{
    [MoneyProperties(QuietOnSuccess = true)]
    public class MoneyAdditionTests
    {
        [Property]
        public void Adding_x_to_y_is_the_same_as_adding_y_to_x(SameCurrency<string> monies)
        {
            var (x, y) = monies;

            Assert.Equal(x + y, y + x);
            Assert.Equal(x.Add(y), y.Add(x));
        }

        [Property]
        public void Adding_3_monies_in_any_order_returns_the_same_result(SameCurrency<string> monies)
        {
            var (x, y, z) = monies;

            Assert.Equal(x + y + z, x + (y + z));
            Assert.Equal(x.Add(y).Add(z), x.Add(y.Add(z)));
        }

        [Property]
        public void Adding_zero_to_a_money_returns_same_money(Money<string> x)
        {
            var zero = Money.Create(0, x.Currency);

            Assert.Equal(x, x + zero);
            Assert.Equal(x, zero + x);
            Assert.Equal(x, x.Add(zero));
            Assert.Equal(x, zero.Add(x));
        }

        [Property]
        public void Adding_the_opposite_returns_zero_money(Money<string> x)
        {
            var zero = Money.Create(0, x.Currency);

            Assert.Equal(zero, x + (-x));
            Assert.Equal(zero, x.Add(x.Negate()));
        }

        [Property]
        public void Adding_null_to_a_money_returns_null(Money<string> x)
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
            var x = Money.Create(a1, c1);
            var y = Money.Create(a2, c2);

            Assert.Throws<InvalidOperationException>(() => x + y);
            Assert.Throws<InvalidOperationException>(() => x.Add(y));
        }

        [Theory]
        [MemberData(nameof(TestData))]
        public void Adding_x_to_y_returns_z(Money<string> x, Money<string> y, Money<string> z)
        {
            Assert.Equal(z, x + y);
            Assert.Equal(z, x.Add(y));
        }

        public static TheoryData<Money<string>, Money<string>, Money<string>> TestData
            => new TheoryData<Money<string>, Money<string>, Money<string>>
            {
                { Money.Create(101, "$"), Money.Create(99, "$"), Money.Create(200, "$") },
                { Money.Create(0.05m, "USD"), Money.Create(0.025m, "USD"), Money.Create(0.075m, "USD") },
                { Money.Create(1.999m, "EUR"), Money.Create(0.9m, "EUR"), Money.Create(2.899m, "EUR") },
                { Money.Create(2, "XBT"), Money.Create(-1, "XBT"), Money.Create(1, "XBT") },
                { Money.Create(-100.5m, "€"), Money.Create(-0.9m, "€"), Money.Create(-101.4m, "€") },
            };
    }
}

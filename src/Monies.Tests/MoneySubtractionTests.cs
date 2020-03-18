using FsCheck.Xunit;
using Monies.Tests.Generators;
using System;
using Xunit;

namespace Monies.Tests
{
    [Properties(Arbitrary = new[] { typeof(MoneyArbitrary), typeof(SameCurrencyArbitrary) }, QuietOnSuccess = true)]
    public class MoneySubtractionTests
    {
        [Property]
        public void Substracting_x_to_y_is_opposite_of_substracting_y_to_x(SameCurrency<string> monies)
        {
            var (x, y) = monies;

            Assert.Equal(x - y, -(y - x));
            Assert.Equal(x.Subtract(y), y.Subtract(x).Negate());
        }

        [Property]
        public void Substracting_3_monies_in_different_order_gives_different_results(SameCurrency<string> monies)
        {
            var (x, y, z) = monies;

            Assert.NotEqual(x - y - z, x - (y - z));
            Assert.NotEqual(x.Subtract(y).Subtract(z), x.Subtract(y.Subtract(z)));
        }

        [Property(Skip = "cannot check decimal datatype equality without rounding")]
        public void For_any_x_and_y_there_must_be_2_monies_that_turns_x_into_y(SameCurrency<string> monies)
        {
            var (x, y) = monies;

            var a = x - y;
            var b = x + y;

            Assert.Equal(y, x - a);
            Assert.Equal(y, x.Subtract(a));
            Assert.Equal(y, b - x);
            Assert.Equal(y, b.Subtract(x));
        }

        [Property]
        public void Substracting_null_money_returns_null(Money<string> x)
        {
            Assert.Null(x.Subtract(null));
            Assert.Null(x - null);
            Assert.Null(null - x);
        }

        [Theory]
        [InlineData(100, "$", 200, "€")]
        [InlineData(0, "USD", 50, "EUR")]
        [InlineData(-15, "", 13, "XXX")]
        public void Cannot_substract_monies_with_different_currencies(decimal a1, string c1, decimal a2, string c2)
        {
            var x = Money.Create(a1, c1);
            var y = Money.Create(a2, c2);

            Assert.Throws<InvalidOperationException>(() => x - y);
            Assert.Throws<InvalidOperationException>(() => x.Subtract(y));
        }
    }
}

using FsCheck;
using FsCheck.Xunit;
using Monies.Tests.Attributes;
using Monies.Tests.Generators;
using System.Linq;
using Xunit;

namespace Monies.Tests
{
    [MoneyProperties(QuietOnSuccess = true)]
    public class MoneyMultiplicationTests
    {
        [Property(Skip = "cannot compare decimals without rounding")]
        public void Multiplying_3_values_in_any_order_returns_the_same_result(Money<string> x, decimal y, decimal z)
        {
            Assert.Equal(x * y * z, x * (y * z));
            Assert.Equal(x.Multiply(y).Multiply(z), x.Multiply(y * z));
        }

        [Property(Skip = "cannot compare decimals without rounding")]
        public void Multiplying_the_sum_of_2_monies_is_the_same_as_multiplying_each_one_and_then_adding_the_result(decimal x, SameCurrency<string> monies)
        {
            var (y, z) = monies;

            Assert.Equal((y + z) * x, y * x + z * x);
            Assert.Equal(y.Add(z).Multiply(x), y.Multiply(x).Add(z.Multiply(x)));
        }

        [Property]
        public void Multiplying_x_times_1_returns_x(Money<string> x)
        {
            Assert.Equal(x, x * 1);
            Assert.Equal(x, x.Multiply(1));
        }

        [Property]
        public void Multiplying_x_times_0_returns_0_money(Money<string> x)
        {
            var zero = Money.Create(0, x.Currency);

            Assert.Equal(zero, x * 0);
            Assert.Equal(zero, x.Multiply(0));
        }

        [Property]
        public void Multiplying_x_times_negative_1_is_equal_to_the_opposite_of_x(Money<string> x)
        {
            Assert.Equal(-x, x * -1);
            Assert.Equal(-x, x.Multiply(-1));
        }

        [Property]
        public void Multiplying_by_positive_preserves_order(PositiveInt x, SameCurrency<string> monies)
        {
            var (y, z) = monies;
            var x_ = (int)x;

            Assert.Equal(y > z, (y * x_) > (z * x_));
            Assert.Equal(y > z, y.Multiply(x_) > z.Multiply(x_));
        }

        [Property]
        public void Multiplying_by_negative_reverses_order(NegativeInt x, SameCurrency<string> monies)
        {
            var (y, z) = monies;
            var x_ = (int)x;

            Assert.NotEqual(y > z, (y * x_) > (z * x_));
            Assert.NotEqual(y > z, y.Multiply(x_) > z.Multiply(x_));
        }

        [Property(Skip = "cannot compare decimals without rounding")]
        public void Multiplying_x_times_y_is_the_sum_of_y_copies_of_x(Money<string> x, PositiveInt y)
        {
            var y_ = (int)y;

            var sum = Enumerable.Repeat(x, y_).Aggregate((a, b) => a + b);

            Assert.Equal(sum, x * y_);
            Assert.Equal(sum, x.Multiply(y_));
        }

        [Property]
        public void Multiplying_null_money_returns_null(decimal x)
        {
            Money<string> y = null;

            Assert.Null(y * x);
        }

    }
}

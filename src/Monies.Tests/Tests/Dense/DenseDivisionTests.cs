using FsCheck;
using FsCheck.Xunit;
using Monies.Tests.Attributes;
using Monies.Tests.Generators;
using System;
using Xunit;

namespace Monies.Tests
{
    [MoneyProperties(QuietOnSuccess = true)]
    public class DenseDivisionTests
    {
        [Property]
        public void Divide_by_1_returns_the_same_money(Dense<string> x)
        {
            Assert.Equal(x, x / 1);
            Assert.Equal(x, x.Divide(1));
        }

        [Property(Arbitrary = new[] { typeof(NonZeroDenseArbitrary) })]
        public void Divide_by_whole_amount_returns_1(Dense<string> x)
        {
            var y = Money.Dense(1, x.Currency);

            Assert.Equal(y, x / x.Amount);
            Assert.Equal(y, x.Divide(x.Amount));
        }

        [Property]
        public void Dividing_null_money_returns_null(decimal x)
        {
            Dense<string> y = null;

            Assert.Null(y / x);
        }

        [Property]
        public void Cannot_divide_by_0(Dense<string> x)
        {
            Assert.Throws<DivideByZeroException>(() => x / 0);
            Assert.Throws<DivideByZeroException>(() => x.Divide(0));
        }

        [Property]
        public void Dividing_the_sum_of_2_monies_is_the_same_as_dividing_each_and_then_adding_the_result(NonZeroInt x, SameCurrencyDense<string> monies)
        {
            var (y, z) = monies;
            var x_ = (int)x;

            Assert.Equal((y + z) / x_, (y / x_) + (z / x_));
            Assert.Equal(y.Add(z).Divide(x_), y.Divide(x_).Add(z.Divide(x_)));
        }

        [Property]
        public void Dividing_3_values_is_the_same_as_dividing_x_by_the_product_of_y_and_z(Dense<string> x, NonZeroInt y, NonZeroInt z)
        {
            int y_ = (int)y, z_ = (int)z;

            Assert.Equal(x / (y_ * z_), x / y_ / z_);
            Assert.Equal(x.Divide(y_ * z_), x.Divide(y_).Divide(z_));
        }

        [Theory]
        [MemberData(nameof(TestData))]
        public void Dividing_x_by_y_returns_z(Dense<string> x, decimal y, Dense<string> z)
        {
            Assert.Equal(z, x / y);
            Assert.Equal(z, x.Divide(y));
        }

        public static TheoryData<Dense<string>, decimal, Dense<string>> TestData
            => new TheoryData<Dense<string>, decimal, Dense<string>>
            {
                { Money.Dense(120, "$"), 12, Money.Dense(10, "$") },
                { Money.Dense(200.5m, "USD"), 2, Money.Dense(100.25m, "USD") },
                { Money.Dense(50.0625m, "EUR"), 0.5m, Money.Dense(100.125m, "EUR") },
                { Money.Dense(-500, "€"), 5m, Money.Dense(-100, "€") },
            };
    }
}

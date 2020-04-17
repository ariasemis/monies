using FsCheck;
using FsCheck.Xunit;
using Monies.Tests.Attributes;
using Monies.Tests.Generators;
using System;
using Xunit;

namespace Monies.Tests
{
    [MoneyProperties(QuietOnSuccess = true)]
    public class MoneyDivisionTests
    {
        [Property]
        public void Divide_by_1_returns_the_same_money(Money<string> x)
        {
            Assert.Equal(x, x / 1);
            Assert.Equal(x, x.Divide(1));
        }

        [Property(Arbitrary = new[] { typeof(NonZeroMoneyArbitrary) })]
        public void Divide_by_whole_amount_returns_1(Money<string> x)
        {
            var y = Money.Create(1, x.Currency);

            Assert.Equal(y, x / x.Amount);
            Assert.Equal(y, x.Divide(x.Amount));
        }

        [Property]
        public void Dividing_null_money_returns_null(decimal x)
        {
            Money<string> y = null;

            Assert.Null(y / x);
        }

        [Property]
        public void Cannot_divide_by_0(Money<string> x)
        {
            Assert.Throws<DivideByZeroException>(() => x / 0);
            Assert.Throws<DivideByZeroException>(() => x.Divide(0));
        }

        [Property(Skip = "cannot compare values with repeating decimals")]
        public void Dividing_the_sum_of_2_monies_is_the_same_as_dividing_each_and_then_adding_the_result(NonZeroInt x, SameCurrency<string> monies)
        {
            var (y, z) = monies;
            var x_ = (int)x;

            Assert.Equal((y + z) / x_, (y / x_) + (z / x_));
            Assert.Equal(y.Add(z).Divide(x_), y.Divide(x_).Add(z.Divide(x_)));
        }

        [Property(Skip = "cannot compare values with repeating decimals")]
        public void Dividing_3_values_is_the_same_as_dividing_x_by_the_product_of_y_and_z(Money<string> x, NonZeroInt y, NonZeroInt z)
        {
            int y_ = (int)y, z_ = (int)z;

            Assert.Equal(x / (y_ * z_), x / y_ / z_);
            Assert.Equal(x.Divide(y_ * z_), x.Divide(y_).Divide(z_));
        }

        [Theory]
        [MemberData(nameof(TestData))]
        public void Dividing_x_by_y_returns_z(Money<string> x, decimal y, Money<string> z)
        {
            Assert.Equal(z, x / y);
            Assert.Equal(z, x.Divide(y));
        }

        public static TheoryData<Money<string>, decimal, Money<string>> TestData
            => new TheoryData<Money<string>, decimal, Money<string>>
            {
                { Money.Create(120, "$"), 12, Money.Create(10, "$") },
                { Money.Create(200.5m, "USD"), 2, Money.Create(100.25m, "USD") },
                { Money.Create(50.0625m, "EUR"), 0.5m, Money.Create(100.125m, "EUR") },
                { Money.Create(-500, "€"), 5m, Money.Create(-100, "€") },
            };
    }
}

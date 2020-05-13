using FsCheck.Xunit;
using Monies.Internal;
using Monies.Tests.Generators;
using System;
using Xunit;

namespace Monies.Tests
{
    public class RationalDivisionTests
    {
        [Property]
        public void Divide_by_1_returns_the_same_value(Rational x)
        {
            Assert.Equal(x, x / Rational.One);
            Assert.Equal(x, Rational.Divide(x, Rational.One));
        }

        [Property]
        public void Divide_by_itself_returns_1(NonZeroRational x)
        {
            Assert.Equal(Rational.One, x.Item / x);
            Assert.Equal(Rational.One, Rational.Divide(x, x));
        }

        [Property]
        public void Cannot_divide_by_zero(Rational x)
        {
            Assert.Throws<DivideByZeroException>(() => x / Rational.Zero);
            Assert.Throws<DivideByZeroException>(() => Rational.Divide(x, Rational.Zero));
        }

        [Property(Arbitrary = new[] { typeof(NonZeroDenominatorArbitrary) })]
        public void Division_is_the_same_as_multiplying_by_reciprocal(Rational x, NonZeroRational y)
        {
            var expected = x * y.Item.Invert();

            Assert.Equal(expected, x / y);
            Assert.Equal(expected, Rational.Divide(x, y));
        }

        [Property]
        public void Dividing_the_sum_is_the_same_as_dividing_each_and_then_adding_the_result(NonZeroRational x, Rational y, Rational z)
        {
            Assert.Equal((y + z) / x, (y / x) + (z / x));
            Assert.Equal(Rational.Divide(Rational.Add(y, z), x), Rational.Add(Rational.Divide(y, x), Rational.Divide(z, x)));
        }

        [Property]
        public void Dividing_3_values_is_the_same_as_dividing_by_the_product(Rational x, NonZeroRational y, NonZeroRational z)
        {
            Rational y_ = y, z_ = z;

            Assert.Equal(x / (y_ * z_), x / y_ / z_);
            Assert.Equal(Rational.Divide(x, Rational.Multiply(y_, z_)), Rational.Divide(Rational.Divide(x, y_), z_));
        }
    }
}

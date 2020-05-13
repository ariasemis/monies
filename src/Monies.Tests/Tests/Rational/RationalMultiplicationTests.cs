using FsCheck.Xunit;
using Monies.Internal;
using Monies.Tests.Generators;
using Xunit;

namespace Monies.Tests
{
    public class RationalMultiplicationTests
    {
        [Property]
        public void Multiplying_3_values_in_any_order_returns_the_same_result(Rational x, Rational y, Rational z)
        {
            Assert.Equal(x * y * z, x * (y * z));
            Assert.Equal(Rational.Multiply(Rational.Multiply(x, y), z), Rational.Multiply(x, Rational.Multiply(y, z)));
        }

        [Property(Arbitrary = new[] { typeof(NonZeroDenominatorArbitrary) })]
        public void Multiplying_the_sum_is_the_same_as_multiplying_each_one_and_then_adding_the_result(Rational x, Rational y, Rational z)
        {
            Assert.Equal((y + z) * x, y * x + z * x);
            Assert.Equal(Rational.Multiply(Rational.Add(y, z), x), Rational.Add(Rational.Multiply(y, x), Rational.Multiply(z, x)));
        }

        [Property]
        public void Multiplying_x_times_1_returns_x(Rational x)
        {
            Assert.Equal(x, x * Rational.One);
            Assert.Equal(x, Rational.Multiply(x, Rational.One));
        }

        [Property]
        public void Multiplying_x_times_0_returns_0(Rational x)
        {
            Assert.Equal(Rational.Zero, x * Rational.Zero);
            Assert.Equal(Rational.Zero, Rational.Multiply(x, Rational.Zero));
        }

        [Property]
        public void Multiplying_x_by_its_reciprocal_returns_1(NonZeroRational x)
        {
            var x_ = x.Item;

            Assert.Equal(Rational.One, x_ * x_.Invert());
            Assert.Equal(Rational.One, Rational.Multiply(x_, x_.Invert()));
        }

        [Property(Arbitrary = new[] { typeof(NonZeroDenominatorArbitrary) })]
        public void Multiplying_by_positive_preserves_order(PositiveRational x, Rational y, Rational z)
        {
            Assert.Equal(y > z, (y * x) > (z * x));
            Assert.Equal(y > z, Rational.Multiply(y, x) > Rational.Multiply(z, x));
        }

        [Property(Arbitrary = new[] { typeof(NonZeroDenominatorArbitrary) })]
        public void Multiplying_by_negative_reverses_order(NegativeRational x, Rational y, Rational z)
        {
            if (y > z)
            {
                Assert.True((y * x) < (z * x));
                Assert.True(Rational.Multiply(y, x) < Rational.Multiply(z, x));
            }
        }
    }
}

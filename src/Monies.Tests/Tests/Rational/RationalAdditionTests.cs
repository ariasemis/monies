using FsCheck.Xunit;
using Monies.Internal;
using Monies.Tests.Generators;
using Xunit;

namespace Monies.Tests
{
    public class RationalAdditionTests
    {
        [Property]
        public void Adding_x_to_y_is_the_same_as_adding_y_to_x(Rational x, Rational y)
        {
            Assert.Equal(x + y, y + x);
            Assert.Equal(Rational.Add(x, y), Rational.Add(y, x));
        }

        [Property]
        public void Adding_3_rationals_in_any_order_returns_the_same_result(Rational x, Rational y, Rational z)
        {
            Assert.Equal(x + y + z, x + (y + z));
            Assert.Equal(Rational.Add(Rational.Add(x, y), z), Rational.Add(x, Rational.Add(y, z)));
        }

        [Property]
        public void Adding_0_to_a_rational_returns_the_same_value(Rational x)
        {
            Assert.Equal(x, x + Rational.Zero);
            Assert.Equal(x, Rational.Zero + x);
            Assert.Equal(x, Rational.Add(x, Rational.Zero));
            Assert.Equal(x, Rational.Add(Rational.Zero, x));
        }

        [Property(Arbitrary = new[] { typeof(NonZeroDenominatorArbitrary) })]
        public void Adding_the_opposite_returns_0(Rational x)
        {
            Assert.Equal(Rational.Zero, x + (-x));
            Assert.Equal(Rational.Zero, Rational.Add(x, Rational.Negate(x)));
        }

        [Property]
        public void Adding_NaN_returns_NaN(Rational x)
        {
            Assert.Equal(Rational.NaN, x + Rational.NaN);
            Assert.Equal(Rational.NaN, Rational.Add(x, Rational.NaN));
        }
    }
}

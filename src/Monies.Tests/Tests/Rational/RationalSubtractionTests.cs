using FsCheck.Xunit;
using Monies.Internal;
using Monies.Tests.Generators;
using Xunit;

namespace Monies.Tests
{
    public class RationalSubtractionTests
    {
        [Property]
        public void Substracting_x_to_y_is_opposite_of_substracting_y_to_x(Rational x, Rational y)
        {
            Assert.Equal(x - y, -(y - x));
            Assert.Equal(Rational.Subtract(x, y), Rational.Negate(Rational.Subtract(y, x)));
        }

        [Property(Arbitrary = new[] { typeof(NonZeroDenominatorArbitrary) })]
        public void For_any_x_and_y_there_must_be_2_monies_that_turns_x_into_y(Rational x, Rational y)
        {
            var a = x - y;
            var b = x + y;

            Assert.Equal(y, x - a);
            Assert.Equal(y, Rational.Subtract(x, a));
            Assert.Equal(y, b - x);
            Assert.Equal(y, Rational.Subtract(b, x));
        }

        [Property]
        public void Substracting_y_to_x_is_the_same_as_adding_opposite_of_y_to_x(Rational x, Rational y)
        {
            Assert.Equal(x - y, x + (-y));
            Assert.Equal(Rational.Subtract(x, y), Rational.Add(x, Rational.Negate(y)));
        }
    }
}

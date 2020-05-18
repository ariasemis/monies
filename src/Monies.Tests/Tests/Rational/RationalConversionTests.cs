using FsCheck.Xunit;
using Monies.Internal;
using Monies.Tests.Generators;
using System;
using Xunit;

namespace Monies.Tests
{
    public class RationalConversionTests
    {
        [Property]
        public void Converting_from_decimal_and_back_again_returns_the_original_value(decimal x)
        {
            var accuracy = 1E-28m;

            Assert.True(Math.Abs(x - (decimal)(Rational)x) <= accuracy);
            Assert.True(Math.Abs(x - Rational.ToDecimal(Rational.FromDecimal(x))) <= accuracy);
        }

        [Property(Arbitrary = new[] { typeof(NonZeroDenominatorArbitrary) })]
        public void Converting_to_decimal_and_back_again_returns_the_original_value(Rational x)
        {
            Assert.Equal(x, (Rational)(decimal)x);
            Assert.Equal(x, Rational.FromDecimal(Rational.ToDecimal(x)));
        }

        [Fact]
        public void Cannot_convert_to_decimal_if_denominator_is_zero()
        {
            var x = new Rational(1, 0);

            Assert.Throws<DivideByZeroException>(() => (decimal)x);
            Assert.Throws<DivideByZeroException>(() => Rational.ToDecimal(x));
        }
    }
}

using FsCheck.Xunit;
using Monies.Internal;
using System;
using Xunit;

namespace Monies.Tests
{
    public class RationalUnaryTests
    {
        [Property]
        public void Negating_rational_works(Rational rational)
        {
            var numerator = Math.Abs(rational.Numerator);
            var denominator = rational.Denominator;
            var sign = rational.Sign * -1;

            Assert.Equal(numerator, Math.Abs((-rational).Numerator));
            Assert.Equal(denominator, (-rational).Denominator);
            Assert.Equal(numerator, Math.Abs(Rational.Negate(rational).Numerator));
            Assert.Equal(denominator, Rational.Negate(rational).Denominator);
            Assert.Equal(sign, (-rational).Sign);
            Assert.Equal(sign, Rational.Negate(rational).Sign);
        }
    }
}

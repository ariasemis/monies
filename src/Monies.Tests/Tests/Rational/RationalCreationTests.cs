using FsCheck.Xunit;
using Monies.Internal;
using System;
using Xunit;

namespace Monies.Tests
{
    public sealed class RationalCreationTests
    {
        [Property]
        public void Created_rational_has_expected_values(long numerator, long denominator)
        {
            var actual = new Rational(numerator, denominator);

            var expectedSign = denominator == 0 ? Math.Sign(numerator) : Math.Sign(numerator * denominator);
            
            Assert.Equal(expectedSign, actual.Sign);
            Assert.Equal(Math.Abs(numerator), Math.Abs(actual.Numerator));
            Assert.Equal(Math.Abs(denominator), Math.Abs(actual.Denominator));
        }
    }
}

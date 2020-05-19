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

            if (denominator == 0)
            {
                Assert.Equal(Math.Sign(numerator), actual.Numerator);
                Assert.Equal(0, actual.Denominator);
            }
            else
            {
                Assert.Equal(Math.Sign(numerator * denominator), actual.Sign);
                Assert.Equal(Math.Abs(numerator * actual.Denominator), Math.Abs(actual.Numerator * denominator));
            }
        }
    }
}

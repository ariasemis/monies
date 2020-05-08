using Monies.Internal;
using System;
using Xunit;

namespace Monies.Tests
{
    public sealed class RationalCreationTests
    {
        public static TheoryData<long, long> Data => new TheoryData<long, long>
        {
            { 1, 1 },
            { 12, 5 },
            { 0, 1 },
            { -3, 7 },
        };

        [Theory]
        [MemberData(nameof(Data))]
        public void Created_rational_has_expected_values(long numerator, long denominator)
        {
            var actual = new Rational(numerator, denominator);

            Assert.Equal(numerator, actual.Numerator);
            Assert.Equal(denominator, actual.Denominator);
            Assert.Equal(Math.Sign(numerator), actual.Sign);
        }

        [Fact]
        public void Created_rational_has_normalized_values()
        {
            var actual = new Rational(1, -2);

            Assert.Equal(-1, actual.Numerator);
            Assert.Equal(2, actual.Denominator);
        }
    }
}

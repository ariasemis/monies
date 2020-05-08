﻿using Monies.Internal;
using Xunit;

namespace Monies.Tests
{
    public sealed class RationalComparisonTests
    {
        public static TheoryData<Rational, Rational> SameValues => new TheoryData<Rational, Rational>
        {
            { new Rational(1, 3), new Rational(1, 3) },
            { new Rational(1, 2), new Rational(2, 4) },
            { new Rational(2, 4), new Rational(4, 8) },
            { new Rational(-11, 5), new Rational(11, -5) },
            { new Rational(0, 1), new Rational(0, 10) },
        };

        public static TheoryData<Rational, Rational> DifferentValues => new TheoryData<Rational, Rational>
        {
            { new Rational(3, 5), new Rational(2, 7) },
            { new Rational(1, 2), new Rational(-1, 2) },
            { new Rational(-1, 3), new Rational(-1, 1) },
            { new Rational(0, 1), new Rational(1, 0) },
        };

        public static TheoryData<Rational, Rational> LesserValues => new TheoryData<Rational, Rational>
        {
            { new Rational(1, 3), new Rational(1, 2) },
            { new Rational(-1, 1), new Rational(1, 1) },
            { new Rational(-2, 3), new Rational(-1, 3) },
        };

        [Fact]
        public void Rational_is_equal_to_itself()
        {
            var x = new Rational(1, 2);
            var y = x;

            Assert.True(Equals(x, y));
            Assert.True(x == y);
            Assert.True(x >= y);
            Assert.True(x <= y);
            Assert.True(x.Equals(y));
            Assert.True(x.Equals((object)y));
            Assert.Equal(0, x.CompareTo(y));
        }

        [Theory]
        [MemberData(nameof(SameValues))]
        public void Rational_are_equivalent_if_represent_the_same_value(Rational x, Rational y)
        {
            Assert.True(Equals(x, y));
            Assert.True(x == y);
            Assert.True(x >= y);
            Assert.True(x <= y);
            Assert.True(x.Equals(y));
            Assert.True(x.Equals((object)y));
            Assert.Equal(0, x.CompareTo(y));
            Assert.Equal(x.GetHashCode(), y.GetHashCode());
        }

        [Theory]
        [MemberData(nameof(DifferentValues))]
        public void Rational_are_not_equal_if_different_values(Rational x, Rational y)
        {
            Assert.True(x != y);
            Assert.False(x == y);
            Assert.False(Equals(x, y));
            Assert.False(x.Equals(y));
            Assert.False(x.Equals((object)y));
            Assert.NotEqual(0, x.CompareTo(y));
        }

        [Fact]
        public void Rational_is_not_equal_to_another_object()
        {
            var x = new Rational(1, 1);
            var y = (1, 1);

            Assert.False(Equals(x, y));
            Assert.False(x.Equals(y));
        }

        [Theory]
        [MemberData(nameof(SameValues))]
        [MemberData(nameof(DifferentValues))]
        public void At_least_one_rational_must_be_lt_or_eq_the_other(Rational x, Rational y)
        {
            Assert.True(x.CompareTo(y) <= 0 || y.CompareTo(x) <= 0);
            Assert.True(x <= y || y <= x);
            Assert.True(x >= y || y >= x);
        }

        [Theory]
        [MemberData(nameof(DifferentValues))]
        [MemberData(nameof(LesserValues))]
        public void If_not_equal_then_x_to_y_is_opposite_than_y_to_x(Rational x, Rational y)
        {
            Assert.Equal(x.CompareTo(y), y.CompareTo(x) * -1);
            Assert.True(x < y != y < x);
            Assert.True(x <= y != y <= x);
            Assert.True(x > y != y > x);
            Assert.True(x >= y != y >= x);
        }

        [Theory]
        [MemberData(nameof(LesserValues))]
        public void Rational_x_is_less_than_y(Rational x, Rational y)
        {
            Assert.True(x.CompareTo(y) < 0);
            Assert.True(x < y);
            Assert.True(x <= y);
            Assert.False(x > y);
            Assert.False(x >= y);
        }
    }
}

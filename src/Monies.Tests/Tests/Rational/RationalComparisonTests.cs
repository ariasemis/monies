using Monies.Internal;
using Xunit;

namespace Monies.Tests.Tests
{
    public sealed class RationalComparisonTests
    {
        // TODO: use parameterized tests

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

        [Fact]
        public void Rational_are_equivalent_if_represent_the_same_value()
        {
            var x = new Rational(1, 2);
            var y = new Rational(2, 4);

            Assert.True(Equals(x, y));
            Assert.True(x == y);
            Assert.True(x >= y);
            Assert.True(x <= y);
            Assert.True(x.Equals(y));
            Assert.True(x.Equals((object)y));
            Assert.Equal(0, x.CompareTo(y));
            Assert.Equal(x.GetHashCode(), y.GetHashCode());
        }

        [Fact]
        public void Rational_are_not_equal_if_different_values()
        {
            var x = new Rational(3, 5);
            var y = new Rational(2, 7);

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

        [Fact]
        public void At_least_one_rational_must_be_lt_or_eq_the_other()
        {
            var x = new Rational(2, 3);
            var y = new Rational(3, 4);

            Assert.True(x.CompareTo(y) <= 0 || y.CompareTo(x) <= 0);
            Assert.True(x <= y || y <= x);
            Assert.True(x >= y || y >= x);
        }

        [Fact]
        public void If_not_equal_then_x_to_y_is_opposite_than_y_to_x()
        {
            var x = new Rational(1, 5);
            var y = new Rational(6, 3);

            Assert.Equal(x.CompareTo(y), y.CompareTo(x) * -1);
            Assert.True(x < y != y < x);
            Assert.True(x <= y != y <= x);
            Assert.True(x > y != y > x);
            Assert.True(x >= y != y >= x);
        }

        [Fact]
        public void Rational_x_is_less_than_y()
        {
            var x = new Rational(1, 3);
            var y = new Rational(1, 2);

            Assert.True(x.CompareTo(y) < 0);
            Assert.True(x < y);
            Assert.True(x <= y);
            Assert.False(x > y);
            Assert.False(x >= y);
        }
    }
}

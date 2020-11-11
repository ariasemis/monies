using FsCheck;
using FsCheck.Xunit;
using Monies.Internal;
using Monies.Tests.Generators;
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

        [Property]
        public void Rational_equality_methods_are_equivalent(Rational x, Rational y)
        {
            var expected = Equals(x, y);

            Assert.Equal(expected, x == y);
            Assert.NotEqual(expected, x != y);
            Assert.Equal(expected, x.Equals(y));
            Assert.Equal(expected, x.Equals((object)y));
            Assert.Equal(expected, x >= y && x <= y);
            Assert.Equal(expected, 0 == x.CompareTo(y));

            if (expected)
            {
                Assert.Equal(x.GetHashCode(), y.GetHashCode());
            }
        }

        [Property]
        public bool Rational_is_equal_to_itself(Rational x)
            => Specs.Equality.Reflexivity(x);

        [Theory]
        [MemberData(nameof(SameValues))]
        public bool Rational_are_equivalent_if_represent_the_same_value(Rational x, Rational y)
            => x == y;

        [Theory]
        [MemberData(nameof(DifferentValues))]
        public bool Rational_are_not_equal_if_different_values(Rational x, Rational y)
            => x != y;

        [Fact]
        public void Rational_is_not_equal_to_another_object()
        {
            var x = new Rational(1, 1);
            var y = (1, 1);

            Assert.False(Equals(x, y));
            Assert.False(x.Equals(y));
        }

        [Property]
        public bool If_x_equals_y_then_y_equals_x(Rational x, Rational y)
            => Specs.Equality.Symmetry(x, y);

        [Property(Arbitrary = new[] { typeof(EquivalentRationalsArbitrary) })]
        public Property If_x_equals_y_and_y_equals_z_then_x_equals_z(RationalSet set)
        {
            var (x, y, z) = set;
            return Specs.Equality.Transitivity(x, y, z);
        }

        [Property]
        public void At_least_one_rational_must_be_lt_or_eq_the_other(Rational x, Rational y)
        {
            Assert.True(x.CompareTo(y) <= 0 || y.CompareTo(x) <= 0);
            Assert.True(x <= y || y <= x);
            Assert.True(x >= y || y >= x);
        }

        [Property]
        public void If_both_are_lte_then_they_are_equal(Rational x, Rational y)
        {
            if (x <= y && y <= x)
            {
                Assert.True(x.Equals(y));
                Assert.True(x.Equals((object)y));
                Assert.True(x == y);
                Assert.False(x != y);
                Assert.Equal(0, x.CompareTo(y));
            }
        }

        [Property]
        public void If_x_lte_y_and_y_lte_z_then_x_lte_z(Rational x, Rational y, Rational z)
        {
            if (x <= y && y <= z)
            {
                Assert.True(x <= z);
                Assert.True(x.CompareTo(z) <= 0);
            }
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

using System;

namespace Monies.Internal
{
    public partial struct Rational : IComparable<Rational>
    {
        public int CompareTo(Rational other)
        {
            var first = numerator * other.denominator;
            var second = other.numerator * denominator;

            return first.CompareTo(second);
        }

        public static bool operator <(Rational left, Rational right)
            => left.CompareTo(right) < 0;

        public static bool operator <=(Rational left, Rational right)
            => left.CompareTo(right) <= 0;

        public static bool operator >(Rational left, Rational right)
            => left.CompareTo(right) > 0;

        public static bool operator >=(Rational left, Rational right)
            => left.CompareTo(right) >= 0;
    }
}

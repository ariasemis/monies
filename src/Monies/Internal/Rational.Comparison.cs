using System;

namespace Monies.Internal
{
    public partial struct Rational : IComparable<Rational>
    {
        public int CompareTo(Rational other)
        {
            var first = Numerator * other.Denominator;
            var second = other.Numerator * Denominator;

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

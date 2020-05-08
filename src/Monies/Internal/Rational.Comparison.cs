using System;

namespace Monies.Internal
{
    public partial struct Rational : IComparable<Rational>
    {
        public int CompareTo(Rational other)
        {
            var sign = Sign.CompareTo(other.Sign);
            if (sign != 0)
                return sign;

            if (IsInfinity && other.IsInfinity)
                return 0;
            if (IsPositiveInfinity || other.IsNegativeInfinity)
                return 1;
            if (IsNegativeInfinity || other.IsPositiveInfinity)
                return -1;

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

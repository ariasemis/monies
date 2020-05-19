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

            var diff = this - other;

            return diff.Sign;
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

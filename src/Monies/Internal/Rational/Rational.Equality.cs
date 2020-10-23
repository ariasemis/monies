using System;

namespace Monies.Internal
{
    public partial struct Rational : IEquatable<Rational>
    {
        public bool Equals(Rational other)
            => CompareTo(other) == 0;

        public override bool Equals(object obj)
            => obj is Rational rational && Equals(rational);

        public override int GetHashCode()
        {
            unchecked
            {
                if (IsNaN)
                    return double.NaN.GetHashCode();
                if (IsPositiveInfinity)
                    return double.PositiveInfinity.GetHashCode();
                if (IsNegativeInfinity)
                    return double.NegativeInfinity.GetHashCode();

                var d = Numerator / Denominator;
                return d.GetHashCode();
            }
        }

        public static bool operator ==(Rational left, Rational right) => left.Equals(right);

        public static bool operator !=(Rational left, Rational right) => !(left == right);
    }
}

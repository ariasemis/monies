using System;

namespace Monies.Internal
{
    public partial struct Rational : IEquatable<Rational>
    {
        public bool Equals(Rational other)
            => CompareTo(other) == 0;

        public override bool Equals(object obj)
            => obj is Rational && Equals((Rational)obj);

        public override int GetHashCode()
        {
            var d = (decimal)numerator / denominator;
            return d.GetHashCode();
        }

        public static bool operator ==(Rational left, Rational right) => left.Equals(right);

        public static bool operator !=(Rational left, Rational right) => !(left == right);
    }
}

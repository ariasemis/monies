using System;

namespace Monies
{
    public sealed partial class Discrete<TCurrency> : IEquatable<Discrete<TCurrency>>
        where TCurrency : IEquatable<TCurrency>
    {
        public static bool operator ==(Discrete<TCurrency> left, Discrete<TCurrency> right)
        {
            if (left is null && right is null)
                return true;

            if (left is null || right is null)
                return false;

            return left.Equals(right);
        }

        public static bool operator !=(Discrete<TCurrency> left, Discrete<TCurrency> right)
            => !(left == right);

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 13;

                hash = hash * 397 + Amount.GetHashCode();
                hash = hash * 397 + Currency.GetHashCode();
                hash = hash * 397 + Unit.Scale.GetHashCode();

                return hash;
            }
        }

        public override bool Equals(object obj)
            => Equals(obj as Discrete<TCurrency>);

        public bool Equals(Discrete<TCurrency> other)
        {
            if (other is null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return Amount.Equals(other.Amount)
                && Currency.Equals(other.Currency)
                && Unit.Scale.Equals(other.Unit.Scale);
        }
    }
}

using System;

namespace Monies
{
    public sealed partial class Money<TCurrency> : IEquatable<Money<TCurrency>>
        where TCurrency : IEquatable<TCurrency>
    {
        public static bool operator ==(Money<TCurrency> left, Money<TCurrency> right)
        {
            if (left is null && right is null)
                return true;

            if (left is null || right is null)
                return false;

            return left.Equals(right);
        }

        public static bool operator !=(Money<TCurrency> left, Money<TCurrency> right) => !(left == right);

        public override bool Equals(object obj)
            => Equals(obj as Money<TCurrency>);

        public override int GetHashCode()
            => (Amount.GetHashCode() * 397) ^ Currency.GetHashCode();

        public bool Equals(Money<TCurrency> other)
        {
            if (other is null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return Amount.Equals(other.Amount)
                && Currency.Equals(other.Currency);
        }
    }
}

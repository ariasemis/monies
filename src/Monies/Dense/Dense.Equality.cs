using System;

namespace Monies
{
    public sealed partial class Dense<TCurrency> : IEquatable<Dense<TCurrency>>
        where TCurrency : IEquatable<TCurrency>
    {
        public static bool operator ==(Dense<TCurrency> left, Dense<TCurrency> right)
        {
            if (left is null && right is null)
                return true;

            if (left is null || right is null)
                return false;

            return left.Equals(right);
        }

        public static bool operator !=(Dense<TCurrency> left, Dense<TCurrency> right) => !(left == right);

        public override bool Equals(object? obj)
            => Equals(obj as Dense<TCurrency>);

        public override int GetHashCode()
            => (amount.GetHashCode() * 397) ^ Currency.GetHashCode();

        public bool Equals(Dense<TCurrency>? other)
        {
            if (other is null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            return amount.Equals(other.amount)
                && Currency.Equals(other.Currency);
        }
    }
}

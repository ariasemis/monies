using System;

namespace Monies
{
    public sealed partial class Dense<TCurrency> : IComparable, IComparable<Dense<TCurrency>>
    {
        public static bool operator <(Dense<TCurrency> left, Dense<TCurrency> right)
        {
            if (right is null)
                return false;

            if (left is null)
                return true;

            AssertSameCurrency(left, right);

            return left.amount < right.amount;
        }

        public static bool operator >(Dense<TCurrency> left, Dense<TCurrency> right) => right < left;

        public static bool operator <=(Dense<TCurrency> left, Dense<TCurrency> right) => left == right || left < right;

        public static bool operator >=(Dense<TCurrency> left, Dense<TCurrency> right) => left == right || left > right;

        public int CompareTo(object? obj)
        {
            if (obj is null)
                return 1;

            if (obj is not Dense<TCurrency> other)
                throw new ArgumentException($"{nameof(obj)} must be of type {typeof(Dense<TCurrency>).Name}", nameof(obj));

            return CompareTo(other);
        }

        public int CompareTo(Dense<TCurrency>? other)
        {
            if (other is null || other < this)
                return 1;

            if (this < other)
                return -1;

            return 0;
        }
    }
}

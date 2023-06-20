using System;

namespace Monies
{
    public sealed partial class Money<TCurrency> : IComparable, IComparable<Money<TCurrency>>
    {
        public static bool operator <(Money<TCurrency> left, Money<TCurrency> right)
        {
            if (right is null)
                return false;

            if (left is null)
                return true;

            AssertSameCurrency(left, right);

            return left.amount < right.amount;
        }

        public static bool operator >(Money<TCurrency> left, Money<TCurrency> right) => right < left;

        public static bool operator <=(Money<TCurrency> left, Money<TCurrency> right) => left == right || left < right;

        public static bool operator >=(Money<TCurrency> left, Money<TCurrency> right) => left == right || left > right;

        public int CompareTo(object? obj)
        {
            if (obj is null)
                return 1;

            if (obj is not Money<TCurrency> other)
                throw new ArgumentException($"{nameof(obj)} must be of type {typeof(Money<TCurrency>).Name}", nameof(obj));

            return CompareTo(other);
        }

        public int CompareTo(Money<TCurrency>? other)
        {
            if (other is null || other < this)
                return 1;

            if (this < other)
                return -1;

            return 0;
        }
    }
}

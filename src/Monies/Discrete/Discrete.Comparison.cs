using System;

namespace Monies
{
    public sealed partial class Discrete<TCurrency> : IComparable, IComparable<Discrete<TCurrency>>
    {
        public static bool operator <(Discrete<TCurrency> left, Discrete<TCurrency> right)
        {
            if (right is null)
                return false;

            if (left is null)
                return true;

            AssertSameCurrency(left, right);

            var first = left.Amount * right.Unit.Scale;
            var second = right.Amount * left.Unit.Scale;

            return first < second;
        }

        public static bool operator >(Discrete<TCurrency> left, Discrete<TCurrency> right) => right < left;

        public static bool operator <=(Discrete<TCurrency> left, Discrete<TCurrency> right) => left == right || left < right;

        public static bool operator >=(Discrete<TCurrency> left, Discrete<TCurrency> right) => left == right || left > right;

        public int CompareTo(object? obj)
        {
            if (obj is null)
                return 1;

            if (obj is not Discrete<TCurrency> other)
                throw new ArgumentException($"{nameof(obj)} must be of type {typeof(Discrete<TCurrency>).Name}", nameof(obj));

            return CompareTo(other);
        }

        public int CompareTo(Discrete<TCurrency>? other)
        {
            if (other is null || other < this)
                return 1;

            if (this < other)
                return -1;

            return 0;
        }
    }
}

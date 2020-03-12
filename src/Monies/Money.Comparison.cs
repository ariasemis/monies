using System;

namespace Monies
{
    public sealed partial class Money<TCurrency> : IComparable, IComparable<Money<TCurrency>>
    {
        public static bool operator <(Money<TCurrency> left, Money<TCurrency> right)
        {
            throw new NotImplementedException();
        }

        public static bool operator >(Money<TCurrency> left, Money<TCurrency> right) => right < left;

        public static bool operator <=(Money<TCurrency> left, Money<TCurrency> right) => left == right || left < right;

        public static bool operator >=(Money<TCurrency> left, Money<TCurrency> right) => left == right || left > right;

        public int CompareTo(object obj)
        {
            throw new NotImplementedException();
        }

        public int CompareTo(Money<TCurrency> other)
        {
            throw new NotImplementedException();
        }
    }
}

using System;

namespace Monies
{
    public sealed partial class Discrete<TCurrency> : IComparable, IComparable<Discrete<TCurrency>>
    {
        public static bool operator <(Discrete<TCurrency> left, Discrete<TCurrency> right)
        {
            throw new NotImplementedException();
        }

        public static bool operator >(Discrete<TCurrency> left, Discrete<TCurrency> right)
        {
            throw new NotImplementedException();
        }

        public static bool operator <=(Discrete<TCurrency> left, Discrete<TCurrency> right)
        {
            throw new NotImplementedException();
        }

        public static bool operator >=(Discrete<TCurrency> left, Discrete<TCurrency> right)
        {
            throw new NotImplementedException();
        }

        public int CompareTo(object obj)
        {
            throw new NotImplementedException();
        }

        public int CompareTo(Discrete<TCurrency> other)
        {
            throw new NotImplementedException();
        }
    }
}

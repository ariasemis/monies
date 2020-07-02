using Monies.Internal;
using System;

namespace Monies
{
    public static class Money
    {
        public static Dense<TCurrency> Dense<TCurrency>(decimal amount, TCurrency currency)
            where TCurrency : IEquatable<TCurrency>
            => new((Rational)amount, currency);
    }
}

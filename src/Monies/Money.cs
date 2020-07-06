using Monies.Internal;
using System;

namespace Monies
{
    public static class Money
    {
        public static Dense<TCurrency> Dense<TCurrency>(decimal amount, TCurrency currency)
            where TCurrency : IEquatable<TCurrency>
            => new((Rational)amount, currency);

        public static Discrete<TCurrency> Discrete<TCurrency>(long amount, TCurrency currency)
            where TCurrency : IEquatable<TCurrency>
            => Discrete(amount, currency, Unit(1, currency));

        public static Discrete<TCurrency> Discrete<TCurrency>(long amount, TCurrency currency, Unit<TCurrency> unit)
            where TCurrency : IEquatable<TCurrency>
            => new(amount, currency, unit);

        public static Unit<TCurrency> Unit<TCurrency>(short scale, TCurrency currency)
            where TCurrency : IEquatable<TCurrency>
            => new(scale, currency);
    }
}

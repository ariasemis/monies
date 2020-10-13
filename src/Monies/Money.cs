using Monies.Internal;
using System;
using System.Diagnostics;

namespace Monies
{
    public static class Money
    {
        [DebuggerStepThrough]
        public static Dense<TCurrency> Dense<TCurrency>(decimal amount, TCurrency currency)
            where TCurrency : IEquatable<TCurrency>
            => new((Rational)amount, currency);

        [DebuggerStepThrough]
        public static Discrete<TCurrency> Discrete<TCurrency>(long amount, TCurrency currency)
            where TCurrency : IEquatable<TCurrency>
            => Discrete(amount, currency, Unit(1, currency));

        [DebuggerStepThrough]
        public static Discrete<TCurrency> Discrete<TCurrency>(long amount, Unit<TCurrency> unit)
            where TCurrency : IEquatable<TCurrency>
            => new(amount, unit == null ? default : unit.Currency, unit);

        [DebuggerStepThrough]
        public static Discrete<TCurrency> Discrete<TCurrency>(long amount, TCurrency currency, Unit<TCurrency> unit)
            where TCurrency : IEquatable<TCurrency>
            => new(amount, currency, unit);

        [DebuggerStepThrough]
        public static Unit<TCurrency> Unit<TCurrency>(short scale, TCurrency currency)
            where TCurrency : IEquatable<TCurrency>
            => new(scale, currency);
    }
}

using System;

namespace Monies
{
    public sealed partial class Discrete<TCurrency>
        where TCurrency : IEquatable<TCurrency>
    {
        internal Discrete(long amount, TCurrency currency, Unit<TCurrency> unit)
        {
            if (currency == null) throw new ArgumentNullException(nameof(currency));
            if (unit == null) throw new ArgumentNullException(nameof(unit));
            if (!unit.Currency.Equals(currency)) throw new ArgumentException("currency mismatch", nameof(unit));

            Amount = amount;
            Currency = currency;
            Unit = unit;
        }

        public long Amount { get; }

        public TCurrency Currency { get; }

        public Unit<TCurrency> Unit { get; }
    }
}

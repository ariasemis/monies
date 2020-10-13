using System;

namespace Monies
{
    public sealed partial class Discrete<TCurrency>
    {
        internal Discrete(long amount, TCurrency? currency, Unit<TCurrency>? unit)
        {
            if (currency == null) throw new ArgumentNullException(nameof(currency));
            if (unit == null) throw new ArgumentNullException(nameof(unit));

            if (!unit.Currency.Equals(currency))
                throw new ArgumentException($"Currency mismatch. Expected: {currency}; Actual: {unit.Currency}", nameof(unit));

            Amount = amount;
            Currency = currency;
            Unit = unit;
        }

        public long Amount { get; }

        public TCurrency Currency { get; }

        public Unit<TCurrency> Unit { get; }
    }
}

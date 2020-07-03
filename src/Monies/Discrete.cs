using System;

namespace Monies
{
    // notes:
    // I added the unit as a type parameter to prevent mixing different units of a same currency when doing calculations.
    // This is because depending on the scale it could lead to fractional values which are not supported by this type.
    // So when mixing units, we need to convert to a Dense type first.
    public sealed partial class Discrete<TCurrency, TUnit>
    {
        internal Discrete(long amount, TCurrency currency, TUnit unit)
        {
            if (currency == null) throw new ArgumentNullException(nameof(currency));
            if (unit == null) throw new ArgumentNullException(nameof(unit));

            Amount = amount;
            Currency = currency;
            Unit = unit;
        }

        public long Amount { get; }

        public TCurrency Currency { get; }

        public TUnit Unit { get; }
    }
}

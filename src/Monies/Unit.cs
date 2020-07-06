using System;

namespace Monies
{
    // TODO: implement equality
    public sealed class Unit<TCurrency>
        where TCurrency : IEquatable<TCurrency>
    {
        internal Unit(short scale, TCurrency currency)
        {
            if (scale < 1) throw new ArgumentOutOfRangeException(nameof(scale));
            if (currency == null) throw new ArgumentNullException(nameof(currency));

            Scale = scale;
            Currency = currency;
        }

        public short Scale { get; }

        public TCurrency Currency { get; }
    }
}

using Monies.Internal;
using System;

namespace Monies
{
    public sealed partial class Dense<TCurrency>
    {
        private readonly Rational amount;

        internal Dense(Rational amount, TCurrency currency)
        {
            if (currency == null)
                throw new ArgumentNullException(nameof(currency));

            this.amount = amount;
            Currency = currency;
        }

        public decimal Amount => (decimal)amount;

        public TCurrency Currency { get; }
    }
}

using System;

namespace Monies
{
    public sealed class Money<TCurrency>
    {
        public Money(decimal amount, TCurrency currency)
        {
            if (currency == null)
                throw new ArgumentNullException(nameof(currency));

            Amount = amount;
            Currency = currency;
        }

        public decimal Amount { get; }

        public TCurrency Currency { get; }
    }
}

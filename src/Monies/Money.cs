﻿using System;

namespace Monies
{
    public static class Money
    {
        public static Money<TCurrency> Create<TCurrency>(decimal amount, TCurrency currency)
            where TCurrency : IEquatable<TCurrency>
            => new Money<TCurrency>(amount, currency);
    }

    public sealed partial class Money<TCurrency>
    {
        internal Money(decimal amount, TCurrency currency)
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
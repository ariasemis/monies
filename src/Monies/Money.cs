using Monies.Internal;
using System;

namespace Monies
{
    public static class Money
    {
        public static Money<TCurrency> Create<TCurrency>(decimal amount, TCurrency currency)
            where TCurrency : IEquatable<TCurrency>
            => new Money<TCurrency>((Rational)amount, currency);
    }

    public sealed partial class Money<TCurrency>
    {
        private readonly Rational amount;

        internal Money(Rational amount, TCurrency currency)
        {
            if (currency == null)
                throw new ArgumentNullException(nameof(currency));

            this.amount = amount;
            Currency = currency;
        }

        public decimal Amount => (decimal)amount;

        public TCurrency Currency { get; }

        public override string ToString()
            => $"{Amount} ({Currency})";

        private static void AssertSameCurrency(Money<TCurrency> left, Money<TCurrency> right)
        {
            if (!left.Currency.Equals(right.Currency))
                throw new InvalidOperationException(
                    $"Cannot compare monies with different currency. Expected: {left.Currency}, Actual: {right.Currency}");
        }
    }
}

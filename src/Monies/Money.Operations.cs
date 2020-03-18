﻿namespace Monies
{
    public partial class Money<TCurrency>
    {
        public static Money<TCurrency> operator -(Money<TCurrency> money)
            => money is null ? null : Money.Create(-money.Amount, money.Currency);

        public static Money<TCurrency> operator +(Money<TCurrency> left, Money<TCurrency> right)
        {
            if (left is null || right is null)
                return null;

            AssertSameCurrency(left, right);

            return Money.Create(left.Amount + right.Amount, left.Currency);
        }

        public Money<TCurrency> Negate() => -this;

        public Money<TCurrency> Add(Money<TCurrency> other) => this + other;
    }
}

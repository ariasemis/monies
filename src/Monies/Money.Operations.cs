using System;

namespace Monies
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

        public static Money<TCurrency> operator -(Money<TCurrency> left, Money<TCurrency> right)
        {
            if (left is null || right is null)
                return null;

            AssertSameCurrency(left, right);

            return Money.Create(left.Amount - right.Amount, left.Currency);
        }

        public static Money<TCurrency> operator *(Money<TCurrency> multiplier, decimal multiplicand)
            => multiplier == null ? null : Money.Create(multiplier.Amount * multiplicand, multiplier.Currency);

        public static Money<TCurrency> operator /(Money<TCurrency> dividend, decimal divisor)
        {
            if (dividend == null)
                return null;

            if (divisor == 0)
                throw new DivideByZeroException();

            return Money.Create(dividend.Amount / divisor, dividend.Currency);
        }

        public Money<TCurrency> Negate() => -this;

        public Money<TCurrency> Add(Money<TCurrency> other) => this + other;

        public Money<TCurrency> Subtract(Money<TCurrency> other) => this - other;

        public Money<TCurrency> Multiply(decimal times) => this * times;

        public Money<TCurrency> Divide(decimal divisor) => this / divisor;
    }
}

using Monies.Internal;
using System;

namespace Monies
{
    public partial class Dense<TCurrency>
    {
        public static Dense<TCurrency>? operator -(Dense<TCurrency>? money)
            => money is null ? null : new Dense<TCurrency>(-money.amount, money.Currency);

        public static Dense<TCurrency>? operator +(Dense<TCurrency>? left, Dense<TCurrency>? right)
        {
            if (left is null || right is null)
                return null;

            AssertSameCurrency(left, right);

            return new Dense<TCurrency>(left.amount + right.amount, left.Currency);
        }

        public static Dense<TCurrency>? operator -(Dense<TCurrency>? left, Dense<TCurrency>? right)
        {
            if (left is null || right is null)
                return null;

            AssertSameCurrency(left, right);

            return new Dense<TCurrency>(left.amount - right.amount, left.Currency);
        }

        public static Dense<TCurrency>? operator *(Dense<TCurrency>? multiplier, decimal multiplicand)
            => multiplier is null ? null : new Dense<TCurrency>(multiplier.amount * (Rational)multiplicand, multiplier.Currency);

        public static Dense<TCurrency>? operator /(Dense<TCurrency>? dividend, decimal divisor)
        {
            if (dividend is null)
                return null;

            if (divisor == 0)
                throw new DivideByZeroException();

            return new Dense<TCurrency>(dividend.amount / (Rational)divisor, dividend.Currency);
        }

        public Dense<TCurrency>? Negate() => -this;

        public Dense<TCurrency>? Add(Dense<TCurrency>? other) => this + other;

        public Dense<TCurrency>? Subtract(Dense<TCurrency>? other) => this - other;

        public Dense<TCurrency>? Multiply(decimal times) => this * times;

        public Dense<TCurrency>? Divide(decimal divisor) => this / divisor;
    }
}

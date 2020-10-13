using System;

namespace Monies
{
    public partial class Dense<TCurrency>
    {
        private static void AssertSameCurrency(Dense<TCurrency> left, Dense<TCurrency> right)
        {
            if (!left.Currency.Equals(right.Currency))
                throw new InvalidOperationException(
                    $"Cannot compare monies with different currency. Expected: {left.Currency}, Actual: {right.Currency}");
        }
    }
}

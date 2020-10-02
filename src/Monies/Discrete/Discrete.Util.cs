using System;

namespace Monies
{
    public sealed partial class Discrete<TCurrency>
    {
        private static void AssertSameCurrency(Discrete<TCurrency> left, Discrete<TCurrency> right)
        {
            if (!left.Currency.Equals(right.Currency))
                throw new InvalidOperationException(
                    $"Cannot compare monies with different currency. Expected: {left.Currency}, Actual: {right.Currency}");
        }
    }
}

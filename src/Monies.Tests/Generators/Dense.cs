using FsCheck;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Monies.Tests.Generators
{
    public static class DenseArbitrary
    {
        public static Arbitrary<Dense<T>> Get<T>() where T : IEquatable<T>
        {
            var gen = from money in DenseGenerators.Generator<T>()
                      where money != null
                      select money;

            return Arb.From(gen, DenseGenerators.Shrinker);
        }
    }

    public static class NullDenseArbitrary
    {
        public static Arbitrary<Dense<T>> Get<T>() where T : IEquatable<T>
            => Arb.From(DenseGenerators.Generator<T>(), DenseGenerators.Shrinker);
    }

    public static class NonZeroDenseArbitrary
    {
        public static Arbitrary<Dense<T>> Get<T>() where T : IEquatable<T>
        {
            var gen = from money in DenseGenerators.Generator<T>()
                      where money != null && money.Amount != 0
                      select money;

            return Arb.From(gen, DenseGenerators.Shrinker);
        }
    }

    public static class DenseGenerators
    {
        public static Gen<Dense<T>> Generator<T>() where T : IEquatable<T>
            => from amount in AmountGenerators.All()
               from currency in Arb.Generate<T>()
               select currency == null ? null : Money.Dense(amount, currency);

        public static IEnumerable<Dense<T>> Shrinker<T>(Dense<T> money) where T : IEquatable<T>
        {
            if (money == null)
                yield break;

            var amount = money.Amount;
            var currency = money.Currency;

            using var ds = Arb.Shrink(money.Amount).GetEnumerator();
            using var ss = Arb.Shrink(money.Currency).GetEnumerator();

            while (ds.MoveNext())
            {
                amount = ds.Current;

                if (ss.MoveNext())
                {
                    currency = ss.Current;
                }

                yield return Money.Dense(amount, currency);
            }
            while (ss.MoveNext())
            {
                yield return Money.Dense(amount, ss.Current);
            }
        }
    }
}

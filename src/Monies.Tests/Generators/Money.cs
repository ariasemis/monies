using FsCheck;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Monies.Tests.Generators
{
    public class MoneyArbitrary
    {
        public static Arbitrary<Money<T>> Get<T>() where T : IEquatable<T>
        {
            var gen = from money in MoneyGenerators.Generator<T>()
                      where money != null
                      select money;

            return Arb.From(gen, MoneyGenerators.Shrinker);
        }
    }

    public class NullMoneyArbitrary
    {
        public static Arbitrary<Money<T>> Get<T>() where T : IEquatable<T>
            => Arb.From(MoneyGenerators.Generator<T>(), MoneyGenerators.Shrinker);
    }

    public class NonZeroMoneyArbitrary
    {
        public static Arbitrary<Money<T>> Get<T>() where T : IEquatable<T>
        {
            var gen = from money in MoneyGenerators.Generator<T>()
                      where money != null && money.Amount != 0
                      select money;

            return Arb.From(gen, MoneyGenerators.Shrinker);
        }
    }

    public class MoneyGenerators
    {
        public static Gen<Money<T>> Generator<T>() where T : IEquatable<T>
            => from amount in AmountGenerators.All()
               from currency in Arb.Generate<T>()
               select currency == null ? null : Money.Create(amount, currency);

        public static IEnumerable<Money<T>> Shrinker<T>(Money<T> money) where T : IEquatable<T>
        {
            if (money == null)
                yield break;

            var amount = money.Amount;
            var currency = money.Currency;

            using (var ds = Arb.Shrink(money.Amount).GetEnumerator())
            using (var ss = Arb.Shrink(money.Currency).GetEnumerator())
            {
                while (ds.MoveNext())
                {
                    amount = ds.Current;

                    if (ss.MoveNext())
                    {
                        currency = ss.Current;
                    }

                    yield return Money.Create(amount, currency);
                }
                while (ss.MoveNext())
                {
                    yield return Money.Create(amount, ss.Current);
                }
            }
        }
    }
}

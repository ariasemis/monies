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

            foreach (var amount in Arb.Shrink(money.Amount))
                yield return Money.Create(amount, money.Currency);

            foreach (var currency in Arb.Shrink(money.Currency))
            {
                if (currency != null)
                {
                    yield return Money.Create(money.Amount, currency);
                }
            }
        }
    }
}

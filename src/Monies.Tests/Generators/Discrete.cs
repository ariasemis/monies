using FsCheck;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Monies.Tests.Generators
{
    public static class DiscreteArbitrary
    {
        public static Arbitrary<Discrete<T>> Get<T>() where T : IEquatable<T>
        {
            var gen = from money in DiscreteGenerators.Generator<T>()
                      where money != null
                      select money;

            return Arb.From(gen, DiscreteGenerators.Shrinker);
        }
    }

    public static class NullDiscreteArbitrary
    {
        public static Arbitrary<Discrete<T>> Get<T>() where T : IEquatable<T>
            => Arb.From(DiscreteGenerators.Generator<T>(), DiscreteGenerators.Shrinker);
    }

    public static class DiscreteGenerators
    {
        public static Gen<Discrete<T>> Generator<T>() where T : IEquatable<T>
            => from amount in Arb.Default.Int64().Generator
               from currency in Arb.Generate<T>()
               from unit in UnitGenerator.Generator(currency)
               select currency == null ? null : Money.Discrete(amount, currency, unit);

        public static IEnumerable<Discrete<T>> Shrinker<T>(Discrete<T> money) where T : IEquatable<T>
        {
            if (money == null)
                yield break;

            var amount = money.Amount;
            var currency = money.Currency;
            var unit = money.Unit;

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

                    yield return Money.Discrete(amount, currency, Money.Unit(unit.Scale, currency));
                }
                while (ss.MoveNext())
                {
                    currency = ss.Current;

                    yield return Money.Discrete(amount, currency, Money.Unit(unit.Scale, currency));
                }
            }
        }
    }
}

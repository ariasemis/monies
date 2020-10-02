using FsCheck;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Monies.Tests.Generators
{
    public class SameCurrencyDiscrete<T> where T : IEquatable<T>
    {
        public SameCurrencyDiscrete(Discrete<T> first, Discrete<T> second, Discrete<T> third)
        {
            if (!first.Currency.Equals(second.Currency) || !first.Currency.Equals(third.Currency))
            {
                throw new ArgumentException("all monies must have the same currency");
            }

            First = first;
            Second = second;
            Third = third;
        }

        public Discrete<T> First { get; }

        public Discrete<T> Second { get; }

        public Discrete<T> Third { get; }

        public override string ToString()
            => $"x: {First}, y: {Second}, z: {Third}";

        public void Deconstruct(out Discrete<T> first)
        {
            first = First;
        }

        public void Deconstruct(out Discrete<T> first, out Discrete<T> second)
        {
            first = First;
            second = Second;
        }

        public void Deconstruct(out Discrete<T> first, out Discrete<T> second, out Discrete<T> third)
        {
            first = First;
            second = Second;
            third = Third;
        }
    }

    public class SameCurrencyDiscreteArbitrary
    {
        public static Arbitrary<SameCurrencyDiscrete<T>> Get<T>() where T : IEquatable<T>
        {
            var gen = from amount1 in Arb.Default.Int64().Generator
                      from amount2 in Arb.Default.Int64().Generator
                      from amount3 in Arb.Default.Int64().Generator
                      from currency in Arb.Generate<T>()
                      from unit1 in UnitGenerator.Generator(currency)
                      from unit2 in UnitGenerator.Generator(currency)
                      from unit3 in UnitGenerator.Generator(currency)
                      where currency != null
                      select new SameCurrencyDiscrete<T>(
                          Money.Discrete(amount1, currency, unit1),
                          Money.Discrete(amount2, currency, unit2),
                          Money.Discrete(amount3, currency, unit3));

            return Arb.From(gen, Shrinker);
        }

        private static IEnumerable<SameCurrencyDiscrete<T>> Shrinker<T>(SameCurrencyDiscrete<T> x) where T : IEquatable<T>
        {
            if (x == null)
                yield break;

            using (var first = DiscreteGenerators.Shrinker(x.First).GetEnumerator())
            using (var second = DiscreteGenerators.Shrinker(x.Second).GetEnumerator())
            using (var third = DiscreteGenerators.Shrinker(x.Third).GetEnumerator())
            {
                while (first.MoveNext()
                    && second.MoveNext()
                    && third.MoveNext())
                {
                    yield return new SameCurrencyDiscrete<T>(
                        first.Current,
                        second.Current,
                        third.Current);
                }
            }
        }
    }
}

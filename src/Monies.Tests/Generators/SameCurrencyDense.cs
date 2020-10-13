using FsCheck;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Monies.Tests.Generators
{
    public class SameCurrencyDense<T> where T : IEquatable<T>
    {
        public SameCurrencyDense(Dense<T> first, Dense<T> second, Dense<T> third)
        {
            if (first == null)
                throw new ArgumentNullException(nameof(first));
            if (second == null)
                throw new ArgumentNullException(nameof(second));
            if (third == null)
                throw new ArgumentNullException(nameof(third));

            if (!first.Currency.Equals(second.Currency) || !first.Currency.Equals(third.Currency))
            {
                throw new ArgumentException("all monies must have the same currency");
            }

            First = first;
            Second = second;
            Third = third;
        }

        public Dense<T> First { get; }

        public Dense<T> Second { get; }

        public Dense<T> Third { get; }

        public override string ToString()
            => $"x: {First}, y: {Second}, z: {Third}";

        public void Deconstruct(out Dense<T> first)
        {
            first = First;
        }

        public void Deconstruct(out Dense<T> first, out Dense<T> second)
        {
            first = First;
            second = Second;
        }

        public void Deconstruct(out Dense<T> first, out Dense<T> second, out Dense<T> third)
        {
            first = First;
            second = Second;
            third = Third;
        }
    }

    public static class SameCurrencyDenseArbitrary
    {
        public static Arbitrary<SameCurrencyDense<T>> Get<T>() where T : IEquatable<T>
        {
            var gen = from amount1 in AmountGenerators.All()
                      from amount2 in AmountGenerators.All()
                      from amount3 in AmountGenerators.All()
                      from currency in Arb.Generate<T>()
                      where currency != null
                      select new SameCurrencyDense<T>(
                          Money.Dense(amount1, currency),
                          Money.Dense(amount2, currency),
                          Money.Dense(amount3, currency));

            return Arb.From(gen, Shrinker);
        }

        private static IEnumerable<SameCurrencyDense<T>> Shrinker<T>(SameCurrencyDense<T> x) where T : IEquatable<T>
        {
            if (x == null)
                yield break;

            using var first = DenseGenerators.Shrinker(x.First).GetEnumerator();
            using var second = DenseGenerators.Shrinker(x.Second).GetEnumerator();
            using var third = DenseGenerators.Shrinker(x.Third).GetEnumerator();

            while (first.MoveNext()
                && second.MoveNext()
                && third.MoveNext())
            {
                yield return new SameCurrencyDense<T>(
                    first.Current,
                    second.Current,
                    third.Current);
            }
        }
    }

}

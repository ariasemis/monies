using FsCheck;
using System;
using System.Linq;

namespace Monies.Tests.Generators
{
    public class SameCurrency<T> where T : IEquatable<T>
    {
        public SameCurrency(Money<T> first, Money<T> second, Money<T> third)
        {
            if (!first.Currency.Equals(second.Currency) || !first.Currency.Equals(third.Currency))
            {
                throw new ArgumentException("all monies must have the same currency");
            }

            First = first;
            Second = second;
            Third = third;
        }

        public Money<T> First { get; }

        public Money<T> Second { get; }

        public Money<T> Third { get; }

        public override string ToString()
            => $"x: {First}, y: {Second}, z: {Third}";

        public void Deconstruct(out Money<T> first)
        {
            first = First;
        }

        public void Deconstruct(out Money<T> first, out Money<T> second)
        {
            first = First;
            second = Second;
        }

        public void Deconstruct(out Money<T> first, out Money<T> second, out Money<T> third)
        {
            first = First;
            second = Second;
            third = Third;
        }
    }

    public class SameCurrencyArbitrary
    {
        public static Arbitrary<SameCurrency<T>> Get<T>() where T : IEquatable<T>
        {
            var gen = from amount1 in Arb.Generate<decimal>()
                      from amount2 in Arb.Generate<decimal>()
                      from amount3 in Arb.Generate<decimal>()
                      from currency in Arb.Generate<T>()
                      where currency != null
                      select new SameCurrency<T>(
                          Money.Create(amount1, currency),
                          Money.Create(amount2, currency),
                          Money.Create(amount3, currency));

            return gen.ToArbitrary();
        }
    }

}

using FsCheck;
using System;
using System.Linq;

namespace Monies.Tests.Generators
{
    public class MoneyArbitrary
    {
        public static Arbitrary<Money<T>> Get<T>() where T : IEquatable<T>
        {
            var gen = from amount in Arb.Generate<decimal>()
                      from currency in Arb.Generate<T>()
                      where currency != null
                      select Money.Create(amount, currency);

            return gen.ToArbitrary();
        }
    }

    public class NullMoneyArbitrary
    {
        public static Arbitrary<Money<T>> Get<T>() where T : IEquatable<T>
        {
            var gen = from amount in Arb.Generate<decimal>()
                      from currency in Arb.Generate<T>()
                      select currency != null ? Money.Create(amount, currency) : null;

            return gen.ToArbitrary();
        }
    }

}

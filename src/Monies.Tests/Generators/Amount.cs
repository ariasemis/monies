using FsCheck;
using System.Linq;

namespace Monies.Tests.Generators
{
    public class AmountArbitrary
    {
        public static Arbitrary<decimal> Get()
            => Arb.From(
                AmountGenerators.All(),
                Arb.Default.Decimal().Shrinker);
    }

    public class AmountGenerators
    {
        // TODO: add support for more scenarios/currencies

        public static Gen<decimal> All()
            => from dollars in Dollars()
               select dollars;

        public static Gen<decimal> Dollars()
            => from low in Gen.Choose(0, 1000)
               from mid in Gen.Choose(0, 100)
               from scale in Gen.Choose(0, 6)
               from sign in Arb.Default.Bool().Generator
               from val in Gen.Elements(new[]
               {
                   new decimal(low, mid, 0, sign, (byte)scale),
                   new decimal(low, 0, 0, sign, (byte)scale)
               })
               select val;
    }
}

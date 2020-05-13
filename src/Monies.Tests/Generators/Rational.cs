using FsCheck;
using Monies.Internal;
using System.Linq;

namespace Monies.Tests.Generators
{
    public class NonZeroDenominatorArbitrary
    {
        public static Arbitrary<Rational> Get()
        {
            var gen = from rational in Arb.Default.Derive<Rational>().Generator
                      where rational.Denominator != 0
                      select rational;
            return Arb.From(gen);
        }
    }
}

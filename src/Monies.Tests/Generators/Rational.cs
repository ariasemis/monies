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

    public class PositiveRational
    {
        public PositiveRational(PositiveInt numerator, PositiveInt denominator)
        {
            Item = new Rational(numerator.Item, denominator.Item);
        }

        public Rational Item { get; }

        public static implicit operator Rational(PositiveRational positiveRational) => positiveRational.Item;

        public override string ToString() => Item.ToString();
    }

    public class NegativeRational
    {
        public NegativeRational(NegativeInt numerator, PositiveInt denominator)
        {
            Item = new Rational(numerator.Item, denominator.Item);
        }

        public Rational Item { get; }

        public static implicit operator Rational(NegativeRational negativeRational) => negativeRational.Item;

        public override string ToString() => Item.ToString();
    }

    public class NonZeroRational
    {
        public NonZeroRational(NonZeroInt numerator, PositiveInt denominator)
        {
            Item = new Rational(numerator.Item, denominator.Item);
        }

        public Rational Item { get; }

        public static implicit operator Rational(NonZeroRational nonZeroRational) => nonZeroRational.Item;

        public override string ToString() => Item.ToString();
    }
}

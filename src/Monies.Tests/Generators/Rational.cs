using FsCheck;
using Monies.Internal;
using System;
using System.Linq;

namespace Monies.Tests.Generators
{
    public static class NonZeroDenominatorArbitrary
    {
        public static Arbitrary<Rational> Get()
        {
            var gen = from rational in Arb.Default.Derive<Rational>().Generator
                      where rational.Denominator != 0
                      select rational;
            return Arb.From(gen);
        }
    }

    public class EquivalentRationalsArbitrary
    {
        public static Arbitrary<RationalSet> Get()
        {
            var gen = from numerator in Arb.Default.Int64().Generator
                      from denominator in Arb.Default.Int64().Generator
                      from factor1 in Arb.Default.Int32().Generator
                      from factor2 in Arb.Default.Int32().Generator
                      select new RationalSet(
                          new Rational(numerator, denominator),
                          new Rational(numerator * factor1, denominator * factor1),
                          new Rational(numerator * factor2, denominator * factor2));

            return Arb.From(gen);
        }
    }

    public class PositiveRational
    {
        public PositiveRational(PositiveInt numerator, PositiveInt denominator)
        {
            if (numerator is null)
                throw new ArgumentNullException(nameof(numerator));

            if (denominator is null)
                throw new ArgumentNullException(nameof(denominator));

            Item = new Rational(numerator.Item, denominator.Item);
        }

        public Rational Item { get; }

        public static implicit operator Rational(PositiveRational positiveRational)
            => positiveRational?.Item ?? Rational.One;

        public override string ToString() => Item.ToString();

        public Rational ToRational() => Item;
    }

    public class NegativeRational
    {
        public NegativeRational(NegativeInt numerator, PositiveInt denominator)
        {
            if (numerator is null)
                throw new ArgumentNullException(nameof(numerator));

            if (denominator is null)
                throw new ArgumentNullException(nameof(denominator));

            Item = new Rational(numerator.Item, denominator.Item);
        }

        public Rational Item { get; }

        public static implicit operator Rational(NegativeRational negativeRational)
            => negativeRational?.Item ?? -Rational.One;

        public override string ToString() => Item.ToString();

        public Rational ToRational() => Item;
    }

    public class NonZeroRational
    {
        public NonZeroRational(NonZeroInt numerator, PositiveInt denominator)
        {
            if (numerator is null)
                throw new ArgumentNullException(nameof(numerator));

            if (denominator is null)
                throw new ArgumentNullException(nameof(denominator));

            Item = new Rational(numerator.Item, denominator.Item);
        }

        public Rational Item { get; }

        public static implicit operator Rational(NonZeroRational nonZeroRational)
            => nonZeroRational?.Item ?? Rational.One;

        public override string ToString() => Item.ToString();

        public Rational ToRational() => Item;
    }

    public class RationalSet
    {
        private readonly Rational x, y, z;

        public RationalSet(Rational x, Rational y, Rational z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public override string ToString()
            => $"x: {x}, y: {y}, z: {z}";

        public void Deconstruct(out Rational x)
        {
            x = this.x;
        }

        public void Deconstruct(out Rational x, out Rational y)
        {
            x = this.x;
            y = this.y;
        }

        public void Deconstruct(out Rational x, out Rational y, out Rational z)
        {
            x = this.x;
            y = this.y;
            z = this.z;
        }
    }
}

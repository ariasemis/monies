using System;

namespace Monies.Internal
{
    public partial struct Rational
    {
        public static explicit operator Rational(decimal value)
        {
            // Based on "Algorithm To Convert A Decimal To A Fraction", written by John Kennedy
            // https://web.archive.org/web/20111027100847/http://homepage.smc.edu/kennedy_john/DEC2FRAC.PDF

            var sign = Math.Sign(value);

            value = Math.Abs(value);

            if (value == Math.Truncate(value))
            {
                return new Rational(value * sign, 1);
            }

            var accuracy = 1E-28m;

            var z = value;
            var previousDenominator = 0m;
            var denominator = 1m;

            decimal numerator;

            do
            {
                z = 1m / (z - Math.Truncate(z));
                var t = denominator;
                denominator = denominator * Math.Truncate(z) + previousDenominator;
                previousDenominator = t;
                numerator = Math.Round(value * denominator);
            }
            while (Math.Abs(value - (numerator / denominator)) > accuracy && z != Math.Truncate(z));

            return new Rational(numerator * sign, denominator);
        }

        public static explicit operator decimal(Rational value)
        {
            if (value.Denominator == 0)
                throw new DivideByZeroException();

            return value.Numerator / value.Denominator;
        }

        public static Rational FromDecimal(decimal value) => (Rational)value;

        public static decimal ToDecimal(Rational value) => (decimal)value;
    }
}

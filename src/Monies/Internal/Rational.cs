using System;

namespace Monies.Internal
{
    public readonly partial struct Rational
    {
        public Rational(long numerator, long denominator)
            : this((decimal)numerator, denominator)
        {
        }

        private Rational(decimal numerator, decimal denominator)
        {
            if (numerator % 1 != 0)
                throw new ArgumentException("must be a whole number", nameof(numerator));
            if (denominator % 1 != 0)
                throw new ArgumentException("must be a whole number", nameof(denominator));

            if (denominator == 0)
            {
                Numerator = Math.Sign(numerator);
                Denominator = 0;
            }
            else
            {
                Numerator = denominator < 0 ? -numerator : numerator;
                Denominator = denominator < 0 ? -denominator : denominator;
            }
        }

        public decimal Numerator { get; }

        public decimal Denominator { get; }

        public readonly int Sign => Math.Sign(Numerator);

        public readonly bool IsInfinity => Denominator == 0 && Numerator != 0;

        public readonly bool IsPositiveInfinity => Denominator == 0 && Numerator > 0;

        public readonly bool IsNegativeInfinity => Denominator == 0 && Numerator < 0;

        public readonly bool IsNaN => Denominator == 0 && Numerator == 0;

        public override readonly string ToString()
        {
            if (IsNaN) return "NaN";

            return Numerator == 0 || Denominator == 1 ?
                $"{Numerator}" : $"{Numerator}/{Denominator}";
        }
    }
}

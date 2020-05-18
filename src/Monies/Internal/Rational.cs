using System;

namespace Monies.Internal
{
    public partial struct Rational
    {
        public Rational(long numerator, long denominator) : this((decimal)numerator, denominator)
        {
        }

        private Rational(decimal numerator, decimal denominator)
        {
            if (numerator % 1 != 0) throw new ArgumentException("must be a whole number", nameof(numerator));
            if (denominator % 1 != 0) throw new ArgumentException("must be a whole number", nameof(denominator));

            Numerator = denominator < 0 ? -numerator : numerator;
            Denominator = denominator < 0 ? -denominator : denominator;
        }

        public decimal Numerator { get; }

        public decimal Denominator { get; }

        public int Sign => Math.Sign(Numerator);

        public bool IsInfinity => IsPositiveInfinity || IsNegativeInfinity;

        public bool IsPositiveInfinity => Denominator == 0 && Numerator > 0;

        public bool IsNegativeInfinity => Denominator == 0 && Numerator < 0;

        public override string ToString()
            => Numerator == 0 || Denominator == 1 ?
            $"{Numerator}" : $"{Numerator}/{Denominator}";
    }
}

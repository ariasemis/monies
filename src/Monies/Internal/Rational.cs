using System;

namespace Monies.Internal
{
    public partial struct Rational
    {
        public Rational(long numerator, long denominator)
        {
            Numerator = denominator < 0 ? -numerator : numerator;
            Denominator = denominator < 0 ? -denominator : denominator;
        }

        public long Numerator { get; }

        public long Denominator { get; }

        public int Sign => Math.Sign(Numerator);

        public bool IsInfinity => IsPositiveInfinity || IsNegativeInfinity;

        public bool IsPositiveInfinity => Denominator == 0 && Numerator > 0;

        public bool IsNegativeInfinity => Denominator == 0 && Numerator < 0;

        public override string ToString()
            => Numerator == 0 || Denominator == 1 ?
            $"{Numerator}" : $"{Numerator}/{Denominator}";
    }
}

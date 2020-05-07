namespace Monies.Internal
{
    public partial struct Rational
    {
        private readonly long numerator, denominator;

        public Rational(long numerator, long denominator)
        {
            this.numerator = numerator;
            this.denominator = denominator;
        }

        public override string ToString()
            => numerator == 0 || denominator == 1 ?
            $"{numerator}" : $"{numerator}/{denominator}";
    }
}

using System;

namespace Monies.Internal
{
    public partial struct Rational
    {
        public static Rational operator -(Rational rational)
            => new Rational(-rational.Numerator, rational.Denominator);

        public static Rational Negate(Rational item) => -item;

    }
}

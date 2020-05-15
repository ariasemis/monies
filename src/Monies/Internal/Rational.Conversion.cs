using System;

namespace Monies.Internal
{
    public partial struct Rational
    {
        public static explicit operator Rational(decimal value)
        {
            throw new NotImplementedException();
        }

        public static explicit operator decimal(Rational value)
        {
            throw new NotImplementedException();
        }

        public static Rational FromDecimal(decimal value) => (Rational)value;

        public static decimal ToDecimal(Rational value) => (decimal)value;
    }
}

using System;

namespace Monies.Internal
{
    public partial struct Rational
    {
        public static Rational operator -(Rational rational)
            => new Rational(-rational.Numerator, rational.Denominator);

        public static Rational operator +(Rational left, Rational right)
        {
            return new Rational(
                left.Numerator * right.Denominator + right.Numerator * left.Denominator,
                left.Denominator * right.Denominator);
        }

        public static Rational operator -(Rational left, Rational right)
        {
            return new Rational(
                left.Numerator * right.Denominator - right.Numerator * left.Denominator,
                left.Denominator * right.Denominator);
        }

        public static Rational operator *(Rational left, Rational right)
        {
            return new Rational(
                left.Numerator * right.Numerator,
                left.Denominator * right.Denominator);
        }

        public static Rational operator /(Rational left, Rational right)
        {
            if (right.Equals(Zero))
                throw new DivideByZeroException();

            return new Rational(
                left.Numerator * right.Denominator,
                right.Numerator * left.Denominator);
        }

        public static Rational Negate(Rational item) => -item;

        public static Rational Add(Rational left, Rational right) => left + right;

        public static Rational Subtract(Rational left, Rational right) => left - right;

        public static Rational Multiply(Rational left, Rational right) => left * right;

        public static Rational Divide(Rational left, Rational right) => left / right;

        public Rational Invert() => new Rational(Denominator, Numerator);
    }
}

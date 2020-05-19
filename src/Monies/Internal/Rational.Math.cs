using System;

namespace Monies.Internal
{
    public partial struct Rational
    {
        public static Rational operator -(Rational rational)
            => new Rational(-rational.Numerator, rational.Denominator);

        public static Rational operator +(Rational left, Rational right)
        {
            if (left.Denominator == 0)
            {
                if (right.Denominator == 0 && left.Sign != right.Sign)
                    return NaN;

                return left;
            }
            if (right.Denominator == 0)
            {
                return right;
            }

            var denominator = MathUtils.LeastCommonMultiple(left.Denominator, right.Denominator);

            var lfactor = denominator / left.Denominator;
            var rfactor = denominator / right.Denominator;

            return new Rational(left.Numerator * lfactor + right.Numerator * rfactor, denominator);
        }

        public static Rational operator -(Rational left, Rational right)
        {
            if (left.Denominator == 0)
            {
                if (right.Denominator == 0 && left.Sign != -right.Sign)
                    return NaN;

                return left;
            }
            if (right.Denominator == 0)
            {
                return -right;
            }

            var denominator = MathUtils.LeastCommonMultiple(left.Denominator, right.Denominator);

            var lfactor = denominator / left.Denominator;
            var rfactor = denominator / right.Denominator;

            return new Rational(left.Numerator * lfactor - right.Numerator * rfactor, denominator);
        }

        public static Rational operator *(Rational left, Rational right)
        {
            var ln = left.Numerator;
            var ld = left.Denominator;
            var rn = right.Numerator;
            var rd = right.Denominator;

            var gcd = MathUtils.GreatestCommonDivisor(ln, rd);
            if (gcd != 0 && gcd != 1)
            {
                ln /= gcd;
                rd /= gcd;
            }

            gcd = MathUtils.GreatestCommonDivisor(rn, ld);
            if (gcd != 0 && gcd != 1)
            {
                rn /= gcd;
                ld /= gcd;
            }

            return new Rational(ln * rn, ld * rd);
        }

        public static Rational operator /(Rational left, Rational right)
        {
            if (right.Equals(Zero))
                throw new DivideByZeroException();

            var ln = left.Numerator;
            var ld = left.Denominator;
            var rn = right.Numerator;
            var rd = right.Denominator;

            var gcd = MathUtils.GreatestCommonDivisor(ln, rn);
            if (gcd != 0 && gcd != 1)
            {
                ln /= gcd;
                rn /= gcd;
            }

            gcd = MathUtils.GreatestCommonDivisor(ld, rd);
            if (gcd != 0 && gcd != 1)
            {
                ld /= gcd;
                rd /= gcd;
            }

            return new Rational(ln * rd, rn * ld);
        }

        public static Rational Negate(Rational item) => -item;

        public static Rational Add(Rational left, Rational right) => left + right;

        public static Rational Subtract(Rational left, Rational right) => left - right;

        public static Rational Multiply(Rational left, Rational right) => left * right;

        public static Rational Divide(Rational left, Rational right) => left / right;

        public Rational Invert() => new Rational(Denominator, Numerator);
    }
}

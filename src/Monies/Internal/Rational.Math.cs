using System;
using System.Numerics;

namespace Monies.Internal
{
    public partial struct Rational
    {
        private static BigInteger MaxDecimal = new BigInteger(decimal.MaxValue);

        private delegate (BigInteger n, BigInteger d) SafeCalculation((BigInteger n, BigInteger d) left, (BigInteger n, BigInteger d) right);

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

            return Perform(left, right, (l, r) => (l.n * r.d + r.n * l.d, l.d * r.d));
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

            return Perform(left, right, (l, r) => (l.n * r.d - r.n * l.d, l.d * r.d));
        }

        public static Rational operator *(Rational left, Rational right)
            => Perform(left, right, (l, r) => (l.n * r.n, l.d * r.d));

        public static Rational operator /(Rational left, Rational right)
        {
            if (right.Equals(Zero))
                throw new DivideByZeroException();

            return Perform(left, right, (l, r) => (l.n * r.d, r.n * l.d));
        }

        public static Rational Negate(Rational item) => -item;

        public static Rational Add(Rational left, Rational right) => left + right;

        public static Rational Subtract(Rational left, Rational right) => left - right;

        public static Rational Multiply(Rational left, Rational right) => left * right;

        public static Rational Divide(Rational left, Rational right) => left / right;

        public Rational Invert() => new Rational(Denominator, Numerator);

        private static Rational Perform(Rational left, Rational right, SafeCalculation op)
        {
            var x = (new BigInteger(left.Numerator), new BigInteger(left.Denominator));
            var y = (new BigInteger(right.Numerator), new BigInteger(right.Denominator));

            var (n, d) = op(x, y);

            var gcd = BigInteger.GreatestCommonDivisor(n, d);
            if (gcd != BigInteger.Zero && gcd != BigInteger.One)
            {
                n /= gcd;
                d /= gcd;
            }

            while (BigInteger.Abs(n) > MaxDecimal && d > 1 && d % 10 == 0)
            {
                n /= 10;
                d /= 10;
            }

            return new Rational((decimal)n, (decimal)d);
        }
    }
}

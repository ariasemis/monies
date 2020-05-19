using System;

namespace Monies.Internal
{
    public static class MathUtils
    {
        public static decimal GreatestCommonDivisor(decimal a, decimal b)
        {
            // see Euclidean algorithm: 
            // https://en.wikipedia.org/wiki/Euclidean_algorithm

            a = Math.Abs(a);
            b = Math.Abs(b);

            while (b != 0)
            {
                var t = b;
                b = a % b;
                a = t;
            }

            return a;
        }

        public static decimal LeastCommonMultiple(decimal a, decimal b)
        {
            var gcd = GreatestCommonDivisor(a, b);
            return a / gcd * b;
        }
    }
}

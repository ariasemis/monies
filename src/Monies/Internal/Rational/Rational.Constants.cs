namespace Monies.Internal
{
    public partial struct Rational
    {
        public static readonly Rational NaN = new();
        public static readonly Rational Zero = new(0, 1);
        public static readonly Rational One = new(1, 1);
    }
}

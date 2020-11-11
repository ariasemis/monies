using FsCheck;
using System;

namespace Monies.Tests.Specs
{
    public static class Equality
    {
        public static bool Reflexivity<T>(T x) where T : IEquatable<T>
            => x.Equals(x); // TODO: should test values rather than references

        public static bool Symmetry<T>(T x, T y) where T : IEquatable<T>
            => x.Equals(y) == y.Equals(x);

        public static Property Transitivity<T>(T x, T y, T z) where T : IEquatable<T>
            => x.Equals(z).When(x.Equals(y) && y.Equals(z));
    }
}

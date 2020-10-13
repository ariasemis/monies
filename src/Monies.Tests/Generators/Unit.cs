using FsCheck;
using System;
using System.Linq;

namespace Monies.Tests.Generators
{
    public static class UnitArbitrary
    {
        public static Arbitrary<Unit<T>> Get<T>() where T : IEquatable<T>
            => UnitGenerator.Generator<T>().ToArbitrary();
    }

    public static class UnitGenerator
    {
        public static Gen<Unit<T>> Generator<T>() where T : IEquatable<T>
            => from scale in Arb.Default.Int16().Generator
               from currency in Arb.Generate<T>()
               where scale >= 1
               select currency == null ? null : Money.Unit(scale, currency);

        public static Gen<Unit<T>> Generator<T>(T currency) where T : IEquatable<T>
            => from scale in Arb.Default.Int16().Generator
               where scale >= 1
               select currency == null ? null : Money.Unit(scale, currency);
    }
}

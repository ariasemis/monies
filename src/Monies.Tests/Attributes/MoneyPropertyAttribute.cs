using FsCheck.Xunit;
using Monies.Tests.Generators;

namespace Monies.Tests.Attributes
{
    public class MoneyPropertyAttribute : PropertyAttribute
    {
        public MoneyPropertyAttribute()
        {
            Arbitrary = new[]
            {
                typeof(DenseArbitrary),
                typeof(SameCurrencyDenseArbitrary),
                typeof(AmountArbitrary),
                typeof(DiscreteArbitrary),
                typeof(UnitArbitrary),
            };
        }
    }

    public class MoneyPropertiesAttribute : PropertiesAttribute
    {
        public MoneyPropertiesAttribute()
        {
            Arbitrary = new[]
            {
                typeof(DenseArbitrary),
                typeof(SameCurrencyDenseArbitrary),
                typeof(AmountArbitrary),
                typeof(DiscreteArbitrary),
                typeof(UnitArbitrary),
            };
        }
    }
}

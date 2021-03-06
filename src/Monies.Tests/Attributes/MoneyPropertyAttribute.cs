﻿using FsCheck.Xunit;
using Monies.Tests.Generators;

namespace Monies.Tests.Attributes
{
    public class MoneyPropertyAttribute : PropertyAttribute
    {
        public MoneyPropertyAttribute()
        {
            Arbitrary = new[]
            {
                typeof(MoneyArbitrary),
                typeof(SameCurrencyArbitrary),
                typeof(AmountArbitrary)
            };
        }
    }

    public class MoneyPropertiesAttribute : PropertiesAttribute
    {
        public MoneyPropertiesAttribute()
        {
            Arbitrary = new[]
            {
                typeof(MoneyArbitrary),
                typeof(SameCurrencyArbitrary),
                typeof(AmountArbitrary)
            };
        }
    }
}

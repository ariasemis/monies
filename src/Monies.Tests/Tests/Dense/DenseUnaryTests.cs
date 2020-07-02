using FsCheck.Xunit;
using Monies.Tests.Attributes;
using System;
using Xunit;

namespace Monies.Tests
{
    [MoneyProperties(QuietOnSuccess = true)]
    public class DenseUnaryTests
    {
        [Property]
        public void Negating_money_returns_money_with_negated_amount(Dense<string> x)
        {
            var abs = Math.Abs(x.Amount);

            Assert.Equal(abs, Math.Abs((-x).Amount));
            Assert.Equal(abs, Math.Abs(x.Negate().Amount));

            var sign = Math.Sign(x.Amount) * -1;

            Assert.Equal(sign, Math.Sign((-x).Amount));
            Assert.Equal(sign, Math.Sign(x.Negate().Amount));
        }

        [Fact]
        public void Negating_null_returns_null()
        {
            Dense<string> x = null;

            Assert.Null(-x);
        }
    }
}

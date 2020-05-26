using System;

namespace Monies
{
    public sealed partial class Money<TCurrency> : IFormattable
    {
        public override string ToString()
            => $"{Amount} ({Currency})";

        public string ToString(string format)
        {
            throw new NotImplementedException();
        }

        public string ToString(IFormatProvider provider)
        {
            throw new NotImplementedException();
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            throw new NotImplementedException();
        }
    }
}

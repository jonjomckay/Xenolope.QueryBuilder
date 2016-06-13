using System;

namespace Xenolope.QueryBuilder
{
    public class StringLiteral
    {
        readonly string value;

        public StringLiteral(string value)
        {
            this.value = value;
        }

        public override string ToString()
        {
            return "'" + this.value + "'";
        }
    }
}


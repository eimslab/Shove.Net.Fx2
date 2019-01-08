namespace Microsoft.International.Converters.PinYinConverter
{
    using System;

    internal class CharUnitPredicate
    {
        private char ExpectedChar;

        internal CharUnitPredicate(char ch)
        {
            this.ExpectedChar = ch;
        }

        internal bool Match(CharUnit charUnit)
        {
            return (charUnit.Char == this.ExpectedChar);
        }
    }
}


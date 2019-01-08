namespace Microsoft.International.Converters.PinYinConverter
{
    using System;

    internal class StrokeUnitPredicate
    {
        private int ExpectedStrokeNum;

        internal StrokeUnitPredicate(int strokeNum)
        {
            this.ExpectedStrokeNum = strokeNum;
        }

        internal bool Match(StrokeUnit strokeUnit)
        {
            return (strokeUnit.StrokeNumber == this.ExpectedStrokeNum);
        }
    }
}


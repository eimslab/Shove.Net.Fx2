namespace Microsoft.International.Converters.PinYinConverter
{
    using System;
    using System.IO;

    internal class StrokeUnit
    {
        internal short CharCount;
        internal char[] CharList;
        internal byte StrokeNumber;

        internal static StrokeUnit Deserialize(BinaryReader binaryReader)
        {
            StrokeUnit unit;
            unit = new StrokeUnit {
                StrokeNumber = binaryReader.ReadByte(),
                CharCount = binaryReader.ReadInt16(),
                CharList = null
            };
            unit.CharList = new char[unit.CharCount];

            for (int i = 0; i < unit.CharCount; i++)
            {
                unit.CharList[i] = binaryReader.ReadChar();
            }
            return unit;
        }

        internal void Serialize(BinaryWriter binaryWriter)
        {
            if (this.CharCount != 0)
            {
                binaryWriter.Write(this.StrokeNumber);
                binaryWriter.Write(this.CharCount);
                binaryWriter.Write(this.CharList);
            }
        }
    }
}


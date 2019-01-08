namespace Microsoft.International.Converters.PinYinConverter
{
    using System;
    using System.IO;

    internal class CharUnit
    {
        internal char Char;
        internal byte PinyinCount;
        internal short[] PinyinIndexList;
        internal byte StrokeNumber;

        internal static CharUnit Deserialize(BinaryReader binaryReader)
        {
            CharUnit unit;
            unit = new CharUnit {
                Char = binaryReader.ReadChar(),
                StrokeNumber = binaryReader.ReadByte(),
                PinyinCount = binaryReader.ReadByte(),
                PinyinIndexList = null
            };
            unit.PinyinIndexList = new short[unit.PinyinCount];

            for (int i = 0; i < unit.PinyinCount; i++)
            {
                unit.PinyinIndexList[i] = binaryReader.ReadInt16();
            }
            return unit;
        }

        internal void Serialize(BinaryWriter binaryWriter)
        {
            binaryWriter.Write(this.Char);
            binaryWriter.Write(this.StrokeNumber);
            binaryWriter.Write(this.PinyinCount);
            for (int i = 0; i < this.PinyinCount; i++)
            {
                binaryWriter.Write(this.PinyinIndexList[i]);
            }
        }
    }
}


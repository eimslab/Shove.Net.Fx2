namespace Microsoft.International.Converters.PinYinConverter
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    internal class StrokeDictionary
    {
        internal int Count;
        internal readonly short EndMark = 0x7fff;
        internal int Length;
        internal const short MaxStrokeNumber = 0x30;
        internal short Offset;
        internal readonly byte[] Reserved = new byte[0x18];
        internal List<StrokeUnit> StrokeUnitTable;

        internal static StrokeDictionary Deserialize(BinaryReader binaryReader)
        {
            StrokeDictionary dictionary = new StrokeDictionary();
            binaryReader.ReadInt32();
            dictionary.Length = binaryReader.ReadInt32();
            dictionary.Count = binaryReader.ReadInt32();
            dictionary.Offset = binaryReader.ReadInt16();
            binaryReader.ReadBytes(0x18);
            dictionary.StrokeUnitTable = new List<StrokeUnit>();
            for (int i = 0; i < dictionary.Count; i++)
            {
                dictionary.StrokeUnitTable.Add(StrokeUnit.Deserialize(binaryReader));
            }
            binaryReader.ReadInt16();
            return dictionary;
        }

        internal StrokeUnit GetStrokeUnit(int strokeNum)
        {
            if ((strokeNum <= 0) || (strokeNum > 0x30))
            {
                throw new ArgumentOutOfRangeException("strokeNum");
            }
            StrokeUnitPredicate predicate = new StrokeUnitPredicate(strokeNum);
            return this.StrokeUnitTable.Find(new Predicate<StrokeUnit>(predicate.Match));
        }

        internal StrokeUnit GetStrokeUnitByIndex(int index)
        {
            if ((index < 0) || (index >= this.Count))
            {
                throw new ArgumentOutOfRangeException("index", AssemblyResource.INDEX_OUT_OF_RANGE);
            }
            return this.StrokeUnitTable[index];
        }

        internal void Serialize(BinaryWriter binaryWriter)
        {
            binaryWriter.Write(this.Length);
            binaryWriter.Write(this.Count);
            binaryWriter.Write(this.Offset);
            binaryWriter.Write(this.Reserved);
            for (int i = 0; i < this.Count; i++)
            {
                this.StrokeUnitTable[i].Serialize(binaryWriter);
            }
            binaryWriter.Write(this.EndMark);
        }
    }
}


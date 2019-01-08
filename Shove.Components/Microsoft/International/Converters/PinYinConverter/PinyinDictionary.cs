namespace Microsoft.International.Converters.PinYinConverter
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    internal class PinyinDictionary
    {
        internal short Count;
        internal readonly short EndMark = 0x7fff;
        internal short Length;
        internal short Offset;
        internal List<PinyinUnit> PinyinUnitTable;
        internal readonly byte[] Reserved = new byte[8];

        internal static PinyinDictionary Deserialize(BinaryReader binaryReader)
        {
            PinyinDictionary dictionary = new PinyinDictionary();
            binaryReader.ReadInt32();
            dictionary.Length = binaryReader.ReadInt16();
            dictionary.Count = binaryReader.ReadInt16();
            dictionary.Offset = binaryReader.ReadInt16();
            binaryReader.ReadBytes(8);
            dictionary.PinyinUnitTable = new List<PinyinUnit>();
            for (int i = 0; i < dictionary.Count; i++)
            {
                dictionary.PinyinUnitTable.Add(PinyinUnit.Deserialize(binaryReader));
            }
            binaryReader.ReadInt16();
            return dictionary;
        }

        internal PinyinUnit GetPinYinUnit(string pinyin)
        {
            PinyinUnitPredicate predicate = new PinyinUnitPredicate(pinyin);
            return this.PinyinUnitTable.Find(new Predicate<PinyinUnit>(predicate.Match));
        }

        internal PinyinUnit GetPinYinUnitByIndex(int index)
        {
            if ((index < 0) || (index >= this.Count))
            {
                throw new ArgumentOutOfRangeException("index", AssemblyResource.INDEX_OUT_OF_RANGE);
            }
            return this.PinyinUnitTable[index];
        }

        internal int GetPinYinUnitIndex(string pinyin)
        {
            PinyinUnitPredicate predicate = new PinyinUnitPredicate(pinyin);
            return this.PinyinUnitTable.FindIndex(new Predicate<PinyinUnit>(predicate.Match));
        }

        internal void Serialize(BinaryWriter binaryWriter)
        {
            binaryWriter.Write(this.Length);
            binaryWriter.Write(this.Count);
            binaryWriter.Write(this.Offset);
            binaryWriter.Write(this.Reserved);
            for (int i = 0; i < this.Count; i++)
            {
                this.PinyinUnitTable[i].Serialize(binaryWriter);
            }
            binaryWriter.Write(this.EndMark);
        }
    }
}


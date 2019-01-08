namespace Microsoft.International.Converters.PinYinConverter
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    internal class CharDictionary
    {
        internal List<CharUnit> CharUnitTable;
        internal int Count;
        internal readonly short EndMark = 0x7fff;
        internal int Length;
        internal short Offset;
        internal readonly byte[] Reserved = new byte[0x18];

        internal static CharDictionary Deserialize(BinaryReader binaryReader)
        {
            CharDictionary dictionary = new CharDictionary();
            binaryReader.ReadInt32();
            dictionary.Length = binaryReader.ReadInt32();
            dictionary.Count = binaryReader.ReadInt32();
            dictionary.Offset = binaryReader.ReadInt16();
            binaryReader.ReadBytes(0x18);
            dictionary.CharUnitTable = new List<CharUnit>();
            for (int i = 0; i < dictionary.Count; i++)
            {
                dictionary.CharUnitTable.Add(CharUnit.Deserialize(binaryReader));
            }
            binaryReader.ReadInt16();
            return dictionary;
        }

        internal CharUnit GetCharUnit(char ch)
        {
            CharUnitPredicate predicate = new CharUnitPredicate(ch);
            return this.CharUnitTable.Find(new Predicate<CharUnit>(predicate.Match));
        }

        internal CharUnit GetCharUnit(int index)
        {
            if ((index < 0) || (index >= this.Count))
            {
                throw new ArgumentOutOfRangeException("index", AssemblyResource.INDEX_OUT_OF_RANGE);
            }
            return this.CharUnitTable[index];
        }

        internal void Serialize(BinaryWriter binaryWriter)
        {
            binaryWriter.Write(this.Length);
            binaryWriter.Write(this.Count);
            binaryWriter.Write(this.Offset);
            binaryWriter.Write(this.Reserved);
            for (int i = 0; i < this.Count; i++)
            {
                this.CharUnitTable[i].Serialize(binaryWriter);
            }
            binaryWriter.Write(this.EndMark);
        }
    }
}


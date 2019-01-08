namespace Microsoft.International.Converters.PinYinConverter
{
    using System;
    using System.Collections.Generic;
    using System.IO;

    internal class HomophoneDictionary
    {
        internal short Count;
        internal readonly short EndMark = 0x7fff;
        internal List<HomophoneUnit> HomophoneUnitTable;
        internal int Length;
        internal short Offset;
        internal readonly byte[] Reserved = new byte[8];

        internal static HomophoneDictionary Deserialize(BinaryReader binaryReader)
        {
            HomophoneDictionary dictionary = new HomophoneDictionary();
            binaryReader.ReadInt32();
            dictionary.Length = binaryReader.ReadInt32();
            dictionary.Count = binaryReader.ReadInt16();
            dictionary.Offset = binaryReader.ReadInt16();
            binaryReader.ReadBytes(8);
            dictionary.HomophoneUnitTable = new List<HomophoneUnit>();
            for (int i = 0; i < dictionary.Count; i++)
            {
                dictionary.HomophoneUnitTable.Add(HomophoneUnit.Deserialize(binaryReader));
            }
            binaryReader.ReadInt16();
            return dictionary;
        }

        internal HomophoneUnit GetHomophoneUnit(int index)
        {
            if ((index < 0) || (index >= this.Count))
            {
                throw new ArgumentOutOfRangeException("index", AssemblyResource.INDEX_OUT_OF_RANGE);
            }
            return this.HomophoneUnitTable[index];
        }

        internal HomophoneUnit GetHomophoneUnit(PinyinDictionary pinyinDictionary, string pinyin)
        {
            return this.GetHomophoneUnit(pinyinDictionary.GetPinYinUnitIndex(pinyin));
        }

        internal void Serialize(BinaryWriter binaryWriter)
        {
            binaryWriter.Write(this.Length);
            binaryWriter.Write(this.Count);
            binaryWriter.Write(this.Offset);
            binaryWriter.Write(this.Reserved);
            for (int i = 0; i < this.Count; i++)
            {
                this.HomophoneUnitTable[i].Serialize(binaryWriter);
            }
            binaryWriter.Write(this.EndMark);
        }
    }
}


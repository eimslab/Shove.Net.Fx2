namespace Microsoft.International.Converters.PinYinConverter
{
    using System;
    using System.IO;

    internal class HomophoneUnit
    {
        internal short Count;
        internal char[] HomophoneList;

        internal static HomophoneUnit Deserialize(BinaryReader binaryReader)
        {
            HomophoneUnit unit;
            unit = new HomophoneUnit {
                Count = binaryReader.ReadInt16(),
                HomophoneList = null
            };
            unit.HomophoneList = new char[unit.Count];

            for (int i = 0; i < unit.Count; i++)
            {
                unit.HomophoneList[i] = binaryReader.ReadChar();
            }
            return unit;
        }

        internal void Serialize(BinaryWriter binaryWriter)
        {
            binaryWriter.Write(this.Count);
            for (int i = 0; i < this.Count; i++)
            {
                binaryWriter.Write(this.HomophoneList[i]);
            }
        }
    }
}


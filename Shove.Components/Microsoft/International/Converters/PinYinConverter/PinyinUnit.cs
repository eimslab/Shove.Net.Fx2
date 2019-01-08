namespace Microsoft.International.Converters.PinYinConverter
{
    using System;
    using System.IO;
    using System.Text;

    internal class PinyinUnit
    {
        internal string Pinyin;

        internal static PinyinUnit Deserialize(BinaryReader binaryReader)
        {
            PinyinUnit unit = new PinyinUnit();
            byte[] bytes = binaryReader.ReadBytes(7);
            unit.Pinyin = Encoding.ASCII.GetString(bytes, 0, 7);
            char[] trimChars = new char[1];
            unit.Pinyin = unit.Pinyin.TrimEnd(trimChars);
            return unit;
        }

        internal void Serialize(BinaryWriter binaryWriter)
        {
            byte[] bytes = new byte[7];
            Encoding.ASCII.GetBytes(this.Pinyin, 0, this.Pinyin.Length, bytes, 0);
            binaryWriter.Write(bytes);
        }
    }
}


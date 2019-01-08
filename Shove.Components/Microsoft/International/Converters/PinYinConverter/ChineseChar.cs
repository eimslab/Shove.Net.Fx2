namespace Microsoft.International.Converters.PinYinConverter
{
    using System;
    using System.Collections.ObjectModel;
    using System.Globalization;
    using System.IO;
    using System.Reflection;
    using System.Resources;

    public class ChineseChar
    {
        private static CharDictionary charDictionary;
        private char chineseCharacter;
        private static HomophoneDictionary homophoneDictionary;
        private bool isPolyphone;
        private const short MaxPolyphoneNum = 8;
        private short pinyinCount;
        private static PinyinDictionary pinyinDictionary;
        private string[] pinyinList = new string[8];
        private static StrokeDictionary strokeDictionary;
        private short strokeNumber;

        static ChineseChar()
        {
            string str;
            byte[] buffer;
            Assembly executingAssembly = Assembly.GetExecutingAssembly();
            using (Stream stream = executingAssembly.GetManifestResourceStream("Shove.Properties.Microsoft.International.Converters.PinYinConverter.PinyinDictionary.resources"))
            {
                using (ResourceReader reader = new ResourceReader(stream))
                {
                    reader.GetResourceData("PinyinDictionary", out str, out buffer);
                    using (BinaryReader reader2 = new BinaryReader(new MemoryStream(buffer)))
                    {
                        pinyinDictionary = PinyinDictionary.Deserialize(reader2);
                    }
                }
            }
            using (Stream stream2 = executingAssembly.GetManifestResourceStream("Shove.Properties.Microsoft.International.Converters.PinYinConverter.CharDictionary.resources"))
            {
                using (ResourceReader reader3 = new ResourceReader(stream2))
                {
                    reader3.GetResourceData("CharDictionary", out str, out buffer);
                    using (BinaryReader reader4 = new BinaryReader(new MemoryStream(buffer)))
                    {
                        charDictionary = CharDictionary.Deserialize(reader4);
                    }
                }
            }
            using (Stream stream3 = executingAssembly.GetManifestResourceStream("Shove.Properties.Microsoft.International.Converters.PinYinConverter.HomophoneDictionary.resources"))
            {
                using (ResourceReader reader5 = new ResourceReader(stream3))
                {
                    reader5.GetResourceData("HomophoneDictionary", out str, out buffer);
                    using (BinaryReader reader6 = new BinaryReader(new MemoryStream(buffer)))
                    {
                        homophoneDictionary = HomophoneDictionary.Deserialize(reader6);
                    }
                }
            }
            using (Stream stream4 = executingAssembly.GetManifestResourceStream("Shove.Properties.Microsoft.International.Converters.PinYinConverter.StrokeDictionary.resources"))
            {
                using (ResourceReader reader7 = new ResourceReader(stream4))
                {
                    reader7.GetResourceData("StrokeDictionary", out str, out buffer);
                    using (BinaryReader reader8 = new BinaryReader(new MemoryStream(buffer)))
                    {
                        strokeDictionary = StrokeDictionary.Deserialize(reader8);
                    }
                }
            }
        }

        public ChineseChar(char ch)
        {
            if (!IsValidChar(ch))
            {
                throw new NotSupportedException(AssemblyResource.CHARACTER_NOT_SUPPORTED);
            }
            this.chineseCharacter = ch;
            CharUnit charUnit = charDictionary.GetCharUnit(ch);
            this.strokeNumber = charUnit.StrokeNumber;
            this.pinyinCount = charUnit.PinyinCount;
            this.isPolyphone = charUnit.PinyinCount > 1;
            for (int i = 0; i < this.pinyinCount; i++)
            {
                PinyinUnit pinYinUnitByIndex = pinyinDictionary.GetPinYinUnitByIndex(charUnit.PinyinIndexList[i]);
                this.pinyinList[i] = pinYinUnitByIndex.Pinyin;
            }
        }

        public int CompareStrokeNumber(char ch)
        {
            CharUnit charUnit = charDictionary.GetCharUnit(ch);
            return (this.StrokeNumber - charUnit.StrokeNumber);
        }

        private static bool ExistSameElement<T>(T[] array1, T[] array2) where T: IComparable
        {
            int index = 0;
            int num2 = 0;
            while ((index < array1.Length) && (num2 < array2.Length))
            {
                if (array1[index].CompareTo(array2[num2]) < 0)
                {
                    index++;
                }
                else
                {
                    if (array1[index].CompareTo(array2[num2]) > 0)
                    {
                        num2++;
                        continue;
                    }
                    return true;
                }
            }
            return false;
        }

        public static short GetCharCount(short strokeNumber)
        {
            if (!IsValidStrokeNumber(strokeNumber))
            {
                return -1;
            }
            return strokeDictionary.GetStrokeUnit(strokeNumber).CharCount;
        }

        public static char[] GetChars(short strokeNumber)
        {
            if (!IsValidStrokeNumber(strokeNumber))
            {
                return null;
            }
            return strokeDictionary.GetStrokeUnit(strokeNumber).CharList;
        }

        public static char[] GetChars(string pinyin)
        {
            if (pinyin == null)
            {
                throw new ArgumentNullException("pinyin");
            }
            if (!IsValidPinyin(pinyin))
            {
                return null;
            }
            return homophoneDictionary.GetHomophoneUnit(pinyinDictionary, pinyin).HomophoneList;
        }

        public static short GetHomophoneCount(string pinyin)
        {
            if (pinyin == null)
            {
                throw new ArgumentNullException("pinyin");
            }
            if (!IsValidPinyin(pinyin))
            {
                return -1;
            }
            return homophoneDictionary.GetHomophoneUnit(pinyinDictionary, pinyin).Count;
        }

        public static short GetStrokeNumber(char ch)
        {
            if (!IsValidChar(ch))
            {
                return -1;
            }
            return charDictionary.GetCharUnit(ch).StrokeNumber;
        }

        public bool HasSound(string pinyin)
        {
            if (pinyin == null)
            {
                throw new ArgumentNullException("HasSound_pinyin");
            }
            for (int i = 0; i < this.PinyinCount; i++)
            {
                if (string.Compare(this.Pinyins[i], pinyin, true, CultureInfo.CurrentCulture) == 0)
                {
                    return true;
                }
            }
            return false;
        }

        public bool IsHomophone(char ch)
        {
            return IsHomophone(this.chineseCharacter, ch);
        }

        public static bool IsHomophone(char ch1, char ch2)
        {
            CharUnit charUnit = charDictionary.GetCharUnit(ch1);
            CharUnit unit2 = charDictionary.GetCharUnit(ch2);
            return ExistSameElement<short>(charUnit.PinyinIndexList, unit2.PinyinIndexList);
        }

        public static bool IsValidChar(char ch)
        {
            return (charDictionary.GetCharUnit(ch) != null);
        }

        public static bool IsValidPinyin(string pinyin)
        {
            if (pinyin == null)
            {
                throw new ArgumentNullException("pinyin");
            }
            if (pinyinDictionary.GetPinYinUnitIndex(pinyin) < 0)
            {
                return false;
            }
            return true;
        }

        public static bool IsValidStrokeNumber(short strokeNumber)
        {
            return (((strokeNumber >= 0) && (strokeNumber <= 0x30)) && (strokeDictionary.GetStrokeUnit(strokeNumber) != null));
        }

        public char ChineseCharacter
        {
            get
            {
                return this.chineseCharacter;
            }
        }

        public bool IsPolyphone
        {
            get
            {
                return this.isPolyphone;
            }
        }

        public short PinyinCount
        {
            get
            {
                return this.pinyinCount;
            }
        }

        public ReadOnlyCollection<string> Pinyins
        {
            get
            {
                return new ReadOnlyCollection<string>(this.pinyinList);
            }
        }

        public short StrokeNumber
        {
            get
            {
                return this.strokeNumber;
            }
        }
    }
}


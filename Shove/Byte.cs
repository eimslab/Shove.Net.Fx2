using System;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;

using ICSharpCode.SharpZipLib;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.Zip.Compression;
using ICSharpCode.SharpZipLib.Checksums;

namespace Shove
{
    /// <summary>
    /// �ַ�����ء�
    /// </summary>
    public class _Byte
    {
        /// <summary>
        /// ���� Byte[] ������ȫ�Ƚ�
        /// </summary>
        /// <param name="input1"></param>
        /// <param name="input2"></param>
        /// <returns></returns>
        public static bool ByteCompare(byte[] input1, byte[] input2)
        {
            if ((input1 == null) || (input2 == null))
            {
                return false;
            }

            if (input1.Length != input2.Length)
            {
                return false;
            }

            for (int i = 0; i < input1.Length; i++)
            {
                if (input1[i] != input2[i])
                {
                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// �� Source ����Ĳ���Ԫ�� CopyTo Destination ������ƶ���ʼλ��
        /// </summary>
        /// <param name="Source"></param>
        /// <param name="StartIndex"></param>
        /// <param name="Count"></param>
        /// <param name="Destination"></param>
        /// <param name="DestinationStartIndex"></param>
        public static void ByteCopy(byte[] Source, int StartIndex, int Count, byte[] Destination, int DestinationStartIndex)
        {
            for (int i = StartIndex; i < StartIndex + Count; i++)
            {
                Destination[DestinationStartIndex + i - StartIndex] = Source[i];
            }
        }

        /// <summary>
        /// �� Buffer ��ָ����λ����ȡһ���µ�������
        /// </summary>
        /// <param name="Buffer"></param>
        /// <param name="Index"></param>
        /// <param name="Count"></param>
        /// <returns></returns>
        public static byte[] ExtractBytesFromBuffer(byte[] Buffer, int Index, int Count)
        {
            if (Buffer == null)
            {
                return null;
            }

            if (Index >= Buffer.Length)
            {
                return null;
            }

            byte[] Result = new byte[Count];

            for (int i = Index; i < Index + Count; i++)
            {
                Result[i - Index] = Buffer[i];
            }

            return Result;
        }

        /// <summary>
        /// �� Buffer ��ָ����λ����ȡһ���ַ���
        /// </summary>
        /// <param name="Buffer"></param>
        /// <param name="Index"></param>
        /// <param name="Count"></param>
        /// <returns></returns>
        public static string ExtractStringFromBuffer(byte[] Buffer, int Index, int Count)
        {
            string str = Encoding.Default.GetString(Buffer, Index, Count);

            while (str.Length > 0 && str[str.Length - 1] == '\0')
            {
                str = str.Remove(str.Length - 1);
            }

            return str;
        }

        /// <summary>
        /// �� Buffer ��ָ����λ����ȡһ�� int
        /// </summary>
        /// <param name="Buffer"></param>
        /// <param name="Index"></param>
        /// <param name="Count"></param>
        /// <returns></returns>
        public static int ExtractIntFromBuffer(byte[] Buffer, int Index, int Count)
        {
            return System.BitConverter.ToInt32(Buffer, Index);
        }

        /// <summary>
        /// �� Buffer ��ָ����λ����ȡһ�� lonog
        /// </summary>
        /// <param name="Buffer"></param>
        /// <param name="Index"></param>
        /// <param name="Count"></param>
        /// <returns></returns>
        public static long ExtractLongFromBuffer(byte[] Buffer, int Index, int Count)
        {
            return System.BitConverter.ToInt64(Buffer, Index);
        }

        /// <summary>
        /// �� Buffer ��ָ����λ����ȡһ�� float
        /// </summary>
        /// <param name="Buffer"></param>
        /// <param name="Index"></param>
        /// <param name="Count"></param>
        /// <returns></returns>
        public static float ExtractFloatFromBuffer(byte[] Buffer, int Index, int Count)
        {
            return System.BitConverter.ToSingle(Buffer, Index);
        }

        /// <summary>
        /// �� Buffer ��ָ����λ����ȡһ�� double
        /// </summary>
        /// <param name="Buffer"></param>
        /// <param name="Index"></param>
        /// <param name="Count"></param>
        /// <returns></returns>
        public static double ExtractDoubleFromBuffer(byte[] Buffer, int Index, int Count)
        {
            return System.BitConverter.ToDouble(Buffer, Index);
        }

        /// <summary>
        /// Byte[] ת����16�����ַ��������� 0x ǰ׺�� _String ���д˷�������������� 0x ǰ׺
        /// </summary>
        /// <param name="Input">input bytes[]</param>
        /// <returns>16�����ַ�����0x......</returns>
        public static string BytesToHexString(byte[] Input)
        {
            string Result = "";

            if (Input.Length == 0)
            {
                return Result;
            }

            foreach (byte b in Input)
            {
                Result += b.ToString("X").PadLeft(2, '0');
            }

            return Result;
        }

        /// <summary>
        /// 16�������ִ�תΪ��ͨ�ַ���
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string HexToString(string input)
        {
            if (String.IsNullOrEmpty(input))
            {
                return "";
            }

            if (input.Length % 2 != 0)
            {
                return "";
            }

            string Result = "";

            for (int i = 0; i < input.Length / 2; i++)
            {
                string str = input.Substring(i * 2, 2);
                byte b = 0;

                try
                {
                    b = Convert.ToByte(str, 16);
                }
                catch
                {
                    return "";
                }

                Result += ((char)b).ToString();
            }

            return Result;
        }

        /// <summary>
        /// 16�������ִ�תΪ Byte ����
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static byte[] HexToBytes(string input)
        {
            if (String.IsNullOrEmpty(input))
            {
                return null;
            }

            if (input.Length % 2 != 0)
            {
                return null;
            }

            byte[] Result = new byte[input.Length / 2];

            for (int i = 0; i < input.Length / 2; i++)
            {
                string str = input.Substring(i * 2, 2);

                byte b = 0;

                try
                {
                    b = Convert.ToByte(str, 16);
                }
                catch
                {
                    return null;
                }

                Result[i] = b;
            }

            return Result;
        }

        /// <summary>
        /// �ַ���תΪ Byte[]
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string StringToHex(string input)
        {
            if (String.IsNullOrEmpty(input))
            {
                return "";
            }

            string Result = "";
            byte[] Buffer = System.Text.Encoding.Default.GetBytes(input);

            foreach (byte b in Buffer)
            {
                Result += String.Format("{0:X}", b);
            }

            return Result;
        }
    }
}

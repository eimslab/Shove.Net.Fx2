using System;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;

using ICSharpCode.SharpZipLib;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.Zip.Compression;
using ICSharpCode.SharpZipLib.Checksums;
using System.Collections.Generic;

namespace Shove
{
    /// <summary>
    /// �ַ�����ء�
    /// </summary>
    public class _String
    {
        /// <summary>
        /// �ַ� ch �� �ַ��� str �г��ֵĴ���
        /// </summary>
        /// <param name="str"></param>
        /// <param name="ch"></param>
        /// <returns></returns>
        public static int StringAt(string str, char ch)
        {
            if (str == null)
                return 0;

            int Result = 0;
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == ch)
                    Result++;
            }

            return Result;
        }

        /// <summary>
        /// �滻�ַ����е�ĳ���ַ�
        /// </summary>
        /// <param name="input"></param>
        /// <param name="ch"></param>
        /// <param name="pos"></param>
        /// <returns></returns>
        public static string ReplaceAt(string input, char ch, int pos)
		{
            return input.Substring(0, pos) + ch.ToString() + ((pos < input.Length) ? input.Substring(pos + 1) : "");
		}

        /// <summary>
        /// ��ת�ַ���
        /// </summary>
        /// <param name="sSourceStr"></param>
        /// <returns></returns>
        public static string Reverse(string sSourceStr)
        {
            StringBuilder sReversed = new StringBuilder();
            for (int i = sSourceStr.Length - 1; i >= 0; i--)
                sReversed.Append(sSourceStr[i]);
            return sReversed.ToString();
        }

        /// <summary>
        /// �ִ���ȡ(���Ǻ���)
        /// </summary>
        /// <param name="Input">Ҫ��ȡ���ַ���</param>
        /// <param name="Length">Ҫ��ȡ���ַ�������</param>
        /// <returns></returns>
        public static string Cut(string Input, int Length)
        {
            if (Length < 0)
            {
                Length = 0;
            }

            Length *= 2;

            if (GetLength(Input) <= Length)
            {
                return Input;
            }

            string Result = "";
            int i = 0;

            while ((GetLength(Result) < Length) && (i < Input.Length))
            {
                Result += Input[i].ToString();

                i++;
            }

            if (Result != Input)
            {
                Result += "..";
            }

            return Result;
        }

        /// <summary>
        /// HTML ��ʽ�ִ���ȡ
        /// </summary>
        /// <param name="Input">Ҫ��ȡ���ַ�</param>
        /// <param name="Length">Ҫ��ȡ���ַ�����</param>
        /// <returns></returns>
        public static string HtmlTextCut(string Input, int Length)
        {
            if (Length < 0)
            {
                Length = 0;
            }

            Length *= 2;

            if (!Input.Contains("<body>"))
            {
                Input = "<body>" + Input;
            }

            Input = HTML.HTML.StandardizationHTML(Input, true, true, true);
            Input = HTML.HTML.GetText(Input, 0);

            return Cut(Input, Length);
        }

        /// <summary>
        /// ���ݳ��Ȳ���ַ���������̺��ֶ���������
        /// </summary>
        /// <param name="Input">������ַ���</param>
        /// <param name="PartLength">ÿ���ֵĳ���</param>
        /// <returns>���ر���ֵĶಿ���ַ�������</returns>
        public static string[] Split(string Input, int PartLength)
        {
            return Split(Input, PartLength, 0);
        }

        /// <summary>
        /// ���ݳ��Ȳ���ַ���������̺��ֶ���������
        /// </summary>
        /// <param name="Input">������ַ���</param>
        /// <param name="PartLength">ÿ���ֵĳ���</param>
        /// <param name="MaxPartNum">���ֻ���ؼ������֣��ұ߶���Ĳ��ֽ�ȡ</param>
        /// <returns>���ر���ֵĶಿ���ַ�������</returns>
        public static string[] Split(string Input, int PartLength, int MaxPartNum)
        {
            if (String.IsNullOrEmpty(Input))
            {
                return null;
            }

            IList<string> list = new List<string>();
            IList<int> list_length = new List<int>();

            list.Add("");
            list_length.Add(0);

            int locate = 0;

            for (int i = 0; i < Input.Length; i++)
            {
                string ch = Input[i].ToString();
                int len = System.Text.Encoding.Default.GetBytes(ch).Length;

                if (list_length[locate] + len > PartLength)
                {
                    locate++;

                    if ((MaxPartNum > 0) && (locate >= MaxPartNum))
                    {
                        break;
                    }

                    list.Add("");
                    list_length.Add(0);
                }

                list[locate] += ch;
                list_length[locate] += len;
            }

            string[] Result = new string[list.Count];
            for (int i = 0; i < list.Count; i++)
            {
                Result[i] = list[i];
            }

            return Result;
        }

        /// <summary>
        /// Byte[] ת����16�����ַ���������� 0x ǰ׺�� _Byte ���д˷�������������� 0x ǰ׺
        /// </summary>
        /// <param name="Input">input bytes[]</param>
        /// <returns>16�����ַ�����0x......</returns>
        public static string BytesToHexString(byte[] Input)
        {
            string Result = "0x";

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
        /// ��⺬�������ַ�����ʵ�ʳ��� ��һ�����ֻ�ȫ���ַ��� 2 ������
        /// </summary> 
        /// <param name="str">�ַ���</param> 
        public static int GetLength(string str)
        {
            //System.Text.ASCIIEncoding n = new System.Text.ASCIIEncoding();
            byte[] bytes = System.Text.ASCIIEncoding.ASCII.GetBytes(str);
            int len = 0; // len Ϊ�ַ���֮ʵ�ʳ��� 
            for (int i = 0; i <= bytes.Length - 1; i++)
            {
                if (bytes[i] == 63) //�ж��Ƿ�Ϊ���ֻ�ȫ�Ƿ��� 
                {
                    len++;
                }

                len++;
            }

            return len;
        }

        /// <summary> 
        /// ��⺬�������ַ�����ʵ�ʳ��ȣ�ʹ��ָ�����ַ���
        /// </summary> 
        /// <param name="str">�ַ���</param> 
        public static int GetBytesLength(string str)
        {
            return GetBytesLength(str, System.Text.Encoding.UTF8);
        }

        /// <summary>
        /// ��⺬�������ַ�����ʵ�ʳ��ȣ�ʹ��ָ�����ַ���
        /// </summary>
        /// <param name="str"></param>
        /// <param name="encoding"></param>
        /// <returns></returns>
        public static int GetBytesLength(string str, System.Text.Encoding encoding)
        {
            byte[] bytes = encoding.GetBytes(str);

            if (bytes != null)
            {
                return bytes.Length;
            }

            return 0;
        }

        /// <summary>
        /// �Ƿ���
        /// </summary>
        public static bool isChineseCharacters(char ch)
        {
            int Unicode = (int)(ch) - 19968;
            return ((Unicode >= 0) && (Unicode <= 20900));
        }

        /// <summary>
        /// �Ƿ�ȫ���ַ�
        /// </summary>
        public static bool isDBCCharacters(char ch)
        {
            int Unicode = (int)(ch);

            return ((Unicode == 12288) || ((Unicode > 65280) && (Unicode < 65375)));
        }

        /// <summary>
        /// ���ַ���ת��Ϊ��׼�ġ���ʶ����
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string StandardizationIdentifier(string str)
        {
            str = str.Trim();
            if (str == "")
                return "CHERY_ADD";

            if ("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz_".IndexOf(str[0].ToString()) < 0)
                str = "CHERY_ADD_" + str;

            int i;
            string StandardizationChars = "1234567890ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz_";
            string d = "";
            for (i = 0; i < str.Length; i++)
            {
                if (StandardizationChars.IndexOf(str[i].ToString()) >= 0)
                    d += str[i].ToString();
            }

            return d;
        }

        /// <summary>
        /// �ַ���ѹ��
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static byte[] Compress(string str)
        {
            byte[] data = System.Text.UnicodeEncoding.Unicode.GetBytes(str);
            Deflater f = new Deflater(Deflater.BEST_COMPRESSION);
            f.SetInput(data);
            f.Finish();

            MemoryStream o = new MemoryStream(data.Length);
            try
            {
                byte[] buf = new byte[1024];
                while (!f.IsFinished)
                {
                    int got = f.Deflate(buf);
                    o.Write(buf, 0, got);
                }
            }
            finally
            {
                o.Close();
            }

            byte[] Result = o.ToArray();
            if ((Result.Length % 2) == 0)
                return Result;

            byte[] Result2 = new byte[Result.Length + 1];
            for (int i = 0; i < Result.Length; i++)
                Result2[i] = Result[i];
            Result2[Result.Length] = 0;
            return Result2;
        }

        /// <summary>
        /// �ַ�����ѹ��
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string Decompress(byte[] data)
        {
            if (data == null)
                return "";
            if (data.Length == 0)
                return "";

            Inflater f = new Inflater();
            f.SetInput(data);

            MemoryStream o = new MemoryStream(data.Length);
            try
            {
                byte[] buf = new byte[1024];
                while (!f.IsFinished)
                {
                    int got = f.Inflate(buf);
                    o.Write(buf, 0, got);
                }
            }
            finally
            {
                o.Close();
            }
            return System.Text.UnicodeEncoding.Unicode.GetString(o.ToArray());
        }

        //������ѹ��\��ѹ���ַ���2������������һ��д��
        /*
        public static byte[] Compress(string s)
        {
            Byte[] pBytes = System.Text.UnicodeEncoding.Unicode.GetBytes(s);

            //����֧���ڴ�洢����
            MemoryStream mMemory = new MemoryStream();
            Deflater mDeflater = new Deflater(ICSharpCode.SharpZipLib.Zip.Compression.Deflater.BEST_COMPRESSION);
            ICSharpCode.SharpZipLib.Zip.Compression.Streams.DeflaterOutputStream mStream = new ICSharpCode.SharpZipLib.Zip.Compression.Streams.DeflaterOutputStream(mMemory,mDeflater,131072);

            mStream.Write(pBytes, 0, pBytes.Length);
            mStream.Close();

            return mMemory.ToArray();
        }

        public static string Decompress(byte[] data)
        {
            ICSharpCode.SharpZipLib.Zip.Compression.Streams.InflaterInputStream mStream = new ICSharpCode.SharpZipLib.Zip.Compression.Streams.InflaterInputStream(new MemoryStream(data));
            
            //����֧���ڴ�洢����
            MemoryStream mMemory = new MemoryStream();
            Int32 mSize;

            Byte[] mWriteData = new Byte[4096];
            while(true)
            {
                mSize = mStream.Read(mWriteData, 0, mWriteData.Length);
                if (mSize > 0)
                {
                    mMemory.Write(mWriteData, 0, mSize);
                }
                else
                {
                    break;
                }
            }

            mStream.Close();
            return System.Text.UnicodeEncoding.Unicode.GetString(mMemory.ToArray());
        }
        */

        /// <summary>
        /// ���ַ���תΪ Base64 ����
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string EncodeBase64(string str)
        {
            string strResult = "";

            if ((str != null) && (str != ""))
            {
                strResult = Convert.ToBase64String(System.Text.ASCIIEncoding.Default.GetBytes(str));
            }

            return strResult;
        }

        /// <summary>
        /// �� Base64 ����תΪ�ַ���
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string DecodeBase64(string str)
        {
            string strResult = "";

            if ((str != null) && (str != ""))
            {
                strResult = System.Text.ASCIIEncoding.Default.GetString(Convert.FromBase64String(str));
            }

            return strResult;
        }

        /// <summary>
        /// Byte[] ת����16�����ַ���
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string BytesToString(byte[] bytes)
        {
            string Result = "0x";

            if (bytes.Length == 0)
            {
                return Result;
            }

            foreach (byte b in bytes)
            {
                Result += b.ToString("X").PadLeft(2, '0');
            }

            return Result;
        }

        /// <summary>
        /// �ַ���ת������
        /// </summary>
        /// <param name="input"></param>
        /// <param name="srcEncoding"></param>
        /// <param name="tarEncoding"></param>
        /// <returns></returns>
        public static string ConvertEncoding(string input, string srcEncoding, string tarEncoding)
        {
            Encoding _srcEncoding = Encoding.GetEncoding(srcEncoding);
            Encoding _tarEncoding = Encoding.GetEncoding(tarEncoding);

            return ConvertEncoding(input, _srcEncoding, _tarEncoding);
        }

        /// <summary>
        /// �ַ���ת������
        /// </summary>
        /// <param name="input"></param>
        /// <param name="srcEncoding"></param>
        /// <param name="tarEncoding"></param>
        /// <returns></returns>
        public static string ConvertEncoding(string input, Encoding srcEncoding, Encoding tarEncoding)
        {
            if (srcEncoding == tarEncoding)
            {
                return input;
            }

            byte[] temp = srcEncoding.GetBytes(input);
            byte[] temp2 = Encoding.Convert(srcEncoding, tarEncoding, temp);

            return tarEncoding.GetString(temp2);
        }

        /// <summary>
        /// У�����
        /// </summary>
        public class Valid
        {
            /// <summary>
            /// У�� Email ��ʽ
            /// </summary>
            /// <param name="Email"></param>
            /// <returns></returns>
            public static bool isEmail(string Email)
            {
                return Regex.IsMatch(Email, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
            }

            /// <summary>
            /// У�����֤��ʽ_��½
            /// </summary>
            /// <param name="IDCardNumber"></param>
            /// <returns></returns>
            public static bool isIDCardNumber(string IDCardNumber)
            {
                return Regex.IsMatch(IDCardNumber, @"(^\d{17}|^\d{14})(\d|x|X|y|Y)$");
            }

            /// <summary>
            /// У�����֤��ʽ_̨��
            /// </summary>
            /// <param name="IDCardNumber"></param>
            /// <returns></returns>
            public static bool isIDCardNumber_Taiwan(string IDCardNumber)
            {
                return Regex.IsMatch(IDCardNumber, @"[A-Za-z][12]\d{8}");
            }

            /// <summary>
            /// У�����֤��ʽ_���
            /// </summary>
            /// <param name="IDCardNumber"></param>
            /// <returns></returns>
            public static bool isIDCardNumber_Hongkong(string IDCardNumber)
            {
                return Regex.IsMatch(IDCardNumber, @"[A-Za-z]{1,2}\d{6}\([Aa0-9]\)");
            }

            /// <summary>
            /// У�����֤��ʽ_�¼���
            /// </summary>
            /// <param name="IDCardNumber"></param>
            /// <returns></returns>
            public static bool isIDCardNumber_Singapore(string IDCardNumber)
            {
                return Regex.IsMatch(IDCardNumber, @"\d{7}[A-JZa-jz]");
            }

            /// <summary>
            /// У�����֤��ʽ_����
            /// </summary>
            /// <param name="IDCardNumber"></param>
            /// <returns></returns>
            public static bool isIDCardNumber_Macau(string IDCardNumber)
            {
                return Regex.IsMatch(IDCardNumber, @"\d{7}\([0-9]\)");
            }

            /// <summary>
            /// У�����п���ʽ
            /// </summary>
            /// <param name="BankCardNumber"></param>
            /// <returns></returns>
            public static bool isBankCardNumber(string BankCardNumber)
            {
                int Valid_BankCardNumberMinLength = _Convert.StrToInt(System.Configuration.ConfigurationManager.AppSettings["Valid_BankCardNumberMinLength"], 12);
                int Valid_BankCardNumberMaxLength = _Convert.StrToInt(System.Configuration.ConfigurationManager.AppSettings["Valid_BankCardNumberMaxLength"], 22);

                if (Valid_BankCardNumberMinLength < 1)
                {
                    Valid_BankCardNumberMinLength = 1;
                }

                if (Valid_BankCardNumberMaxLength < Valid_BankCardNumberMinLength)
                {
                    Valid_BankCardNumberMaxLength = Valid_BankCardNumberMinLength;
                }

                string Pattern = "^\\d{" + Valid_BankCardNumberMinLength.ToString() + "," + Valid_BankCardNumberMaxLength.ToString() + "}$";
                return Regex.IsMatch(BankCardNumber, Pattern);
            }

            /// <summary>
            /// У������ʱ���ʽ
            /// </summary>
            /// <param name="DateTimeString"></param>
            /// <returns></returns>
            public static bool isDateTime(string DateTimeString)
            {
                return Regex.IsMatch(DateTimeString, @"^((\d{2}(([02468][048])|([13579][26]))[\-\/\s]?((((0?[13578])|(1[02]))[\-\/\s]?((0?[1-9])|([1-2][0-9])|(3[01])))|(((0?[469])|(11))[\-\/\s]?((0?[1-9])|([1-2][0-9])|(30)))|(0?2[\-\/\s]?((0?[1-9])|([1-2][0-9])))))|(\d{2}(([02468][1235679])|([13579][01345789]))[\-\/\s]?((((0?[13578])|(1[02]))[\-\/\s]?((0?[1-9])|([1-2][0-9])|(3[01])))|(((0?[469])|(11))[\-\/\s]?((0?[1-9])|([1-2][0-9])|(30)))|(0?2[\-\/\s]?((0?[1-9])|(1[0-9])|(2[0-8]))))))(\s(((0?[1-9])|(1[0-2]))\:([0-5][0-9])((\s)|(\:([0-5][0-9])\s))([AM|PM|am|pm]{2,2})))?$");
            }

            /// <summary>
            /// У�����ڸ�ʽ
            /// </summary>
            /// <param name="DateString"></param>
            /// <returns></returns>
            public static bool isDate(string DateString)
            {
                return Regex.IsMatch(DateString, @"^\d{4}[\-\/\s]?((((0[13578])|(1[02]))[\-\/\s]?(([0-2][0-9])|(3[01])))|(((0[469])|(11))[\-\/\s]?(([0-2][0-9])|(30)))|(02[\-\/\s]?[0-2][0-9]))$");
            }

            /// <summary>
            /// У�� IP ��ַ��ʽ
            /// </summary>
            /// <param name="Address"></param>
            /// <returns></returns>
            public static bool isIPAddress(string Address)
            {
                return Regex.IsMatch(Address, @"^(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])\.(\d{1,2}|1\d\d|2[0-4]\d|25[0-5])$");
            }

            /// <summary>
            /// У��������ʽ����ʽҪ�������� http://  ftp://  https:// �ȵ�ǰ׺
            /// </summary>
            /// <param name="Url"></param>
            /// <returns></returns>
            public static bool isUrl(string Url)
            {
                return isUrl(Url, true);
            }

            /// <summary>
            /// У��������ʽ
            /// </summary>
            /// <param name="Url"></param>
            /// <param name="WithPreFix">�Ƿ���Ҫ�� http:// ftp:// https:// ��ǰ׺</param>
            /// <returns></returns>
            public static bool isUrl(string Url, bool WithPreFix)
            {
                if (WithPreFix)
                {
                    return Regex.IsMatch(Url, @"^(http|ftp|https):\/\/[\w\-_]+(\.[\w\-_]+)+([\w\-\.,@?^=%&:/~\+#]*[\w\-\@?^=%&/~\+#])?$");
                }
                else
                {
                    return Regex.IsMatch(Url, @"^[\w\-_]+(\.[\w\-_]+)+([\w\-\.,@?^=%&:/~\+#]*[\w\-\@?^=%&/~\+#])?$");
                }

            }

            /// <summary>
            /// У���ֻ�����
            /// </summary>
            /// <param name="MobileNumber"></param>
            /// <returns></returns>
            public static bool isMobile(string MobileNumber)
            {
                return Regex.IsMatch(MobileNumber, @"^((13[0-9])|(14[5,7])|(15[0-3,5-9])|(17[0,3,5-8])|(18[0-9])|166|198|199|(147))\d{8}$");
            }
        }
    }
}

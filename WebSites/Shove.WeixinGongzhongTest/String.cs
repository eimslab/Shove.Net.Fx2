using System;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;

namespace Shove
{
    /// <summary>
    /// 字符串相关。
    /// </summary>
    public class _String
    {
        /// <summary> 
        /// 检测含有中文字符串的实际长度 ，一个汉字或全角字符算 2 个长度
        /// </summary> 
        /// <param name="str">字符串</param> 
        public static int GetLength(string str)
        {
            //System.Text.ASCIIEncoding n = new System.Text.ASCIIEncoding();
            byte[] bytes = System.Text.ASCIIEncoding.ASCII.GetBytes(str);
            int len = 0; // len 为字符串之实际长度 
            for (int i = 0; i <= bytes.Length - 1; i++)
            {
                if (bytes[i] == 63) //判断是否为汉字或全角符号 
                {
                    len++;
                }

                len++;
            }

            return len;
        }
    }
}
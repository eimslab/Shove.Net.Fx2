using System;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;

namespace Shove
{
    /// <summary>
    /// �ַ�����ء�
    /// </summary>
    public class _String
    {
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
    }
}
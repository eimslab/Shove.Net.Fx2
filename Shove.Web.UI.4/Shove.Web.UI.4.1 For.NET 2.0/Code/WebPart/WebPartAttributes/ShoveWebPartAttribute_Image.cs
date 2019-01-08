using System;
using System.Collections.Generic;
using System.Text;

namespace Shove.Web.UI
{
    /// <summary>
    /// ͼƬ��������
    /// </summary>
    public class ShoveWebPartAttribute_Image : ShoveWebPartAttribute
    {
        /// <summary>
        /// ����ͼƬ������ʵ��
        /// </summary>
        /// <param name="name">��������</param>
        /// <param name="range">����ȡֵ��Χ</param>
        /// <param name="defaultValue">����Ĭ��ֵ</param>
        /// <param name="vote">����˵��</param>
        public ShoveWebPartAttribute_Image(string name, string range, object defaultValue, string vote)
        {
            Type = "Image";

            Name = name;
            Range = range;
            Value = defaultValue;
            Vote = vote;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Shove.Web.UI
{
    /// <summary>
    /// ͼƬ��������
    /// </summary>
    public class ShovePart2Attribute_Image : ShovePart2Attribute
    {
        /// <summary>
        /// ����ͼƬ������ʵ��
        /// </summary>
        /// <param name="name">��������</param>
        /// <param name="range">����ȡֵ��Χ</param>
        /// <param name="defaultValue">����Ĭ��ֵ</param>
        /// <param name="vote">����˵��</param>
        public ShovePart2Attribute_Image(string name, string range, object defaultValue, string vote)
        {
            Type = "Image";

            Name = name;
            Range = range;
            Value = defaultValue;
            Vote = vote;
        }
    }
}

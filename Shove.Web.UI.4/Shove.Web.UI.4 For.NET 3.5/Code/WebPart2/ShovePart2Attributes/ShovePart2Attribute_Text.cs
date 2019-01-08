using System;
using System.Collections.Generic;
using System.Text;

namespace Shove.Web.UI
{
    /// <summary>
    /// �ı���ǩ��������
    /// </summary>
    public class ShovePart2Attribute_Text : ShovePart2Attribute
    {
        /// <summary>
        /// �����ı���ǩʵ��
        /// </summary>
        /// <param name="name">��������</param>
        /// <param name="range">����ȡֵ��Χ</param>
        /// <param name="defaultValue">����Ĭ��ֵ</param>
        /// <param name="vote">����˵��</param>
        public ShovePart2Attribute_Text(string name, string range, object defaultValue, string vote)
        {
            Type = "Text";

            Name = name;
            Range = range;
            Value = defaultValue;
            Vote = vote;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Shove.Web.UI
{
    /// <summary>
    /// ������������
    /// </summary>
    public class ShovePart2Attribute_Int : ShovePart2Attribute
    {
        /// <summary>
        /// ����������������ʵ��
        /// </summary>
        /// <param name="name">��������</param>
        /// <param name="range">����ȡֵ��Χ</param>
        /// <param name="defaultValue">����Ĭ��ֵ</param>
        /// <param name="vote">����˵��</param>
        public ShovePart2Attribute_Int(string name, string range, object defaultValue, string vote)
        {
            Type = "Int";

            Name = name;
            Range = range;
            Value = defaultValue;
            Vote = vote;
        }
    }
}

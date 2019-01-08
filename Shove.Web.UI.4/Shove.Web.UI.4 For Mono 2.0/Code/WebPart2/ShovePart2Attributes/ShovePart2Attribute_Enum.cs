using System;
using System.Collections.Generic;
using System.Text;

namespace Shove.Web.UI
{
    /// <summary>
    /// �������ͣ�ö�٣�����
    /// </summary>
    public class ShovePart2Attribute_Enum : ShovePart2Attribute
    {
        /// <summary>
        /// ������������(ö��)����ʵ��
        /// </summary>
        /// <param name="name">��������</param>
        /// <param name="range">����ȡֵ��Χ</param>
        /// <param name="defaultValue">����Ĭ��ֵ</param>
        /// <param name="vote">����˵��</param>
        public ShovePart2Attribute_Enum(string name, string range, object defaultValue, string vote)
        {
            Type = "Enum";

            Name = name;
            Range = range;
            Value = defaultValue;
            Vote = vote;
        }
    }
}

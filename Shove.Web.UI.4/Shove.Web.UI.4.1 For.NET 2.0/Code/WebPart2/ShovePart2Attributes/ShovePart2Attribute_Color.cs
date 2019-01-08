using System;
using System.Collections.Generic;
using System.Text;

namespace Shove.Web.UI
{
    /// <summary>
    /// ��ɫ��������
    /// </summary>
    public class ShovePart2Attribute_Color : ShovePart2Attribute
    {
        /// <summary>
        /// ������ɫ��������ʵ��
        /// </summary>
        /// <param name="name">��������</param>
        /// <param name="defaultValue">����Ĭ��ֵ</param>
        /// <param name="vote">����˵��</param>
        public ShovePart2Attribute_Color(string name, object defaultValue, string vote)
        {
            Type = "Color";

            Name = name;
            Value = defaultValue;
            Vote = vote;
        }
    }
}

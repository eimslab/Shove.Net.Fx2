using System;
using System.Collections.Generic;
using System.Text;

namespace Shove.Web.UI
{
    /// <summary>
    /// �Ƿ���ʾ��������
    /// </summary>
    public class ShovePart2Attribute_Boolean : ShovePart2Attribute
    {
        /// <summary>
        /// �����Ƿ���������ʵ��
        /// </summary>
        /// <param name="name">��������</param>
        /// <param name="defaultValue">����Ĭ��ֵ</param>
        /// <param name="vote">����˵��</param>
        public ShovePart2Attribute_Boolean(string name, object defaultValue, string vote)
        {
            Range = "true,false";
            Type = "Bool";

            Name = name;
            Value = defaultValue;
            Vote = vote;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Shove.Web.UI
{
    /// <summary>
    /// ҳ�浼����������
    /// </summary>
    public class ShovePart2Attribute_PageNavigate : ShovePart2Attribute
    {
        /// <summary>
        /// ����ҳ�浼����������
        /// </summary>
        /// <param name="name">��������</param>
        /// <param name="defaultValue">����Ĭ��ֵ</param>
        /// <param name="vote">����˵��</param>
        public ShovePart2Attribute_PageNavigate(string name, object defaultValue, string vote)
        {
            Type = "Page";

            Name = name;
            Value = defaultValue;
            Vote = vote;
        }
    }
}

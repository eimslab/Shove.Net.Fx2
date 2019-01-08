using System;
using System.Collections.Generic;
using System.Text;

namespace Shove.Web.UI
{
    /// <summary>
    /// ������������
    /// </summary>
    public class ShoveWebPartAttribute_Int : ShoveWebPartAttribute
    {
        /// <summary>
        /// ����������������ʵ��
        /// </summary>
        /// <param name="name">��������</param>
        /// <param name="range">����ȡֵ��Χ</param>
        /// <param name="defaultValue">����Ĭ��ֵ</param>
        /// <param name="vote">����˵��</param>
        public ShoveWebPartAttribute_Int(string name, string range, object defaultValue, string vote)
        {
            Type = "Int";

            Name = name;
            Range = range;
            Value = defaultValue;
            Vote = vote;
        }
    }
}

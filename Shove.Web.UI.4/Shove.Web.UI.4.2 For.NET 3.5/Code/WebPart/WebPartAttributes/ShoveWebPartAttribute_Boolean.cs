using System;
using System.Collections.Generic;
using System.Text;

namespace Shove.Web.UI
{
    /// <summary>
    /// �Ƿ���ʾ��������
    /// </summary>
    public class ShoveWebPartAttribute_Boolean : ShoveWebPartAttribute
    {
        /// <summary>
        /// �����Ƿ���������ʵ��
        /// </summary>
        /// <param name="name">��������</param>
        /// <param name="defaultValue">����Ĭ��ֵ</param>
        /// <param name="vote">����˵��</param>
        public ShoveWebPartAttribute_Boolean(string name, object defaultValue, string vote)
        {
            Range = "true,false";
            Type = "Bool";

            Name = name;
            Value = defaultValue;
            Vote = vote;
        }
    }
}

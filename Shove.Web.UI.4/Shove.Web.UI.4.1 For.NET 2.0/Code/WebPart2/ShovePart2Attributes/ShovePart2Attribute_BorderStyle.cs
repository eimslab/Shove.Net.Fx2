using System;
using System.Collections.Generic;
using System.Text;

namespace Shove.Web.UI
{
    /// <summary>
    /// �߿���ɫ����
    /// </summary>
    public class ShovePart2Attribute_BorderStyle : ShovePart2Attribute
    {
        /// <summary>
        /// �����߿���ɫ��������
        /// </summary>
        /// <param name="name">��������</param>
        /// <param name="defaultValue">����Ĭ��ֵ</param>
        /// <param name="vote">����˵��</param>
        public ShovePart2Attribute_BorderStyle(string name, object defaultValue, string vote)
        {
            Type = "Enum";
            Range = "δ����=BNotSet,�ޱ���=BNone,����=BDotted,����=BDashed,ʵ��=BSolid,˫��=BDouble,����=BGroove,͹��=BRidge,���尼��=BInset,����͹��=BOutset";

            Value = defaultValue;
            Name = name;
            Vote = vote;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Shove.Web.UI
{
    /// <summary>
    /// ShoveWebPart�û��ؼ����Ի���
    /// </summary>
    public class ShoveWebPartAttribute
    {
        /// <summary>
        /// ��������
        /// </summary>
        public string Name = "";

        /// <summary>
        /// ����Ĭ��ֵ
        /// </summary>
        public object Value = "";

        /// <summary>
        /// ����ȡֵ��Χ
        /// </summary>
        public string Range = "";

        /// <summary>
        /// ��������
        /// </summary>
        public string Type = "";

        /// <summary>
        /// ����˵��
        /// </summary>
        public string Vote = "";

        /// <summary>
        /// ShoveWebPart�û��ؼ����Ի��๹�캯��
        /// </summary>
        public ShoveWebPartAttribute()
        {
        }
    }
}
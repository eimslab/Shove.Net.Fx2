using System;
using System.Collections.Generic;
using System.Text;

namespace Shove.Web.UI
{
    /// <summary>
    /// ShoveWebPart用户控件属性基类
    /// </summary>
    public class ShoveWebPartAttribute
    {
        /// <summary>
        /// 属性名称
        /// </summary>
        public string Name = "";

        /// <summary>
        /// 属性默认值
        /// </summary>
        public object Value = "";

        /// <summary>
        /// 属性取值范围
        /// </summary>
        public string Range = "";

        /// <summary>
        /// 属性类型
        /// </summary>
        public string Type = "";

        /// <summary>
        /// 属性说明
        /// </summary>
        public string Vote = "";

        /// <summary>
        /// ShoveWebPart用户控件属性基类构造函数
        /// </summary>
        public ShoveWebPartAttribute()
        {
        }
    }
}
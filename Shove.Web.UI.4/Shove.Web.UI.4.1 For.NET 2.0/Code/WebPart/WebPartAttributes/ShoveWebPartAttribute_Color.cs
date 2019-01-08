using System;
using System.Collections.Generic;
using System.Text;

namespace Shove.Web.UI
{
    /// <summary>
    /// 颜色类型属性
    /// </summary>
    public class ShoveWebPartAttribute_Color : ShoveWebPartAttribute
    {
        /// <summary>
        /// 创建颜色类型属性实例
        /// </summary>
        /// <param name="name">属性名称</param>
        /// <param name="defaultValue">属性默认值</param>
        /// <param name="vote">属性说明</param>
        public ShoveWebPartAttribute_Color(string name, object defaultValue, string vote)
        {
            Type = "Color";

            Name = name;
            Value = defaultValue;
            Vote = vote;
        }
    }
}

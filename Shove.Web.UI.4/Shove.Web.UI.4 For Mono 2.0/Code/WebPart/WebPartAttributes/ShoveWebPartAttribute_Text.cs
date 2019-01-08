using System;
using System.Collections.Generic;
using System.Text;

namespace Shove.Web.UI
{
    /// <summary>
    /// 文本标签类型属性
    /// </summary>
    public class ShoveWebPartAttribute_Text : ShoveWebPartAttribute
    {
        /// <summary>
        /// 创建文本标签实例
        /// </summary>
        /// <param name="name">属性名称</param>
        /// <param name="range">属性取值范围</param>
        /// <param name="defaultValue">属性默认值</param>
        /// <param name="vote">属性说明</param>
        public ShoveWebPartAttribute_Text(string name, string range, object defaultValue, string vote)
        {
            Type = "Text";

            Name = name;
            Range = range;
            Value = defaultValue;
            Vote = vote;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Shove.Web.UI
{
    /// <summary>
    /// 是否显示类型属性
    /// </summary>
    public class ShovePart2Attribute_Boolean : ShovePart2Attribute
    {
        /// <summary>
        /// 创建是否类型属性实例
        /// </summary>
        /// <param name="name">属性名称</param>
        /// <param name="defaultValue">属性默认值</param>
        /// <param name="vote">属性说明</param>
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

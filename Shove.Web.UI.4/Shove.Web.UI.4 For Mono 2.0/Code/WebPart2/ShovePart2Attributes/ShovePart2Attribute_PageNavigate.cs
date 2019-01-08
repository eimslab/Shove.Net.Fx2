using System;
using System.Collections.Generic;
using System.Text;

namespace Shove.Web.UI
{
    /// <summary>
    /// 页面导航类型属性
    /// </summary>
    public class ShovePart2Attribute_PageNavigate : ShovePart2Attribute
    {
        /// <summary>
        /// 创建页面导航类型属性
        /// </summary>
        /// <param name="name">属性名称</param>
        /// <param name="defaultValue">属性默认值</param>
        /// <param name="vote">属性说明</param>
        public ShovePart2Attribute_PageNavigate(string name, object defaultValue, string vote)
        {
            Type = "Page";

            Name = name;
            Value = defaultValue;
            Vote = vote;
        }
    }
}

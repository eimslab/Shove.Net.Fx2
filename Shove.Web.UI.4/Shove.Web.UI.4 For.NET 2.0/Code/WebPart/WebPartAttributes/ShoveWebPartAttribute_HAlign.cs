using System;
using System.Collections.Generic;
using System.Text;

namespace Shove.Web.UI
{
    /// <summary>
    /// 水平位置属性
    /// </summary>
    public class ShoveWebPartAttribute_HAlign : ShoveWebPartAttribute
    {
        /// <summary>
        /// 创建水平位置类型属性
        /// </summary>
        /// <param name="name">属性名称</param>
        /// <param name="defaultValue">属性默认值</param>
        /// <param name="vote">属性说明</param>
        public ShoveWebPartAttribute_HAlign(string name, object defaultValue, string vote)
        {
            Type = "Enum";
            Range = "未设置=HNotSet,居左=HLeft,居中=HCenter,居右=HRight";

            Value = defaultValue;
            Name = name;
            Vote = vote;
        }
    }
}

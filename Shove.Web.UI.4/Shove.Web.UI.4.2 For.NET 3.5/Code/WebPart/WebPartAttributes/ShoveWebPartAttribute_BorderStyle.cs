using System;
using System.Collections.Generic;
using System.Text;

namespace Shove.Web.UI
{
    /// <summary>
    /// 边框颜色属性
    /// </summary>
    public class ShoveWebPartAttribute_BorderStyle : ShoveWebPartAttribute
    {
        /// <summary>
        /// 创建边框颜色类型属性
        /// </summary>
        /// <param name="name">属性名称</param>
        /// <param name="defaultValue">属性默认值</param>
        /// <param name="vote">属性说明</param>
        public ShoveWebPartAttribute_BorderStyle(string name, object defaultValue, string vote)
        {
            Type = "Enum";
            Range = "未设置=BNotSet,无边线=BNone,点线=BDotted,虚线=BDashed,实线=BSolid,双线=BDouble,凹下=BGroove,凸起=BRidge,立体凹下=BInset,立体凸起=BOutset";

            Value = defaultValue;
            Name = name;
            Vote = vote;
        }
    }
}

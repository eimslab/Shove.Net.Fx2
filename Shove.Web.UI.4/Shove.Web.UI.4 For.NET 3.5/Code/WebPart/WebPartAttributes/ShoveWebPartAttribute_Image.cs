using System;
using System.Collections.Generic;
using System.Text;

namespace Shove.Web.UI
{
    /// <summary>
    /// 图片类型属性
    /// </summary>
    public class ShoveWebPartAttribute_Image : ShoveWebPartAttribute
    {
        /// <summary>
        /// 创建图片类属性实例
        /// </summary>
        /// <param name="name">属性名称</param>
        /// <param name="range">属性取值范围</param>
        /// <param name="defaultValue">属性默认值</param>
        /// <param name="vote">属性说明</param>
        public ShoveWebPartAttribute_Image(string name, string range, object defaultValue, string vote)
        {
            Type = "Image";

            Name = name;
            Range = range;
            Value = defaultValue;
            Vote = vote;
        }
    }
}

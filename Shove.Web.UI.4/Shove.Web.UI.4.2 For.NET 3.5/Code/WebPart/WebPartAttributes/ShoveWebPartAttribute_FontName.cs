using System;
using System.Collections.Generic;
using System.Text;

namespace Shove.Web.UI
{
    /// <summary>
    /// 字体名称属性
    /// </summary>
    public class ShoveWebPartAttribute_FontName : ShoveWebPartAttribute
    {
        /// <summary>
        /// 创建字体名称属性
        /// </summary>
        /// <param name="name">属性名称</param>
        /// <param name="defaultValue">属性默认值</param>
        /// <param name="vote">属性说明</param>
        public ShoveWebPartAttribute_FontName(string name, object defaultValue, string vote)
        {
            Type = "Enum";
            Range = "宋体,新宋体,幼圆,微软雅黑,隶书,楷体_GB2312,华文中宋,华文行楷,华文新魏,华文宋体,华文隶书,华文楷体,"
            + "华文琥珀,华文仿宋,华文彩云,黑体,仿宋_GB2312,方正姚体,方正舒体";

            Name = name;
            Value = defaultValue;
            vote = Vote;
        }

        /// <summary>
        /// 字体名称
        /// </summary>
        public enum FontName
        {
            宋体,
            新宋体,
            幼圆,
            微软雅黑,
            隶书,
            楷体_GB2312,
            华文中宋,
            华文行楷,
            华文新魏,
            华文宋体,
            华文隶书,
            华文楷体,
            华文琥珀,
            华文仿宋,
            华文彩云,
            黑体,
            仿宋_GB2312,
            方正姚体,
            方正舒体
        }
    }
}

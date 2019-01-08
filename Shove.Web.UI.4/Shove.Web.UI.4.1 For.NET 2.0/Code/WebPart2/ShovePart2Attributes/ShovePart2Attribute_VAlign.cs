using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace Shove.Web.UI
{
    /// <summary>
    /// 垂直位置属性
    /// </summary>
    public class ShovePart2Attribute_VAlign : ShovePart2Attribute
    {
        /// <summary>
        /// 创建垂直位置类型属性
        /// </summary>
        /// <param name="name">属性名称</param>
        /// <param name="defaultValue">属性默认值</param>
        /// <param name="vote">属性说明</param>
        public ShovePart2Attribute_VAlign(string name, object defaultValue, string vote)
        {
            Type = "Enum";
            Range = "未设置=VNotSet,居顶=VTop,居中=VMiddle,居底=VBottom";

            Value = defaultValue;
            Name = name;
            Vote = vote;
        }
    }
}

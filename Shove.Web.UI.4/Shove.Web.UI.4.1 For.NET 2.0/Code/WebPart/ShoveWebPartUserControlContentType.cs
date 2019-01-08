using System;
using System.Collections.Generic;
using System.Text;

namespace Shove.Web.UI
{
    /// <summary>
    ///用户控件根据内容划分的类别
    ///根据控件的内容类别，决定在网站的后台，用那些表编译内容。主要是为了建立控件和后台编辑之间的对应关系。
    /// </summary>
    public class ShoveWebPartUserControlContentType
    {
        // 控件类别 -1 未知类型

        /// <summary>
        /// 未知内容
        /// </summary>
        public const short Unknow = -1;

        public static string BuildTypeString(params short[] Types)
        {
            string Result = "";

            foreach (short Type in Types)
            {
                if (Result != "")
                {
                    Result += ",";
                }
                Result += Type.ToString();
            }

            return Result;
        }
    }
}

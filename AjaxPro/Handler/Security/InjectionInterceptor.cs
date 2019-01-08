//[shove]
/* 为了对 Ajax 的参数进行 SQL、脚本等注入检查，新增了此方法
 * 在 AjaxProcHelper.cs 中 135 行，解析完 Ajax 传递给服务器的参数后，调用了此方法。
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Text.RegularExpressions;
using System.Web.UI;

namespace AjaxPro
{
    /// <summary>
    /// SQL，脚本注入拦截器
    /// 请在 Web.Config 的 <AppSetting> 中，增加 <add key="IsStartAjaxInjectionInterceptor" value="true/false" />
    /// 来控制是否启用此功能，默认为启动
    /// </summary>
    public class InjectionInterceptor
    {
        internal static bool IsStartAjaxInjectionInterceptor = ReadStartStatus();
        private static bool ReadStartStatus()
        {
            bool result = true;
            string str = System.Web.Configuration.WebConfigurationManager.AppSettings.Get("IsStartAjaxInjectionInterceptor");

            if (String.IsNullOrEmpty(str))
            {
                str = "true";
            }

            Boolean.TryParse(str, out result);

            return result;
        }

        private const string rule = @"<[^>]+?style=[\w]+?:expression\(|\b(alert|confirm|prompt)\b|^\+/v(8|9)|<[^>]*?=[^>]*?&#[^>]*?>|\b(and|or)\b.{1,6}?(=|>|<|\bin\b|\blike\b)|/\*.+?\*/|<\s*script\b|\bEXEC\b|UNION.+?SELECT|UPDATE.+?SET|INSERT\s+INTO.+?VALUES|(SELECT|DELETE).+?FROM|(CREATE|ALTER|DROP|TRUNCATE)\s+(TABLE|DATABASE)|[\']+?.*?(OR|AND|[-]{2,}|UPDATE|CREATE|ALTER|DROP|TRUNCATE|SELECT|DELETE|EXEC|INSERT)\b|\b(OR|AND|[-]{2,}|UPDATE|CREATE|ALTER|DROP|TRUNCATE|SELECT|DELETE|EXEC|INSERT)\b.*?[\']+?";
        private static Regex regex = new Regex(rule, RegexOptions.IgnoreCase | RegexOptions.Compiled);

        /// <summary>
        /// 检测 Ajax 参数，如果存在注入，返回 true
        /// 如果配置为不启动 Ajax 注入检查功能，则直接跳过检查，并返回 false
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        internal static bool Check(object[] args)
        {
            if ((!IsStartAjaxInjectionInterceptor) || (args == null) || (args.Length < 1))
            {
                return false;
            }

            foreach (object obj in args)
            {
                if ((obj == null) || (obj.GetType() != typeof(string)))
                {
                    continue;
                }

                if (CheckData(Convert.ToString(obj)))
                {
                    return true;
                }
            }

            return false;
        }

        private static bool CheckData(string value)
        {
            if (String.IsNullOrEmpty(value))
            {
                return false;
            }

            if (regex.IsMatch(value))
            {
                return true;
            }

            return false;
        }
    }
}
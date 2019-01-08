using System;
using System.Collections.Generic;
using System.Text;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.IO;
using System.Web.UI.Design;
using System.ComponentModel;
using System.Data;
using System.Management;
using System.Text.RegularExpressions;

namespace Shove.Web.UI
{
    /// <summary>
    /// 公共类
    /// </summary>
    internal class PublicFunction
    {
        /// <summary>
        /// 获取当前域名
        /// </summary>
        /// <returns></returns>
        public static string GetSiteUrl()
        {
            string SiteUrl = System.Web.HttpContext.Current.Request.Url.AbsoluteUri.Replace(System.Web.HttpContext.Current.Request.Url.PathAndQuery, "") + System.Web.HttpContext.Current.Request.ApplicationPath;

            if (SiteUrl.EndsWith("/"))
            {
                SiteUrl = SiteUrl.Substring(0, SiteUrl.Length - 1);
            }

            return SiteUrl;
        }

        /// <summary>
        /// 获取当前路径的相对于根的相对路径
        /// </summary>
        /// <returns></returns>
        public static string GetCurrentRelativePath()
        {
            string ApplicationPath = System.Web.HttpContext.Current.Request.ApplicationPath;
            string CurrentExecutionFilePath = System.Web.HttpContext.Current.Request.CurrentExecutionFilePath;

            string str = "";

            if (ApplicationPath != "/")
            {
                str = CurrentExecutionFilePath.Replace(ApplicationPath, "");
            }
            else
            {
                str = CurrentExecutionFilePath;
            }

            if (str.StartsWith("/"))
            {
                str = str.Substring(1, str.Length - 1);
            }

            int Count = 0;

            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == '/')
                {
                    Count++;
                }
            }

            string RelativePath = "";

            for (int i = 0; i < Count; i++)
            {
                RelativePath += "../";
            }

            return RelativePath;
        }

        #region Cookie 操作

        /// <summary>
        /// 读取 Cookie
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetCookie(string key)
        {
            string cookievalue = "";
            HttpCookie hc = HttpContext.Current.Request.Cookies.Get(key);
            if (hc != null)
            {
                //cookievalue = Microsoft.JScript.GlobalObject.unescape(hc.Value);
                cookievalue = Microsoft.JScript.GlobalObject.unescape(HttpContext.Current.Server.UrlDecode(hc.Value));
            }
            return cookievalue;
        }

        /// <summary>
        /// 删除 Cookie
        /// </summary>
        /// <param name="key"></param>
        public static void DeleteCookie(string key)
        {
            //HttpContext.Current.Request.Cookies.Remove(key);
            HttpCookie hc = HttpContext.Current.Request.Cookies[key];

            if (hc == null)
            {
                return;
            }

            hc.Value = "";
            hc.Expires = DateTime.Now.AddDays(-10);
            HttpContext.Current.Response.Cookies.Add(hc);
        }

        /// <summary>
        /// 增加 Cookie
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void AddCookie(string key, string value)
        {
            HttpCookie hc = HttpContext.Current.Request.Cookies.Get(key);

            if (hc == null)
            {
                hc = new HttpCookie(key);
                hc.Expires = DateTime.Now.AddDays(1);
                hc.Value = HttpContext.Current.Server.UrlEncode(value);
                HttpContext.Current.Response.Cookies.Add(hc);
            }
            else
            {
                hc.Value = HttpContext.Current.Server.UrlEncode(value);
                hc.Expires = DateTime.Now.AddDays(1);
                HttpContext.Current.Response.Cookies.Add(hc);
            }
        }

        #endregion

        /// <summary>
        /// 从 DataTable 中根据条件删除数据
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="Condition"></param>
        public static void DeleteDataRows(DataTable dt, string Condition)
        {
            if (dt == null)
            {
                return;
            }

            if (Condition == "")
            {
                dt.Rows.Clear();

                return;
            }

            bool CaseSensitive = dt.CaseSensitive;
            dt.CaseSensitive = true;
            DataRow[] rows = dt.Select(Condition);
            dt.CaseSensitive = CaseSensitive;

            foreach (DataRow dr in rows)
            {
                dt.Rows.Remove(dr);
            }
        }

        /// <summary>
        /// 从 DataTable 中根据条件找到行
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="Columns"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        public static int FindRow(DataTable dt, string Columns, object Value)
        {
            if (dt == null)
            {
                return -1;
            }

            if (dt.Rows.Count == 0)
            {
                return -2;
            }

            int i;
            for (i = 0; i < dt.Rows.Count; i++)
            {
                try
                {
                    if (dt.Rows[i][Columns].ToString() == Value.ToString())
                    {
                        return i;
                    }
                }
                catch
                {
                    return -3;
                }
            }

            return -4;
        }

        /// <summary>
        /// 从 DataTable 中根据条件找到行集
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="Condition"></param>
        /// <returns></returns>
        public static DataRow[] Select(DataTable dt, string Condition)
        {
            bool CaseSensitive = dt.CaseSensitive;
            dt.CaseSensitive = true;
            DataRow[] rows = dt.Select(Condition);
            dt.CaseSensitive = CaseSensitive;

            return rows;
        }

        /// <summary>
        /// 判断 DataTable 中是否存在列
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="ColumnName"></param>
        /// <returns></returns>
        public static bool IsExistColumn(DataTable dt, string ColumnName)
        {
            if (dt == null)
            {
                throw new Exception("未将对象引用设置到对象的实例。");
            }

            foreach (DataColumn dc in dt.Columns)
            {
                if (dc.ColumnName.ToLower() == ColumnName.ToLower())
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 获取当前页面的实际文件名
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static string GetPageFileName(HttpContext context)
        {
            return context.Request.ServerVariables["PATH_INFO"].Substring(context.Request.ServerVariables["PATH_INFO"].LastIndexOf("/") + 1);
        }

        # region Web.Config 相关

        /// <summary>
        /// 获取 Web.config 中的 AppSetting 中的配置
        /// </summary>
        /// <param name="Key"></param>
        /// <returns></returns>
        public static string GetWebConfigAppSettingAsString(string Key)
        {
            string Result = System.Web.Configuration.WebConfigurationManager.AppSettings[Key];

            if (Result == null)
            {
                return "";
            }

            return Result.Trim();
        }

        #endregion

        #region 分页相关

        /// <summary>
        /// 数据集分页查询。obj-结果集，table(先要添加列名)-返回的查询结果，currPage-当前页码，pageRows-每页显示行数。
        /// </summary>
        /// <returns></returns>
        public static DataTable DisPage(DataSet obj, DataTable table, int currPage, int pageRows)
        {
            int count = pageRows;

            if (obj.Tables.Count != 0)
            {
                if (currPage == 1)
                {
                    if (obj.Tables[0].Rows.Count < pageRows)
                    {
                        count = obj.Tables[0].Rows.Count;
                    }
                    for (int i = 0; i < count; i++)
                    {
                        DataRow row = table.NewRow();

                        foreach (DataColumn col in table.Columns)
                        {
                            row[col.ColumnName] = obj.Tables[0].Rows[i][col.ColumnName];
                        }
                        table.Rows.Add(row);
                    }
                }
                else
                {
                    if (obj.Tables[0].Rows.Count - (currPage - 1) * pageRows < pageRows)
                    {
                        count = obj.Tables[0].Rows.Count - (currPage - 1) * pageRows;
                    }
                    for (int j = pageRows * (currPage - 1); j < pageRows * (currPage - 1) + count; j++)
                    {
                        DataRow row2 = table.NewRow();

                        foreach (DataColumn col2 in table.Columns)
                        {
                            row2[col2.ColumnName] = obj.Tables[0].Rows[j][col2.ColumnName];
                        }

                        table.Rows.Add(row2);
                    }
                }
            }
            return table;
        }
        /// <summary>
        ///  数据集分页查询。obj-结果集，table(先要添加列名)-返回的查询结果，currPage-当前页码，pageRows-每页显示行数。columns--显示每行显示的列数
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="table"></param>
        /// <param name="currPage"></param>
        /// <param name="pageRows"></param>
        /// <param name="columns"></param>
        /// <returns></returns>
        public static DataTable DisPage(DataSet obj, DataTable table, int currPage, int pageRows, int columns)
        {
            int count = pageRows * columns;

            if (obj.Tables.Count != 0)
            {
                if (currPage == 1)
                {
                    if (obj.Tables[0].Rows.Count < pageRows * columns)
                    {
                        count = obj.Tables[0].Rows.Count;
                    }
                    for (int i = 0; i < count; i++)
                    {
                        DataRow row = table.NewRow();

                        foreach (DataColumn col in table.Columns)
                        {
                            row[col.ColumnName] = obj.Tables[0].Rows[i][col.ColumnName];
                        }

                        table.Rows.Add(row);
                    }
                }
                else
                {
                    if (obj.Tables[0].Rows.Count - (currPage - 1) * pageRows * columns < pageRows * columns)
                    {
                        count = obj.Tables[0].Rows.Count - (currPage - 1) * pageRows * columns;
                    }
                    for (int j = pageRows * columns * (currPage - 1); j < pageRows * columns * (currPage - 1) + count; j++)
                    {
                        DataRow row2 = table.NewRow();

                        foreach (DataColumn col2 in table.Columns)
                        {
                            row2[col2.ColumnName] = obj.Tables[0].Rows[j][col2.ColumnName];
                        }

                        table.Rows.Add(row2);
                    }
                }
            }
            return table;
        }
        /// <summary>
        /// 控制分页按钮的可见性(frist---首页， prev---上一页 ，next---下一页，last---尾页，currPage---当前页，totalPage---总页数) 
        /// </summary>
        /// <param name="frist"></param>
        /// <param name="prev"></param>
        /// <param name="next"></param>
        /// <param name="last"></param>
        /// <param name="currPage"></param>
        /// <param name="totalPage"></param>
        public static void isDisplay(LinkButton frist, LinkButton prev, LinkButton next, LinkButton last, int currPage, int totalPage)
        {
            if (totalPage > 0)
            {
                if (totalPage != 1)
                {
                    if (currPage == 1)
                    {
                        prev.Enabled = false;
                    }
                    else
                    {
                        prev.Enabled = true;
                    }
                    if (currPage == totalPage)
                    {
                        next.Enabled = false;
                    }
                    else
                    {

                        next.Enabled = true;
                    }
                }
                else
                {
                    prev.Enabled = false; next.Enabled = false;
                }
            }
            else
            {
                frist.Enabled = false; prev.Enabled = false; ; next.Enabled = false; last.Enabled = false;
            }
        }

        #endregion

        /// <summary>
        /// 过滤Sql注入，html 编辑器的恶意代码注入
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string FilteSqlInfusion(string input)
        {
            if ((input == null) || (input.Trim() == ""))
            {
                return "";
            }

            double d;
            if (!Double.TryParse(input, out d))
                return input.Replace("'", "’").Replace("update", "ｕｐｄａｔｅ").Replace("drop", "ｄｒｏｐ").Replace("delete", "ｄｅｌｅｔｅ").Replace("exec", "ｅｘｅｃ").Replace("create", "ｃｒｅａｔｅ").Replace("execute", "ｅｘｅｃｕｔｅ").Replace("where", "ｗｈｅｒｅ").Replace("truncate", "ｔｒｕｎｃａｔｅ").Replace("insert", "ｉｎｓｅｒｔ");
            else
                return input;
        }

        #region 转换

        /// <summary>
        /// 从字符串转换到 HorizontalAlign
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static HorizontalAlign HorizontalAlignFromString(string input)
        {
            if (String.IsNullOrEmpty(input) || input.Length < 2)
            {
                return HorizontalAlign.Center;
            }

            if (input.Equals("notset", StringComparison.OrdinalIgnoreCase))
            {
                return HorizontalAlign.NotSet;
            }

            input = input[0].ToString().ToUpper()[0].ToString() + input.Substring(1);

            try
            {
                return (HorizontalAlign)Enum.Parse(typeof(HorizontalAlign), input, true);
            }
            catch
            {
                return HorizontalAlign.Center;
            }
        }

        /// <summary>
        /// 从字符串转换到 VerticalAlign
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static VerticalAlign VerticalAlignFromString(string input)
        {
            if (String.IsNullOrEmpty(input) || input.Length < 2)
            {
                return VerticalAlign.Top;
            }

            if (input.Equals("notset", StringComparison.OrdinalIgnoreCase))
            {
                return VerticalAlign.NotSet;
            }

            input = input[0].ToString().ToUpper()[0].ToString() + input.Substring(1);

            try
            {
                return (VerticalAlign)Enum.Parse(typeof(VerticalAlign), input, true);
            }
            catch
            {
                return VerticalAlign.Top;
            }
        }

        /// <summary>
        /// 从字符串转换到 BorderStyle
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static BorderStyle BorderStyleFromString(string input)
        {
            if (String.IsNullOrEmpty(input) || input.Length < 2)
            {
                return BorderStyle.None;
            }

            if (input.Equals("notset", StringComparison.OrdinalIgnoreCase))
            {
                return BorderStyle.NotSet;
            }

            input = input[0].ToString().ToUpper()[0].ToString() + input.Substring(1);

            try
            {
                return (BorderStyle)Enum.Parse(typeof(BorderStyle), input, true);
            }
            catch
            {
                return BorderStyle.None;
            }
        }

        #endregion
    }

    /// <summary>
    /// 字符串加密、解密函数
    /// </summary>
    internal class Encrypt
    {
        private class ReplaceKey
        {
            public string[,] Key;

            public ReplaceKey()
            {
                Key = new string[2, 52];

                Key[0, 0] = "00"; Key[1, 0] = "E";
                Key[0, 1] = "11"; Key[1, 1] = "M";
                Key[0, 2] = "22"; Key[1, 2] = "Q";
                Key[0, 3] = "33"; Key[1, 3] = "c";
                Key[0, 4] = "44"; Key[1, 4] = "Z";
                Key[0, 5] = "55"; Key[1, 5] = "g";
                Key[0, 6] = "66"; Key[1, 6] = "U";
                Key[0, 7] = "77"; Key[1, 7] = "k";
                Key[0, 8] = "88"; Key[1, 8] = "N";
                Key[0, 9] = "99"; Key[1, 9] = "F";

                Key[0, 10] = "10"; Key[1, 10] = "n";
                Key[0, 11] = "20"; Key[1, 11] = "A";
                Key[0, 12] = "30"; Key[1, 12] = "p";
                Key[0, 13] = "40"; Key[1, 13] = "r";
                Key[0, 14] = "50"; Key[1, 14] = "I";
                Key[0, 15] = "60"; Key[1, 15] = "v";
                Key[0, 16] = "70"; Key[1, 16] = "d";
                Key[0, 17] = "80"; Key[1, 17] = "O";
                Key[0, 18] = "90"; Key[1, 18] = "x";

                Key[0, 19] = "01"; Key[1, 19] = "V";
                Key[0, 20] = "21"; Key[1, 20] = "o";
                Key[0, 21] = "31"; Key[1, 21] = "G";
                Key[0, 22] = "41"; Key[1, 22] = "h";
                Key[0, 23] = "51"; Key[1, 23] = "T";
                Key[0, 24] = "61"; Key[1, 24] = "l";
                Key[0, 25] = "71"; Key[1, 25] = "P";
                Key[0, 26] = "81"; Key[1, 26] = "i";
                Key[0, 27] = "91"; Key[1, 27] = "q";

                Key[0, 28] = "02"; Key[1, 28] = "W";
                Key[0, 29] = "12"; Key[1, 29] = "t";
                Key[0, 30] = "32"; Key[1, 30] = "B";
                Key[0, 31] = "42"; Key[1, 31] = "w";
                Key[0, 32] = "52"; Key[1, 32] = "s";
                Key[0, 33] = "62"; Key[1, 33] = "H";
                Key[0, 34] = "72"; Key[1, 34] = "y";
                Key[0, 35] = "82"; Key[1, 35] = "e";
                Key[0, 36] = "92"; Key[1, 36] = "J";

                Key[0, 37] = "03"; Key[1, 37] = "X";
                Key[0, 38] = "13"; Key[1, 38] = "a";
                Key[0, 39] = "23"; Key[1, 39] = "L";
                Key[0, 40] = "43"; Key[1, 40] = "m";
                Key[0, 41] = "53"; Key[1, 41] = "C";
                Key[0, 42] = "63"; Key[1, 42] = "u";
                Key[0, 43] = "73"; Key[1, 43] = "R";
                Key[0, 44] = "83"; Key[1, 44] = "z";
                Key[0, 45] = "93"; Key[1, 45] = "Y";

                Key[0, 46] = "04"; Key[1, 46] = "S";
                Key[0, 47] = "14"; Key[1, 47] = "K";
                Key[0, 48] = "24"; Key[1, 48] = "f";
                Key[0, 49] = "34"; Key[1, 49] = "D";
                Key[0, 50] = "54"; Key[1, 50] = "b";
                Key[0, 51] = "64"; Key[1, 51] = "j";
            }
        }

        public string EncryptString(string s)
        {
            if (s == "") return "";

            byte[] Byte = System.Text.Encoding.UTF8.GetBytes(s);
            int len = Byte.Length;

            int Key = (System.DateTime.Now.Millisecond + 200) / 2;
            int[] Keys = new int[3];
            Keys[0] = Key / 100;
            Keys[1] = (Key % 100) / 10;
            Keys[2] = (Key % 10);

            string d = Key.ToString().PadLeft(3, '0');

            for (int i = 0; i < len; i++)
            {
                int t = Byte[i];
                t += Keys[i % 3];

                d += t.ToString().PadLeft(3, '0');
            }

            ReplaceKey rk = new ReplaceKey();
            for (int i = 0; i < 52; i++)
                d = d.Replace(rk.Key[0, i], rk.Key[1, i]);
            return "04" + d;
        }

        public string UnEncryptString(string d)
        {
            if ((d == null) || (d == "") || (d.Length < 2) || (!d.StartsWith("04")))
                return "";

            d = d.Substring(2, d.Length - 2);

            int i;
            ReplaceKey rk = new ReplaceKey();
            for (i = 51; i >= 0; i--)
                d = d.Replace(rk.Key[1, i], rk.Key[0, i]);

            int len, Key = int.Parse(d.Substring(0, 3));
            int[] Keys = new int[3];
            Keys[0] = Key / 100;
            Keys[1] = (Key % 100) / 10;
            Keys[2] = (Key % 10);

            d = d.Substring(3, d.Length - 3);
            len = d.Length / 3;

            byte[] Byte = new byte[len];

            for (i = 0; i < len; i++)
                Byte[i] = (byte)(int.Parse(d.Substring(i * 3, 3)) - Keys[i % 3]);

            return System.Text.Encoding.UTF8.GetString(Byte);
        }
    }

    /// <summary>
    /// ControlExt 的摘要说明。
    /// </summary>
    internal class ControlExt
    {
        public ControlExt()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        /// <summary>
        /// 设置下拉文本，return -1：下拉无项目。-2：下拉无此项目
        /// </summary>
        public static int SetDownListBoxText(DropDownList ddl, string Text)
        {
            if (ddl.Items.Count == 0)
                return -1;
            for (int i = 0; i < ddl.Items.Count; i++)
            {
                if (ddl.Items[i].Text.Trim().ToLower() == Text.Trim().ToLower())
                {
                    ddl.SelectedIndex = i;
                    return i;
                }
            }
            return -2;
        }

        /// <summary>
        /// 根据Item的Value设置下拉文本，return -1：下拉无项目。-2：下拉无此项目
        /// </summary>
        public static int SetDownListBoxTextFromValue(DropDownList ddl, string Value)
        {
            if (ddl.Items.Count == 0)
                return -1;
            for (int i = 0; i < ddl.Items.Count; i++)
            {
                if (ddl.Items[i].Value.Trim().ToLower() == Value.Trim().ToLower())
                {
                    ddl.SelectedIndex = i;
                    return i;
                }
            }
            return -2;
        }
    }

    /// <summary>
    /// 文件有关
    /// </summary>
    internal class File
    {
        /// <summary>
        /// 取服务器上 Path 目录下的文件列表
        /// </summary>
        /// <param name="Path">服务器上的绝对路径，调用前用 Server.MapPath 取到完整路径再传入</param>
        /// <param name="ExtNames"></param>
        /// <returns></returns>
        public static string[] GetFileList(string Path, string ExtNames)
        {
            DirectoryInfo di = new DirectoryInfo(Path);

            if (!di.Exists)
            {
                return null;
            }

            FileInfo[] files = di.GetFiles();

            if (files.Length == 0)
            {
                return null;
            }

            ArrayList al = new ArrayList();

            foreach (FileInfo fi in files)
            {
                if ((ExtNames == "") || CompareExtName(fi.FullName, ExtNames))
                {
                    al.Add(fi.FullName);
                }
            }

            if (al.Count < 1)
            {
                return null;
            }

            string[] Result = new string[al.Count];

            for (int i = 0; i < al.Count; i++)
            {
                Result[i] = al[i].ToString();
            }

            return Result;
        }

        /// <summary>
        /// 取服务器上 Path 目录下的文件列表(递归，含所有子目录)
        /// </summary>
        /// <param name="Path">服务器上的绝对路径，调用前用 Server.MapPath 取到完整路径再传入</param>
        /// <param name="ExtNames"></param>
        /// <returns></returns>
        public static string[] GetFileListWithSubDir(string Path, string ExtNames)
        {
            DirectoryInfo di = new DirectoryInfo(Path);
            if (!di.Exists)
            {
                return null;
            }

            ArrayList al = new ArrayList();
            GetFile(Path, ExtNames, al);

            if (al.Count < 1)
            {
                return null;
            }

            string[] Result = new string[al.Count];
            for (int i = 0; i < al.Count; i++)
                Result[i] = al[i].ToString();

            return Result;
        }

        /// <summary>
        /// GetFileListWithSubDir 的辅助递归方法
        /// </summary>
        /// <param name="Dir"></param>
        /// <param name="ExtNames"></param>
        /// <param name="al"></param>
        private static void GetFile(string Dir, string ExtNames, ArrayList al)
        {
            string[] Files = GetFileList(Dir, ExtNames);
            string[] Dirs = Directory.GetDirectories(Dir);

            if (Files != null)
            {
                for (int i = 0; i < Files.Length; i++)
                {
                    al.Add(Files[i]);
                }
            }

            for (int i = 0; i < Dirs.Length; i++)
            {
                GetFile(Dirs[i], ExtNames, al);
            }
        }

        /// <summary>
        /// 比较文件的扩展名是否在允许的范围之内
        /// </summary>
        /// <param name="FileName"></param>
        /// <param name="ExtNames"></param>
        /// <returns></returns>
        private static bool CompareExtName(string FileName, string ExtNames)
        {
            foreach (string ext in ExtNames.Split('|'))
            {
                if (FileName.EndsWith(ext, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <param name="page">输入this.Page即可</param>
        /// <param name="file">file 控件名称</param>
        /// <param name="TargetDirectory">上传服务器上哪个目录(相对目录，如：../Images/)</param>
        /// <param name="ShortFileName">返回一个只有纯文件名的字符串</param>
        /// <param name="OverwriteExistFile">是否覆盖同名文件</param>
        /// <param name="LimitFileTypeList">限制的文件类型列表，如：image, text</param>
        /// <returns>返回：	-1 解析文件错误; -2 OverwriteExistFile = false, 不覆盖已有文件时，文件已经存在; -3 上传错误; 0 OK</returns>
        public static int UploadFile(Page page, HtmlInputFile file, string TargetDirectory, ref string ShortFileName, bool OverwriteExistFile, string LimitFileTypeList)
        {
            if (!ValidFileType(file, LimitFileTypeList))
            {
                return -101;
            }

            string NewFile, NewFileShortName;

            try
            {
                NewFile = file.Value.Trim().Replace("\\", "\\\\");
                NewFileShortName = NewFile.Substring(NewFile.LastIndexOf("\\") + 1, NewFile.Length - NewFile.LastIndexOf("\\") - 1);
                ShortFileName = NewFileShortName;
            }
            catch
            {
                return -1;
            }

            string TargetFileName = page.Server.MapPath(TargetDirectory + NewFileShortName);

            if (System.IO.File.Exists(TargetFileName) && (!OverwriteExistFile))
                return -2;

            try
            {
                file.PostedFile.SaveAs(TargetFileName);
            }
            catch
            {
                return -3;
            }

            return 0;
        }

        /// <summary>
        /// 校验上传的文件类型
        /// </summary>
        /// <param name="file"></param>
        /// <param name="LimitFileTypeList"></param>
        /// <returns></returns>
        private static bool ValidFileType(HtmlInputFile file, string LimitFileTypeList)
        {
            if (String.IsNullOrEmpty(LimitFileTypeList))
            {
                return true;
            }

            string ContentType = file.PostedFile.ContentType.ToLower();

            LimitFileTypeList = LimitFileTypeList.Trim().ToLower();
            string[] strs = LimitFileTypeList.Split(',');

            foreach (string str in strs)
            {
                if (String.IsNullOrEmpty(str))
                {
                    continue;
                }

                string t = str.Trim();

                if (ContentType.IndexOf(t) >= 0)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 校验上传的文件类型
        /// </summary>
        /// <param name="file"></param>
        /// <param name="LimitFileTypeList"></param>
        /// <returns></returns>
        public static bool ValidFileType(FileUpload file, string LimitFileTypeList)
        {
            if (String.IsNullOrEmpty(LimitFileTypeList))
            {
                return true;
            }

            string ContentType = file.PostedFile.ContentType.ToLower();

            LimitFileTypeList = LimitFileTypeList.Trim().ToLower();
            string[] strs = LimitFileTypeList.Split(',');

            foreach (string str in strs)
            {
                if (String.IsNullOrEmpty(str))
                {
                    continue;
                }

                string t = str.Trim();

                if (ContentType.IndexOf(t) >= 0)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="page"></param>
        /// <param name="FileName">完整路径的文件名</param>
        public static void DownloadFile(Page page, string FileName)
        {
            if (!System.IO.File.Exists(FileName))
            {
                JavaScript.Alert(page, "对不起，文件没有找到！");

                return;
            }

            HttpResponse response = page.Response;

            response.AppendHeader("Content-Disposition", "attachment;filename=" + Path.GetFileName(FileName));
            response.ContentType = "application/octet-stream";
            response.WriteFile(FileName);
            response.Flush();
            response.End();
        }
    }

    /// <summary>
    /// 图片 Url 选择框
    /// </summary>
    internal class FlashUrlEditor : ImageUrlEditor
    {
        protected override string Caption
        {
            get
            {
                return "选择 Flash 文件";
            }
        }

        protected override string Filter
        {
            get
            {
                return "Flash 文件(*.swf)|*.swf|所有文件(*.*)|*.*";
            }
        }
    }

    /// <summary>
    /// Xml 格式文件选择框
    /// </summary>
    internal class XmlUrlEditor : ImageUrlEditor
    {
        protected override string Caption
        {
            get
            {
                return "选择 Xml 文件";
            }
        }

        protected override string Filter
        {
            get
            {
                return "Xml 文件(*.xml)|*.xml|所有文件(*.*)|*.*";
            }
        }
    }

    /// <summary>
    /// JavaScript 封装
    /// </summary>
    internal class JavaScript
    {
        public JavaScript()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        #region Alert

        public static void Alert(Page page, string Msg)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), "", "<Script language=javascript>alert('" + Msg + "');</script>");
        }

        public static void Alert(Page page, string Msg, string RedirectUrl)
        {
            Alert(page, Msg, RedirectUrl, "");
        }

        public static void Alert(Page page, string Msg, string RedirectUrl, string FrameName)
        {
            FrameName = FrameName.Trim();

            page.ClientScript.RegisterStartupScript(page.GetType(), "", "<Script language=javascript>alert('" + Msg + "');window." + ((FrameName == "") ? "" : (FrameName + ".")) + "location.replace('" + RedirectUrl + "')</script>");
        }

        #endregion
    }

    /// <summary>
    /// 获取信息信息
    /// </summary>
    internal class SystemInformation
    {
        private string GetWMIInfo(string sInfoType, string sWin32_Database)
        {
            try
            {
                ManagementObjectSearcher searcher = new ManagementObjectSearcher("Select " + sInfoType + " From " + sWin32_Database);
                string sResult = "";

                foreach (ManagementObject mo in searcher.Get())
                {
                    sResult = mo[sInfoType].ToString().Trim();
                }

                return sResult;
            }
            catch
            {
                return "";
            }
        }

        /// <summary>
        /// 获取主板序列号
        /// </summary>
        /// <returns></returns>
        public string GetBIOSSerialNumber()
        {
            return GetWMIInfo("SerialNumber", "Win32_BIOS");
            //return GetWMIInfo("SerialNumber", "Win32_BaseBoard");
        }

        /// <summary>
        /// 获取CPU序列号
        /// </summary>
        /// <returns></returns>
        public string GetCPUSerialNumber()
        {
            return GetWMIInfo("ProcessorId", "Win32_Processor");
        }

        /// <summary>
        /// 获取硬盘序列号
        /// </summary>
        /// <returns></returns>
        public string GetHardDiskSerialNumber()
        {
            return GetWMIInfo("SerialNumber", "Win32_PhysicalMedia");
            //return GetWMIInfo("SerialNumber", "Win32_LogicalDisk");
        }

        /// <summary>
        /// 获取网卡地址
        /// </summary>
        /// <returns></returns>
        public string GetNetCardMACAddress()
        {
            return GetWMIInfo("MACAddress", "Win32_NetworkAdapter WHERE ((MACAddress Is Not NULL) AND (Manufacturer <> 'Microsoft'))");
            //return GetWMIInfo("MACAddress", "Win32_NetworkAdapterConfiguration");
        }
    }
}
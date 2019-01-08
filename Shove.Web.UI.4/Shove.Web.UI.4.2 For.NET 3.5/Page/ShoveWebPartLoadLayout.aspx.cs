using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.IO;
using System.Text.RegularExpressions;
using Shove.Web.UI;

namespace Shove.Web.UI
{
    /// <summary>
    /// ShoveWebPartLoadLayout
    /// </summary>
    public partial class ShoveWebPartLoadLayout : System.Web.UI.Page
    {
        protected System.Web.UI.WebControls.TextBox tbStyle;
        protected System.Web.UI.WebControls.TextBox tbTag;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (HttpContext.Current.Request.QueryString["SiteID"] != null)
                {
                    string SiteID = HttpContext.Current.Request.QueryString["SiteID"].ToString();
                    string PageName = HttpContext.Current.Request.QueryString["PageName"].ToString();

                    this.ViewState["SiteID"] = SiteID;
                    this.ViewState["PageName"] = PageName;
                }

            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnOK_Click(object sender, EventArgs e)
        {
            string css = this.tbStyle.Text.Trim();
            string DIVs = this.tbTag.Text.Trim();

            if (css.Equals("") && DIVs.Equals(""))
            {
                JavaScript.Alert(this.Page, "请输入你要导入的信息");

                return;
            }

            if (!css.Equals(""))
            {
                if (css.IndexOf("ShoveWebPart") > -1)
                {
                    JavaScript.Alert(this.Page, "您输入的样式中含有不合法的字符串\"ShoveWebPart\"");
                    return;
                }
            }

            if (!DIVs.Equals(""))
            {
                if (this.tbTag.Text.Trim().IndexOf("ShoveWebPart") > -1)
                {
                    JavaScript.Alert(this.Page, "您输入的布局标签中含有不合法的字符串\"ShoveWebPart\"");
                    return;
                }

                if (this.tbTag.Text.Trim().IndexOf("container") < 0)
                {
                    JavaScript.Alert(this.Page, "您输入的布局标签中含有不合法的字符串\"ShoveWebPart\"");
                    return;
                }

                //清除所有换行符，使布局的标签之间没有任何的空格和换行字符
                DIVs = DIVs.Replace("\r\n", "").Replace("\t", "");
                string strRegex = @"[\r\n\t]";
                DIVs = RegexReplace(DIVs, strRegex, "");
            }

            string PageName = this.ViewState["PageName"].ToString();
            string SiteID = this.ViewState["SiteID"].ToString();

            try
            {
                //保存布局样式
                if (!css.Equals(""))
                {
                    string FileDir = AppDomain.CurrentDomain.BaseDirectory + "/Private/" + SiteID.ToString() + "/style/" + PageName + ".css";
                    if (!System.IO.Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "/Private/" + SiteID.ToString() + "/style"))
                    {
                        System.IO.Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "/Private/" + SiteID.ToString() + "/style");
                    }
                    System.IO.File.WriteAllText(FileDir, css);

                    if (!System.IO.File.Exists(FileDir))
                    {
                        FileStream fs = System.IO.File.Create(FileDir);
                        fs.Close();
                    }
                }

                //保存布局的标签
                if (!DIVs.Equals(""))
                {
                    string DivFileDir = AppDomain.CurrentDomain.BaseDirectory + "/Private/" + SiteID.ToString() + "/Layout/" + PageName + ".xml";
                    if (!System.IO.Directory.Exists(AppDomain.CurrentDomain.BaseDirectory + "/Private/" + SiteID.ToString() + "/Layout"))
                    {
                        System.IO.Directory.CreateDirectory(AppDomain.CurrentDomain.BaseDirectory + "/Private/" + SiteID.ToString() + "/Layout");
                    }
                    System.IO.File.WriteAllText(DivFileDir, DIVs);

                    //导入布局，判断PageCaches文件夹中的布局文件aspx是否存在 如果不存在就创建文件aspx，如果存在就取出Part重新创建文件     将布局文件写入Cookie
                    PublicFunction.AddCookie("ShoveWebPageLayout_" + SiteID.ToString() + "_" + PageName, DIVs);
                }

                string ShoveWebPageLayoutINI = PublicFunction.GetCookie("cooldrag_" + SiteID.ToString() + "_" + PageName);

                string[] str_cooldragINIList = ShoveWebPageLayoutINI.Split('|');
                bool containerFlag;
                string str = "";

                Regex regex = new Regex(@"id=""(?<L0>[^""]+)""", RegexOptions.Compiled | RegexOptions.IgnoreCase);
                for (Match m = regex.Match(DIVs); m.Success; m = m.NextMatch())
                {
                    containerFlag = false;

                    if (m.Groups["L0"].Value != "" && m.Groups["L0"].Value.IndexOf("container") > -1)
                    {
                        for (int j = 0; j < str_cooldragINIList.Length; j++)
                        {
                            if (m.Groups["L0"].Value.ToLower() == str_cooldragINIList[j].Split(':')[0].ToLower())
                            {
                                str += "|" + str_cooldragINIList[j];
                                containerFlag = true;

                                break;
                            }
                        }

                        if (!containerFlag)
                        {
                            str += "|" + m.Groups["L0"].Value + ":";
                        }
                    }
                }

                //写入ini文件
                string FileDirini = this.Page.Server.MapPath("../../Private/" + SiteID.ToString());
                if (!System.IO.Directory.Exists(FileDirini))
                {
                    System.IO.Directory.CreateDirectory(FileDirini);
                }

                //取出Part
                Shove.Web.UI.ShoveWebPartFile swpf = new Shove.Web.UI.ShoveWebPartFile(long.Parse(SiteID), PageName);
                int RowNo = 0;
                DataTable dt = swpf.Read(null, null, ref RowNo);

                if (dt == null)
                {
                    JavaScript.Alert(this.Page, "导入完成，\"ShoveWebPart\"读取失败！");
                    Response.Write("<script>window.close(); parent.document.location.reload();</script>");

                    return;
                }

                foreach (DataRow dr in dt.Rows)
                {
                    if (str.ToLower().IndexOf(dr["id"].ToString().ToLower() + ",") > -1)
                    {
                        continue;
                    }

                    if (str == "")
                    {
                        str += "|" + dr["id"].ToString() + ",";
                    }
                    else
                    {
                        str += dr["id"].ToString() + ",";
                    }
                }

                if (!str.Equals(""))
                {
                    IniFile ini = new IniFile(FileDirini + "/ShoveWebPartLayout.ini");

                    ini.Write("Layout", PageName, str);

                    PublicFunction.DeleteCookie("cooldrag_" + SiteID.ToString() + "_" + PageName);
                }

                JavaScript.Alert(this.Page, "导入布局成功");
            }
            catch (Exception ex)
            {
                JavaScript.Alert(this.Page, "导入布局失败" + "，原因：" + ex.Message);

                return;
            }

            Response.Write("<script>window.close(); parent.document.location.reload();</script>");
        }

        //正则表达式替换通用方法
        public static string RegexReplace(string StrReplace, string strRegex, string NewStr)
        {
            Regex regex = new Regex(strRegex, RegexOptions.IgnoreCase | RegexOptions.Compiled);

            return regex.Replace(StrReplace, NewStr);
        }

        //正则表达式
        public static string[] Regex(string Str, string StrRegex)
        {
            Regex regex = new Regex(StrRegex, RegexOptions.IgnoreCase | RegexOptions.Compiled);
            Match m = regex.Match(Str);

            string[] strings = new string[m.Length];
            int i = 0;

            while (m.Success && i < m.Length)
            {
                strings[i] = m.Value;
                m = m.NextMatch();
                i++;
            }

            return strings;
        }
    }
}
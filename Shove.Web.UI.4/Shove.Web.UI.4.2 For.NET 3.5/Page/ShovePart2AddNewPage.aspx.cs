using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

using Shove.Web.UI;
using System.IO;

namespace Shove.Web.UI
{
    public partial class ShovePart2AddNewPage : System.Web.UI.Page
    {
        protected System.Web.UI.WebControls.Button btnOK;
        protected System.Web.UI.WebControls.TextBox tbPageName;
        protected System.Web.UI.HtmlControls.HtmlInputHidden tbResult;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Session["Shove.Web.UI.ShovePart2.RunMode"].ToString() != "DESIGN")
            {
                this.Response.Write("访问被拒绝。");
                this.Response.End();

                return;
            }
        }

        protected void btnOK_Click(object sender, EventArgs e)
        {
            string NewPageName = tbPageName.Text.Trim();
            string PageName = Request["PageName"];

            if (String.IsNullOrEmpty(NewPageName))
            {
                JavaScript.Alert(this.Page, "请输入页名。");

                return;
            }

            if (NewPageName.Equals(PageName, StringComparison.OrdinalIgnoreCase))
            {
                JavaScript.Alert(this.Page, "页名重复，请重新输入。");

                return;
            }

            string SiteID = Request["SiteID"];

            if (String.IsNullOrEmpty(SiteID))
            {
                JavaScript.Alert(this.Page, "站点未找到。");

                return;
            }

            DirectoryInfo di = new DirectoryInfo(System.AppDomain.CurrentDomain.BaseDirectory + "/Private/" + SiteID + "/PageCaches");
            if (!di.Exists)
            {
                return;
            }

            FileInfo[] files = di.GetFiles("*.aspx");
            if (files.Length > 0)
            {
                foreach (FileInfo file in files)
                {
                    if (NewPageName.Equals(file.Name.Substring(0, file.Name.LastIndexOf(".")), StringComparison.OrdinalIgnoreCase))
                    {
                        JavaScript.Alert(this.Page, "页名重复，请重新输入。");

                        return;
                    }
                }
            }

            tbResult.Value = NewPageName;
            this.Response.Write("<script type=\"text/javascript\">window.returnValue = '" + NewPageName + "'; window.close();</script>");
        }
    }
}
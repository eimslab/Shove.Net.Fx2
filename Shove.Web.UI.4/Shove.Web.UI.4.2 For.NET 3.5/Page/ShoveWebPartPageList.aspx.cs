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
    /// <summary>
    /// 多站页面列表
    /// </summary>
    public partial class ShoveWebPartPageList : System.Web.UI.Page
    {
        protected System.Web.UI.WebControls.Button btnGoto;
        protected System.Web.UI.WebControls.DropDownList ddlPages;
        protected System.Web.UI.HtmlControls.HtmlInputHidden tbResult;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Session["Shove.Web.UI.ShoveWebPart.RunMode"].ToString() != "DESIGN")
            {
                this.Response.Write("访问被拒绝。");
                this.Response.End();

                return;
            }

            if (!IsPostBack)
            {
                string SiteID = Page.Request["SiteID"];
                string PageName = Page.Request["PageName"];

                this.ViewState["SiteID"] = SiteID;
                this.ViewState["PageName"] = PageName;

                BindData();

                btnGoto.UseSubmitBehavior = false;
            }
        }

        private void BindData()
        {
            DirectoryInfo di = new DirectoryInfo(System.AppDomain.CurrentDomain.BaseDirectory + "/Private/" + this.ViewState["SiteID"].ToString() + "/PageCaches");
            if (!di.Exists)
            {
                return;
            }

            FileInfo[] files = di.GetFiles("*.aspx");
            if (files.Length == 0)
            {
                return;
            }

            ddlPages.Items.Clear();

            foreach (FileInfo file in files)
            {
                ddlPages.Items.Add(file.Name.Substring(0, file.Name.LastIndexOf(".")));
            }

            btnGoto.Enabled = (ddlPages.Items.Count > 0);
            ddlPages.Text = this.ViewState["PageName"].ToString();
        }

        protected void btnGoto_Click(object sender, EventArgs e)
        {
            if (this.ViewState["PageName"].ToString() != ddlPages.SelectedItem.Text)
            {
                this.Response.Write("<script type=\"text/javascript\">window.returnValue = '" + ddlPages.SelectedItem.Text + "'; window.close();</script>");
            }
            else
            {
                this.Response.Write("<script type=\"text/javascript\">window.returnValue = null; window.close();</script>");
            }
        }
    }
}
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
    public partial class CopyPage : System.Web.UI.Page
    {
        public string SiteID = "";
        public string PageName = "";

        protected System.Web.UI.WebControls.Button btnCopy;
        protected System.Web.UI.WebControls.DropDownList ddlPages;
        protected System.Web.UI.WebControls.Label lblMsg;
        protected System.Web.UI.HtmlControls.HtmlInputHidden tbResult;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Session["Shove.Web.UI.ShovePart2.RunMode"].ToString() != "DESIGN")
            {
                this.Response.Write("访问被拒绝。");
                this.Response.End();

                return;
            }

            if (Page.Request["SiteID"] != null)
            {
                SiteID = Page.Request["SiteID"].ToString();
            }

            if (Page.Request["PageName"] != null)
            {
                PageName = Page.Request["PageName"].ToString();
            }

            if (!IsPostBack)
            {
                BindData(SiteID, PageName);

                btnCopy.UseSubmitBehavior = false;
                btnCopy.OnClientClick = "if (!confirm(\'" + HttpUtility.HtmlEncode("从其他页面复制内容和布局到本页面，将导致所有页面原来的布局、内容都被覆盖，继续吗？") + "\')) return false; this.disabled=true";
            }
        }

        private void BindData(string SiteID, string PageName)
        {
            DirectoryInfo di = new DirectoryInfo(System.AppDomain.CurrentDomain.BaseDirectory + "/Private/" + SiteID + "/PageCaches");
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
                string t_FileName = file.Name.Substring(0, file.Name.LastIndexOf("."));
                if (t_FileName.Equals(PageName, StringComparison.OrdinalIgnoreCase))
                {
                    continue;
                }

                ddlPages.Items.Add(t_FileName);
            }
        }

        protected void btnCopy_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(ddlPages.SelectedValue))
            {
                lblMsg.Text = "请选择复制页面";

                return;
            }

            string SiteID = Page.Request["SiteID"].ToString();
            string PageName = Page.Request["PageName"].ToString();

            //string SourceFileName = System.AppDomain.CurrentDomain.BaseDirectory + "/Private/" + SiteID + "/PageCaches/" + ddlPages.SelectedValue + ".aspx";
            
            ShovePart2File swpf = new ShovePart2File(long.Parse(SiteID), PageName);
            swpf.CopyPage(PageName);

            tbResult.Value = "Copied";

            lblMsg.Text = "复制完成，请关闭此窗口。";
        }
    }
}
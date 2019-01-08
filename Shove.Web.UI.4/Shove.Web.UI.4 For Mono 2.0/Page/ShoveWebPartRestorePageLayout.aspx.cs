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
using System.IO;

using Shove.Web.UI;

namespace Shove.Web.UI
{
    /// <summary>
    /// 页面布局备份、恢复
    /// </summary>
    public partial class ShoveWebPartRestorePageLayout : System.Web.UI.Page
    {
        protected System.Web.UI.WebControls.TreeView tvFiles;
        protected System.Web.UI.HtmlControls.HtmlInputHidden tbResult;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Session["Shove.Web.UI.ShoveWebPart.RunMode"].ToString() != "DESIGN")
            {
                this.Response.Write("访问被拒绝。");
                this.Response.End();

                return;
            }

            if (!Page.IsPostBack)
            {
                if (HttpContext.Current.Request.QueryString["SiteID"] != null)
                {
                    string SiteID = HttpContext.Current.Request.QueryString["SiteID"].ToString();
                    string PageName = HttpContext.Current.Request.QueryString["PageName"].ToString();

                    this.ViewState["SiteID"] = SiteID;
                    this.ViewState["PageName"] = PageName;

                    BindData(SiteID, PageName);
                }
            }
        }

        private void BindData(string SiteID, string PageName)
        {
            tvFiles.Nodes.Clear();

            TreeNode parentNode = new TreeNode(SiteID + "页面布局备份", SiteID);
            tvFiles.Nodes.Add(parentNode);
            tvFiles.Nodes[0].ChildNodes.Add(new TreeNode("恢复到原始状态", "-1"));


            DirectoryInfo di = new DirectoryInfo(System.AppDomain.CurrentDomain.BaseDirectory + "/Private/" + SiteID + "/PageCaches/Backup/Pages/" + PageName);
            if (!di.Exists)
            {
                return;
            }

            FileInfo[] files = di.GetFiles("*.aspx.bak");

            foreach (FileInfo file in files)
            {
                TreeNode node = new TreeNode(file.Name, file.FullName);
                tvFiles.Nodes[0].ChildNodes.Add(node);
            }
        }

        protected void btnResumption_Click(object sender, EventArgs e)
        {
            string SiteID = this.ViewState["SiteID"].ToString();
            string PageName = this.ViewState["PageName"].ToString();

            TreeNodeCollection nodes = this.tvFiles.CheckedNodes;

            if (nodes.Count != 1)
            {
                ResetCheckBox(nodes);

                JavaScript.Alert(this.Page, "请选择一个备份文件进行恢复！");

                return;
            }

            string BackupFileName = nodes[0].Value;

            ShoveWebPartFile swpf = new ShoveWebPartFile(long.Parse(SiteID), PageName);
            swpf.RestorePage(BackupFileName);

            ResetCheckBox(nodes);

            tbResult.Value = "Restored";

            JavaScript.Alert(this.Page, "页面恢复成功！");
        }

        /// <summary>
        /// 清空复选框 
        /// </summary>
        /// <param name="nodes"></param>
        private void ResetCheckBox(TreeNodeCollection nodes)
        {
            for (int i = 0; i < nodes.Count; i++)
            {
                nodes[i].Checked = false;
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            string SiteID = this.ViewState["SiteID"].ToString();
            string PageName = this.ViewState["PageName"].ToString();
            ShoveWebPartFile swpf = new ShoveWebPartFile(long.Parse(SiteID), PageName);

            TreeNodeCollection nodes = this.tvFiles.CheckedNodes;

            if (nodes.Count < 1)
            {
                JavaScript.Alert(this.Page, "请选择要删除的备份文件！");

                return;
            }

            for (int i = 0; i < nodes.Count; i++)
            {
                swpf.DeletePageBackup(nodes[i].Value);
            }

            this.Response.Redirect(this.Request.Url.AbsoluteUri, true);
        }
    }
}
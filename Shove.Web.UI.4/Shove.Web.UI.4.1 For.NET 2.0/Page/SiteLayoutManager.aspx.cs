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
    /// 站点备份、恢复
    /// </summary>
    public partial class SiteLayoutManager : System.Web.UI.Page
    {
        protected System.Web.UI.WebControls.TreeView tvFiles;
        protected System.Web.UI.HtmlControls.HtmlInputHidden tbResult;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Session["Shove.Web.UI.ShovePart2.RunMode"].ToString() != "DESIGN")
            {
                this.Response.Write("访问被拒绝。");
                this.Response.End();

                return;
            }

            if (!Page.IsPostBack)
            {
                if (!String.IsNullOrEmpty(HttpContext.Current.Request.QueryString["SiteID"]))
                {
                    string SiteID = HttpContext.Current.Request.QueryString["SiteID"].ToString();

                    this.ViewState["SiteID"] = SiteID;

                    BindData(SiteID);
                }
            }
        }

        private void BindData(string SiteID)
        {
            tvFiles.Nodes.Clear();

            TreeNode parentNode = new TreeNode(SiteID + "站点布局备份", SiteID);

            tvFiles.Nodes.Add(parentNode);

            tvFiles.Nodes[0].ChildNodes.Add(new TreeNode("恢复到原始状态", "-1"));

            DirectoryInfo di = new DirectoryInfo(System.AppDomain.CurrentDomain.BaseDirectory + "/Private/" + SiteID + "/PageCaches/Backup/Site/");
            if (!di.Exists)
            {
                return;
            }

            DirectoryInfo[] directorys = di.GetDirectories();

            foreach (DirectoryInfo directory in directorys)
            {
                //TreeNode node = new TreeNode(directory.Name, directory.FullName);
                TreeNode node = new TreeNode(directory.Name, directory.FullName);
                tvFiles.Nodes[0].ChildNodes.Add(node);
            }
        }

        protected void btnResumption_Click(object sender, EventArgs e)
        {
            string SiteID = this.ViewState["SiteID"].ToString();

            TreeNodeCollection nodes = this.tvFiles.CheckedNodes;

            if (nodes.Count != 1)
            {
                ResetCheckBox(nodes);

                JavaScript.Alert(this.Page, "请选择一个备份进行恢复！");

                return;
            }
            else
            {
                string BackupDirectoryName = "";

                if (nodes[0].Value != "-1")
                {
                    BackupDirectoryName = nodes[0].Value;
                }

                ShovePart2File swpf = new ShovePart2File(long.Parse(SiteID), null);
                swpf.RestoreSite(BackupDirectoryName);

                ResetCheckBox(nodes);

                tbResult.Value = "Restored";

                JavaScript.Alert(this.Page, "站点恢复成功！");
            }
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
            ShovePart2File swpf = new ShovePart2File(long.Parse(SiteID), null);

            TreeNodeCollection nodes = this.tvFiles.CheckedNodes;

            if (nodes.Count < 1)
            {
                JavaScript.Alert(this.Page, "请选择要删除的备份文件！");

                return;
            }

            for (int i = 0; i < nodes.Count; i++)
            {
                swpf.DeleteSiteBackup(nodes[i].Value);
            }

            this.Response.Redirect(this.Request.Url.AbsoluteUri, true);
        }
    }
}